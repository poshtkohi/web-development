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
	   WHERE  name = 'DoPurchase_ShoppingCartPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DoPurchase_ShoppingCartPage_proc
GO

CREATE PROCEDURE DoPurchase_ShoppingCartPage_proc


@UserID BIGINT,
@EachBookPrice INT,
@PursuitCode VARCHAR(50),
@HaveItemsToPurchase BIT OUTPUT,
@HaveEnoughCredits BIT OUTPUT

AS

DECLARE @CurrentShopingCartItems INT;
DECLARE @CurrentUserCredits BIGINT;
SET @CurrentShopingCartItems=0;
SET @CurrentUserCredits=0;


SET @HaveItemsToPurchase=1;
SET @HaveEnoughCredits=1;

SELECT @CurrentShopingCartItems=COUNT(*) FROM ShoppingCart WHERE UserID=@UserID AND IsDeleted=0 AND PurchaseID=-1;

IF(@CurrentShopingCartItems=0)
BEGIN
	SET @HaveItemsToPurchase=0;
	RETURN;
END
ELSE
BEGIN
	SELECT @CurrentUserCredits=UserCredits FROM accounts WHERE [id]=@UserID AND IsDeleted=0;
	IF(@EachBookPrice * @CurrentShopingCartItems > @CurrentUserCredits)
	BEGIN
		SET @HaveEnoughCredits=0;
		RETURN;
	END
	ELSE
	BEGIN
		INSERT INTO purchases (UserID,PursuitCode) VALUES(@UserID,@PursuitCode);
		UPDATE ShoppingCart SET PurchaseID=@@IDENTITY WHERE UserID=@UserID AND PurchaseID=-1 AND IsDeleted=0;
		UPDATE accounts SET UserCredits=UserCredits-@EachBookPrice * @CurrentShopingCartItems WHERE [id]=@UserID AND IsDeleted=0;
	END
END


--UPDATE ShoppingCart SET IsDeleted=1 WHERE [id]=@ShopingCartID AND UserID=@UserID AND IsDeleted=0;

RETURN