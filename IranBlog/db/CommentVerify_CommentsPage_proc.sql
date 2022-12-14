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
	   WHERE  name = 'CommentVerify_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CommentVerify_CommentsPage_proc
GO

CREATE PROCEDURE CommentVerify_CommentsPage_proc

@CommentID BIGINT,
@PostID BIGINT,
@BlogID BIGINT

AS

UPDATE comments SET IsVerified=1 WHERE [id]=@CommentID AND PostID=@PostID AND BlogID=@BlogID;

RETURN 
