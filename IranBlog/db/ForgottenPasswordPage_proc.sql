--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [general]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ForgottenPassword_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ForgottenPassword_HomePage_proc
GO

CREATE PROCEDURE ForgottenPassword_HomePage_proc

@username NVARCHAR(12),
@email NVARCHAR(30)

AS

SELECT [password] FROM usersInfo WHERE [username]=@username AND email=@email;

RETURN;