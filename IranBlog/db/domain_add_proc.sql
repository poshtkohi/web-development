--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE general
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'domain_add_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE domain_add_proc
GO

CREATE PROCEDURE domain_add_proc

@BlogID BIGINT,
@subdomain VARCHAR(50),
@domain VARCHAR(50),
@DomainNum INT OUTPUT,
@AllowedDomainNum INT,
@IsExists BIT OUTPUT,
@IsDeleted BIT OUTPUT


AS

SELECT @DomainNum=COUNT(*) FROM domains WHERE BlogID=@BlogID AND IsDeleted=0;

IF (@DomainNum > @AllowedDomainNum)
BEGIN
	SET @IsExists=1;
	SET @IsDeleted=0;
	RETURN;
END

DECLARE @_id BIGINT;
DECLARE @_IsDeleted BIT;

SELECT TOP 1 @_id=[id],@_IsDeleted=IsDeleted FROM domains WHERE domain=@domain;

IF @@ROWCOUNT = 0
BEGIN
	INSERT INTO domains (BlogID,subdomain,domain,IsDeleted) VALUES(@BlogID,@subdomain,@domain,0);
	SET @IsExists=0;
	SET @IsDeleted=0;
	RETURN;
END

ELSE
BEGIN
	IF @_IsDeleted=1
	BEGIN
		UPDATE domains SET BlogID=@BlogID,subdomain=@subdomain,domain=@domain,IsDeleted=0 WHERE [id]=@_id;
		SET @IsExists=1;
		SET @IsDeleted=1;
		RETURN;
	END
	
	ELSE
	BEGIN
		SET @IsExists=1;
		SET @IsDeleted=0;
		RETURN;
	END
END