--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-accounts]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ComputeFriendsPostNum_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ComputeFriendsPostNum_MyPage_proc
GO

CREATE PROCEDURE ComputeFriendsPostNum_MyPage_proc

@BlogID BIGINT,
@IsFriends1DbLinkedServer BIT,
@PostNum INT OUTPUT

AS

SET @PostNum=0;

IF (@IsFriends1DbLinkedServer=1)
BEGIN
	SELECT @PostNum=SUM(PostNum) FROM accounts AS ac INNER JOIN FRIENDS1DB.[peyghamak-friends-1].dbo.friends AS fr
		ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0;
END
ELSE
BEGIN
	SELECT @PostNum=SUM(PostNum) FROM accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
		ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFriend=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0;
END

RETURN;