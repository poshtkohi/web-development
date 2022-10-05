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

namespace Peyghamak.mobile
{
    public partial class signout : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.CookieAbandon(this);
            this.Session.Clear();
            this.Session.Abandon();
            this.Response.Redirect("login.aspx", true);
            return;
        }
        //--------------------------------------------------------------------
    }
}
