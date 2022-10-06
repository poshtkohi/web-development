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
	   WHERE  name = 'RandomBooks_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE RandomBooks_HomePage_proc
GO

CREATE PROCEDURE RandomBooks_HomePage_proc

@RandomNumbers INT

AS


DECLARE @Random BIGINT;
DECLARE @Upper BIGINT;
DECLARE @Lower BIGINT



SELECT TOP 1 @Upper=[id] FROM books WHERE IsDeleted=0 ORDER BY [id] DESC;
SELECT TOP 1 @Lower=[id] FROM books WHERE IsDeleted=0;
SET @Lower =@Lower+@RandomNumbers;
SET @Upper =@Upper-@RandomNumbers;
SELECT @Random = ROUND(((@Upper - @Lower -1) * RAND() + @Lower), 0)

SET ROWCOUNT @RandomNumbers;
SELECT TOP 10 [id] AS BookID,IDENTIFIER FROM books WHERE [id]>@Random AND IsDeleted=0;
SET ROWCOUNT 0;

RETURN 