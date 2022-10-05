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

using services;
public partial class blogbuilderv1_signout : System.Web.UI.Page
{
    //------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        Common.CookieAbandon(this);
        this.Session.Clear();
        this.Session.Abandon();
        Response.Redirect("/services/?i=userlogout");
        return;
    }
    //------------------------------------------------------------
}
