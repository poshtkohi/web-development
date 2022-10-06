--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `SelectForNewsGroupNewsAdminMode_NewsAdminPage_proc`$$
CREATE PROCEDURE `SelectForNewsGroupNewsAdminMode_NewsAdminPage_proc`(IN _UserID BIGINT,IN _NewsGroupID BIGINT,IN _PostID BIGINT,IN _PostDate DATETIME,IN _PersianYear INT,IN _PersianMonth INT,IN _PersianDay INT,OUT IsExisted INT)
BEGIN

  DECLARE _PostTitle LONGTEXT CHARACTER SET utf8;
  DECLARE _PostContent LONGTEXT CHARACTER SET utf8;
  DECLARE _NewsSubject INT;

  SET IsExisted = 0;

  IF (SELECT COUNT(*) FROM png WHERE NewsGroupID=_NewsGroupID AND PostID=_PostID AND PersianYear=_PersianYear AND PersianMonth=_PersianMonth AND PersianDay=_PersianDay AND IsDeleted=0) > 0
  THEN
      SET IsExisted = 1;
  ELSE
    SELECT @_PostTitle:=PostTitle,@_PostContent:=PostContent,@_NewsSubject:=NewsSubject FROM posts WHERE id=_PostID AND IsDeleted=0;
    IF FOUND_ROWS() = 1
    THEN
      UPDATE accounts SET CopiedPostNum=CopiedPostNum+1 WHERE id=_UserID AND IsDeleted=0;
      INSERT INTO png (UserID,PostID,NewsGroupID,PostTitle,PostContent,NewsSubject,PostDate,PersianYear,PersianMonth,PersianDay,IsDeleted) VALUES(_UserID,_PostID,_NewsGroupID,@_PostTitle,@_PostContent,@_NewsSubject,_PostDate,_PersianYear,_PersianMonth,_PersianDay,0);
    ELSE
      SET IsExisted = 1;
    END IF;
  END IF;

END $$

DELIMITER ;