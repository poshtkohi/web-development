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
    public partial class account : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.IsLoginProc(this))
            {
                this.Response.Redirect("/signin.aspx?i=logouted", true);
                return;
            }
            PageSettings();
            if (!this.IsPostBack)
            {
                if (this.BirthYear.Items.Count == 1)
                {
                    for (int i = 1380; i >= 1300; i--)
                        this.BirthYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "LoadProfile_AccountPage_proc";

                command.Parameters.Add("@UserID", SqlDbType.BigInt);
                command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.name.Text = (string)reader["name"];
                    this.email.Text = (string)reader["email"];
                    this.address.Text = (string)reader["address"];
                    this.PostalCode.Text = (string)reader["PostalCode"];
                    this.tel.Text = (string)reader["tel"];
                    this.BirthYear.Items.FindByValue(reader["BirthYear"].ToString()).Selected = true;
                    this.CountryKey.Items.FindByValue((string)reader["CountryKey"]).Selected = true;
                    this.ProvinceKey.Items.FindByValue((string)reader["ProvinceKey"]).Selected = true;
                    string _sex = "true";
                    if (!(bool)reader["sex"])
                        _sex = "false";
                    this.sex.Items.FindByValue(_sex).Selected = true;

                }

                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }
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
            if (this.Request.Form["name"] == null || this.Request.Form["name"] == "")
            {
                this.message.Text = "نام خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["email"] == null || this.Request.Form["email"] == "")
            {
                this.message.Text = "آدرس ایمیل خالی است.";
                this.message.Visible = true;
                return;
            }
            Regex rex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
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


            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AccountUpdate_AccountPage_proc";


            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];

            command.Parameters.Add("@name", SqlDbType.NVarChar);
            command.Parameters["@name"].Value = this.Request.Form["name"];

            command.Parameters.Add("@email", SqlDbType.NVarChar);
            command.Parameters["@email"].Value = this.Request.Form["email"].ToLower().Trim();

            command.Parameters.Add("@address", SqlDbType.NVarChar);
            command.Parameters["@address"].Value = this.Request.Form["address"];

            command.Parameters.Add("@tel", SqlDbType.NVarChar);
            command.Parameters["@tel"].Value = this.Request.Form["tel"];

            command.Parameters.Add("@PostalCode", SqlDbType.NVarChar);
            command.Parameters["@PostalCode"].Value = this.Request.Form["PostalCode"];

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

            this.message.Text = "اطلاعات شما با موفقیت در سیستم به روز رسانی شد.";
            this.message.Visible = true;
            return;
        }
        //--------------------------------------------------------------------
    }
}
