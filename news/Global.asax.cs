/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

using news.Classes.Enums;
using news.Classes;

namespace news 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		//--------------------------------------------------------------------------------
		public Global()
		{
			InitializeComponent();
		}	
		//--------------------------------------------------------------------------------
		protected void Application_Start(Object sender, EventArgs e)
		{

		}
		//--------------------------------------------------------------------------------
		protected void Session_Start(Object sender, EventArgs e)
		{
			if(this.Session["LoginInfo"] == null)
			{
				LoginInfo _LoginInfo = new LoginInfo();
				_LoginInfo.Username = "admin";
				_LoginInfo.AccountType = AccountType.Admin;
				_LoginInfo.UserID = (Int64)208;
				this.Session["LoginInfo"] = _LoginInfo;
			}
		}
		//--------------------------------------------------------------------------------

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}
		//--------------------------------------------------------------------------------
		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}
		//--------------------------------------------------------------------------------
		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}
		//--------------------------------------------------------------------------------
		protected void Application_Error(Object sender, EventArgs e)
		{

		}
		//--------------------------------------------------------------------------------
		protected void Session_End(Object sender, EventArgs e)
		{

		}
		//--------------------------------------------------------------------------------
		protected void Application_End(Object sender, EventArgs e)
		{

		}
		//--------------------------------------------------------------------------------
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

