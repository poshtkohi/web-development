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
	   WHERE  name = 'ChangePassword_AjaxAccountPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ChangePassword_AjaxAccountPage_proc
GO

CREATE PROCEDURE ChangePassword_AjaxAccountPage_proc

@BlogID BIGINT,
@NewPassword NVARCHAR(12),
@LastPassword NVARCHAR(12),
@NumAffectedRows INT OUTPUT

AS

UPDATE  usersInfo SET [password]=@NewPassword WHERE [i]=@BlogID AND [password]=@LastPassword;
SET @NumAffectedRows=@@ROWCOUNT;

RETURN;