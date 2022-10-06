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

using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.IO;

using news.Classes.Enums;
using news.Classes;

namespace news
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label message;
		protected System.Web.UI.WebControls.ImageButton signin;
		//--------------------------------------------------------------------------------
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(this.Request.QueryString["mode"] != null)
			{
				switch(this.Request.QueryString["mode"])
				{
					case "logouted":
						this.message.Visible = true;
						this.message.Text = "جلسه شما منقضی شده است.";
						break;
					case "userlogout":
						this.message.Visible = true;
						this.message.Text = "عملیات خروج با موفقیت انجام شد.";
						break;
					default:
						break;
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
			this.signin.Click += new System.Web.UI.ImageClickEventHandler(this.signin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		//--------------------------------------------------------------------------------

		//--------------------------------------------------------------------------------
		private void signin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string _username = this.Request.Form["username"].Trim().ToLower();
			//------------
			if (_username == null || _username == "")
			{
				this.message.Text = "نام کاربری خالی است.";
				this.message.Visible = true;
				return;
			}
			if (this.Request.Form["password"] == null || this.Request.Form["password"] == "")
			{
				this.message.Text = "کلمه عبور خالی است.";
				this.message.Visible = true;
				return;
			}
			Regex rex = new Regex(@"^[\-0-9a-zA-Z]{1,}$");
			if (!rex.IsMatch(_username))
			{
				this.message.Text = "نام کاربری نامعتبر است.";
				this.message.Visible = true;
				return;
			}
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "Login_LoginPage_proc"; 
			//-------------------------------------------

			command.Parameters.Add("?_username", MySqlDbType.VarChar);
			command.Parameters["?_username"].Value = _username;

			command.Parameters.Add("?_password", MySqlDbType.VarChar);
			command.Parameters["?_password"].Value =  this.Request.Form["password"];
			//-------------------------------------------
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.HasRows)
			{
				LoginInfo _LoginInfo = new LoginInfo();
				reader.Read();

				_LoginInfo.UserID = Convert.ToInt64(reader["id"].ToString());
			    _LoginInfo.Username = _username;
				_LoginInfo.AccountType = (AccountType)Convert.ToInt32(reader["AccountType"].ToString());

				this.Session["LoginInfo"] = _LoginInfo;

				reader.Close();
				connection.Close();

				this.Response.Redirect("NewsAdmin.aspx", true);
				return;
			}
			else
			{
				this.message.Visible = true;
				this.message.Text = "نام کاربری یا کلمه عبور اشتباه است.";

				reader.Close();
				connection.Close();
				return;
			}
		}
		//--------------------------------------------------------------------------------
	}
}
