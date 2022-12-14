--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-postimporter]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'CheckExisitingPostTransferSession_PostImporterPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CheckExisitingPostTransferSession_PostImporterPage_proc
GO

CREATE PROCEDURE CheckExisitingPostTransferSession_PostImporterPage_proc

@BlogID BIGINT,
@IsExisted BIT OUTPUT,
@domain NVARCHAR(50) OUTPUT,
@TotalPosts INT OUTPUT,
@CurrentFetchedPost INT OUTPUT,
@LastFetchedPostDate DATETIME OUTPUT,
@BlogType INT OUTPUT,
@CategoryID BIGINT OUTPUT,
@PostImporterTag VARCHAR(16) OUTPUT,
@PostTag VARCHAR(16) OUTPUT,
@PostTitleTag VARCHAR(16) OUTPUT,
@PostContentTag VARCHAR(16) OUTPUT,
@DirectLinkTag VARCHAR(16) OUTPUT, 
@ContinuedPostTag VARCHAR(16) OUTPUT,
@IsFirstTimeStarted BIT OUTPUT


AS

SET @IsExisted=0;

SELECT @domain=domain,@TotalPosts=TotalPosts,@CurrentFetchedPost=CurrentFetchedPost,@LastFetchedPostDate=LastFetchedPostDate,@BlogType=BlogType,@CategoryID=CategoryID,@PostImporterTag=PostImporterTag,@PostTag=PostTag,@PostTitleTag=PostTitleTag,@PostContentTag=PostContentTag,@DirectLinkTag=DirectLinkTag,@ContinuedPostTag=ContinuedPostTag,@IsFirstTimeStarted=IsFirstTimeStarted FROM [iranblog-postimporter].dbo.sessions 
	WHERE BlogID=@BlogID AND IsDeleted=0;

IF(@@ROWCOUNT=1)
BEGIN
	IF(@TotalPosts=@CurrentFetchedPost AND @TotalPosts != 0)--consider date
	BEGIN
		UPDATE [iranblog-postimporter].dbo.sessions SET IsDeleted=1 WHERE BlogID=@BlogID AND IsDeleted=0;
		SET @IsExisted=0;
		RETURN;
	END
	ELSE
	BEGIN
		SET @IsExisted=1;
		RETURN;
	END
END

ELSE
BEGIN
	SET @IsExisted=0;
	RETURN;
END


RETURN;