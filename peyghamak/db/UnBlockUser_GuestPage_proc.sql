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
	   WHERE  name = 'UnBlockUser_GuestPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE UnBlockUser_GuestPage_proc
GO

CREATE PROCEDURE UnBlockUser_GuestPage_proc

@BlogID BIGINT,
@FriendBlogID BIGINT,
@_idMy BIGINT,
@_idHe BIGINT,
@IsAccountsDbLinkedServer BIT

AS

DECLARE @_idMyIsFrieand BIT;
DECLARE @_idMyIsFollower BIT;
DECLARE @IHaveBlocked BIT;
DECLARE @HeHasBlocked BIT;

DECLARE @_idHeIsFriend BIT;
DECLARE @_idHeIsFollower BIT;
DECLARE @_idHeIsBlocked BIT;


SELECT TOP 1 @_idMyIsFrieand=IsFriend,@_idMyIsFollower=IsFollower,@IHaveBlocked=IHaveBlocked,@HeHasBlocked=HeHasBlocked FROM friends WHERE [id]=@_idMy AND IsDeleted=0;
SELECT TOP 1 @_idHeIsFriend=IsFriend,@_idHeIsFollower=IsFollower FROM friends WHERE [id]=@_idHe AND IsDeleted=0;


IF (@@ROWCOUNT = 1)
BEGIN
	IF( @IHaveBlocked=1/* AND @HeHasBlocked=1*/)
	BEGIN
		UPDATE friends SET IHaveBlocked=0 WHERE [id]=@_idMy;
		UPDATE friends SET HeHasBlocked=0 WHERE [id]=@_idHe;
		IF (@_idMyIsFrieand=1 AND @_idMyIsFollower=0 AND @_idHeIsFriend=0 AND @_idHeIsFollower=1)
		BEGIN
			IF( @IsAccountsDbLinkedServer=1)
			BEGIN
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum+1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			ELSE
			BEGIN
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE [peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum+1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			RETURN;
		END
		IF (@_idMyIsFrieand=1 AND @_idMyIsFollower=1 AND @_idHeIsFriend=1 AND @_idHeIsFollower=1)
		BEGIN
			IF( @IsAccountsDbLinkedServer=1)
			BEGIN
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1,FollowerNum=FollowerNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1,FollowerNum=FollowerNum+1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			ELSE
			BEGIN
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1,FollowerNum=FollowerNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1,FollowerNum=FollowerNum+1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			RETURN;
		END
		IF (@_idMyIsFrieand=0 AND @_idMyIsFollower=1 AND @_idHeIsFriend=1 AND @_idHeIsFollower=0)
		BEGIN
			IF( @IsAccountsDbLinkedServer=1)
			BEGIN
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
			END
			ELSE
			BEGIN
				UPDATE [peyghamak-accounts].dbo.accounts SET FollowerNum=FollowerNum+1 WHERE [id]=@BlogID AND IsDeleted=0;
				UPDATE [peyghamak-accounts].dbo.accounts SET FriendNum=FriendNum+1 WHERE [id]=@FriendBlogID AND IsDeleted=0;
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