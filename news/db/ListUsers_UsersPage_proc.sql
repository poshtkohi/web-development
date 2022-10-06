--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListNews_UsersPage_proc`$$
CREATE PROCEDURE `ListUsers_UsersPage_proc`(IN PageSize INT,IN PageNumber INT,OUT UsersNum BIGINT)
BEGIN
  DECLARE _Ignore INT;
  DECLARE LastID INT;
  DECLARE _i INT;

  IF PageNumber > 1
  THEN
	  SET @_Ignore = PageSize * (PageNumber - 1);
    SET @_i=@_Ignore;
    SET @@SQL_SELECT_LIMIT = @_Ignore;

    WHILE @_i > 0 DO
      IF @_i = @_Ignore
        THEN
          SET @LastID = (SELECT id FROM accounts WHERE IsDeleted=0 ORDER BY id DESC LIMIT 1);
      ELSE
          SET @LastID = (SELECT id FROM accounts WHERE id<@LastID AND IsDeleted=0 ORDER BY id DESC LIMIT 1);
      END IF;
      SET @_i = @_i - 1;
    END WHILE;

  ELSE

	  SET UsersNum = (SELECT count(*) FROM accounts WHERE IsDeleted=0);
    SET @@SQL_SELECT_LIMIT = 1;
	  SET @LastID = (SELECT id FROM accounts WHERE IsDeleted=0 ORDER BY id DESC LIMIT 1);
  	SET @LastID=@LastID+1;

  END IF;

SET @@SQL_SELECT_LIMIT = PageSize;
SELECT ac.id,ac.username,ac.`password`,at.`role-fa` AS AccountType,ac.PostNum,ac.CopiedPostNum FROM accounts AS ac
	INNER JOIN AccountType AS at
	on at.`value`=ac.AccountType
	WHERE ac.id<@LastID AND ac.IsDeleted=0 ORDER BY ac.id DESC;
SET @@SQL_SELECT_LIMIT = 0;

END $$

DELIMITER ;