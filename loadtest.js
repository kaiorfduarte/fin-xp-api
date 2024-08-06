import { check } from 'k6';
import http from 'k6/http';

const only422Callback = http.expectedStatuses(422);

export const options = {
    vus: 1,
    duration: '10s',
    thresholds: {
        'http_req_duration{type:NegotiationSave}': ['p(99)<100'], 
        'http_req_duration{type:GetClientProductList}': ['p(99)<100'],
    },
};

export default function () {
    let data = {
        ProductId: Math.floor(Math.random() * 5) + 1, //gera de 1 a 5 os produtos
        ClientId: Math.floor(Math.random() * 5) + 1, //gera de 1 a 5 os clientes
        Quantity: Math.floor(Math.random() * 5) + 1, //gera de 1 a 5 as quantidades
        OperationTypeId: Math.floor(Math.random() * 2) + 1 //1 para compra e 2 para venda
    };

    const response = http.post('http://localhost:8080/Negotiation/Save', JSON.stringify(data), {
        headers: { 'Content-Type': 'application/json' }, tags: { type: 'NegotiationSave' }, responseCallback: only422Callback,
    });

    check(response, {
        'save is status 204': (r) => r.status === 204,
        'save is status 201': (r) => r.status === 201,
        'save is status 422': (r) => r.status === 422,
        'save is status 500': (r) => r.status === 500,
    });

    const clientId = Math.floor(Math.random() * 5) + 1;
    const responseGet = http.get(`http://localhost:8080/Product/GetClientProductList?clientId=${clientId}` , {
        tags: { type: 'GetClientProductList' },
    });

    check(responseGet, {
        'list is status 200': (r) => r.status === 200,
        'list is status 204': (r) => r.status === 204,
        'list is status 500': (r) => r.status === 500,
    });
}