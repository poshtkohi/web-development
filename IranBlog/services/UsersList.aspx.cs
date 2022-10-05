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
	/// Summary description for UsersList.
	/// </summary>
	public partial class UsersList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton LoginSubmit;
        //-----------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            SetSiteHeaderControl();
            SetCopyrightFooterControl();
            return;
        }
        //----------------------------------------------------------------
        private void SetSiteHeaderControl()
        {
            this.MainSiteHeaderControl.Controls.Add(LoadControl("MainSiteHeaderControl.ascx"));
            return;
        }
        //----------------------------------------------------------------
        private void SetCopyrightFooterControl()
        {
            this.CopyrightFooterControl.Controls.Add(LoadControl("CopyrightFooterControl.ascx"));
            return;
        }
        //-----------------------------------------------------------------
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
		//-----------------------------------------------------------------
	}
}
