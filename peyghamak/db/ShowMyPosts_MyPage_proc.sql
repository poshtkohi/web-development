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
	   WHERE  name = 'ShowMyPosts_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowMyPosts_MyPage_proc
GO

CREATE PROCEDURE ShowMyPosts_MyPage_proc

@BlogID BIGINT,
@MyBlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@IsAccountsDbLinkedServer BIT,
@IsStarredPosts1DbLinkedServer BIT,
@PostNum INT OUTPUT

AS

IF (@IsAccountsDbLinkedServer=1)
BEGIN
	SELECT @PostNum=PostNum FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
END
ELSE
BEGIN
	SELECT @PostNum=PostNum FROM [peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
END


DECLARE @Ignore INT
DECLARE @LastID INT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [peyghamak-posts-1].dbo.posts WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [peyghamak-posts-1].dbo.posts WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

SET ROWCOUNT @PageSize;

IF (@IsStarredPosts1DbLinkedServer=1)
BEGIN
	SELECT [id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred as st WHERE st.BlogID=@MyBlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@MyBlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID FROM [peyghamak-posts-1].dbo.posts AS po
		 WHERE BlogID=@BlogID AND [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	SELECT [id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred as st WHERE st.BlogID=@MyBlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@MyBlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID  FROM [peyghamak-posts-1].dbo.posts AS po
		 WHERE BlogID=@BlogID AND [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
END

SET ROWCOUNT 0;


RETURN 
