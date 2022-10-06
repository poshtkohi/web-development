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
	   WHERE  name = 'ShowShoppingCartByPurchaseID_PurchaseDetailsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowShoppingCartByPurchaseID_PurchaseDetailsPage_proc
GO

CREATE PROCEDURE ShowShoppingCartByPurchaseID_PurchaseDetailsPage_proc


@PurchaseID BIGINT

AS
	SELECT bo.Title AS BookTitle,bo.ISBN AS BookISBN FROM ShoppingCart AS sc
		INNER JOIN books AS bo
		on sc.BookID=bo.[id]
		WHERE sc.IsDeleted=0 AND sc.PurchaseID=@PurchaseID ORDER BY sc.[id] DESC;
RETURN 