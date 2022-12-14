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
	   WHERE  name = 'CreatePrimarySubjectedArchive_PostAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CreatePrimarySubjectedArchive_PostAdminPage_proc
GO

CREATE PROCEDURE CreatePrimarySubjectedArchive_PostAdminPage_proc

@BlogID BIGINT,
@CategoryID BIGINT OUTPUT

AS

	INSERT INTO dbo.SubjectedArchive (BlogID,subject,PostNum) VALUES(@BlogID,'عمومی',0); 
	SET @CategoryID = @@IDENTITY;


RETURN