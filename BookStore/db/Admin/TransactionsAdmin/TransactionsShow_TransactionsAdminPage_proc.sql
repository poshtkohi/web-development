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
	   WHERE  name = 'TransactionsShow_TransactionsAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE TransactionsShow_TransactionsAdminPage_proc
GO

CREATE PROCEDURE TransactionsShow_TransactionsAdminPage_proc

@PageSize INT,
@PageNumber INT,
@TransactionsNum INT OUTPUT

AS


DECLARE @Ignore INT
DECLARE @LastID INT

IF( @PageNumber > 1)
BEGIN 
	SET @Ignore = @PageSize * (@PageNumber - 1);
	SET ROWCOUNT @Ignore;
	SELECT @LastID=[id] FROM [transaction] WHERE IsPaied=0 AND IsDeleted=0 ORDER BY [id] DESC;

END
ELSE
BEGIN
	SET @TransactionsNum=0;
	SELECT @TransactionsNum=COUNT(*) FROM [transaction] WHERE IsPaied=0 AND IsDeleted=0;
	SET ROWCOUNT 1;
	SELECT @LastID=[id] FROM [transaction] WHERE IsPaied=0 AND IsDeleted=0 ORDER BY [id] DESC;
	SET @LastID=@LastID+1;
END

	SET ROWCOUNT @PageSize;
	SELECT  [id] AS TransactionsID,UserID,(SELECT username FROM accounts WHERE [id]=UserID) AS username,[date],amount FROM [transaction] WHERE [id]<@LastID AND IsPaied=0 AND IsDeleted=0 ORDER BY [id] DESC;
	SET ROWCOUNT 0;
RETURN 