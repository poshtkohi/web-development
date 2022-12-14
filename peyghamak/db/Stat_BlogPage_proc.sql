--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-posts-1]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'Stat_BlogPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Stat_BlogPage_proc
GO

CREATE PROCEDURE Stat_BlogPage_proc

@PostID BIGINT,
@BlogID BIGINT,
@IsAccountsDbLinkedServer BIT

AS


DECLARE @CommentNum INT

SELECT @CommentNum=NumComments FROM posts WHERE [id]=@PostID AND BlogID=@BlogID;
UPDATE posts SET IsDeleted=1 WHERE [id]=@PostID AND BlogID=@BlogID;
IF @@ROWCOUNT = 1
BEGIN
	IF @IsAccountsDbLinkedServer=1
	BEGIN
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET PostNum=PostNum-1,CommentNum=CommentNum-@CommentNum WHERE [id]=@BlogID  AND IsDeleted=0;
	END
	ELSE
	BEGIN
		UPDATE [peyghamak-accounts].dbo.accounts SET PostNum=PostNum-1,CommentNum=CommentNum-@CommentNum WHERE [id]=@BlogID  AND IsDeleted=0;
	END
END

RETURN 
