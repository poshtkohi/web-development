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
	   WHERE  name = 'Delete_AjaxPostArchivePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Delete_AjaxPostArchivePage_proc
GO

CREATE PROCEDURE Delete_AjaxPostArchivePage_proc

@PostArchiveID BIGINT,
@BlogID BIGINT

AS

DELETE FROM SubjectedArchive WHERE [id]=@PostArchiveID AND BlogID=@BlogID;

RETURN