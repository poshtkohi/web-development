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
	   WHERE  name = 'ShowLatestPosts_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowLatestPosts_HomePage_proc
GO

CREATE PROCEDURE ShowLatestPosts_HomePage_proc

@TopLatestPostsShow INT,
@IsAccountsDbLinkedServer BIT

AS

IF (@IsAccountsDbLinkedServer=1)
BEGIN
	SET ROWCOUNT @TopLatestPostsShow;

	SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture 
		FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID
		WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;

	SET ROWCOUNT 0;	
	RETURN;
END
ELSE
BEGIN
	SET ROWCOUNT @TopLatestPostsShow;

	SELECT ac.username,ac.[name],ac.ImageGuid,po.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture 
		FROM [peyghamak-posts-1].dbo.posts AS po INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID
		WHERE po.IsDeleted=0 ORDER BY po.[id] DESC;

	SET ROWCOUNT 0;	
	RETURN;
END