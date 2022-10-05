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
using System.Text.RegularExpressions;

namespace Peyghamak.mobile
{
    public partial class login : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Login.IsLoginProc(this))
            {
                this.Response.Redirect(String.Format("http://{0}.{1}/m/my.aspx",((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username, Constants.BlogDomain),true);
                return;
            }

            if (this.Request.Url.Host != Constants.BlogDomain && this.Request.Url.Host != "www." + Constants.BlogDomain)
            {
				this.Response.Redirect(String.Format("http://{0}/m/login.aspx",Constants.BlogDomain),true);	
                return;
            }
            
            MetaCopyrightControl();
            SetSiteFooterControl();           
			
			string _mode = this.Request.QueryString["mode"];
			if( _mode==null ) _mode = this.Request.Form["mode"];			
			switch (_mode)
			{
				case "l":
					DoLogin();
					return;
				default:
					return;
			}			
        }
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            //this.SiteFooterSection.Controls.Add(LoadControl("http://www.peyghamak.com/SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            //this.MetaCopyrightSection.Controls.Add(LoadControl("http://www.peyghamak.com/MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        protected void DoLogin()
        {
            string _username = this.Request.Form["username"].Trim().ToLower();
            //------------
            if (_username == null || _username == "")
            {
                this.message.Text = "نام کاربری خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["password"] == null || this.Request.Form["password"] == "")
            {
                this.message.Text = "کلمه عبور خالی است.";
                this.message.Visible = true;
                return;
            }
            Regex rex = new Regex(@"^[\-0-9a-zA-Z]{1,}$");
            if (!rex.IsMatch(_username))
            {
                this.message.Text = "نام کاربری نامعتبر است.";
                this.message.Visible = true;
                return;
            }
            //------------

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SigninPage_proc";

            command.Parameters.Add("@username", SqlDbType.NVarChar);
            command.Parameters["@username"].Value = _username;

            command.Parameters.Add("@password", SqlDbType.NVarChar);
            command.Parameters["@password"].Value = this.Request.Form["password"];

            command.Parameters.Add("@IsLogined", SqlDbType.Bit);
            command.Parameters["@IsLogined"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@name", SqlDbType.NVarChar, 50);
            command.Parameters["@name"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@ImageGuid", SqlDbType.VarChar, 50);
            command.Parameters["@ImageGuid"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@ThemeString", SqlDbType.NVarChar, 50);
            command.Parameters["@ThemeString"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            if ((bool)command.Parameters["@IsLogined"].Value)
            {
                SigninSessionInfo _signinInfo = new SigninSessionInfo();
                _signinInfo.Username = _username;
                _signinInfo.BlogID = (Int64)command.Parameters["@BlogID"].Value;
                _signinInfo.Name = (string)command.Parameters["@name"].Value;
                _signinInfo.ImageGuid = (string)command.Parameters["@ImageGuid"].Value;
                _signinInfo.ThemeString = (string)command.Parameters["@ImageGuid"].Value;
                this.Session["SigninSessionInfo"] = _signinInfo;
                this.Session["IsLogined"] = true;


                //---cookie-----
                EncryptedCookie ec = new EncryptedCookie(Constants.RijndaelKey, Constants.RijndaelIV);
                this.Response.Cookies["userInfo"]["BlogID"] = ec.EncryptWithMd5Hash(_signinInfo.BlogID);
                this.Response.Cookies["userInfo"].Domain = Constants.BlogDomain;
                this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(Constants.CookieTimeoutDays);
                //this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinuts(Constants.SessionTimeoutMinutes);
                //--------------
                command.Dispose();
                connection.Close();

                this.Response.Redirect(String.Format("http://{0}.{1}/m/my.aspx", _username, Constants.BlogDomain), true);
                return;
            }
            else
            {
                command.Dispose();
                connection.Close();
                this.message.Text = "نام کاربری یا کلمه عبور اشتباه است.";
                this.message.Visible = true;
                return;
            }
        }
        //--------------------------------------------------------------------
    }
}