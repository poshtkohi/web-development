USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `NewsLoadForNewsGroupsMode_NewsAdminPage_proc`$$
CREATE PROCEDURE `NewsLoadForNewsGroupsMode_NewsAdminPage_proc`(IN _id BIGINT)
BEGIN

  SELECT PostTitle,PostContent,NewsSubject FROM png WHERE id=_id AND IsDeleted=0;

END $$

DELIMITER ;