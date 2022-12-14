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
	   WHERE  name = 'BooksLoadCategories_BooksAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BooksLoadCategories_BooksAdminPage_proc
GO

CREATE PROCEDURE BooksLoadCategories_BooksAdminPage_proc


AS

SELECT  [id] AS CategoryID,EnglishCategory,PersianCategory FROM BookCategory WHERE IsDeleted=0;

RETURN;
