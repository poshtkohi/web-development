--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [weblogs]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ListUpdates_MainPAge_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ListUpdates_MainPage_proc
GO

CREATE PROCEDURE ListUpdates_MainPage_proc

@PageSize INT,
@PageNumber INT,
@PostNum INT OUTPUT

AS

DECLARE @Ignore INT
DECLARE @LastID INT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM weblogs.dbo.updates ORDER BY [id] DESC;
END
ELSE
BEGIN
	SELECT @PostNum=COUNT(*) FROM weblogs.dbo.updates;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM weblogs.dbo.updates ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END


SET ROWCOUNT @PageSize;

SELECT  ac.subdomain,ac.title,po.[date],po.[id] AS PostID,po.subject FROM weblogs.dbo.updates AS up
	INNER JOIN general.dbo.usersInfo AS ac
	on up.BlogID=ac.i
	INNER JOIN weblogs.dbo.posts AS po
	on po.[id]=up.PostID AND po.BlogID=up.BlogID
	WHERE up.[id]<@LastID AND ac.f='y' ORDER BY up.[id] DESC;

SET ROWCOUNT 0;


RETURN

