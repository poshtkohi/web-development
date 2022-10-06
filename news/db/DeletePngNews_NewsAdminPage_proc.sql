--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `DeletePngNews_NewsAdminPage_proc` $$
CREATE PROCEDURE `DeletePngNews_NewsAdminPage_proc` (IN _PostID BIGINT)
BEGIN

  DECLARE _UserID BIGINT;
  SELECT @_UserID:=`UserID` FROM png WHERE id=_PostID AND IsDeleted=0;
  IF FOUND_ROWS() = 1
  THEN
    UPDATE accounts SET CopiedPostNum=CopiedPostNum-1 WHERE id=@_UserID AND IsDeleted=0;
    UPDATE png SET IsDeleted=1 WHERE id=_PostID AND UserID=@_UserID AND IsDeleted=0;
  END IF;

END $$

DELIMITER ;