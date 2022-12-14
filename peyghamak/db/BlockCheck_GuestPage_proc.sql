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
	   WHERE  name = 'BlockCheck_GuestPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE BlockCheck_GuestPage_proc
GO

CREATE PROCEDURE BlockCheck_GuestPage_proc

@BlogID BIGINT,
@FriendBlogID BIGINT,
@_IHaveBlocked BIT OUTPUT,
@_HeHasBlocked BIT OUTPUT,
@_IsFriend BIT OUTPUT,
@_IsFollower BIT OUTPUT,
@_IsExisted BIT OUTPUT,
@_idMy BIGINT OUTPUT,
@_idHe BIGINT OUTPUT

AS

SET @_IHaveBlocked=0;
SET @_HeHasBlocked=0;
SET @_IsFriend=0;
SET @_IsFollower=0;
SET @_IsExisted=1;
SET @_idMy=-1;
SET @_idHe=-1;


DECLARE @_IsDeleted BIT;

SELECT TOP 1 @_idMy=[id],@_IsFriend=IsFriend,@_IsFollower=IsFollower,@_IHaveBlocked=IHaveBlocked,@_IsDeleted=IsDeleted FROM friends WHERE BlogID=@BlogID AND FriendBlogID=@FriendBlogID;
SELECT TOP 1 @_idHe=[id],@_HeHasBlocked=IHaveBlocked FROM friends WHERE BlogID=@FriendBlogID AND FriendBlogID=@BlogID;

IF (@@ROWCOUNT = 0)
BEGIN
	SET @_IsExisted=0;
	RETURN;
END
ELSE
BEGIN
	IF (@_IsDeleted=1)
	BEGIN
		RETURN;
	END
END

RETURN;