--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE `news`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `InsertNewPost_PostsPage_proc` $$
CREATE PROCEDURE `InsertNewPost_PostsPage_proc` (IN UserID BIGINT,IN PostTitle LONGTEXT CHARACTER SET utf8,IN PostContent LONGTEXT CHARACTER SET utf8,
                                          ,IN NewsSubject INT,IN PostDate DATETIME,IN PersianYear INT,IN PersianMonth INT,IN PersianDay INT)
BEGIN

    INSERT INTO posts (UserID,PostTitle,PostContent,PostDate,PersianYear,PersianMonth,PersianDay) VALUES(UserID,PostTitle,PostContent,PostDate,PersianYear,PersianMonth,PersianDay);
    UPDATE accounts set PostNum=PostNum+1 WHERE id=UserID AND IsDeleted=0;
    /*IF _mode = 1
    THEN
      UPDATE accounts set PostNum=PostNum+1 WHERE id=UserID AND IsDeleted=0;
    ELSE
      UPDATE accounts set CopiedPostNum=CopiedPostNum+1 WHERE id=UserID AND IsDeleted=0;
    END IF;*/

END $$

DELIMITER ;



DELIMITER $$
