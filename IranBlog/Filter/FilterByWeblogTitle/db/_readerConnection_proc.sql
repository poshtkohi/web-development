--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [general]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = '_readerConnection_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE _readerConnection_proc
GO

CREATE PROCEDURE _readerConnection_proc

@TopMost BIGINT


AS

SET ROWCOUNT 1000;
SELECT i AS BlogID,title AS BlogTitle FROM usersInfo WHERE i>=@TopMost AND f='y';
SET ROWCOUNT 0;


RETURN;