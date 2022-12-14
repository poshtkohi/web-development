--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-StarredPosts-1]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'StaringPost_MyAndGuestPages_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE StaringPost_MyAndGuestPages_proc
GO

CREATE PROCEDURE StaringPost_MyAndGuestPages_proc

@BlogID BIGINT,
@PostID BIGINT,
@IsAccountsDbLinkedServer BIT,
@IsPosts1DbLinkedServer BIT

AS


IF (@IsAccountsDbLinkedServer=0 AND @IsPosts1DbLinkedServer=1)
BEGIN
	IF((SELECT COUNT(*) FROM POSTS1DB.[peyghamak-posts-1].dbo.posts WHERE [id]=@PostID AND IsDeleted=0) = 1 AND 
		(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred WHERE BlogID=@BlogID AND PostID=@PostID AND IsDeleted=0) = 0)
	BEGIN
		INSERT INTO [peyghamak-StarredPosts-1].dbo.starred (BlogID,PostID) VALUES(@BlogID,@PostID);
		UPDATE POSTS1DB.[peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum+1 WHERE [id]=@PostID AND IsDeleted=0;
		UPDATE [peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
END

IF (@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=1)
BEGIN
	IF((SELECT COUNT(*) FROM POSTS1DB.[peyghamak-posts-1].dbo.posts WHERE [id]=@PostID AND IsDeleted=0) = 1 AND 
		(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred WHERE BlogID=@BlogID AND PostID=@PostID AND IsDeleted=0) = 0)
	BEGIN
		INSERT INTO [peyghamak-StarredPosts-1].dbo.starred (BlogID,PostID) VALUES(@BlogID,@PostID);
		UPDATE POSTS1DB.[peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum+1 WHERE [id]=@PostID AND IsDeleted=0;
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
END

IF (@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=0)
BEGIN
	IF((SELECT COUNT(*) FROM [peyghamak-posts-1].dbo.posts WHERE [id]=@PostID AND IsDeleted=0) = 1 AND 
		(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred WHERE BlogID=@BlogID AND PostID=@PostID AND IsDeleted=0) = 0)
	BEGIN
		INSERT INTO [peyghamak-StarredPosts-1].dbo.starred (BlogID,PostID) VALUES(@BlogID,@PostID);
		UPDATE [peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum+1 WHERE [id]=@PostID AND IsDeleted=0;
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
END

ELSE
BEGIN
	IF((SELECT COUNT(*) FROM [peyghamak-posts-1].dbo.posts WHERE [id]=@PostID AND IsDeleted=0) = 1 AND 
		(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred WHERE BlogID=@BlogID AND PostID=@PostID AND IsDeleted=0) = 0)
	BEGIN
		INSERT INTO [peyghamak-StarredPosts-1].dbo.starred (BlogID,PostID) VALUES(@BlogID,@PostID);
		UPDATE [peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum+1 WHERE [id]=@PostID AND IsDeleted=0;
		UPDATE [peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
END

RETURN ;