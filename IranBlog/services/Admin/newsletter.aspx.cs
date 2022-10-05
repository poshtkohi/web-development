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
using System.Web.Mail;
using AlirezaPoshtkoohiLibrary;
using System.IO;
using System.Threading;

namespace services
{
	/// <summary>
	/// Summary description for newsletter.
	/// </summary>
	public partial class newsletter : System.Web.UI.Page
	{
		private Thread worker = null;
		//-----------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}
		//-----------------------------------------------------------------
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
		//-----------------------------------------------------------------
		protected void send_Click(object sender, System.EventArgs e)
		{
			if(this.Request.Form["verify"] == "000")
			{
				/*invitation();
				return ;*/
				/*this.worker = new Thread(new ThreadStart(this.invitation));
				this.worker.Start();
				while(true)
				{
					if(!worker.IsAlive)
						break;
					Thread.Sleep(1);
				}*/
				
				StreamWriter sw = new StreamWriter(this.Request.PhysicalApplicationPath + @"services\Admin\emailed.cs");
				MailMessage MailSender = new MailMessage();
				MailSender.BodyEncoding = System.Text.Encoding.UTF8;
				MailSender.BodyFormat = MailFormat.Html;
				MailSender.Priority = MailPriority.High;
				MailSender.To = "alireza_electronics_net@yahoo.com";
				MailSender.From = constants.IranBlogWelcomeEmail;
				MailSender.Subject="News of www.IranBlog.com (Akhbare IranBlog)";
				//MailSender.Subject="Download Album Besiar Zibaie Ghasasm ba Sedaie Shahram Solati (www.IranBlog.com)";
				MailSender.Body = this.Request.Form["message"];
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication   
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", constants.IranBlogWelcomeEmailUsername); //set your username here  
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", constants.IranBlogWelcomeEmailPassword);

				SmtpMail.SmtpServer = constants.IranBlogWelcomeEmailSmtpAddress;
				SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
				connection.Open();
				SqlCommand command = connection.CreateCommand();
				command.Connection = connection;
				command.CommandText = string.Format("SELECT email,i FROM {0}", constants.SQLUsersInformationTableName);
				SqlDataReader reader = command.ExecuteReader();
				int i = 0;
				while(reader.Read())



				{
					if(!reader.IsDBNull(0))
					{
						string email = reader.GetString(0);
						if(email.IndexOf("@") > 0 && email.IndexOf(".") > 0)
						{
							MailSender.To = email;
							SmtpMail.Send(MailSender);
							sw.WriteLine(reader.GetInt64(1).ToString() + ": " + email);
							sw.Flush();
							i++;
						}
					}
				}
				sw.Close();
				reader.Close();
				connection.Close();
				command.Dispose();
				this.message.Text = String.Format("Sent messages are {0} in whole.", i);
			}
			else
				this.message.Text = "Incorrect Verify Code.";
		}
		//-----------------------------------------------------------------
		private void invitation()
		{
			StreamWriter sw = new StreamWriter(this.Request.PhysicalApplicationPath + @"services\Admin\emailed.cs");
			StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"services\Admin\10000.cs");
			MailMessage MailSender = new MailMessage();
			MailSender.BodyEncoding = System.Text.Encoding.UTF8;
			MailSender.BodyFormat = MailFormat.Html;
			MailSender.Priority = MailPriority.High;
			MailSender.To = "alireza_electronics_net@yahoo.com";
			MailSender.From = "IranBlog<welcome@iranblog.com>";
			MailSender.Subject="Ba Salam, www.IranBlog.com";
			//MailSender.Subject="Download Album Besiar Zibaie Ghasasm ba Sedaie Shahram Solati (www.IranBlog.com)";
			MailSender.Body = this.Request.Form["message"];
			MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication   
			MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", constants.IranBlogWelcomeEmailUsername); //set your username here  
			MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", constants.IranBlogWelcomeEmailPassword);

			SmtpMail.SmtpServer = constants.IranBlogWelcomeEmailSmtpAddress;
			int i = 1;
			string email;
			while(true)
			{
				email = sr.ReadLine();
				if(email == null)
					break;
				if(email.IndexOf("@") > 0 && email.IndexOf(".") > 0)
				{
					if(i > 972)
					{
						MailSender.To = email;
						SmtpMail.Send(MailSender);
						sw.WriteLine(i.ToString() + ": " + email);
						sw.Flush();
					}
					i++;
				}
			}
			sw.Close();
			this.message.Text = String.Format("Sent messages are {0} in whole.", i);
		}
		//-----------------------------------------------------------------
	}
}
