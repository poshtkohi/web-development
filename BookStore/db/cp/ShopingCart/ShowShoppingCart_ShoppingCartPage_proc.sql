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
	   WHERE  name = 'ShowShoppingCart_ShoppingCartPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowShoppingCart_ShoppingCartPage_proc
GO

CREATE PROCEDURE ShowShoppingCart_ShoppingCartPage_proc

@PageSize INT,
@PageNumber INT,
@ShoppingCartNum INT OUTPUT,
@UserID BIGINT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM ShoppingCart WHERE IsDeleted=0 AND UserID=@UserID AND PurchaseID=-1 ORDER BY [id] DESC;

END
ELSE
BEGIN
	SET @ShoppingCartNum=0;
	SELECT @ShoppingCartNum=COUNT(*) FROM ShoppingCart WHERE IsDeleted=0 AND UserID=@UserID AND PurchaseID=-1 ;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM ShoppingCart WHERE IsDeleted=0 AND UserID=@UserID AND PurchaseID=-1 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT bo.Title AS BookTitle,bo.ISBN AS BookISBN,sc.[id] AS ShopingCartID FROM ShoppingCart AS sc
		INNER JOIN books AS bo
		on bo.[id]=sc.BookID
		WHERE sc.[id]<@LastID AND sc.IsDeleted=0 AND sc.UserID=@UserID AND sc.PurchaseID=-1 ORDER BY sc.[id] DESC;
	SET ROWCOUNT 0;
RETURN 


