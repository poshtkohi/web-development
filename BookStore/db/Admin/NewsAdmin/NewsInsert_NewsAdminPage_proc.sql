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
	   WHERE  name = 'NewsInsert_NewsAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE NewsInsert_NewsAdminPage_proc
GO

CREATE PROCEDURE NewsInsert_NewsAdminPage_proc

@NewsTitle NVARCHAR(400),
@NewsContent NTEXT,
@NewsLanguage INT,
@IsTopNews BIT,
@TitleIsExisted BIT OUTPUT

AS

SET @TitleIsExisted=0;

IF((SELECT COUNT(*) FROM news WHERE NewsTitle LIKE @NewsTitle) >= 1)
BEGIN
	SET @TitleIsExisted = 1;
	RETURN;
END
ELSE
BEGIN
	INSERT INTO news ([date],NewsTitle,NewsContent,NewsLanguage,IsTopNews) VALUES(GETDATE(),@NewsTitle,@NewsContent,@NewsLanguage,@IsTopNews);
END

RETURN;
