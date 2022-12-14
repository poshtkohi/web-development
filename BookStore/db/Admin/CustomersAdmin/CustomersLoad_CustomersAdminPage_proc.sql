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
	   WHERE  name = 'CustomersLoad_CustomersAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CustomersLoad_CustomersAdminPage_proc
GO

CREATE PROCEDURE CustomersLoad_CustomersAdminPage_proc

@CustomerID BIGINT

AS

SELECT PersianCustomerName,EnglishCustomerName,CustomerLink FROM customers WHERE [id]=@CustomerID AND IsDeleted=0;

RETURN;
