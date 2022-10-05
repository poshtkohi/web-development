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
using System.Drawing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using Peyghamak.ServerManagement;

namespace Peyghamak
{
    public partial class signup : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Login.IsLoginProc(this))
            {
                this.Response.Redirect(Login.BuildUserUrl(((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username), true);
                return;
            }
            //---------
            /*if (this.Session["IsInvited"] == null)
                IsInvited();
            if (!(bool)this.Session["IsInvited"])
            {
                this.Response.Redirect("signup_ref.aspx", true);
                return;
            }*/
            //---------
            if (this.BirthYear.Items.Count == 1)
            {
                for (int i = 1380; i >= 1300 ; i--)
                    this.BirthYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                for (int i = 12; i >= 1 ; i--)
                    this.BirthMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                for (int i = 31; i >= 1 ; i--)
                    this.BirthDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            MetaCopyrightControl();
            SetSiteFooterControl();
        }
        //--------------------------------------------------------------------
        private void IsInvited()
        {
            string _ref = this.Request.QueryString["_ref"];
            if (_ref == null)
            {
                this.Session["IsInvited"] = false;
                return;
            }
            if (_ref.IndexOf("'") >= 0 || _ref == "")
            {
                this.Session["IsInvited"] = false;
                return;
            }
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE _ref=@_ref", Constants.InvitationsTableName);
            command.Parameters.Add("@_ref", SqlDbType.VarChar);
            command.Parameters["@_ref"].Value = _ref; 
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if ((int)reader[0] == 1)
            {
                reader.Close();
                //command.Parameters.Clear();
                command.CommandText = String.Format("DELETE FROM {0} WHERE _ref=@_ref", Constants.InvitationsTableName);
                //command.Parameters.Add("@_ref", SqlDbType.VarChar);
                //command.Parameters["@_ref"].Value = _ref;
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                this.Session["IsInvited"] = true;
                return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Session["IsInvited"] = false;
                return;
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
        protected void save_Click(object sender, EventArgs e)
        {
            if (this.Request.Form["name"] == null || this.Request.Form["name"] == "")
            {
                this.message.Text = "نام خالی است.";
                this.message.Visible = true;
                return;
            }
            //------------
            string _username = this.Request.Form["username"].Trim().ToLower();
            if (_username == null || _username == "")
            {
                this.message.Text = "نام کاربری خالی است.";
                this.message.Visible = true;
                return;
            }
            if (_username.Length < 3)
            {
                this.message.Text = "نام کاربری حداقل باید 3 حرفی باشد.";
                this.message.Visible = true;
                return;
            }
            if (_username == "www" || _username == "mail"
                || _username == "mssql" || _username == "webmail"
                || _username == "ftp" || _username == "w"
                || _username == "ww" || _username == "wwww"
                || _username == "ftp" || _username == "ns1"
                || _username == "ns2" || _username == "ns"
                || _username == "sitebilder" || _username == "img1"
                || _username == "img2" || _username == "img3"
                || _username == "img4" || _username == "img5"
                || _username == "img6" || _username == "img7")
            {
                this.message.Text = "نام کاربری نا معتبر است.";
                this.message.Visible = true;
                return;
            }
            if (_username.IndexOf("-") >= 0)
            {
                this.message.Text = "نام کاربری نمی تواند دارای حرف - باشد.";
                this.message.Visible = true;
                return;
            }
            if (_username.IndexOf(".") >= 0)
            {
                this.message.Text = "نام کاربری نمی تواند دارای حرف . باشد.";
                this.message.Visible = true;
                return;
            }
            try
            {
                Convert.ToInt64(_username);
                this.message.Text = "نام کاربری نمی نواند عدد باشد.";
                this.message.Visible = true;
                return;
            }
            catch { }
            try
            {
                Convert.ToInt64(_username[0].ToString());
                this.message.Text = "نام کاربری نمی نواند با عدد آغاز شود.";
                this.message.Visible = true;
                return;
            }
            catch { }
            Regex rex = new Regex(@"^[\-0-9a-zA-Z]{1,}$");
            if (!rex.IsMatch(_username))
            {
                this.message.Text = "نام کاربری نامعتبر است.";
                this.message.Visible = true;
                return;
            }
            //------------
            if (this.Request.Form["password"] == null || this.Request.Form["password"] == "")
            {
                this.message.Text = "کلمه عبور خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["password"] != this.Request.Form["confirmPasword"])
            {
                this.message.Text = "کلمه عبور با تکرار کلمه عبور برابر نیست.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["username"] == this.Request.Form["password"])
            {
                this.message.Text = "کلمه عبور نمی تواند با نام کاربری یکی باشد.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["email"] == null || this.Request.Form["email"] == "")
            {
                this.message.Text = "آدرس ایمیل خالی است.";
                this.message.Visible = true;
                return;
            }
            rex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!rex.IsMatch(this.Request.Form["email"]))
            {
                this.message.Text = "آدرس ایمیل نامعتبر است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["BirthYear"] != null && this.Request.Form["BirthYear"] != "" && this.Request.Form["BirthYear"] == "none")
            {
                this.message.Text = "سال تولد خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["BirthMonth"] != null && this.Request.Form["BirthMonth"] != "" && this.Request.Form["BirthMonth"] == "none")
            {
                this.message.Text = "ماه تولد خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["BirthDay"] != null && this.Request.Form["BirthDay"] != "" && this.Request.Form["BirthDay"] == "none")
            {
                this.message.Text = "روز تولد خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["sex"] != null && this.Request.Form["sex"] != "" && this.Request.Form["sex"] == "none")
            {
                this.message.Text = "جنسیت خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["CountryKey"] != null && this.Request.Form["CountryKey"] != "" && this.Request.Form["CountryKey"] == "none")
            {
                this.message.Text = "کشور خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["ProvinceKey"] != null && this.Request.Form["ProvinceKey"] != "" && this.Request.Form["ProvinceKey"] == "none")
            {
                this.message.Text = "استان خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["CountryKey"] == "irn" && this.Request.Form["ProvinceKey"] == "0")
            {
                this.message.Text = "استان خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["city"] == null || this.Request.Form["city"] == "")
            {
                this.message.Text = "شهر خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["SecurityCode"] == null || this.Request.Form["SecurityCode"] == "")
            {
                this.message.Text = "کلمه تایید خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["SecurityCode"].ToLower() != ((string)this.Session["SecurityCode"]).ToLower())
            {
                this.message.Text = "کلمه تایید اشتباه است.";
                this.message.Visible = true;
                this.SecurityCode.Text = "";
                return;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE username='{1}'", Constants.AccountsTableName, _username.Trim().ToLower());
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if ((int)reader[0] > 0)
            {
                this.message.Text = "نام کاربری وجود دارد. نام دیگری انتخاب کنید.";
                this.message.Visible = true;
                reader.Close();
                //connection.Close();
                command.Dispose();
                connection.Close();
                return;
            }
            else
            {
                reader.Close();
                //-----registers the user subdomain on IIS and DNS servers---------------
                string _SubdomainServer = Constants.UsersSubdomainManagment.FindNewSubdomainServerName();
                string _subdomainServerIP = Constants.UsersSubdomainManagment.FindSubdomainServerIP(_SubdomainServer);

                /*DNS dns = new DNS(Constants.DnsServerUsername, Constants.DnsServerPassword);
                dns.CreateRecord(Constants.DnsServerIP, Constants.BlogDomain, _username, _subdomainServerIP);
                dns.CreateRecord(Constants.DnsServerIP, Constants.BlogDomain, "www." + _username, _subdomainServerIP);
                dns.Dispose();*/

                /*IIS iis = new IIS(_subdomainServerIP, Constants.IISServerUsername, Constants.IIServerPassword);
                iis.BindSubdomain(_username, _subdomainServerIP, "80", Constants.BlogDomain);
                iis.Dispose();*/
                //-----------------------------------------------------------------------

                //command.CommandText = string.Format("INSERT INTO {0} (name,username,password,email,mobile,BirthYear,BirthMonth,BirthDay,sex,ProvinceKey,ImageGuid,DateRegister) VALUES(@name,@username,@password,@email,@mobile,@BirthYear,@BirthMonth,@BirthDay,@sex,@ProvinceKey,@ImageGuid,@DateRegister);SELECT id FROM {0} WHERE id=@@IDENTITY;", Constants.AccountsTableName);
                command.CommandText = string.Format("INSERT INTO {0} (name,username,password,email,BirthYear,BirthMonth,BirthDay,sex,CountryKey,ProvinceKey,city,DateRegister) VALUES(@name,@username,@password,@email,@BirthYear,@BirthMonth,@BirthDay,@sex,@CountryKey,@ProvinceKey,@city,@DateRegister);SELECT id FROM {0} WHERE id=@@IDENTITY;", Constants.AccountsTableName);

                SqlParameter NameParam = new SqlParameter("@name", SqlDbType.NVarChar);
                SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.NVarChar);
                SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
                SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.NVarChar);
                SqlParameter BirthYearParam = new SqlParameter("@BirthYear", SqlDbType.Int);
                SqlParameter BirthMonthParam = new SqlParameter("@BirthMonth", SqlDbType.Int);
                SqlParameter BirthDayParam = new SqlParameter("@BirthDay", SqlDbType.Int);
                SqlParameter SexParam = new SqlParameter("@sex", SqlDbType.Bit);
                SqlParameter CountryKeyParam = new SqlParameter("@CountryKey", SqlDbType.VarChar);
                SqlParameter ProvinceKeyParam = new SqlParameter("@ProvinceKey", SqlDbType.VarChar);
                SqlParameter CityParam = new SqlParameter("@city", SqlDbType.NVarChar);
                SqlParameter DateRegisterParam = new SqlParameter("@DateRegister", SqlDbType.DateTime);
                SqlParameter SubdomainServerParam = new SqlParameter("@SubdomainServer", SqlDbType.VarChar);


                NameParam.Value = this.Request.Form["name"].Trim();
                UsernameParam.Value = _username;
                PasswordParam.Value = this.Request.Form["password"];
                EmailParam.Value = this.Request.Form["email"].Trim().ToLower();
                BirthYearParam.Value = Convert.ToInt32(this.Request.Form["BirthYear"]);
                BirthMonthParam.Value = Convert.ToInt32(this.Request.Form["BirthMonth"]);
                BirthDayParam.Value = Convert.ToInt32(this.Request.Form["BirthDay"]);
                SexParam.Value = Convert.ToBoolean(this.Request.Form["sex"]);
                CountryKeyParam.Value = this.Request.Form["CountryKey"];
                ProvinceKeyParam.Value = this.Request.Form["ProvinceKey"];
                CityParam.Value = this.Request.Form["city"];

                DateRegisterParam.Value = DateTime.Now;//_SubdomainServer
                SubdomainServerParam.Value = _SubdomainServer;


                command.Parameters.Add(NameParam);
                command.Parameters.Add(UsernameParam);
                command.Parameters.Add(PasswordParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(BirthYearParam);
                command.Parameters.Add(BirthMonthParam);
                command.Parameters.Add(BirthDayParam);
                command.Parameters.Add(SexParam);
                command.Parameters.Add(CountryKeyParam);
                command.Parameters.Add(ProvinceKeyParam);
                command.Parameters.Add(CityParam);
                command.Parameters.Add(DateRegisterParam);
                command.Parameters.Add(SubdomainServerParam);

                reader = command.ExecuteReader();
                reader.Read();
                //---for the future system developments, here must transfer the session to remote IIS Web server
                /*this.Session["username"] = _username;
                this.Session["BlogID"] = (Int64)reader[0];
                this.Session["name"] = this.Request.Form["name"];
                this.Session["ImageGuid"] = (string)ImageGuidParam.Value;
                 this.Session["IsLogined"] = true*/
                SigninSessionInfo _signinInfo = new SigninSessionInfo();
                _signinInfo.Username = _username;
                _signinInfo.BlogID = (Int64)reader[0];
                _signinInfo.Name = this.Request.Form["name"];
                _signinInfo.ImageGuid = "";
                _signinInfo.ThemeString = Constants.DefaultThemeString;
                this.Session["SigninSessionInfo"] = _signinInfo;
                this.Session["IsLogined"] = true;



                //---cookie-----
                EncryptedCookie ec = new EncryptedCookie(Constants.RijndaelKey, Constants.RijndaelIV);
                this.Response.Cookies["userInfo"]["BlogID"] = ec.EncryptWithMd5Hash(_signinInfo.BlogID);
                //this.Response.Cookies["userInfo"]["SessionMode"] = "true";
                this.Response.Cookies["userInfo"].Domain = Constants.BlogDomain;//
                this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(Constants.SessionTimeoutMinutes);
                //---

                reader.Close();
                command.Dispose();
                connection.Close();

                //here, email the user account information

                this.Response.Redirect(String.Format("http://www.{0}/signin.aspx", Constants.BlogDomain), true);
                return;
            }
        }
        //--------------------------------------------------------------------
    }
}
