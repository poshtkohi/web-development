--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-chatbox]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowMessages_ChatBox_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowMessages_ChatBox_proc
GO

CREATE PROCEDURE ShowMessages_ChatBox_proc

@BlogID BIGINT,
@PageSize INT,
@PageNumber INT,
@MessageNum INT OUTPUT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [iranblog-chatbox].dbo.messages WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;

END
ELSE
BEGIN
	SET @MessageNum=0;
	SELECT @MessageNum=MessageNum FROM [iranblog-chatbox].dbo.MessageStat WHERE BlogID=@BlogID AND IsDeleted=0;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [iranblog-chatbox].dbo.messages WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT  [id],[date],[name],email,PostContent FROM [iranblog-chatbox].dbo.messages WHERE [id]<@LastID AND BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;
	SET ROWCOUNT 0;
RETURN 