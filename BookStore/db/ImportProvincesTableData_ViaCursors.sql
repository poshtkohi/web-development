--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

-- =======================================================
DECLARE @_id BIGINT
DECLARE @ProvinceKey VARCHAR(3)
DECLARE @province NVARCHAR(50)

-- =======================================================
DECLARE _idCursor CURSOR FOR
SELECT [id] FROM [peyghamak-accounts].dbo.provinces

OPEN _idCursor

FETCH NEXT FROM _idCursor INTO @_id
WHILE @@FETCH_STATUS = 0
BEGIN
        SELECT @ProvinceKey=ProvinceKey,@province=province FROM [peyghamak-accounts].dbo.provinces WHERE [id]=@_id;
	INSERT INTO msdbb.dbo.provinces (ProvinceKey,province) VALUES(@ProvinceKey,@province);
	FETCH NEXT FROM _idCursor INTO @_id
END

CLOSE _idCursor
DEALLOCATE _idCursor