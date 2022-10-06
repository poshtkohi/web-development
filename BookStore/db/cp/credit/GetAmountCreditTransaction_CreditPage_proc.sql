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
	   WHERE  name = 'GetAmountCreditTransaction_CreditPage_proc'
	   AND 	  type = 'P')
    DROP PROCEDURE GetAmountCreditTransaction_CreditPage_proc
GO

CREATE PROCEDURE GetAmountCreditTransaction_CreditPage_proc

@ReservationCode BIGINT,
@amount BIGINT OUTPUT


AS

SET @amount=0;
SELECT @amount=amount FROM [transaction] WHERE [id]=@ReservationCode;

RETURN 