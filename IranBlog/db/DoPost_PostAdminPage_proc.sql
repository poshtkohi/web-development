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
	   WHERE  name = 'DoPost_PostAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DoPost_PostAdminPage_proc
GO

CREATE PROCEDURE DoPost_PostAdminPage_proc

@BlogID BIGINT,
@CategoryID BIGINT,
@AuthorID BIGINT,
@PostTitle NVARCHAR(200),
@PostContent NTEXT,
@ContinuedPostContent NTEXT,
@comment VARCHAR(50)


AS

DECLARE @NumComments INT;
DECLARE @IsShowCommentsPreVerify BIT;
DECLARE @_PostID BIGINT;

SET @NumComments = 0;
SET @IsShowCommentsPreVerify = 1;

IF(@comment = 'disabled')
BEGIN
	SET @NumComments = -1;
END
IF(@comment = 'PreverifyActivate')
BEGIN
	SET @IsShowCommentsPreVerify = 0;
END

INSERT INTO dbo.posts (BlogID,AuthorID,CategoryID,subject,content,continued,date,NumComments,IsShowCommentsPreVerify) 
		VALUES(@BlogID,@AuthorID,@CategoryID,@PostTitle,@PostContent,@ContinuedPostContent,GETDATE(),@NumComments,@IsShowCommentsPreVerify);
SET @_PostID = @@IDENTITY;
EXECUTE general.dbo.CategoryToAuthor_PostPage_proc @BlogID , @CategoryID , @AuthorID ;

DELETE FROM dbo.updates WHERE BlogID=@BlogID;
INSERT INTO dbo.updates (BlogID,PostID) VALUES(@BlogID,@_PostID);

RETURN;