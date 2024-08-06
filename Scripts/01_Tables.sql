DROP INDEX IF EXISTS idx_client_clientid;
DROP INDEX IF EXISTS idx_product_productid;
DROP INDEX IF EXISTS idx_clientid_productid;
DROP INDEX IF EXISTS idx_operationtype_operationtypeid;
DROP INDEX IF EXISTS idx_negotiation_egotiationid;

DROP TABLE IF EXISTS Negotiation;
DROP TABLE IF EXISTS OperationType;
DROP TABLE IF EXISTS ClientProduct;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS Client;


CREATE TABLE Client(ClientId SERIAL PRIMARY KEY, Name VARCHAR(200) NOT NULL, RegisterDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP);

CREATE TABLE Product(ProductId SERIAL PRIMARY KEY, Name VARCHAR(100) NOT NULL, Quantity INTEGER NOT NULL, RegisterDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, ProductDueDate TIMESTAMP NOT NULL);

CREATE TABLE ClientProduct(ClientProductId SERIAL PRIMARY KEY, ClientId INTEGER NOT NULL, ProductId INTEGER NOT NULL, Quantity INTEGER NOT NULL,
CONSTRAINT fk_clientproduct_clientId FOREIGN KEY(ClientId) REFERENCES Client(ClientId),
CONSTRAINT fk_clientproduct_productId FOREIGN KEY(ProductId) REFERENCES Product(ProductId));

CREATE TABLE OperationType(OperationTypeId INTEGER NOT NULL, Name VARCHAR(10));

CREATE TABLE Negotiation(NegotiationId SERIAL PRIMARY KEY, ClientProductId INTEGER NOT NULL, Quantity INTEGER NOT NULL, OperationTypeId INTEGER NOT NULL, RegisterDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
CONSTRAINT fk_negotiation_clientproductid FOREIGN KEY(ClientProductId) REFERENCES ClientProduct(ClientProductId));

CREATE INDEX idx_client_clientid ON Client(ClientId);

CREATE INDEX idx_product_productid ON Product(ProductId);

CREATE UNIQUE INDEX idx_clientid_productid ON ClientProduct (ClientId, ProductId);

CREATE UNIQUE INDEX idx_operationtype_operationtypeid ON OperationType(OperationTypeId);

CREATE INDEX idx_negotiation_egotiationid ON Negotiation(NegotiationId);