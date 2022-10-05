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

using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
	/// <summary>
	/// Summary description for PostArchive.
	/// </summary>
	public partial class PostArchive : System.Web.UI.Page
	{
        //------------------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.SubjectedArchiveAccess)
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
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1}", constants.SQLSubjectedArchiveTableName, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int total = reader.GetInt32(0);
                this.data.DataBind();
                reader.Close();
                //connection.Close();
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
        //--------------------------------------------------------------------------------
		protected void store_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["subject"] == null || this.Request.Form["subject"] == "")
			{
				this.subject.Text = "";
				this.message.Text = ".متن موضوع خالی است";
				this.message.Visible = true;
				return ;
			}

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("INSERT INTO {0} (BlogID,subject,PostNum) VALUES(@BlogID,@subject,0)", constants.SQLSubjectedArchiveTableName);
			SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
			SqlParameter subjectParam = new SqlParameter("@subject", SqlDbType.NVarChar);
			BlogIDParam.Value = _SigninSessionInfo.BlogID;
			subjectParam.Value = this.Request.Form["subject"];
			command.Parameters.Add(BlogIDParam);
			command.Parameters.Add(subjectParam);
			command.ExecuteNonQuery();
			//connection.Close();
			command.Dispose();
			this.subject.Text = "";
			this.message.Text = ".موضوع آرشیوی جدید با موفقیت به پایگاه داده سیستم وارد شد";
			this.message.Visible = true;
		    BindGrid(true);
            this.data.Visible = true;
            this.GridMessage.Visible = false;
		}
		//--------------------------------------------------------------------------------
        protected void data_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			//this.Response.Write(data.CurrentPageIndex);
			bool IsNext = true;
			if(data.CurrentPageIndex > e.NewPageIndex)
				IsNext = false;
			data.CurrentPageIndex = e.NewPageIndex;
			BindGrid(IsNext);
		}
		//--------------------------------------------------------------------------------
		private void BindGrid(bool IsNext) 
		{
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			if(this.data.CurrentPageIndex == 0)
			{
				SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.Connection = connection;
                command.CommandText = string.Format("SELECT TOP 10 id,subject,PostNum FROM {0} WHERE BlogID={1} ORDER BY id DESC", constants.SQLSubjectedArchiveTableName, _SigninSessionInfo.BlogID);
				SqlDataReader reader = command.ExecuteReader();
				DataTable dt = new DataTable();
				DataRow dr;
				dt.Columns.Add(new DataColumn("id", typeof(string)));
				dt.Columns.Add(new DataColumn("subject", typeof(string)));
                dt.Columns.Add(new DataColumn("PostNum", typeof(string)));
				Int64 NextID = -1;
				int id = 0;
				while(reader.Read())
				{
					NextID = reader.GetInt64(0);
					if(id == 0)
						this.LabelPrevoiusID.Text = NextID.ToString();
					dr = dt.NewRow();
                    dr["id"] = NextID;
                    dr["subject"] = (string)reader["subject"];
                    dr["PostNum"] = reader["PostNum"].ToString();
					dt.Rows.Add(dr);
					id++;
				}
				this.LabelNextID.Text = NextID.ToString();
				DataView dv = new DataView(dt);
				this.data.DataSource = dv;
				this.data.DataBind();
				reader.Close();
				//connection.Close();
				command.Dispose();
			}
			else
			{
				SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.Connection = connection;
				if(IsNext)
                    command.CommandText = string.Format("SELECT TOP 10 id,subject,PostNum FROM {0} WHERE id < {1} AND BlogID={2} ORDER BY id DESC", constants.SQLSubjectedArchiveTableName, this.LabelNextID.Text, _SigninSessionInfo.BlogID);
				else
                    command.CommandText = string.Format("SELECT TOP 10 id,subject,PostNum FROM {0} WHERE id >= {1} AND BlogID={2} ORDER BY id DESC", constants.SQLSubjectedArchiveTableName, this.LabelPrevoiusID.Text, _SigninSessionInfo.BlogID);
				SqlDataReader reader = command.ExecuteReader();
				DataTable dt = new DataTable();
				DataRow dr;
				dt.Columns.Add(new DataColumn("id", typeof(string)));
				dt.Columns.Add(new DataColumn("subject", typeof(string)));
                dt.Columns.Add(new DataColumn("PostNum", typeof(string)));
				Int64 NextID = -1;
				int id = 0;
				while(reader.Read())
				{
					NextID = reader.GetInt64(0);
					if(id == 0)
					{
						if(IsNext)
						{
							this.LabelPrevoiusID.Text = this.LabelTemp.Text;
							this.LabelTemp.Text = NextID.ToString();
						}
					}
					dr = dt.NewRow();
                    dr["id"] = NextID;
                    dr["subject"] = (string)reader["subject"];
                    dr["PostNum"] = reader["PostNum"].ToString();
					dt.Rows.Add(dr);
					id++;
				}
				//this.LabelPrevoiusID.Text = this.LabelNextID.Text;
				this.LabelNextID.Text = NextID.ToString();
				DataView dv = new DataView(dt);
				this.data.DataSource = dv;
				this.data.DataBind();
				reader.Close();
				//connection.Close();
				command.Dispose();
				this.LabelFirstLoad.Text = "false";
			}
		}
		//--------------------------------------------------------------------------------
        protected void data_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.Response.Redirect("UpdatePostArchive.aspx?id=" + e.Item.Cells[0].Text, true);
		}
		//--------------------------------------------------------------------------------
        protected void data_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CategoryToAuthor_PostArchivePage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@CategoryID", SqlDbType.BigInt);
            command.Parameters["@CategoryID"].Value = Convert.ToInt64(e.Item.Cells[0].Text);

            command.Parameters.Add("@PostNum", SqlDbType.Int);
            command.Parameters["@PostNum"].Value = Convert.ToInt32(e.Item.Cells[2].Text);

            command.ExecuteNonQuery();
            command.Dispose();
            /*command.CommandText = string.Format("DELETE FROM {0} WHERE BlogID={1} AND id={2}", constants.SQLSubjectedArchiveTableName, _SigninSessionInfo.BlogID, e.Item.Cells[0].Text);
            command.ExecuteNonQuery();

            if (Convert.ToInt32(e.Item.Cells[2].Text) > 0)
            {
                command.CommandText = string.Format("UPDATE {0} SET PostNum=PostNum-{1} WHERE i={2}", constants.SQLUsersInformationTableName, e.Item.Cells[2].Text, _SigninSessionInfo.BlogID);
                command.ExecuteNonQuery();
            }
            command.Dispose();*/

            connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("UPDATE {0} SET IsDeleted=1 WHERE BlogID={1} AND CategoryID={2}", constants.SQLPostsTableName, _SigninSessionInfo.BlogID, e.Item.Cells[0].Text);
            command.ExecuteNonQuery();
            command.Dispose();

            this.message.Text = ".آرشیو موضوعی مربوطه با موفقیت از سیستم حذف شد";
            this.message.Visible = true;
            BindGrid(true);
            this.data.Visible = true;
            this.GridMessage.Visible = false;
		}
		//--------------------------------------------------------------------------------
	}
}
