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
using AlirezaPoshtkoohiLibrary;
using System.IO;
using services.blogbuilderv1;
using services;
using IranBlog.Classes.Security;

using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    //--------------------------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.Form["username"] != null)
        {
            signin();
            return;
        }
        if (this.Request.Form["mode"] != null)
        {
            switch (this.Request.Form["mode"])
            {
                case "ShowUpdates":
                    ShowUpdates();
                    return;
                case "signin":
                    signin();
                    return;
                case "PassForget":
                    PassForget();
                    return;
                default:
                    return;
            }
        }

        PageSettings();
        return;
    }
    //--------------------------------------------------------------------
    private void PageSettings()
    {
        SetSiteHeaderControl();
        SetCopyrightFooterControl();
        this.LabelTime.Text = Common.PersianDate(DateTime.Now);
        bool _IsLogin = Common.IsLoginProc(this);
        if (_IsLogin)
        {
            //SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            this.HomePageLoginSection.Visible = false;
            this.HomePageToolbarSection.Visible = true;
            //this.MyWeblogUrl.NavigateUrl = String.Format("http://{0}.{1}/", info.Subdomain, constants.DomainBlog);
            return;
        }
        else
        {
            this.HomePageLoginSection.Visible = true;
            this.HomePageToolbarSection.Visible = false;
            return;
        }
    }
    //--------------------------------------------------------------------------------
    private void SetSiteHeaderControl()
    {
        this.MainSiteHeaderControl.Controls.Add(LoadControl("MainSiteHeaderControl.ascx"));
        return;
    }
    //--------------------------------------------------------------------------------
    private void SetCopyrightFooterControl()
    {
        this.CopyrightFooterControl.Controls.Add(LoadControl("CopyrightFooterControl.ascx"));
        return;
    }
    //--------------------------------------------------------------------------------
    private void PassForget()
    {
        string _username = this.Request.Form["username_fp"].Trim().ToLower();
        //------------
        if (_username == null || _username == "")
        {
            WriteStringToAjaxRequest(".نام کاربری خالی است");
            return;
        }
        Regex rex = new Regex(@"^[\-0-9a-zA-Z]{1,}$");
        if (!rex.IsMatch(_username))
        {
            WriteStringToAjaxRequest(".نام کاربری نامعتبر است");
            return;
        }
        string _email = this.Request.Form["email_fp"].Trim().ToLower();
        if (_email == null || _email == "")
        {
            WriteStringToAjaxRequest(".آدرس ایمیل خالی است");
            return;
        }
        rex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        if (!rex.IsMatch(_email))
        {
            WriteStringToAjaxRequest(".آدرس ایمیل نامعتبر است");
            return;
        }
        //------------

        string _id = this.Request.UserHostName + "_PassForget";
        if (this.Cache[_id] == null)
            this.Cache.Add(_id, 1, null, DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        else
        {
            WriteStringToAjaxRequest(".عمل در خواست شده به علت مسایل امنیتی سایت ایران بلاگ تا یک ساعت آینده از سوی کامپیوتر شما پذیرفته نخواهد شد");
            return;
        }

        SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "ForgottenPassword_HomePage_proc";

        command.Parameters.Add("@username", SqlDbType.VarChar);
        command.Parameters["@username"].Value = _username;

        command.Parameters.Add("@email", SqlDbType.VarChar);
        command.Parameters["@email"].Value = _email;

        SqlDataReader reader = command.ExecuteReader();

        string _password = null;

        if (reader.Read())
            _password = (string)reader["password"];

        command.Dispose();
        connection.Close();

        if (_password != null)
            SendForgottenPassword(_password, _email);

        //WriteStringToAjaxRequest("کلمه عبورتان به آدرس ایمیلتان ارسال شد.");
        WriteStringToAjaxRequest("Success");
        return;
    }
    //--------------------------------------------------------------------------------
    private static void SendForgottenPassword(string _password, string _email)
    {
        //MailMessage MailSender = new MailMessage(Constants.AdminEmail, _email);
        MailMessage MailSender = new MailMessage("alireza.poshtkohi@gmail.com", _email);
        MailSender.BodyEncoding = System.Text.Encoding.UTF8;
        MailSender.IsBodyHtml = true;
        MailSender.Priority = MailPriority.High;
        MailSender.Subject = "Forgotten Password for IranBlog.com Users";
        MailSender.Body = String.Format("<p style='direction:rtl;text-align:right;font-family:Tahoma;font-size:12px'>با سلام<br><br>"
              + "کاربر گرامی سایت ایران بلاگ کلمه عبور فراموش شده شما در ذیل آمده است:<br>"
              + "<b>کلمه عبور: "+"{0} .</b><br><br>"
              + "با تشکر،<br>"
              + "گروه ایران بلاگ"
              + "</p>", _password);
        SmtpClient client = new SmtpClient("smtp.gmail.com");
        //System.Net.NetworkCredential _SMTPUserInfo = new System.Net.NetworkCredential(Constants.AdminEmail, Constants.AdminEmailPassword);
        System.Net.NetworkCredential _SMTPUserInfo = new System.Net.NetworkCredential("pass.iranblog.com@gmail.com", "bh&*6743n8osudfklbduGHIjg78");
        client.UseDefaultCredentials = false;
        client.Credentials = _SMTPUserInfo;
        client.EnableSsl = true;
        client.Send(MailSender);
        ///*mxs = null;
        client = null;
    }
    //--------------------------------------------------------------------------------
    private void ShowUpdates()
    {
        int currentPage = 1;
        try
        {
            currentPage = Convert.ToInt32(this.Request.Form["page"]);
        }
        catch { }

        if (currentPage == 0)
            currentPage++;

        if (currentPage > 1 && this.Session["_ItemNumPostUpdates"] == null)
        {
            WriteStringToAjaxRequest("DoRefresh");
            return;
        }

        SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "ListUpdates_MainPage_proc";
        //-------------------------------------------
        command.Parameters.Add("@PageSize", SqlDbType.Int);
        command.Parameters["@PageSize"].Value = 20;//constants.MaxPostAdminShow;

        command.Parameters.Add("@PageNumber", SqlDbType.Int);
        command.Parameters["@PageNumber"].Value = currentPage;

        command.Parameters.Add("@PostNum", SqlDbType.Int);
        command.Parameters["@PostNum"].Direction = ParameterDirection.Output;


        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            string template = "";
            if (this.Cache["_template_homePage_updates"] == null)
            {
                StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\blogbuilderv1\AjaxTemplates\UpdatesTemplate.html");
                template = sr.ReadToEnd();
                this.Cache["_template_homePage_updates"] = template;
                sr.Close();
            }
            else
                template = (string)this.Cache["_template_homePage_updates"];

            /*StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\blogbuilderv1\AjaxTemplates\UpdatesTemplate.html");
            template = sr.ReadToEnd();
            sr.Close();*/

            int _p1Post = template.IndexOf("<post>") + "<post>".Length;
            int _p2Post = template.IndexOf("</post>");
            int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
            int _p2Paging = template.IndexOf("</paging>");
            if (_p1Post <= 0 || _p2Post <= 0)
            {
                this.Response.Write(template);
                this.Response.OutputStream.Flush();
                return;
            }
            this.Response.Write(template.Substring(0, _p1Post - "<post>".Length));
            this.Response.Flush();
            string _mainFormat = template.Substring(_p1Post, _p2Post - _p1Post);

            string temp = null;
            bool boxing = true;
            DateTime dt;
            AlirezaPoshtkoohiLibrary.PersianCalendar pc = new AlirezaPoshtkoohiLibrary.PersianCalendar();
            while (reader.Read())
            {
                temp = _mainFormat;

                if (boxing)
                {
                    temp = temp.Replace("[boxing]", constants.boxing1);
                    boxing = false;
                }
                else
                {
                    temp = temp.Replace("[boxing]", constants.boxing2);
                    boxing = true;
                }
                dt = (DateTime)reader["date"];
                temp = temp.Replace("[PostID]", reader["PostID"].ToString());
                temp = temp.Replace("[subdomain]", reader["subdomain"].ToString());
                string title = reader["title"].ToString();
                int len = title.Length;
                if (len >= 25)
                    len = 25;
                title = title.Substring(0, len);
                temp = temp.Replace("[title]", title);
                temp = temp.Replace("[subject]", reader["subject"].ToString());
                temp = temp.Replace("[date]", String.Format("{0}/{1}/{2}&nbsp;&nbsp;{3}:{4}:{5}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt), dt.Hour, dt.Minute, dt.Second));

                this.Response.Write(temp);
                this.Response.Flush();
            }

            reader.Close();

            if (currentPage == 1)
                this.Session["_ItemNumPostUpdates"] = (int)command.Parameters["@PostNum"].Value;

            if (_p1Paging > 0 && _p2Paging > 0)
            {
                this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                services.Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowUpdates", currentPage, /*constants.MaxPostAdminShow*/20, (int)this.Session["_ItemNumPostUpdates"], constants.PostAdminPagingNumber, "ShowItems");
            }
            else
                this.Response.Write(template.Substring(_p2Post + "</post>".Length));
            this.Response.Flush();

            connection.Close();
            //this.Response.Close();
            this.Response.End();
            return;
        }
        else
        {
            WriteStringToAjaxRequest("NoFoundPost");
            reader.Close();
            connection.Close();

            return;
        }
    }
    //--------------------------------------------------------------------------------
    private void signin()
    {
//		this.Response.Write(this.Request.Form["cookieEnabled"] );return ;
        string username = this.Request.Form["username"].Trim() + "" ;
        string password = this.Request.Form["password"] + "";
        string weblog = this.Request.Form["weblog"].ToLower().Trim() + "";
        bool cookieEnabled = false;
        if (this.Request.Form["cookieEnabled"] != null)
//            if (this.Request.Form["cookieEnabled"].Trim().ToLower() == "true")
            if (this.Request.Form["cookieEnabled"].Trim().ToLower() == "on")
                cookieEnabled = true;

        if ((username.IndexOf("'") >= 0) || (password.IndexOf("'") >= 0) || (weblog.IndexOf("'") >=0))
        {
            WriteStringToAjaxRequest("!بچه بازی در نیار بابا");
            return;
        }

        if (weblog == "")
        {

            db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName, constants.SQLUsername, constants.SQLPassowrd);
            int result = d.LoginPage(username, password, this);
            d.Dispose();
            if (result == -3)
            {
                WriteStringToAjaxRequest(".خطایی در سرور رخ داد");
                return;
            }
            if (result == -1)
            {
                //WriteStringToAjaxRequest(".نام کاربری یا کلمه عبور اشتباه است");
                this.Response.Redirect("/services/?i=unauthorized", true);
                return;
            }
            if (result == 0)
            {
                SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

                IpLog(_SigninSessionInfo.BlogID);

                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = String.Format("SELECT id FROM {0} WHERE BlogID={1} AND username='{2}';", constants.SQLTeamWeblogName, _SigninSessionInfo.BlogID, _SigninSessionInfo.Username);

                SqlDataReader reader = command.ExecuteReader();
                Int64 _AuthorID = -1;
                if (!reader.HasRows)
                {
                    reader.Close();
                    //connection.Close();

                    //connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                    //connection.Open();
                    //command = connection.CreateCommand();
                    //command.Connection = connection;
                    command.CommandText = String.Format("INSERT INTO {0} (BlogID,subdomain,username,password,email,name,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess) VALUES(@BlogID,@subdomain,@username,@password,@email,@name,1,1,1,1,1,1,1,1,1,1);SELECT id FROM {0} WHERE BlogID=@BlogID AND username=@username;", constants.SQLTeamWeblogName);
                    SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
                    SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
                    SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
                    SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
                    SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.VarChar);
                    SqlParameter NameParam = new SqlParameter("@name", SqlDbType.NVarChar);

                    BlogIDParam.Value = _SigninSessionInfo.BlogID;
                    SubdomainParam.Value = _SigninSessionInfo.Subdomain;
                    UsernameParam.Value = username;
                    PasswordParam.Value = password;
                    EmailParam.Value = "";
                    NameParam.Value = "مدیر وبلاگ";


                    command.Parameters.Add(BlogIDParam);
                    command.Parameters.Add(SubdomainParam);
                    command.Parameters.Add(UsernameParam);
                    command.Parameters.Add(PasswordParam);
                    command.Parameters.Add(EmailParam);
                    command.Parameters.Add(NameParam);

                    reader = command.ExecuteReader();
                    reader.Read();
                    _AuthorID = reader.GetInt64(0);
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                }
                else
                {
                    reader.Read();
                    _AuthorID = reader.GetInt64(0);
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                }
                Session.Abandon();
                //---cookie-----
                EncryptedCookie ec = new EncryptedCookie(constants.key, constants.IV);
                this.Response.Cookies["userInfo"]["info"] = ec.EncryptWithMd5Hash(String.Format("{0},false", _AuthorID));
                this.Response.Cookies["userInfo"].Domain = constants.DomainBlog;
                if (cookieEnabled)
                {
                    //this.Response.Cookies["userInfo"]["SessionMode"] = "false";
                    this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(20);
                }
                else
                {
                    //this.Response.Cookies["userInfo"]["SessionMode"] = "true";
                    this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(constants.SessionTimeoutMinutes);
                }

                //--------------
                //WriteStringToAjaxRequest("Logined");
                this.Response.Redirect("/services/blogbuilderv1/", true);
                return;
            }
            return;
        }
        else
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT id,BlogID,ChatBoxIsEnabled,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess FROM {0},usersInfo WHERE {0}.username='{1}' AND {0}.password='{2}' AND {0}.subdomain='{3}' AND usersInfo.i=BlogID", constants.SQLTeamWeblogName, username, password, weblog);
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                connection.Close();
                command.Dispose();
                //WriteStringToAjaxRequest(".نام کاربری یا کلمه عبور اشتباه است یا شما به عنوان نویسنده این وبلاگ توسط مدیر اصلی تعریف نشده اید");
                this.Response.Redirect("/services/?i=unauthorized");
                return;
            }

            reader.Read();

            IpLog((Int64)reader["BlogID"]);

            Int64 _AuthorID = -1;
            _AuthorID = (Int64)reader["id"];

            reader.Close();
            connection.Close();
            command.Dispose();

            /*this.Session["TeamWeblogAccessInfo"] = info;
            this.Session["IsTeamWeblogSession"] = true;*/

            Session.Abandon();
            //---cookie-----
            EncryptedCookie ec = new EncryptedCookie(constants.key, constants.IV);
            this.Response.Cookies["userInfo"]["info"] = ec.EncryptWithMd5Hash(String.Format("{0},true", _AuthorID));
            this.Response.Cookies["userInfo"].Domain = constants.DomainBlog;
            if (cookieEnabled)
            {
                //this.Response.Cookies["userInfo"]["SessionMode"] = "false";
                this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(20);
            }
            else
            {
                //this.Response.Cookies["userInfo"]["SessionMode"] = "true";
                this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(constants.SessionTimeoutMinutes);
            }

            //--------------

            //WriteStringToAjaxRequest("Logined");
            this.Response.Redirect("/services/blogbuilderv1/", true);
            return;
        }
        
    }
    //--------------------------------------------------------------------------------
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
        command.Parameters["@type"].Value = 0;

        command.Parameters.Add("@ip", SqlDbType.VarChar);
        command.Parameters["@ip"].Value = this.Request.UserHostAddress;

        command.ExecuteNonQuery();
        connection.Close();
        command.Dispose();
    }
    //--------------------------------------------------------------------------------
    /*protected void login_admin_Click(object sender, System.EventArgs e)
    {
        //throw new Exception("Hello world!");
        db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
             constants.SQLUsername, constants.SQLPassowrd);
        if (this.username_admin.Text.IndexOf("'") >= 0 || this.password_admin.Text.IndexOf("'") >= 0)
        {
            Session.Abandon();
            this.Response.Redirect("/services/?i=unauthorized");
            return;
        }
        int result = d.LoginPage(this.username_admin.Text.Trim().ToLower(), this.password_admin.Text, this);
        d.Dispose();
        if (result == -3)
        {
            Session.Abandon();
            this.Response.Redirect("/services/exception.aspx?error=SQL Server");
            return;
        }
        if (result == -1)
        {
            Session.Abandon();
            this.Response.Redirect("/services/?i=unauthorized");
            return;
        }
        if (result == 1)
        {
            d.Dispose();
            this.Response.Redirect("wizard.aspx");
            return;
        }
        if (result == 0)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = String.Format("SELECT id FROM {0} WHERE BlogID={1} AND username='{2}';", constants.SQLTeamWeblogName, _SigninSessionInfo.BlogID, _SigninSessionInfo.Username);

            SqlDataReader reader = command.ExecuteReader();
            Int64 _AuthorID = -1;
            if (!reader.HasRows)
            {
                reader.Close();
                //connection.Close();

                //connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                //connection.Open();
                //command = connection.CreateCommand();
                //command.Connection = connection;
                command.CommandText = String.Format("INSERT INTO {0} (BlogID,subdomain,username,password,email,name,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess) VALUES(@BlogID,@subdomain,@username,@password,@email,@name,1,1,1,1,1,1,1,1,1,1);SELECT id FROM {0} WHERE BlogID=@BlogID AND username=@username;", constants.SQLTeamWeblogName);
                SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
                SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
                SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
                SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
                SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.VarChar);
                SqlParameter NameParam = new SqlParameter("@name", SqlDbType.NVarChar);

                BlogIDParam.Value = _SigninSessionInfo.BlogID;
                SubdomainParam.Value = _SigninSessionInfo.Subdomain;
                UsernameParam.Value = this.username_admin.Text.Trim().ToLower();
                PasswordParam.Value = this.password_admin.Text;
                EmailParam.Value = "";
                NameParam.Value = "مدیر وبلاگ";


                command.Parameters.Add(BlogIDParam);
                command.Parameters.Add(SubdomainParam);
                command.Parameters.Add(UsernameParam);
                command.Parameters.Add(PasswordParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(NameParam);

                reader = command.ExecuteReader();
                reader.Read();
                _AuthorID = reader.GetInt64(0);
                reader.Close();
                connection.Close();
                command.Dispose();
            }
            else
            {
                reader.Read();
                _AuthorID = reader.GetInt64(0);
                reader.Close();
                connection.Close();
                command.Dispose();
            }
            Session.Abandon();
            //---cookie-----
            EncryptedCookie ec = new EncryptedCookie(constants.key, constants.IV);
            this.Response.Cookies["userInfo"]["info"] = ec.EncryptWithMd5Hash(String.Format("{0},false", _AuthorID));
            this.Response.Cookies["userInfo"].Domain = constants.DomainBlog;
            if (this.cookieEnabled.Checked)
            {
                //this.Response.Cookies["userInfo"]["SessionMode"] = "false";
                this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(2);
            }
            else
            {
                //this.Response.Cookies["userInfo"]["SessionMode"] = "true";
                this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(constants.SessionTimeoutMinutes);
            }

            //--------------
            this.Response.Redirect("blogbuilderv1/cp.aspx");
            return;
        }
    }*/
    //--------------------------------------------------------------------------------
    protected void login_team_Click(object sender, System.EventArgs e)
    {
        /*if (this.username_team.Text.IndexOf("'") >= 0 || this.password_team.Text.IndexOf("'") >= 0 || this.weblog_team.Text.IndexOf("'") >=0)
        {
            Session.Abandon();
            this.Response.Redirect("/services/?i=unauthorized");
            return;
        }
        SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.Connection = connection;
        command.CommandText = string.Format("SELECT id,BlogID,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess FROM {0},usersInfo WHERE {0}.username='{1}' AND {0}.password='{2}' AND {0}.subdomain='{3}' AND usersInfo.i=BlogID", constants.SQLTeamWeblogName, this.username_team.Text, this.password_team.Text, this.weblog_team.Text);
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            //connection.Close();
            command.Dispose();
            Session.Abandon();
            this.Response.Redirect("/services/?i=unauthorized", true);
            return;
        }

        reader.Read();

        TeamWeblogAccessInfo info = new TeamWeblogAccessInfo();

        this.Session["username"] = this.username_team.Text;
		this.Session["subdomain"] = this.weblog_team.Text;
        this.Session["AuthorID"] = (Int64)reader["id"];
        this.Session["id"] = (Int64)reader["BlogID"];
        info.PostAccess = (bool)reader["PostAccess"];
        info.SubjectedArchiveAccess = (bool)reader["SubjectedArchiveAccess"];
        info.WeblogLinksAccess = (bool)reader["WeblogLinksAccess"];
        info.DailyLinksAccess = (bool)reader["DailyLinksAccess"];
        info.TemplateAccess = (bool)reader["TemplateAccess"];
        info.NewsletterAccess = (bool)reader["NewsletterAccess"];
        info.OthersPostAccess = (bool)reader["OthersPostAccess"];
        info.LinkBoxAccess = (bool)reader["LinkBoxAccess"];
        info.PollAccess = (bool)reader["PollAccess"];
        info.FullAccess = (bool)reader["FullAccess"];

        reader.Close();
        //connection.Close();
        command.Dispose();

        this.Session["TeamWeblogAccessInfo"] = info;
        this.Session["IsTeamWeblogSession"] = true;

        this.Response.Redirect("blogbuilderv1/PostAdmin.aspx", true);
        return ;*/
    }
    //--------------------------------------------------------------------------------
    private void WriteStringToAjaxRequest(string str)
    {
        this.Response.Write(str);
        this.Response.Flush();
        //this.Response.Close();
        this.Response.End();
    }
    //--------------------------------------------------------------------------------
}
