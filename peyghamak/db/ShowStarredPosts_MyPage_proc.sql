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
	   WHERE  name = 'ShowStarredPosts_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowStarredPosts_MyPage_proc
GO

CREATE PROCEDURE ShowStarredPosts_MyPage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@StarredPostNum INT OUTPUT,
@IsAccountsDbLinkedServer BIT,
@IsPosts1DbLinkedServer BIT

AS

DECLARE @Ignore INT;
DECLARE @LastID INT;

/*----------------------------------------------------------------------------------------------------------------*/

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [peyghamak-StarredPosts-1].dbo.starred WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	IF (@IsAccountsDbLinkedServer = 1)
	BEGIN
		SELECT @StarredPostNum=StarredPostNum FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT @StarredPostNum=StarredPostNum FROM [peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
	END

	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [peyghamak-StarredPosts-1].dbo.starred WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

SET ROWCOUNT @PageSize;


IF (@IsAccountsDbLinkedServer=0 AND @IsPosts1DbLinkedServer=1)
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,st.[id],po.PostDate,po.PostType,po.PostAlign,po.PostLanguage,po.PostContent,po.CommentEnabled,po.NumComments,po.CommentVerified,po.HasPicture,po.[id] AS PostID 
		FROM [peyghamak-StarredPosts-1].dbo.starred AS st INNER JOIN POSTS1DB.[peyghamak-posts-1].dbo.posts AS po 
		ON st.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID
		WHERE st.BlogID=@BlogID AND st.[id]<@LastID AND st.IsDeleted=0 ORDER BY st.[id] DESC;
END

IF (@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=1)
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,st.[id],po.PostDate,po.PostType,po.PostAlign,po.PostLanguage,po.PostContent,po.CommentEnabled,po.NumComments,po.CommentVerified,po.HasPicture,po.[id] AS PostID 
		FROM [peyghamak-StarredPosts-1].dbo.starred AS st INNER JOIN POSTS1DB.[peyghamak-posts-1].dbo.posts AS po 
		ON st.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID
		WHERE st.BlogID=@BlogID AND st.[id]<@LastID AND st.IsDeleted=0 ORDER BY st.[id] DESC;
END

IF (@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=0)
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,st.[id],po.PostDate,po.PostType,po.PostAlign,po.PostLanguage,po.PostContent,po.CommentEnabled,po.NumComments,po.CommentVerified,po.HasPicture,po.[id] AS PostID 
		FROM [peyghamak-StarredPosts-1].dbo.starred AS st INNER JOIN [peyghamak-posts-1].dbo.posts AS po 
		ON st.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID
		WHERE st.BlogID=@BlogID AND st.[id]<@LastID AND st.IsDeleted=0 ORDER BY st.[id] DESC;
END

ELSE
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,st.[id],po.PostDate,po.PostType,po.PostAlign,po.PostLanguage,po.PostContent,po.CommentEnabled,po.NumComments,po.CommentVerified,po.HasPicture,po.[id] AS PostID 
		FROM [peyghamak-StarredPosts-1].dbo.starred AS st INNER JOIN [peyghamak-posts-1].dbo.posts AS po 
		ON st.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID
		WHERE st.BlogID=@BlogID AND st.[id]<@LastID AND st.IsDeleted=0 ORDER BY st.[id] DESC;
END

SET ROWCOUNT 0;

RETURN;

/*----------------------------------------------------------------------------------------------------------------*/