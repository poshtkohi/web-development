--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-pages]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowPages_PagesPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowPages_PagesPage_proc
GO

CREATE PROCEDURE ShowPages_PagesPage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@PagesNum INT OUTPUT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [iranblog-pages].dbo.PageThemes WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;

END
ELSE
BEGIN
	SET @PagesNum=0;
	SELECT @PagesNum=PagesNum FROM [general].dbo.usersInfo WHERE i=@BlogID;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [iranblog-pages].dbo.PageThemes WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;

	SELECT PageThemes.[id],[iranblog-pages].dbo.posts.title FROM [iranblog-pages].dbo.PageThemes,[iranblog-pages].dbo.posts
		 WHERE BlogID=@BlogID AND PageThemeID=PageThemes.[id] AND PageThemes.[id]<@LastID AND PageThemes.IsDeleted=0 ORDER BY PageThemes.[id] DESC;

	SET ROWCOUNT 0;
RETURN 