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
	   WHERE  name = 'NewsUpdate_NewsAdminPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE NewsUpdate_NewsAdminPage_proc
GO

CREATE PROCEDURE NewsUpdate_NewsAdminPage_proc

@NewsID BIGINT,
@NewsTitle NVARCHAR(400),
@NewsContent NTEXT,
@NewsLanguage INT,
@IsTopNews BIT,
@TitleIsExisted BIT OUTPUT

AS

SET @TitleIsExisted=0;

IF((SELECT COUNT(*) FROM news WHERE NewsTitle LIKE @NewsTitle AND [id]!=@NewsID) >= 1)
BEGIN
	SET @TitleIsExisted = 1;
	RETURN;
END
ELSE
BEGIN
	UPDATE news SET NewsTitle=@NewsTitle,NewsContent=@NewsContent,NewsLanguage=@NewsLanguage,IsTopNews=@IsTopNews WHERE [id]=@NewsID AND IsDeleted=0;
END

RETURN;
