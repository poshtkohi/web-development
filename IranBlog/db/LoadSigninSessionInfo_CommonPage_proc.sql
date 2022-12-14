--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [general]
GO
-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'LoadSigninSessionInfo_CommonPage_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE LoadSigninSessionInfo_CommonPage_proc
GO

CREATE PROCEDURE LoadSigninSessionInfo_CommonPage_proc

@AuthorID BIGINT,
@IsInTeamWeblogMode BIT


AS

IF (@IsInTeamWeblogMode=0)
BEGIN
	SELECT ac.i AS BlogID,ac.username,ac.subdomain,ac.ChatBoxIsEnabled FROM usersInfo AS ac
		INNER JOIN TeamWeblog AS tw
		on ac.i=tw.BlogID
		WHERE tw.[id]=@AuthorID;
END
ELSE

BEGIN
	SELECT ac.i AS BlogID,ac.username,ac.subdomain,ac.ChatBoxIsEnabled,tw.PostAccess,tw.OthersPostAccess,tw.SubjectedArchiveAccess,tw.WeblogLinksAccess,tw.DailyLinksAccess,tw.TemplateAccess,tw.PollAccess,tw.LinkBoxAccess,tw.NewsletterAccess,tw.FullAccess FROM usersInfo AS ac
		INNER JOIN TeamWeblog AS tw
		on ac.i=tw.BlogID
		WHERE tw.[id]=@AuthorID;
END

RETURN;