import http from 'k6/http';

export const options = {
    vus: 10,
    duration: '30s',
    thresholds: {
        http_req_failed: ['rate<0.01'], // http errors should be less than 1%
        http_req_duration: ['p(95)<200'], // 95% of requests should be below 200ms
    },
};

export default function () {
    let data = {
        ProductId: 3,
        ClientId: 1,
        Quantity: 1,
        OperationTypeId: 1
    };

    http.post('http://localhost:8080/Negotiation/Save', JSON.stringify(data), {
        headers: { 'Content-Type': 'application/json' }
    });
}