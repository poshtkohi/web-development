--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE ListUsersBySearchQuery_UsersPage_proc

@query NVARCHAR(30),
@PageSize INT

AS


SET ROWCOUNT @PageSize;

SELECT accounts.[id],username,[name],ImageGuid,BirthYear,co.country,city,ShowAgeEnabled,ShowCityEnabled FROM accounts 
	INNER JOIN countries AS co
	on accounts.CountryKey=co.CountryKey
	WHERE IsDeleted=0 AND 
		(accounts.[name] LIKE '%'+@query+'%' OR accounts.city LIKE '%'+@query+'%' OR accounts.username LIKE '%'+@query+'%' OR co.country LIKE '%'+@query+'%')
			ORDER BY accounts.[id] DESC;

SET ROWCOUNT 0;


RETURN

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

