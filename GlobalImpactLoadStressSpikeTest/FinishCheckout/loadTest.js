//export const options = {
import http from 'k6/http';
import { sleep, check } from 'k6';
import { sharedArray } from "k6/data";

const url = 'https://localhost:7154';

export const options = {
    stages: [
        { duration: '5s', target: 1 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes.
        { duration: '10s', target: 1 }, // stay at 200 users for 10 minutes
        { duration: '5s', target: 0 }, // ramp down to 0 users
    ],
    thresholds: {
        http_req_duration: ['p(95)<3000'], // 95% of requests must complete below 3 sec
    }
};

let id = '321c58d1-5964-46ef-8ca1-752fbda5c9f7'


export default () => {
    http.get(url + '/Store/Add/' + id);

    let data = {
        name: "Cliente",
        total: 0
    }

    const res = http.get(url + '/Store/FinalizeCheckout?name=' + data.name + '&total=' + data.total)
    sleep(1);
};



//k6 run --out cloud loadTest.js