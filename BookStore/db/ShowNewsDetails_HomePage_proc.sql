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
	   WHERE  name = 'ShowNewsDetails_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowNewsDetails_HomePage_proc
GO

CREATE PROCEDURE ShowNewsDetails_HomePage_proc

@NewsID INT

AS

SELECT  NewsTitle,NewsContent,[date],NewsImageGuid FROM news WHERE [id]=@NewsID AND IsDeleted=0;

RETURN 