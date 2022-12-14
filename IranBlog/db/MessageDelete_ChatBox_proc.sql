--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-chatbox]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'MessageDelete_ChatBox_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE MessageDelete_ChatBox_proc
GO

CREATE PROCEDURE MessageDelete_ChatBox_proc

@MesseageID BIGINT,
@BlogID BIGINT


AS

--BEGIN TRANSACTION


UPDATE [iranblog-chatbox].dbo.messages SET IsDeleted=1 WHERE [id]=@MesseageID AND BlogID=@BlogID;

IF @@ROWCOUNT = 1
BEGIN
	UPDATE [iranblog-chatbox].dbo.MessageStat SET MessageNum=MessageNum-1 WHERE BlogID=@BlogID AND IsDeleted=0;
END

--COMMIT

RETURN 