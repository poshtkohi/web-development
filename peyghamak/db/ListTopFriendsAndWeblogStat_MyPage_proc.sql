--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-friends-1]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ListTopFriendsAndWeblogStat_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ListTopFriendsAndWeblogStat_MyPage_proc
GO

CREATE PROCEDURE ListTopFriendsAndWeblogStat_MyPage_proc

@BlogID BIGINT,
@TopFriendsShow INT,
@PostNum INT OUTPUT,
@FriendNum INT OUTPUT,
@FollowerNum INT OUTPUT,
@PrivateMessagesNum INT OUTPUT,
@HasReadPrivateMessages BIT OUTPUT,
@IsAccountsDbLinkedServer BIT

AS


SET ROWCOUNT @TopFriendsShow;

IF (@IsAccountsDbLinkedServer=1)
BEGIN
	SELECT TOP 1 @PostNum=PostNum,@PrivateMessagesNum=PrivateMessagesNum,@HasReadPrivateMessages=HasReadPrivateMessages,@FollowerNum=FollowerNum,@FriendNum=FriendNum FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID;
	SELECT username,[name],ImageGuid FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
		ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
END
ELSE
BEGIN
	SELECT TOP 1 @PostNum=PostNum,@PrivateMessagesNum=PrivateMessagesNum,@HasReadPrivateMessages=HasReadPrivateMessages,@FollowerNum=FollowerNum,@FriendNum=FriendNum FROM [peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID;
	SELECT username,[name],ImageGuid FROM [peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
		ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;

END

SET ROWCOUNT 0;

RETURN;