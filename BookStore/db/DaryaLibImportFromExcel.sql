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
	   WHERE  name = 'BookStoreImportFromExcel' 
	   AND 	  type = 'P')
    DROP PROCEDURE BookStoreImportFromExcel
GO

CREATE PROCEDURE BookStoreImportFromExcel

@Title NVARCHAR(400),
@Writer NVARCHAR(100),
@Publisher NVARCHAR(100),
@PublishDate INT,
@Pages INT,
@ISBN NVARCHAR(50),
@IDENTIFIER NVARCHAR(50)
--@EnglishCategory NTEXT


AS

INSERT INTO books (Title,Writer,Publisher,PublishDate,Pages,ISBN,IDENTIFIER) VALUES(@Title,@Writer,@Publisher,@PublishDate,@Pages,@ISBN,@IDENTIFIER);

RETURN;
