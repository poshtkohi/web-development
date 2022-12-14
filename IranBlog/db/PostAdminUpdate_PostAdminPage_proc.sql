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
	   WHERE  name = 'PostAdminUpdate_PostAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PostAdminUpdate_PostAdminPage_proc
GO

CREATE PROCEDURE PostAdminUpdate_PostAdminPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@CategoryID BIGINT,
@AuthorID BIGINT,
@PostTitle NVARCHAR(200),
@PostContent NTEXT,
@ContinuedPostContent NTEXT,
@comment VARCHAR(50),
@IsInAdminMode BIT

AS

DECLARE @NumComments INT;
DECLARE @TempNumComments INT;
DECLARE @IsShowCommentsPreVerify BIT;

SET @NumComments = 0;
SET @IsShowCommentsPreVerify = 1;

--SELECT @TempNumComments=NumComments,@NumComments=NumComments FROM dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID AND AuthorID=@AuthorID IsDeleted=0;
SELECT @TempNumComments=NumComments,@NumComments=NumComments FROM dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;

IF(@comment = 'disabled')
BEGIN
	SET @NumComments = -1;
END
IF(@comment = 'PreverifyActivate')
BEGIN
	SET @IsShowCommentsPreVerify = 0;
END

	IF(@IsInAdminMode = 1)
	BEGIN
		UPDATE dbo.posts SET subject=@PostTitle,content=@PostContent,continued=@ContinuedPostContent,NumComments=@NumComments,TempNumComments=@TempNumComments,IsShowCommentsPreVerify=@IsShowCommentsPreVerify 
				WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		UPDATE dbo.posts SET subject=@PostTitle,content=@PostContent,continued=@ContinuedPostContent,NumComments=@NumComments,TempNumComments=@TempNumComments,IsShowCommentsPreVerify=@IsShowCommentsPreVerify 
				WHERE [id]=@PostID AND BlogID=@BlogID AND AuthorID=@AuthorID AND IsDeleted=0;
	END



RETURN;