--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `GetNewsGrroupTitleByNewsGroupID_NewsGropusPage_proc`$$
CREATE PROCEDURE `GetNewsGrroupTitleByNewsGroupID_NewsGropusPage_proc`(IN title LONGTEXT CHARACTER SET utf8)
BEGIN

  SET NewsGroupTitle = (SELECT title FROM NewsGroups WHERE IsDeleted=0);
  IF FOUND_ROWS() = 0
  THEN
    SET NewsGroupTitle = '';
  END IF;

END $$

DELIMITER ;