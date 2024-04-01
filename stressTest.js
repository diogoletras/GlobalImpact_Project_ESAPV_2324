/*
    Stress Testing is a type of testing that determine the limits of the software application.
    The purpose of this test is to verify the stability and reliability of the system under extreme conditions.

    Run a stress test to:
        - Determine the upper limits of capacity within the system
        - Evaluate how the system behaves under extreme conditions
        - Determine the breaking point of the system
        - Determine how the system recovers from failure
*/

import http from 'k6/http';
import { sleep, check } from 'k6';
import { sharedArray } from "k6/data";

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '2m', target: 10 }, // below normal load
        { duration: '5m', target: 10 },
        { duration: '2m', target: 25 }, // normal load
        { duration: '5m', target: 25 },
        { duration: '2m', target: 50 }, // around the breaking point
        { duration: '5m', target: 50 },
        { duration: '2m', target: 75 }, // beyond the breaking point
        { duration: '5m', target: 75 },
        { duration: '10m', target: 0 }, // scale down. Recovery stage.
    ]
};

const url = 'https://localhost:7154';

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