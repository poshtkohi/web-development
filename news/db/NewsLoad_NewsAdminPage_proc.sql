--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `NewsLoad_NewsAdminPage_proc`$$
CREATE PROCEDURE `NewsLoad_NewsAdminPage_proc`(IN _id BIGINT)
BEGIN

  SELECT PostTitle,PostContent,NewsSubject FROM posts WHERE id=_id AND IsDeleted=0;

END $$

DELIMITER ;