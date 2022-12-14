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
	   WHERE  name = 'InsertNewPost_PostImporterPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE InsertNewPost_PostImporterPage_proc
GO

CREATE PROCEDURE InsertNewPost_PostImporterPage_proc

@BlogID BIGINT,
@subject NVARCHAR(200),
@content NTEXT,
@CategoryID BIGINT,
@AuthorID BIGINT

AS

EXECUTE [general].dbo.CategoryToAuthor_PostPage_proc @BlogID,@CategoryID,@AuthorID;
INSERT INTO [weblogs].dbo.posts (BlogID,date,subject,content,CategoryID,AuthorID) VALUES(@BlogID,GETDATE(),@subject,@content,@CategoryID,@AuthorID);
UPDATE [iranblog-postimporter].dbo.sessions SET CurrentFetchedPost=CurrentFetchedPost+1,LastFetchedPostDate=GETDATE() WHERE BlogID=@BlogID;

RETURN;