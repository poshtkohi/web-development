/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

namespace Peyghamak
{
    public partial class ForgottenPassword : System.Web.UI.Page
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            MetaCopyrightControl();
            SetSiteFooterControl();
        }
        //--------------------------------------------------------------------
        protected void submit_Click(object sender, EventArgs e)
        {
            string _username = this.Request.Form["username"].Trim().ToLower();
            //------------
            if (_username == null || _username == "")
            {
                this.message.Text = "نام کاربری خالی است.";
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
            command.CommandText = "ForgottenPasswordPage_proc";

            command.Parameters.Add("@username", SqlDbType.NVarChar);
            command.Parameters["@username"].Value = _username;

            SqlDataReader reader = command.ExecuteReader();

            string _email = null;
            string _password = null;

            if (reader.Read())
            {
                _email = (string)reader["email"];
                _password = (string)reader["password"];
            }

            command.Dispose();
            connection.Close();

            if (_email != null)
                SendForgottenPassword(_username, _password, _email);

            this.Response.Redirect("signin.aspx?mode=sent", true);
            return;

        }
        //--------------------------------------------------------------------------------
        private static void SendForgottenPassword(string _username, string _password, string _email)
        {
            MailMessage MailSender = new MailMessage(Constants.AdminEmail, _email);
            MailSender.BodyEncoding = System.Text.Encoding.UTF8;
            MailSender.IsBodyHtml = true;
            MailSender.Priority = MailPriority.High;
            MailSender.Subject = "Forgotten Password for Peyghamak.com Users";
            MailSender.Body = String.Format("Username: {0}<br>Password: {1}", _username, _password);
            /*string domain = _email.Substring(_email.IndexOf("@") + 1);
            String[] mxs = DnsMx.GetMXRecords(domain);
            SmtpClient client = new SmtpClient(mxs[0]);*/
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            System.Net.NetworkCredential _SMTPUserInfo = new System.Net.NetworkCredential(Constants.AdminEmail, Constants.AdminEmailPassword);
            client.UseDefaultCredentials = false;
            client.Credentials = _SMTPUserInfo;
            client.EnableSsl = true; 
            client.Send(MailSender);
            ///*mxs = null;
            client = null;
        }
        //--------------------------------------------------------------------
    }
}
