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
	   WHERE  name = 'PageInsert_PagesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageInsert_PagesAdminPage_proc
GO

CREATE PROCEDURE PageInsert_PagesAdminPage_proc

@PageTitle NVARCHAR(400),
@PageContent NTEXT,
@PageLanguage INT

AS

INSERT INTO pages (PageTitle,PageContent,PageLanguage) VALUES(@PageTitle,@PageContent,@PageLanguage);

RETURN;
