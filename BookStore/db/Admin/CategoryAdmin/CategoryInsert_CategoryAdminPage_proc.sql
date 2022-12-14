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
	   WHERE  name = 'CategoryInsert_CategoryAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryInsert_CategoryAdminPage_proc
GO

CREATE PROCEDURE CategoryInsert_CategoryAdminPage_proc

@EnglishCategory NVARCHAR(400),
@PersianCategory NVARCHAR(400)

AS

INSERT INTO BookCategory (EnglishCategory,PersianCategory) VALUES(@EnglishCategory,@PersianCategory);

RETURN;
