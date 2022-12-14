--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [general]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ChatBoxEnableDisable_ChatBox_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ChatBoxEnableDisable_ChatBox_proc
GO

CREATE PROCEDURE ChatBoxEnableDisable_ChatBox_proc

@BlogID BIGINT,
@ChatBoxIsEnabled BIT

AS

UPDATE [general].dbo.usersInfo SET ChatBoxIsEnabled=@ChatBoxIsEnabled WHERE i=@BlogID;

RETURN 