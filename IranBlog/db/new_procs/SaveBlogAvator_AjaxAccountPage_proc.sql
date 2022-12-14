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
	   WHERE  name = 'SaveBlogAvator_AjaxAccountPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SaveBlogAvator_AjaxAccountPage_proc
GO

CREATE PROCEDURE SaveBlogAvator_AjaxAccountPage_proc

@BlogID BIGINT,
@BlogImageGuid VARCHAR(50)

AS

UPDATE usersInfo SET ImageGuid=@BlogImageGuid WHERE i=@BlogID;

RETURN;