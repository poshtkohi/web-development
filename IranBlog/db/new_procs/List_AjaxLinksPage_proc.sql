--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [general]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'List_AjaxLinksPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE List_AjaxLinksPage_proc
GO

CREATE PROCEDURE List_AjaxLinksPage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@LinkNum INT OUTPUT,
@IsAjaxLinks BIT

AS

DECLARE @Ignore INT
DECLARE @LastID INT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	IF(@IsAjaxLinks=1)
	BEGIN
		SELECT @LastID=i FROM links WHERE BlogID=@BlogID ORDER BY i DESC;
	END
	ELSE
	BEGIN
		SELECT @LastID=i FROM linkss WHERE BlogID=@BlogID ORDER BY i DESC;
	END
END
ELSE
BEGIN
	IF(@IsAjaxLinks=1)
	BEGIN
		SELECT @LinkNum=COUNT(*) FROM links WHERE BlogID=@BlogID;
	END
	ELSE
	BEGIN
		SELECT @LinkNum=COUNT(*) FROM linkss WHERE BlogID=@BlogID;
	END

	SET ROWCOUNT 1;
	IF(@IsAjaxLinks=1)
	BEGIN
		SELECT @LastID=i FROM links WHERE BlogID=@BlogID ORDER BY i DESC;
	END
	ELSE
	BEGIN
		SELECT @LastID=i FROM linkss WHERE BlogID=@BlogID ORDER BY i DESC;
	END
	SET @LastID=@LastID+1;
END


SET ROWCOUNT @PageSize;


IF(@IsAjaxLinks=1)
BEGIN
	SELECT i AS LinkID,title AS LinkTitle,url AS LinkAddress FROM links WHERE i<@LastID AND BlogID=@BlogID ORDER BY i DESC;
END
ELSE
BEGIN
	SELECT i AS LinkID,title AS LinkTitle,url AS LinkAddress FROM linkss WHERE i<@LastID AND BlogID=@BlogID ORDER BY i DESC;
END

SET ROWCOUNT 0;


RETURN