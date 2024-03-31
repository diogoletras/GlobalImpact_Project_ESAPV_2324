//export const options = {
import http from 'k6/http';
import { sleep, check } from 'k6';
import { sharedArray } from "k6/data"; }

export const options = {
    stages: [
        { duration: '30s', target: 200 }, // ramp up to 200 users
        { duration: '5m', target: 200 }, // stay at 200 users for 10 minutes
        { duration: '30s', target: 0 }, // ramp down to 0 users
    ],
    thresholds: {
        http_req_duration: ['p(95)<250'], // 95% of requests must complete below 250ms
    }
};

export default () => {
    const res = http.get('https://localhost:7154/Account/Register');
    sleep(1);
};



//k6 run --out cloud loadTest.js