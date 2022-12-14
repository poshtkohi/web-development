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
	   WHERE  name = 'AccountUpdate_AccountPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE AccountUpdate_AccountPage_proc
GO

CREATE PROCEDURE AccountUpdate_AccountPage_proc

@UserID BIGINT,
@name NVARCHAR(50),
@email NVARCHAR(50),
@address NVARCHAR(200),
@PostalCode NVARCHAR(50),
@tel NVARCHAR(50),
@BirthYear INT,
@sex BIT,
@CountryKey VARCHAR(3),
@ProvinceKey VARCHAR(3)

AS


UPDATE accounts SET [name]=@name,email=@email,BirthYear=@BirthYear,sex=@sex,CountryKey=@CountryKey,ProvinceKey=@ProvinceKey,address=@address,PostalCode=@PostalCode,tel=@tel WHERE [id]=@UserID AND IsDeleted=0;

RETURN