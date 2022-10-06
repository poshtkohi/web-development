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

namespace bookstore
{
    public partial class LoginControl : System.Web.UI.UserControl
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Common.IsLoginProc(this.Page))
            {
                this.LoginedPanel.Visible = true;
                this.UnLogginedPanel.Visible = false;
            }
            else
            {
                this.LoginedPanel.Visible = false;
                this.UnLogginedPanel.Visible = true;
            }
            /*string _subdomain = Login.FindSubdomain(this.Page);
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
            }*/
        }
        //--------------------------------------------------------------------
    }
}
