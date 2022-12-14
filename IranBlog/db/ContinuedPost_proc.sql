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
	   WHERE  name = 'ContinuedPost_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ContinuedPost_proc
GO

CREATE PROCEDURE ContinuedPost_proc

@PostID BIGINT,
@BlogID BIGINT

AS

SELECT TOP 1 [id],[date],subject,NumComments,continued,CategoryID,AuthorID FROM posts WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;

RETURN 
