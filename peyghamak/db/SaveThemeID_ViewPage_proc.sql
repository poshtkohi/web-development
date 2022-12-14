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
	   WHERE  name = 'SaveThemeID_ViewPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SaveThemeID_ViewPage_proc
GO

CREATE PROCEDURE SaveThemeID_ViewPage_proc

@BlogID BIGINT,
@ThemeID INT,
@ThemeString NVARCHAR(50) OUTPUT

AS

DECLARE @_ThemeString NVARCHAR(50);
 
SELECT @_ThemeString=ThemeString FROM themes WHERE [id]=@ThemeID;

IF(@@ROWCOUNT>0)
BEGIN
	UPDATE accounts SET ThemeID=@ThemeID WHERE [id]=@BlogID AND IsDeleted=0;
	SET @ThemeString = @_ThemeString;
END

ELSE
BEGIN
	SET @ThemeString = (SELECT ThemeString FROM themes WHERE [id]=1);
END

RETURN