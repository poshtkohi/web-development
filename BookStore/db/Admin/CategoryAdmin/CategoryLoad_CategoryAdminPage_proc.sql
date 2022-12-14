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
	   WHERE  name = 'CategoryLoad_CategoryAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryLoad_CategoryAdminPage_proc
GO

CREATE PROCEDURE CategoryLoad_CategoryAdminPage_proc

@CategoryID BIGINT

AS

SELECT EnglishCategory,PersianCategory FROM BookCategory WHERE [id]=@CategoryID AND IsDeleted=0;

RETURN;