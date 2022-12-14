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
	   WHERE  name = 'LoadBlogSettings_AjaxAccountPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE LoadBlogSettings_AjaxAccountPage_proc
GO

CREATE PROCEDURE LoadBlogSettings_AjaxAccountPage_proc

@BlogID BIGINT

AS

SELECT email AS BlogEmail,first_name AS BlogFirstName,last_name AS BlogLastName,title AS BlogTitle,about AS BlogAbout,emailEnable AS BlogEmailEnable,category AS BlogCategory,MaxPostShow AS BlogMaxPostShow,ArciveDisplayMode AS BlogArciveDisplayMode,ImageGuid AS BlogImageGuid FROM usersInfo WHERE i=@BlogID;

RETURN;