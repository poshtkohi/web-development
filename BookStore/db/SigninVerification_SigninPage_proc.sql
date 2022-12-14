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
	   WHERE  name = 'SigninVerification_SigninPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SigninVerification_SigninPage_proc
GO

CREATE PROCEDURE SigninVerification_SigninPage_proc

@_ref VARCHAR(50),
@IsVerified BIT OUTPUT

AS

DECLARE @username NVARCHAR(50);
DECLARE @password NVARCHAR(50);
DECLARE @name NVARCHAR(50);
DECLARE @email NVARCHAR(50);
DECLARE @address NVARCHAR(200);
DECLARE @PostalCode NVARCHAR(50);
DECLARE @tel NVARCHAR(50);
DECLARE @date DATETIME;
DECLARE @BirthYear INT;
DECLARE @sex BIT;
DECLARE @CountryKey VARCHAR(3);
DECLARE @ProvinceKey VARCHAR(3);

DECLARE @_id BIGINT;

SELECT @_id=[id],@username=username,@password=[password],@name=[name],@email=email,@address=address,@PostalCode=PostalCode,@tel=tel,@date=[date],@BirthYear=BirthYear,@sex=sex,@CountryKey=CountryKey,@ProvinceKey=ProvinceKey FROM [accounts-preverify] WHERE IsDeleted=0 AND VerificationCode=@_ref;


IF @@ROWCOUNT=0
BEGIN
	SET @IsVerified=0;
	RETURN;
END

ELSE
BEGIN
	INSERT INTO accounts (username,[password],[name],email,address,PostalCode,tel,[date],BirthYear,sex,CountryKey,ProvinceKey) VALUES(@username,@password,@name,@email,@address,@PostalCode,@tel,@date,@BirthYear,@sex,@CountryKey,@ProvinceKey);
	UPDATE [accounts-preverify] SET IsDeleted=1 WHERE [id]=@_id;
	SET @IsVerified=1;
	RETURN;
END
