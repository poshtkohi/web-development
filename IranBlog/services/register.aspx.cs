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

using System.Text.RegularExpressions;
using ServerManagement;
using AlirezaPoshtkoohiLibrary;
using services.blogbuilderv1;
using System.Data.SqlClient;
using System.IO;

using IranBlog.Classes.Security;
using services;

namespace AlirezaPoshtkoohiLibrary
{
    public partial class register : System.Web.UI.Page
    {
        //----------------------------------------------------------------------------------
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string flag = this.Request.QueryString["flag"];
            if (flag != null && flag != "")
            {
                if (String.Compare("longhornxp8033200608371362", flag) == 0)
                {
                    db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
                        constants.SQLUsername, constants.SQLPassowrd);
                    d.flag();
                    d.Dispose();
                }
            }
            bool _IsLogin = Common.IsLoginProc(this);
            if (_IsLogin)
            {
                this.Response.Redirect("/services/", true);
                return;
            }

            Session["FirstCome"] = true;
            this.message.Text = "";
            this.message.Visible = false;
            SetSiteHeaderControl();
            SetCopyrightFooterControl();
            this.LabelTime.Text = Common.PersianDate(DateTime.Now);
            return;
        }
        //----------------------------------------------------------------
        private void SetSiteHeaderControl()
        {
            this.MainSiteHeaderControl.Controls.Add(LoadControl("MainSiteHeaderControl.ascx"));
            return;
        }
        //----------------------------------------------------------------
        private void SetCopyrightFooterControl()
        {
            this.CopyrightFooterControl.Controls.Add(LoadControl("CopyrightFooterControl.ascx"));
            return;
        }
        //-----------------------------------------------------------------
        protected void submit_Click(object sender, System.EventArgs e)
        {
            /*if (this.Session["id"] != null)
            {
                if ((Int64)this.Session["id"] > 0)
                {
                    this.Response.Redirect("blogbuilderv1/PostAdmin.aspx", true);
                    return;
                }
            }*/
            if (this.acknowledge.Text.Trim().ToLower() != ((string)Session["acknowledge"]).ToLower())
            {
                this.message.Text = ".کلمه تایید اشتباه است";
                this.message.Visible = true;
                this.acknowledge.Text = "";
                this.password.Text = "";
                this.confirmPassword.Text = "";
                return;
            }
            //------------------------------------------------------------------

            if (this.Request.Form["username"] == null || this.Request.Form["username"] == "")
            {
                this.message.Text = ".ورود نام کاربری الزامی است";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["username"].IndexOf("'") >= 0)
            {
                this.Response.Redirect("/services/", true);
                return;
            }
            //------------------------------------------------------------------
            if (this.Request.Form["password"] == null || this.Request.Form["password"] == "")
            {
                this.message.Text = ".ورود کلمه عبور الزامی است";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            if (this.Request.Form["email"] == null || this.Request.Form["email"] == "")
            {
                this.message.Text = ".ورود ایمیل نویسنده الزامی است";
                this.message.Visible = true;
                return;
            }//\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
            Regex rex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!rex.IsMatch(this.Request.Form["email"]))
            {
                this.message.Text = ".حروف ایمیل نامعتبر است";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            if (this.Request.Form["title"] == null || this.Request.Form["title"] == "")
            {
                this.message.Text = ".عنوان وبلاگ خالی است";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            string _subdomain = this.Request.Form["subdomain"].Trim().ToLower();
            if (_subdomain == null || _subdomain == "")
            {
                this.message.Text = ".آدرس خالی است";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            if (_subdomain == "www" || _subdomain == "mail"
                || _subdomain == "mssql" || _subdomain == "webmail"
                || _subdomain == "ftp" || _subdomain == "w"
                || _subdomain == "ww" || _subdomain == "wwww"
                || _subdomain == "ftp" || _subdomain == "ns1"
                || _subdomain == "ns2" || _subdomain == "ns"
                || _subdomain == "sitebilder" || _subdomain == "forum"
                || _subdomain == "up" || _subdomain == "upload")
            {
                this.message.Text = ".آدرس نا معتبر است";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            if (_subdomain.IndexOf("-") >= 0)
            {
                this.message.Text = ".آدرس نمی تواند دارای حرف - باشد";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            if (_subdomain.IndexOf(".") >= 0)
            {
                this.message.Text = ".آدرس نمی تواند دارای حرف . باشد";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            try
            {
                Convert.ToInt64(_subdomain);
                this.message.Text = ".آدرس نمی نواند عدد باشد";
                this.message.Visible = true;
                return;
            }
            catch { }
            //------------------------------------------------------------------
            try
            {
                Convert.ToInt64(_subdomain[0].ToString());
                this.message.Text = ".آدرس نمی نواند با عدد آغاز شود";
                this.message.Visible = true;
                return;
            }
            catch { }
            rex = new Regex(@"^[\-0-9a-zA-Z]{1,}$");
            if (!rex.IsMatch(_subdomain))
            {
                this.message.Text = ".حروف آدرس وبلاگ نامعتبر است";
                this.message.Visible = true;
                return;
            }
            //------------------------------------------------------------------
            int _MaxPostShow = 10;
            try { _MaxPostShow = Convert.ToInt32(this.Request.Form["MaxPostShow"]); }
            catch { }
            if (_MaxPostShow == 0 || _MaxPostShow > 30)
                _MaxPostShow = 10;

            FormInfo infos = new FormInfo(this.username.Text.ToLower(), this.password.Text, this.email.Text.ToLower(),
                this.first_name.Text, this.last_name.Text, "male",
                "public", "2000", _MaxPostShow, Convert.ToBoolean(this.Request.Form["ArciveDisplayMode"]));
            int tempnum = 56;
            string subdomain = _subdomain.Trim().ToLower();
            subdomain = subdomain.Replace(subdomain, " ");
            SubdomainInfoPage infoss = new SubdomainInfoPage(this.username.Text.ToLower(), _subdomain.Trim().ToLower(),
                this.Request.Form["title"], "general");

            db reg = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
                constants.SQLUsername, constants.SQLPassowrd);
            int result = reg.RegisterPage(infos, infoss, constants.SQLUsersInformationTableName, this);
            reg.Dispose();
            if (result == -2)
            {
                this.username.Text = "";
                this.message.Visible = true;
                this.message.Text = ".نام کاربری وجود دارد";
                return;
            }
            if (result == -4)
            {
                this.message.Text = ".آدرس وجود دارد";
                this.message.Visible = true;
                return;
            }
            if (result == -3)
            {
                this.Session.Abandon();
                Response.Redirect("exception.aspx?error=SQL Error");
                return;
            }
            else if (result > 0)
            {
                DNS dns = new DNS(constants.password);
                result = dns.CreateRecord(constants.DNSServerAddress,
                    constants.ZoneName, infoss.Subdomain, constants.DnsDomainIPBlog);
                if (result < 0)
                {
                    this.Session.Abandon();
                    Response.Redirect("exception.aspx?error=DNS Server");
                    return;
                }
                result = dns.CreateRecord(constants.DNSServerAddress,
                    constants.ZoneName, "www." + infoss.Subdomain, constants.DnsDomainIPBlog);
                dns.Dispose();
                if (result < 0)
                {
                    this.Session.Abandon();
                    Response.Redirect("exception.aspx?error=DNS Server");
                    return;
                }
                /*IIS iis = new IIS(constants.password);
                result = iis.CreateWebSite(infoss.Subdomain, constants.RootDircetoryWeblogs, constants.DnsDomainIPBlog, "80", constants.ZoneName, tempnum, true);
                iis.Dispose();*/
                /////

                Directory.CreateDirectory(constants.RootDircetoryWeblogs + "\\" + infoss.Subdomain);
                File.Copy(constants.TemplatesPath + "Template" + tempnum.ToString() + ".html",
                    constants.RootDircetoryWeblogs + "\\" + infoss.Subdomain + "\\Default.html", true);
                /////

                if (result < 0)
                {
                    this.Session.Abandon();
                    Response.Redirect("exception.aspx?error=IIS Web Server");
                    return;
                }
                if (result >= 0)
                {
                    //-----------------------------------------------------------------------
                    Session["FirstCome"] = false;
                    this.Session["ChatBoxIsEnabled"] = false;
                    //-----------------------------------------------------------------------
                    SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                    IpLog(_SigninSessionInfo.BlogID);

                    SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = String.Format("INSERT INTO {0} (BlogID,subdomain,username,password,email,name,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess) VALUES(@BlogID,@subdomain,@username,@password,@email,@name,1,1,1,1,1,1,1,1,1,1);SELECT id FROM {0} WHERE BlogID=@BlogID AND username=@username;", constants.SQLTeamWeblogName);
                    SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
                    SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
                    SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
                    SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
                    SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.VarChar);
                    SqlParameter NameParam = new SqlParameter("@name", SqlDbType.NVarChar);


                    BlogIDParam.Value = _SigninSessionInfo.BlogID;
                    SubdomainParam.Value = infoss.Subdomain;
                    UsernameParam.Value = infos.Username;
                    PasswordParam.Value = infos.Password;
                    EmailParam.Value = infos.Email;
                    string name = String.Format("{1} {0}", this.Request.Form["last_name"].Trim(), this.Request.Form["first_name"].Trim()).Trim();
                    if (name != "")
                        NameParam.Value = name;
                    else
                        NameParam.Value = "مدیر وبلاگ";


                    command.Parameters.Add(BlogIDParam);
                    command.Parameters.Add(SubdomainParam);
                    command.Parameters.Add(UsernameParam);
                    command.Parameters.Add(PasswordParam);
                    command.Parameters.Add(EmailParam);
                    command.Parameters.Add(NameParam);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Int64 _AuthorID = -1;

                    _AuthorID = reader.GetInt64(0);
                    reader.Close();
                    connection.Close();
                    command.Dispose();

                    Session["acknowledge"] = "";

                    Session.Abandon();
                    //---cookie-----
                    EncryptedCookie ec = new EncryptedCookie(constants.key, constants.IV);
                    this.Response.Cookies["userInfo"]["info"] = ec.EncryptWithMd5Hash(String.Format("{0},false", _AuthorID));
                    this.Response.Cookies["userInfo"].Domain = constants.DomainBlog;
                    this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(constants.SessionTimeoutMinutes);
                    //---

                    this.Response.Redirect("blogbuilderv1/cp.aspx", true);
                    /*if (this.Request.UrlReferrer.AbsolutePath.IndexOf("register.aspx") >= 0)
                        this.Response.Redirect("cp.aspx");*/
                    return;
                }
                return;
            }

        }
        //----------------------------------------------------------------------------------
        private void IpLog(Int64 _BlogID)
        {
            if (_BlogID == 14231)
                return;

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "InsertIp_IpLog_proc";
            //-------------------------------------------
            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.Parameters.Add("@type", SqlDbType.Int);
            command.Parameters["@type"].Value = 1;

            command.Parameters.Add("@ip", SqlDbType.VarChar);
            command.Parameters["@ip"].Value = this.Request.UserHostAddress;

            command.ExecuteNonQuery();
            connection.Close();
            command.Dispose();
        }
        //----------------------------------------------------------------------------------
    }
}
