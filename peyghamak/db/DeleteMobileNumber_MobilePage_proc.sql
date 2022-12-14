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
	   WHERE  name = 'DeleteMobileNumber_MobilePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DeleteMobileNumber_MobilePage_proc
GO

CREATE PROCEDURE DeleteMobileNumber_MobilePage_proc

@BlogID BIGINT

AS


BEGIN TRANSACTION

UPDATE accounts SET mobile='' WHERE [id]=@BlogID AND IsDeleted=0;
DELETE FROM MobileVerification WHERE BlogID=@BlogID;

COMMIT

RETURN;
