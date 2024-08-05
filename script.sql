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

DROP FUNCTION IF EXISTS SaveBuyNegotiation;
DROP FUNCTION IF EXISTS SaveSellNegotiation;

CREATE TABLE Client(ClientId SERIAL PRIMARY KEY, Name VARCHAR(200) NOT NULL, RegisterDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP);

CREATE TABLE Product(ProductId SERIAL PRIMARY KEY, Name VARCHAR(100) NOT NULL, Quantity INTEGER NOT NULL, RegisterDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, ProductDueDate TIMESTAMP NOT NULL);

CREATE TABLE ClientProduct(ClientProductId SERIAL PRIMARY KEY, ClientId INTEGER NOT NULL, ProductId INTEGER NOT NULL, Quantity INTEGER NOT NULL,
CONSTRAINT fk_clientproduct_clientId FOREIGN KEY(ClientId) REFERENCES Client(ClientId),
CONSTRAINT fk_clientproduct_productId FOREIGN KEY(ProductId) REFERENCES Product(ProductId));

CREATE TABLE OperationType(OperationTypeId INTEGER NOT NULL, Name VARCHAR(10));

CREATE TABLE Negotiation(NegotiationId SERIAL PRIMARY KEY, ClientProductId INTEGER NOT NULL, Quantity INTEGER NOT NULL, OperationType INTEGER NOT NULL, RegisterDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
CONSTRAINT fk_negotiation_clientproductid FOREIGN KEY(ClientProductId) REFERENCES ClientProduct(ClientProductId));

CREATE INDEX idx_client_clientid ON Client(ClientId);
CREATE INDEX idx_product_productid ON Product(ProductId);
CREATE UNIQUE INDEX idx_clientid_productid ON ClientProduct (ClientId, ProductId);
CREATE UNIQUE INDEX idx_operationtype_operationtypeid ON OperationType(OperationTypeId);
CREATE INDEX idx_negotiation_egotiationid ON Negotiation(NegotiationId);

CREATE OR REPLACE FUNCTION SaveBuyNegotiation(ex_clientId INTEGER, ex_productId INTEGER, ex_quantity INTEGER)
RETURNS bool
LANGUAGE plpgsql 
AS $$
DECLARE
    result bool;
	qtd INTEGER;
	iclientProductId INTEGER;
	ioperationType INTEGER = 1;
BEGIN   
   IF EXISTS (SELECT 1 FROM Product WHERE ProductId = ex_productId AND Quantity >= ex_quantity) THEN
   		result := true;
		UPDATE Product SET Quantity = Quantity - ex_quantity WHERE ProductId = ex_productId;
		
		IF EXISTS (SELECT 1 FROM ClientProduct WHERE ClientId = ex_clientId AND ProductId = ex_productId) THEN
			UPDATE ClientProduct SET Quantity = Quantity + ex_quantity WHERE ClientId = ex_clientId AND ProductId = ex_productId RETURNING ClientProductId INTO iclientProductId;
		else
			INSERT INTO ClientProduct (ClientId, ProductId, Quantity) VALUES (ex_clientId, ex_productId, ex_quantity) RETURNING ClientProductId INTO iclientProductId;
		END IF;	

		INSERT INTO Negotiation (ClientProductId, Quantity, OperationTypeId) VALUES (iclientProductId, ex_quantity, ioperationType);
	else
		result := false;
   END IF;
   
   RETURN result;
END;
$$;

CREATE OR REPLACE FUNCTION SaveSellNegotiation(ex_clientId INTEGER, ex_productId INTEGER, ex_quantity INTEGER)
RETURNS bool
LANGUAGE plpgsql 
AS $$
DECLARE
    result bool;
	qtd INTEGER;
	iclientProductId INTEGER;
	ioperationType INTEGER = 2;
BEGIN   
   IF EXISTS (SELECT 1 FROM ClientProduct WHERE ClientId = ex_clientId AND ProductId = ex_productId AND Quantity >= ex_quantity) THEN
   		result := true;
		UPDATE ClientProduct SET Quantity = Quantity - ex_quantity WHERE ClientId = ex_clientId AND ProductId = ex_productId RETURNING ClientProductId INTO iclientProductId;
		
		UPDATE Product SET Quantity = Quantity + ex_quantity WHERE ProductId = ex_productId;			

		INSERT INTO Negotiation (ClientProductId, Quantity, OperationTypeId) VALUES (iclientProductId, ex_quantity, ioperationType);
	else
		result := false;
   END IF;
   
   RETURN result;
END;
$$;

INSERT INTO OperationType (OperationTypeId, Name) VALUES (1, 'Buy');
INSERT INTO OperationType (OperationTypeId, Name) VALUES (2, 'Sell');

INSERT INTO Client (Name) VALUES ('Cliente A');
INSERT INTO Client (Name) VALUES ('Cliente B');
INSERT INTO Client (Name) VALUES ('Cliente C');
INSERT INTO Client (Name) VALUES ('Cliente D');
INSERT INTO Client (Name) VALUES ('Cliente E');

INSERT INTO Product (Name, Quantity, ProductDueDate) VALUES ('Produto A', 10000, '2025-08-05');
INSERT INTO Product (Name, Quantity, ProductDueDate) VALUES ('Produto B', 10000, '2024-09-05');
INSERT INTO Product (Name, Quantity, ProductDueDate) VALUES ('Produto C', 10000, '2025-10-05');
INSERT INTO Product (Name, Quantity, ProductDueDate) VALUES ('Produto D', 10000, '2024-11-05');
INSERT INTO Product (Name, Quantity, ProductDueDate) VALUES ('Produto E', 10000, '2024-12-05');