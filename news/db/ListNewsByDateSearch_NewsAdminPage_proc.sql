--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListNewsByDateSearch_NewsAdminPage_proc`$$
CREATE PROCEDURE `ListNewsByDateSearch_NewsAdminPage_proc`(IN _PersianYear INT,IN _PersianMonth INT,IN _PersianDay INT)
BEGIN

SELECT id,UserID,PostTitle,PostDate FROM posts WHERE PersianYear=_PersianYear AND PersianMonth=_PersianMonth AND PersianDay=_PersianDay AND IsDeleted=0 ORDER BY id DESC;

END $$

DELIMITER ;