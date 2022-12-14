--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-accounts]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ListUsers_UsersPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ListUsers_UsersPage_proc
GO

CREATE PROCEDURE ListUsers_UsersPage_proc

@PageSize INT,
@PageNumber INT,
@UsersNum BIGINT OUTPUT

AS

DECLARE @Ignore INT
DECLARE @LastID INT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM accounts WHERE IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	SELECT @UsersNum=count(*) FROM accounts WHERE IsDeleted=0;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM accounts WHERE IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END


SET ROWCOUNT @PageSize;


SELECT accounts.[id],username,[name],ImageGuid,BirthYear,co.country,city,ShowAgeEnabled,ShowCityEnabled FROM accounts 
	INNER JOIN countries AS co
	on accounts.CountryKey=co.CountryKey
	WHERE accounts.[id]<@LastID AND IsDeleted=0 ORDER BY accounts.[id] DESC;

SET ROWCOUNT 0;


RETURN