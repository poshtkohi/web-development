--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `AddUser_UsersPage_proc`$$
CREATE PROCEDURE `AddUser_UsersPage_proc`(IN user VARCHAR(50) CHARACTER SET utf8,IN pass VARCHAR(50) CHARACTER SET utf8,IN AccountType INT,
                                          IN RegDate DATETIME,OUT IsExisted INT)
BEGIN

  SET  IsExisted = 0;
  IF (SELECT COUNT(*) FROM accounts WHERE username=user AND IsDeleted=0) > 0
  THEN
    SET IsExisted = 1;
  ELSE
    INSERT INTO accounts (username,password,AccountType,RegDate) VALUES(user,pass,AccountType,RegDate);
  END IF;

END $$

DELIMITER ;