--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE weblogs
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'SubjectedArchive_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SubjectedArchive_proc
GO

CREATE PROCEDURE SubjectedArchive_proc

@BlogID BIGINT,
@CategoryID BIGINT,
@PageSize INT,
@PageNumber INT

AS

DECLARE @Ignore int
DECLARE @LastID int

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM posts WHERE BlogID=@BlogID AND IsDeleted=0 AND CategoryID=@CategoryID ORDER BY [id] DESC;
END
ELSE
BEGIN
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM posts WHERE BlogID=@BlogID AND IsDeleted=0 AND CategoryID=@CategoryID ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

SET ROWCOUNT @PageSize ;
SELECT [id],[date],subject,content,NumComments,continued,CategoryID,AuthorID FROM posts WHERE BlogID=@BlogID AND [id]<@LastID AND IsDeleted=0 AND CategoryID=@CategoryID ORDER BY [id] DESC;
SET ROWCOUNT 0;

RETURN 
