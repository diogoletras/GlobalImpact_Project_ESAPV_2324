//export const options = {
import http from 'k6/http';
import { sleep, check } from 'k6';
import { sharedArray } from "k6/data";

const url = 'https://localhost:7154';

export const options = {
    stages: [
        { duration: '5m', target: 100 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes.
        { duration: '10m', target: 100 }, // stay at 200 users for 10 minutes
        { duration: '5m', target: 0 }, // ramp down to 0 users
    ],
    thresholds: {
        http_req_duration: ['p(95)<1000'], // 95% of requests must complete below 150ms
    }
};

export default () => {
    let data = {
        idEco: "84696df6-c4c5-407b-870b-2802dc3fab23",
        nome: "teste1",
        peso: 0,
        pontos: 0,
    }

    const res = http.post(url + '/RecyclingTransaction/FinishRecycling', data)
    sleep(1);
};



//k6 run --out cloud loadTest.js