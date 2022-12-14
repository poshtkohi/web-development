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
	   WHERE  name = '_updaterConnection_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE _updaterConnection_proc
GO

CREATE PROCEDURE _updaterConnection_proc

@PostID BIGINT,
@IsUpdatePostContent BIT,
@IsUpdateContinuedPostContent BIT,
@PostContent NTEXT,
@ContinuedPostContent NTEXT

AS

IF(@IsUpdatePostContent = 1 AND @IsUpdateContinuedPostContent = 1)
BEGIN
	UPDATE posts SET content=@PostContent,continued=@ContinuedPostContent WHERE [id]=@PostID;
END

IF(@IsUpdatePostContent = 1 AND @IsUpdateContinuedPostContent = 0)
BEGIN
	UPDATE posts SET content=@PostContent WHERE [id]=@PostID;
END

IF(@IsUpdatePostContent = 0 AND @IsUpdateContinuedPostContent = 1)
BEGIN
	UPDATE posts SET continued=@ContinuedPostContent WHERE [id]=@PostID;
END


RETURN;