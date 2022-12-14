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
	   WHERE  name = 'PageUpdate_PagesPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageUpdate_PagesPage_proc
GO

CREATE PROCEDURE PageUpdate_PagesPage_proc

@BlogID BIGINT,
@PageID BIGINT,
@title NVARCHAR(200),
@ThemeContent NTEXT,
@PostContent NTEXT

AS

	UPDATE PageThemes SET ThemeContent=@ThemeContent WHERE [id]=@PageID AND BlogID=@BlogID AND IsDeleted=0;
	IF( @@ROWCOUNT > 0)
	BEGIN 
		UPDATE posts SET title=@title,PostContent=@PostContent WHERE PageThemeID=@PageID AND IsDeleted=0;
	END

RETURN 