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
	   WHERE  name = 'CategoryToAuthor_LastPostEditPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryToAuthor_LastPostEditPage_proc
GO

CREATE PROCEDURE CategoryToAuthor_LastPostEditPage_proc

@BlogID BIGINT,
@CategoryID BIGINT,
@AuthorID BIGINT

AS

--BEGIN TRANSACTION


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

UPDATE usersInfo SET PostNum=PostNum-1 WHERE i=@BlogID
UPDATE SubjectedArchive SET PostNum=PostNum-1 WHERE [id]=@CategoryID AND BlogID=@BlogID
UPDATE TeamWeblog SET PostNum=PostNum-1 WHERE [id]=@AuthorID AND BlogID=@BlogID
UPDATE CategoryToAuthor SET PostNum=PostNum-1 WHERE BlogID=@BlogID AND CategoryID=@CategoryID AND AuthorID=@AuthorID


--COMMIT

RETURN