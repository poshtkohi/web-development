--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-accounts]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'SigninPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SigninPage_proc
GO

CREATE PROCEDURE SigninPage_proc

@username NVARCHAR(50),
@password NVARCHAR(50),
@BlogID BIGINT OUTPUT,
@name NVARCHAR(50) OUTPUT,
@ImageGuid VARCHAR(50) OUTPUT,
@ThemeString NVARCHAR(50) OUTPUT,
@IsLogined BIT OUTPUT

AS

SELECT @BlogID=[id],@name=[name],@ImageGuid=ImageGuid,@ThemeString=(SELECT ThemeString FROM themes WHERE themes.[id]=accounts.ThemeID) FROM 
	accounts WHERE IsDeleted=0 AND [username]=@username AND [password]=@password;


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
