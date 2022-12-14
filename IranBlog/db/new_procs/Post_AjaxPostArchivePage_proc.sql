--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [general]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'Post_AjaxPostArchivePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Post_AjaxPostArchivePage_proc
GO

CREATE PROCEDURE Post_AjaxPostArchivePage_proc

@BlogID BIGINT,
@PostArchiveTitle NVARCHAR(400)

AS

INSERT INTO SubjectedArchive (BlogID,subject,PostNum) VALUES(@BlogID,@PostArchiveTitle,0);

RETURN