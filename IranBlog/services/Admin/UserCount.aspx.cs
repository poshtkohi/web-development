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

namespace services.Admin
{
	/// <summary>
	/// Summary description for UserCount.
	/// </summary>
	public partial class UserCount : System.Web.UI.Page
	{
		//--------------------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("SELECT count(*) FROM {0}", constants.SQLUsersInformationTableName);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			this.Response.Write(reader.GetInt32(0));
			reader.Close();
			//connection.Close();
			command.Dispose();
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
