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

using System.IO;

namespace Peyghamak
{
    public partial class MetaCopyrightControl : System.Web.UI.UserControl
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            string _subdomain = Login.FindSubdomain(this.Page);
            bool _IsLogin = Login.IsLoginProc(this.Page);
            if (_IsLogin)
            {
                SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                if (_SigninSessionInfo.Username == _subdomain || _subdomain == "" || _subdomain == "www")
                {
                    this.css.Attributes.Add("href", String.Format("http://www.{0}/theme/{1}.css", Constants.BlogDomain, _SigninSessionInfo.ThemeString));
                    return;
                }
                else
                {
                    this.css.Attributes.Add("href", String.Format("http://www.{0}/theme/{1}.css", Constants.BlogDomain, (string)this.Session["GuestThemeString"]));
                    return;
                }
            }
            else
            {
                if (_subdomain == "" || _subdomain == "www")
                {
                    this.css.Attributes.Add("href", String.Format("http://www.{0}/theme/{1}.css", Constants.BlogDomain, Constants.DefaultThemeString));
                    return;
                }
                else
                {
                    this.css.Attributes.Add("href", String.Format("http://www.{0}/theme/{1}.css", Constants.BlogDomain, (string)this.Session["GuestThemeString"]));
                    return;
                }
            }
        }
        //--------------------------------------------------------------------
    }
}
