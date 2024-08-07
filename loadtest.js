import { check } from 'k6';
import http from 'k6/http';

const only422Callback = http.expectedStatuses(422);

export const options = {
    vus: 5,
    duration: '120s',
    thresholds: {
        'http_req_duration{type:NegotiationSave}': ['p(99)<100'], 
        'http_req_duration{type:GetClientProductList}': ['p(99)<100'],
    },
};

export default function () {
    let data = {
        ProductId: Math.floor(Math.random() * 10) + 1, 
        ClientId: Math.floor(Math.random() * 10) + 1, 
        Quantity: Math.floor(Math.random() * 5) + 1, 
        OperationTypeId: Math.floor(Math.random() * 2) + 1 
    };

    const response = http.post('http://localhost:8080/Negotiation/Save', JSON.stringify(data), {
        headers: { 'Content-Type': 'application/json' }, tags: { type: 'NegotiationSave' }, responseCallback: only422Callback,
    });

    check(response, {
        'NegotiationSave is status 201': (r) => r.status === 201,
        'NegotiationSave is status 422': (r) => r.status === 422,
        'NegotiationSave is status 500': (r) => r.status === 500,
    });

    const clientId = Math.floor(Math.random() * 10) + 1;
    const responseGet = http.get(`http://localhost:8080/Product/GetClientProductList?clientId=${clientId}` , {
        tags: { type: 'GetClientProductList' },
    });

    check(responseGet, {
        'GetClientProductList is status 200': (r) => r.status === 200,
        'GetClientProductList is status 204': (r) => r.status === 204,
        'GetClientProductList is status 500': (r) => r.status === 500,
    });
}