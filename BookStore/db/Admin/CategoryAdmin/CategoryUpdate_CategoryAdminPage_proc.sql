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
	   WHERE  name = 'CategoryUpdate_CategoryAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryUpdate_CategoryAdminPage_proc
GO

CREATE PROCEDURE CategoryUpdate_CategoryAdminPage_proc

@CategoryID BIGINT,
@EnglishCategory NVARCHAR(400),
@PersianCategory NVARCHAR(400)

AS

UPDATE BookCategory SET EnglishCategory=@EnglishCategory,PersianCategory=@PersianCategory WHERE [id]=@CategoryID AND IsDeleted=0;

RETURN;
