--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [peyghamak-posts-1]
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'ShowMyPostsRss_MyPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE ShowMyPostsRss_MyPage_proc
GO

CREATE PROCEDURE ShowMyPostsRss_MyPage_proc

@BlogID BIGINT,
@Top INT

AS

SET ROWCOUNT @Top;

SELECT [id],PostDate,PostType,PostAlign,PostLanguage,PostContent,CommentEnabled,NumComments,CommentVerified,HasPicture FROM [peyghamak-posts-1].dbo.posts
	 WHERE BlogID=@BlogID AND IsDeleted=0 ORDER BY [id] DESC;

SET ROWCOUNT 0;


RETURN 
