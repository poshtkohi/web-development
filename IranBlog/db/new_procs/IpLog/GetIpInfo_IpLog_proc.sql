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
	   WHERE  name = 'GetIpInfo_IpLog_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE GetIpInfo_IpLog_proc
GO

CREATE PROCEDURE GetIpInfo_IpLog_proc 

@subdomain VARCHAR(30)

AS

SELECT ip.ip,ip.type,ip.[date] FROM IpLog as ip,usersInfo as ac WHERE ac.subdomain=@subdomain and ac.i=ip.BlogID;

RETURN;
