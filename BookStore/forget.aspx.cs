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
using System.Net;
using System.Net.Mail;

namespace bookstore
{
    public partial class forget : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSettings();
        }
        //--------------------------------------------------------------------
        private void PageSettings()
        {
            LoginPanelControlLoad();
            MainMenuControlLoad();
        }
        //--------------------------------------------------------------------
        private void LoginPanelControlLoad()
        {
            this.LoginPanelControl.Controls.Add(LoadControl("LoginControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MainMenuControlLoad()
        {
            this.MainMenuControl.Controls.Add(LoadControl("MainMenuControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        protected void submit_Click(object sender, EventArgs e)
        {
            string _username = this.Request.Form["username"].Trim().ToLower();
            //------------
            if (_username == null || _username == "")
            {
                this.message.Text = ".نام کاربری خالی است";
                this.message.Visible = true;
                return;
            }
            Regex rex = new Regex(@"^[\-0-9a-zA-Z]{1,}$");
            if (!rex.IsMatch(_username))
            {
                this.message.Text = ".نام کاربری نامعتبر است";
                this.message.Visible = true;
                return;
            }
            //------------

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "FindPassword_ForgetPage_proc";

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
            MailMessage MailSender = new MailMessage(constants.Email, _email);
            MailSender.BodyEncoding = System.Text.Encoding.UTF8;
            MailSender.IsBodyHtml = true;
            MailSender.Priority = MailPriority.High;
            MailSender.Subject = "Forgotten Password for bookstore Users";
            MailSender.Body = String.Format("Username: {0}<br>Password: {1}", _username, _password);

            SmtpClient client = new SmtpClient(constants.EmailSmtpAddress);
            System.Net.NetworkCredential _SMTPUserInfo = new System.Net.NetworkCredential(constants.EmailUsername, constants.EmailPassword);
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
