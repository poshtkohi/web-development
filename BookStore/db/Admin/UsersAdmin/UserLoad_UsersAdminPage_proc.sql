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
	   WHERE  name = 'UserLoad_UsersAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE UserLoad_UsersAdminPage_proc
GO

CREATE PROCEDURE UserLoad_UsersAdminPage_proc

@UserID BIGINT

AS

SELECT username,[password],[name],email,address,PostalCode,BirthYear,[date],UserCredits FROM accounts WHERE [id]=@UserID AND IsDeleted=0;

RETURN;