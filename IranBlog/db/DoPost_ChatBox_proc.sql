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
	   WHERE  name = 'DoPost_ChatBox_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DoPost_ChatBox_proc
GO

CREATE PROCEDURE DoPost_ChatBox_proc

@BlogID BIGINT,
@name NVARCHAR(30),
@email VARCHAR(50),
@PostContent NVARCHAR(200),
@date DATETIME,
@IsUserExisted BIT OUTPUT

AS

SET @IsUserExisted=0;

IF( (SELECT count(*) FROM [general].dbo.usersInfo where i=@BlogID)=1 )
BEGIN
	SET @IsUserExisted=1;
	UPDATE [iranblog-chatbox].dbo.MessageStat SET MessageNum=MessageNum+1 WHERE BlogID=@BlogID AND IsDeleted=0;
	IF(@@ROWCOUNT=0)
	BEGIN
		UPDATE [iranblog-chatbox].dbo.MessageStat SET MessageNum=1,IsDeleted=0 WHERE BlogID=@BlogID AND IsDeleted=1;
		IF(@@ROWCOUNT=0)
		BEGIN
			INSERT INTO [iranblog-chatbox].dbo.MessageStat (BlogID,MessageNum) VALUES(@BlogID,1);
		END
	END
	INSERT INTO [iranblog-chatbox].dbo.messages (BlogID,[name],email,PostContent,[date]) VALUES(@BlogID,@name,@email,@PostContent,@date);
	RETURN;
END


RETURN
