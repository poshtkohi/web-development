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

namespace Peyghamak.cp
{
    public partial class password : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login.IsMyCp(this))
                return;
            MetaCopyrightControl();
            SetSiteFooterControl();
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

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "ChangePassword_PasswordPage_proc";
            command.CommandType = CommandType.StoredProcedure;

            //--------------------
            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Direction = ParameterDirection.Input;
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 50);
            command.Parameters["@NewPassword"].Direction = ParameterDirection.Input;
            command.Parameters["@NewPassword"].Value = this.Request.Form["NewPassword"].Trim();

            command.Parameters.Add("@LastPassword", SqlDbType.NVarChar, 50);
            command.Parameters["@LastPassword"].Direction = ParameterDirection.Input;
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
