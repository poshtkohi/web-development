/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

using MySql.Data.MySqlClient;
using System.IO;

using news.Classes.Enums;

namespace news.Classes
{
	class Common
	{
		//-------------------------------------------------------------
		public static bool AddNewUser(string username, string password, AccountType _AccountType)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "AddUser_UsersPage_proc"; 


			command.Parameters.Add("?user", MySqlDbType.VarChar);
			command.Parameters["?user"].Value = username;

			command.Parameters.Add("?pass", MySqlDbType.VarChar);
			command.Parameters["?pass"].Value = password;

			command.Parameters.Add("?AccountType", MySqlDbType.Int32);
			command.Parameters["?AccountType"].Value = (int)_AccountType;

			command.Parameters.Add("?RegDate", MySqlDbType.Datetime);
			command.Parameters["?RegDate"].Value = DateTime.Now;

			command.Parameters.Add("?IsExisted", MySqlDbType.Int32);
			command.Parameters["?IsExisted"].Direction = ParameterDirection.Output;

			command.ExecuteNonQuery();

			if(Convert.ToBoolean((int)command.Parameters["?IsExisted"].Value))
			{
				command.Dispose();
				connection.Close();
				return true;
			}
			else
			{
				command.Dispose();
				connection.Close();
				return false;
			}
		}
		//-------------------------------------------------------------
		public static void DeleteUser(Int64 UserID)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "DeleteUser_UsersPage_proc"; 

			command.Parameters.Add("?UserID", MySqlDbType.Int64);
			command.Parameters["?UserID"].Value = UserID;

			command.ExecuteNonQuery();

			command.Dispose();
			connection.Close();

