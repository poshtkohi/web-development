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

using MySql.Data.MySqlClient;
using System.IO;

using news.Classes.Enums;
using news.Classes;

namespace news
{
	/// <summary>
	/// Summary description for NewsGroupsShow.
	/// </summary>
	public class NewsGroupsShow : System.Web.UI.Page
	{
		//--------------------------------------------------------------------------------
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Common.IsLoginProc(this))
				return ;

			LoginInfo _LoginInfo = (LoginInfo)this.Session["LoginInfo"];

			if(!Common.PageAccessControl(this, _LoginInfo.AccountType))
				return ;

			if (this.Request.Form["mode"] != null)
			{
				switch (this.Request.Form["mode"])
				{
					case "ShowNewsGroupsShow":
						ShowNewsGroupsShow();
						return;
					default:
						return;
				}
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
		//--------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------
		private void ShowNewsGroupsShow()
		{
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "ListAllNewsGroups_NewsGropusPage_proc";
			//-------------------------------------------
            
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.HasRows)
			{
				string template = "";
				/*if (this.Cache["_template_NewsGroupsShow"] == null)
				{
					StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\NewsGroupsTemplateShow.html");
					template = sr.ReadToEnd();
					this.Cache["_template_NewsGroupsShow"] = template;
					sr.Close();
				}
				else
					template = (string)this.Cache["_template_NewsGroupsShow"];*/

				StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\NewsGroupsTemplateShow.html");
				template = sr.ReadToEnd();
				sr.Close();

				int _p1Post = template.IndexOf("<post>") + "<post>".Length;
				int _p2Post = template.IndexOf("</post>");
				/*int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
				int _p2Paging = template.IndexOf("</paging>");*/
				if (_p1Post <= 0 || _p2Post <= 0)
				{
					this.Response.Write(template);
					this.Response.OutputStream.Flush();
					return;
				}
				this.Response.Write(template.Substring(0, _p1Post - "<post>".Length));
				this.Response.Flush();
				string _mainFormat = template.Substring(_p1Post, _p2Post - _p1Post);

				string temp = null;
				bool boxing = true;
				while (reader.Read())
				{
					temp = _mainFormat;

					if(boxing)
					{
						temp = temp.Replace("[boxing]", Constants.boxing1);
						boxing = false;
					}
					else
					{
						temp = temp.Replace("[boxing]", Constants.boxing2);
						boxing = true;
					}

					temp = temp.Replace("[id]", reader["id"].ToString());
					temp = temp.Replace("[title]", reader["title"].ToString());

					this.Response.Write(temp);
					this.Response.Flush();
				}

				reader.Close();

				/*if (currentPage == 1)
					this.Session["_ItemNumNewsGroups"] = (int)command.Parameters["?NewsGroupsNum"].Value;

				if (_p1Paging > 0 && _p2Paging > 0)
				{
					this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
					//Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
					Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowNewsGroups", currentPage, Constants.MaxUsersShow, (int)this.Session["_ItemNumNewsGroups"], Constants.UsersPagingNumber, "ShowItems");
				}
				else*/
					this.Response.Write(template.Substring(_p2Post + "</post>".Length));
				this.Response.Flush();

				connection.Close();
				//this.Response.Close();
				this.Response.End();
				return;
			}
			else
			{
				WriteStringToAjaxRequest("NoFoundPost");
				reader.Close();
				connection.Close();

				return;
			}
		}
		//--------------------------------------------------------------------------------
		private void WriteStringToAjaxRequest(string str)
		{
			this.Response.Write(str);
			this.Response.Flush();
			//this.Response.Close();
			this.Response.End();
		}
		//--------------------------------------------------------------------------------
	}
}
