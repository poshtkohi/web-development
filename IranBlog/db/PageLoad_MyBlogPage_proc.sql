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
	   WHERE  name = 'PageLoad_MyBlogPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageLoad_MyBlogPage_proc
GO

CREATE PROCEDURE PageLoad_MyBlogPage_proc

@subdomain VARCHAR(30),
@PageID BIGINT

AS

	SELECT posts.title,ThemeContent,PostContent,[date] FROM PageThemes,posts,[general].dbo.usersInfo AS ui
		 WHERE PageThemes.[id]=@PageID AND BlogID=ui.i AND ui.subdomain=@subdomain AND PageThemeID=@PageID AND PageThemes.IsDeleted=0;
RETURN 