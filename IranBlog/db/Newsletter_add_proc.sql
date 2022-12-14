--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [iranblog-newsletter]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'Newsletter_add_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE Newsletter_add_proc
GO

CREATE PROCEDURE Newsletter_add_proc

@BlogID BIGINT,
@MemberNum INT OUTPUT,
@AllowedMemberNum INT,
@name NVARCHAR(50),
@email VARCHAR(50),
@IsExists BIT OUTPUT

AS

SELECT @MemberNum=COUNT(*) FROM newsletter WHERE BlogID=@BlogID AND IsDeleted=0;
IF (@MemberNum > @AllowedMemberNum)
BEGIN
	SET @IsExists=0;
	RETURN;
END

IF (SELECT COUNT(*) FROM newsletter WHERE BlogID=@blogID AND IsDeleted=0 AND email=@email) > 0
BEGIN
	SET @IsExists=1;
	RETURN;
END

ELSE
BEGIN
	INSERT INTO newsletter (BlogID,[name],email,IsDeleted) VALUES(@BlogID,@name,@email,0);
	SET @IsExists=0;
	RETURN;
END