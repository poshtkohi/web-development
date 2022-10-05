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

using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;
using System.IO;
using services.blogbuilderv1;
using services;
using IranBlog.Classes.Security;

public partial class about : System.Web.UI.Page
{
    //----------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        SetSiteHeaderControl();
        SetCopyrightFooterControl();
        this.LabelTime.Text = Common.PersianDate(DateTime.Now);
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
    //----------------------------------------------------------------
}
