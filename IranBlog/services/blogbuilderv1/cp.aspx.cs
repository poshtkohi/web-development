/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class cp : System.Web.UI.Page
    {
        //----------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Common.IsLoginProc(this);
            if (!_IsLogin)
            {
                if (this.Request.Form["mode"] != null)
                    Common.WriteStringToAjaxRequest("Logouted", this);
                else
                    this.Response.Redirect("/services/?i=logouted", true);
                return;
            }

            //SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            PageSettings();
            return;
        }
        //----------------------------------------------------------------
        private void SetSiteControlPanelHeadrControl()
        {
            this.ControlPanelHeadrControl.Controls.Add(LoadControl("ControlPanelHeadrControl.ascx"));
            return;
        }
        //----------------------------------------------------------------
        private void SetCopyrightFooterControl()
        {
            this.CopyrightFooterControl.Controls.Add(LoadControl("../CopyrightFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void PageSettings()
        {
            SetSiteControlPanelHeadrControl();
            SetCopyrightFooterControl();
            this.LabelTime.Text = Common.PersianDate(DateTime.Now);
        }
        //--------------------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //----------------------------------------------------------------
    }
}
