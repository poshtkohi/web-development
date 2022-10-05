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
    public partial class Default : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            string _subdomian = Login.FindSubdomain(this);
            bool _IsLogined = Login.IsLoginProc(this);
            if (_IsLogined)
            {
                SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                if (info.Username == _subdomian || _subdomian == "")
                {
                    this.Response.Redirect(String.Format("http://{0}.{1}/m/my.aspx", info.Username, Constants.BlogDomain), true);
                    return;
                }
            }
            
	if (!_IsLogined && _subdomian == "")// the user has not already logined into sysem.
            {
				this.Response.Redirect(String.Format("http://www.{0}/m/login.aspx",Constants.BlogDomain), true);
                return;
            }
            
	if (_subdomian != "")
            {
                this.Response.Redirect(String.Format("http://{0}.{1}/m/guest.aspx", _subdomian, Constants.BlogDomain), true);
                return;
            }
            else
            {
                this.Response.Redirect(String.Format("http://www.{0}/m/login.aspx",Constants.BlogDomain), true);
                return;
            }
        }
        //--------------------------------------------------------------------
    }
}
