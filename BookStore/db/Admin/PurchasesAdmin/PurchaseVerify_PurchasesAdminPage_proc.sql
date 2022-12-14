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
	   WHERE  name = 'PurchaseVerify_PurchasesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PurchaseVerify_PurchasesAdminPage_proc
GO

CREATE PROCEDURE PurchaseVerify_PurchasesAdminPage_proc


@PurchaseID BIGINT

AS

UPDATE purchases SET PurchaseCompleted=1 WHERE [id]=@PurchaseID AND IsDeleted=0 AND PurchaseCompleted=0;
IF(@@ROWCOUNT>0)
BEGIN
	UPDATE ShoppingCart SET PurchaseCompleted=1 WHERE PurchaseID=@PurchaseID AND IsDeleted=0;
END

RETURN 