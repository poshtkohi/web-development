--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListAllNewsGroups_NewsGropusPage_proc`$$
CREATE PROCEDURE `ListAllNewsGroups_NewsGropusPage_proc`()
BEGIN

  SELECT id,title FROM newsgroups WHERE IsDeleted=0 ORDER BY id DESC;

END $$

DELIMITER ;