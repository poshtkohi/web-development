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
	   WHERE  name = 'PageLoad_PagesPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageLoad_PagesPage_proc
GO

CREATE PROCEDURE PageLoad_PagesPage_proc

@BlogID BIGINT,
@PageID BIGINT

AS

	SELECT title,ThemeContent,PostContent FROM PageThemes,posts
		 WHERE PageThemes.[id]=@PageID AND BlogID=@BlogID AND PageThemeID=@PageID AND PageThemes.IsDeleted=0;
RETURN 