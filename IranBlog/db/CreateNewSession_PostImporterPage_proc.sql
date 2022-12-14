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
	   WHERE  name = 'CreateNewSession_PostImporterPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CreateNewSession_PostImporterPage_proc
GO

CREATE PROCEDURE CreateNewSession_PostImporterPage_proc

@BlogID BIGINT,
@domain NVARCHAR(50),
@BlogType INT,
@PostImporterTag VARCHAR(16),
@PostTag VARCHAR(16),
@PostTitleTag VARCHAR(16),
@PostContentTag VARCHAR(16),
@DirectLinkTag VARCHAR(16), 
@ContinuedPostTag VARCHAR(16),
@subject NVARCHAR(400)


AS


IF((SELECT count(*) FROM [iranblog-postimporter].dbo.sessions WHERE BlogID=@BlogID AND IsDeleted=0) = 0)
BEGIN
	DECLARE @CategoryID BIGINT;
	INSERT INTO  general.dbo.SubjectedArchive (BlogID,subject,PostNum) VALUES(@BlogID,@subject,0);
	SET @CategoryID=@@IDENTITY;
	INSERT INTO [iranblog-postimporter].dbo.sessions (BlogID,CategoryID,domain,LastFetchedPostDate,BlogType,PostImporterTag,PostTag,PostTitleTag,PostContentTag,DirectLinkTag,ContinuedPostTag) VALUES(@BlogID,@CategoryID,@domain,GETDATE(),@BlogType,@PostImporterTag,@PostTag,@PostTitleTag,@PostContentTag,@DirectLinkTag,@ContinuedPostTag);
	RETURN;
END

ELSE
BEGIN
	RETURN;
END


RETURN;