--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListPosts_PostsPage_proc`$$
CREATE PROCEDURE `ListPosts_PostsPage_proc`(IN PageSize INT,IN PageNumber INT,OUT PostsNum BIGINT)
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
          SET @LastID = (SELECT id FROM posts WHERE `mode`=0 AND IsDeleted=0 ORDER BY id DESC LIMIT 1);
      ELSE
          SET @LastID = (SELECT id FROM posts WHERE `mode`=0 AND id<@LastID AND IsDeleted=0 ORDER BY id DESC LIMIT 1);
      END IF;
      SET @_i = @_i - 1;
    END WHILE;

  ELSE

	  SET PostsNum = (SELECT count(*) FROM posts WHERE `mode`=0 AND IsDeleted=0);
    SET @@SQL_SELECT_LIMIT = 1;
	  SET @LastID = (SELECT id FROM posts WHERE `mode`=0 AND IsDeleted=0 ORDER BY id DESC LIMIT 1);
  	SET @LastID=@LastID+1;

  END IF;

SET @@SQL_SELECT_LIMIT = PageSize;
SELECT id,UserID,PostTitle FROM posts WHERE `mode`=0 AND id<@LastID AND IsDeleted=0 ORDER BY id DESC;
SET @@SQL_SELECT_LIMIT = 0;

END $$

DELIMITER ;