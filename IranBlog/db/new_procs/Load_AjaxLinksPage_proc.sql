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
	   WHERE  name = 'Load_AjaxLinksPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Load_AjaxLinksPage_proc
GO

CREATE PROCEDURE Load_AjaxLinksPage_proc

@LinkID BIGINT,
@BlogID BIGINT,
@IsAjaxLinks BIT

AS

IF(@IsAjaxLinks=1)
BEGIN
	SELECT title AS LinkTitle,url AS LinkAddress FROM links WHERE i=@LinkID AND BlogID=@BlogID;
END

ELSE
BEGIN
	SELECT title AS LinkTitle,url AS LinkAddress FROM linkss WHERE i=@LinkID AND BlogID=@BlogID;
END

RETURN