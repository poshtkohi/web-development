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

namespace services.blogbuilderv1
{
	public partial class exception : System.Web.UI.Page
	{

		protected System.Web.UI.WebControls.Panel divSlideMenu;
		//--------------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["username"] == null || (string)Session["username"] == "" || 
				Session["subdomain"] == null || (string)Session["subdomain"] == "")
			{
				Session.Abandon();
				                Response.Redirect("Logouted.aspx");
				return ;
			}
			else
			{
				string i = this.Request.QueryString["i"];
				if(i != null && i == "logout")
				{
					Session.Abandon();
					Response.Redirect("/services/?i=userlogout");
					return ;
				}
				return ;
			}
		}
		//--------------------------------------------------------------------------
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

		}
		#endregion
		//--------------------------------------------------------------------------
		//--------------------------------------------------------------------------
	}
}
