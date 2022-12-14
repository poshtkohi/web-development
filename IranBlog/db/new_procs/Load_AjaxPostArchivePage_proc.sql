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
	   WHERE  name = 'Load_AjaxPostArchivePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Load_AjaxPostArchivePage_proc
GO

CREATE PROCEDURE Load_AjaxPostArchivePage_proc

@PostArchiveID BIGINT,
@BlogID BIGINT

AS


SELECT subject AS PostArchiveTitle FROM SubjectedArchive WHERE [id]=@PostArchiveID AND BlogID=@BlogID;

RETURN