/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using MySql.Data.MySqlClient;
using System.IO;

using news.Classes.Enums;
using news.Classes;

namespace news
{
	/// <summary>
	/// Summary description for Users.
	/// </summary>
	public class Users : System.Web.UI.Page
	{
		//--------------------------------------------------------------------------------
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Common.IsLoginProc(this))
				return ;

			LoginInfo _LoginInfo = (LoginInfo)this.Session["LoginInfo"];

			if(!Common.PageAccessControl(this, _LoginInfo.AccountType))
				return ;

			if (this.Request.Form["mode"] != null)
			{
				switch (this.Request.Form["mode"])
				{
					case "ShowUsers":
						ShowUsers();
						return;
					case "delete":
						UserDelete();
						return;
					case "load":
						UserLoad();
						return;
					case "update":
						UserUpdate();
						return;
					case "post":
						DoPost();
						return;
					default:
						return;
				}
			}
		}
		//--------------------------------------------------------------------------------
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		//--------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------
		private void DoPost()
		{
			if(!Common.AddNewUser(this.Request.Form["username"].ToLower(), this.Request.Form["password"], (AccountType)Convert.ToInt32(this.Request.Form["AccountType"])))
				WriteStringToAjaxRequest(".کاربر مورد نظر با موفقیت در سیستم تعریف شد");
			else
			    WriteStringToAjaxRequest("UserExisted");

			return;
		}
		//--------------------------------------------------------------------------------
		private void ShowUsers()
		{
			int currentPage = 1;
			try
			{
				currentPage = Convert.ToInt32(this.Request.Form["page"]);
			}
			catch { }

			if (currentPage == 0)
				currentPage++;

			if (currentPage > 1 && this.Session["_ItemNumUsers"] == null)
			{
				WriteStringToAjaxRequest("DoRefresh");
				return;
			}

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "ListUsers_UsersPage_proc";
			//-------------------------------------------

			command.Parameters.Add("?PageSize", MySqlDbType.Int32);
			command.Parameters["?PageSize"].Value = Constants.MaxUsersShow;

			command.Parameters.Add("?PageNumber", MySqlDbType.Int32);
			command.Parameters["?PageNumber"].Value = currentPage;

			command.Parameters.Add("?UsersNum", MySqlDbType.Int32);
			command.Parameters["?UsersNum"].Direction = ParameterDirection.Output;

            
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.HasRows)
			{
				string template = "";
				if (this.Cache["_template_Users"] == null)
				{
					StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\UsersTemplate.html");
					template = sr.ReadToEnd();
					this.Cache["_template_Users"] = template;
					sr.Close();
				}
				else
					template = (string)this.Cache["_template_Users"];

				/*StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\UsersTemplate.html");
				template = sr.ReadToEnd();
				sr.Close();*/

				int _p1Post = template.IndexOf("<post>") + "<post>".Length;
				int _p2Post = template.IndexOf("</post>");
				int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
				int _p2Paging = template.IndexOf("</paging>");
				if (_p1Post <= 0 || _p2Post <= 0)
				{
					this.Response.Write(template);
					this.Response.OutputStream.Flush();
					return;
				}
				this.Response.Write(template.Substring(0, _p1Post - "<post>".Length));
				this.Response.Flush();
				string _mainFormat = template.Substring(_p1Post, _p2Post - _p1Post);

				string temp = null;
				bool boxing = true;
				while (reader.Read())
				{
					temp = _mainFormat;

					if(boxing)
					{
						temp = temp.Replace("[boxing]", Constants.boxing1);
						boxing = false;
					}
					else
					{
						temp = temp.Replace("[boxing]", Constants.boxing2);
						boxing = true;
					}

					temp = temp.Replace("[id]", reader["id"].ToString());
					temp = temp.Replace("[username]", reader["username"].ToString());
					temp = temp.Replace("[AccountType]", reader["AccountType"].ToString());

					this.Response.Write(temp);
					this.Response.Flush();
				}

				reader.Close();

				if (currentPage == 1)
					this.Session["_ItemNumUsers"] = (int)command.Parameters["?UsersNum"].Value;

				if (_p1Paging > 0 && _p2Paging > 0)
				{
					this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
					//Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
					Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowUsers", currentPage, Constants.MaxUsersShow, (int)this.Session["_ItemNumUsers"], Constants.UsersPagingNumber, "ShowItems");
				}
				else
					this.Response.Write(template.Substring(_p2Post + "</post>".Length));
				this.Response.Flush();

				connection.Close();
				//this.Response.Close();
				this.Response.End();
				return;
			}
			else
			{
				WriteStringToAjaxRequest("NoFoundPost");
				reader.Close();
				connection.Close();

				return;
			}
		}
		//--------------------------------------------------------------------------------
		private void UserDelete()
		{
			Int64 _DeleteID = -1;
			try
			{
				_DeleteID = Convert.ToInt64(this.Request.Form["DeleteID"]);
			}
			catch { return; }

			if(_DeleteID == 208)
			{
				WriteStringToAjaxRequest("Success");
				return;
			}

			Common.DeleteUser(_DeleteID);

			WriteStringToAjaxRequest("Success");
			return;
		}
		//--------------------------------------------------------------------------------
		private void UserLoad()
		{
			Int64 _id = -1;
			try
			{
				_id = Convert.ToInt64(this.Request.Form["id"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "UserLoad_UsersPage_proc";

			command.Parameters.Add("?_id", SqlDbType.BigInt);
			command.Parameters["?_id"].Value = _id;


			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				//string str = "title,ThemeContent,PostContent";
				WriteStringToAjaxRequest(String.Format("{0},{1}", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["username"].ToString())),
					System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["AccountType"].ToString())) ));

				reader.Close();
				command.Dispose();
				connection.Close();
				return;
			}
			else
			{
				WriteStringToAjaxRequest("NoFoundPost");
				reader.Close();
				command.Dispose();
				connection.Close();
				return;
			}
		}
		//----------------------------------------------------------------------------------
		private void UserUpdate()
		{
			Int64 _id = -1;
			try
			{
				_id = Convert.ToInt32(this.Request.Form["id"]);
			}
			catch { return; }

			
			if(_id == 208)
			{
				WriteStringToAjaxRequest("Success");
				return;
			}

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "UserEdit_UsersPage_proc";

			command.Parameters.Add("?_id", SqlDbType.BigInt);
			command.Parameters["?_id"].Value = _id;

			command.Parameters.Add("?pass", MySqlDbType.VarChar);
			command.Parameters["?pass"].Value = this.Request.Form["password"];

			command.Parameters.Add("?_AccountType", MySqlDbType.Int32);
			command.Parameters["?_AccountType"].Value = Convert.ToInt32(this.Request.Form["AccountType"]);

			command.ExecuteNonQuery();
			connection.Close();
			command.Dispose();

			WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

			return;
		}
		//--------------------------------------------------------------------------------
		private void WriteStringToAjaxRequest(string str)
		{
			this.Response.Write(str);
			this.Response.Flush();
			//this.Response.Close();
			this.Response.End();
		}
		//--------------------------------------------------------------------------------
	}
}
