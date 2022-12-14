--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-private-messages-1]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'PrivateMessageDelete_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PrivateMessageDelete_MyPage_proc
GO

CREATE PROCEDURE PrivateMessageDelete_MyPage_proc

@MessageID BIGINT,
@BlogID BIGINT,
@IsAccountsDbLinkedServer BIT

AS


UPDATE messages SET IsDeleted=1 WHERE [id]=@MessageID AND BlogID=@BlogID AND IsDeleted=0;

IF (@@ROWCOUNT = 1)
BEGIN
	IF (@IsAccountsDbLinkedServer = 1)
	BEGIN
		UPDATE ACCOUNTSDB.[peyghamak-accounts].dbo.accounts SET PrivateMessagesNum=PrivateMessagesNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
	ELSE
	BEGIN
		UPDATE [peyghamak-accounts].dbo.accounts SET PrivateMessagesNum=PrivateMessagesNum-1 WHERE [id]=@BlogID AND IsDeleted=0;
	END
END

RETURN 
