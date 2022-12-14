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
	   WHERE  name = 'CommitCreditTransaction_CreditPage_proc'
	   AND 	  type = 'P')
    DROP PROCEDURE CommitCreditTransaction_CreditPage_proc
GO

CREATE PROCEDURE CommitCreditTransaction_CreditPage_proc

@ReservationCode BIGINT,
@amount BIGINT


AS

DECLARE @UserID BIGINT;
UPDATE [transaction] SET IsPaied=1 WHERE [id]=@ReservationCode;
SELECT @UserID=UserID FROM [transaction] WHERE [id]=@ReservationCode;
UPDATE accounts SET UserCredits=UserCredits+@amount WHERE [id]=@UserID;

RETURN 