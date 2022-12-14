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
	   WHERE  name = 'DoPost_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DoPost_MyPage_proc
GO

CREATE PROCEDURE DoPost_MyPage_proc

@BlogID BIGINT,
@PostDate DATETIME,
@PostType CHAR,
@PostLanguage CHAR,
@PostAlign BIT,
@PostContent NVARCHAR(500),
@CommentEnabled BIT,
@HasPicture BIT,
@IsAccountsDbLinkedServer BIT

AS


--BEGIN TRANSACTION

INSERT INTO  posts (BlogID,PostDate,PostType,PostLanguage,PostAlign,PostContent,CommentEnabled,HasPicture) VALUES(@BlogID,@PostDate,@PostType,@PostLanguage,@PostAlign,@PostContent,@CommentEnabled,@HasPicture);


IF @IsAccountsDbLinkedServer=1
BEGIN
	UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET PostNum=PostNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
END
ELSE
BEGIN
	UPDATE [peyghamak-accounts].dbo.accounts SET PostNum=PostNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
END

--COMMIT

RETURN
