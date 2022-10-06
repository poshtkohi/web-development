--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `ListNewsByTextSearch_NewsAdminPage_proc`$$
CREATE PROCEDURE `ListNewsByTextSearch_NewsAdminPage_proc`(IN SearchText LONGTEXT CHARACTER SET utf8)
BEGIN

SELECT id,UserID,PostTitle,PostDate FROM posts WHERE IsDeleted=0 AND (PostTitle LIKE SearchText OR PostContent LIKE SearchText) ORDER BY id DESC;

END $$

DELIMITER ;