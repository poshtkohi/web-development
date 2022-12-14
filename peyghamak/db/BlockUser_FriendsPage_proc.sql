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
	   WHERE  name = 'BlockUser_FriendsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BlockUser_FriendsPage_proc
GO

CREATE PROCEDURE BlockUser_FriendsPage_proc

@BlogID BIGINT,
@FriendBlogID BIGINT,
@IsAccountsDbLinkedServer BIT

AS

DECLARE @_idMyIsFrieand BIT;
DECLARE @_idMyIsFollower BIT;
DECLARE @IHaveBlocked BIT;
DECLARE @HeHasBlocked BIT;

DECLARE @_idHeIsFriend BIT;
DECLARE @_idHeIsFollower BIT;
DECLARE @_idHeIsBlocked BIT;

DECLARE @_idMy BIGINT;
DECLARE @_idHe BIGINT;


SELECT TOP 1 @_idMy=[id],@_idMyIsFrieand=IsFriend,@_idMyIsFollower=IsFollower,@IHaveBlocked=IHaveBlocked,@HeHasBlocked=HeHasBlocked FROM friends WHERE BlogID=@BlogID AND FriendBlogID=@FriendBlogID AND IsDeleted=0;
SELECT TOP 1 @_idHe=[id],@_idHeIsFriend=IsFriend,@_idHeIsFollower=IsFollower FROM friends WHERE BlogID=@FriendBlogID AND FriendBlogID=@BlogID AND IsDeleted=0;


IF (@@ROWCOUNT = 1)
BEGIN
	IF (@IHaveBlocked=0/* AND @HeHasBlocked=0*/)
	BEGIN
		UPDATE friends SET IHaveBlocked=1 WHERE [id]=@_idMy AND IsDeleted=0;
		UPDATE friends SET HeHasBlocked=1 WHERE [id]=@_idHe AND IsDeleted=0;
		IF (@_idMyIsFrieand=1 AND @_idMyIsFollower=0 AND @_idHeIsFriend=0 AND @_idHeIsFollower=1)
		BEGIN
			IF( @IsAccountsDbLinkedServer=1)
			BEGIN
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum-1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			ELSE
			BEGIN
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE [peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum-1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			RETURN;
		END
		IF (@_idMyIsFrieand=1 AND @_idMyIsFollower=1 AND @_idHeIsFriend=1 AND @_idHeIsFollower=1)
		BEGIN
			IF( @IsAccountsDbLinkedServer=1)
			BEGIN
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1,FollowerNum=FollowerNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1,FollowerNum=FollowerNum-1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			ELSE
			BEGIN
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1,FollowerNum=FollowerNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1,FollowerNum=FollowerNum-1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			RETURN;
		END
		IF (@_idMyIsFrieand=0 AND @_idMyIsFollower=1 AND @_idHeIsFriend=1 AND @_idHeIsFollower=0)
		BEGIN
			IF( @IsAccountsDbLinkedServer=1)
			BEGIN
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			ELSE
			BEGIN
				UPDATE [peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum-1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			RETURN;
		END
	END
END
ELSE
BEGIN
	RETURN ;
END

RETURN;