			return ;
		}
		//-------------------------------------------------------------
		public static void ChangePassword(Int64 UserID ,string _username, string _lastPassword, string _newPassword)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "ChangePassword_UsersPage_proc"; 

			command.Parameters.Add("?UserID", MySqlDbType.Int64);
			command.Parameters["?UserID"].Value = UserID;

			command.Parameters.Add("?_username", MySqlDbType.VarChar);
			command.Parameters["?_username"].Value = _username;

			command.Parameters.Add("?_lastPassword", MySqlDbType.VarChar);
			command.Parameters["?_lastPassword"].Value = _lastPassword;

			command.Parameters.Add("?_newPassword", MySqlDbType.VarChar);
			command.Parameters["?_newPassword"].Value = _newPassword;

			command.ExecuteNonQuery();

			command.Dispose();
			connection.Close();

			return ;
		}
		//-------------------------------------------------------------
		public static bool IsLogin(string username, string password, ref AccountType _AccountType)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "Login_LoginPage_proc"; 

			command.Parameters.Add("?user", MySqlDbType.VarChar);
			command.Parameters["?user"].Value = username;

			command.Parameters.Add("?pass", MySqlDbType.VarChar);
			command.Parameters["?pass"].Value = password;

			command.Parameters.Add("?_AccountType", MySqlDbType.Int32);
			command.Parameters["?_AccountType"].Direction = ParameterDirection.Output;

			command.Parameters.Add("?IsLogined", MySqlDbType.Int32);
			command.Parameters["?IsLogined"].Direction = ParameterDirection.Output;

			command.ExecuteNonQuery();

			if(Convert.ToBoolean((int)command.Parameters["?IsLogined"].Value))
			//if((bool)command.Parameters["?IsLogined"].Value)
			{
				_AccountType = (AccountType)command.Parameters["?_AccountType"].Value;
				command.Dispose();
				connection.Close();
				return true;
			}
			else
			{
				command.Dispose();
				connection.Close();
				return false;
			}
		}
		//-------------------------------------------------------------
		public static void InsertNewPost(Int64 UserID, string PostTitle, string PostContent, int NewsSubject)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "InsertNewPost_PostsPage_proc"; 


			command.Parameters.Add("?UserID", MySqlDbType.Int64);
			command.Parameters["?UserID"].Value = UserID;

			command.Parameters.Add("?PostTitle", MySqlDbType.LongText);
			command.Parameters["?PostTitle"].Value = PostTitle;

			command.Parameters.Add("?PostContent", MySqlDbType.LongText);
			command.Parameters["?PostContent"].Value = PostContent;

			command.Parameters.Add("?PostDate", MySqlDbType.Datetime);
			command.Parameters["?PostDate"].Value = DateTime.Now;

			command.Parameters.Add("?NewsSubject", MySqlDbType.Int32);
			command.Parameters["?NewsSubject"].Value = NewsSubject;

			/*command.Parameters.Add("?_mode", MySqlDbType.Int32);
			command.Parameters["?_mode"].Value = mode;*/

			PersianCalendar pc = new PersianCalendar();
			DateTime dt = DateTime.Now;

			command.Parameters.Add("?PersianYear", MySqlDbType.Int32);
			command.Parameters["?PersianYear"].Value = pc.GetYear(dt);

			command.Parameters.Add("?PersianMonth", MySqlDbType.Int32);
			command.Parameters["?PersianMonth"].Value = pc.GetMonth(dt);

			command.Parameters.Add("?PersianDay", MySqlDbType.Int32);
			command.Parameters["?PersianDay"].Value = pc.GetDayOfMonth(dt);


			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();
			return ;
		}
		//-------------------------------------------------------------
		public static void DeletePost(Int64 _PostID)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "DeletePost_PostsPage_proc"; 

			command.Parameters.Add("?_PostID", MySqlDbType.Int64);
			command.Parameters["?_PostID"].Value = _PostID;

			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();
			return ;
		}
		//-------------------------------------------------------------
		public static void UpdatePost(Int64 PostID, string _PostTitle, string _PostContent, int _NewsSubject)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "UpdatePost_PostsPage_proc"; 


			command.Parameters.Add("?PostID", MySqlDbType.Int64);
			command.Parameters["?PostID"].Value = PostID;

			command.Parameters.Add("?_PostTitle", MySqlDbType.LongText);
			command.Parameters["?_PostTitle"].Value = _PostTitle;

			command.Parameters.Add("?_PostContent", MySqlDbType.LongText);
			command.Parameters["?_PostContent"].Value = _PostContent;

			command.Parameters.Add("?_NewsSubject", MySqlDbType.Int32);
			command.Parameters["?_NewsSubject"].Value = _NewsSubject;


			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();
			return ;
		}
		//-------------------------------------------------------------
		public static void AddNewNewsGroup(string title)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "AddNewNewsGroup_NewsGropusPage_proc"; 

			command.Parameters.Add("?title", MySqlDbType.LongText);
			command.Parameters["?title"].Value = title;

			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();
			return ;
		}
		//-------------------------------------------------------------
		public static void DeleteNewsGroup(Int64 DeleteID)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "DeleteNewsGroup_NewsGropusPage_proc"; 

			command.Parameters.Add("?DeleteID", MySqlDbType.Int64);
			command.Parameters["?DeleteID"].Value = DeleteID;

			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();
			return ;
		}
		//-------------------------------------------------------------
		public static void UpdateNewsGroup(Int64 NewsGroupID, string _title)
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "UpdateNewsGroup_NewsGropusPage_proc"; 

			command.Parameters.Add("?NewsGroupID", MySqlDbType.Int64);
			command.Parameters["?NewsGroupID"].Value = NewsGroupID;

			command.Parameters.Add("?_title", MySqlDbType.LongText);
			command.Parameters["?_title"].Value = _title;

			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();
			return ;
		}
		//--------------------------------------------------------------------------------
		public static void AjaxDoPaging(Page page, string paging, string _mode, int currentPage, int _MaxItemShow, int _TotalItems, int _PagingNumber, string _ajaxShowFunctionName)
		{
			string pagingStr = "";
			if (_TotalItems != 0)
			{
				int total = _TotalItems;
				int a = total / _MaxItemShow;
				int b = total % _MaxItemShow;
				int pages = a;
				if (b != 0)
					pages++;

				if (pages < currentPage || currentPage <= 0)
					currentPage = 1;

				int n = currentPage / _PagingNumber; //current section
				/*if(pages < PagingNumber)
					n++;*/
				if (currentPage % _PagingNumber != 0)
					n++;


				//-------end section--------
				int end;
				int n1 = total / (_MaxItemShow * _PagingNumber); // total paging sections
				int n2 = total % (_MaxItemShow * _PagingNumber);
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
					end = ((n - 1) * _PagingNumber) + _PagingNumber;
				//end = pages;
				//--------------------------
				string next = null;
				string previous = null;
				if (n == 1 && n1 != 1) //first page
					next = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">»</a>&nbsp;", _ajaxShowFunctionName, n * _PagingNumber + 1, _mode);
				if (n1 == n && n != 1) //last page
					previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">«</a>&nbsp;", _ajaxShowFunctionName, (n - 1) * _PagingNumber, _mode);
				else //middle pages
				{
					if (n != 1 && n1 != n)
					{
						next = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">»</a>&nbsp;", _ajaxShowFunctionName, n * _PagingNumber + 1, _mode);
						previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">«</a>&nbsp;", _ajaxShowFunctionName, (n - 1) * _PagingNumber, _mode);
					}
				}

				if (previous != null)
					pagingStr += previous;

				for (int i = ((n - 1) * _PagingNumber) + 1; i <= end; i++)
				{
					if (i == currentPage)
						pagingStr += String.Format("<u>{0}</u>&nbsp;", i);
					else
						pagingStr += String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">{1}</a>&nbsp;", _ajaxShowFunctionName, i, _mode);
				}

				if (next != null)
					pagingStr += next;
				//pagingStr += "<br>n: ' + n + "<br>n1: "+ n1;
			}
			paging = paging.Replace("[paging]", pagingStr);
			page.Response.Write(paging);
			return;
		}
		//--------------------------------------------------------------------
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
				//buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), "");
			}
			return;
		}
		//--------------------------------------------------------------------------------
		public static string GetInternalTagContent(string buffer, string tagName)
		{
			string start = String.Format("<{0}>", tagName);
			string end =  String.Format("</{0}>", tagName);

			int _p1 = buffer.IndexOf(start) + start.Length;
			int _p2 = buffer.IndexOf(end);

			if(_p1 >= 0 && _p2 > _p1)
				return buffer.Substring(_p1, _p2 - _p1);
			else
				return null;
		}
		//--------------------------------------------------------------------
		public static bool IsLoginProc(Page page)
		{
			bool _IsLogined = false;
			if (page.Session["LoginInfo"] != null)
			{
				LoginInfo _LoginInfo = (LoginInfo)page.Session["LoginInfo"];
				if(_LoginInfo.Username == "")
					_IsLogined = false;
				else
					_IsLogined = true;
			}
			else
				_IsLogined = false;
			if(_IsLogined)
				return true;
			else
			{
				if(page.Request.Form["mode"] != null)
					WriteStringToAjaxRequest("Logouted", page);
				else
					page.Response.Redirect("/Login.aspx?mode=logouted", true);
				return false;
			}
		}
		//--------------------------------------------------------------------------------
		public static bool PageAccessControl(Page page, AccountType accountType)
		{
			if(accountType == AccountType.Admin)
				return true;
			else
			{
				bool _canAccess = true;
				string _filename = new FileInfo(page.Request.PhysicalPath).Name.ToLower();
				//news.Constants.AccessibleAdminPages
				for(int i = 0 ; i < Constants.AccessibleAdminPages.Length ; i++)
					if(_filename == Constants.AccessibleAdminPages[i])
					{
						_canAccess = false;
						break;
					}
				if(!_canAccess)
					page.Response.Redirect(Constants.NoneAdminAccessPageForward, true);
				return _canAccess;
			}
		}
		//--------------------------------------------------------------------------------
		private static void WriteStringToAjaxRequest(string str, Page page)
		{
			page.Response.Write(str);
			page.Response.Flush();
			//this.Response.Close();
			page.Response.End();
		}
		//--------------------------------------------------------------------------------
	}
}
