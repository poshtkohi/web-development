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
	   WHERE  name = 'NewsShow_NewsDetailsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE NewsShow_NewsDetailsPage_proc
GO

CREATE PROCEDURE NewsShow_NewsDetailsPage_proc

@NewsID BIGINT

AS

SELECT TOP 1 [date],NewsTitle,NewsContent FROM news WHERE [id]=@NewsID AND IsDeleted=0;

RETURN 