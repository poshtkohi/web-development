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
	   WHERE  name = 'DoPost_PagesPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DoPost_PagesPage_proc
GO

CREATE PROCEDURE DoPost_PagesPage_proc

@BlogID BIGINT,
@ThemeContent NTEXT,
@PostContent NTEXT,
@title NVARCHAR(200)

AS


--BEGIN TRANSACTION

	INSERT INTO  [iranblog-pages].dbo.PageThemes (BlogID,ThemeContent) VALUES(@BlogID,@ThemeContent);
	INSERT INTO  [iranblog-pages].dbo.posts (PageThemeID,title,PostContent) VALUES(@@IDENTITY,@title,@PostContent);
	UPDATE [general].dbo.usersInfo SET PagesNum=PagesNum+1 WHERE i=@BlogID;

--COMMIT

RETURN
