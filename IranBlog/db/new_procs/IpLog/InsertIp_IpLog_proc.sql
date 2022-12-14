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
	   WHERE  name = 'InsertIp_IpLog_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE InsertIp_IpLog_proc
GO

CREATE PROCEDURE InsertIp_IpLog_proc 

@BlogID BIGINT,
@type INT,
@ip VARCHAR(20)

AS


IF((SELECT COUNT(*) FROM IpLog WHERE BlogId=@BlogID AND type=@type AND IsDeleted=0) = 0)
BEGIN
	INSERT INTO IpLog (BlogID,type,ip) VALUES(@BlogID,@type,@ip);
	RETURN;
END


--last login
IF(@type = 0)
BEGIN
	UPDATE IpLog SET ip=@ip,[date]=GETDATE() WHERE BlogID=@BlogID AND type=@type AND IsDeleted=0;
	RETURN;
END

RETURN;
