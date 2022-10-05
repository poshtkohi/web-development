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

using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;

namespace services.linkbox
{
	/// <summary>
	/// Summary description for Show.
	/// </summary>
	public partial class Show : System.Web.UI.Page
	{
		//--------------------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string subdomain = FindSubdomain(this);
			if(string.Compare(subdomain, "www") == 0 || string.Compare(subdomain, "iranblog") == 0)
			{
				this.Response.Redirect(constants.WeblogUrl, true);
				return ;
			}
			else
			{
				//----------www---------------------
				int p = subdomain.IndexOf(".");
				if(p > 0)
					subdomain = subdomain.Substring(p + 1);
				//----------------------------------
				SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.Connection = connection;
				command.CommandText = string.Format("SELECT i,HasLinkBox FROM {0} WHERE subdomain='{1}'", constants.SQLUsersInformationTableName, subdomain);
				SqlDataReader reader = command.ExecuteReader();
				Int64 BlogID = -1;
				bool HasLinkBox;
				if(reader.Read())
				{
					BlogID = (Int64)reader["i"];
					HasLinkBox = (bool)reader["HasLinkBox"];
				}
				else
				{
					reader.Close();
					//connection.Close();
					command.Dispose();
					this.Response.Redirect(constants.WeblogUrl, true);
					return ;	
				}
				reader.Close();
				//connection.Close();
				command.Dispose();

				if(!HasLinkBox)
				{
					this.Response.Redirect(constants.WeblogUrl, true);
					return ;	
				}

				string path = constants.RootDircetoryWeblogs + "\\" + subdomain + "\\" + "linkbox.html";
				StreamReader fs = File.OpenText(path);
				string buffer = fs.ReadToEnd();
				fs.Close();

				int p1 = buffer.IndexOf("<{LinkBox}>") + "<{LinkBox}>".Length;
				int p2 = buffer.IndexOf("</{LinkBox}>");
				if(p1 <= 0 || p2 <= 0)
				{
					this.Response.Redirect(constants.WeblogUrl, true);
					return ;	
				}
				buffer = Regex.Replace(buffer, "(\\{\\$\\$ShowLinkBoxUrl\\})", String.Format("http://{0}.{1}/services/linkbox/Show.aspx", subdomain, constants.DomainBlog));

				this.Response.Write(buffer.Substring(0, p1 - "<{LinkBox}>".Length));
				this.Response.OutputStream.Flush();
				string  temp = buffer.Substring(p1, p2 - p1) ;
				string buff = "";

				bool ArchiveMode = false;
				if(this.Request.QueryString["action"] == "archive")
					ArchiveMode = true;

				//connection.Open();
				command = connection.CreateCommand();
				command.Connection = connection;
				if(ArchiveMode)
					command.CommandText = string.Format("SELECT TOP 20 id,title,url,views FROM {0} WHERE BlogID={1} ORDER BY id DESC", constants.SQLLinkBoxTableName, BlogID);
				else
					command.CommandText = string.Format("SELECT id,title,url,views FROM {0} WHERE BlogID={1} ORDER BY id DESC", constants.SQLLinkBoxTableName, BlogID);
				reader = command.ExecuteReader();
				while(reader.Read())
				{
					buff += Regex.Replace(temp, "(\\{\\$\\$title\\})", (string)reader["title"]);
					buff = Regex.Replace(buff, "(\\{\\$\\$url\\})", (string)reader["url"]);
					buff = Regex.Replace(buff, "(\\{\\$\\$views\\})", ((int)reader["views"]).ToString());
					buff = Regex.Replace(buff, "(\\{\\$\\$RedirectID\\})", ((Int64)reader["id"]).ToString());
					//------------------
					this.Response.Write(buff);
					this.Response.OutputStream.Flush();
					buff = "";
				}
				reader.Close();
				//connection.Close();
				command.Dispose();

				this.Response.Write(buffer.Substring(p2 + "</{LinkBox}>".Length));
				this.Response.OutputStream.Flush();
				this.Response.End();
				return;
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
		//--------------------------------------------------------------------------------
		private string FindSubdomain(Page page)
		{
			if(String.Compare(this.Request.Url.Host, constants.DomainBlog) == 0)
				return this.Request.Url.Host.Substring(0, this.Request.Url.Host.IndexOf('.'));
			else 
				return this.Request.Url.Host.Substring(0, this.Request.Url.Host.IndexOf("." + constants.DomainBlog));
		}
		//--------------------------------------------------------------------------------
	}
}
