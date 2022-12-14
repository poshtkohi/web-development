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
	   WHERE  name = 'ShowLatestPrivateMessages_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowLatestPrivateMessages_MyPage_proc
GO

CREATE PROCEDURE ShowLatestPrivateMessages_MyPage_proc

@BlogID BIGINT,
@TopLatestPrivateMessages INT,
@IsAccountsDbLinkedServer BIT

AS

SET ROWCOUNT @TopLatestPrivateMessages;

IF (@IsAccountsDbLinkedServer = 0)
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,me.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,HasPicture 
		FROM [peyghamak-private-messages-1].dbo.messages AS me 
		INNER JOIN [peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=me.MessagerBlogID
		WHERE me.BlogID=@BlogID AND me.IsDeleted=0 ORDER BY me.[id] DESC;
END

ELSE
BEGIN
	SELECT ac.username,ac.[name],ac.ImageGuid,me.[id],PostDate,PostType,PostAlign,PostLanguage,PostContent,HasPicture 
		FROM [peyghamak-private-messages-1].dbo.messages AS me 
		INNER JOIN ACCOUNTSDB.[peyghamak-accounts].dbo.accounts AS ac
		ON ac.[id]=me.MessagerBlogID
		WHERE me.BlogID=@BlogID AND me.IsDeleted=0 ORDER BY me.[id] DESC;
END

SET ROWCOUNT 0;

RETURN;