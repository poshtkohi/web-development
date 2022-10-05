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

using System.Data.SqlClient;

namespace Peyghamak
{
    public partial class message : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Login.IsLoginProc(this);
            string _subdomain = Login.FindSubdomain(this);
            if (_subdomain == "")
            {
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
            Int64 _PersonID = -1;
            try
            {
                if (this.Request.QueryString["PersonID"] != null)
                    _PersonID = Convert.ToInt64(this.Request.QueryString["PersonID"]);
                else
                    _PersonID = Convert.ToInt64(this.Request.Form["PersonID"]);
                if (_PersonID <= 0)
                {
                    this.Response.Redirect("/", true);
                    return;
                }
            }
            catch
            {
                this.Response.Redirect("/", true);
                return;
            }
            //--------
            if (this.Request.Form["mode"] != null && _IsLogin)
            {
                switch (this.Request.Form["mode"])
                {
                    case "post":
                        SendPrivateMessage(_PersonID);
                        return;
                    default:
                        return;
                }
            }
            if (this.Request.Form["mode"] != null && !_IsLogin)
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }
            if (!_IsLogin)// the user has not already logined into sysem.
            {
                this.Response.Redirect(Constants.LoginPageUrl, true);
                return;
            }
            bool _IsMyMessagePageForwardedFromFollowersPage = IsMyMessagePageForwardedFromFollowersPage(_subdomain, _IsLogin);
            if (!_IsMyMessagePageForwardedFromFollowersPage)
            {
                this.Response.Redirect("/", true);
                return;
            }
            if (!CheckCorrectMessageFromForwardedFollowerPage(_PersonID))
            {
                this.Response.Redirect("/", true);
                return;
            }
            PageSettings();
            //--------
        }
        //--------------------------------------------------------------------
        private void PageSettings()
        {
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
        }
        //--------------------------------------------------------------------
        private void SendPrivateMessage(Int64 _PersonID)
        {
            byte _LanguageType = (byte)LanguageType.Persian;
            bool _PostAlign = Convert.ToBoolean(PostAlign.Right);
            if (this.Request.Form["LanguageType"] != null && this.Request.Form["LanguageType"] != "")
            {
                try { _LanguageType = Convert.ToByte(this.Request.Form["LanguageType"]); }
                catch { }
                if (!Enum.IsDefined(typeof(LanguageType), (LanguageType)_LanguageType))
                    _LanguageType = (byte)LanguageType.Persian;
            }

            if (this.Request.Form["PostAlign"] != null && this.Request.Form["PostAlign"] != "")
            {
                try { _PostAlign = Convert.ToBoolean(Convert.ToInt32(this.Request.Form["PostAlign"])); }
                catch { }
                if (!Enum.IsDefined(typeof(PostAlign), (PostAlign)Convert.ToInt32(_PostAlign)))
                    _PostAlign = Convert.ToBoolean(PostAlign.Right);
            }
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPrivateMessages1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SendPrivateMessage_MessagePage_proc";

            command.Parameters.Add("@PersonID", SqlDbType.BigInt);
            command.Parameters["@PersonID"].Value = _PersonID;

            command.Parameters.Add("@MessagerBlogID", SqlDbType.BigInt);
            command.Parameters["@MessagerBlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@PostDate", SqlDbType.DateTime);
            command.Parameters["@PostDate"].Value = DateTime.Now;

            command.Parameters.Add("@PostType", SqlDbType.Char);
            command.Parameters["@PostType"].Value = (byte)PostType.FromWeb;

            command.Parameters.Add("@PostLanguage", SqlDbType.Char);
            command.Parameters["@PostLanguage"].Value = _LanguageType;

            command.Parameters.Add("@PostAlign", SqlDbType.Bit);
            command.Parameters["@PostAlign"].Value = _PostAlign;

            command.Parameters.Add("@PostContent", SqlDbType.NVarChar);
            command.Parameters["@PostContent"].Value = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);

            command.Parameters.Add("@HasPicture", SqlDbType.Bit);
            command.Parameters["@HasPicture"].Value = false;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsFriends1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsFriends1DbLinkedServer"].Value = Constants.@IsFriends1DbLinkedServer;

            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private bool CheckCorrectMessageFromForwardedFollowerPage(Int64 _PersonID)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CheckCorrectMessageFromForwardedFollowerPage_MessagePage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@PersonID", SqlDbType.BigInt);
            command.Parameters["@PersonID"].Value = _PersonID;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@IsMyFollower", SqlDbType.Bit);
            command.Parameters["@IsMyFollower"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@username", SqlDbType.NVarChar, 50);
            command.Parameters["@username"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@name", SqlDbType.NVarChar, 50);
            command.Parameters["@name"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@ImageGuid", SqlDbType.VarChar, 50);
            command.Parameters["@ImageGuid"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            if ((bool)command.Parameters["@IsMyFollower"].Value)
            {
                string name = (string)command.Parameters["@name"].Value;
                this.UserImage.ToolTip = name;
                this.UserImage.ImageUrl = Login.Build75x75ImageUrl((string)command.Parameters["@ImageGuid"].Value, false);
                this.HyperLinkName.Text = name;
                this.HyperLinkName.NavigateUrl = Login.BuildUserUrl((string)command.Parameters["@username"].Value);
                this.CurrentDate.Text = Login.PersianDate(DateTime.Now);
                command.Dispose();
                connection.Close();
                return true;
            }
            else
            {
                command.Dispose();
                connection.Close();
                return false;
            }
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
        private bool IsMyMessagePageForwardedFromFollowersPage(string _subdomain, bool _IsLogin)
        {
            if (_IsLogin)
            {
                if (((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username == _subdomain)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        //--------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------
    }
}