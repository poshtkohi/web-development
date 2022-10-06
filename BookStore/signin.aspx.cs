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

//using Peyghamak.ServerManagement;

using System.Net;
using System.Net.Mail;

namespace bookstore
{
    public partial class signin : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            string _ref = this.Request.QueryString["_ref"];
            if (_ref != null)
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SigninVerification_SigninPage_proc";

                command.Parameters.Add("@_ref", SqlDbType.VarChar);
                command.Parameters["@_ref"].Value = _ref;

                command.Parameters.Add("@IsVerified", SqlDbType.Bit);
                command.Parameters["@IsVerified"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                if ((bool)command.Parameters["@IsVerified"].Value)
                {
                    command.Dispose();
                    connection.Close();

                    this.message.Text = ".عملیات تایید انجام شد. نام کاربری و کلمه عبور خود را وارد نمایید";
                    this.message.Visible = true;
                    return;
                }
                else
                {
                    command.Dispose();
                    connection.Close();
                    this.message.Text = ".کد تایید اشتباه است";
                    this.message.Visible = true;
                    return;
                }
            }
            if (this.Request.QueryString["mode"] != null && this.Request.QueryString["mode"] == "sent")
            {
                this.message.Text = ".کلمه عبورتان به آدرس ایمیلتان ارسال شد";
                this.message.Visible = true;
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
            if (this.Request.Form["password"] == null || this.Request.Form["password"] == "")
            {
                this.message.Text = ".کلمه عبور خالی است";
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
            command.CommandText = "Signin_SigninPage_proc";

            command.Parameters.Add("@username", SqlDbType.NVarChar);
            command.Parameters["@username"].Value = _username;

            command.Parameters.Add("@password", SqlDbType.NVarChar);
            command.Parameters["@password"].Value = this.Request.Form["password"];

            command.Parameters.Add("@IsLogined", SqlDbType.Bit);
            command.Parameters["@IsLogined"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Direction = ParameterDirection.Output;


            command.ExecuteNonQuery();

            if ((bool)command.Parameters["@IsLogined"].Value)
            {
                this.Session["UserID"] = (Int64)command.Parameters["@UserID"].Value;
                this.Session["username"] = _username;
                this.Session["IsLogined"] = true;
                //--------------
                command.Dispose();
                connection.Close();

                this.Response.Redirect("/", true);
                return;
            }
            else
            {
                command.Dispose();
                connection.Close();
                this.message.Text = ".نام کاربری یا کلمه عبور اشتباه است";
                this.message.Visible = true;
                this.username.Text = "";
                this.password.Text = "";
                return;
            }
        }
        //--------------------------------------------------------------------
    }
}
