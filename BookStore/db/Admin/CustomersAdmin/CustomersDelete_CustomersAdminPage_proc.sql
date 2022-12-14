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
	   WHERE  name = 'CustomersDelete_CustomersAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CustomersDelete_CustomersAdminPage_proc
GO

CREATE PROCEDURE CustomersDelete_CustomersAdminPage_proc

@CustomersID BIGINT

AS


--BEGIN TRANSACTION

UPDATE customers SET IsDeleted=1 WHERE [id]=@CustomersID;

RETURN