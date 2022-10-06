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
	   WHERE  name = 'ShowUserInfoByUserID_UsersDetailsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowUserInfoByUserID_UsersDetailsPage_proc
GO

CREATE PROCEDURE ShowUserInfoByUserID_UsersDetailsPage_proc


@UserID BIGINT

AS
	SELECT username,[password],[name],email,address,PostalCode,tel,UserCredits FROM accounts WHERE IsDeleted=0 AND [id]=@UserID;
RETURN 