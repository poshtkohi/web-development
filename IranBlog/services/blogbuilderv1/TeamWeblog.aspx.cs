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
    public partial class TeamWeblog : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
                return false;
            else
                return true;
        }
        //--------------------------------------------------------------------------------
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

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }
            string id = this.Request.QueryString["id"];
            if (id != null && id != "")
            {
                Int64 ID = -1;
                try { ID = Convert.ToInt64(id); }
                catch
                {
                    this.Response.Redirect("TeamWeblog.aspx", true);
                    return;
                }
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT username,password,email,name,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess FROM {0} WHERE id={1} AND BlogID={2}", constants.SQLTeamWeblogName, ID, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    //connection.Close();
                    command.Dispose();
                    this.Response.Redirect("TeamWeblog.aspx", true);
                    return;
                }
                reader.Read();

                this.username.Text = reader.GetString(0);
                this.password.Text = reader.GetString(1);
                this.ConfirmPassword.Text = reader.GetString(1);
                this.email.Text = reader.GetString(2);
                this.name.Text = reader.GetString(3);
                this.PostAccess.Checked = (bool)reader["PostAccess"];
                this.SubjectedArchiveAccess.Checked = (bool)reader["SubjectedArchiveAccess"];
                this.WeblogLinksAccess.Checked = (bool)reader["WeblogLinksAccess"];
                this.DailyLinksAccess.Checked = (bool)reader["DailyLinksAccess"];
                this.TemplateAccess.Checked = (bool)reader["TemplateAccess"];
                this.NewsletterAccess.Checked = (bool)reader["NewsletterAccess"];
                this.OthersPostAccess.Checked = (bool)reader["OthersPostAccess"];
                this.LinkBoxAccess.Checked = (bool)reader["LinkBoxAccess"];
                this.PollAccess.Checked = (bool)reader["PollAccess"];
                this.FullAccess.Checked = (bool)reader["FullAccess"];

                reader.Close();
                connection.Close();
                command.Dispose();

                this.username.Enabled = false;
            }

            if (LabelFirstLoad.Text == "true")
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1}", constants.SQLTeamWeblogName, _SigninSessionInfo.BlogID);
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
                    BindGrid();
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
        protected void Save_Click(object sender, EventArgs e)
        {
            if (this.username.Enabled)
            {
                if (this.Request.Form["username"] == null || this.Request.Form["username"] == "")
                {
                    this.message.Text = ".ورود نام کاربری الزامی است";
                    this.message.Visible = true;
                    return;
                }
                if (String.Compare(this.Request.Form["username"].Trim().ToLower(), (string)this.Session["username"]) == 0)
                {
                    this.message.Text = ".کلمه کاربری نویستده وبلاگ نمی تواند، کلمه کاربری مدیر وبلاگ باشد";
                    this.message.Visible = true;
                    return;
                }
            }
            if (this.Request.Form["password"] == null || this.Request.Form["password"] == "")
            {
                this.message.Text = ".ورود کلمه عبور الزامی است";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["name"] == null || this.Request.Form["name"] == "")
            {
                this.message.Text = ".ورود نام نویسنده الزامی است";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["email"] == null || this.Request.Form["email"] == "")
            {
                this.message.Text = ".ورود ایمیل نویسنده الزامی است";
                this.message.Visible = true;
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            string id = this.Request.QueryString["id"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            if (id == null || id == "")
            {
                command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1} AND username='{2}'", constants.SQLTeamWeblogName, _SigninSessionInfo.BlogID, this.Request.Form["username"].Trim().ToLower().Replace("'", ""));
                //this.Response.Write(command.CommandText);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int hasLastUser = reader.GetInt32(0);
                reader.Close();
                connection.Close();

                if (hasLastUser != 0)
                {
                    this.username.Text = "";
                    this.message.Text = ".این نام کاربری وجود دارد";
                    this.message.Visible = true;
                    return;
                }
                command = connection.CreateCommand();
                command.Connection = connection;
                connection.Open();
            }
            if (id == null || id == "")
            {
                command.CommandText = string.Format("INSERT INTO {0} (BlogID,subdomain,username,password,email,name,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess) VALUES(@BlogID,@subdomain,@username,@password,@email,@name,@PostAccess,@OthersPostAccess,@SubjectedArchiveAccess,@WeblogLinksAccess,@DailyLinksAccess,@TemplateAccess,@PollAccess,@LinkBoxAccess,@NewsletterAccess,@FullAccess)", constants.SQLTeamWeblogName);
            }
            else
            {
                Int64 ID = -1;
                try { ID = Convert.ToInt64(id); }
                catch
                {
                    connection.Close();
                    command.Dispose();
                    this.Response.Redirect("TeamWeblog.aspx", true);
                    return;
                }
                command.CommandText = string.Format("UPDATE {0} SET BlogID=@BlogID,subdomain=@subdomain,password=@password,email=@email,name=@name,PostAccess=@PostAccess,OthersPostAccess=@OthersPostAccess,SubjectedArchiveAccess=@SubjectedArchiveAccess,WeblogLinksAccess=@WeblogLinksAccess,DailyLinksAccess=@DailyLinksAccess,TemplateAccess=@TemplateAccess,PollAccess=@PollAccess,LinkBoxAccess=@LinkBoxAccess,NewsletterAccess=@NewsletterAccess,FullAccess=@FullAccess WHERE id={1} AND BlogID={2}", constants.SQLTeamWeblogName, ID, _SigninSessionInfo.BlogID);
            }


            if (this.username.Enabled)
            {
                SqlParameter UsernameParam = new SqlParameter("@username", SqlDbType.VarChar);
                UsernameParam.Value = this.Request.Form["username"].Trim().ToLower();
                command.Parameters.Add(UsernameParam);
            }

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter SubdomainParam = new SqlParameter("@subdomain", SqlDbType.VarChar);
            SqlParameter PasswordParam = new SqlParameter("@password", SqlDbType.NVarChar);
            SqlParameter EmailParam = new SqlParameter("@email", SqlDbType.VarChar);
            SqlParameter NameParam = new SqlParameter("@name", SqlDbType.NVarChar);
            SqlParameter PostAccessParam = new SqlParameter("@PostAccess", SqlDbType.Bit);
            SqlParameter OthersPostAccessParam = new SqlParameter("@OthersPostAccess", SqlDbType.Bit);
            SqlParameter SubjectedArchiveAccessParam = new SqlParameter("@SubjectedArchiveAccess", SqlDbType.Bit);
            SqlParameter WeblogLinksAccessParam = new SqlParameter("@WeblogLinksAccess", SqlDbType.Bit);
            SqlParameter DailyLinksAccessParam = new SqlParameter("@DailyLinksAccess", SqlDbType.Bit);
            SqlParameter TemplateAccessParam = new SqlParameter("@TemplateAccess", SqlDbType.Bit);
            SqlParameter PollAccessParam = new SqlParameter("@PollAccess", SqlDbType.Bit);
            SqlParameter LinkBoxAccessParam = new SqlParameter("@LinkBoxAccess", SqlDbType.Bit);
            SqlParameter NewsletterAccessParam = new SqlParameter("@NewsletterAccess", SqlDbType.Bit);
            SqlParameter FullAccessParam = new SqlParameter("@FullAccess", SqlDbType.Bit);

            TeamWeblogAccessInfo accessInfo = GetTeamWeblogAccessInfo();


            BlogIDParam.Value = _SigninSessionInfo.BlogID;
            SubdomainParam.Value = _SigninSessionInfo.Subdomain;
            PasswordParam.Value = this.Request.Form["password"];
            EmailParam.Value = this.Request.Form["email"].Trim().ToLower();
            NameParam.Value = this.Request.Form["name"];
            PostAccessParam.Value = accessInfo.PostAccess;
            OthersPostAccessParam.Value = accessInfo.OthersPostAccess;
            SubjectedArchiveAccessParam.Value = accessInfo.SubjectedArchiveAccess;
            WeblogLinksAccessParam.Value = accessInfo.WeblogLinksAccess;
            DailyLinksAccessParam.Value = accessInfo.DailyLinksAccess;
            TemplateAccessParam.Value = accessInfo.TemplateAccess;
            PollAccessParam.Value = accessInfo.PollAccess;
            LinkBoxAccessParam.Value = accessInfo.LinkBoxAccess;
            NewsletterAccessParam.Value = accessInfo.NewsletterAccess;
            FullAccessParam.Value = accessInfo.FullAccess;


            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(SubdomainParam);
            command.Parameters.Add(PasswordParam);
            command.Parameters.Add(EmailParam);
            command.Parameters.Add(NameParam);
            command.Parameters.Add(PostAccessParam);
            command.Parameters.Add(OthersPostAccessParam);
            command.Parameters.Add(SubjectedArchiveAccessParam);
            command.Parameters.Add(WeblogLinksAccessParam);
            command.Parameters.Add(DailyLinksAccessParam);
            command.Parameters.Add(TemplateAccessParam);
            command.Parameters.Add(PollAccessParam);
            command.Parameters.Add(LinkBoxAccessParam);
            command.Parameters.Add(NewsletterAccessParam);
            command.Parameters.Add(FullAccessParam);


            command.ExecuteNonQuery();
            connection.Close();
            command.Dispose();
            if (this.username.Enabled)
                this.message.Text = ".نویسنده جدید وبلاگ با موفقیت به پایگاه داده سیستم اضافه شد";
            else
                this.message.Text = ".اطلاعات نویسنده وبلاگ مورد نظر با موفقیت در پایگاه داده سیستم به روز رسانی شد";
            this.message.Visible = true;
            ClearFields();
            BindGrid();
            this.data.Visible = true;
            this.GridMessage.Visible = false;
            return;
        }
        //--------------------------------------------------------------------------------
        private void ClearFields()
        {
            this.username.Enabled = true;
            this.username.Text = "";
            this.email.Text = "";
            this.name.Text = "";
            this.PostAccess.Checked = true;
            this.SubjectedArchiveAccess.Checked = false;
            this.WeblogLinksAccess.Checked = false;
            this.DailyLinksAccess.Checked = false;
            this.TemplateAccess.Checked = false;
            this.NewsletterAccess.Checked = false;
            this.OthersPostAccess.Checked = false;
            this.LinkBoxAccess.Checked = false;
            this.PollAccess.Checked = false;
            this.FullAccess.Checked = false;
        }
        //--------------------------------------------------------------------------------
        private TeamWeblogAccessInfo GetTeamWeblogAccessInfo()
        {
            TeamWeblogAccessInfo info = new TeamWeblogAccessInfo();

            /*info.PostAccess = this.PostAccess.Checked;
            info.SubjectedArchiveAccess = this.SubjectedArchiveAccess.Checked;
            info.WeblogLinksAccess = this.WeblogLinksAccess.Checked;
            info.DailyLinksAccess = this.DailyLinksAccess.Checked;
            info.TemplateAccess = this.TemplateAccess.Checked;
            info.NewsletterAccess = this.NewsletterAccess.Checked;
            info.OthersPostAccess = this.OthersPostAccess.Checked;
            info.LinkBoxAccess = this.LinkBoxAccess.Checked;
            info.PollAccess = this.PollAccess.Checked;
            info.FullAccess = this.FullAccess.Checked;*/

            if (this.Request.Form["PostAccess"] == "on")
                info.PostAccess = true;
            if (this.Request.Form["SubjectedArchiveAccess"] == "on")
                info.SubjectedArchiveAccess = true;
            if (this.Request.Form["WeblogLinksAccess"] == "on")
                info.WeblogLinksAccess = true;
            if (this.Request.Form["DailyLinksAccess"] == "on")
                info.DailyLinksAccess = true;
            if (this.Request.Form["TemplateAccess"] == "on")
                info.TemplateAccess = true;
            if (this.Request.Form["NewsletterAccess"] == "on")
                info.NewsletterAccess = true;
            if (this.Request.Form["OthersPostAccess"] == "on")
                info.OthersPostAccess = true;
            if (this.Request.Form["LinkBoxAccess"] == "on")
                info.LinkBoxAccess = true;
            if (this.Request.Form["PollAccess"] == "on")
                info.PollAccess = true;
            if (this.Request.Form["FullAccess"] == "on")
                info.FullAccess = true;
            return info;
        }
        //--------------------------------------------------------------------------------
        private void BindGrid()
        {

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT id,username,PostNum FROM {0} WHERE BlogID={1} ORDER BY id DESC", constants.SQLTeamWeblogName, _SigninSessionInfo.BlogID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("username", typeof(string)));
                dt.Columns.Add(new DataColumn("PostNum", typeof(string)));
                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["id"] = reader["id"].ToString();
                    dr["username"] = (string)reader["username"];
                    dr["PostNum"] = reader["PostNum"].ToString();
                    dt.Rows.Add(dr);
                }
                DataView dv = new DataView(dt);
                this.data.DataSource = dv;
                this.data.DataBind();
            }
            else
            {
                this.GridMessage.Visible = true;
                this.data.DataBind();
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return;
        }
        //--------------------------------------------------------------------------------
        protected void data_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            this.Response.Redirect("TeamWeblog.aspx?id=" + e.Item.Cells[0].Text, true);
            return;
        }
        //--------------------------------------------------------------------------------
        protected void data_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (e.Item.Cells[1].Text != (string)this.Session["username"])
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("DELETE FROM {0} WHERE BlogID={1} AND id={2};", constants.SQLTeamWeblogName, _SigninSessionInfo.BlogID, e.Item.Cells[0].Text);
                command.ExecuteNonQuery();
                if (Convert.ToInt64(e.Item.Cells[2].Text) > 0)
                {
                    command.CommandText = string.Format("UPDATE {0} SET PostNum=PostNum+{1} WHERE BlogID={2} AND id={3}", constants.SQLTeamWeblogName, e.Item.Cells[2].Text, _SigninSessionInfo.BlogID, Convert.ToInt64(e.Item.Cells[0].Text));
                    command.ExecuteNonQuery();
                }
                command.Dispose();
                connection.Close();

                connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
                connection.Open();
                command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("UPDATE {0} SET AuthorID={1} WHERE BlogID={2} AND AuthorID={3}", constants.SQLPostsTableName, _SigninSessionInfo.AuthorID, _SigninSessionInfo.BlogID, Convert.ToInt64(e.Item.Cells[0].Text));
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();

                this.message.Text = ".نام کاربری مربوطه با موفقیت از سیستم حذف شد";
                this.message.Visible = true;
                BindGrid();
                this.data.Visible = true;
                this.GridMessage.Visible = false;
                return;
            }
            else
            {
                this.message.Text = ".شما تمی توانید نام کاربری خود را از سیستم حذف کنید";
                this.message.Visible = true;
                return;
            }
        }
        //--------------------------------------------------------------------------------
    }
}
