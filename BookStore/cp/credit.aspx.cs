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
    public partial class credit : System.Web.UI.Page
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
            SetCurrentCredit();
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
            if (this.Request.Form["NewCredit"] == null || this.Request.Form["NewCredit"] == "")
            {
                this.message.Text = "اعتبار خالی است.";
                this.message.Visible = true;
                return;
            }
            Int64 _CurrentCredit = -1;

            try { _CurrentCredit = Convert.ToInt64(this.Request.Form["NewCredit"]); }
            catch { }

            if (_CurrentCredit <= 0)
            {
                this.message.Text = "اعتبار نامعتبر است.";
                this.message.Visible = true;
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "RegCreditTransaction_CreditPage_proc";
            command.CommandType = CommandType.StoredProcedure;

            //--------------------
            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];

            command.Parameters.Add("@amount", SqlDbType.BigInt);
            command.Parameters["@amount"].Value = _CurrentCredit;

            command.Parameters.Add("@ReservationCode", SqlDbType.BigInt);
            command.Parameters["@ReservationCode"].Direction = ParameterDirection.Output;
            //--------------------

            command.ExecuteNonQuery();

            Session["TotalAmount"] = _CurrentCredit;
            Session["reservationcode"] = command.Parameters["@ReservationCode"].Value.ToString();
            Session["merchantid"] = constants.MarchantID;
            Session["redirecturl"] = String.Format("{0}", constants.RedirectUrl);


            command.Dispose();
            connection.Close();

            this.Response.Redirect("Send.aspx", true);

            return;
        }
        //--------------------------------------------------------------------
        private void SetCurrentCredit()
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "ShowCurrentCredits_CreditPage_proc";
            command.CommandType = CommandType.StoredProcedure;

            //--------------------
            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];

            command.Parameters.Add("@CurrentCredit", SqlDbType.BigInt);
            command.Parameters["@CurrentCredit"].Direction = ParameterDirection.Output;
            //--------------------

            command.ExecuteNonQuery();

            this.CurrentCredit.Text = command.Parameters["@CurrentCredit"].Value.ToString();

            command.Dispose();
            connection.Close();

            return;
        }
        //--------------------------------------------------------------------
    }
}
