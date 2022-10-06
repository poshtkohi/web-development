--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListNewsForNewsGroupsMode_NewsAdminPage_proc`$$
CREATE PROCEDURE `ListNewsForNewsGroupsMode_NewsAdminPage_proc`(IN _NewsGroupID BIGINT,IN _PersianYear INT,IN _PersianMonth INT,IN _PersianDay INT)
BEGIN

  SELECT id,UserID,PostTitle,PostDate FROM png WHERE NewsGroupID=_NewsGroupID AND PersianYear=_PersianYear AND PersianMonth=_PersianMonth AND PersianDay=_PersianDay AND IsDeleted=0 ORDER BY id DESC;

END $$

DELIMITER ;