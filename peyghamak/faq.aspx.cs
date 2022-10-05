/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Peyghamak
{
    public partial class faq : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSettings();
        }
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            this.SiteFooterSection.Controls.Add(LoadControl("SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            this.MetaCopyrightSection.Controls.Add(LoadControl("MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void GoogleAnalyticsControl()
        {
            this.GoogleAnalyticsSection.Controls.Add(LoadControl("GoogleAnalyticsControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void PageSettings()
        {
            bool _Islogined = Login.IsLoginProc(this);
            if (_Islogined)
            {
                this.LoginedPanel.Visible = true;
                this.UnloginedPanel.Visible = false;
                SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);
            }
            else
            {
                this.LoginedPanel.Visible = false;
                this.UnloginedPanel.Visible = true;
            }
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
            return;
        }
        //--------------------------------------------------------------------
    }
}
