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


using bookstore;

namespace bookstore.admin
{
	/// <summary>
	/// Summary description for SignIn.
	/// </summary>
    public partial class _Default : System.Web.UI.Page
	{
		//--------------------------------------------------------------------------------
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if(this.Request.QueryString["i"] != null && this.Request.QueryString["i"] != "" && this.Request.QueryString["i"] == "signouted")
			{
				this.error.Text = "جلسه شما منقضی شده است";
				this.error.Visible = true;
				return ;
			}
		}
		//--------------------------------------------------------------------------------
        protected void login_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["username"] == null || this.Request.Form["username"] == "")
			{
				this.error.Text = "کلمه کاربری خالی است";
				this.error.Visible = true;
				this.username.Text = "";
				this.password.Text = "";
				return ;
			}
			if(this.Request.Form["password"] == null || this.Request.Form["password"] == "")
			{
				this.error.Text = "کلمه عبور خالی است";
				this.error.Visible = true;
				this.username.Text = "";
				this.password.Text = "";
				return ;
			}
			if(this.Request.Form["username"] != constants.AdminUsername || this.Request.Form["password"] !=  constants.AdminPassword)
			{
				this.error.Text = "کلمه کاربری و یا کلمه عبور اشتباه است";
				this.error.Visible = true;
				this.username.Text = "";
				this.password.Text = "";
				return ;
			}
            this.Session["AdminUsername"] = constants.AdminUsername;
			this.Response.Redirect("cp.aspx", true);
			return ;
		}
		//--------------------------------------------------------------------------------
	}
}
