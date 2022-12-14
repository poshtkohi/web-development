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
	   WHERE  name = 'VerifyMobileNumber_MobilePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE VerifyMobileNumber_MobilePage_proc
GO

CREATE PROCEDURE VerifyMobileNumber_MobilePage_proc

@MobileNumber NVARCHAR(20),
@VerificationCode VARCHAR(10),
@BlogID BIGINT OUTPUT,
@IsExisted BIT OUTPUT,
@IsVerified BIT OUTPUT

AS

SET @IsExisted = 0;
SET @IsVerified = 0;

SELECT TOP 1 @BlogID=[id] FROM accounts WHERE mobile=@MobileNumber AND IsDeleted=0;
IF (@@ROWCOUNT != 0)
BEGIN
	SET @IsExisted = 1;
	SET @IsVerified = 1;
	RETURN;
END

ELSE
BEGIN
	BEGIN TRANSACTION	
		DECLARE @_verID BIGINT;
		SELECT TOP 1 @_verID=[id],@BlogID=BlogID FROM MobileVerification WHERE VerificationCode=@VerificationCode AND MobileNumber=@MobileNumber;
		IF (@@ROWCOUNT != 0)
		BEGIN
			UPDATE accounts SET mobile=@MobileNumber WHERE [id]=@BlogID;
			DELETE FROM MobileVerification WHERE [id]=@_verID;
			SET @IsExisted = 0;
			SET @IsVerified = 1;
		END	
	COMMIT
END

RETURN;
