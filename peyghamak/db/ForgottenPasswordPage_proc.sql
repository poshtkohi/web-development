USE [peyghamak-accounts]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ForgottenPasswordPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ForgottenPasswordPage_proc
GO

CREATE PROCEDURE ForgottenPasswordPage_proc

@username NVARCHAR(50)

AS

SELECT email,[password] FROM accounts WHERE IsDeleted=0 AND [username]=@username;

RETURN;