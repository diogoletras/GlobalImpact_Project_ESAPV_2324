import http from 'k6/http';
import { sleep, check } from 'k6';
import { sharedArray } from "k6/data";

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '10s', target: 10 }, // below normal load
        { duration: '1m', target: 10 },
        { duration: '10s', target: 100 }, // spike to 1400 users
        { duration: '3m', target: 100 }, // stay at 1400 for 3 minutes
        { duration: '10s', target: 10 }, // scale down. Recover stage.
        { duration: '3m', target: 10 },
        { duration: '10s', target: 0 }, // scale down. Recovery stage.
    ]
};

const url = 'https://localhost:7154';

const payload = {
    UserName: 'Cliente',
    Password: 'Cliente123',
    RememberMe: false,
    // add more properties as needed
};

export default function () {
    const response = http.get(url + "/Account/Login");

    const token = response.html().find('input[name="__RequestVerificationToken"]').attr('value');

    http.post(url + "/Account/Login", JSON.stringify(payload), {
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': token,
        },
    });
}