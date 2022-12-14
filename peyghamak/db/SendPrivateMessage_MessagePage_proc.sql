--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-private-messages-1]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'SendPrivateMessage_MessagePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SendPrivateMessage_MessagePage_proc
GO

CREATE PROCEDURE SendPrivateMessage_MessagePage_proc

@PersonID BIGINT,
@MessagerBlogID BIGINT,
@PostDate DATETIME,
@PostType CHAR,
@PostLanguage CHAR,
@PostAlign BIT,
@PostContent NVARCHAR(500),
@HasPicture BIT,
@IsFriends1DbLinkedServer BIT,
@IsAccountsDbLinkedServer BIT

AS


--BEGIN TRANSACTION

--check follwership status
IF (@IsFriends1DbLinkedServer = 1)
BEGIN
	IF((SELECT count(*) FROM FRIENDS1DB.[peyghamak-friends-1].dbo.friends WHERE BlogID=@MessagerBlogID AND FriendBlogID=@PersonID AND IsFollower=1 AND IHaveBlocked=0 AND HeHasBlocked=0 AND IsDeleted=0) = 0)
	BEGIN
		RETURN;
	END
END
ELSE
BEGIN
	IF((SELECT count(*) FROM [peyghamak-friends-1].dbo.friends WHERE BlogID=@MessagerBlogID AND FriendBlogID=@PersonID AND IsFollower=1 AND IHaveBlocked=0 AND HeHasBlocked=0 AND IsDeleted=0) = 0)
	BEGIN
		RETURN;
	END
END


INSERT INTO messages (BlogID,MessagerBlogID,PostDate,PostAlign,PostLanguage,PostType,PostContent,HasPicture) VALUES(@PersonID,@MessagerBlogID,@PostDate,@PostAlign,@PostLanguage,@PostType,@PostContent,@HasPicture);


IF (@IsAccountsDbLinkedServer = 1)
BEGIN
	UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET PrivateMessagesNum=PrivateMessagesNum+1,HasReadPrivateMessages=0 WHERE [id]=@PersonID AND IsDeleted=0;
END
ELSE
BEGIN
	UPDATE [peyghamak-accounts].dbo.accounts SET PrivateMessagesNum=PrivateMessagesNum+1,HasReadPrivateMessages=0 WHERE [id]=@PersonID AND IsDeleted=0;
END

--COMMIT

RETURN
