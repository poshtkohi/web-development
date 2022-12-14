--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-comments]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowComments_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowComments_CommentsPage_proc
GO

CREATE PROCEDURE ShowComments_CommentsPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@IsLogined BIT,
@IsShowCommentsPreVerify BIT

AS

IF (@IsLogined=0)
BEGIN
	IF (@IsShowCommentsPreVerify=1)
	BEGIN
		SELECT [id],CommentDate,CommenterName,CommenterUrl,CommenterEmail,CommentContent,IsVerified FROM comments WHERE PostID=@PostID AND BlogID=@BlogID AND IsPrivateComment=0 AND IsDeleted=0 ORDER BY [id] DESC;
	END
	ELSE
	BEGIN
		SELECT [id],CommentDate,CommenterName,CommenterUrl,CommenterEmail,CommentContent,IsVerified FROM comments WHERE PostID=@PostID AND BlogID=@BlogID AND IsVerified=1 AND IsPrivateComment=0 AND IsDeleted=0 ORDER BY [id] DESC;
	END
END
ELSE
BEGIN
	SELECT [id],CommentDate,CommenterName,CommenterUrl,CommenterEmail,CommentContent,IsVerified FROM comments WHERE PostID=@PostID AND BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;	
END


RETURN 
