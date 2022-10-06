--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

-- =======================================================
DECLARE @_id BIGINT
DECLARE @CountryKey VARCHAR(3)
DECLARE @country NVARCHAR(100)

-- =======================================================
DECLARE _idCursor CURSOR FOR
SELECT [id] FROM [peyghamak-accounts].dbo.countries

OPEN _idCursor

FETCH NEXT FROM _idCursor INTO @_id
WHILE @@FETCH_STATUS = 0
BEGIN
        SELECT @CountryKey=CountryKey,@country=country FROM [peyghamak-accounts].dbo.countries WHERE [id]=@_id;
	INSERT INTO [MSSQL501.IXWEBHOSTING.COM].C119368_msdbb.dbo.countries (CountryKey,country) VALUES(@CountryKey,@country);
	FETCH NEXT FROM _idCursor INTO @_id
END

CLOSE _idCursor
DEALLOCATE _idCursor