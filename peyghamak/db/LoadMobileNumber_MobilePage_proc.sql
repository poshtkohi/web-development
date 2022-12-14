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
	   WHERE  name = 'LoadMobileNumber_MobilePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE LoadMobileNumber_MobilePage_proc
GO

CREATE PROCEDURE LoadMobileNumber_MobilePage_proc

@BlogID BIGINT,
@MobileNumber NVARCHAR(20) OUTPUT, --this parameter concurrently is used for input and output varitaions through the code flow
@VerificationCode VARCHAR(10) OUTPUT, --this parameter concurrently is used for input and output varitaions through the code flow
@IsExisted BIT OUTPUT, --use this parameter to test whether the mobile is existed or not 
@HasAlreadyVerified BIT OUTPUT

AS

DECLARE @_mobileNumber NVARCHAR(20);
DECLARE @_verificationCode VARCHAR(10);

SELECT @_mobileNumber=mobile FROM accounts WHERE [id]=@BlogID AND IsDeleted=0;


IF(@_mobileNumber != N'') --the mobile number has already exsited and verified
BEGIN
	SET @MobileNumber = @_mobileNumber;
	SET @IsExisted = 1;
	SET @HasAlreadyVerified = 1;
	RETURN;
END

ELSE
BEGIN
	SELECT @_mobileNumber=MobileNumber,@_verificationCode=VerificationCode FROM MobileVerification WHERE BlogID=@BlogID;
	IF (@@ROWCOUNT = 0)
	BEGIN
		--INSERT INTO MobileVerification (BlogID,MobileNumber,VerificationCode) VALUES(@BlogID,@MobileNumber,@VerificationCode);
		SET @IsExisted = 0;
		SET @HasAlreadyVerified = 0;
		RETURN;
	END
	ELSE
	BEGIN
		SET @MobileNumber = @_mobileNumber;
		SET @VerificationCode = @_verificationCode
		SET @IsExisted = 1;
		SET @HasAlreadyVerified = 0;
		RETURN;		
	END	
END

RETURN;
