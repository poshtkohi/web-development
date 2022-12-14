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
	   WHERE  name = 'AddFriend_GuestPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE AddFriend_GuestPage_proc
GO

CREATE PROCEDURE AddFriend_GuestPage_proc

@BlogID BIGINT,
@FriendBlogID BIGINT,
@IsAccountsDbLinkedServer BIT,
@_idMy BIGINT OUTPUT,
@_idHe BIGINT OUTPUT

AS


DECLARE @_idMyIsFriend BIT;
DECLARE @_IsDeleted BIT;
DECLARE @_HeHasBlocked BIT;

SELECT TOP 1 @_idMy=[id],@_idMyIsFriend=IsFriend,@_HeHasBlocked=HeHasBlocked,@_IsDeleted=IsDeleted FROM friends WHERE BlogID=@BlogID AND FriendBlogID=@FriendBlogID;
SELECT TOP 1 @_idHe=[id] FROM friends WHERE BlogID=@FriendBlogID AND FriendBlogID=@BlogID;

IF (@@ROWCOUNT = 0)
BEGIN
	INSERT INTO friends (BlogID,FriendBlogID,IsFriend,IsFollower,IHaveBlocked,HeHasBlocked,IsDeleted) 
	              VALUES(@BlogID,@FriendBlogID,1,        0,          0,            0,           0);
	SET @_idMy=@@IDENTITY;
	INSERT INTO friends (BlogID,FriendBlogID,IsFriend,IsFollower,IHaveBlocked,HeHasBlocked,IsDeleted) 
	              VALUES(@FriendBlogID,@BlogID, 0,        1,         0,            0,             0);
	SET @_idHe=@@IDENTITY;
	IF (@IsAccountsDbLinkedServer=1)
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

ELSE
BEGIN
	IF (@_IsDeleted=1)
	BEGIN
		UPDATE friends SET IsFriend=1,IsFollower=0,IHaveBlocked=0,HeHasBlocked=0,IsDeleted=0 WHERE [id]=@_idMy AND IsDeleted=0;
		UPDATE friends SET IsFriend=0,IsFollower=1,IHaveBlocked=0,HeHasBlocked=0,IsDeleted=0 WHERE [id]=@_idHe AND IsDeleted=0;
		IF (@IsAccountsDbLinkedServer=1)
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
	ELSE
	BEGIN
		IF(@_idMyIsFriend=0 AND @_HeHasBlocked=0)
		BEGIN
			UPDATE friends SET IsFriend=1,IHaveBlocked=0,HeHasBlocked=0 WHERE [id]=@_idMy AND IsDeleted=0;
			UPDATE friends SET IsFollower=1,IHaveBlocked=0,HeHasBlocked=0 WHERE [id]=@_idHe AND IsDeleted=0;
			IF (@IsAccountsDbLinkedServer=1)
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
	END
END


RETURN;