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
	   WHERE  name = 'ListPosts_PostAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ListPosts_PostAdminPage_proc
GO

CREATE PROCEDURE ListPosts_PostAdminPage_proc

@BlogID BIGINT,
@AuthorID BIGINT,
@PageSize INT,
@PageNumber INT,
@IsInAdminMode BIT,
@PostNum INT OUTPUT

AS

DECLARE @Ignore INT
DECLARE @LastID INT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	IF (@IsInAdminMode = 1)
	BEGIN
		SELECT @LastID=[id] FROM weblogs.dbo.posts WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	END
	ELSE
	BEGIN
		SELECT @LastID=[id] FROM weblogs.dbo.posts WHERE BlogID=@BlogID AND AuthorID=@AuthorID AND IsDeleted=0 ORDER BY [id] DESC;
	END
END
ELSE
BEGIN
	IF (@IsInAdminMode = 1)
	BEGIN
		SELECT @PostNum=PostNum FROM general.dbo.usersInfo WHERE i=@BlogID;--IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT @PostNum=PostNum FROM general.dbo.TeamWeblog WHERE [id]=@AuthorID AND BlogID=@BlogID;--IsDeleted=0;
	END
	SET ROWCOUNT 1;
	IF (@IsInAdminMode = 1)
	BEGIN
		SELECT @LastID=[id] FROM weblogs.dbo.posts WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	END
	ELSE
	BEGIN
		SELECT @LastID=[id] FROM weblogs.dbo.posts WHERE BlogID=@BlogID AND AuthorID=@AuthorID AND IsDeleted=0 ORDER BY [id] DESC;
	END
	SET @LastID=@LastID+1;
END


SET ROWCOUNT @PageSize;

IF (@IsInAdminMode = 1)
BEGIN
	SELECT [id] AS PostID,subject,[date] FROM weblogs.dbo.posts WHERE BlogID=@BlogID AND [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	SELECT [id] AS PostID,subject,[date] FROM weblogs.dbo.posts WHERE BlogID=@BlogID AND AuthorID=@AuthorID AND [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
END

SET ROWCOUNT 0;


RETURN