--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [weblogs]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'IsShowCommentsPreVerifyLoad_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE IsShowCommentsPreVerifyLoad_CommentsPage_proc
GO

CREATE PROCEDURE IsShowCommentsPreVerifyLoad_CommentsPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@IsShowCommentsPreVerify BIT OUTPUT

AS


SELECT TOP 1 @IsShowCommentsPreVerify=IsShowCommentsPreVerify FROM posts WHERE [id]=@PostID AND BlogID=@BlogID;

RETURN
