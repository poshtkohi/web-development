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
	   WHERE  name = 'RemoveFriend_FriendsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE RemoveFriend_FriendsPage_proc
GO

CREATE PROCEDURE RemoveFriend_FriendsPage_proc

@BlogID BIGINT,
@FriendBlogID BIGINT,
@IsAccountsDbLinkedServer BIT

AS

DECLARE @_idMy BIGINT;
DECLARE @IsMyFriend BIT;
DECLARE @IHaveBlocked BIT;
DECLARE @HeHasBlocked BIT;

DECLARE @_idHe BIGINT;


SELECT TOP 1 @_idMy=[id],@IsMyFriend=IsFriend,@IHaveBlocked=IHaveBlocked,@HeHasBlocked=HeHasBlocked FROM friends WHERE BlogID=@BlogID AND FriendBlogID=@FriendBlogID AND IsDeleted=0;
SELECT TOP 1 @_idHe=[id] FROM friends WHERE BlogID=@FriendBlogID AND FriendBlogID=@BlogID AND IsDeleted=0;


IF (@@ROWCOUNT = 1)
BEGIN
	IF (@IsMyFriend=1 AND @IHaveBlocked=0 AND @HeHasBlocked=0)
	BEGIN
		UPDATE friends SET IsFriend=0 WHERE [id]=@_idMy AND IsDeleted=0;
		UPDATE friends SET IsFollower=0 WHERE [id]=@_idHe AND IsDeleted=0;
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
END
ELSE
BEGIN
	RETURN ;
END

RETURN;