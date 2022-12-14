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
	   WHERE  name = 'ShowAllBooks_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowAllBooks_HomePage_proc
GO

CREATE PROCEDURE ShowAllBooks_HomePage_proc

@PageSize INT,
@PageNumber INT,
@BookNum INT OUTPUT

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
	SET @BookNum=100;
	/*SELECT @BookNum=COUNT(*) FROM books WHERE IsDeleted=0;*/
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM books WHERE IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT  [id] AS BookID,Title AS BookTitle,Writer AS BookWriter,Abstract AS BookAbstract,Language AS BookLanguage,ImageGuid AS BookImage,ISBN AS BookISBN,PublishDate AS BookPublishDate,IDENTIFIER FROM books WHERE [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
	SET ROWCOUNT 0;
RETURN 