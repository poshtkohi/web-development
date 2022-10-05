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

public partial class signup_ref : System.Web.UI.Page
{
    //--------------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session["IsInvited"] = null;
        this.Session.Clear();
        this.Session.Abandon();
        MetaCopyrightControl();
        SetSiteFooterControl();
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
}
