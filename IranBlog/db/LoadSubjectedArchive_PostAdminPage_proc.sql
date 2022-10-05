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
	   WHERE  name = 'LoadSubjectedArchive_PostAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE LoadSubjectedArchive_PostAdminPage_proc
GO

CREATE PROCEDURE LoadSubjectedArchive_PostAdminPage_proc

@BlogID BIGINT

AS

	SELECT [id],subject FROM dbo.SubjectedArchive WHERE BlogID=@BlogID;

--Alireza br in baksh baiad SP ro ke sale 85 ezafeh kardeh boodi ro call koni

RETURN