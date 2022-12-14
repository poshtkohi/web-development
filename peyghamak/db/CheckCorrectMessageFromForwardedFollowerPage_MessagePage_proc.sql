--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-friends-1]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'CheckCorrectMessageFromForwardedFollowerPage_MessagePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CheckCorrectMessageFromForwardedFollowerPage_MessagePage_proc
GO

CREATE PROCEDURE CheckCorrectMessageFromForwardedFollowerPage_MessagePage_proc

@BlogID BIGINT,
@PersonID BIGINT,
@IsAccountsDbLinkedServer BIT,
@IsMyFollower BIT OUTPUT,
@username NVARCHAR(50) OUTPUT,
@name NVARCHAR(50) OUTPUT,
@ImageGuid VARCHAR(50) OUTPUT

AS

IF((SELECT count(*) FROM friends WHERE BlogID=@BlogID AND FriendBlogID=@PersonID AND IsFollower=1 AND IHaveBlocked=0 AND HeHasBlocked=0 AND IsDeleted=0) = 1)
BEGIN
	IF (@IsAccountsDbLinkedServer=1)
	BEGIN
		SELECT TOP 1 @username=username,@name=[name],@ImageGuid=ImageGuid FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts WHERE [id]=@PersonID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT TOP 1 @username=username,@name=[name],@ImageGuid=ImageGuid FROM [peyghamak-accounts].dbo.accounts WHERE [id]=@PersonID AND IsDeleted=0;
	END
	IF( @@ROWCOUNT = 1)
	BEGIN
		SET @IsMyFollower = 1;
		RETURN;
	END
	ELSE
	BEGIN
		SET @IsMyFollower = 0;
		RETURN;
	END
END

ELSE
BEGIN
	SET @IsMyFollower = 0;
	RETURN;
END

RETURN
