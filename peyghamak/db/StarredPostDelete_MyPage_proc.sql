--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-StarredPosts-1]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'StarredPostDelete_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE StarredPostDelete_MyPage_proc
GO

CREATE PROCEDURE StarredPostDelete_MyPage_proc

@StarredID BIGINT,
@BlogID BIGINT,
@IsAccountsDbLinkedServer BIT,
@IsPosts1DbLinkedServer BIT

AS


DECLARE @PostID BIGINT;

SELECT @PostID=PostID FROM [peyghamak-StarredPosts-1].dbo.starred WHERE [id]=@StarredID AND BlogID=@BlogID AND IsDeleted=0;
UPDATE [peyghamak-StarredPosts-1].dbo.starred SET IsDeleted=1 WHERE [id]=@StarredID AND BlogID=@BlogID AND IsDeleted=0;

IF (@@ROWCOUNT = 1)
BEGIN
	IF(@IsAccountsDbLinkedServer=0 AND @IsPosts1DbLinkedServer=1)
	BEGIN
		UPDATE [peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
		UPDATE POSTS1DB.[peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum-1 WHERE [id]=@PostID AND IsDeleted=0;
	END
	
	IF(@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=1)
	BEGIN
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
		UPDATE POSTS1DB.[peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum-1 WHERE [id]=@PostID AND IsDeleted=0;
	END
	
	IF(@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=0)
	BEGIN
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
		UPDATE [peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum-1 WHERE [id]=@PostID AND IsDeleted=0;
	END
	
	ELSE
	BEGIN
		UPDATE [peyghamak-accounts].dbo.accounts SET StarredPostNum=StarredPostNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
		UPDATE [peyghamak-posts-1].dbo.posts SET StarredNum=StarredNum-1 WHERE [id]=@PostID AND IsDeleted=0;
	END
END

RETURN 
