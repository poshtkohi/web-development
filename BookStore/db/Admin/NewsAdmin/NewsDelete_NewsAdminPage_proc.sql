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
	   WHERE  name = 'NewsDelete_NewsAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE NewsDelete_NewsAdminPage_proc
GO

CREATE PROCEDURE NewsDelete_NewsAdminPage_proc

@NewsID BIGINT,
@NewsImageGuid VARCHAR(50) OUTPUT

AS


--BEGIN TRANSACTION

UPDATE news SET NewsImageGuid='default',IsDeleted=1 WHERE [id]=@NewsID;
SELECT @NewsImageGuid=NewsImageGuid FROM news WHERE [id]=@NewsID;

RETURN