--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [general]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'AdminLogin_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE AdminLogin_CommentsPage_proc
GO

CREATE PROCEDURE AdminLogin_CommentsPage_proc

@BlogID BIGINT,
@PostID BIGINT,
@username VARCHAR(12),
@password NVARCHAR(12),
@IsLogined BIT OUTPUT /*,
@IsShowCommentsPreVerify BIT OUTPUT ,
@IsWeblogsDbLinkedServer BIT,*/

AS


SET @IsLogined=0;

IF ((SELECT count(*) FROM  dbo.usersInfo WHERE i=@BlogID AND username=@username AND [password]=@password) = 1)
BEGIN
	SET @IsLogined=1;
	--SELECT TOP 1 @IsShowCommentsPreVerify=IsShowCommentsPreVerify FROM weblogs.dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID;
	--Linked server settings for future system developments
	/*IF @IsWeblogsDbLinkedServer=1
	BEGIN
		SELECT TOP 1 @IsShowCommentsPreVerify=IsShowCommentsPreVerify FROM WEBLOGSDB.weblogs.dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID;
	END
	ELSE
	BEGIN
		SELECT TOP 1 @IsShowCommentsPreVerify=IsShowCommentsPreVerify FROM weblogs.dbo.posts WHERE [id]=@PostID AND BlogID=@BlogID;
	END*/
END

RETURN
