//export const options = {
import http from 'k6/http';
import { sleep, check } from 'k6';

const url = 'https://localhost:7154';

export const options = {
    stages: [
        { duration: '5m', target: 100 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes.
        { duration: '10m', target: 100 }, // stay at 200 users for 10 minutes
        { duration: '5m', target: 0 }, // ramp down to 0 users
    ],
    thresholds: {
        http_req_duration: ['p(95)<3000'], // 95% of requests must complete below 3 sec
    }
};

const payload = {
    UserName: 'Cliente',
    Password: 'Cliente123',
    RememberMe: false,
    // add more properties as needed
};

export default function () {
    const response = http.get(url + "/Account/Login");

    const token = response.html().find('input[name="__RequestVerificationToken"]').attr('value');

    http.post(url +"/Account/Login", JSON.stringify(payload), {
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': token,
        },
    });
}



//k6 run --out cloud loadTest.js