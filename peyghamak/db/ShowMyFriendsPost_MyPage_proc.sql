--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-posts-1]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowMyFriendsPost_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowMyFriendsPost_MyPage_proc
GO

CREATE PROCEDURE ShowMyFriendsPost_MyPage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@IsAccountsDbLinkedServer BIT,
@IsFriends1DbLinkedServer BIT,
@IsStarredPosts1DbLinkedServer BIT

AS

DECLARE @Ignore INT;
DECLARE @LastID INT;

/*----------------------------------------------------------------------------------------------------------------*/
IF (@IsFriends1DbLinkedServer=0 AND @IsAccountsDbLinkedServer=0)
BEGIN
	IF (@PageNumber > 1)
	BEGIN 
		SET @Ignore = @PageSize * (@PageNumber - 1);
		SET ROWCOUNT @Ignore;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SET ROWCOUNT 1;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
		SET @LastID=@LastID+1;
	END
	
	SET ROWCOUNT @PageSize;
	
	IF (@IsStarredPosts1DbLinkedServer=1)
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred ,(SELECT st.[id] FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END	

	SET ROWCOUNT 0;
	
	RETURN;
END
/*----------------------------------------------------------------------------------------------------------------*/
IF (@IsFriends1DbLinkedServer=1 AND @IsAccountsDbLinkedServer=0)
BEGIN
	IF (@PageNumber > 1)
	BEGIN 
		SET @Ignore = @PageSize * (@PageNumber - 1);
		SET ROWCOUNT @Ignore;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SET ROWCOUNT 1;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
		SET @LastID=@LastID+1;
	END
	
	SET ROWCOUNT @PageSize;
	
	IF (@IsStarredPosts1DbLinkedServer=1)
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END	

	SET ROWCOUNT 0;
	
	RETURN;
END
/*----------------------------------------------------------------------------------------------------------------*/
IF (@IsFriends1DbLinkedServer=0 AND @IsAccountsDbLinkedServer=1)
BEGIN
	IF (@PageNumber > 1)
	BEGIN 
		SET @Ignore = @PageSize * (@PageNumber - 1);
		SET ROWCOUNT @Ignore;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SET ROWCOUNT 1;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
		SET @LastID=@LastID+1;
	END
	
	SET ROWCOUNT @PageSize;
	
	IF (@IsStarredPosts1DbLinkedServer=1)
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN [peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END	

	SET ROWCOUNT 0;
	
	RETURN;
END
/*----------------------------------------------------------------------------------------------------------------*/
IF (@IsFriends1DbLinkedServer=1 AND @IsAccountsDbLinkedServer=1)
BEGIN
	IF (@PageNumber > 1)
	BEGIN 
		SET @Ignore = @PageSize * (@PageNumber - 1);
		SET ROWCOUNT @Ignore;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SET ROWCOUNT 1;
		SELECT @LastID=po.[id] FROM posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 
			AND fr.IsDeleted=0 WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;
		SET @LastID=@LastID+1;
	END
	
	SET ROWCOUNT @PageSize;
	
	IF (@IsStarredPosts1DbLinkedServer=1)
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM STARREDPOSTS1DB.[peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END
	ELSE
	BEGIN
		SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture,(SELECT COUNT(*) FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS Starred,(SELECT st.[id] FROM [peyghamak-StarredPosts-1].dbo.starred AS st WHERE st.BlogID=@BlogID AND st.PostID=po.[id] AND st.IsDeleted=0) AS StarredID 
			FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr 
			ON fr.BlogID=@BlogID AND po.BlogID=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
	--		ON fr.BlogID=@BlogID AND ac.[id]=fr.FriendBlogID
			ON ac.[id]=fr.FriendBlogID
			WHERE po.[id]<@LastID AND po.IsDeleted=0 ORDER BY po.[id] DESC;
	END	

	SET ROWCOUNT 0;
	
	RETURN;
END
/*----------------------------------------------------------------------------------------------------------------*/