/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Web.Mail;

using AlirezaPoshtkoohiLibrary;

namespace services
{
	public class Email
	{
		private string To;
		private string From;
		//--------------------------------------------------------
		public Email(string To, string From)
		{
			this.To = To;
			this.From = From;
		}
		//--------------------------------------------------------
		public int ForgottenPassword(string username, string password)
		{
			try
			{
				MailMessage MailSender = new MailMessage();
				MailSender.BodyEncoding = System.Text.Encoding.UTF8;
				MailSender.BodyFormat = MailFormat.Html;
				MailSender.Priority = MailPriority.High;
				MailSender.To = this.To;
				MailSender.From = this.From;
				MailSender.Subject="IranBlog Account(Forgotten Password)";
				MailSender.Body="Hello Dear IranBlog User.<br><br>Your Username : " + username + 
                                "<br>Your New Password : " + password + "<br><br><br><a href='http://www.iranblog.com'>Website:www.iranblog.com</a><br><br><a href='mailto:alireza.poshtkohi@yahoo.com'>Technical Support.<br>Email:  alireza.poshtkohi@yahoo.com</a>";
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication   
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", constants.IranBlogWelcomeEmailUsername); //set your username here  
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", constants.IranBlogWelcomeEmailPassword);
				SmtpMail.SmtpServer = constants.IranBlogWelcomeEmailSmtpAddress;
				SmtpMail.Send(MailSender);
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
		public int RegisterAccount(string username, string password)
		{
			try
			{
				MailMessage MailSender = new MailMessage();
				MailSender.BodyEncoding = System.Text.Encoding.UTF8;
				MailSender.BodyFormat = MailFormat.Html;
				MailSender.Priority = MailPriority.High;
				MailSender.To = this.To;
				MailSender.From = this.From;
				MailSender.Subject="IranBlog Account";
				MailSender.Body="Hello Dear New IranBlog User.<br><br>Your Username : " + username + 
					"<br>Your Password : " + password + "<br><br><br><a href='http://www.iranblog.com'>Website:www.iranblog.com</a><br><br><a href='mailto:alireza.poshtkohi@yahoo.com'>Technical Support.<br>Email:  alireza.poshtkohi@yahoo.com</a>";
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication   
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", constants.IranBlogWelcomeEmailUsername); //set your username here  
				MailSender.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", constants.IranBlogWelcomeEmailPassword);
				SmtpMail.SmtpServer = constants.IranBlogWelcomeEmailSmtpAddress;
				SmtpMail.Send(MailSender);
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
	}
}