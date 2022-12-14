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
	   WHERE  name = 'PurchasesShow_PurchasesAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE PurchasesShow_PurchasesAdminPage_proc
GO

CREATE PROCEDURE PurchasesShow_PurchasesAdminPage_proc

@PageSize INT,
@PageNumber INT,
@PurchaseNum INT OUTPUT,
@ShowVerifiedPurcheses BIT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM purchases WHERE IsDeleted=0 AND PurchaseCompleted=@ShowVerifiedPurcheses ORDER BY [id] DESC;
	

END
ELSE
BEGIN
	SET @PurchaseNum=0;
	SELECT @PurchaseNum=COUNT(*) FROM purchases WHERE IsDeleted=0 AND PurchaseCompleted=@ShowVerifiedPurcheses;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM purchases WHERE IsDeleted=0 AND PurchaseCompleted=@ShowVerifiedPurcheses ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT pu.[id] AS PurchaseID,pu.UserID,ac.username,pu.PursuitCode FROM purchases AS pu
		INNER JOIN accounts AS ac
		on pu.UserID=ac.[id]
		WHERE pu.[id]<@LastID AND pu.IsDeleted=0 AND pu.PurchaseCompleted=@ShowVerifiedPurcheses ORDER BY pu.[id] DESC;
	SET ROWCOUNT 0;
RETURN 