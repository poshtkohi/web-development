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
	/// Summary description for poll.
	/// </summary>
	public partial class poll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox ResponseText;
		protected System.Web.UI.WebControls.TextBox address;
        //------------------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.PollAccess)
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

            if (this.Request.Form != null)
            {
                for (int j = 0; j < this.Request.Form.Count; j++)
                {
                    if (this.Request.Form.Keys[j].IndexOf("data", 0) >= 0 && this.Request.Form[j] == "حذف")
                    {
                        //this.Response.Write(this.Request.Form.Keys[j] + "<br>" + this.Request.Form[j]); //data:_ctl11:_ctl0
                        int p1 = this.Request.Form.Keys[j].IndexOf(":_ctl") + ":_ctl".Length;
                        int p2 = this.Request.Form.Keys[j].IndexOf(":_ctl", p1);
                        int row = Convert.ToInt32(this.Request.Form.Keys[j].Substring(p1, p2 - p1));
                        DataGridCommandEventArgs args = new DataGridCommandEventArgs(this.data.Items[row - 2], this.data, new CommandEventArgs("data_DeleteCommand", null));
                        this.data_DeleteCommand(this.data, args);
                        return;
                    }
                }
            }
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT id,QuestionText FROM {0} WHERE BlogID={1}", constants.SQLPollQuestionsTableName, _SigninSessionInfo.BlogID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                this.LabelQuestionID.Text = ((Int64)reader["id"]).ToString();
                this.question.Text = (string)reader["QuestionText"];
                this.delete.Enabled = true;
                this.modify.Enabled = true;
                this.define.Enabled = false;
                this.LabelFirstLoad.Text = "false";
                this.data.Visible = true;
                this.GridMessage.Visible = false;
                this.response.Enabled = true;
                this.define_response.Enabled = true;
                BindGrid();
            }
            else
            {

                this.delete.Enabled = false;
                this.modify.Enabled = false;
                this.define.Enabled = true;
                this.data.Visible = false;
                this.GridMessage.Visible = true;
                this.response.Enabled = false;
                this.define_response.Enabled = false;
            }
            reader.Close();
            connection.Close();
            command.Dispose();
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
		protected void define_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["question"] == null || this.Request.Form["question"] == "")
			{
				this.question.Text = "";
				this.message.Text = ".سوال نظرسنجی خالی است";
				this.message.Visible = true;
				return ;
			}
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("INSERT INTO {0} (BlogID,QuestionText,date) VALUES(@BlogID,@QuestionText,@date); SELECT id FROM {0} WHERE BlogID=@BlogID", constants.SQLPollQuestionsTableName);
			SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
			SqlParameter QuestionTextParam = new SqlParameter("@QuestionText", SqlDbType.NVarChar);
			SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
			BlogIDParam.Value = _SigninSessionInfo.BlogID;
			QuestionTextParam.Value = this.Request.Form["question"];
			DateParam.Value = DateTime.Now;
			command.Parameters.Add(BlogIDParam);
			command.Parameters.Add(QuestionTextParam);
			command.Parameters.Add(DateParam);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			this.LabelQuestionID.Text = ((Int64)reader["id"]).ToString();
			reader.Close();
			connection.Close();
			command.Dispose();
			this.message.Text = ".سوال نظرسنجی جدید با موفقیت به پایگاه داده سیستم وارد شد";
			this.message.Visible = true;
			this.delete.Enabled = true;
			this.modify.Enabled = true;
			this.define.Enabled = false;
			this.response.Enabled = true;
			this.define_response.Enabled = true;
			this.MessageResponse.Visible = false;
			BindGrid();
            this.data.Visible = true;
            this.GridMessage.Visible = false;
			return ;
		}
		//--------------------------------------------------------------------------------
		protected void delete_Click(object sender, System.EventArgs e)
		{
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("DELETE FROM {0} WHERE BlogID={1} AND id={2}; DELETE FROM {3} WHERE BlogID={1} AND QuestionID={2}; ", 
					     constants.SQLPollQuestionsTableName, _SigninSessionInfo.BlogID, this.LabelQuestionID.Text, constants.SQLPollResponsesTableName);
			command.ExecuteNonQuery();
			connection.Close();
			command.Dispose();
			this.message.Text = ". نظرسنجی مربوطه با موفقیت از پایگاه داده سیستم حذف شد";
			this.question.Text = "";
			this.message.Visible = true;
			this.delete.Enabled = false;
			this.modify.Enabled = false;
			this.define.Enabled = true;
		    this.response.Enabled = false;
			this.define_response.Enabled = false;
			this.MessageResponse.Visible = false;
			this.GridMessage.Visible = true;
			this.data.DataSource = null;
			this.data.DataBind();
			return ;
		}
		//--------------------------------------------------------------------------------
		protected void modify_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["question"] == null || this.Request.Form["question"] == "")
			{
				this.question.Text = "";
				this.message.Text = ".سوال نظرسنجی خالی است";
				this.message.Visible = true;
				return ;
			}
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("UPDATE {0} SET QuestionText=@QuestionText WHERE BlogID={1} AND id={2}", constants.SQLPollQuestionsTableName, _SigninSessionInfo.BlogID, this.LabelQuestionID.Text);
			SqlParameter QuestionTextParam = new SqlParameter("@QuestionText", SqlDbType.NVarChar);
			QuestionTextParam.Value = this.Request.Form["question"];
			command.Parameters.Add(QuestionTextParam);
			command.ExecuteNonQuery();
			connection.Close();
			command.Dispose();
			this.message.Text = ".سوال نظرسنجی با موفقیت در پایگاه داده سیستم به روز رسانی شد";
			this.message.Visible = true;
			this.delete.Enabled = true;
			this.modify.Enabled = true;
			this.define.Enabled = false;
			this.response.Enabled = true;
			this.define_response.Enabled = true;
			this.MessageResponse.Visible = false;
			BindGrid();
			return ;
		}
		//--------------------------------------------------------------------------------
		protected void define_response_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["response"] == null || this.Request.Form["response"] == "")
			{
				this.response.Text = "";
				this.MessageResponse.Text = ".جواب نظرسنجی خالی است";
				this.MessageResponse.Visible = true;
				return ;
			}
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("INSERT INTO {0} (QuestionID,BlogID,ResponseText,PollNumbers) VALUES(@QuestionID,@BlogID,@ResponseText,0)", constants.SQLPollResponsesTableName);
			SqlParameter QuestionIDParam = new SqlParameter("@QuestionID", SqlDbType.BigInt);
			SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
			SqlParameter ResponseTextParam = new SqlParameter("@ResponseText", SqlDbType.NVarChar);
			QuestionIDParam.Value = Convert.ToInt64(this.LabelQuestionID.Text);
			BlogIDParam.Value = _SigninSessionInfo.BlogID;
			ResponseTextParam.Value = this.Request.Form["response"];
			command.Parameters.Add(QuestionIDParam);
			command.Parameters.Add(BlogIDParam);
			command.Parameters.Add(ResponseTextParam);
			command.ExecuteNonQuery();
			connection.Close();
			command.Dispose();
			this.MessageResponse.Text = ".جواب جدید نظرسنجی با موفقیت به پایگاه داده سیستم وارد شد";
			this.MessageResponse.Visible = true;
			this.delete.Enabled = true;
			this.modify.Enabled = true;
			this.define.Enabled = false;
			this.response.Enabled = true;
			this.define_response.Enabled = true;
			this.GridMessage.Visible = false;
			this.message.Visible = false;
			this.response.Text = "";
			BindGrid();
			return ;
		}
		//--------------------------------------------------------------------------------
		private void BindGrid() 
		{
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("SELECT id,ResponseText FROM {0} WHERE QuestionID={1} AND BlogID={2}", constants.SQLPollResponsesTableName, this.LabelQuestionID.Text, _SigninSessionInfo.BlogID);
			SqlDataReader reader = command.ExecuteReader();
			if(reader.HasRows)
			{
				DataTable dt = new DataTable();
				DataRow dr;
				dt.Columns.Add(new DataColumn("id", typeof(string)));
				dt.Columns.Add(new DataColumn("ResponseText", typeof(string)));
				while(reader.Read())
				{
					dr = dt.NewRow();
					dr[1] = reader.GetString(1);
					dr[0] = reader.GetInt64(0);;
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
			connection.Close();
			command.Dispose();
			return ;
		}
		//--------------------------------------------------------------------------------
        protected void data_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("DELETE FROM {0} WHERE id={1} AND BlogID={2}", constants.SQLPollResponsesTableName, e.Item.Cells[0].Text, _SigninSessionInfo.BlogID);
			command.ExecuteNonQuery();
			connection.Close();
			command.Dispose();
			this.MessageResponse.Text = ".پاسخ مربوطه با موفقیت از سیستم حذف شد";
			this.MessageResponse.Visible = true;
			this.GridMessage.Visible = false;
			this.response.Enabled = true;
			this.define_response.Enabled = true;
			this.response.Text = "";
			BindGrid();
			return ;
		}
		//--------------------------------------------------------------------------------
	}
}
