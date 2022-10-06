--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `DeleteNewsGroup_NewsGropusPage_proc`$$
CREATE PROCEDURE `DeleteNewsGroup_NewsGropusPage_proc`(IN DeleteID BIGINT)
BEGIN

    UPDATE NewsGroups SET IsDeleted=1 WHERE id=DeleteID AND IsDeleted=0;

END $$

DELIMITER ;