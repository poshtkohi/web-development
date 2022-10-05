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
using System.Text.RegularExpressions;

namespace services
{
    public partial class newsletter : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 id = 0;
            try { id = Convert.ToInt64(this.Request.QueryString["id"]);  }
            catch
            {
                this.message.Text = ".نامعتبر است URL";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["name"] == null || this.Request.Form["name"] == "")
            {
                this.message.Text = ".نام خالی است";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["name"].Length > 50)
            {
                this.message.Text = ".تعداد حروف نام نمیتواند از 50 حرف بیشتر باشد";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["email"] == null || this.Request.Form["email"] == "")
            {
                this.message.Text = ".ورود ایمیل الزامی است";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["email"].Length > 50)
            {
                this.message.Text = ".تعداد حروف ایمیل نمیتواند از 50 حرف بیشتر باشد";
                this.message.Visible = true;
                return;
            }
            Regex rex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!rex.IsMatch(this.Request.Form["email"]))
            {
                this.message.Text = ".حروف ایمیل نامعتبر است";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["MailAction"] != null && this.Request.Form["MailAction"] == "delete")
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("UPDATE {0} SET IsDeleted=1 WHERE BlogID={1} AND email='{2}'", constants.SQLNewsletterTableName, id, this.Request.Form["email"].Trim().ToLower());
                command.ExecuteNonQuery();
                command.Dispose();

                this.message.Text = ".فرد مربوطه با موفقیت از خبرنامه سیستم حذف شد";
                this.message.Visible = true;
                return;
            }
            else
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Newsletter_add_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = id;

                command.Parameters.Add("@MemberNum", SqlDbType.Int);
                command.Parameters["@MemberNum"].Direction = ParameterDirection.Output;

                command.Parameters.Add("@AllowedMemberNum", SqlDbType.Int);
                command.Parameters["@AllowedMemberNum"].Value = constants.AllowedNewsletterMemberNum;

                command.Parameters.Add("@name", SqlDbType.NVarChar);
                command.Parameters["@name"].Value = this.Request.Form["name"].Trim();

                command.Parameters.Add("@email", SqlDbType.VarChar);
                command.Parameters["@email"].Value = this.Request.Form["email"].Trim().ToLower();

                command.Parameters.Add("@IsExists", SqlDbType.Bit);
                command.Parameters["@IsExists"].Direction = ParameterDirection.Output;


                command.ExecuteNonQuery();

                if ((int)command.Parameters["@MemberNum"].Value > constants.AllowedNewsletterMemberNum)
                {
                    command.Dispose();
                    this.message.Text = ".تعداد اعضای خبر نامه نمی تواند بیشتر از 1000 باشد";
                    this.message.Visible = true;
                    return;
                }
                if ((bool)command.Parameters["@IsExists"].Value)
                {
                    command.Dispose();
                    this.message.Text = ".کاربری با این آدرس ایمیل وجود دارد";
                    this.message.Visible = true;
                    return;
                }
                else
                {
                    command.Dispose();
                    this.message.Text = ".عضو جدید با موفقیت به خبر نامه سیستم اضافه شد";
                    this.message.Visible = true;
                    return;
                }
            }
        }
        //--------------------------------------------------------------------------------
    }
}
