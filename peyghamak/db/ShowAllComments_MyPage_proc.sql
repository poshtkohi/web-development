--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-comments-1]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowAllComments_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowAllComments_MyPage_proc
GO

CREATE PROCEDURE ShowAllComments_MyPage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@CommentNum BIGINT OUTPUT,
@IsAccountsDbLinkedServer BIT,
@IsPosts1DbLinkedServer BIT

AS

DECLARE @Ignore INT
DECLARE @LastID INT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [peyghamak-comments-1].dbo.comments WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	IF (@IsAccountsDbLinkedServer=1)
	BEGIN
		SELECT @CommentNum=CommentNum FROM [peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT @CommentNum=CommentNum FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
	END
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [peyghamak-comments-1].dbo.comments WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END


SET ROWCOUNT @PageSize;
IF (@IsAccountsDbLinkedServer=0 AND @IsPosts1DbLinkedServer=1)
BEGIN
	SELECT ac.ImageGuid,ac.username,ac.[name],co.[id],co.PostID,co.PostContent AS CommentContent,co.PostAlign AS CommentAlign,po.PostContent,po.PostAlign AS PostAlign FROM [peyghamak-comments-1].dbo.comments AS co INNER JOIN POSTS1DB.[peyghamak-posts-1].dbo.posts AS po 
		ON co.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=co.CommenterBlogID and ac.IsDeleted=0 
		WHERE co.BlogID=@BlogID AND co.[id]<@LastID AND co.IsDeleted=0 ORDER BY co.[id] DESC;
END

IF (@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=1)
BEGIN
	SELECT ac.ImageGuid,ac.username,ac.[name],co.[id],co.PostID,co.PostContent AS CommentContent,co.PostAlign AS CommentAlign,po.PostContent,po.PostAlign AS PostAlign FROM [peyghamak-comments-1].dbo.comments AS co INNER JOIN POSTS1DB.[peyghamak-posts-1].dbo.posts AS po 
		ON co.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=co.CommenterBlogID and ac.IsDeleted=0 
		WHERE co.BlogID=@BlogID AND co.[id]<@LastID AND co.IsDeleted=0 ORDER BY co.[id] DESC;
END

IF (@IsAccountsDbLinkedServer=1 AND @IsPosts1DbLinkedServer=0)
BEGIN
	SELECT ac.ImageGuid,ac.username,ac.[name],co.[id],co.PostID,co.PostContent AS CommentContent,co.PostAlign AS CommentAlign,po.PostContent,po.PostAlign AS PostAlign FROM [peyghamak-comments-1].dbo.comments AS co INNER JOIN [peyghamak-posts-1].dbo.posts AS po 
		ON co.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=co.CommenterBlogID and ac.IsDeleted=0 
		WHERE co.BlogID=@BlogID AND co.[id]<@LastID AND co.IsDeleted=0 ORDER BY co.[id] DESC;
END

ELSE
BEGIN
	SELECT ac.ImageGuid,ac.username,ac.[name],co.[id],co.PostID,co.PostContent AS CommentContent,co.PostAlign AS CommentAlign,po.PostContent,po.PostAlign AS PostAlign FROM [peyghamak-comments-1].dbo.comments AS co INNER JOIN [peyghamak-posts-1].dbo.posts AS po 
		ON co.PostID=po.[id] AND po.IsDeleted=0 
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=co.CommenterBlogID and ac.IsDeleted=0 
		WHERE co.BlogID=@BlogID AND co.[id]<@LastID AND co.IsDeleted=0 ORDER BY co.[id] DESC;
END

SET ROWCOUNT 0;


RETURN