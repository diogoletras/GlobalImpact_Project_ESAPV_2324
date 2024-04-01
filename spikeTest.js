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

export default () => {
    const res = http.get(url);
};