/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_UsersCategory_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'UsersCategory.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
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
	/// Summary description for UsersCategory.
	/// </summary>
	public partial class Migrated_UsersCategory : UsersCategory
	{
		protected System.Web.UI.WebControls.ImageButton LoginSubmit;
		//-----------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (this.Request.QueryString["id"] != null)
            {
                if (this.Request.QueryString["id"].IndexOf("'") >= 0)
                {
                    this.Response.Redirect("/services/", true);
                    return;
                }
            }

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
		//----------------------------------------------------------------
	}
}
