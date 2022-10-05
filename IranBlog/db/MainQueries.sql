--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE weblogs
ALTER TABLE posts ADD IsDeleted BIT DEFAULT(0) NOT NULL ;
ALTER TABLE posts DROP DF__posts__CategoryC__7F56CBB8;
ALTER TABLE posts DROP COLUMN CategoryCounter;
ALTER TABLE posts DROP DF__posts__PostCount__004AEFF1;
ALTER TABLE posts DROP COLUMN PostCounter;
ALTER TABLE posts DROP DF_posts_AuthorCounter;
ALTER TABLE posts DROP COLUMN AuthorCounter;



USE general
CREATE TABLE [CategoryToAuthor] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[BlogID] [bigint] NOT NULL ,
	[CategoryID] [int] NOT NULL ,
	[AuthorID] [bigint] NOT NULL ,
	[PostNum] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [CategoryToAuthor] WITH NOCHECK ADD 
	CONSTRAINT [PK_CategoryToAuthor] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [CategoryToAuthor] ADD 
	CONSTRAINT [DF_CategoryToAuthor_PostNum] DEFAULT (0) FOR [PostNum]
GO

