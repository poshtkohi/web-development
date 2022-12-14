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
	   WHERE  name = 'AddMobileNumber_MobilePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE AddMobileNumber_MobilePage_proc
GO

CREATE PROCEDURE AddMobileNumber_MobilePage_proc

@BlogID BIGINT,
@MobileNumber NVARCHAR(20),
@VerificationCode VARCHAR(10),
@IsOwnedByAnotherUser BIT OUTPUT

AS


SET @IsOwnedByAnotherUser = 0;

IF ( (SELECT count(*) FROM accounts WHERE mobile=@MobileNumber AND IsDeleted=0) = 1)
BEGIN
        SET @IsOwnedByAnotherUser = 1;
	RETURN;
END

IF ( (SELECT count(*) FROM MobileVerification WHERE [id]=@BlogID AND MobileNumber=@MobileNumber AND VerificationCode=@VerificationCode) = 0)
BEGIN
	INSERT INTO MobileVerification (BlogID,MobileNumber,VerificationCode) VALUES(@BlogID,@MobileNumber,@VerificationCode);
	RETURN;
END


RETURN;
