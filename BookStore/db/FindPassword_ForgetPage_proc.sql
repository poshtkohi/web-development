--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'FindPassword_ForgetPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE FindPassword_ForgetPage_proc
GO

CREATE PROCEDURE FindPassword_ForgetPage_proc

@username NVARCHAR(50)

AS

SELECT email,[password] FROM accounts WHERE IsDeleted=0 AND [username]=@username;

RETURN;