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
	   WHERE  name = 'CommentDelete_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CommentDelete_CommentsPage_proc
GO

CREATE PROCEDURE CommentDelete_CommentsPage_proc

@CommentID BIGINT,
@PostID BIGINT,
@BlogID BIGINT,
@IsPosts1DbLinkedServer BIT,
@IsAccountsDbLinkedServer BIT

AS

--BEGIN TRANSACTION

DECLARE @CommentNum INT

UPDATE [peyghamak-comments-1].dbo.comments SET IsDeleted=1 WHERE [id]=@CommentID AND BlogID=@BlogID;

IF @@ROWCOUNT = 1
BEGIN
	IF @IsAccountsDbLinkedServer=1
	BEGIN
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET CommentNum=CommentNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		UPDATE [peyghamak-accounts].dbo.accounts SET CommentNum=CommentNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
	END

	IF @IsPosts1DbLinkedServer=1
	BEGIN
		UPDATE POSTS1DB.[peyghamak-posts-1].dbo.posts SET NumComments=NumComments-1 WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		UPDATE [peyghamak-posts-1].dbo.posts SET NumComments=NumComments-1 WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
	END
END

--COMMIT

RETURN 