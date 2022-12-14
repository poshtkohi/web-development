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
	   WHERE  name = 'DeleteSession_PostImporterPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DeleteSession_PostImporterPage_proc
GO

CREATE PROCEDURE DeleteSession_PostImporterPage_proc

@BlogID BIGINT

AS

UPDATE [iranblog-postimporter].dbo.sessions SET IsDeleted=1 WHERE BlogID=@BlogID AND IsDeleted=0;


RETURN;