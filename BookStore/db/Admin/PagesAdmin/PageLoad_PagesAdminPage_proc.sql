--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]

GO

-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'PageLoad_PagesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageLoad_PagesAdminPage_proc
GO

CREATE PROCEDURE PageLoad_PagesAdminPage_proc

@PageID BIGINT

AS

SELECT PageTitle,PageContent,PageLanguage FROM pages WHERE [id]=@PageID AND IsDeleted=0;

RETURN;