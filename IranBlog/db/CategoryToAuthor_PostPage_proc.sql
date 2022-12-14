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
	   WHERE  name = 'CategoryToAuthor_PostPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryToAuthor_PostPage_proc
GO

CREATE PROCEDURE CategoryToAuthor_PostPage_proc

@BlogID BIGINT,
@CategoryID INT,
@AuthorID BIGINT

AS

--BEGIN TRANSACTION


UPDATE usersInfo SET PostNum=PostNum+1 WHERE i=@BlogID
UPDATE SubjectedArchive SET PostNum=PostNum+1 WHERE [id]=@CategoryID AND BlogID=@BlogID
UPDATE TeamWeblog SET PostNum=PostNum+1 WHERE [id]=@AuthorID AND BlogID=@BlogID



UPDATE CategoryToAuthor SET PostNum=PostNum+1 WHERE BlogID=@BlogID AND CategoryID=@CategoryID AND AuthorID=@AuthorID

IF @@ROWCOUNT = 0
BEGIN
	INSERT INTO CategoryToAuthor (BlogID,CategoryID,AuthorID,PostNum) VALUES(@BlogID,@CategoryID,@AuthorID,1)
END



--COMMIT

RETURN