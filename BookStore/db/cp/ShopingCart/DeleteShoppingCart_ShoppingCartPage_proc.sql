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
	   WHERE  name = 'DeleteShoppingCart_ShoppingCartPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE DeleteShoppingCart_ShoppingCartPage_proc
GO

CREATE PROCEDURE DeleteShoppingCart_ShoppingCartPage_proc

@ShopingCartID BIGINT,
@UserID BIGINT

AS


UPDATE ShoppingCart SET IsDeleted=1 WHERE [id]=@ShopingCartID AND UserID=@UserID AND IsDeleted=0;

RETURN