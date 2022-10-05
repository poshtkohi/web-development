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
    public partial class messenger : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login.IsMyCp(this))
                return;
            MetaCopyrightControl();
            SetSiteFooterControl();

            //LoadMobileNumber_MobilePage_proc();
            LoadYahooId_MessengerPage_proc();
            return;
        }
        //--------------------------------------------------------------------
        private void LoadYahooId_MessengerPage_proc()
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "LoadMobileNumber_MobilePage_proc";
            command.CommandText = "LoadYahooId_MessengerPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@YahooId", SqlDbType.NVarChar, 20);
            command.Parameters["@YahooId"].Direction = ParameterDirection.Output;

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
                this.YahooId.Text = (string)command.Parameters["@YahooId"].Value;
                this.YahooId.Enabled = false;
                this.add.Enabled = false;
                this.delete.Enabled = true;
                //this.message.Text = String.Format("برای به روز کردن میکرپیغامک خود متن پست تان را به شماره {0} اس ام اس کنید.", Constants.MobileNumber);
                //this.message.Text = String.Format("برای به روز کردن میکرپیغامک خود پيغامك خود را به شناسه ياهو با نام {0} بفرستيد.", Constants.YahooId);
                this.message.Text = String.Format("برای به روز کردن میکرپیغامک خود پيغامك خود را به شناسه ياهو با نام {0} بفرستيد.", "peyghamak");
                this.message.Visible = true;
                goto End;
            }
            if (!_IsExisted && !_HasAlreadyVerified)
            {
                ValidtaionEnabler(true);
                this.YahooId.Text = "";
                this.YahooId.Enabled = true;
                this.add.Enabled = true;
                this.delete.Enabled = false;
                this.message.Text = "برای دریافت کلمه تایید شناسه ياهوي خود را وارد كنيد.";
                this.message.Visible = true;
                goto End;
            }
            if (_IsExisted && !_HasAlreadyVerified)
            {
                ValidtaionEnabler(false);
                this.YahooId.Text = (string)command.Parameters["@YahooId"].Value;
                this.YahooId.Enabled = false;
                this.add.Enabled = false;
                this.delete.Enabled = true;
                this.message.Text = String.Format("برای تایید ابتدا {0} را به ليست دوستان خود در ياهو اضافه كنيد و سپس {1} را به آن بفرستيد.",
                    //String.Format("<font color='red' size='2pt'>{0}</font>", Constants.YahooId), String.Format("<font color='red' size='2pt'>{0}</font>", (string)command.Parameters["@VerificationCode"].Value));
                    String.Format("<font color='red' size='2pt'>{0}</font>", "peyghamak"), String.Format("<font color='red' size='2pt'>{0}</font>", (string)command.Parameters["@VerificationCode"].Value));
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
                if (this.Request.Form["YahooId"] == null || this.Request.Form["YahooId"] == "")
                {
                    this.message.Text = "شناسه ياهو خالي است.";
                    this.message.Visible = true;
                    return;
                }
                else
                {
                    Regex rex = new Regex(@"[a-zA-Z0-9_.]*");
                    if (!rex.IsMatch(this.Request.Form["YahooId"]))
                    {
                        this.message.Text = "شناسه ياهو نامعتبر است، مثال: yahooid";
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
                //command.CommandText = "AddMobileNumber_MobilePage_proc";
                command.CommandText = "AddYahooId_MessengerPage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

                command.Parameters.Add("@YahooId", SqlDbType.NVarChar, 20);
                command.Parameters["@YahooId"].Value = this.Request.Form["YahooId"];

                command.Parameters.Add("@VerificationCode", SqlDbType.VarChar, 10);
                command.Parameters["@VerificationCode"].Value = _verificationCode;

                command.Parameters.Add("@IsOwnedByAnotherUser", SqlDbType.Bit);
                command.Parameters["@IsOwnedByAnotherUser"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                if (!(bool)command.Parameters["@IsOwnedByAnotherUser"].Value)
                {
                    this.YahooId.Text = this.Request.Form["YahooId"];
                    this.add.Enabled = false;
                    this.delete.Enabled = true;
                    this.YahooId.Enabled = false;
                    this.message.Text = String.Format("برای تایید ابتدا {0} را به ليست دوستان خود در ياهو اضافه كنيد و سپس {1} را به آن بفرستيد.",
                        //String.Format("<font color='red' size='2pt'>{0}</font>", Constants.YahooId), String.Format("<font color='red' size='2pt'>{0}</font>", (string)command.Parameters["@VerificationCode"].Value));
                        String.Format("<font color='red' size='2pt'>{0}</font>", "peyghamak"), String.Format("<font color='red' size='2pt'>{0}</font>", (string)command.Parameters["@VerificationCode"].Value));
                    this.message.Visible = true;
                    ValidtaionEnabler(false);

                    command.Dispose();
                    connection.Close();

                    return;
                }
                else
                {
                    this.message.Text = String.Format("شناسه ياهو {0} قبلا ثبت شده است. شناسه دیگری وارد کنید.",
                        String.Format("<font color='red' size='2pt'>{0}</font>", this.Request.Form["YahooId"]));
                    this.message.Visible = true;
                    this.YahooId.Text = "";
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
                //command.CommandText = "DeleteMobileNumber_MobilePage_proc";
                command.CommandText = "DeleteYahooId_MessengerPage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

                command.ExecuteNonQuery();

                this.YahooId.Text = "";
                this.add.Enabled = true;
                this.delete.Enabled = false;
                this.YahooId.Enabled = true;
                this.message.Text = "برای دریافت کلمه تایید شناسه ياهوي خود را وارد كنيد.";
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
            string text = letters[(new Random(unchecked((int)DateTime.Now.Second * 2))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Minute))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Millisecond))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.Ticks * 6))).Next(26)].ToString();
            text += letters[(new Random(unchecked((int)DateTime.Now.TimeOfDay.Ticks))).Next(26)].ToString();
            return text;
        }
        //--------------------------------------------------------------------
    }
}
