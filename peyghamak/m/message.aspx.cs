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

namespace Peyghamak.mobile
{
    public partial class message : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogined = Login.IsLoginProc(this);
			string _subdomain = Login.FindSubdomain(this);
            if (!_IsLogined || _subdomain == "")
			{
				this.Response.Redirect(String.Format("{0}/m",Constants.MainPageUrl), true);
				return;
			}
			     
			string PersonID = this.Request.QueryString["PersonID"];
			if( PersonID==null ) PersonID = this.Request.Form["PersonID"];

            Int64 _PersonID = -1;
            try
			{
				_PersonID = _PersonID = Convert.ToInt64(PersonID);
			}
			catch
			{
				this.Response.Redirect(String.Format("{0}/m",Constants.MainPageUrl), true);
			}
			
			if (_PersonID <= 0)
			{
				this.Response.Redirect(String.Format("{0}/m",Constants.MainPageUrl), true);
				return;
			}
			
			PageSettings(_PersonID);
			
			string _mode = this.Request.QueryString["mode"];
			if( _mode==null ) _mode = this.Request.Form["mode"];
			switch (_mode)
			{
				case "post":
					SendPrivateMessage(_PersonID);
					return;
				default:
					return;
			}
        }
        //--------------------------------------------------------------------
        private void PageSettings(Int64 _PersonID)
        {
            //MetaCopyrightControl();
            //SetSiteFooterControl();
            //GoogleAnalyticsControl();
		
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

            if ( (bool)command.Parameters["@IsMyFollower"].Value );
            else
            {
                command.Dispose();
                connection.Close();
				this.Response.Redirect(String.Format("{0}/m",Constants.MainPageUrl), true);
				return;
            }
			
			
			string _name = (string)command.Parameters["@name"].Value;
			//this.HyperLinkName.Text = name;
			//this.HyperLinkName.NavigateUrl = Login.BuildUserUrl((string)command.Parameters["@username"].Value);
			command.Dispose();
			connection.Close();
			
			this.title.InnerText = "پيغام خصوصي به " + _name;
			this.NameLabel.Text = "پيغام خصوصي به " + _name;
			this.Message.Text = "";
			
			this.h.Text = "<input name=\"PersonID\" type=\"hidden\" id=\"PersonID\" value=\"";
			this.h.Text += _PersonID.ToString();
			this.h.Text += "\"/>";			
        }
        //--------------------------------------------------------------------
        private void SendPrivateMessage(Int64 _PersonID)
        {
            byte _LanguageType = (byte)LanguageType.Persian;
            bool _PostAlign = Convert.ToBoolean(PostAlign.Right);
			
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
            //command.Parameters["@PostContent"].Value = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);
            string ss = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);
            if (ss.Length > 450) ss = ss.Substring(0, 450);
            command.Parameters["@PostContent"].Value = ss;


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

			this.Message.Text += "پيغام خصوصي شما با موفقيت ارسال شد در صورت نياز مي‌توانيد هم‌اكنون پيغام ديگري ارسال كنيد";
			
            return;
        }
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            //this.SiteFooterSection.Controls.Add(LoadControl("SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            //this.MetaCopyrightSection.Controls.Add(LoadControl("MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void GoogleAnalyticsControl()
        {
            //this.GoogleAnalyticsSection.Controls.Add(LoadControl("GoogleAnalyticsControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
    }
}