--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-comments]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'PostComment_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PostComment_CommentsPage_proc
GO

CREATE PROCEDURE PostComment_CommentsPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@CommentDate DATETIME,
@CommenterName NVARCHAR(50),
@CommenterUrl NVARCHAR(100),
@CommenterEmail VARCHAR(50),
@CommentContent NVARCHAR(2048),
@IsPrivateComment BIT /*,
@IsWeblogsDbLinkedServer BIT,
@IsAccountsDbLinkedServer BIT*/

AS


--BEGIN TRANSACTION

INSERT INTO comments (BlogID,PostID,CommentDate,CommenterName,CommenterUrl,CommenterEmail,CommentContent,IsPrivateComment) VALUES(@BlogID,@PostID,@CommentDate,@CommenterName,@CommenterUrl,@CommenterEmail,@CommentContent,@IsPrivateComment);

UPDATE weblogs.dbo.posts SET NumComments=NumComments+1 WHERE [id]=@PostID AND BlogID=@BlogID AND NumComments>=0;
IF (@@ROWCOUNT=1)
BEGIN
	UPDATE general.dbo.usersInfo SET CommentNum=CommentNum+1 WHERE i=@BlogID;
END

--Linked server settings for future system developments
/*IF @IsWeblogsDbLinkedServer=1
BEGIN
	UPDATE WEBLOGSDB.weblogs.dbo.posts SET NumComments=NumComments+1 WHERE [id]=@PostID AND BlogID=@BlogID;
END
ELSE
BEGIN
	UPDATE weblogs.dbo.posts SET NumComments=NumComments+1 WHERE [id]=@PostID AND BlogID=@BlogID;
END


IF (@@ROWCOUNT=1)
	BEGIN
	IF @IsAccountsDbLinkedServer=1
	BEGIN
		UPDATE ACCOUNTSDB.general.dbo.usersInfo SET CommentNum=CommentNum+1 WHERE i=@BlogID;
	END
	ELSE
	BEGIN
		UPDATE general.dbo.usersInfo SET CommentNum=CommentNum+1 WHERE i=@BlogID;
	END
END*/

--COMMIT

RETURN