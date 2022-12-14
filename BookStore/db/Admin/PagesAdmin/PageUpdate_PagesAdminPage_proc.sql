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
	   WHERE  name = 'PageUpdate_PagesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageUpdate_PagesAdminPage_proc
GO

CREATE PROCEDURE PageUpdate_PagesAdminPage_proc

@PageID BIGINT,
@PageTitle NVARCHAR(400),
@PageContent NTEXT,
@PageLanguage INT

AS

UPDATE pages SET PageTitle=@PageTitle,PageContent=@PageContent,PageLanguage=@PageLanguage WHERE [id]=@PageID AND IsDeleted=0;

RETURN;
