--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListSelectedNewsGroupsForNews_NewsAdminPage_proc`$$
CREATE PROCEDURE `ListSelectedNewsGroupsForNews_NewsAdminPage_proc`(IN _PostID BIGINT,IN _PersianYear INT,IN _PersianMonth INT,IN _PersianDay INT)
BEGIN

  SELECT ng.title FROM NewsGroups AS ng
	  INNER JOIN png AS pg
	  on pg.NewsGroupID=ng.id AND pg.PostID=_PostID AND pg.PersianYear=_PersianYear AND pg.PersianMonth=_PersianMonth AND pg.PersianDay=_PersianDay AND pg.IsDeleted=0
	  WHERE ng.IsDeleted=0 ORDER BY ng.id DESC;


END $$

DELIMITER ;