--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-posts-1]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'PostDelete_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PostDelete_MyPage_proc
GO

CREATE PROCEDURE PostDelete_MyPage_proc

@PostID BIGINT,
@BlogID BIGINT,
@IsAccountsDbLinkedServer BIT,
@IsStarredPosts1DbLinkedServer BIT

AS

------------------Delete Starred Posts-------------
IF (@IsAccountsDbLinkedServer=0 AND @IsStarredPosts1DbLinkedServer=1)
BEGIN
	UPDATE ac SET ac.StarredPostNum=ac.StarredPostNum-1
			FROM [peyghamak-accounts].dbo.accounts AS ac,STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st
				WHERE st.PostID=@PostID AND ac.[id]=st.[BlogID] AND st.IsDeleted=0 AND ac.IsDeleted=0;
	
	IF (@@ROWCOUNT = 1)
	BEGIN
		UPDATE STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred SET IsDeleted=1 WHERE PostID=@PostID AND IsDeleted=0;
	END
END

IF (@IsAccountsDbLinkedServer=1 AND @IsStarredPosts1DbLinkedServer=1)
BEGIN
	UPDATE ac SET ac.StarredPostNum=ac.StarredPostNum-1
			FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac,STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st
				WHERE st.PostID=@PostID AND ac.[id]=st.[BlogID] AND st.IsDeleted=0 AND ac.IsDeleted=0;
	
	IF (@@ROWCOUNT = 1)
	BEGIN
		UPDATE STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred SET IsDeleted=1 WHERE PostID=@PostID AND IsDeleted=0;
	END
END

IF (@IsAccountsDbLinkedServer=1 AND @IsStarredPosts1DbLinkedServer=0)
BEGIN
	UPDATE ac SET ac.StarredPostNum=ac.StarredPostNum-1
			FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac,[peyghamak-StarredPosts-1].dbo.starred AS st
				WHERE st.PostID=@PostID AND ac.[id]=st.[BlogID] AND st.IsDeleted=0 AND ac.IsDeleted=0;
	
	IF (@@ROWCOUNT = 1)
	BEGIN
		UPDATE [peyghamak-StarredPosts-1].dbo.starred SET IsDeleted=1 WHERE PostID=@PostID AND IsDeleted=0;
	END
END

ELSE
BEGIN
	UPDATE ac SET ac.StarredPostNum=ac.StarredPostNum-1
			FROM [peyghamak-accounts].dbo.accounts AS ac,[peyghamak-StarredPosts-1].dbo.starred AS st
				WHERE st.PostID=@PostID AND ac.[id]=st.[BlogID] AND st.IsDeleted=0 AND ac.IsDeleted=0;
	
	IF (@@ROWCOUNT = 1)
	BEGIN
		UPDATE [peyghamak-StarredPosts-1].dbo.starred SET IsDeleted=1 WHERE PostID=@PostID AND IsDeleted=0;
	END
END
-----------------------------------------------



DECLARE @CommentNum INT;

SELECT @CommentNum=NumComments FROM posts WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
UPDATE posts SET IsDeleted=1 WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;

IF(@@ROWCOUNT = 1)
BEGIN
	IF @IsAccountsDbLinkedServer=1
	BEGIN
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET PostNum=PostNum-1,CommentNum=CommentNum-@CommentNum WHERE [id]=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		UPDATE [peyghamak-accounts].dbo.accounts SET PostNum=PostNum-1,CommentNum=CommentNum-@CommentNum WHERE [id]=@BlogID AND IsDeleted=0;
	END
END

RETURN 
