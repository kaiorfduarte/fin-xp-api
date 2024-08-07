**Como executar o projeto:**

**Pre-requisitos**
 - Ter o docker na maquina;
 - Ter o gitbash;
 - Instalar o k6;
     - Instacao windows:
         winget install k6 --source winget
         https://dl.k6.io/msi/k6-latest-amd64.msi
     - Instalacao linux:
         sudo gpg -k
         sudo gpg --no-default-keyring --keyring /usr/share/keyrings/k6-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys C5AD17C747E3415A3642D57D77C6C491D6AC1D69
         echo "deb [signed-by=/usr/share/keyrings/k6-archive-keyring.gpg] https://dl.k6.io/deb stable main" | sudo tee /etc/apt/sources.list.d/k6.list
         sudo apt-get update
         sudo apt-get install k6

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

**Passo 2 - Executando o teste de carga do K6**

Ainda no diretorio raiz do projeto, executar o comando:
```
k6 run loadtest.js
```
O script esta configurado para executar 5 vus com duracao de 120 segundos.
Temos configurado as metricas NegotiationSave e GetClientProductList, onde NegotiationSave ele vai salvar as negociacoes de compra/venda e o outro vai buscar a quantidade de produtos que o cliente possui.
Foi criado 10 Clientes e 10 Produtos, nesse script vai ser randomizado os dois.
No topo do resultado, sera tambem informado a quantidade de request por status.
