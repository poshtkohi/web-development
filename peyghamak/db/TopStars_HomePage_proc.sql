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
	   WHERE  name = 'TopStars_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE TopStars_HomePage_proc
GO

CREATE PROCEDURE TopStars_HomePage_proc

@TopStars INT,
@IsAccountsDbLinkedServer BIT

AS

SET ROWCOUNT @TopStars;

IF (@IsAccountsDbLinkedServer=1)
BEGIN
	SELECT ac.ImageGuid,ac.username,ac.[name],po.[id],po.PostContent ,po.PostAlign FROM [peyghamak-posts-1].dbo.posts AS po 
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID and ac.IsDeleted=0 
		WHERE po.StarredNum <= (SELECT MAX(StarredNum) FROM [peyghamak-posts-1].dbo.posts WHERE IsDeleted=0) AND po.IsDeleted=0 ORDER BY po.StarredNum DESC;
END

ELSE
BEGIN
	SELECT ac.ImageGuid,ac.username,ac.[name],po.[id],po.PostContent ,po.PostAlign FROM [peyghamak-posts-1].dbo.posts AS po 
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=po.BlogID and ac.IsDeleted=0 
		WHERE po.StarredNum <= (SELECT MAX(StarredNum) FROM [peyghamak-posts-1].dbo.posts WHERE IsDeleted=0) AND po.IsDeleted=0 ORDER BY po.StarredNum DESC;
END

SET ROWCOUNT 0;

RETURN ;