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
	   WHERE  name = 'CategoryShow_CategoryAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryShow_CategoryAdminPage_proc
GO

CREATE PROCEDURE CategoryShow_CategoryAdminPage_proc

@PageSize INT,
@PageNumber INT,
@CategoryNum INT OUTPUT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM BookCategory WHERE IsDeleted=0 ORDER BY [id] DESC;

END
ELSE
BEGIN
	SET @CategoryNum=0;
	SELECT @CategoryNum=COUNT(*) FROM BookCategory WHERE IsDeleted=0;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM BookCategory WHERE IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT  [id] AS CategoryID,EnglishCategory,PersianCategory FROM BookCategory WHERE [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
	SET ROWCOUNT 0;
RETURN 