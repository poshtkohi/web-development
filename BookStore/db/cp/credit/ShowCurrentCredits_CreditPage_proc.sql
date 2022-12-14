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
	   WHERE  name = 'ShowCurrentCredits_CreditPage_proc'
	   AND 	  type = 'P')
    DROP PROCEDURE ShowCurrentCredits_CreditPage_proc
GO

CREATE PROCEDURE ShowCurrentCredits_CreditPage_proc

@UserID BIGINT,
@CurrentCredit BIGINT OUTPUT

AS

SET @CurrentCredit=0;

SELECT @CurrentCredit=UserCredits FROM accounts WHERE [id]=@UserID AND IsDeleted=0;


RETURN 