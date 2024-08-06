DROP FUNCTION IF EXISTS SaveBuyNegotiation;
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

DROP FUNCTION IF EXISTS SaveSellNegotiation;
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