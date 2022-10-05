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
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class NewsLetter : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.NewsletterAccess)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        //--------------------------------------------------------------------------------
        protected void Page_Load(object sender, System.EventArgs e)
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

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }
            if (LabelFirstLoad.Text == "true")
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1} AND IsDeleted=0", constants.SQLNewsletterTableName, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int total = reader.GetInt32(0);
                this.data.DataBind();
                reader.Close();
                connection.Close();
                command.Dispose();
                // Set the virtual item count, which specifies the total number
                // items displayed in the DataGrid control when custom paging
                // is used.
                this.data.VirtualItemCount = total;
                // Retrieve the segment of data to display on the page from the
                // data source and bind it to the DataGrid control.
                if (total > 0)
                {
                    BindGrid(true);
                    this.LabelFirstLoad.Text = "false";
                    this.data.Visible = true;
                    this.GridMessage.Visible = false;
                }
                else
                {
                    this.data.Visible = false;
                    this.GridMessage.Visible = true;
                }
            }
            return;

        }
        //--------------------------------------------------------------------------------
        protected void download_Click(object sender, EventArgs e)
        {
            if (this.data.VirtualItemCount > 0)
            {
                SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

                this.Response.ContentType = "text/plain";
                this.Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}-newsletter.txt", _SigninSessionInfo.Subdomain));

                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT email FROM {0} WHERE BlogID={1} AND IsDeleted=0", constants.SQLNewsletterTableName, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    this.Response.Write((string)reader["email"] + ",");
                    this.Response.Flush();
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Response.Close();
                return;
            }
            else
            {
                this.message.Text = ".خبرنامه شما فاقد هر گونه عضوی برای دانلود است";
                this.message.Visible = true;
                return;
            }
        }
        //--------------------------------------------------------------------------------
        protected void send_Click(object sender, EventArgs e)
        {
            if (this.data.VirtualItemCount > 0)
            {
                this.Response.Redirect("SendNewsletter.aspx", true);
                return;
            }
            else
            {
                this.message.Text = ".خبرنامه شما فاقد هر گونه عضوی برای ارسال خبر نامه جدید است";
                this.message.Visible = true;
                return;
            }
        }
        //--------------------------------------------------------------------------------
        protected void add_Click(object sender, EventArgs e)
        {
            if (this.data.VirtualItemCount > 1000)
            {
                this.message.Text = ".تعداد اعضای خبر نامه نمی تواند بیشتر از 1000 باشد";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["name"] == null || this.Request.Form["name"] == "")
            {
                this.message.Text = ".فیلد نام خالی است";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["email"] == null || this.Request.Form["email"] == "")
            {
                this.message.Text = ".ورود ایمیل الزامی است";
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

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Newsletter_add_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

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
                connection.Close();
                this.message.Text = ".کاربری با این آدرس ایمیل وجود دارد";
                this.message.Visible = true;
                this.email.Text = "";
                return;
            }
            else
            {
                command.Dispose();
                connection.Close();

                this.message.Text = ".عضو جدید با موفقیت به خبر نامه سیستم اضافه شد";
                this.message.Visible = true;
                this.name.Text = "";
                this.email.Text = "";
                this.data.VirtualItemCount++;
                BindGrid(true);
                this.data.Visible = true;
                this.GridMessage.Visible = false;

                return;
            }

        }
        //--------------------------------------------------------------------------------
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            if (this.data.VirtualItemCount > 0)
            {
                SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("UPDATE {0} SET IsDeleted=1 WHERE BlogID={1}", constants.SQLNewsletterTableName, _SigninSessionInfo.BlogID);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                this.data.VirtualItemCount = 0;
            }

            this.message.Text = ".تمامی اعضای خبر نامه با موفقیت از سیستم حذف شدند";
            this.message.Visible = true;
            this.data.Visible = false;
            this.GridMessage.Visible = true;
        }
        //--------------------------------------------------------------------------------
        protected void data_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            //this.Response.Write(data.CurrentPageIndex);
            bool IsNext = true;
            if (data.CurrentPageIndex > e.NewPageIndex)
                IsNext = false;
            data.CurrentPageIndex = e.NewPageIndex;
            BindGrid(IsNext);
        }
        //--------------------------------------------------------------------------------
        private void BindGrid(bool IsNext)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (this.data.CurrentPageIndex == 0)
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT TOP 10 id,name,email FROM {0} WHERE BlogID={1} AND IsDeleted=0 ORDER BY id DESC", constants.SQLNewsletterTableName, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("name", typeof(string)));
                dt.Columns.Add(new DataColumn("email", typeof(string)));
                Int64 NextID = -1;
                int id = 0;
                while (reader.Read())
                {
                    NextID = reader.GetInt64(0);
                    if (id == 0)
                        this.LabelPrevoiusID.Text = NextID.ToString();
                    dr = dt.NewRow();
                    dr["id"] = NextID;
                    dr["name"] = (string)reader["name"];
                    dr["email"] = (string)reader["email"];
                    dt.Rows.Add(dr);
                    id++;
                }
                this.LabelNextID.Text = NextID.ToString();
                DataView dv = new DataView(dt);
                this.data.DataSource = dv;
                this.data.DataBind();
                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }
            else
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                if (IsNext)
                    command.CommandText = string.Format("SELECT TOP 10 id,name,email FROM {0} WHERE id < {1} AND BlogID={2} AND IsDeleted=0 ORDER BY id DESC", constants.SQLNewsletterTableName, this.LabelNextID.Text, _SigninSessionInfo.BlogID);
                else
                    command.CommandText = string.Format("SELECT TOP 10 id,name,email FROM {0} WHERE id >= {1} AND BlogID={2} AND IsDeleted=0 ORDER BY id DESC", constants.SQLNewsletterTableName, this.LabelPrevoiusID.Text, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("name", typeof(string)));
                dt.Columns.Add(new DataColumn("email", typeof(string)));
                Int64 NextID = -1;
                int id = 0;
                while (reader.Read())
                {
                    NextID = (Int64)reader["id"];
                    if (id == 0)
                    {
                        if (IsNext)
                        {
                            this.LabelPrevoiusID.Text = this.LabelTemp.Text;
                            this.LabelTemp.Text = NextID.ToString();
                        }
                    }
                    dr = dt.NewRow();
                    dr["id"] = NextID;
                    dr["name"] = (string)reader["name"];
                    dr["email"] = (string)reader["email"];
                    dt.Rows.Add(dr);
                    id++;
                }
                //this.LabelPrevoiusID.Text = this.LabelNextID.Text;
                this.LabelNextID.Text = NextID.ToString();
                DataView dv = new DataView(dt);
                this.data.DataSource = dv;
                this.data.DataBind();
                reader.Close();
                command.Dispose();
                connection.Close();
                this.LabelFirstLoad.Text = "false";
                return;
            }
        }
        //--------------------------------------------------------------------------------
        protected void data_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            //this.Response.Redirect("UpdatePostArchive.aspx?id=" + e.Item.Cells[0].Text, true);
        }
        //--------------------------------------------------------------------------------
        protected void data_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("UPDATE {0} SET IsDeleted=1 WHERE id={1} AND BlogID={2}", constants.SQLNewsletterTableName, e.Item.Cells[0].Text, _SigninSessionInfo.BlogID);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            this.message.Text = ".فرد مربوطه با موفقیت از خبرنامه سیستم حذف شد";
            this.message.Visible = true;         
            BindGrid(true);
            this.data.Visible = true;
            this.GridMessage.Visible = false;
        }
        //--------------------------------------------------------------------------------
    }
}
