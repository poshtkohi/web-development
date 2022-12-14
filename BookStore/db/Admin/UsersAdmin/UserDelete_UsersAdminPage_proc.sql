--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]

GO

-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'UserDelete_UsersAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE UserDelete_UsersAdminPage_proc
GO

CREATE PROCEDURE UserDelete_UsersAdminPage_proc

@UserID BIGINT

AS


UPDATE accounts SET IsDeleted=1 WHERE [id]=@UserID;
UPDATE purchases SET IsDeleted=1 WHERE [id]=@UserID;
UPDATE ShoppingCart SET IsDeleted=1 WHERE [id]=@UserID;

RETURN