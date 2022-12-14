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
	   WHERE  name = 'CheckCorrectComment_CommentsPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE CheckCorrectComment_CommentsPage_proc
GO

CREATE PROCEDURE CheckCorrectComment_CommentsPage_proc

@BlogID BIGINT,
@PostID BIGINT

AS


SELECT TOP 1 [id],PostContent,PostAlign,PostType,PostLanguage,PostDate,NumComments FROM posts WHERE [id]=@PostID AND BlogID=@BlogID AND IsDeleted=0;

RETURN
