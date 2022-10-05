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
	   WHERE  name = 'PostDelete_PostAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PostDelete_PostAdminPage_proc
GO

CREATE PROCEDURE PostDelete_PostAdminPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@AuthorID BIGINT,
@IsInAdminMode BIT

AS

DECLARE @_CategoryID BIGINT;
DECLARE @_MainAuthorID BIGINT;

	SELECT @_CategoryID=CategoryID,@_MainAuthorID=AuthorID FROM dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
	IF(@IsInAdminMode = 1)
	BEGIN
		UPDATE dbo.posts SET IsDeleted=1 WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		UPDATE dbo.posts SET IsDeleted=1 WHERE [id]=@PostID AND BlogID=@BlogID AND AuthorID=@AuthorID AND IsDeleted=0;
	END

	IF (@@ROWCOUNT > 0)
	BEGIN
		EXECUTE general.dbo.CategoryToAuthor_LastPostEditPage_proc @BlogID , @_CategoryID , @_MainAuthorID;
		DELETE FROM dbo.updates WHERE BlogID=@BlogID AND PostID=@PostID;
	END

RETURN;