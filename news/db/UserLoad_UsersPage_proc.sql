--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `UserLoad_UsersPage_proc`$$
CREATE PROCEDURE `UserLoad_UsersPage_proc`(IN _id BIGINT)
BEGIN

  SELECT username,AccountType FROM accounts WHERE id=_id AND IsDeleted=0;

END $$

DELIMITER ;