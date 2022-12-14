USE [peyghamak-private-friends-1]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ListFollowers_FriendsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ListFollowers_FriendsPage_proc
GO

CREATE PROCEDURE ListFollowers_FriendsPage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@IsAccountsDbLinkedServer BIT,
@FollowerNum INT OUTPUT,
@_IsLogin BIT,
@_IsMyFriendsPage BIT,
@_IsOtherFriendsPage BIT,
@MyLoginedBlogID BIGINT

AS


DECLARE @Ignore INT
DECLARE @LastID INT
DECLARE @IHaveBlocked BIT

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM friends WHERE BlogID=@BlogID AND IsFollower=1 AND IHaveBlocked=0 AND HeHasBlocked=0 AND IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	IF (@IsAccountsDbLinkedServer=1)
	BEGIN
		SELECT @FollowerNum=FollowerNum FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT @FollowerNum=FollowerNum FROM [peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
	END

	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM friends WHERE BlogID=@BlogID AND IsFollower=1 AND IHaveBlocked=0 AND HeHasBlocked=0 AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

SET ROWCOUNT @PageSize;

IF (@IsAccountsDbLinkedServer=1)
BEGIN
	IF (@_IsLogin=0)
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,city,ShowAgeEnabled,ShowCityEnabled FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
			ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.countries AS co
			on ac.CountryKey=co.CountryKey
			WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
	IF ((@_IsLogin=1 AND @_IsMyFriendsPage=1) OR (@_IsLogin=1 AND @_IsOtherFriendsPage=1))
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,city,ShowAgeEnabled,ShowCityEnabled,(SELECT TOP 1 STR(IsFriend)+STR(IHaveBlocked)+STR(HeHasBlocked) FROM [peyghamak-friends-1].dbo.friends WHERE BlogID=@MyLoginedBlogID AND FriendBlogID=fr.FriendBlogID AND IsDeleted=0) AS FriendshipStatus
				FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
					ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
					INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.countries AS co
					on ac.CountryKey=co.CountryKey
					WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
END
ELSE
BEGIN
	IF (@_IsLogin=0)
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,city,ShowAgeEnabled,ShowCityEnabled FROM [peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
			ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN [peyghamak-accounts].dbo.countries AS co
			on ac.CountryKey=co.CountryKey
			WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
	IF ((@_IsLogin=1 AND @_IsMyFriendsPage=1) OR (@_IsLogin=1 AND @_IsOtherFriendsPage=1))
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,city,ShowAgeEnabled,ShowCityEnabled,(SELECT TOP 1 STR(IsFriend)+STR(IHaveBlocked)+STR(HeHasBlocked) FROM [peyghamak-friends-1].dbo.friends WHERE BlogID=@MyLoginedBlogID AND FriendBlogID=fr.FriendBlogID AND IsDeleted=0) AS FriendshipStatus
				FROM [peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
					ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
					INNER JOIN [peyghamak-accounts].dbo.countries AS co
					on ac.CountryKey=co.CountryKey
					WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
END

/*IF (@IsAccountsDbLinkedServer=1)
BEGIN
	IF (@_IsLogin=0)
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,pr.province,ShowAgeEnabled,ShowCityEnabled FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
			ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.countries AS co
			on ac.CountryKey=co.CountryKey
			INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.provinces AS pr
			on ac.ProvinceKey=pr.ProvinceKey
			WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
	IF ((@_IsLogin=1 AND @_IsMyFriendsPage=1) OR (@_IsLogin=1 AND @_IsOtherFriendsPage=1))
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,pr.province,ShowAgeEnabled,ShowCityEnabled,(SELECT TOP 1 STR(IsFriend)+STR(IHaveBlocked)+STR(HeHasBlocked) FROM [peyghamak-friends-1].dbo.friends WHERE BlogID=@MyLoginedBlogID AND FriendBlogID=fr.FriendBlogID AND IsDeleted=0) AS FriendshipStatus
				FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
					ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
					INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.countries AS co
					on ac.CountryKey=co.CountryKey
					INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.provinces AS pr
					on ac.ProvinceKey=pr.ProvinceKey
					WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
END
ELSE
BEGIN
	IF (@_IsLogin=0)
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,pr.province,ShowAgeEnabled,ShowCityEnabled FROM [peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
			ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
			INNER JOIN [peyghamak-accounts].dbo.countries AS co
			on ac.CountryKey=co.CountryKey
			INNER JOIN [peyghamak-accounts].dbo.provinces AS pr
			on ac.ProvinceKey=pr.ProvinceKey
			WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
	IF ((@_IsLogin=1 AND @_IsMyFriendsPage=1) OR (@_IsLogin=1 AND @_IsOtherFriendsPage=1))
	BEGIN
		SELECT fr.[id],fr.FriendBlogID,username,name,ImageGuid,BirthYear,co.country,pr.province,ShowAgeEnabled,ShowCityEnabled,(SELECT TOP 1 STR(IsFriend)+STR(IHaveBlocked)+STR(HeHasBlocked) FROM [peyghamak-friends-1].dbo.friends WHERE BlogID=@MyLoginedBlogID AND FriendBlogID=fr.FriendBlogID AND IsDeleted=0) AS FriendshipStatus
				FROM [peyghamak-accounts].dbo.accounts AS ac INNER JOIN [peyghamak-friends-1].dbo.friends AS fr
					ON fr.BlogID=@BlogID AND ac.id=fr.FriendBlogID AND fr.IsFollower=1 AND fr.IHaveBlocked=0 AND fr.HeHasBlocked=0 AND fr.IsDeleted=0 
					INNER JOIN [peyghamak-accounts].dbo.countries AS co
					on ac.CountryKey=co.CountryKey
					INNER JOIN [peyghamak-accounts].dbo.provinces AS pr
					on ac.ProvinceKey=pr.ProvinceKey
					WHERE fr.[id]<@LastID AND fr.IsDeleted=0 ORDER BY fr.[id] DESC;
	END
END*/

SET ROWCOUNT 0;


RETURN