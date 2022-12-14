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
	   WHERE  name = 'UserProfileInfo_GuestPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE UserProfileInfo_GuestPage_proc
GO

CREATE PROCEDURE UserProfileInfo_GuestPage_proc

@BlogID BIGINT

AS

SET ROWCOUNT 1;

/*SELECT [accounts].[name],[accounts].ImageGuid,[accounts].BirthYear,[accounts].about,[accounts].url,
	[accounts].[ShowAgeEnabled],[accounts].[ShowCityEnabled],[accounts].PostNum,[accounts].FriendNum,[accounts].FollowerNum,
	[countries].country,[provinces].province FROM [accounts],[countries],[provinces]
		WHERE [accounts].[id]=@BlogID AND [accounts].CountryKey=[countries].CountryKey AND
			[accounts].ProvinceKey=[provinces].ProvinceKey AND [accounts].IsDeleted=0;*/

SELECT [accounts].[name],[accounts].ImageGuid,[accounts].BirthYear,[accounts].about,[accounts].url,
	[accounts].[ShowAgeEnabled],[accounts].[ShowCityEnabled],[accounts].PostNum,[accounts].FriendNum,[accounts].FollowerNum,
	[countries].country,[accounts].city FROM [accounts],[countries],[provinces]
		WHERE [accounts].[id]=@BlogID AND [accounts].CountryKey=[countries].CountryKey AND
			 [accounts].IsDeleted=0;
SET ROWCOUNT 0;


RETURN 