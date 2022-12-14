--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-accounts]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ListTopUsers_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ListTopUsers_HomePage_proc
GO

CREATE PROCEDURE ListTopUsers_HomePage_proc

@TopUsersNum INT

AS


SET ROWCOUNT @TopUsersNum;

SELECT username,[name],ImageGuid FROM accounts WHERE IsDeleted=0 ORDER BY [id] DESC;

SET ROWCOUNT 0;


RETURN