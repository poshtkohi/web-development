--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]

GO

-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowPurchaseInfoByPurchaseID_PurchaseDetailsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowPurchaseInfoByPurchaseID_PurchaseDetailsPage_proc
GO

CREATE PROCEDURE ShowPurchaseInfoByPurchaseID_PurchaseDetailsPage_proc


@PurchaseID BIGINT

AS
	SELECT ac.username AS CustomerUsername,ac.[name] AS CustomerName,ac.address AS CustomerAddress,ac.tel AS CustomerTel,ac.PostalCode AS CustomerPostalCode,ac.email AS CustomerEmail,pu.PurchaseDate,pu.PursuitCode FROM purchases AS pu
		INNER JOIN accounts AS ac
		on ac.[id]=pu.[UserID]
		WHERE pu.IsDeleted=0 AND pu.[id]=@PurchaseID;
RETURN 