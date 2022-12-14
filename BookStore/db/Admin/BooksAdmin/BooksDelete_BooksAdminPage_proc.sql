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
	   WHERE  name = 'BooksDelete_BooksAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BooksDelete_BooksAdminPage_proc
GO

CREATE PROCEDURE BooksDelete_BooksAdminPage_proc

@BookID BIGINT

AS


--BEGIN TRANSACTION

UPDATE books SET IsDeleted=1 WHERE [id]=@BookID;

RETURN