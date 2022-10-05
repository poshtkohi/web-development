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

namespace services
{
	/// <summary>
	/// Summary description for FindPassword.
	/// </summary>
    public partial class FindIP : System.Web.UI.Page
	{
		//----------------------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}
		//----------------------------------------------------------------------------------
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
		//----------------------------------------------------------------------------------
		protected void find_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["verify"] == "ipiranblog0")
			{
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetIpInfo_IpLog_proc";

                command.Parameters.Add("@subdomain", SqlDbType.VarChar);
                command.Parameters["@subdomain"].Value = this.Request.Form["subdomain"];

				SqlDataReader reader = command.ExecuteReader();
				if(reader.HasRows)
				{
                    while (reader.Read())
                    {
                        if ((int)reader["type"] == 0)
                        {
                            this.LastLoginIP.Text = reader["ip"].ToString();
                            this.LastLoginDate.Text = reader["date"].ToString();
                        }
                        if ((int)reader["type"] == 1)
                        {
                            this.RegisterIP.Text = reader["ip"].ToString();
                            this.RegisterDate.Text = reader["date"].ToString();
                        }
                    }
				}
				else
					this.subdomain.Text = "Not Found.";

				reader.Close();
				command.Dispose();
			}
			else
                this.subdomain.Text = "Incorrect Verify Code.";
		}
		//----------------------------------------------------------------------------------
	}
}
