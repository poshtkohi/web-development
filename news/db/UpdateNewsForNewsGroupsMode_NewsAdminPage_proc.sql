--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `UpdateNewsForNewsGroupsMode_NewsAdminPage_proc`$$
CREATE PROCEDURE `UpdateNewsForNewsGroupsMode_NewsAdminPage_proc`(IN PostID BIGINT,IN _PostTitle LONGTEXT CHARACTER SET utf8,IN _PostContent LONGTEXT CHARACTER SET utf8, IN _NewsSubject INT)
BEGIN

  UPDATE png set PostTitle=_PostTitle,PostContent=_PostContent,NewsSubject=_NewsSubject WHERE id=PostID AND IsDeleted=0;

END $$

DELIMITER ;