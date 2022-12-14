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
	   WHERE  name = 'BooksLoad_BooksAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BooksLoad_BooksAdminPage_proc
GO

CREATE PROCEDURE BooksLoad_BooksAdminPage_proc

@BookID BIGINT

AS

SELECT EnglishCategory,PersianCategory,Title,Writer,Translator,Publisher,PublishDate,Pages,ISBN,FileType,FileSize,Price,Abstract,[filename],Language,ImageGuid as BookImage,IDENTIFIER FROM books WHERE [id]=@BookID AND IsDeleted=0;

RETURN;