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
	/// Summary description for Redirect.
	/// </summary>
	public partial class Redirect : System.Web.UI.Page
	{
		//--------------------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string id = this.Request.QueryString["id"];
			if(id != null && id != "")
			{
				try { Convert.ToInt32(id); }
				catch 
				{
					this.Response.Redirect(constants.WeblogUrl, true);
					return ;
				}
				SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.Connection = connection;
				command.CommandText = string.Format("UPDATE {0} SET views=views+1 WHERE id={1}; SELECT url FROM {0} WHERE id={1};", constants.SQLLinkBoxTableName, id);
				SqlDataReader reader = command.ExecuteReader();
				string url = constants.WeblogUrl;
				if(reader.Read())
					url =  (string)reader["url"];
				reader.Close();
				connection.Close();
				command.Dispose();
				this.Response.Redirect(url, true);
				return ;
			}
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
	}
}
