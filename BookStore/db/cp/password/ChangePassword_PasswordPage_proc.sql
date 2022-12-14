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
	   WHERE  name = 'ChangePassword_PasswordPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ChangePassword_PasswordPage_proc
GO

CREATE PROCEDURE ChangePassword_PasswordPage_proc

@UserID BIGINT,
@NewPassword NVARCHAR(50),
@LastPassword NVARCHAR(50),
@NumAffectedRows INT OUTPUT

AS

UPDATE accounts SET [password]=@NewPassword WHERE [id]=@UserID AND [password]=@LastPassword AND IsDeleted=0; 
SET @NumAffectedRows=@@ROWCOUNT;

RETURN;