using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using news.Classes.Enums;
using news.Classes;
using System.IO;

namespace news.Controls
{
	/// <summary>
	///		Summary description for SiteFooter.
	/// </summary>
	public class SiteFooter : System.Web.UI.UserControl
	{
		//--------------------------------------------------------------------------------
		private void Page_Load(object sender, System.EventArgs e)
		{
			LoginInfo _LoginInfo = (LoginInfo)this.Session["LoginInfo"];

			if(_LoginInfo.AccountType == AccountType.Admin)
			{
				this.FindControl("NewsGroupSection").Visible = true;
				this.FindControl("UsersSection").Visible = true;
			}
			else
			{
				this.FindControl("NewsGroupSection").Visible = false;
				this.FindControl("UsersSection").Visible = false;
			}

			FileInfo _FileInfo = new FileInfo(this.Request.PhysicalPath);

			if(_FileInfo.Name == "NewsAdmin.aspx")
			{
				this.FindControl("DirectSearch").Visible = true;
				this.FindControl("IndirectSearch").Visible = false;

				this.FindControl("DirectNewsAdmin").Visible = true;
				this.FindControl("IndirectNewsAdmin").Visible = false;
			}
			else
			{
				this.FindControl("DirectSearch").Visible = false;
				this.FindControl("IndirectSearch").Visible = true;

				this.FindControl("DirectNewsAdmin").Visible = false;
				this.FindControl("IndirectNewsAdmin").Visible = true;
			}
			return ;
		}
		//--------------------------------------------------------------------------------
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		//--------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------
	}
}
