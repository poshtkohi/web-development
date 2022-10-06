--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `UpdateNewsGroup_NewsGropusPage_proc`$$
CREATE PROCEDURE `UpdateNewsGroup_NewsGropusPage_proc`(IN NewsGroupID BIGINT,IN _title LONGTEXT CHARACTER SET utf8)
BEGIN

    UPDATE NewsGroups SET title=_title WHERE id=NewsGroupID AND IsDeleted=0;

END $$

DELIMITER ;