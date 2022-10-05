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
using System.Web.Mail;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class SendNewsletter : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                TeamWeblogAccessInfo info = (TeamWeblogAccessInfo)this.Session["TeamWeblogAccessInfo"];
                if (info.FullAccess)
                    return true;
                if (info.NewsletterAccess)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        //--------------------------------------------------------------------------------
        protected void Page_Load(object sender, System.EventArgs e)
        {
            bool _IsLogin = Common.IsLoginProc(this);
            if (!_IsLogin)
            {
                if (this.Request.Form["mode"] != null)
                    Common.WriteStringToAjaxRequest("Logouted", this);
                else
                    this.Response.Redirect("Logouted.aspx", true);
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }
        }
        //--------------------------------------------------------------------------------
        protected void submit_Click(object sender, EventArgs e)
        {
            if (this.content.Text.Trim() == "")
            {
                this.message.Text = ".عنوان متن ارسالی خبر نامه خالی است";
                this.message.Visible = true;
                return;
            }
            if (this.subject.Text.Length > 200)
            {
                this.message.Text = ".تعداد حروف عنوان نمیتواند از 200 حرف بیشتر باشد";
                this.message.Visible = true;
                return;
            }
            if (this.content.Text.Length > 40960)
            {
                this.message.Text = ".حجم متن نمی تواند از 40 کیلو بایت بیشتر باشد";
                this.message.Visible = true;
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT email FROM {0} WHERE BlogID={1} AND IsDeleted=0", constants.SQLNewsletterTableName, _SigninSessionInfo.BlogID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                MailMessage _MailSender = new MailMessage();
                _MailSender.BodyEncoding = System.Text.Encoding.UTF8;
                _MailSender.BodyFormat = MailFormat.Html;
                _MailSender.Priority = MailPriority.High;
                _MailSender.From = "no-reply@iranblog.com";
                _MailSender.Subject = this.subject.Text;
                _MailSender.Body = this.content.Text;
                _MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication   
                _MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", constants.IranBlogWelcomeEmailUsername); //set your username here  
                _MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", constants.IranBlogWelcomeEmailPassword);
                if (reader.Read())
                    _MailSender.To = (string)reader["email"];
                while (reader.Read())
                    _MailSender.Bcc += String.Format("{0};", (string)reader["email"]);

                reader.Close();
                command.Dispose();
                connection.Close();

                SmtpMail.SmtpServer = constants.IranBlogWelcomeEmailSmtpAddress;
                SmtpMail.Send(_MailSender);

                this.message.Text = ".خبرنامه شما با موفقیت به همه اعضای خبرنامه ارسال شد";
                this.message.Visible = true;
                this.subject.Text = "";
                this.content.Text = "";
                return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Response.Redirect("NewsLetter.aspx", true);
                return;
            }
        }
        //--------------------------------------------------------------------------------
    }
}
