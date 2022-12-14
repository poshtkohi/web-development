--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-private-messages-1]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowPrivateMessages_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowPrivateMessages_MyPage_proc
GO

CREATE PROCEDURE ShowPrivateMessages_MyPage_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@PrivateMessagesNum INT OUTPUT,
@IsAccountsDbLinkedServer BIT

AS

DECLARE @Ignore INT;
DECLARE @LastID INT;

/*----------------------------------------------------------------------------------------------------------------*/

IF (@PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [peyghamak-private-messages-1].dbo.messages WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
END
ELSE
BEGIN
	IF (@IsAccountsDbLinkedServer = 1)
	BEGIN
		SELECT @PrivateMessagesNum=PrivateMessagesNum FROM ACCOUNTSDB.[peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET HasReadPrivateMessages=1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		SELECT @PrivateMessagesNum=PrivateMessagesNum FROM [peyghamak-accounts].dbo.accounts WHERE [id]=@BlogID AND IsDeleted=0;
		UPDATE [peyghamak-accounts].dbo.accounts SET HasReadPrivateMessages=1 WHERE [id]=@BlogID AND IsDeleted=0;
	END

	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [peyghamak-private-messages-1].dbo.messages WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

SET ROWCOUNT @PageSize;

IF (@IsAccountsDbLinkedServer = 0)
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,me.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,HasPicture 
		FROM [peyghamak-private-messages-1].dbo.messages AS me 
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=me.MessagerBlogID
		WHERE me.BlogID=@BlogID AND me.[id]<@LastID AND me.IsDeleted=0 ORDER BY me.[id] DESC;
END

ELSE
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,me.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,HasPicture 
		FROM [peyghamak-private-messages-1].dbo.messages AS me 
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=me.MessagerBlogID
		WHERE me.BlogID=@BlogID AND me.[id]<@LastID AND me.IsDeleted=0 ORDER BY me.[id] DESC;
END

SET ROWCOUNT 0;

RETURN;

/*----------------------------------------------------------------------------------------------------------------*/