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
	   WHERE  name = 'PageShow_PagesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageShow_PagesAdminPage_proc
GO

CREATE PROCEDURE PageShow_PagesAdminPage_proc

@PageSize INT,
@PageNumber INT,
@PagesNum INT OUTPUT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM pages WHERE IsDeleted=0 ORDER BY [id] DESC;

END
ELSE
BEGIN
	SET @PagesNum=0;
	SELECT @PagesNum=COUNT(*) FROM pages WHERE IsDeleted=0;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM pages WHERE IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT  [id] AS PageID,PageTitle FROM pages WHERE [id]<@LastID AND IsDeleted=0 ORDER BY [id] DESC;
	SET ROWCOUNT 0;
RETURN 