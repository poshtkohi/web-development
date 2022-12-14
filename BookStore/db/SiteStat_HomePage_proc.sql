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
	   WHERE  name = 'SiteStat_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SiteStat_HomePage_proc
GO

CREATE PROCEDURE SiteStat_HomePage_proc

@BooksNum BIGINT OUTPUT,
@UsersNum BIGINT OUTPUT

AS

SET @BooksNum=0;
SET @UsersNum=0;
SELECT @BooksNum=COUNT(*) FROM books WHERE IsDeleted=0;
SELECT @UsersNum=COUNT(*) FROM accounts WHERE IsDeleted=0;

RETURN 