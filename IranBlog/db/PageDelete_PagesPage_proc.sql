--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-pages]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'PageDelete_PagesPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PageDelete_PagesPage_proc
GO

CREATE PROCEDURE PageDelete_PagesPage_proc

@PageID BIGINT,
@BlogID BIGINT


AS

--BEGIN TRANSACTION

UPDATE PageThemes SET IsDeleted=1 WHERE [id]=@PageID AND BlogID=@BlogID AND IsDeleted=0;

IF (@@ROWCOUNT > 0)
BEGIN
	UPDATE posts SET IsDeleted=1 WHERE PageThemeID=@PageID AND IsDeleted=0;
END

IF (@@ROWCOUNT > 0)
BEGIN
	UPDATE [general].dbo.usersInfo SET PagesNum=PagesNum-1 WHERE i=@BlogID;
END

--COMMIT

RETURN 