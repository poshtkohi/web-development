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
using System.IO;
using AlirezaPoshtkoohiLibrary;
using ServerManagement;
using System.Management;
using System.DirectoryServices;
using System.Threading;


namespace services
{
	/// <summary>
	/// Summary description for restore.
	/// </summary>
	public partial class restore : System.Web.UI.Page
	{
		private Thread worker = null;
	
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
		//------------------------------------------------------------
		protected void restore1_Click(object sender, System.EventArgs e)
		{
			Restore();
			/*this.worker = new Thread(new ThreadStart(this.Restore));
			this.worker.Start();
			Thread.Sleep(1000);
			while(true)
			{
				if(!worker.IsAlive)
					break;
				Thread.Sleep(1);
			}*/
		}
		//------------------------------------------------------------
		private void Restore()
		{
			//------------------------www restore----------------------------------------
			/*DNS dns = new DNS(constants.password);
			IIS iis = new IIS(constants.password);
			//dns.CreateZone(constants.DNSServerAddress, "iranblog.com", 0);
			//dns.CreateRecord(constants.DNSServerAddress, 
			//	"", "test.com", constants.DnsDomainIPBlog);
			//dns.CreateRecord(constants.DNSServerAddress, 
			//	"iranblog.com", "www", constants.DnsDomainIPBlog);
			//return ;
			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("SELECT subdomain,i FROM {0}", constants.SQLUsersInformationTableName);
			StreamWriter sw = new StreamWriter(this.Request.PhysicalApplicationPath + @"services\Admin\created.cs");
			SqlDataReader reader = command.ExecuteReader();
			while(reader.Read())
			{
				if(!reader.IsDBNull(0))
				{
					string subdomain = reader.GetString(0).Trim().ToLower();
					if(String.Compare(subdomain , "www") != 0 && subdomain.IndexOf(".") < 0 && subdomain.IndexOf("-") < 0 &&
						String.Compare(subdomain , "wwww") != 0 && subdomain.IndexOf("?") < 0 && subdomain.IndexOf("/") < 0 &&
						subdomain.IndexOf(" ") < 0 && subdomain.IndexOf("&") < 0)
					{
						sw.WriteLine(String.Format("id: {0}  subdomain: {1}", reader.GetInt64(1).ToString(), subdomain));
						sw.Flush();
						//dns.DeleteRecord(constants.DNSServerAddress, constants.ZoneName, subdomain);
						//dns.DeleteRecord(constants.DNSServerAddress, constants.ZoneName, "www." + subdomain);
						//dns.CreateRecord(constants.DNSServerAddress, 
						//	constants.ZoneName, subdomain, constants.DnsDomainIPBlog);
						//dns.CreateRecord(constants.DNSServerAddress, 
						//	constants.ZoneName, "www." + subdomain, constants.DnsDomainIPBlog);
						iis.CreateWebSite(subdomain, "", constants.DnsDomainIPBlog, "80", constants.ZoneName, 1, false);
					}
				}
			}
			sw.Close();
			dns.Dispose();
			iis.Dispose();
			reader.Close();
			connection.Close();
			command.Dispose();*/
			//------------------------template restore------------------------------------
			StreamReader s = File.OpenText(constants.TemplatesPath + "Template22.html");
			byte[] buffer = new byte[s.BaseStream.Length];
			s.BaseStream.Read(buffer, 0, buffer.Length);
			s.Close();
			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = string.Format("SELECT subdomain FROM {0}", constants.SQLUsersInformationTableName);
			SqlDataReader reader = command.ExecuteReader();
			while(reader.Read())
			{
				if(!reader.IsDBNull(0))
				{
					try
					{
						string path = constants.RootDircetoryWeblogs + @"\" + reader.GetString(0);
						if(!Directory.Exists(path))
							Directory.CreateDirectory(path);
						FileStream fs = File.Open(path + @"\Default.html", FileMode.Create, FileAccess.Write, FileShare.None);
						fs.Write(buffer, 0, buffer.Length);
						fs.Close();
					}
					catch{}
				}
			}
			reader.Close();
			//connection.Close();
			command.Dispose();
			//----------------------------------------------------------------------------
		}
		//------------------------------------------------------------
	}

}
