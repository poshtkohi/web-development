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


namespace bookstore.cp
{
    public partial class password : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.IsLoginProc(this))
            {
                this.Response.Redirect("/?i=logouted", true);
                return;
            }
            PageSettings();
        }
        //--------------------------------------------------------------------
        private void PageSettings()
        {
            UserMenuControlLoad();
            LoginControlLoad();
        }
        //--------------------------------------------------------------------
        private void UserMenuControlLoad()
        {
            this.UserMenuControl.Controls.Add(LoadControl("UserMenuControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void LoginControlLoad()
        {
            this.LoginControl.Controls.Add(LoadControl("LoginControl.ascx"));
            return;
        }
        //-----------------------------------------------------
        protected void save_Click(object sender, EventArgs e)
        {
            if (this.Request.Form["LastPassword"] == null || this.Request.Form["LastPassword"] == "")
            {
                this.message.Text = "کلمه عبور قدیمی خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["NewPassword"] != this.Request.Form["ConfirmNewPassword"])
            {
                this.message.Text = "کلمه عبور جدید با تکرار کلمه عبور جدید برابر نیست.";
                this.message.Visible = true;
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "ChangePassword_PasswordPage_proc";
            command.CommandType = CommandType.StoredProcedure;

            //--------------------
            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];

            command.Parameters.Add("@NewPassword", SqlDbType.NVarChar);
            command.Parameters["@NewPassword"].Value = this.Request.Form["NewPassword"].Trim();

            command.Parameters.Add("@LastPassword", SqlDbType.NVarChar);
            command.Parameters["@LastPassword"].Value = this.Request.Form["LastPassword"].Trim();

            command.Parameters.Add("@NumAffectedRows", SqlDbType.Int);
            command.Parameters["@NumAffectedRows"].Direction = ParameterDirection.Output;
            //--------------------

            command.ExecuteNonQuery();

            if ((int)command.Parameters["@NumAffectedRows"].Value == 1)
            {
                this.message.Text = "کلمه تایید با موفقیت به روز رسانی شد.";
                this.message.Visible = true;
            }
            else
            {
                this.message.Text = "کلمه عبور قدیمی اشتباه است.";
                this.message.Visible = true;
            }

            command.Dispose();
            connection.Close();

            return;
        }
        //--------------------------------------------------------------------
    }
}
