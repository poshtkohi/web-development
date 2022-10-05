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
    public partial class signout : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.CookieAbandon(this);
            /*this.Session["SigninSessionInfo"] = null;
            this.Session["IsLogined"] = null;
            this.Session["SecurityCode"] = null;*/
            this.Session.Clear();
            this.Session.Abandon();
            if (this.Request.Url.Host == Constants.BlogDomain || this.Request.Url.Host == "www." + Constants.BlogDomain)
                this.Response.Redirect(Constants.MainPageUrl, true);
            else
                this.Response.Redirect(Constants.LogoutPageUrl, true);
            return;
        }
        //--------------------------------------------------------------------
    }
}
