/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Globalization;

namespace bookstore
{
	[Serializable]
	public class PersianCalendar : Calendar 
	{
		public static readonly int PersianEra = 1;
		//---------------------------------------------------------------------------------------------------
		public override System.DateTime AddMonths(System.DateTime time, int months) 
		{
			if (Math.Abs(months) > 120000)
			{
				throw new System.ArgumentOutOfRangeException("months", "Valid values are between -120000 and 120000, inclusive.");
			}
			int year = GetYear(true, time);
			int month = GetMonth(false, time);
			int day = GetDayOfMonth(false, time);

			month += (year-1)*12 + months;
			year = (month-1)/12 + 1;
			month -= (year-1)*12;
			if (day > 29)
			{
				int maxday = GetDaysInMonth(false, year, month, 0);
				if (maxday < day) day = maxday;
			}
			DateTime dateTime;
			try 
			{
				dateTime = ToDateTime(year, month, day, 0, 0, 0, 0) + time.TimeOfDay;
			}
			catch (System.ArgumentOutOfRangeException)
			{
				throw new System.ArgumentException("The resulting DateTime is outside the supported range.", "months");
			}
			return dateTime;
		}
		//---------------------------------------------------------------------------------------------------
		public override System.DateTime AddYears(System.DateTime time, int years) 
		{
			int year = GetYear(true, time);
			int month = GetMonth(false, time);
			int day = GetDayOfMonth(false, time);
			year += years;
			if (day == 30 && month == 12)
			{
				if (!IsLeapYear(false, year, 0))
					day = 29;
			}
			DateTime dateTime;
			try 
			{
				dateTime = ToDateTime(year, month, day, 0, 0, 0, 0) + time.TimeOfDay;
			}
			catch (System.ArgumentOutOfRangeException)
			{
				throw new System.ArgumentException("The resulting DateTime is outside the supported range.", "years");
			}
			return dateTime;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetDayOfMonth(System.DateTime time) 
		{
			return GetDayOfMonth(true, time);
		}
		//---------------------------------------------------------------------------------------------------
		private int GetDayOfMonth(bool validate, System.DateTime time) 
		{
			int days = GetDayOfYear(validate, time);
			for (int i=0; i<6; i++)
			{
				if (days <= 31) return days;
				days -= 31;
			}
			for (int i=0; i<5; i++)
			{
				if (days <= 30) return days;
				days -= 30;
			}
			return days;
		}
		//---------------------------------------------------------------------------------------------------
		public override System.DayOfWeek GetDayOfWeek(System.DateTime time) 
		{			
			return time.DayOfWeek;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetDayOfYear(System.DateTime time) 
		{
			return GetDayOfYear(true, time);
		}
		//---------------------------------------------------------------------------------------------------
		private int GetDayOfYear(bool validate, System.DateTime time) 
		{
			int year;
			int days;
			GetYearAndRemainingDays(validate, time, out year, out days);
			return days;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetDaysInMonth(int year, int month, int era) 
		{
			return GetDaysInMonth(true, year, month, era);
		}
		//---------------------------------------------------------------------------------------------------
		private int GetDaysInMonth(bool validate, int year, int month, int era)
		{
			CheckEraRange(validate, era);
			CheckYearRange(validate, year);
			CheckMonthRange(validate, month);
			if (month < 7) return 31;
			if (month < 12) return 30;
			if (IsLeapYear(false, year, 0)) return 30;
			else return 29;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetDaysInYear(int year, int era) 
		{
			return GetDaysInYear(true, year, era);
		}
		//---------------------------------------------------------------------------------------------------
		private int GetDaysInYear(bool validate, int year, int era) 
		{
			if (IsLeapYear(validate, year, era)) return 366;
			return 365;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetEra(System.DateTime time) 
		{
			CheckTicksRange(true, time);
			return PersianCalendar.PersianEra;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetMonth(System.DateTime time) 
		{
			return GetMonth(true, time);
		}
		//---------------------------------------------------------------------------------------------------
		private int GetMonth(bool validate, System.DateTime time) 
		{
			int days = GetDayOfYear(validate, time);
			if (days <= 31) return 1;
			if (days <= 62) return 2;
			if (days <= 93) return 3;
			if (days <= 124) return 4;
			if (days <= 155) return 5;
			if (days <= 186) return 6;
			if (days <= 216) return 7;
			if (days <= 246) return 8;
			if (days <= 276) return 9;
			if (days <= 306) return 10;
			if (days <= 336) return 11;
			return 12;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetMonthsInYear(int year, int era) 
		{
			CheckEraRange(true, era);
			CheckYearRange(true, year);
			return 12;
		}
		//---------------------------------------------------------------------------------------------------
		public override int GetYear(System.DateTime time) 
		{
			return GetYear(true, time);
		}
		//---------------------------------------------------------------------------------------------------
		private int GetYear(bool validate, System.DateTime time)
		{
			int days;
			int year;
			GetYearAndRemainingDays(validate, time, out year, out days);
			return year;
		}
		//---------------------------------------------------------------------------------------------------
		public override bool IsLeapDay(int year, int month, int day, int era) 
		{
			CheckEraRange(true, era);
			CheckYearRange(true, year);
			CheckMonthRange(true, month);
			if (day == 30 && month == 12 && IsLeapYear(false, year, 0))
				return true;
			return false;
		}
		//---------------------------------------------------------------------------------------------------
		public override bool IsLeapMonth(int year, int month, int era) 
		{
			CheckEraRange(true, era);
			CheckYearRange(true, year);
			CheckMonthRange(true, month);
			return false;
		}
		//---------------------------------------------------------------------------------------------------
		private static bool HasLeapFrac(int year)
		{
			double a = 31*((double)year+38)/128;
			if (a - Math.Floor(a) < 0.31)
				return true;
			return false;
		}
		//---------------------------------------------------------------------------------------------------
		public override bool IsLeapYear(int year, int era) 
		{
			return IsLeapYear(true, year, era);
		}
		//---------------------------------------------------------------------------------------------------
		private bool IsLeapYear(bool validate, int year, int era) 
		{
			CheckEraRange(validate, era);
			CheckYearRange(validate, year);
			if (HasLeapFrac(year) && !HasLeapFrac(year-1))
				return true;
			return false;
		}
		//---------------------------------------------------------------------------------------------------
		public override System.DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era) 
		{
			CheckEraRange(true, era);
			CheckDateRange(true, year, month, day);
			int days = day;
			for (int i=1; i<month; i++)
			{
				if (i<7) days += 31;
				else if (i<12) days += 30;
			}
			days += 365 * year + NumberOfLeapYearsUntil(false, year);
			DateTime timePart = new DateTime(1, 1, 1, hour, minute, second, millisecond);
			long ticks = days * 864000000000L + timePart.Ticks + 195721056000000000L;
			DateTime dateTime;
			try 
			{
				dateTime = new DateTime(ticks);
			}
			catch (System.ArgumentOutOfRangeException)
			{
				throw new System.ArgumentOutOfRangeException("month and/or day", "Specified time is not supported in this calendar. It should be between 12:00:00 AM, 1/01/0001 AP and 11:59:59 PM, 12/10/9378 AP, inclusive.");
			}
			return dateTime;
		}
		//---------------------------------------------------------------------------------------------------
		public override int ToFourDigitYear(int year)
		{
			if (year != 0)
			{
				try 
				{
					CheckYearRange(true, year);
				}
				catch (System.ArgumentOutOfRangeException)
				{
					throw new System.ArgumentOutOfRangeException("year", "Valid values for year to be converted are between 0 and 9378, inclusive.");
				}
			}
			if (year > 99) return year;
			int a = TwoDigitYearMax/100;
			if (year > TwoDigitYearMax-a*100) a--;
			return a*100+year;
		}
		//---------------------------------------------------------------------------------------------------
		public override int[] Eras 
		{
			get 
			{
				int[] eras = {1};
				return eras;
			}
		}
		//---------------------------------------------------------------------------------------------------
		private int twoDigitYearMax = 1409;
		public new int TwoDigitYearMax 
		{
			get 
			{
				return twoDigitYearMax;
			}
			set
			{
				if (value < 100 || 9378 < value)
					throw new System.ArgumentOutOfRangeException("value", "Valid values are between 100 and 9378, inclusive.");
				twoDigitYearMax = value;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public int GetCentury(System.DateTime time) 
		{
			return (GetYear(true, time) - 1)/100 + 1;
		}
		//---------------------------------------------------------------------------------------------------
		public int NumberOfLeapYearsUntil(int year)
		{
			return NumberOfLeapYearsUntil(true, year);
		}
		//---------------------------------------------------------------------------------------------------
		private int NumberOfLeapYearsUntil(bool validate, int year)
		{
			CheckYearRange(validate, year);
			int count = 0;
			for (int i=4; i<year; i++)
			{
				if (IsLeapYear(false, i, 0))
				{
					count++;
					i+=3;
				}
			}
			return count;
		}
		//---------------------------------------------------------------------------------------------------
		private void CheckEraRange(bool validate, int era)
		{
			if (validate)
			{
				if (era < 0 || 1 < era)
					throw new System.ArgumentOutOfRangeException("era", "Era value was not valid.");
			}
			return;
		}
		//---------------------------------------------------------------------------------------------------
		private void CheckYearRange(bool validate, int year) 
		{
			if (validate)
			{
				if (year < 1 || 9378 < year)
					throw new System.ArgumentOutOfRangeException("year", "Valid values for year are between 1 and 9378, inclusive.");
			}
			return;
		}
		//---------------------------------------------------------------------------------------------------
		private void CheckMonthRange(bool validate, int month)
		{
			if (validate)
			{
				if (month < 1 || 12 < month)
					throw new System.ArgumentOutOfRangeException("month", "Values for month must be between 1 and 12.");
			}
			return;
		}
		//---------------------------------------------------------------------------------------------------
		private void CheckDateRange(bool validate, int year, int month, int day)
		{
			if (validate)
			{
				int maxday = GetDaysInMonth(true, year, month, 0);
				if (day < 1 || maxday < day)
				{
					if (day == 30 && month == 12)
						throw new System.ArgumentOutOfRangeException("day", "Year "+year+" is not a leap year. Day must be at most 29 for month 12 of this year.");
					throw new System.ArgumentOutOfRangeException("day", "Day must be between 1 and "+maxday+" for month "+month+".");
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void CheckTicksRange(bool validate, DateTime time)
		{
			if (validate)
			{
				long ticks = time.Ticks;
				if (ticks < 196037280000000000L)
					throw new System.ArgumentOutOfRangeException("time", "Specified time is not supported in this calendar. It should be between 22/03/0622 12:00:00 AM and 31/12/9999 11:59:59 PM, inclusive.");
			}
			return;
		}
		//---------------------------------------------------------------------------------------------------
		private void GetYearAndRemainingDays(bool validate, DateTime time, out int year, out int days)
		{
			CheckTicksRange(validate, time);
			days = (time - new DateTime(196036416000000000L)).Days;
			year = 1;
			int daysInNextYear = 365;
			while (days > daysInNextYear)
			{
				days -= daysInNextYear;
				year++;
				daysInNextYear = GetDaysInYear(false, year, 0);
			}
			return;
		}
		//---------------------------------------------------------------------------------------------------
	}
}