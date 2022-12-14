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
	   WHERE  name = 'LoadProfile_AccountPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE LoadProfile_AccountPage_proc
GO

CREATE PROCEDURE LoadProfile_AccountPage_proc

@UserID BIGINT

AS

SELECT TOP 1 [name],email,BirthYear,sex,CountryKey,ProvinceKey,address,PostalCode,tel FROM  accounts WHERE [id]=@UserID AND IsDeleted=0;

RETURN;