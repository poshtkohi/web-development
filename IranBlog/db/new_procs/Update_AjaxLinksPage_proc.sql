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
	   WHERE  name = 'Update_AjaxLinksPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Update_AjaxLinksPage_proc
GO

CREATE PROCEDURE Update_AjaxLinksPage_proc

@LinkID BIGINT,
@BlogID BIGINT,
@LinkTitle NVARCHAR(400),
@LinkAddress NVARCHAR(400),
@IsAjaxLinks BIT

AS

IF(@IsAjaxLinks=1)
BEGIN
	UPDATE links SET title=@LinkTitle,url=@LinkAddress WHERE i=@LinkID AND BlogID=@BlogID;
END

ELSE
BEGIN
	UPDATE linkss SET title=@LinkTitle,url=@LinkAddress WHERE i=@LinkID AND BlogID=@BlogID;
END

RETURN