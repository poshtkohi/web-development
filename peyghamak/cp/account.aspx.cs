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
    public partial class account : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login.IsMyCp(this))
                return;
            MetaCopyrightControl();
            SetSiteFooterControl();
            if (!this.IsPostBack)
            {
                if (this.BirthYear.Items.Count == 1)
                {
                    for (int i = 1380; i >= 1300; i--)
                        this.BirthYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    for (int i = 12; i >= 1; i--)
                        this.BirthMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    for (int i = 31; i >= 1; i--)
                        this.BirthDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "LoadProfile_AccountPage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                this.name.Text = (string)reader["name"];
                this.email.Text = (string)reader["email"];
                this.BirthYear.Items.FindByValue(reader["BirthYear"].ToString()).Selected = true;
                this.BirthMonth.Items.FindByValue(reader["BirthMonth"].ToString()).Selected = true;
                this.BirthDay.Items.FindByValue(reader["BirthDay"].ToString()).Selected = true;
                this.CountryKey.Items.FindByValue((string)reader["CountryKey"]).Selected = true;
                this.ProvinceKey.Items.FindByValue((string)reader["ProvinceKey"]).Selected = true;
                this.city.Text = (string)reader["city"];
                string _sex = "true";
                if (!(bool)reader["sex"])
                    _sex = "false";
                this.sex.Items.FindByValue(_sex).Selected = true;
                this.about.Text = (string)reader["about"];
                this.url.Text = (string)reader["url"];
                if ((bool)reader["ShowAgeEnabled"])
                {
                    this.ShowAgeEnabled.Checked = true;
                    this.ShowAgeDisabled.Checked = false;
                }
                else
                {
                    this.ShowAgeEnabled.Checked = false;
                    this.ShowAgeDisabled.Checked = true;
                }
                if ((bool)reader["ShowCityEnabled"])
                {
                    this.ShowCityEnabled.Checked = true;
                    this.ShowCityDisabled.Checked = false;
                }
                else
                {
                    this.ShowCityEnabled.Checked = false;
                    this.ShowCityDisabled.Checked = true;
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }
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
            if (this.Request.Form["BirthYear"] != null && this.Request.Form["BirthYear"] != "" && this.Request.Form["BirthYear"] == "none")
            {
                this.message.Text = "سال تولد خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["BirthMonth"] != null && this.Request.Form["BirthMonth"] != "" && this.Request.Form["BirthMonth"] == "none")
            {
                this.message.Text = "ماه تولد خود را انتخاب کنید.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["BirthDay"] != null && this.Request.Form["BirthDay"] != "" && this.Request.Form["BirthDay"] == "none")
            {
                this.message.Text = "روز تولد خود را انتخاب کنید.";
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
            if (this.Request.Form["city"] == null || this.Request.Form["city"] == "")
            {
                this.message.Text = "شهر خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["url"] != null && this.Request.Form["url"] != "" && this.Request.Form["url"].ToLower() != "http://")
            {
                rex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
                if (!rex.IsMatch(this.Request.Form["url"].ToLower()))
                {
                    this.message.Text = "آدرس وب نامعتبر است.";
                    this.message.Visible = true;
                    return;
                }
            }

            SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("UPDATE {0} SET name=@name,email=@email,BirthYear=@BirthYear,BirthMonth=@BirthMonth,BirthDay=@BirthDay,sex=@sex,CountryKey=@CountryKey,ProvinceKey=@ProvinceKey,city=@city,about=@about,url=@url,ShowAgeEnabled=@ShowAgeEnabled,ShowCityEnabled=@ShowCityEnabled WHERE id={1}", Constants.AccountsTableName, _info.BlogID);

            SqlParameter NameParam = new SqlParameter("@name", SqlDbType.NVarChar);
            SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.NVarChar);
            SqlParameter BirthYearParam = new SqlParameter("@BirthYear", SqlDbType.Int);
            SqlParameter BirthMonthParam = new SqlParameter("@BirthMonth", SqlDbType.Int);
            SqlParameter BirthDayParam = new SqlParameter("@BirthDay", SqlDbType.Int);
            SqlParameter SexParam = new SqlParameter("@sex", SqlDbType.Bit);
            SqlParameter CountryKeyParam = new SqlParameter("@CountryKey", SqlDbType.VarChar);
            SqlParameter ProvinceKeyParam = new SqlParameter("@ProvinceKey", SqlDbType.VarChar);
            SqlParameter CityParam = new SqlParameter("@city", SqlDbType.NVarChar);
            SqlParameter AboutParam = new SqlParameter("@about", SqlDbType.NVarChar);
            SqlParameter UrlParam = new SqlParameter("@url", SqlDbType.NVarChar);
            SqlParameter ShowAgeEnabledParam = new SqlParameter("@ShowAgeEnabled", SqlDbType.Bit);
            SqlParameter ShowCityEnabledParam = new SqlParameter("@ShowCityEnabled", SqlDbType.Bit);


            NameParam.Value = this.Request.Form["name"].Trim();
            EmailParam.Value = this.Request.Form["email"].Trim().ToLower();
            BirthYearParam.Value = Convert.ToInt32(this.Request.Form["BirthYear"]);
            BirthMonthParam.Value = Convert.ToInt32(this.Request.Form["BirthMonth"]);
            BirthDayParam.Value = Convert.ToInt32(this.Request.Form["BirthDay"]);
            SexParam.Value = Convert.ToBoolean(this.Request.Form["sex"]);
            CountryKeyParam.Value = this.Request.Form["CountryKey"];
            ProvinceKeyParam.Value = this.Request.Form["ProvinceKey"];
            CityParam.Value = this.Request.Form["city"];
            AboutParam.Value = this.Request.Form["about"].Trim();
            UrlParam.Value = this.Request.Form["url"];
            if (this.ShowAgeEnabled.Checked)
                ShowAgeEnabledParam.Value = true;
            else
                ShowAgeEnabledParam.Value = false;
            if (this.ShowCityEnabled.Checked)
                ShowCityEnabledParam.Value = true;
            else
                ShowCityEnabledParam.Value = false;


            command.Parameters.Add(NameParam);
            command.Parameters.Add(EmailParam);
            command.Parameters.Add(BirthYearParam);
            command.Parameters.Add(BirthMonthParam);
            command.Parameters.Add(BirthDayParam);
            command.Parameters.Add(SexParam);
            command.Parameters.Add(CountryKeyParam);
            command.Parameters.Add(ProvinceKeyParam);
            command.Parameters.Add(CityParam);
            command.Parameters.Add(AboutParam);
            command.Parameters.Add(UrlParam);
            command.Parameters.Add(ShowAgeEnabledParam);
            command.Parameters.Add(ShowCityEnabledParam);

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            _info.Name = this.Request.Form["name"];
            this.Session["SigninSessionInfo"] = _info;

            this.message.Text = "اطلاعات شما با موفقیت در سیستم به روز رسانی شد.";
            this.message.Visible = true;
            return;
        }
        //--------------------------------------------------------------------
    }
}