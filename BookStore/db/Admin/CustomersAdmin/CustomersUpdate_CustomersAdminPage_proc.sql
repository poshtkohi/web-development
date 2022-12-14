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
	   WHERE  name = 'CustomersUpdate_CustomersAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CustomersUpdate_CustomersAdminPage_proc
GO

CREATE PROCEDURE CustomersUpdate_CustomersAdminPage_proc

@CustomersID BIGINT,
@PersianCustomerName NVARCHAR(200),
@EnglishCustomerName NVARCHAR(200),
@CustomerLink NVARCHAR(200)

AS

UPDATE customers SET PersianCustomerName=@PersianCustomerName,EnglishCustomerName=@EnglishCustomerName,CustomerLink=@CustomerLink WHERE [id]=@CustomersID AND IsDeleted=0;

RETURN;
