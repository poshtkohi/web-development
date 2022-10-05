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
	/// Summary description for PostArchive.
	/// </summary>
	public partial class PostArchive : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

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
		//------------------------------------------------------------------------------------------
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			SqlConnection connection1 = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection1.Open();
			SqlCommand command1 = connection1.CreateCommand();
			command1.Connection = connection1;
			command1.CommandText = "SELECT PostNum FROM usersInfo WHERE subdomain IS NOT NULL";
			SqlDataReader reader1 = command1.ExecuteReader();

			while(reader1.Read())
			{
				this.Response.Write((int)reader1["PostNum"] + "<br>");
				this.Response.Flush();
			}
			reader1.Close();
			connection1.Close();
			/*SqlConnection connection1 = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection1.Open();
			SqlCommand command1 = connection1.CreateCommand();
			SqlTransaction trans1;
			command1.Connection = connection1;
			trans1 = connection1.BeginTransaction();
			command1.Transaction = trans1;
			command1.CommandText = "SELECT i FROM usersInfo WHERE subdomain IS NOT NULL";
			SqlDataReader reader1 = command1.ExecuteReader();


			SqlConnection connection2 = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
			connection2.Open();
			SqlCommand command2 = connection2.CreateCommand();
			SqlTransaction trans2;
			command2.Connection = connection2;
			trans2 = connection2.BeginTransaction();
			command2.Transaction = trans2;
			SqlDataReader reader2;

			
			ArrayList al1 = new ArrayList();
			while(reader1.Read())
			{
				al1.Add((Int64)reader1["i"]);
			}
			reader1.Close();

			for(int i = 0 ; i < al1.Count ; i++)
			{
				command2.CommandText = string.Format("SELECT count(*) FROM posts WHERE BlogID={0}", (Int64)al1[i]);
				reader2 = command2.ExecuteReader();
				reader2.Read();
				int PostNum = reader2.GetInt32(0);
				reader2.Close();
				if(PostNum > 0)
				{
					command1.CommandText = string.Format("UPDATE usersInfo Set PostNum={1} WHERE i={0}", (Int64)al1[i], PostNum);
					command1.ExecuteNonQuery();
				}
			}
			trans1.Commit();
			trans2.Commit();
			connection1.Close();
			connection2.Close();*/
			/*SqlConnection connection1 = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection1.Open();
			SqlCommand command1 = connection1.CreateCommand();
			SqlTransaction trans1;
			command1.Connection = connection1;
			trans1 = connection1.BeginTransaction();
			command1.Transaction = trans1;
			SqlDataReader reader1;


			SqlConnection connection2 = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
			connection2.Open();
			SqlCommand command2 = connection2.CreateCommand();
			SqlTransaction trans2;
			command2.Connection = connection2;
			trans2 = connection2.BeginTransaction();
			command2.Transaction = trans2;
			SqlDataReader reader2;

			command1.CommandText = "SELECT id,BlogID FROM SubjectedArchive WHERE PostNum > 0";
			reader1 = command1.ExecuteReader();
			ArrayList al1 = new ArrayList();
			while(reader1.Read())
			{
				string[] temp = new string[2];
				temp[0] = ((Int64)reader1["id"]).ToString();
				temp[1] = ((Int64)reader1["BlogID"]).ToString();
				al1.Add(temp);
			}
			reader1.Close();

			for(int i = 0 ; i < al1.Count ; i++)
			{
				string[] temp = (string[])al1[i];
				command2.CommandText = string.Format("SELECT count(*) FROM posts WHERE BlogID={1} AND CategoryID={0}", temp[0], temp[1]);
				reader2 = command2.ExecuteReader();
				reader2.Read();
				int PostNum = reader2.GetInt32(0);
				reader2.Close();
				if(PostNum > 0)
				{
					command1.CommandText = string.Format("UPDATE SubjectedArchive Set PostNum={2} WHERE BlogID={1} AND id={0}", temp[0], temp[1], PostNum);
					command1.ExecuteNonQuery();
				}
			}
			trans1.Commit();
			trans2.Commit();
			connection1.Close();
			connection2.Close();*/

			/*SqlConnection connection1 = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection1.Open();
			SqlCommand command1 = connection1.CreateCommand();
			command1.Connection = connection1;
			command1.CommandText = "SELECT id,BlogID FROM SubjectedArchive WHERE PostNum > 0";
			SqlDataReader reader1 = command1.ExecuteReader();


			SqlConnection connection2 = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
			connection2.Open();
			SqlCommand command2 = connection2.CreateCommand();
			SqlTransaction trans;
			command2.Connection = connection2;
			trans = connection2.BeginTransaction();
			command2.Transaction = trans;
			SqlDataReader reader2;


			while(reader1.Read())
			{
				command2.CommandText = string.Format("SELECT id FROM posts WHERE BlogID={1} AND CategoryID={0}", (Int64)reader1["id"], (Int64)reader1["BlogID"]);
				reader2 = command2.ExecuteReader();
				ArrayList al = new ArrayList();
				while(reader2.Read())
				{
					al.Add((Int64)reader2["id"]);
				}
				reader2.Close();
				if(al.Count > 0)
				{
					this.Response.Write(al.Count + "<br>");
					this.Response.Flush();
					string query = "";
					for(int i = 0 ; i < al.Count ; i++)
					{
						query += string.Format("UPDATE posts Set CategoryCounter={0} WHERE id={1};", i + 1, (Int64)al[i]);
					}
					command2.CommandText = query;
					command2.ExecuteNonQuery();
				}
			}
			trans.Commit();
			connection2.Close();*/
		}
		//------------------------------------------------------------------------------------------
	}
}
