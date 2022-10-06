--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `SelectForNewsGroupNewsAdminInNewsGropMode_NewsAdminPage_proc`$$
CREATE PROCEDURE `SelectForNewsGroupNewsAdminInNewsGropMode_NewsAdminPage_proc`(IN _UserID BIGINT,IN _NewsGroupID BIGINT,IN _PostID BIGINT,IN _PostDate DATETIME,IN _PersianYear INT,IN _PersianMonth INT,IN _PersianDay INT,OUT IsExisted INT)
BEGIN

  DECLARE _PostTitle LONGTEXT CHARACTER SET utf8;
  DECLARE _PostContent LONGTEXT CHARACTER SET utf8;
  DECLARE _NewsSubject INT;
  DECLARE _PostIdForPostsTable BIGINT;

  SET IsExisted = 0;

  SELECT @_PostIdForPostsTable:=PostID,@_PostTitle:=PostTitle,@_PostContent:=PostContent,@_NewsSubject:=NewsSubject FROM png WHERE id=_PostID AND IsDeleted=0;

  IF FOUND_ROWS() = 1
  THEN
      IF (SELECT COUNT(*) FROM png WHERE NewsGroupID=_NewsGroupID AND PostID=@_PostIdForPostsTable AND PersianYear=_PersianYear AND PersianMonth=_PersianMonth AND PersianDay=_PersianDay AND IsDeleted=0) > 0
      THEN
        SET IsExisted = 1;
      ELSE
        UPDATE accounts SET CopiedPostNum=CopiedPostNum+1 WHERE id=_UserID AND IsDeleted=0;
        INSERT INTO png (UserID,PostID,NewsGroupID,PostTitle,PostContent,NewsSubject,PostDate,PersianYear,PersianMonth,PersianDay,IsDeleted) VALUES(_UserID,@_PostIdForPostsTable,_NewsGroupID,@_PostTitle,@_PostContent,@_NewsSubject,_PostDate,_PersianYear,_PersianMonth,_PersianDay,0);
      END IF;
  ELSE
      SET IsExisted = 1;
  END IF;

END $$

DELIMITER ;