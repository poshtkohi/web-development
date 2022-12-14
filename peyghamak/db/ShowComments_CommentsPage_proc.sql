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
	   WHERE  name = 'ShowComments_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowComments_CommentsPage_proc
GO

CREATE PROCEDURE ShowComments_CommentsPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@PageSize INT,
@PageNumber INT,
@IsPosts1DbLinkedServer BIT,
@IsAccountsDbLinkedServer BIT,
@CommentNum INT OUTPUT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF @PageNumber > 1
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [peyghamak-comments-1].dbo.comments WHERE PostID=@PostID AND BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;

END
ELSE
BEGIN
	IF @IsPosts1DbLinkedServer=1
	BEGIN
		SELECT @CommentNum=NumComments FROM POSTS1DB.[peyghamak-posts-1].dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT @CommentNum=NumComments FROM [peyghamak-posts-1].dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
	END
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [peyghamak-comments-1].dbo.comments WHERE PostID=@PostID AND BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

SET ROWCOUNT @PageSize;

IF @IsAccountsDbLinkedServer=1
BEGIN
	SELECT  ac.username,ac.[name],ac.ImageGuid,co.[id],PostDate,PostAlign,PostLanguage,PostType,PostContent,HasPicture FROM [peyghamak-comments-1].dbo.comments AS co
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		on co.PostID=@PostID AND co.BlogID=@BlogID AND ac.[id]=co.CommenterBlogID AND ac.IsDeleted=0
		WHERE co.[id]<@LastID AND co.IsDeleted=0 ORDER BY co.[id] DESC;
END
ELSE
BEGIN
	SELECT  ac.username,ac.[name],ac.ImageGuid,co.[id],PostDate,PostAlign,PostLanguage,PostType,PostContent,HasPicture FROM [peyghamak-comments-1].dbo.comments AS co
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		on co.PostID=@PostID AND co.BlogID=@BlogID AND ac.[id]=co.CommenterBlogID AND ac.IsDeleted=0
		WHERE co.[id]<@LastID AND co.IsDeleted=0 ORDER BY co.[id] DESC;
END
SET ROWCOUNT 0;


RETURN 
