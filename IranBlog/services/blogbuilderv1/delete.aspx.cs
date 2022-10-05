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

using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
	public partial class delete : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image Image1;
        //------------------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
                return false;
            else
                return true;
        }
		//------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
            bool _IsLogin = Common.IsLoginProc(this);
            if (!_IsLogin)
            {
                if (this.Request.Form["mode"] != null)
                    Common.WriteStringToAjaxRequest("Logouted", this);
                else
                    this.Response.Redirect("Logouted.aspx", true);
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }

            this.AcknowledgeError.Text = "";
            this.AcknowledgeError.Visible = false;
            return;
		}
		//------------------------------------------------------------
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
        //------------------------------------------------------------
		protected void submit_Click(object sender, System.EventArgs e)
		{
			if(this.acknowledge.Text.Trim() != (string)Session["acknowledge"])
			{
				this.AcknowledgeError.Text = ".کلمه تایید اشتباه است";
				this.AcknowledgeError.Visible = true;
				this.acknowledge.Text = "";
				return ;
			}
			else
			{
                SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

				db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
				            constants.SQLUsername, constants.SQLPassowrd);
                d.DeleteAccount(_SigninSessionInfo.Subdomain);
				d.Dispose();
				this.Session.Abandon();
				this.Response.Redirect(constants.WeblogUrl);
				return ;
			}
		}
		//------------------------------------------------------------
	}
}
