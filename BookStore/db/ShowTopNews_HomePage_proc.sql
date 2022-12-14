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
	   WHERE  name = 'ShowTopNews_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowTopNews_HomePage_proc
GO

CREATE PROCEDURE ShowTopNews_HomePage_proc

@NewsLanguage INT

AS

SELECT TOP 1 NewsTitle,NewsContent,[date],NewsImageGuid FROM news WHERE IsDeleted=0 AND IsTopNews=1 AND NewsLanguage=@NewsLanguage ORDER BY [id] DESC;

RETURN 