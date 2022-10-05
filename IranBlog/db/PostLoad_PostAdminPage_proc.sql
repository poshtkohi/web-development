--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [weblogs]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'PostLoad_PostAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PostLoad_PostAdminPage_proc
GO

CREATE PROCEDURE PostLoad_PostAdminPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@AuthorID BIGINT,
@IsInAdminMode BIT

AS

IF(@IsInAdminMode = 1)
BEGIN
	SELECT subject AS PostTitle,CategoryID,content AS PostContent,continued AS ContinuedPostContent,NumComments AS comment,IsShowCommentsPreVerify FROM dbo.posts 
		WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
END
ELSE
BEGIN
	SELECT subject AS PostTitle,CategoryID,content AS PostContent,continued AS ContinuedPostContent,NumComments AS comment,IsShowCommentsPreVerify FROM dbo.posts 
		WHERE [id]=@PostID AND BlogID=@BlogID AND AuthorID=@AuthorID AND IsDeleted=0;
END

RETURN;