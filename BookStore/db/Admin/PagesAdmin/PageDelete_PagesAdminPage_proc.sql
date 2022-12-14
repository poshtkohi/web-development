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
	   WHERE  name = 'PageDelete_PagesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageDelete_PagesAdminPage_proc
GO

CREATE PROCEDURE PageDelete_PagesAdminPage_proc

@PageID BIGINT

AS


UPDATE pages SET IsDeleted=1 WHERE [id]=@PageID;

RETURN