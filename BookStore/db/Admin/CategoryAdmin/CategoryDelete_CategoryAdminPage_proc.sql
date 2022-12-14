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
	   WHERE  name = 'CategoryDelete_CategoryAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CategoryDelete_CategoryAdminPage_proc
GO

CREATE PROCEDURE CategoryDelete_CategoryAdminPage_proc

@CategoryID BIGINT

AS


--BEGIN TRANSACTION

UPDATE BookCategory SET IsDeleted=1 WHERE [id]=@CategoryID;
UPDATE books SET IsDeleted=1 WHERE CategoryID=@CategoryID;

RETURN