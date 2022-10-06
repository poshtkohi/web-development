--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `UserEdit_UsersPage_proc`$$
CREATE PROCEDURE `UserEdit_UsersPage_proc`(IN _id BIGINT,IN pass VARCHAR(50) CHARACTER SET utf8,IN _AccountType INT)
BEGIN

  IF pass != ''
  THEN
    UPDATE accounts SET `password`=pass,AccountType=_AccountType WHERE id=_id AND IsDeleted=0;
  ELSE
    UPDATE accounts SET AccountType=_AccountType WHERE id=_id AND IsDeleted=0;
  END IF;

END $$

DELIMITER ;