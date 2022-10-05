/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Text;
using System.IO;
using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
	/// <summary>
	/// Summary description for LinkBox.
	/// </summary>
	public partial class LinkBox : System.Web.UI.Page
	{
        //------------------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.LinkBoxAccess)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
		//------------------------------------------------------------------------------
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

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT HasLinkBox FROM {0} WHERE i={1}", constants.SQLUsersInformationTableName, _SigninSessionInfo.BlogID);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            bool HasLinkBox = (bool)reader["HasLinkBox"];
            reader.Close();
            string path = constants.RootDircetoryWeblogs + "\\" + _SigninSessionInfo.Subdomain + "\\" + "linkbox.html";
            connection.Close();
            command.Dispose();
            if (!HasLinkBox)
            {
                File.Copy(constants.LinkBoxTemplatePath + "\\template.html", path);
                connection.Open();
                command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("UPDATE {0} SET HasLinkBox=1 WHERE i={1}", constants.SQLUsersInformationTableName, _SigninSessionInfo.BlogID);
                command.ExecuteNonQuery();
                connection.Close();
                command.Dispose();
            }
            if (this.content.Text == "")
            {
                StreamReader fs = File.OpenText(path);
                this.content.Text = fs.ReadToEnd();
                fs.Close();
            }
            if (LabelFirstLoad.Text == "true")
            {
                connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1}", constants.SQLLinkBoxTableName, _SigninSessionInfo.BlogID);
                reader = command.ExecuteReader();
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
		//------------------------------------------------------------------------------
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
		//------------------------------------------------------------------------------
		protected void submit_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["content"].Length > 204800)
			{
				this.ContentError.Text = ".حجم قالب لینک باکس نمی تواند از 200 کیلو بایت بیشتر باشد";
				this.ContentError.Visible = true;
				return ;
			}
			else
			{
                SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

                string path = constants.RootDircetoryWeblogs + "\\" + _SigninSessionInfo.Subdomain + "\\" + "linkbox.html";
				StreamWriter sr = File.CreateText(path);
				sr.Write(this.Request.Form["content"]);
				sr.Close();
				this.ContentError.Text = ".قالب لینک باکس وبلاگ شما با موفقیت به روز رسانی شد";
				this.ContentError.Visible = true;
				//this.Response.Redirect("PostAdmin.aspx", true);
				return ;
			}
		}
		//------------------------------------------------------------------------------
		protected void save_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["url"] == null || this.Request.Form["url"] == "")
			{
				this.ContentError.Text = ". پیوند خالی است";
				this.ContentError.Visible = true;
				return ;
			}
			if(this.Request.Form["title"] == null || this.Request.Form["title"] == "")
			{
				this.ContentError.Text = ".عنوان پیوند خالی است";
				this.ContentError.Visible = true;
				return ;
			}

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
            command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1}", constants.SQLLinkBoxTableName, _SigninSessionInfo.BlogID);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			int LinkboxCounter = (int)reader[0];
			reader.Close();
			connection.Close();
			command.Dispose();

			connection.Open();
			command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("INSERT INTO {0} (BlogID,title,url,name,date,views,LinkboxCounter) VALUES(@BlogID,@title,@url,@name,@date,0,@LinkboxCounter)", constants.SQLLinkBoxTableName);
			SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
			SqlParameter TitleParam = new SqlParameter("@title", SqlDbType.NVarChar);
			SqlParameter UrlParam = new SqlParameter("@url", SqlDbType.NVarChar);
			SqlParameter NameParam = new SqlParameter("@name", SqlDbType.NVarChar);
			SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
			SqlParameter LinkboxCounterParam = new SqlParameter("@LinkboxCounter", SqlDbType.Int);
            BlogIDParam.Value = _SigninSessionInfo.BlogID;
			TitleParam.Value = this.Request.Form["title"];
			UrlParam.Value = this.Request.Form["url"];
			NameParam.Value = "";
			LinkboxCounterParam.Value = LinkboxCounter + 1;
			DateParam.Value = DateTime.Now;
			command.Parameters.Add(BlogIDParam);
			command.Parameters.Add(TitleParam);
			command.Parameters.Add(UrlParam);
			command.Parameters.Add(NameParam);
			command.Parameters.Add(DateParam);
			command.Parameters.Add(LinkboxCounterParam);
			command.ExecuteNonQuery();
			command.Dispose();
            connection.Close();
            this.data.VirtualItemCount++;
			this.title.Text = "";
			this.url.Text = "";
			this.ContentError.Text = ".لینک جدید با موفقیت به پایگاه داده سیستم وارد شد";
			this.ContentError.Visible = true;
            BindGrid(true);
            this.data.Visible = true;
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
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT TOP 10 id,title FROM {0} WHERE BlogID={1} ORDER BY id DESC", constants.SQLLinkBoxTableName, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("title", typeof(string)));
                Int64 NextID = -1;
                int i = 0;
                while (reader.Read())
                {
                    NextID = reader.GetInt64(0);
                    if (i == 0)
                        this.LabelPrevoiusID.Text = NextID.ToString();
                    dr = dt.NewRow();
                    dr[1] = reader.GetString(1);
                    dr[0] = NextID;
                    dt.Rows.Add(dr);
                    i++;
                }
                this.LabelNextID.Text = NextID.ToString();
                DataView dv = new DataView(dt);
                this.data.DataSource = dv;
                this.data.DataBind();
                reader.Close();
                connection.Close();
                command.Dispose();
            }
            else
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                if (IsNext)
                    command.CommandText = string.Format("SELECT TOP 10 id,title FROM {0} WHERE id < {1} AND BlogID={2} ORDER BY id DESC", _SigninSessionInfo.BlogID);
                else
                    command.CommandText = string.Format("SELECT TOP 10 id,title FROM {0} WHERE id >= {1} AND BlogID={2} ORDER BY id DESC", constants.SQLLinkBoxTableName, this.LabelPrevoiusID.Text, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("title", typeof(string)));
                Int64 NextID = -1;
                int i = 0;
                while (reader.Read())
                {
                    NextID = reader.GetInt64(0);
                    if (i == 0)
                    {
                        if (IsNext)
                        {
                            this.LabelPrevoiusID.Text = this.LabelTemp.Text;
                            this.LabelTemp.Text = NextID.ToString();
                        }
                    }
                    dr = dt.NewRow();
                    dr[1] = reader.GetString(1);
                    dr[0] = NextID;
                    dt.Rows.Add(dr);
                    i++;
                }
                //this.LabelPrevoiusID.Text = this.LabelNextID.Text;
                this.LabelNextID.Text = NextID.ToString();
                DataView dv = new DataView(dt);
                this.data.DataSource = dv;
                this.data.DataBind();
                reader.Close();
                connection.Close();
                command.Dispose();
                this.LabelFirstLoad.Text = "false";
            }
        }
        //--------------------------------------------------------------------------------
        protected void data_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("DELETE FROM {0} WHERE id={1} AND BlogID={2}", constants.SQLLinkBoxTableName, e.Item.Cells[0].Text, _SigninSessionInfo.BlogID);
            command.ExecuteNonQuery();
            connection.Close();
            command.Dispose();
            this.ContentError.Text = ".پیوند مربوطه با موفقیت از سیستم حذف شد";
            this.ContentError.Visible = true;
            BindGrid(true);
        }
		//------------------------------------------------------------------------------
	}
}
