--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

-- =============================================
-- Create procedure with OUTPUT Parameters
-- =============================================
-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'stat_proc' 
	   AND 	  type = 'P')
    DROP PROCEDURE stat_proc
GO

CREATE PROCEDURE stat_proc

@Today DATETIME,
@Yesterday DATETIME,
@BlogID BIGINT,
@subdomain VARCHAR(50),
@TotalVisits BIGINT OUTPUT,
@TodayVisits INT OUTPUT,
@YesterdayVisits INT OUTPUT,
@ThisMonthVisits INT OUTPUT,
@count INT

AS

SELECT @count=(SELECT COUNT(*) FROM stat WHERE BlogID=@BlogID)


IF @count=1 
BEGIN
	SELECT @Today=Today,@Yesterday=Yesterday FROM stat WHERE BlogID=@BlogID
	/*SELECT @Today=(SELECT Today FROM stat WHERE BlogID=@BlogID)
	SELECT @Yesterday=(SELECT Yesterday FROM stat WHERE BlogID=@BlogID)*/
	IF @Today=@Yesterday
	BEGIN
		UPDATE stat SET TotalVisits=TotalVisits+1,TodayVisits=TodayVisits+1,ThisMonthVisits=ThisMonthVisits+1,Today=GETDATE(),Yesterday=GETDATE()-1 WHERE BlogID=@BlogID
		SELECT @TotalVisits=TotalVisits,@TodayVisits=TodayVisits,@YesterdayVisits=YesterdayVisits,@ThisMonthVisits=ThisMonthVisits FROM stat WHERE BlogID=@BlogID
	END

	ELSE
	BEGIN
		IF DAY(GETDATE())=1
		BEGIN
			UPDATE stat SET ThisMonthVisits=0 WHERE BlogID=@BlogID
		END

		IF DAY(@Today)=DAY(GETDATE())
		BEGIN
			UPDATE stat SET TotalVisits=TotalVisits+1,TodayVisits=TodayVisits+1,ThisMonthVisits=ThisMonthVisits+1 WHERE BlogID=@BlogID
			SELECT @TotalVisits=TotalVisits,@TodayVisits=TodayVisits,@YesterdayVisits=YesterdayVisits,@ThisMonthVisits=ThisMonthVisits FROM stat WHERE BlogID=@BlogID
		END

		ELSE
		BEGIN
			UPDATE stat SET Yesterday=Today,Today=GETDATE(),TotalVisits=TotalVisits+1,YesterdayVisits=TodayVisits,TodayVisits=1,ThisMonthVisits=ThisMonthVisits+1 WHERE BlogID=@BlogID
			SELECT @TotalVisits=TotalVisits,@TodayVisits=TodayVisits,@YesterdayVisits=YesterdayVisits,@ThisMonthVisits=ThisMonthVisits FROM stat WHERE BlogID=@BlogID
		END
	END

END




ELSE
BEGIN
	INSERT INTO stat (BlogID,subdomain,Today,Yesterday) VALUES(@BlogID,@subdomain,GETDATE(),GETDATE())
	UPDATE stat SET TotalVisits=TotalVisits+1,TodayVisits=TodayVisits+1,ThisMonthVisits=ThisMonthVisits+1 WHERE BlogID=@BlogID
	SELECT @TotalVisits=TotalVisits,@TodayVisits=TodayVisits,@YesterdayVisits=YesterdayVisits,@ThisMonthVisits=ThisMonthVisits FROM stat WHERE BlogID=@BlogID
END

RETURN 
