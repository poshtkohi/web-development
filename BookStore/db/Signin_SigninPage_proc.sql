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
	   WHERE  name = 'Signin_SigninPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Signin_SigninPage_proc
GO

CREATE PROCEDURE Signin_SigninPage_proc

@username NVARCHAR(50),
@password NVARCHAR(50),
@UserID BIGINT OUTPUT,
@IsLogined BIT OUTPUT

AS

SELECT @UserID=[id] FROM accounts WHERE IsDeleted=0 AND [username]=@username AND [password]=@password;


IF @@ROWCOUNT=0
BEGIN
	SET @IsLogined=0;
	RETURN;
END

ELSE
BEGIN
	SET @IsLogined=1;
	RETURN;
END
