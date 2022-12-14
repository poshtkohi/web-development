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
	   WHERE  name = 'BooksInsert_BooksAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BooksInsert_BooksAdminPage_proc
GO

CREATE PROCEDURE BooksInsert_BooksAdminPage_proc

@EnglishCategory NTEXT,
@PersianCategory NTEXT,
@Title NVARCHAR(400),
@Writer NVARCHAR(100),
@Translator NVARCHAR(100),
@Publisher NVARCHAR(100),
@PublishDate INT,
@Pages INT,
@ISBN NVARCHAR(50),
@FileType INT,
@FileSize INT,
@Price INT,
@Abstract NTEXT,
@filename NVARCHAR(400),
@Language INT

AS

INSERT INTO books (EnglishCategory,PersianCategory,Title,Writer,Translator,Publisher,PublishDate,Pages,ISBN,FileType,FileSize,Price,Abstract,[filename],Language) VALUES(@EnglishCategory,@PersianCategory,@Title,@Writer,@Translator,@Publisher,@PublishDate,@Pages,@ISBN,@FileType,@FileSize,@Price,@Abstract,@filename,@Language);

RETURN;
