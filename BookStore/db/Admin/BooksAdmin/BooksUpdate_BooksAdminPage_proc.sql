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
	   WHERE  name = 'BooksUpdate_BooksAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BooksUpdate_BooksAdminPage_proc
GO

CREATE PROCEDURE BooksUpdate_BooksAdminPage_proc

@BookID BIGINT,
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

UPDATE books SET EnglishCategory=@EnglishCategory,PersianCategory=@PersianCategory,Title=@Title,Writer=@Writer,Translator=@Translator,Publisher=@Publisher,PublishDate=@PublishDate,Pages=@Pages,ISBN=@ISBN,FileType=@FileType,FileSize=@FileSize,Price=@Price,Abstract=@Abstract,[filename]=@filename,Language=@Language WHERE [id]=@BookID AND IsDeleted=0;

RETURN;
