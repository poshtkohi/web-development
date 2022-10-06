--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListNewsByTextSearchAndStartEndDate_NewsAdminPage_proc`$$
CREATE PROCEDURE `ListNewsByTextSearchAndStartEndDate_NewsAdminPage_proc`(IN SearchText LONGTEXT CHARACTER SET utf8,IN StartDate DATETIME,IN EndDate DATETIME)
BEGIN

  IF SearchText!=''
  THEN
    SELECT id,UserID,PostTitle,PostDate FROM posts WHERE IsDeleted=0 AND (PostTitle LIKE SearchText OR PostContent LIKE SearchText) AND (PostDate>=StartDate AND PostDate<=EndDate) ORDER BY id DESC;
  ELSE
    SELECT id,UserID,PostTitle,PostDate FROM posts WHERE IsDeleted=0 AND PostDate>=StartDate AND PostDate<=EndDate ORDER BY id DESC;
  END IF;

END $$

DELIMITER ;