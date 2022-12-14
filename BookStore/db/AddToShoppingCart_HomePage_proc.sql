--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'AddToShoppingCart_HomePage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE AddToShoppingCart_HomePage_proc
GO

CREATE PROCEDURE AddToShoppingCart_HomePage_proc

@UserID BIGINT,
@BookID BIGINT,
@RecentlySelected BIT OUTPUT

AS


IF((SELECT COUNT(*) FROM ShoppingCart WHERE UserID=@UserID AND BookID=@BookID AND IsDeleted=0 AND PurchaseID=-1) > 0)
BEGIN
	SET @RecentlySelected=1;
	RETURN;
END
ELSE
BEGIN
	INSERT INTO ShoppingCart (UserID,BookID,SelectDate) VALUES(@UserID,@BookID,GETDATE());
	SET @RecentlySelected=0;
	RETURN;
END

RETURN;