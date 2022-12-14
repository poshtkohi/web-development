--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]

GO

-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'NewsLoad_NewsAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE NewsLoad_NewsAdminPage_proc
GO

CREATE PROCEDURE NewsLoad_NewsAdminPage_proc

@NewsID BIGINT

AS

SELECT NewsTitle,NewsContent,NewsLanguage,IsTopNews FROM news WHERE [id]=@NewsID AND IsDeleted=0;

RETURN;