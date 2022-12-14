--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-comments-1]
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
@PostDate DATETIME,
@CommenterBlogID BIGINT,
@PostType CHAR,
@PostLanguage CHAR,
@PostAlign BIT,
@PostContent NVARCHAR(500),
@HasPicture BIT,
@IsPosts1DbLinkedServer BIT,
@IsAccountsDbLinkedServer BIT

AS


--BEGIN TRANSACTION

INSERT INTO comments (BlogID,PostID,CommenterBlogID,PostDate,PostAlign,PostLanguage,PostType,PostContent,HasPicture) VALUES(@BlogID,@PostID,@CommenterBlogID,@PostDate,@PostAlign,@PostLanguage,@PostType,@PostContent,@HasPicture);


IF @IsPosts1DbLinkedServer=1
BEGIN
	UPDATE POSTS1DB.[peyghamak-posts-1].dbo.posts SET NumComments=NumComments+1 WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
END
ELSE
BEGIN
	UPDATE [peyghamak-posts-1].dbo.posts SET NumComments=NumComments+1 WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
END



IF @IsAccountsDbLinkedServer=1
BEGIN
	UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET CommentNum=CommentNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
END
ELSE
BEGIN
	UPDATE [peyghamak-accounts].dbo.accounts SET CommentNum=CommentNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
END

--COMMIT

RETURN
