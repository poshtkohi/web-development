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
	   WHERE  name = 'RegCreditTransaction_CreditPage_proc'
	   AND 	  type = 'P')
    DROP PROCEDURE RegCreditTransaction_CreditPage_proc
GO

CREATE PROCEDURE RegCreditTransaction_CreditPage_proc

@UserID BIGINT,
@amount INT,
@ReservationCode BIGINT OUTPUT


AS


INSERT INTO [transaction] (UserID,amount) VALUES(@UserID,@amount);
SET @ReservationCode = @@IDENTITY;

RETURN 