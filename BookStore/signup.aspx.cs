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
using System.Net;
using System.Net.Mail;

namespace bookstore
{
    public partial class signup : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Login.IsLoginProc(this))
            {
                this.Response.Redirect(Login.BuildUserUrl(((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username), true);
                return;
            }*/
            if (this.BirthYear.Items.Count == 1)
            {
                for (int i = 1380; i >= 1300; i--)
                    this.BirthYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
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
            if (_username == "admin" || _username == "www" || _username == "mail"
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
            if (this.Request.Form["address"] == null || this.Request.Form["address"] == "")
            {
                this.message.Text = "آدرس خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["PostalCode"] == null || this.Request.Form["PostalCode"] == "")
            {
                this.message.Text = "کد پستی خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["tel"] == null || this.Request.Form["tel"] == "")
            {
                this.message.Text = "شماره تماس خالی است.";
                this.message.Visible = true;
                return;
            }
            rex = new Regex(@"[0-9]{6,}");
            if (!rex.IsMatch(this.Request.Form["tel"]))
            {
                this.message.Text = "شماره تماس نامعتبر است.";
                this.message.Visible = true;
                return;
            }
            //rex = new Regex(@"[0-9]{6,}");
            if (!rex.IsMatch(this.Request.Form["PostalCode"]))
            {
                this.message.Text = "کد پستی نامعتبر است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["BirthYear"] != null && this.Request.Form["BirthYear"] != "" && this.Request.Form["BirthYear"] == "none")
            {
                this.message.Text = "سال تولد خود را انتخاب کنید.";
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

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT count(*) FROM accounts WHERE username='{0}' AND IsDeleted=0", _username);
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
                command.CommandText = string.Format("SELECT count(*) FROM [accounts-preverify] WHERE username='{0}' AND IsDeleted=0", _username);
                reader = command.ExecuteReader();
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

                    string _guid = Guid.NewGuid().ToString().Replace("-", "");
                    command.CommandText = "INSERT INTO [accounts-preverify] (username,password,name,email,address,PostalCode,VerificationCode,date,tel,BirthYear,sex,CountryKey,ProvinceKey) VALUES(@username,@password,@name,@email,@address,@PostalCode,@VerificationCode,GETDATE(),@tel,@BirthYear,@sex,@CountryKey,@ProvinceKey)";


                    command.Parameters.Add("@username", SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = _username;

                    command.Parameters.Add("@password", SqlDbType.NVarChar);
                    command.Parameters["@password"].Value = this.Request.Form["password"];

                    command.Parameters.Add("@name", SqlDbType.NVarChar);
                    command.Parameters["@name"].Value = this.Request.Form["name"];

                    command.Parameters.Add("@tel", SqlDbType.NVarChar);
                    command.Parameters["@tel"].Value = this.Request.Form["tel"];

                    command.Parameters.Add("@email", SqlDbType.NVarChar);
                    command.Parameters["@email"].Value = this.Request.Form["email"].ToLower().Trim();

                    command.Parameters.Add("@address", SqlDbType.NVarChar);
                    command.Parameters["@address"].Value = this.Request.Form["address"];

                    command.Parameters.Add("@PostalCode", SqlDbType.NVarChar);
                    command.Parameters["@PostalCode"].Value = this.Request.Form["PostalCode"];

                    command.Parameters.Add("@VerificationCode", SqlDbType.VarChar);
                    command.Parameters["@VerificationCode"].Value = _guid;

                    command.Parameters.Add("@BirthYear", SqlDbType.Int);
                    command.Parameters["@BirthYear"].Value = Convert.ToInt32(this.Request.Form["BirthYear"]);

                    command.Parameters.Add("@sex", SqlDbType.Bit);
                    command.Parameters["@sex"].Value = Convert.ToBoolean(this.Request.Form["sex"]);

                    command.Parameters.Add("@CountryKey", SqlDbType.VarChar);
                    command.Parameters["@CountryKey"].Value = this.Request.Form["CountryKey"];

                    command.Parameters.Add("@ProvinceKey", SqlDbType.VarChar);
                    command.Parameters["@ProvinceKey"].Value = this.Request.Form["ProvinceKey"];


                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();

                    //here, email the user account information

                    //this.Response.Redirect(String.Format("http://www.{0}/signin.aspx", Constants.BlogDomain), true);

                    this.name.Text = "";
                    this.username.Text = "";
                    this.address.Text = "";
                    this.PostalCode.Text = "";
                    this.email.Text = "";
                    this.tel.Text = "";
                    this.SecurityCode.Text = "";
                    SendForgottenPassword(_guid, this.Request.Form["email"]);
                    this.message.Text = "لینک فعال سازی عضویت به ایمیلتان ارسال شد.<br><span style='font-family: tahoma;font-size: 10pt' dir='rtl'>&nbsp;به Inbox ایمیل خود مراجعه کنید در صورت نیافتن کد تایید به بخش Bulk و یا Spam ایمیل خود مراجعه نمایید.</span>";
                    this.message.Visible = true;
                    return;
                }
            }
        }
        //--------------------------------------------------------------------
        private static void SendForgottenPassword(string _guid, string _email)
        {
            MailMessage MailSender = new MailMessage(constants.Email, _email);
            MailSender.BodyEncoding = System.Text.Encoding.UTF8;
            MailSender.IsBodyHtml = true;
            MailSender.Priority = MailPriority.High;
            MailSender.Subject = "Acitivation Code of BookStore Registeration";
            MailSender.Body = String.Format("Please click on this link: <a href=\"http://www.bookstore/signin.aspx?_ref={0}\">http://www.bookstore/signin.aspx?_ref={0}</a>", _guid);

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
