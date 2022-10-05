/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace AlirezaPoshtkoohiLibrary
{
	//==========================================================================================================================
	public class SettingsPage : IDisposable
	{
		private string title;
		private string about;
		private bool emailEnable;
		public string category;
        private int _MaxPostShow = 10;
        private bool _ArciveDisplayMode = false;
        private string _ImageGuid;
		private Component component = new Component();
		private bool disposed = false;
        public SettingsPage(string title, string about, bool emailEnable, string category, int MaxPostShow, bool ArciveDisplayMode, string ImageGuid)
		{
			this.title = title.Trim();
			this.about = about.Trim();
			this.emailEnable = emailEnable;
			this.category = category.Trim();
            this._MaxPostShow = MaxPostShow;
            this._ArciveDisplayMode = ArciveDisplayMode;
            this._ImageGuid = ImageGuid;
		}
		//---------------------------------------------------
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		//---------------------------------------------------
		private void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					component.Dispose();
				} 
			}
			disposed = true;         
		}
		//---------------------------------------------------
		~SettingsPage()      
		{
			Dispose(false);
		}
		//---------------------------------------------------
		public string Title 
		{
			get 
			{ 
				return this.title;
			}
		}
		//---------------------------------------------------
		public string About
		{
			get 
			{ 
				return this.about;
			}
		}
		//---------------------------------------------------
		public bool EmailEnable
		{
			get 
			{ 
				return this.emailEnable; 
			}
		}
		//---------------------------------------------------
		public string Category
		{
			get 
			{ 
				return this.Category; 
			}
		}
        //---------------------------------------------------
        public int MaxPostShow
        {
            get
            {
                return this._MaxPostShow;
            }
        }
        //---------------------------------------------------
        public bool ArciveDisplayMode
        {
            get
            {
                return this._ArciveDisplayMode;
            }
        }
        //---------------------------------------------------
        public string ImageGuid
        {
            get
            {
                return this._ImageGuid;
            }
        }
		//---------------------------------------------------
	}
	//==========================================================================================================================
	public class PostTextBlogPage
	{
		private string subdomain;
		private DateTime date;
		private string username;
		private string subject;
		private string data;
		private string SubjectedArchiveId;
		private string continuedText;
        private bool commentEnabled = true;
        private bool _IsShowCommentsPreVerify = true;
		//---------------------------------------------------
		public PostTextBlogPage(string subdomain, string username, string subject,string data, string SubjectedArchiveId, string continuedText, string CommentEnabled)
		{
			this.subdomain = subdomain.Trim();
			this.date = DateTime.Now;
			this.username = username.Trim();
			this.subject = subject.Trim();
			this.data = data;
			this.SubjectedArchiveId = SubjectedArchiveId.Trim();
			this.continuedText = continuedText;
            if(CommentEnabled == "disabled")
                this.commentEnabled = false;
            if (CommentEnabled == "PreverifyActivate")
                this._IsShowCommentsPreVerify = false;
		}
		//---------------------------------------------------
		public string ContinuedText
		{
			get 
			{ 
				return this.continuedText;
			}
		}
		//---------------------------------------------------
		public string SubjectedArchiveID
		{
			get 
			{ 
				return this.SubjectedArchiveId;
			}
		}
		//---------------------------------------------------
		public string Subdomain 
		{
			get 
			{ 
				return this.subdomain; 
			}
		}
		//---------------------------------------------------
		public DateTime Date 
		{
			get 
			{ 
				return this.date; 
			}
		}
		//---------------------------------------------------
		public string Username 
		{
			get 
			{ 
				return this.username; 
			}
		}
		//---------------------------------------------------
		public string Subject 
		{
			get 
			{ 
				return this.subject; 
			}
		}
		//---------------------------------------------------
		public string Data 
		{
			get 
			{ 
				return this.data; 
			}
		}
        //---------------------------------------------------
        public bool CommentEnabled
        {
            get
            {
                return this.commentEnabled;
            }
        }
        public bool IsShowCommentsPreVerify
        {
            get
            {
                return this._IsShowCommentsPreVerify;
            }
        }
		//---------------------------------------------------
	}
	//==========================================================================================================================
	public class SubdomainInfoPage
	{
		private string username;
		private string subdomain;
		private string title;
		private string category;
		//---------------------------------------------------
		public SubdomainInfoPage(string username, string subdomain, string title, string category)
		{
			this.username = username.Trim();
			this.subdomain = subdomain.Trim();
			this.title = title;
			this.category = category.Trim();
		}
		//---------------------------------------------------
		public string Username 
		{
			get 
			{ 
				return this.username; 
			}
		}
		//---------------------------------------------------
		public string Subdomain 
		{
			get 
			{ 
				return this.subdomain; 
			}
		}
		//---------------------------------------------------
		public string Title
		{
			get 
			{ 
				return this.title; 
			}
		}
		//---------------------------------------------------
		public string Category
		{
			get
			{
				return this.category;
			}
		}
		//---------------------------------------------------
	}
	//==========================================================================================================================
	public class FormInfo
	{
		private string username;
		private string password;
		private string email;
		private string first_name; 
		private string last_name;
		private string gender;
		private string job;
		private string age;
		private DateTime dateRegister;
        private int _MaxPostShow = 10;
        private bool _ArciveDisplayMode = false;
		//---------------------------------------------------
		public FormInfo(string username, string password, string email, string first_name,
            string last_name, string gender, string job, string age, int MaxPostShow, bool ArciveDisplayMode)
		{
			this.username = username.Trim().ToLower();
			this.password = password.Trim();
			this.email = email.Trim();
			this.first_name = first_name.Trim();
			this.last_name = last_name.Trim();
			this.gender = gender.Trim();
			this.job = job.Trim();
			this.age = age.Trim();
			this.dateRegister = DateTime.Now;
            this._MaxPostShow = MaxPostShow;
            this._ArciveDisplayMode = ArciveDisplayMode;
		}
		//---------------------------------------------------
		public string Username 
		{
			get 
			{ 
				return this.username; 
			}
		}
		//---------------------------------------------------
		public string Password 
		{
			get 
			{ 
				return this.password; 
			}
		}
		//---------------------------------------------------
		public string Email 
		{
			get 
			{ 
				return this.email; 
			}
		}
		//---------------------------------------------------
		public string FirstName 
		{
			get 
			{ 
				return this.first_name; 
			}
		}
		//---------------------------------------------------
		public string LastName 
		{
			get 
			{ 
				return this.last_name; 
			}
		}
		//---------------------------------------------------
		public string Gender
		{
			get 
			{ 
				return this.gender; 
			}
		}
		//---------------------------------------------------
		public string Job
		{
			get 
			{ 
				return this.job; 
			}
		}
		//---------------------------------------------------
		public string Age
		{
			get 
			{ 
				return this.age; 
			}
		}
		//---------------------------------------------------
		public DateTime DateRegister
		{
			get 
			{ 
				return this.dateRegister; 
			}
		}
        //---------------------------------------------------
        public int MaxPostShow
        {
            get
            {
                return this._MaxPostShow;
            }
        }
        //---------------------------------------------------
        public bool ArciveDisplayMode
        {
            get
            {
                return this._ArciveDisplayMode;
            }
        }
        //---------------------------------------------------
	}
	//==========================================================================================================================
}