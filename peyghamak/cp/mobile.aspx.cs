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
using System.Text.RegularExpressions;

namespace Peyghamak.cp
{
    public partial class mobile : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login.IsMyCp(this))
                return;
            MetaCopyrightControl();
            SetSiteFooterControl();

            LoadMobileNumber_MobilePage_proc();
            return;
        }
        //--------------------------------------------------------------------
        private void LoadMobileNumber_MobilePage_proc()
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "LoadMobileNumber_MobilePage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 20);
            command.Parameters["@MobileNumber"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@VerificationCode", SqlDbType.VarChar, 10);
            command.Parameters["@VerificationCode"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@IsExisted", SqlDbType.Bit);
            command.Parameters["@IsExisted"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@HasAlreadyVerified", SqlDbType.Bit);
            command.Parameters["@HasAlreadyVerified"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            bool _IsExisted = (bool)command.Parameters["@IsExisted"].Value;
            bool _HasAlreadyVerified = (bool)command.Parameters["@HasAlreadyVerified"].Value;
            if (_IsExisted && _HasAlreadyVerified)
            {
                ValidtaionEnabler(false);
                this.MobileNumber.Text = (string)command.Parameters["@MobileNumber"].Value;
                this.MobileNumber.Enabled = false;
                this.add.Enabled = false;
                this.delete.Enabled = true;
                this.message.Text = String.Format("برای به روز کردن میکرپیغامک خود متن پست تان را به شماره {0} اس ام اس کنید.", Constants.MobileNumber);
                this.message.Visible = true;
                goto End;
            }
            if (!_IsExisted && !_HasAlreadyVerified)
            {
                ValidtaionEnabler(true);
                this.MobileNumber.Text = "";
                this.MobileNumber.Enabled = true;
                this.add.Enabled = true;
                this.delete.Enabled = false;
                this.message.Text = "برای دریافت کلمه تایید شماره موبایل خود را وارد کنید.";
                this.message.Visible = true;
                goto End;
            }
            if (_IsExisted && !_HasAlreadyVerified)
            {
                ValidtaionEnabler(false);
                this.MobileNumber.Text = (string)command.Parameters["@MobileNumber"].Value;
                this.MobileNumber.Enabled = false;
                this.add.Enabled = false;
                this.delete.Enabled = true;
                this.message.Text = String.Format("برای تایید موبایل خود کد {1} را به شماره {0} اس ام اس کنید.",
                    String.Format("<font color='red' size='2pt'>{0}</font>", Constants.MobileNumber), String.Format("<font color='red' size='2pt'>{0}</font>", (string)command.Parameters["@VerificationCode"].Value));
                this.message.Visible = true;
                goto End;
            }
        End:
            command.Dispose();
            connection.Close();
            return;
        }
        //--------------------------------------------------------------------
        protected void add_Click(object sender, EventArgs e)
        {
            if (this.add.Enabled)
            {
                if (this.Request.Form["MobileNumber"] == null || this.Request.Form["MobileNumber"] == "")
                {
                    this.message.Text = "شماره موبایل خالی است.";
                    this.message.Visible = true;
                    return;
                }
                else
                {
                    Regex rex = new Regex(@"(09)?[0-9]{9}");
                    if (!rex.IsMatch(this.Request.Form["MobileNumber"]))
                    {
                        this.message.Text = "شماره موبایل نامعتبر است، مثال: 09127852176";
                        this.message.Visible = true;
                        return;
                    }
                }

                string _verificationCode = RandomText().ToLower();

                SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddMobileNumber_MobilePage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

                command.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 20);
                command.Parameters["@MobileNumber"].Value = this.Request.Form["MobileNumber"];

                command.Parameters.Add("@VerificationCode", SqlDbType.VarChar, 10);
                command.Parameters["@VerificationCode"].Value = _verificationCode;

                command.Parameters.Add("@IsOwnedByAnotherUser", SqlDbType.Bit);
                command.Parameters["@IsOwnedByAnotherUser"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                if (!(bool)command.Parameters["@IsOwnedByAnotherUser"].Value)
                {
                    this.MobileNumber.Text = this.Request.Form["MobileNumber"];
                    this.add.Enabled = false;
                    this.delete.Enabled = true;
                    this.MobileNumber.Enabled = false;
                    this.message.Text = String.Format("برای تایید موبایل خود کد {1} را به شماره {0} اس ام اس کنید.",
                        String.Format("<font color='red' size='2pt'>{0}</font>", Constants.MobileNumber), String.Format("<font color='red' size='2pt'>{0}</font>", (string)command.Parameters["@VerificationCode"].Value));
                    this.message.Visible = true;
                    ValidtaionEnabler(false);

                    command.Dispose();
                    connection.Close();

                    return;
                }
                else
                {
                    this.message.Text = String.Format("شماره موبایل {0} قبلا ثبت شده است. شماره دیگری وارد کنید.",
                        String.Format("<font color='red' size='2pt'>{0}</font>", this.Request.Form["MobileNumber"]));
                    this.message.Visible = true;
                    this.MobileNumber.Text = "";
                    ValidtaionEnabler(true);

                    command.Dispose();
                    connection.Close();

                    return;
                }
            }
        }
        //--------------------------------------------------------------------
        protected void delete_Click(object sender, EventArgs e)
        {
            if (this.delete.Enabled)
            {
                SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeleteMobileNumber_MobilePage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

                command.ExecuteNonQuery();

                this.MobileNumber.Text = "";
                this.add.Enabled = true;
                this.delete.Enabled = false;
                this.MobileNumber.Enabled = true;
                this.message.Text = "برای دریافت کلمه تایید شماره موبایل خود را وارد کنید.";
                this.message.Visible = true;
                ValidtaionEnabler(true);

                command.Dispose();
                connection.Close();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ValidtaionEnabler(bool enable)
        {
            this.RequiredFieldValidator3.Visible = enable;
            this.RequiredFieldValidator3.Enabled = enable;
            this.RegularExpressionValidator2.Visible = enable;
            this.RegularExpressionValidator2.Enabled = enable;
            return;
        }
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            this.SiteFooterSection.Controls.Add(LoadControl("../SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            this.MetaCopyrightSection.Controls.Add(LoadControl("../MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private static string RandomText()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string text = letters[(new Random(unchecked((int)DateTime.Now.Second*2))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Minute))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Millisecond))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Ticks*6))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.TimeOfDay.Ticks))).Next(26)].ToString();
            return text;
        }
        //--------------------------------------------------------------------
    }
}