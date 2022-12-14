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
	   WHERE  name = 'ShowSearchBookByISBN_SearchPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowSearchBookByISBN_SearchPage_proc
GO

CREATE PROCEDURE ShowSearchBookByISBN_SearchPage_proc

@BookISBN NVARCHAR(50)

AS

SELECT [id] AS BookID,Title AS BookTitle,Writer AS BookWriter,Abstract AS BookAbstract,Language AS BookLanguage,ImageGuid AS BookImage,PublishDate AS BookPublishDate,Publisher AS BookPublisher,Pages AS BookPages,IDENTIFIER FROM books WHERE ISBN=@BookISBN AND IsDeleted=0;

RETURN 