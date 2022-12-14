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
	   WHERE  name = 'SaveBlogSettings_AjaxAccountPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE SaveBlogSettings_AjaxAccountPage_proc
GO

CREATE PROCEDURE SaveBlogSettings_AjaxAccountPage_proc

@BlogID BIGINT,
@BlogEmail VARCHAR(50),
@BlogFirstName NVARCHAR(30),
@BlogLastName NVARCHAR(30),
@BlogTitle NVARCHAR(100),
@BlogAbout NVARCHAR(200),
@BlogEmailEnable BIT,
@BlogCategory VARCHAR(20),
@BlogMaxPostShow INT,
@BlogArciveDisplayMode BIT

AS

UPDATE usersInfo SET email=@BlogEmail,first_name=@BlogFirstName,last_name=@BlogLastName,title=@BlogTitle,about=@BlogAbout,emailEnable=@BlogEmailEnable,category=@BlogCategory,MaxPostShow=@BlogMaxPostShow,ArciveDisplayMode=@BlogArciveDisplayMode WHERE i=@BlogID;

RETURN;