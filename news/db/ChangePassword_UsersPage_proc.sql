--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ChangePassword_UsersPage_proc` $$
CREATE PROCEDURE `ChangePassword_UsersPage_proc` (IN UserID BIGINT,IN _username VARCHAR(50) CHARACTER SET utf8,IN _lastPassword VARCHAR(50) CHARACTER SET utf8,IN _newPassword VARCHAR(50) CHARACTER SET utf8)
BEGIN

    UPDATE accounts SET `password`=_newPassword WHERE id=UserID AND username=_username AND `password`=_lastPassword AND IsDeleted=0;

END $$

DELIMITER ;