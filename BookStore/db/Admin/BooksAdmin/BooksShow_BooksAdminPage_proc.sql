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
	   WHERE  name = 'BooksShow_BooksAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BooksShow_BooksAdminPage_proc
GO

CREATE PROCEDURE BooksShow_BooksAdminPage_proc

@PageSize INT,
@PageNumber INT,
@BooksNum INT OUTPUT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM books WHERE IsDeleted=0 ORDER BY [id] DESC;

END
ELSE
BEGIN
	SET @BooksNum=0;
	SELECT @BooksNum=COUNT(*) FROM books WHERE IsDeleted=0;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM books WHERE IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT  [id] AS BookID,Title FROM books WHERE [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
	SET ROWCOUNT 0;
RETURN 