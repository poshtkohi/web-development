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
	   WHERE  name = 'CustomersInsert_CustomersAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CustomersInsert_CustomersAdminPage_proc
GO

CREATE PROCEDURE CustomersInsert_CustomersAdminPage_proc

@PersianCustomerName NVARCHAR(200),
@EnglishCustomerName NVARCHAR(200),
@CustomerLink NVARCHAR(200)

AS

INSERT INTO customers (PersianCustomerName,EnglishCustomerName,CustomerLink) VALUES(@PersianCustomerName,@EnglishCustomerName,@CustomerLink);

RETURN;
