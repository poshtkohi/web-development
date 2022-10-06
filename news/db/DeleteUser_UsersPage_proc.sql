--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `DeleteUser_UsersPage_proc` $$
CREATE PROCEDURE `DeleteUser_UsersPage_proc` (IN UserID BIGINT)
BEGIN

  DECLARE _id BIGINT;
  SELECT @_id:=`id` FROM accounts WHERE id=UserID AND IsDeleted=0;
  IF FOUND_ROWS() = 1
  THEN
    UPDATE posts SET IsDeleted=1 WHERE UserID=@_id AND IsDeleted=0;
    UPDATE accounts SET IsDeleted=1,PostNum=0,CopiedPostNum=0 WHERE id=@_id AND IsDeleted=0;
  END IF;

END $$

DELIMITER ;