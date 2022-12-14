--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE weblogs
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'BlogArchive_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BlogArchive_proc
GO

CREATE PROCEDURE BlogArchive_proc

@BlogID BIGINT,
@dt1 DATETIME,
@dt2 DATETIME

AS

SELECT [id],[date],subject,content,NumComments,continued,CategoryID,AuthorID FROM posts WHERE BlogID=@BlogID AND IsDeleted=0 AND [date] >= @dt1 AND [date] <= @dt2 ORDER BY [id] DESC

RETURN 
