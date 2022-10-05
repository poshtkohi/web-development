/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Collections;

using ServerManagement;
using services;
using services.blogbuilderv1;
using IranBlog.Classes.Security;

namespace AlirezaPoshtkoohiLibrary
{
	//--------------------------------------------------------------------------------------------------
	public class db : IDisposable
	{
		private string userAdmin;
		private string passAdmin;
		private string server;
		private string dbName;
		private string ConnectString;
		private Component component = new Component();
		private bool disposed = false;
		//--------------------------------
		public db(string server, string dbName,string userAdmin, string passAdmin)
		{
			this.server = server;
			this.dbName = dbName;
			this.userAdmin = userAdmin;
			this.passAdmin = passAdmin;
			this.ConnectString="database=" + this.dbName + ";server=" + this.server +
				";User Id=" + this.userAdmin + ";Password=" + this.passAdmin +";Connect Timeout=30;";
		}
		//--------------------------------
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		//--------------------------------
		private void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					component.Dispose();
				} 
			}
			disposed = true;         
		}
		//--------------------------------
		~db()      
		{
			Dispose(false);
		}
		//--------------------------------
		private string mark(string input)
		{
			return "'" + input +"'";
		}
		//--------------------------------
		public int test()
		{
			string query = "SELECT COUNT(*) FROM weblogs WHERE username='t1'";
			SqlConnection connection = new SqlConnection(this.ConnectString);
			SqlCommand command = new SqlCommand(query, connection);
			connection.Open();
			return (int) command.ExecuteScalar();
		}
		//--------------------------------
		public int del()
		{
			string query = "SELECT subdomain FROM " + constants.SQLUsersInformationTableName;
			SqlConnection connection = new SqlConnection(this.ConnectString);
			SqlCommand command = new SqlCommand(query, connection);
			connection.Open();
			SqlDataReader read = command.ExecuteReader();
			string subdomain = "";
			IIS iis = new IIS(constants.password);
			DNS dns = new DNS(constants.password);
			if(read.Read())
			{
				subdomain = read.GetString(0);
				dns.DeleteRecord(constants.DNSServerAddress, constants.ZoneName, subdomain);
				iis.DeleteWebSite(subdomain, constants.ZoneName);
			}
			read.Close();
			connection.Close();
			command.Dispose();
			//this.ExecuteNonQuery("DROP TABLE " + constants.SQLWeblogsTableName);
			this.ExecuteNonQuery("DROP TABLE " + constants.SQLLatestUpdatedWeblogsTableName);
			this.ExecuteNonQuery("DROP TABLE " + constants.SQLCommentsTableName);
			this.ExecuteNonQuery("DROP TABLE " + constants.SQLUsersInformationTableName);
			return 0;
		}
		//--------------------------------
		//============================ this section is for editions of the last posts ==========================
		public int FirstLoadLastPostEdit(Int64 BlogID, DropDownList ListBox, TextBox subject, TextBox content, Label date, TextBox  hidden)
		{
			try
			{                                        /*   0      1      2    */
				string query = String.Format("SELECT TOP 30 date,subject,content FROM {0} WHERE BlogID={1} ORDER BY date DESC", constants.SQLPostsTableName, BlogID);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();
				SqlDataReader read = command.ExecuteReader();
				if(read.HasRows)
				{
					DateTime dt;
					string val = "";
					PersianCalendar pcal = new PersianCalendar();
					bool first = true;
					while(read.Read())
					{
						dt = read.GetDateTime(0);
						val = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", dt.Year,dt.Month,dt.Day,
							dt.Hour,dt.Minute,dt.Second,dt.Millisecond);
						ListBox.Items.Add(new ListItem(string.Format("[ {0}/{1}/{2} ] ------------------------- [ {3}:{4}:{5} ]",pcal.GetYear(dt),
							pcal.GetMonth(dt),pcal.GetDayOfMonth(dt),pcal.GetHour(dt),pcal.GetMinute(dt),pcal.GetSecond(dt)), val));
						if(first)
						{
							subject.Text = read.GetString(1);
							content.Text = read.GetString(2);
							date.Text = string.Format("{0}/{1}/{2} ----- {3}:{4}:{5} ",pcal.GetYear(dt),
							pcal.GetMonth(dt),pcal.GetDayOfMonth(dt),pcal.GetHour(dt),pcal.GetMinute(dt),pcal.GetSecond(dt));
							hidden.Text = val;
						}
						first = false;
					}
				}
				else
				{
					read.Close();
					connection.Close();
					command.Dispose();
					return -1;
				}
				read.Close();
				connection.Close();
				command.Dispose();
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public int LoadLastPostEdit(Int64 BlogID, string ListBox, TextBox subject, TextBox content, Label date/*, Label  hidden*/)
		{
			try
			{
				Regex r = new Regex("(-)");
				string[] s = r.Split(ListBox);
			    int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0, milsecond = 0;
				for(int i = 0 ; i < s.Length; i+=2)
				{
					if(i == 0)
						year = Convert.ToInt32(s[i]);
					if(i == 2)
						month = Convert.ToInt32(s[i]);
					if(i == 4)
						day = Convert.ToInt32(s[i]);
					if(i == 6)
						hour = Convert.ToInt32(s[i]);
					if(i == 8)
						minute = Convert.ToInt32(s[i]);
					if(i == 10)
						second = Convert.ToInt32(s[i]);
					if(i == 12)
						milsecond = Convert.ToInt32(s[i]);
				}
				DateTime dt = new DateTime(year, month, day, hour, minute, second, milsecond);
				                                    /*   0      1   */
				string query = string.Format("SELECT subject,content FROM {0} WHERE BlogID={1} AND date=@date", constants.SQLPostsTableName, BlogID);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
				DateParam.Value = dt;
				command.Parameters.Add(DateParam); 
				connection.Open();
				SqlDataReader read = command.ExecuteReader();
				if(read.Read())
				{
					PersianCalendar pcal = new PersianCalendar();
					subject.Text = read.GetString(0);
					content.Text = read.GetString(1);
					date.Text = string.Format("{0}/{1}/{2} ----- {3}:{4}:{5} ",pcal.GetYear(dt),
						pcal.GetMonth(dt),pcal.GetDayOfMonth(dt),pcal.GetHour(dt),pcal.GetMinute(dt),pcal.GetSecond(dt));
					//hidden.Text = ListBox;
				}
				read.Close();
				connection.Close();
				command.Dispose();
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public int SaveLastPostEdit(Int64 BlogID, string subject, string content, string hidden)
		{
			try
			{
				Regex r = new Regex("(-)");
				string[] s = r.Split(hidden);
				int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0, milsecond = 0;
				for(int i = 0 ; i < s.Length; i+=2)
				{
					if(i == 0)
						year = Convert.ToInt32(s[i]);
					if(i == 2)
						month = Convert.ToInt32(s[i]);
					if(i == 4)
						day = Convert.ToInt32(s[i]);
					if(i == 6)
						hour = Convert.ToInt32(s[i]);
					if(i == 8)
						minute = Convert.ToInt32(s[i]);
					if(i == 10)
						second = Convert.ToInt32(s[i]);
					if(i == 12)
						milsecond = Convert.ToInt32(s[i]);
				}
				DateTime dt = new DateTime(year, month, day, hour, minute, second, milsecond);
				string query = String.Format("UPDATE {0} SET subject=@subject,content=@content WHERE BlogID={1} AND date=@date", constants.SQLPostsTableName, BlogID);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
				SqlParameter SubjectParam = new SqlParameter("@subject", SqlDbType.NVarChar);
				SqlParameter ContentParam = new SqlParameter("@content", SqlDbType.NVarChar);
				DateParam.Value = dt;
				SubjectParam.Value = subject;
				ContentParam.Value = content;
				command.Parameters.Add(DateParam);
				command.Parameters.Add(SubjectParam);
				command.Parameters.Add(ContentParam); 
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
				command.Dispose();
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public int DeleteLastPostEdit(string subdomain, Int64 BlogID, string hidden)
		{
			/*try
			{*/
				Regex r = new Regex("(-)");
				string[] s = r.Split(hidden);
				int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0, milsecond = 0;
				for(int i = 0 ; i < s.Length; i+=2)
				{
					if(i == 0)
						year = Convert.ToInt32(s[i]);
					if(i == 2)
						month = Convert.ToInt32(s[i]);
					if(i == 4)
						day = Convert.ToInt32(s[i]);
					if(i == 6)
						hour = Convert.ToInt32(s[i]);
					if(i == 8)
						minute = Convert.ToInt32(s[i]);
					if(i == 10)
						second = Convert.ToInt32(s[i]);
					if(i == 12)
						milsecond = Convert.ToInt32(s[i]);
				}
				DateTime dt = new DateTime(year, month, day, hour, minute, second, milsecond);
				DeleteFromWeblogsTable(BlogID, dt);
				string query = String.Format("DELETE FROM {0} WHERE BlogID={1} AND blogDate=@date", constants.SQLCommentsTableName, BlogID);
				query += String.Format(";DELETE FROM {0} WHERE subdomain={1} AND date=@date", constants.SQLLatestUpdatedWeblogsTableName, subdomain);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
				DateParam.Value = dt;
				command.Parameters.Add(DateParam);
				connection.Open();
				int result = command.ExecuteNonQuery();
				connection.Close();
				command.Dispose();
				return 0;
			/*}
			catch
			{
				return -3;
			}*/
		}
		private void DeleteFromWeblogsTable(Int64 BlogID, DateTime date)
		{
			string query = String.Format("DELETE FROM {0} WHERE BlogID={1} AND date=@date", constants.SQLPostsTableName, BlogID);
			db d = new db(constants.SQLServerAddress, constants.SQLWeblogsDbName,
				constants.SQLWeblogsDbUsername, constants.SQLWeblogsDbPassowrd);
			SqlConnection connection = new SqlConnection(d.ConnectString);
			SqlCommand command = new SqlCommand(query, connection);
			SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
			DateParam.Value = date;
			command.Parameters.Add(DateParam);
			connection.Open();
			command.ExecuteNonQuery();
			command.Dispose();
			d.Dispose();
			return ;
		}
		//======================================================================================================
		//--------------------------------
		public int TemplateChangePage(string username,string TableName, int TemplateNumber)
		{
			try
			{
				string query = "UPDATE " + TableName + " SET template=@template WHERE username=@username";
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				SqlParameter TemplateParam = new SqlParameter("@template", SqlDbType.Int);
				SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
				TemplateParam.Value = TemplateNumber;
				UsernameParam.Value = username;
				command.Parameters.Add(TemplateParam); 
				command.Parameters.Add(UsernameParam);
				connection.Open();
				int result = command.ExecuteNonQuery();
				command.Dispose();
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public SettingsPage SettingsPageLoad(Int64 BlogID)
		{
			try
			{
				SqlConnection connection = new SqlConnection(this.ConnectString);
                SqlCommand command = connection.CreateCommand();
                command.CommandText = String.Format("SELECT title,about,emailEnable,category,MaxPostShow,ArciveDisplayMode,ImageGuid FROM {0} WHERE i={1}", constants.SQLUsersInformationTableName, BlogID);
				connection.Open();
				SettingsPage infos = null;
				SqlDataReader read = command.ExecuteReader();
                if (read.Read())
                    infos = new SettingsPage((string)read["title"], (string)read["about"], (bool)read["emailEnable"], (string)read["category"], (int)read["MaxPostShow"], (bool)read["ArciveDisplayMode"], (string)read["ImageGuid"]);
				read.Close();
				command.Dispose();
				return infos;
			}
			catch
			{
				return null;
			}
		}
		//--------------------------------
		public int SettingsPageSave(SettingsPage infos, Int64 BlogID)
		{
			try
			{
				SqlConnection connection = new SqlConnection(this.ConnectString);
                SqlCommand command = connection.CreateCommand();
                command.CommandText = String.Format("UPDATE {0} SET title=@title,about=@about,emailEnable=@emailEnable,category=@category,MaxPostShow=@MaxPostShow,ArciveDisplayMode=@ArciveDisplayMode,ImageGuid=@ImageGuid WHERE i={1}", constants.SQLUsersInformationTableName, BlogID);

				SqlParameter TitleParam = new SqlParameter("@title", SqlDbType.NVarChar);
				SqlParameter AboutParam = new SqlParameter("@about", SqlDbType.NVarChar);
				SqlParameter EmailEnableParam = new SqlParameter("@emailEnable", SqlDbType.Bit);
				SqlParameter CategoryParam = new SqlParameter("@category", SqlDbType.VarChar);
                SqlParameter MaxPostShowParam = new SqlParameter("@MaxPostShow", SqlDbType.Int);
                SqlParameter ArciveDisplayModeParam = new SqlParameter("@ArciveDisplayMode", SqlDbType.Bit);
                SqlParameter ImageGuidParam = new SqlParameter("@ImageGuid", SqlDbType.VarChar);

				TitleParam.Value = infos.Title;
				AboutParam.Value = infos.About;
				EmailEnableParam.Value = infos.EmailEnable;
				CategoryParam.Value = infos.category;
                MaxPostShowParam.Value = infos.MaxPostShow;
                ArciveDisplayModeParam.Value = infos.ArciveDisplayMode;
                ImageGuidParam.Value = infos.ImageGuid;

				command.Parameters.Add(TitleParam); 
				command.Parameters.Add(AboutParam);
				command.Parameters.Add(EmailEnableParam); 
				command.Parameters.Add(CategoryParam);
                command.Parameters.Add(MaxPostShowParam);
                command.Parameters.Add(ArciveDisplayModeParam);
                command.Parameters.Add(ImageGuidParam);

				connection.Open();
				command.ExecuteNonQuery();
				command.Dispose();
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public FormInfo UpdatePageLoad(string username)
		{               
			try
			{
				                      /*   0           1      2      3    4   5       */
				string query = "SELECT first_name,last_name,email,gender,age,job FROM " +
					constants.SQLUsersInformationTableName + " WHERE username=" + this.mark(username);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();
				FormInfo infos = null;
				SqlDataReader read = command.ExecuteReader();
				if(read.Read())
				{
					infos = new FormInfo(username, "", read.GetString(2), read.GetString(0),
						read.GetString(1), read.GetString(3),
						read.GetString(5), read.GetString(4), 10, true);
				}
				read.Close();
				command.Dispose();
				return infos;
			}
			catch
			{
				return null;
			}
		}
		//--------------------------------
		public int UpadetePageSave(FormInfo infos, string LastPassword)
		{      /* returns: -1=wrong or unavailable password 0=suucess -3=exception*/
			try
			{
				string query = "SELECT password FROM " + constants.SQLUsersInformationTableName +
					" WHERE username=" + this.mark(infos.Username);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();
				SqlDataReader read = command.ExecuteReader();
				if(read.HasRows)
				{
					read.Read();
					string lastPassword = read.GetString(0); 
					read.Close();
					connection.Close();
					LastPassword = LastPassword.Trim();
					if(LastPassword != null && LastPassword != "")
					{
						if(LastPassword != lastPassword)
						{
							command.Dispose();
							return -1;
						}
					}
					query = "UPDATE " + constants.SQLUsersInformationTableName + " SET password=@password,first_name=@first_name,"+
						"last_name=@last_name,email=@email,age=@age,gender=@gender,job=@job WHERE username=@username";
					command.CommandText = query;
					SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
					SqlParameter FirstNameParam = new SqlParameter("@first_name", SqlDbType.NVarChar);
					SqlParameter LastNameParam = new SqlParameter("@last_name", SqlDbType.NVarChar);
					SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.VarChar);
					SqlParameter AgeParam = new SqlParameter("@age", SqlDbType.VarChar);
					SqlParameter GenderParam = new SqlParameter("@gender", SqlDbType.VarChar);
					SqlParameter JobParam = new SqlParameter("@job", SqlDbType.VarChar);
					SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
					if(LastPassword != null && LastPassword != "")
					{
						PasswordParam.Value = infos.Password;
						Email e = new Email(infos.Email, constants.IranBlogEmailManager);
						e.RegisterAccount(infos.Username, infos.Password);
					}
					else
					{
						PasswordParam.Value = lastPassword;
					}
					FirstNameParam.Value = infos.FirstName;
					LastNameParam.Value = infos.LastName;
					EmailParam.Value = infos.Email;
					AgeParam.Value = infos.Age;
					GenderParam.Value = infos.Gender;
					JobParam.Value= infos.Job;
					UsernameParam.Value= infos.Username;
					command.Parameters.Add(PasswordParam); 
					command.Parameters.Add(FirstNameParam); 
					command.Parameters.Add(LastNameParam);
					command.Parameters.Add(EmailParam); 
					command.Parameters.Add(AgeParam);
					command.Parameters.Add(GenderParam); 
					command.Parameters.Add(JobParam);
					command.Parameters.Add(UsernameParam); 
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
					command.Dispose();
					return 0;
				}
				else
				{
					read.Close();
					connection.Close();
					command.Dispose();
					return -1;
				}
			}
			catch
			{
				return -3;
			}
		}
		public static void Login(Page page)
		{
			if(page.Session["username"] != null && ((string)page.Session["username"]).Trim() == "")
			{
				if(page.Request.Form["username"]!= null && page.Request.Form["password"] != null && page.Request.Form["login"] == "login")
				{
					db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName, 
						constants.SQLUsername, constants.SQLPassowrd);
					int result = d.LoginPage(page.Request.Form["username"], page.Request.Form["password"], page);
					d.Dispose();
					if(result == -3)
					{
						page.Session.Abandon();
						page.Response.Redirect("exception.aspx?error=SQL Server");
						return ;
					}
					if(result == -1)
					{
						page.Session.Abandon();
						page.Response.Redirect("/services/?i=unauthorized");
						return ;
					}
					if(result == 1)
					{
						d.Dispose();
						page.Response.Redirect("wizard.aspx");
						return ;
					}
					if(result == 0)
					{
						page.Response.Redirect("/services/blogbuilderv1/home.aspx");
						return ;
					}
				}
				else
				{
					page.Session.Abandon();
					page.Response.Redirect("/services/");
					return ;
				}
			}
		}
		//--------------------------------
		public int LoginPage(string username, string password, Page page)
		{              //returns: 0=success  -3=internal exception 1=no avilable subdomain -1=not registered
			try
			{
                string query = "SELECT subdomain,i,ChatBoxIsEnabled FROM " + constants.SQLUsersInformationTableName + " WHERE " +
					           "username=@username AND password=@password";
                SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
				SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
				UsernameParam.Value = username.Trim().ToLower();
				PasswordParam.Value = password.Trim().ToLower();
				command.Parameters.Add(UsernameParam);
				command.Parameters.Add(PasswordParam);
				connection.Open();
				SqlDataReader read = command.ExecuteReader();
				int result = 0;
				if(read.HasRows)
				{
					read.Read();
                    SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)page.Session["SigninSessionInfo"];
                    _SigninSessionInfo.ChatBoxIsEnabled = (bool)read["ChatBoxIsEnabled"];
					if(read.IsDBNull(0))
					{
                        _SigninSessionInfo.Username = username;
                        _SigninSessionInfo.Subdomain = "";
						page.Session["FirstCome"] = true;
						result = 1;
					}
					else
					{
						string subdomain = read.GetString(0);
						if(subdomain == null || subdomain == "")
						{
                            _SigninSessionInfo.Username = username;
                            _SigninSessionInfo.Subdomain = "";
							page.Session["FirstCome"] = true;
							result = 1;
						}
						else
						{
                            _SigninSessionInfo.Username = username;
                            _SigninSessionInfo.Subdomain = subdomain;
							result = 0;
						}
					}
					Int64 id = read.GetInt64(1);
                    _SigninSessionInfo.BlogID = id;

                    page.Session["SigninSessionInfo"] = _SigninSessionInfo;
				}
				else
				{
					result = -1;
				}
				read.Close();
				connection.Close();
				command.Dispose();
				return result;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public int RegisterSubdomainPage(SubdomainInfoPage infos, string TableName)/* returned -2 meaning available the requested subdomain*/
		{                                                                      /* returned -3 meaning happened an exception */
			try
			{
				string query = "SELECT * FROM " + TableName + " WHERE subdomain=" + this.mark(infos.Subdomain);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				connection.Open();
				SqlCommand command = new SqlCommand(query, connection);
				SqlDataReader read = command.ExecuteReader();
				if(read.Read())
				{
					read.Close();
					connection.Close();
					return -2;
				}
				else
				{
					read.Close();
					connection.Close();
					query="UPDATE " + TableName  + " SET subdomain=@subdomain, title=@title, category=@category" +
						" WHERE username=" + this.mark(infos.Username);
					SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
					SqlParameter TitleParam = new SqlParameter("@title", SqlDbType.NVarChar);
					SqlParameter CategoryParam = new SqlParameter("@category", SqlDbType.VarChar);
					SubdomainParam.Value = infos.Subdomain; 
					TitleParam.Value = infos.Title;
					CategoryParam.Value = infos.Category;
					command.CommandText = query;
					command.Parameters.Add(SubdomainParam);
					command.Parameters.Add(TitleParam);
					command.Parameters.Add(CategoryParam);
					connection.Open();
					int result = command.ExecuteNonQuery();
					connection.Close();
					command.Dispose();
					return 0;
				}
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public int PostPage(PostTextBlogPage infos, Int64 BlogID, System.Web.UI.Page page)/* returned -2 meaning available the requested subdomain*/
		{                                               /* returned -3 meaning happened an exception */
			/*try
			{*/
				UpdateWeblogTable(infos, BlogID, page);
				string query = String.Format("SELECT title FROM {0} WHERE i={1}", constants.SQLUsersInformationTableName, BlogID);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
				SqlParameter SubjectParam = new SqlParameter("@subject", SqlDbType.NVarChar);
			    SqlParameter ContentParam = new SqlParameter("@content", SqlDbType.NText);
				DateParam.Value = infos.Date;
				SubjectParam.Value = infos.Subject;
			    ContentParam.Value = infos.Data;
				command.Parameters.Add(DateParam);
				command.Parameters.Add(SubjectParam);
			    command.Parameters.Add(ContentParam);
				connection.Open();
				int result ;//= command.ExecuteNonQuery();
				SqlDataReader read = command.ExecuteReader();
				string title = null;
				if(read.Read())
				{
					title = read.GetString(0);
				}
				read.Close();
				connection.Close();
                /*if (title != "")
                {
                    //-------UpdatedTable Algorithm------------------
                    query = "INSERT INTO " + constants.SQLLatestUpdatedWeblogsTableName + " (date,subdomain,subject)" +
                        " VALUES(@date, @subdomain, @subject)";
                    command = new SqlCommand(query, connection);
                    DateParam = new SqlParameter("@date", SqlDbType.DateTime);
                    SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
                    SubjectParam = new SqlParameter("@subject", SqlDbType.NVarChar);
                    DateParam.Value = infos.Date;
                    SubdomainParam.Value = infos.Subdomain;
                    SubjectParam.Value = title;
                    command.Parameters.Add(DateParam);
                    command.Parameters.Add(SubdomainParam);
                    command.Parameters.Add(SubjectParam);
                    connection.Open();
                    result = command.ExecuteNonQuery();
                    connection.Close();
                    command.Dispose();
                    //-----------------------------------------------
                }*/
				if(title != null)
				{
					//-------UpdatedTable Algorithm------------------
					query = "SELECT subdomain FROM " + constants.SQLLatestUpdatedWeblogsTableName +
						" WHERE subdomain=" + mark(infos.Subdomain);
					command.CommandText = query;
					connection.Open();
					read = command.ExecuteReader();
					if(read.HasRows)
					{
						read.Close();
						connection.Close();
						query = "DELETE FROM " + constants.SQLLatestUpdatedWeblogsTableName +
							" WHERE subdomain=" + mark(infos.Subdomain);
						ExecuteNonQuery(query);
					}
					read.Close();
					connection.Close();
					command.CommandText = "SELECT subdomain FROM " + constants.SQLLatestUpdatedWeblogsTableName + " ORDER BY date DESC";
					connection.Open();
					read = command.ExecuteReader();
					string subdomain = "";
					int i = 1;
					while(read.Read())
					{
						subdomain = read.GetString(0);
                        if (i == constants.SQLMaxUpdatedWeblogsTotal)
						{
							read.Close();
							connection.Close();
							query = "DELETE FROM " + constants.SQLLatestUpdatedWeblogsTableName +
								" WHERE subdomain=" + mark(subdomain);
							result = ExecuteNonQuery(query);
							break;
						}
						i++;
					}
					read.Close();
					connection.Close();
					command.Dispose();
					query = "INSERT INTO " + constants.SQLLatestUpdatedWeblogsTableName + " (date,subdomain,subject)" +
						" VALUES(@date, @subdomain, @subject)";
					command = new SqlCommand(query, connection);
					DateParam = new SqlParameter("@date", SqlDbType.DateTime);
					SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
					SubjectParam = new SqlParameter("@subject", SqlDbType.NVarChar);
					DateParam.Value = infos.Date;
					SubdomainParam.Value = infos.Subdomain;
					SubjectParam.Value = title;
					command.Parameters.Add(DateParam);
					command.Parameters.Add(SubdomainParam);
					command.Parameters.Add(SubjectParam);
					connection.Open();
					result = command.ExecuteNonQuery();
					connection.Close();
					command.Dispose();
					//-----------------------------------------------
				}
				return 0;
			/*}
			catch
			{
				return -3;
			}*/
		}
		private void UpdateWeblogTable(PostTextBlogPage infos, Int64 BlogID, System.Web.UI.Page page)
		{
			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CategoryToAuthor_PostPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = BlogID;

            command.Parameters.Add("@CategoryID", SqlDbType.Int);
            command.Parameters["@CategoryID"].Value = infos.SubjectedArchiveID;

            command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
            command.Parameters["@AuthorID"].Value = (Int64)page.Session["AuthorID"];

            command.ExecuteNonQuery();
            command.Dispose();

			/*command.CommandText = String.Format("UPDATE {0} SET PostNum=PostNum+1 WHERE i={1};", constants.SQLUsersInformationTableName, BlogID);
            command.ExecuteNonQuery();

            command.CommandText = String.Format("UPDATE {0} SET PostNum=PostNum+1 WHERE id={1} AND BlogID={2};", constants.SQLSubjectedArchiveTableName, infos.SubjectedArchiveID, BlogID);
            command.ExecuteNonQuery();

            command.CommandText = String.Format("UPDATE {0} SET PostNum=PostNum+1 WHERE id={1} AND BlogID={2};", constants.SQLTeamWeblogName, (Int64)page.Session["AuthorID"], BlogID);
            command.ExecuteNonQuery();*/


            connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            command = connection.CreateCommand();
            command.Connection = connection;
            if (infos.ContinuedText == "")
                command.CommandText = String.Format("INSERT INTO {0} (BlogID,date,subject,content,NumComments,CategoryID,AuthorID,IsShowCommentsPreVerify) VALUES(@BlogID,@date,@subject,@content,@NumComments,@CategoryID,@AuthorID,@IsShowCommentsPreVerify)", constants.SQLPostsTableName);
            else
            {
                command.CommandText = String.Format("INSERT INTO {0} (BlogID,date,subject,content,continued,NumComments,CategoryID,AuthorID,IsShowCommentsPreVerify) VALUES(@BlogID,@date,@subject,@content,@continued,@NumComments,@CategoryID,@AuthorID,@IsShowCommentsPreVerify)", constants.SQLPostsTableName);
                SqlParameter ContinuedParam = new SqlParameter("@continued", SqlDbType.NText);
                ContinuedParam.Value = infos.ContinuedText;
                command.Parameters.Add(ContinuedParam);
            }

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
			SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
			SqlParameter SubjectParam = new SqlParameter("@subject", SqlDbType.NVarChar);
			SqlParameter ContentParam = new SqlParameter("@content", SqlDbType.NText);
            SqlParameter NumCommentsParam = new SqlParameter("@NumComments", SqlDbType.Int);
            SqlParameter CategoryIDParam = new SqlParameter("@CategoryID", SqlDbType.Int);
            SqlParameter AuthorIDParam = new SqlParameter("@AuthorID", SqlDbType.BigInt);
            SqlParameter IsShowCommentsPreVerifyParam = new SqlParameter("@IsShowCommentsPreVerify", SqlDbType.Bit);

            BlogIDParam.Value = BlogID;
			DateParam.Value = infos.Date;
			SubjectParam.Value = infos.Subject;
			ContentParam.Value = infos.Data;
            if (!infos.CommentEnabled)
                NumCommentsParam.Value = -1;
            else
                NumCommentsParam.Value = 0;
            CategoryIDParam.Value = infos.SubjectedArchiveID;
            AuthorIDParam.Value = (Int64)page.Session["AuthorID"];
            IsShowCommentsPreVerifyParam.Value = infos.IsShowCommentsPreVerify;


            command.Parameters.Add(BlogIDParam);
			command.Parameters.Add(DateParam);
			command.Parameters.Add(SubjectParam);
			command.Parameters.Add(ContentParam);
            command.Parameters.Add(NumCommentsParam);
            command.Parameters.Add(CategoryIDParam);
            command.Parameters.Add(AuthorIDParam);
            command.Parameters.Add(IsShowCommentsPreVerifyParam);

			command.ExecuteNonQuery();
			command.Dispose();

			return ;
		}
		//--------------------------------
		public int ForgottenPasswordPage(string username, string email)
		{
			try
			{
				string query = "SELECT username FROM " + constants.SQLUsersInformationTableName + " WHERE " +
					"username=" + this.mark(username) + " AND email=" + this.mark(email);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				connection.Open();
				SqlCommand command = new SqlCommand(query, connection);
				SqlDataReader read = command.ExecuteReader();
				if(read.HasRows)
				{
					read.Close();
					connection.Close();
					command.Dispose();
					string pass = 
						((string)(DateTime.Now.Millisecond + request.RandomText() + DateTime.Now.Second)).ToLower();
					query = "UPDATE " + constants.SQLUsersInformationTableName + " SET password=@pass WHERE username=" + this.mark(username);
					connection = new SqlConnection(this.ConnectString);
					command = new SqlCommand(query, connection);
					SqlParameter PassParam = new SqlParameter("@pass", SqlDbType.NVarChar);
					PassParam.Value = pass;
					command.Parameters.Add(PassParam);
					connection.Open();
					int result = command.ExecuteNonQuery();
					connection.Close();
					command.Dispose();
					Email e = new Email(email, constants.IranBlogWelcomeEmail);
					result = e.ForgottenPassword(username, pass);
					if(result == -3)
					{
						return -2;
					}
					else
					{
						return 0;
					}
				}
				else
				{
					read.Close();
					connection.Close();
					return 0;
				}
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
        public int RegisterPage(FormInfo infos, SubdomainInfoPage infoss, string TableName, Page page)/* returned -2 meaning available the requested username*/
		{                                                        /* returned -3 meaning happened an exception */
			/*try
			{*/
				string query = "SELECT username FROM " + TableName + " WHERE username=" + this.mark(infos.Username);
				SqlConnection connection = new SqlConnection(this.ConnectString);
				connection.Open();
				SqlCommand command = new SqlCommand(query, connection);
				SqlDataReader read = command.ExecuteReader();
				if(read.HasRows)
				{
					read.Close();
					connection.Close();
					command.Dispose();
					return -2;
				}
                read.Close();
                connection.Close();
                query = "SELECT subdomain FROM " + TableName + " WHERE subdomain=" + this.mark(infoss.Subdomain);
                connection = new SqlConnection(this.ConnectString);
                connection.Open();
                command = new SqlCommand(query, connection);
                read = command.ExecuteReader();
                if (read.HasRows)
                {
                    read.Close();
                    connection.Close();
                    command.Dispose();
                    return -4;
                }
				else
                {
					read.Close();
					connection.Close();
                    query = "INSERT INTO " + TableName + " (username,password,email,first_name,last_name,gender,job,age,dateRegister,about,f,subdomain,title,category)" +
                        " VALUES(@username,@password,@email,@first_name,@last_name,@gender,@job,@age,@dateRegister,@about,@f,@subdomain,@title,@category);" +
                        String.Format("SELECT i FROM {0} WHERE username='{1}';", TableName, infos.Username);//+
					SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
					SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
					SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.VarChar);
					SqlParameter FirstNameParam = new SqlParameter("@first_name", SqlDbType.NVarChar);
					SqlParameter LastNameParam = new SqlParameter("@last_name", SqlDbType.NVarChar);
					SqlParameter GenderParam = new SqlParameter("@gender", SqlDbType.VarChar);
					SqlParameter JobParam = new SqlParameter("@job", SqlDbType.VarChar);
					SqlParameter AgeParam = new SqlParameter("@age", SqlDbType.VarChar);
					SqlParameter DateParam = new SqlParameter("@dateRegister", SqlDbType.DateTime);
					SqlParameter FParam = new SqlParameter("@f", SqlDbType.NVarChar);
                    SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
                    SqlParameter TitleParam = new SqlParameter("@title", SqlDbType.NVarChar);
                    SqlParameter CategoryParam = new SqlParameter("@category", SqlDbType.VarChar);
					//----------------
					SqlParameter AboutParam = new SqlParameter("@about", SqlDbType.NVarChar);
					AboutParam.Value = ".در این قسمت هیچ رکوردی وجود ندارد";
					command.Parameters.Add(AboutParam);
					//----------------
					UsernameParam.Value = infos.Username; 
					PasswordParam.Value = infos.Password;
					EmailParam.Value = infos.Email; 
					FirstNameParam.Value = infos.FirstName;
					LastNameParam.Value = infos.LastName;
					GenderParam.Value = infos.Gender; 
					JobParam.Value = infos.Job;
					AgeParam.Value = infos.Age; 
					DateParam.Value = infos.DateRegister;
					FParam.Value = "y";
                    SubdomainParam.Value = infoss.Subdomain;
                    TitleParam.Value = infoss.Title;
                    CategoryParam.Value = infoss.Category;
                    

					command.CommandText = query;
					command.Parameters.Add(UsernameParam); 
					command.Parameters.Add(PasswordParam);
					command.Parameters.Add(EmailParam); 
					command.Parameters.Add(FirstNameParam); 
					command.Parameters.Add(LastNameParam);
					command.Parameters.Add(GenderParam); 
					command.Parameters.Add(JobParam);
					command.Parameters.Add(AgeParam); 
					command.Parameters.Add(DateParam);
					command.Parameters.Add(FParam);
                    command.Parameters.Add(SubdomainParam);
                    command.Parameters.Add(TitleParam);
                    command.Parameters.Add(CategoryParam);
					connection.Open();
					read = command.ExecuteReader();
                    if (read.Read())
                    {
                        SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)page.Session["SigninSessionInfo"];
                        _SigninSessionInfo.BlogID = (Int64)read.GetInt64(0);
                        page.Session["SigninSessionInfo"] = _SigninSessionInfo;
                    }
					int result = 1;
					read.Close();
					connection.Close();
					command.Dispose();
					Email e = new Email(infos.Email, constants.IranBlogWelcomeEmail);
					e.RegisterAccount(infos.Username, infos.Password);
					return result;
				}
			/*}
			catch
			{
				return -3;
			}*/
		}
		//--------------------------------
		public int DeleteAccount(string subdomain)
		{
			try
			{
				IIS iis = new IIS(constants.password);
				iis.DeleteWebSite(subdomain, constants.ZoneName);
				iis.DeleteWebSite("www." + subdomain, constants.ZoneName);
				DNS dns = new DNS(constants.password);
				dns.DeleteRecord(constants.DNSServerAddress, constants.ZoneName, subdomain);
				dns.DeleteRecord(constants.DNSServerAddress, constants.ZoneName, "www." + subdomain);
				iis.Dispose();
				dns.Dispose();
				if(Directory.Exists(constants.RootDircetoryWeblogs + "\\" + subdomain))
					Directory.Delete(constants.RootDircetoryWeblogs + "\\" + subdomain, true);
				string query1 = "DELETE FROM " + constants.SQLUsersInformationTableName + " WHERE subdomain='" + subdomain + "'";
				//string query2 = "DROP TABLE " + subdomain; // must implement for "posts" database
				//string query3 = "DELETE FROM " + constants.SQLCommentsTableName + " WHERE subdomain='" + subdomain + "'";
				//string query4 = "DELETE FROM " + constants.SQLLatestUpdatedWeblogsTableName + " WHERE subdomain='" + subdomain + "'";
                query1 += ";DELETE FROM " + constants.SQLLatestUpdatedWeblogsTableName + " WHERE subdomain='" + subdomain + "'";
				ExecuteNonQuery(query1);
				//ExecuteNonQuery(query3);
				//ExecuteNonQuery(query4);
				/*db d = new db(constants.SQLServerAddress, constants.SQLWeblogsDbName,
					constants.SQLWeblogsDbUsername, constants.SQLWeblogsDbPassowrd);
				d.ExecuteNonQuery(query2);
				d.Dispose();*/
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		private void LatestUpdated(Page page)
		{
            //page.Response.Write("<iframe src=\"/services/error.html\" width=\"100%\" height=\"300\" scrolling=\"yes\" marginwidth=\"1\" marginheight=\"1\" border=\"1\" frameborder=\"0\" style=\"border: 1px solid #316D94;\"></iframe>");
            try
            {
                string query = "SELECT TOP 20 updated.subdomain,subject FROM updated,usersInfo WHERE subject NOT LIKE N'%ف%ی%ل%ت%ر%' AND subject NOT LIKE N'%پشت%' AND subject NOT LIKE N'%س%ک%س%' AND subject NOT LIKE N'%کیر%' AND subject NOT LIKE N'%کوس%' AND subject NOT LIKE N'%sex%' AND subject NOT LIKE N'%kir%' AND subject NOT LIKE N'%kos%' AND usersInfo.subdomain=updated.subdomain AND usersInfo.f='y' ORDER BY updated.i DESC";
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
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
		//--------------------------------
		public static void UpdatedRecords(Page page)
		{
			page.Session.Abandon();
			db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
				constants.SQLUsername, constants.SQLPassowrd);
			d.LatestUpdated(page);
			d.Dispose();
			return ;
		}
		//--------------------------------
		private void ListUsersPage(Page page)
		{
			try
			{
                string query = "SELECT category FROM " + constants.SQLUsersInformationTableName + " WHERE IsWeblogExpired=0 AND f='y'";
				int LEARNING=0,NEWS=0,LITERATURE=0,INTERNET=0,MEDICAL=0,TRADE=0,MEMORY=0,
					NEWSPAPER=0,LIFE=0,CINEMA=0,PERSONAL=0,NATURE=0,SATIRE=0,EMOTIONAL=0,GENERAL=0,
					PHILOSOPHY=0,COMPUTER=0,JOKE=0,RELIGION=0,MUSIC=0,AUTHORING=0,ART=0,SPORT=0;
				SqlConnection connection = new SqlConnection(this.ConnectString);
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();
				SqlDataReader read = command.ExecuteReader();
				if(read.HasRows)
				{
					while(read.Read())
					{
						if(read.IsDBNull(0))
							continue;
						string category = read.GetString(0);
						switch(category)
						{
							case "learning":
								LEARNING++;
								break;
							case "news":
								NEWS++;
								break;
							case "literature":
								LITERATURE++;
								break;
							case "internet":
								INTERNET++;
								break;
							case "medical":
								MEDICAL++;
								break;
							case "trade":
								TRADE++;
								break;
							case "memory":
								MEMORY++;
								break;
							case "newspaper":
								NEWSPAPER++;
								break;
							case "life":
								LIFE++;
								break;
							case "cinema":
								CINEMA++;
								break;
							case "personal":
								PERSONAL++;
								break;
							case "nature":
								NATURE++;
								break;
							case "satire":
								SATIRE++;
								break;
							case "emotional":
								EMOTIONAL++;
								break;
							case "general":
								GENERAL++;
								break;
							case "philosophy":
								PHILOSOPHY++;
								break;
							case "computer":
								COMPUTER++;
								break;
							case "joke":
								JOKE++;
								break;
							case "religion":
								RELIGION++;
								break;
							case "music":
								MUSIC++;
								break;
							case "authoring":
								AUTHORING++;
								break;
							case "art":
								ART++;
								break;
							case "sport":
								SPORT++;
								break;
							default:
								break;
						}
					}
					read.Close();
					connection.Close();
					command.Dispose();
					for(int j = 1 ; j <= 23; j++)
					{
						page.Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td width=\"414\" height=\"15\" valign=\"top\"><div><div align=\"right\">");
						//================================================
						if(j == 1)
						{
							if(LEARNING != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=learning\">" + "( " + LEARNING + " ) " + "آموزشي " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) آموزشي ");
							}
						}
						//================================================
						if(j == 2)
						{
							if(NEWS != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=news\">" + "( " + NEWS + " ) " + "اخبار " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) اخبار ");
							}
						}
						//================================================
						if(j == 3)
						{
							if(LITERATURE != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=literature\">" + "( " + LITERATURE + " ) " + "ادبيات " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) ادبيات ");
							}
						}
						//================================================
						if(j == 4)
						{
							if(INTERNET  != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=internet\">" + "( " + INTERNET + " ) " + "اينترنت " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) اينترنت ");
							}
						}
						//================================================
						if(j == 5)
						{
							if(MEDICAL != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=medical\">" + "( " + MEDICAL + " ) " + "پزشكي " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) پزشكي ");
							}
						}
						//================================================
						if(j == 6)
						{
							if(TRADE != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=trade\">" + "( " + TRADE + " ) " + "تجارت " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) تجارت ");
							}
						}
						//================================================
						if(j == 7)
						{
							if(MEMORY != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=memory\">" + "( " + MEMORY + " ) " + "خاطره " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) خاطره ");
							}
						}
						//================================================
						if(j == 8)
						{
							if(NEWSPAPER != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=newspaper\">" + "( " + NEWSPAPER + " ) " + "روزنامه نگاري " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) روزنامه نگاري ");
							}
						}
						//================================================
						if(j == 9)
						{
							if(LIFE != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=life\">" + "( " + LIFE + " ) " + "زندگي " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) زندگي ");
							}
						}
						//================================================
						if(j == 10)
						{
							if(CINEMA != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=cinema\">" + "( " + CINEMA + " ) " + "سينما " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) سينما ");
							}
						}
						//================================================
						if(j == 11)
						{
							if(PERSONAL != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=personal\">" + "( " + PERSONAL + " ) " + "شخصي " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) شخصي ");
							}
						}
						//================================================
						if(j == 12)
						{
							if(NATURE != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=nature\">" + "( " + NATURE + " ) " + "طبيعت " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) طبيعت ");
							}
						}
						//================================================
						if(j == 13)
						{
							if(SATIRE != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=satire\">" + "( " + SATIRE + " ) " + "طنز " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) طنز ");
							}
						}
						//================================================
						if(j == 14)
						{
							if(EMOTIONAL != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=emotional\">" + "( " + EMOTIONAL + " ) " + "عاطفی " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) عاطفی ");
							}
						}
						//================================================
						if(j == 15)
						{
							if(GENERAL != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=general\">" + "( " + GENERAL + " ) " + "عمومي " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) عمومي ");
							}
						}
						//================================================
						if(j == 16)
						{
							if(PHILOSOPHY != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=philosophy\">" + "( " + PHILOSOPHY + " ) " + "فلسفه " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) فلسفه ");
							}
						}
						//================================================
						if(j == 17)
						{
							if(COMPUTER != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=computer\">" + "( " + COMPUTER + " ) " + "کامپیوتر " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) کامپیوتر ");
							}
						}
						//================================================
						if(j == 18)
						{
							if(JOKE != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=joke\">" + "( " + JOKE + " ) " + "لطیفه " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) لطیفه ");
							}
						}
						//================================================
						if(j == 19)
						{
							if(RELIGION != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=religion\">" + "( " + RELIGION + " ) " + "مذهب " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) مذهب ");
							}
						}
						//================================================
						if(j == 20)
						{
							if(MUSIC != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=music\">" + "( " + MUSIC + " ) " + "موسيقي " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) موسيقي ");
							}
						}
						//================================================
						if(j == 21)
						{
							if(AUTHORING != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=authoring\">" + "( " + AUTHORING + " ) " + "نويسندگي " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) نويسندگي ");
							}
						}
						//================================================
						if(j == 22)
						{
							if(ART != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=art\">" + "( " + ART + " ) " + "هنر " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) هنر ");
							}
						}
						//================================================
						if(j == 23)
						{
							if(SPORT != 0)
							{
								page.Response.Write("<A href=\"UsersCategory.aspx?id=sport\">" + "( " + SPORT + " ) " + "ورزش " + "</A>");
							}
							else
							{
								page.Response.Write("( 0 ) ورزش ");
							}
						}
						//================================================
						page.Response.Write("</div></div></td><td width=\"42\" valign=\"top\"></td></tr></table>");
					}
					return ;
				}
				else
				{
					read.Close();
					connection.Close();
					command.Dispose();
					return ;
				}
			}
			catch
			{
				page.Response.Write("SQL Server Internal Error.");
				return ;
			}
		}
		//--------------------------------
		private void CategoryUsersPage(Page page)
		{
			try
			{
				string id = page.Request.QueryString["id"];
				if(id == null && id == "" && id != "learning" && id != "news" 
					&& id != "literature" && id != "internet" && id != "medical" && id != "trade"
					&& id != "memory" && id != "newspaper" && id != "life" && id != "cinema" && id != "personal"
					&& id != "nature" && id != "satire" && id != "emotional" && id != "general" && id != "philosophy" && id != "computer"
					&& id != "joke" && id != "religion" && id != "music" && id != "authoring" && id != "art"
					&& id != "sport")
				{
				    page.Response.Redirect(constants.WeblogUrl);
					return ;
				}
				else
				{
                    string query = "SELECT subdomain,title FROM " + constants.SQLUsersInformationTableName +
                                   " WHERE category=" + this.mark(id) + " AND IsWeblogExpired=0 AND f='y' ORDER BY i DESC";
					SqlConnection connection = new SqlConnection(this.ConnectString);
					SqlCommand command = new SqlCommand(query, connection);
					connection.Open();
					SqlDataReader read = command.ExecuteReader();
					if(read.HasRows)
					{
						string category = "";
						id = id.Trim();
						switch(id)
						{
							case "learning":
								category = "آموزشي";
								break;
							case "news":
								category = "اخبار";
								break;
							case "literature":
								category = "ادبيات";
								break;
							case "internet":
								category = "اينترنت";
								break;
							case "medical":
								category = "پزشكي";
								break;
							case "trade":
								category = "تجارت";
								break;
							case "memory":
								category = "خاطره";
								break;
							case "newspaper":
								category = "روزنامه نگاري";
								break;
							case "life":
								category = "زندگي";
								break;
							case "cinema":
								category = "سينما";
								break;
							case "personal":
								category = "شخصي";
								break;
							case "nature":
								category = "طبيعت";
								break;
							case "satire":
								category = "طنز";
								break;
							case "emotional":
								category = "عاطفی";
								break;
							case "general":
								category = "عمومي";
								break;
							case "philosophy":
								category = "فلسفه";
								break;
							case "computer":
								category = "کامپیوتر";
								break;
							case "joke":
								category = "لطیفه";
								break;
							case "religion":
								category = "مذهب";
								break;
							case "music":
								category = "موسيقي";
								break;
							case "authoring":
								category = "نويسندگي";
								break;
							case "art":
								category = "هنر";
								break;
							case "sport":
								category = "ورزش";
								break;
							default:
							{
								read.Close();
								//connection.Close();
								command.Dispose();
								page.Response.Redirect(constants.WeblogUrl);
								return ;
							}
						}
						page.Response.Write("       عنوان ليست : " + category + "<br><br>");
						string title = ""; 
						while(read.Read())
						{
							if(!read.IsDBNull(0) && !read.IsDBNull(1))
							{
								title = read.GetString(1);
								if(title.Length > 30)
								{
									title =  "..." + title.Substring(0, 30);
								}
								page.Response.Write(String.Format("<a href=\"http://{0}.{1}/\" target=\"_blank\" style=\"text-decoration: none\">{2}</a><br>", read.GetString(0), 
									constants.ZoneName, title));
							}
							else continue;
						}
						read.Close();
						//connection.Close();
						command.Dispose();
						return ;
					}
					else
					{
						read.Close();
						//connection.Close();
						command.Dispose();
						page.Response.Redirect(constants.WeblogUrl);
						return ;
					}
				}
			}
			catch
			{
				page.Response.Write("SQL Server Internal Error.");
				return ;
			}
		}
		//--------------------------------
		public static void ShowLinks(Page page)
		{
			string id = page.Request.QueryString["id"];
			if(id != null && id != "")
			{
				try { Convert.ToInt32(id); }
				catch 
				{
					page.Response.Redirect(constants.WeblogUrl, true);
					return ;
				}
				SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.Connection = connection;
				command.CommandText = string.Format("SELECT title,url FROM {0} WHERE BlogID={1} ORDER BY date DESC", constants.SQLLinksTableName, id);
				SqlDataReader reader = command.ExecuteReader();
				bool redirect = true;
				if(reader.HasRows)
				{
					redirect = false;
					while(reader.Read())
						page.Response.Write(String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br>", reader["url"], reader["title"]));
				}
				reader.Close();
				//connection.Close();
				command.Dispose();
				if(redirect)
				{
					page.Response.Redirect(constants.WeblogUrl, true);
					return ;
				}
			}
			return ;
		}
        //--------------------------------
        public static void TopVisits(Page page)
        {
            try
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                SqlCommand command = connection.CreateCommand();
                //command.CommandText = String.Format("SELECT TOP {0} subdomain FROM stat WHERE TotalVisits <= (SELECT MAX(TotalVisits) FROM stat) ORDER BY TotalVisits DESC", constants.SQLMaxUpdatedWeblogsShow);
                command.CommandText = String.Format("SELECT TOP {0} subdomain FROM stat WHERE TodayVisits <= (SELECT MAX(TodayVisits) FROM stat) ORDER BY TodayVisits DESC", constants.SQLMaxUpdatedWeblogsShow);
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                //PersianCalendar pcal = new PersianCalendar();
                if (read.HasRows)
                {
                    while (read.Read())
                        //page.Response.Write(String.Format("▪<a href='http://{0}.iranblog.com/' target='_blank'>{0}</a><br>", read.GetString(0)));
                        page.Response.Write(String.Format("<span class=\"v3color_green\" style=\"LINE-HEIGHT:1.5\"> »</span><a href=\"http://{0}/\" target=\"_blank\">{1}</a><br>", read.GetString(0) + "." + constants.ZoneName, read.GetString(0)));
                }
                read.Close();
                //connection.Close();
                command.Dispose();
                return;
            }
            catch
            {
                page.Response.Write("SQL Server Internal Error.");
                return;
            }
        }
		//--------------------------------
		public static void CategoryListUsers(Page page)
		{
			db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
				constants.SQLUsername, constants.SQLPassowrd);
			d.ListUsersPage(page);
			d.Dispose();
			return ;
		}
		//--------------------------------
		public static void CategoryUsers(Page page)
		{
			db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
				constants.SQLUsername, constants.SQLPassowrd);
			d.CategoryUsersPage(page);
			d.Dispose();
			return ;
		}
		//--------------------------------
		private SqlConnection CreateConnection()
		{
			SqlConnection connection=new SqlConnection(ConnectString);
			connection.Open();
			return connection;
		}
		//--------------------------------
		public int CreateTableWeblogs(string TableName)
		{
			try
			{
				/*string query = "CREATE TABLE " + TableName + " (i bigint IDENTITY(1,1),subdomain VARCHAR(30) NOT NULL, date datetime NOT NULL, "+
					"username VARCHAR(12) NOT NULL, subject NVARCHAR(200) NOT NULL, content NTEXT NOT NULL, NumComments INT NOT NULL DEFAULT(0))";*/
					string query = "CREATE TABLE " + TableName + " (date datetime NOT NULL PRIMARY KEY, "+
					"subject NVARCHAR(200) NOT NULL, content NTEXT NOT NULL, NumComments INT NOT NULL DEFAULT(0))";
				ExecuteNonQuery(query);
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------
		public int CreateTableComments()
		{
			string query = "CREATE TABLE " + constants.SQLCommentsTableName + " (i bigint IDENTITY(1,1),subdomain VARCHAR(30) NOT NULL, blogDate datetime NOT NULL, "+
				"postDate datetime NOT NULL, name NVARCHAR(30) NOT NULL, email VARCHAR(50) NOT NULL, URL VARCHAR(100), content NVARCHAR(2048) NOT NULL)";
			return ExecuteNonQuery(query);
		}
		//--------------------------------
		public int flag()
		{
			string query = "UPDATE " + constants.SQLUsersInformationTableName + " SET f=@f";
			SqlConnection connection = new SqlConnection(this.ConnectString);
			connection.Open();
			SqlCommand command = new SqlCommand(query, connection);
			SqlParameter FParam = new SqlParameter("@f", SqlDbType.VarChar);
			FParam.Value = "n";
			command.Parameters.Add(FParam);
			int result = command.ExecuteNonQuery();
			connection.Close();
			command.Dispose();
			return result;
		}
		//--------------------------------
		public int CreateTableInfoUsers()
		{
			string query="CREATE TABLE " + constants.SQLUsersInformationTableName + " (i bigint IDENTITY(1,1),username VARCHAR(12) NOT NULL, password NVARCHAR(12) NOT NULL, "+
				"email VARCHAR(50) NOT NULL, first_name NVARCHAR(30) NOT NULL, last_name NVARCHAR(30), " +
				" gender VARCHAR(10) NOT NULL, job VARCHAR(30) NOT NULL, age VARCHAR(10) NOT NULL, dateRegister datetime NOT NULL," +
				"subdomain VARCHAR(30), title NVARCHAR(100), about NVARCHAR(200) , template INT ,f VARCHAR(10) DEFAULT('y'), LastUpdate datetime, emailEnable bit DEFAULT(1)," +
				"category VARCHAR(20) DEFAULT('general'), link1 VARCHAR(60) DEFAULT('http://'), link2 VARCHAR(60) DEFAULT('http://')," +
				"link3 VARCHAR(60) DEFAULT('http://'), link4 VARCHAR(60) DEFAULT('http://'), link5 VARCHAR(60) DEFAULT('http://')," +
				"link6 VARCHAR(60) DEFAULT('http://'))";
			return this.ExecuteNonQuery(query);
		}
		//--------------------------------
		public int CreateTableLinks()
		{
			string query =  "CREATE TABLE links (" + 
							"[i] [bigint] IDENTITY (1, 1) NOT NULL ," +
							"[BlogID] [bigint] NOT NULL ," +
							"[title] [nvarchar] (400) NOT NULL ," +
							"[url] [nvarchar] (400) NOT NULL ," +
							"[date] [datetime] NOT NULL ) ON [PRIMARY]";;
			return this.ExecuteNonQuery(query);
		}
		//--------------------------------
		public int CreateTableLinkss()
		{
			string query =  "CREATE TABLE linkss (" + 
				"[i] [bigint] IDENTITY (1, 1) NOT NULL ," +
				"[BlogID] [bigint] NOT NULL ," +
				"[title] [nvarchar] (400) NOT NULL ," +
				"[url] [nvarchar] (400) NOT NULL ," +
				"[date] [datetime] NOT NULL ) ON [PRIMARY]";;
			return this.ExecuteNonQuery(query);
		}
		//--------------------------------
		public int CreateTableLatestUpdated()
		{
			string query = "CREATE TABLE " + constants.SQLLatestUpdatedWeblogsTableName +
				           " (i bigint IDENTITY(1,1),date datetime, subdomain VARCHAR(30), subject NVARCHAR(200) NOT NULL)";
			return this.ExecuteNonQuery(query);
		}
		//--------------------------------
		public int CreateIndex(string TableName, string IndexName, string FieldName)
		{
			string query = "Create Index " + IndexName + " ON " + TableName +"(" + FieldName+ ")" ;
			return ExecuteNonQuery(query);
		}
		//--------------------------------
		public int DeleteIndex(string TableName)
		{
			string query = "Drop Index " + TableName;
			return ExecuteNonQuery(query);
		}
		//--------------------------------
		public int DeleteIndex(string TableName,string IndexName)
		{
			string query = "Drop Table " + TableName + "." + IndexName;
			return ExecuteNonQuery(query);
		}
		//--------------------------------
		/*public int AlterWeblogsTable()
		{
			string query = "ALTER TABLE " + constants.SQLWeblogsTableName + " ADD NumComments INT NOT NULL DEFAULT(0)";
			return ExecuteNonQuery(query);
		}*/
		//--------------------------------
		private int ExecuteNonQuery(string query)
		{
			SqlConnection connection = this.CreateConnection();
			SqlCommand command=new SqlCommand(query,connection);
			int result=command.ExecuteNonQuery();
			//connection.Close();
			command.Dispose();
			return result;
		}
		//--------------------------------
	}
	//--------------------------------------------------------------------------------------------------
}