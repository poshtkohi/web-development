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
    DROP PROCEDURE Post_AjaxLinksPage_proc
GO

CREATE PROCEDURE Post_AjaxLinksPage_proc

@BlogID BIGINT,
@LinkTitle NVARCHAR(400),
@LinkAddress NVARCHAR(400),
@IsAjaxLinks BIT

AS

IF(@IsAjaxLinks=1)
BEGIN
	INSERT INTO links (BlogID,title,url,[date]) VALUES(@BlogID,@LinkTitle,@LinkAddress,GETDATE());
END

ELSE
BEGIN
	INSERT INTO linkss (BlogID,title,url,[date]) VALUES(@BlogID,@LinkTitle,@LinkAddress,GETDATE());
END

RETURN