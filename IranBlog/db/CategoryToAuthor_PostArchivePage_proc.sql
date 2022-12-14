--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE general
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'CategoryToAuthor_PostArchivePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryToAuthor_PostArchivePage_proc
GO

CREATE PROCEDURE CategoryToAuthor_PostArchivePage_proc

@BlogID BIGINT,
@CategoryID BIGINT,
@PostNum INT

AS

--BEGIN TRANSACTION


IF @PostNum > 0
BEGIN
-- Temporary section--------------------- this section must be deleted in the next system developments
IF (SELECT COUNT(*) FROM CategoryToAuthor WHERE BlogID=@BlogID AND CategoryID=@CategoryID) = 0
BEGIN
	INSERT INTO CategoryToAuthor
	   SELECT @BlogID,@CategoryID,weblogs.dbo.posts.AuthorID,SUM(1)
	   FROM weblogs.dbo.posts
	   WHERE weblogs.dbo.posts.BlogID=@BlogID AND weblogs.dbo.posts.CategoryID=@CategoryID
	   GROUP BY weblogs.dbo.posts.AuthorID

END
----------------------------------------
UPDATE usersInfo SET PostNum=PostNum-@PostNum WHERE i=@BlogID

UPDATE TeamWeblog
	SET [TeamWeblog].[PostNum]=[TeamWeblog].[PostNum]-[CategoryToAuthor].[PostNum]
		FROM TeamWeblog,CategoryToAuthor
			WHERE [CategoryToAuthor].[AuthorID]=[TeamWeblog].[id] AND [CategoryToAuthor].[CategoryID]=@CategoryID AND [CategoryToAuthor].[BlogID]=@BlogID

DELETE FROM CategoryToAuthor WHERE CategoryID=@CategoryID AND BlogID=@BlogID
END

DELETE FROM SubjectedArchive WHERE [id]=@CategoryID AND BlogID=@BlogID



--COMMIT

RETURN