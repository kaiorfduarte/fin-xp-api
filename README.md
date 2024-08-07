**Como executar o projeto:**

**Pre-requisitos**
 - Ter o docker na maquina;
 - Ter o gitbash;
 - Instalar o k6;
     - Instacao windows:
       ```
         winget install k6 --source winget
       ```
         - https://dl.k6.io/msi/k6-latest-amd64.msi
     - Instalacao linux:
       ```
         sudo gpg -k
         sudo gpg --no-default-keyring --keyring /usr/share/keyrings/k6-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys C5AD17C747E3415A3642D57D77C6C491D6AC1D69
         echo "deb [signed-by=/usr/share/keyrings/k6-archive-keyring.gpg] https://dl.k6.io/deb stable main" | sudo tee /etc/apt/sources.list.d/k6.list
         sudo apt-get update
         sudo apt-get install k6
       ```

**Passo 1 - Subindo os containers**

Depois de baixar o repositorio do git, basta ir na pasta raiz do projeto via terminal e executar o comando:
```
docker-compose up -d
```
Ele ira cria/baixar as imagens e montar o container atraves do arquivo docker-compose.
Caso queira dar um stop, executar o comando:
```
docker-compose stop
```

Na raiz do projeto temos a pasta de Scripts, nela possui 3 arquivos que sera executado junto ao docker-compose, nessa sequecia:
- 01_Tables.sql
- 02_Functions.sql
- 03_Execute.sql

**Passo 2 - Executando o teste de carga do K6**

Ainda no diretorio raiz do projeto, executar o comando:
```
k6 run loadtest.js
```
- O script esta configurado para executar 5 vus com duracao de 60 segundos.
- Temos configurado as metricas NegotiationSave e GetClientProductList, onde NegotiationSave ele vai salvar as negociacoes de compra/venda e o outro vai buscar a quantidade de produtos que o cliente possui.
- Foi criado 10 Clientes e 10 Produtos, nesse script vai ser randomizado os dois.
- No topo do resultado, sera tambem informado a quantidade de request por status.


**Como utilizar o projeto:**

**Compra ou venda**
Para compra ou vendar um produto, deve se fazer um post no endpoint: **http://localhost:8080/Negotiation/Save**

Passando no corpo os seguinte parametros no formato json:

- ClientId -> id identificar do cliente
- ProductId -> id identificar do produto
- Quantity -> quantidade do produto a ser negociado
- OperationTypeId -> tipo da operacao, enum definido como 1 sendo a compra e 2 sendo a venda.

Exemplo do body:
```
{
	"ProductId": 1,
	"ClientId": 1,
	"Quantity": 10,
	"OperationTypeId":1
}
```

**Consulta do extrato do cliente**
Na consulta do extrato, deve se fazer um get no endpoint: **http://localhost:8080/Product/GetClientProductList?clientId={id}**

Na query string do endpoint, deve ser passado no parametro clientId, o id identificar do cliente.

Exemplo do response:

```
[
	{
		"productId": 7,
		"name": "Produto G",
		"quantity": 29
	}
]
```

**Consulta os produtos disponiveis para negociacao**
Na consulta de produtos disponiveis, deve se fazer um get no endpoint: **http://localhost:8080/Product/GetProductList**

Exemplo do response:

```
[
	{
		"productId": 7,
		"name": "Produto G",
		"quantity": 9147,
		"registerDate": "2024-08-07T02:36:03.322465",
		"productDueDate": "2024-09-05T00:00:00"
	}
]
```
