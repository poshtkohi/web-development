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
	   WHERE  name = 'List_AjaxPostArchivePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE List_AjaxPostArchivePage_proc
GO

CREATE PROCEDURE List_AjaxPostArchivePage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@PostArchiveNum INT OUTPUT

AS

DECLARE @Ignore INT
DECLARE @LastID INT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM SubjectedArchive WHERE BlogID=@BlogID ORDER BY [id] DESC;
END
ELSE
BEGIN
	SELECT @PostArchiveNum=COUNT(*) FROM SubjectedArchive WHERE BlogID=@BlogID;

	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM SubjectedArchive WHERE BlogID=@BlogID ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END


SET ROWCOUNT @PageSize;

SELECT [id] AS PostArchiveID,subject AS PostArchiveTitle FROM SubjectedArchive WHERE [id]<@LastID AND BlogID=@BlogID ORDER BY [id] DESC;

SET ROWCOUNT 0;


RETURN