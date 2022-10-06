--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `Login_LoginPage_proc`$$
CREATE PROCEDURE `Login_LoginPage_proc`(IN _username VARCHAR(50) CHARACTER SET utf8,IN _password VARCHAR(50) CHARACTER SET utf8)
BEGIN

  SELECT id,AccountType FROM accounts WHERE username=_username AND `password`=_password AND IsDeleted=0;

END $$

DELIMITER ;