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
	   WHERE  name = 'LoadProfile_AccountPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE LoadProfile_AccountPage_proc
GO

CREATE PROCEDURE LoadProfile_AccountPage_proc

@BlogID BIGINT

AS

SELECT TOP 1 [name],email,BirthYear,BirthMonth,BirthDay,sex,CountryKey,ProvinceKey,city,about,url,ShowAgeEnabled,ShowCityEnabled FROM  accounts WHERE [id]=@BlogID AND IsDeleted=0;

RETURN;