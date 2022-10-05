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
using AlirezaPoshtkoohiLibrary;

namespace services
{
	/// <summary>
	/// Summary description for exception.
	/// </summary>
	public partial class exception : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

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
			this.LoginSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.LoginSubmit_Click);

		}
		#endregion

		private void LoginSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			/*if(Session["username"] != null && ((string)Session["username"]).Trim() == "")
				{*/
					if(this.Request.Form["username"]!= null && this.Request.Form["password"] != null)
					{
						db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName, 
							constants.SQLUsername, constants.SQLPassowrd);
						int result = d.LoginPage(this.Request.Form["username"], this.Request.Form["password"], this);
						d.Dispose();
						if(result == -3)
						{
							Session.Abandon();
							this.Response.Redirect("exception.aspx?error=SQL Server");
							return ;
						}
						if(result == -1)
						{
							Session.Abandon();
							this.Response.Redirect("?i=unauthorized");
							return ;
						}
						if(result == 1)
						{
							d.Dispose();
							this.Response.Redirect("wizard.aspx");
							return ;
						}
						if(result == 0)
						{
							this.Response.Redirect("blogbuilderv1/home.aspx");
							return ;
						}
					/*}
					else
					{
						Session.Abandon();
						this.Response.Redirect("/services/");
						return ;
					}*/
				}
		}
	}
}
