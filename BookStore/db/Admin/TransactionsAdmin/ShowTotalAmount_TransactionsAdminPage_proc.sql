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
	   WHERE  name = 'ShowTotalAmount_TransactionsAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowTotalAmount_TransactionsAdminPage_proc
GO

CREATE PROCEDURE ShowTotalAmount_TransactionsAdminPage_proc

@ShowTotalAmount BIGINT OUTPUT

AS

SET @ShowTotalAmount=0;
SELECT @ShowTotalAmount=SUM(amount) FROM [transaction] WHERE IsPaied=1 AND IsDeleted=0;

RETURN