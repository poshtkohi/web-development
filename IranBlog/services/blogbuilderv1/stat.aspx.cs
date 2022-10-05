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
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class stat : System.Web.UI.Page
    {
        //------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
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

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT TotalVisits,TodayVisits,YesterdayVisits,ThisMonthVisits FROM stat WHERE BlogID={0}", _SigninSessionInfo.BlogID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                this.TotalVisits.Text = ((Int64)reader["TotalVisits"]).ToString();
                this.TodayVisits.Text = ((int)reader["TodayVisits"]).ToString();
                this.YesterdayVisits.Text = ((int)reader["YesterdayVisits"]).ToString();
                this.ThisMonthVisits.Text = ((int)reader["ThisMonthVisits"]).ToString();
                this.message.Visible = false;
            }
            else
            {
                this.message.Text = ".آمار وبلاگ شما فعال نیست. لطفا قالب وبلاگ خود را به روز رسانی کنید";
                this.message.Visible = true;
            }
            reader.Close();
            connection.Close();
            command.Dispose();
            return;
        }
        //------------------------------------------------------------------------------------------
    }
}
