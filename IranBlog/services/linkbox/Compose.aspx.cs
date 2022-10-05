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

namespace services.linkbox
{
	/// <summary>
	/// Summary description for Compose.
	/// </summary>
	public partial class Compose : System.Web.UI.Page
	{
		/*protected System.Web.UI.WebControls.TextBox url;
		protected System.Web.UI.WebControls.TextBox title;
		protected System.Web.UI.WebControls.TextBox name;
		protected System.Web.UI.WebControls.Button save;*/
		//--------------------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
		/*private void save_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["url"] == null || this.Request.Form["url"] == "")
			{
				this.message.Text = ". پیوند خالی است";
				this.message.Visible = true;
				return ;
			}
			if(this.Request.Form["title"] == null || this.Request.Form["title"] == "")
			{
				this.message.Text = ".عنوان پیوند خالی است";
				this.message.Visible = true;
				return ;
			}

			string subdomain = FindSubdomain(this);
			if(string.Compare(subdomain, "www") == 0 || string.Compare(subdomain, "iranblog") == 0)
			{
				this.Response.Redirect(constants.WeblogUrl, true);
				return ;
			}
			//----------www---------------------
			int p = subdomain.IndexOf(".");
			if(p > 0)
				subdomain = subdomain.Substring(p + 1);
			//----------------------------------
			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("SELECT i FROM {0} WHERE subdomain='{1}'", constants.SQLUsersInformationTableName, subdomain);
			SqlDataReader reader = command.ExecuteReader();
			Int64 BlogID = -1;
			if(reader.Read())
			{
				BlogID = (Int64)reader["i"];
			}
			else
			{
				reader.Close();
				connection.Close();
				command.Dispose();
				this.Response.Redirect(constants.WeblogUrl, true);
				return ;	
			}
			reader.Close();
			connection.Close();
			command.Dispose();

			connection.Open();
			command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1}", constants.SQLLinkBoxTableName, BlogID);
			reader = command.ExecuteReader();
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
			BlogIDParam.Value = BlogID;
			TitleParam.Value = this.Request.Form["title"];
			UrlParam.Value = this.Request.Form["url"];
			NameParam.Value = this.Request.Form["name"];
			LinkboxCounterParam.Value = LinkboxCounter + 1;
			DateParam.Value = DateTime.Now;
			command.Parameters.Add(BlogIDParam);
			command.Parameters.Add(TitleParam);
			command.Parameters.Add(UrlParam);
			command.Parameters.Add(NameParam);
			command.Parameters.Add(DateParam);
			command.Parameters.Add(LinkboxCounterParam);
			command.ExecuteNonQuery();
			connection.Close();
			command.Dispose();
			this.title.Text = "";
			this.url.Text = "";
			this.name.Text = "";
			this.message.Text = ".لینک جدید با موفقیت به پایگاه داده سیستم وارد شد";
			this.message.Visible = true;
		}
		//--------------------------------------------------------------------------------
		static private string FindSubdomain(Page page)
		{
			//return page.Request.Url.Host.Substring(0, page.Request.Url.Host.IndexOf('.'));
			if(String.Compare(page.Request.Url.Host, constants.DomainBlog) == 0)
				return page.Request.Url.Host.Substring(0, page.Request.Url.Host.IndexOf('.'));
			else 
				return page.Request.Url.Host.Substring(0, page.Request.Url.Host.IndexOf("." + constants.DomainBlog));
		}
		//--------------------------------------------------------------------------------*/

	}
}
