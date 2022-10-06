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
	   WHERE  name = 'PurchaseDelete_PurchasesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PurchaseDelete_PurchasesAdminPage_proc
GO

CREATE PROCEDURE PurchaseDelete_PurchasesAdminPage_proc


@PurchaseID BIGINT

AS

UPDATE purchases SET IsDeleted=1 WHERE [id]=@PurchaseID AND IsDeleted=0;

RETURN 