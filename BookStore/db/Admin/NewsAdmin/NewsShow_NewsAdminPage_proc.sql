--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]

GO

-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'NewsShow_NewsAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE NewsShow_NewsAdminPage_proc
GO

CREATE PROCEDURE NewsShow_NewsAdminPage_proc

@PageSize INT,
@PageNumber INT,
@NewsNum INT OUTPUT,
@NewsLanguage INT,
@IsTopNews BIT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	IF(@NewsLanguage < 0)
	BEGIN
		SELECT @LastID=[id] FROM news WHERE IsDeleted=0 ORDER BY [id] DESC;
	END
	ELSE
	BEGIN
		SELECT @LastID=[id] FROM news WHERE IsDeleted=0 AND IsTopNews=@IsTopNews AND NewsLanguage=@NewsLanguage ORDER BY [id] DESC;
	END

END
ELSE
BEGIN
	SET @NewsNum=0;
	IF(@NewsLanguage < 0)
	BEGIN
		SELECT @NewsNum=COUNT(*) FROM news WHERE IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT @NewsNum=COUNT(*) FROM news WHERE IsDeleted=0 AND IsTopNews=@IsTopNews AND NewsLanguage=@NewsLanguage;
	END
	SET ROWCOUNT 1;
	IF(@NewsLanguage < 0)
	BEGIN
		SELECT @LastID=[id] FROM news WHERE IsDeleted=0 ORDER BY [id] DESC;
	END
	ELSE
	BEGIN
		SELECT @LastID=[id] FROM news WHERE IsDeleted=0 AND IsTopNews=@IsTopNews AND NewsLanguage=@NewsLanguage ORDER BY [id] DESC;
	END
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT  [id] AS NewsID,[date],NewsTitle,NewsLanguage,IsTopNews FROM news WHERE [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
	SET ROWCOUNT 0;
RETURN 