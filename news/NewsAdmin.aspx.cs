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
	/// Summary description for NewsAdmin.
	/// </summary>
	public class NewsAdmin : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label LabelNewGroupTitle;
		//--------------------------------------------------------------------------------
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Common.IsLoginProc(this))
				return ;

			LoginInfo _LoginInfo = (LoginInfo)this.Session["LoginInfo"];

			if(!Common.PageAccessControl(this, _LoginInfo.AccountType))
				return ;


			if (this.Request.QueryString["action"] != null)
			{
				switch (this.Request.QueryString["action"])
				{
					case "print":
						Print(true);
						return;
					case "preview":
						Print(false);
						return;
					default:
						return;
				}
			}

			if (this.Request.Form["mode"] != null)
			{
				switch (this.Request.Form["mode"])
				{
					case "ShowNewsAdmin":
						ShowNewsAdmin(_LoginInfo, ShowNewsAdminMode.ShowNewsAdmin);
						return;
					case "ShowNewsAdminForNewsGroupsMode":
						ShowNewsAdmin(_LoginInfo, ShowNewsAdminMode.ShowNewsAdminForNewsGroupsMode);
						return;
					case "SearchByDate":
						ShowNewsAdmin(_LoginInfo, ShowNewsAdminMode.SearchByDate);
						return;
					case "SearchByText":
						ShowNewsAdmin(_LoginInfo, ShowNewsAdminMode.SearchByText);
						return;
					case "delete":
						NewsAdminDelete();
						return;
					case "DeleteForNewsGroupsMode":
						NewsAdminDeleteForNewsGroupsMode();
						return;
					case "load":
						NewsAdminLoad();
						return;
					case "LoadForNewsGroupsMode":
						LoadForNewsGroupsMode();
						return;
					case "update":
						NewsAdminUpdate();
						return;
					case "UpdateForNewsGroupsMode":
						UpdateForNewsGroupsMode();
						return;
					case "post":
						DoPost(_LoginInfo);
						return;
					case "SelectForNewsGroupNewsAdminMode":
						SelectForNewsGroupNewsAdminMode(_LoginInfo);
						return;
					case "SelectForNewsGroupNewsAdminInNewsGropMode":
						SelectForNewsGroupNewsAdminInNewsGropMode(_LoginInfo);
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
		private void DoPost(LoginInfo _LoginInfo)
		{
			Common.InsertNewPost(_LoginInfo.UserID, this.Request.Form["PostTitle"], this.Request.Form["PostContent"], Convert.ToInt32(this.Request.Form["NewsSubject"]));
			WriteStringToAjaxRequest(".خبر جدید با موفقیت در سیستم وارد شد");

			return;
		}
		//--------------------------------------------------------------------------------
		private void ShowNewsAdmin(LoginInfo _LoginInfo, ShowNewsAdminMode _ShowNewsAdminMode)
		{
			int currentPage = 1;
			try
			{
				currentPage = Convert.ToInt32(this.Request.Form["page"]);
			}
			catch { }

			if (currentPage == 0)
				currentPage++;

			if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin)
				if (currentPage > 1 && this.Session["_ItemNumNewsAdmin"] == null)
				{
					WriteStringToAjaxRequest("DoRefresh");
					return;
				}

			PersianCalendar pc = new PersianCalendar();
			DateTime dt = DateTime.Now;

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;

			if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin)
			{
				command.CommandText = "ListNews_NewsAdminPage_proc";
				//-------------------------------------------

				command.Parameters.Add("?PageSize", MySqlDbType.Int32);
				command.Parameters["?PageSize"].Value = Constants.MaxUsersShow;

				command.Parameters.Add("?PageNumber", MySqlDbType.Int32);
				command.Parameters["?PageNumber"].Value = currentPage;

				command.Parameters.Add("?PostNum", MySqlDbType.Int32);
				command.Parameters["?PostNum"].Direction = ParameterDirection.Output;
			}
			if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdminForNewsGroupsMode)
			{
				command.CommandText = "ListNewsForNewsGroupsMode_NewsAdminPage_proc";
				//-------------------------------------------

				command.Parameters.Add("?_NewsGroupID", MySqlDbType.Int64);
				command.Parameters["?_NewsGroupID"].Value = (Int64)currentPage;

				command.Parameters.Add("?_PersianYear", MySqlDbType.Int32);
				command.Parameters["?_PersianYear"].Value = pc.GetYear(dt);

				command.Parameters.Add("?_PersianMonth", MySqlDbType.Int32);
				command.Parameters["?_PersianMonth"].Value = pc.GetMonth(dt);

				command.Parameters.Add("?_PersianDay", MySqlDbType.Int32);
				command.Parameters["?_PersianDay"].Value = pc.GetDayOfMonth(dt);
			}
			if(_ShowNewsAdminMode == ShowNewsAdminMode.SearchByDate)
			{
				command.CommandText = "ListNewsByDateSearch_NewsAdminPage_proc";
				//-------------------------------------------
				string[] _date = this.Request.Form["SearchDate"].Split('/');

				command.Parameters.Add("?_PersianYear", MySqlDbType.Int32);
				command.Parameters["?_PersianYear"].Value = Convert.ToInt32(_date[0]);

				command.Parameters.Add("?_PersianMonth", MySqlDbType.Int32);
				command.Parameters["?_PersianMonth"].Value = Convert.ToInt32(_date[1]);

				command.Parameters.Add("?_PersianDay", MySqlDbType.Int32);
				command.Parameters["?_PersianDay"].Value = Convert.ToInt32(_date[2]);
			}
			if(_ShowNewsAdminMode == ShowNewsAdminMode.SearchByText)
			{
				if(this.Request.Form["StartDate"] != null && this.Request.Form["StartDate"] != "")
				{
					command.CommandText = "ListNewsByTextSearchAndStartEndDate_NewsAdminPage_proc";

					string[] _StartDate = this.Request.Form["StartDate"].Split('/');
					string[] _EndDate = this.Request.Form["EndDate"].Split('/');

					PersianCalendar _pc = new PersianCalendar();

					DateTime _dtStart = _pc.ToDateTime(Convert.ToInt32(_StartDate[0]), Convert.ToInt32(_StartDate[1]),
						Convert.ToInt32(_StartDate[2]), 0, 0, 0, 0);

					DateTime _dtEnd = _pc.ToDateTime(Convert.ToInt32(_EndDate[0]), Convert.ToInt32(_EndDate[1]),
						Convert.ToInt32(_EndDate[2]), 23, 59, 59, 0);

					command.Parameters.Add("?StartDate", MySqlDbType.Datetime);
					command.Parameters["?StartDate"].Value = _dtStart;

					command.Parameters.Add("?EndDate", MySqlDbType.Datetime);
					command.Parameters["?EndDate"].Value = _dtEnd;

				}
				else
					command.CommandText = "ListNewsByTextSearch_NewsAdminPage_proc";
				command.Parameters.Add("?SearchText", MySqlDbType.LongText);
				command.Parameters["?SearchText"].Value = "%" + this.Request.Form["SearchText"] +"%";
			}

            
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.HasRows)
			{

				//=====================================================
				MySqlConnection _connection = null;
				MySqlCommand _command = null;
				MySqlDataReader _reader = null;

				if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByDate || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByText)
				{
					_connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
					_connection.Open();
					_command = _connection.CreateCommand();
					_command.Connection = _connection;
					_command.CommandType = CommandType.StoredProcedure;
					_command.CommandText = "ListSelectedNewsGroupsForNews_NewsAdminPage_proc";

					_command.Parameters.Add("?_PostID", MySqlDbType.Int64);
					//_command.Parameters["?_PostID"].Value = (Int64)currentPage;

					_command.Parameters.Add("?_PersianYear", MySqlDbType.Int32);
					_command.Parameters["?_PersianYear"].Value = pc.GetYear(dt);

					_command.Parameters.Add("?_PersianMonth", MySqlDbType.Int32);
					_command.Parameters["?_PersianMonth"].Value = pc.GetMonth(dt);

					_command.Parameters.Add("?_PersianDay", MySqlDbType.Int32);
					_command.Parameters["?_PersianDay"].Value = pc.GetDayOfMonth(dt);
				}
				//=====================================================


				string template = "";
				string _operationsTemplate = "";
				/*if (this.Cache["_template_NewsAdmin"] == null)
				{
					StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\NewsAdminTemplate.html");
					template = sr.ReadToEnd();
					this.Cache["_template_NewsAdmin"] = template;
					sr.Close();
				}
				else
					template = (string)this.Cache["_template_NewsAdmin"];*/

				StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\NewsAdminTemplate.html");
				template = sr.ReadToEnd();
				sr.Close();

				sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\NewsAdminOperationsTemplate.html");
				_operationsTemplate = sr.ReadToEnd();
				sr.Close();

				if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin)
				{
					_operationsTemplate = _operationsTemplate.Replace("[mode]", "NewsAdmin");
					template = template.Replace("[mode]", "NewsAdmin");
				}
				if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdminForNewsGroupsMode)
				{
					Common.TagDelete(ref template, "paging");
					Common.TagDelete(ref template, "Involved");
					Common.TagDelete(ref template, "NotInvolved");
					Common.TagDelete(ref template, "paging");
					_operationsTemplate = _operationsTemplate.Replace("[mode]", "NewsAdminForNewsGroupsMode");
					template = template.Replace("[mode]", "NewsAdminForNewsGroupsMode");
				}
				if(_ShowNewsAdminMode == ShowNewsAdminMode.SearchByDate || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByText)
				{
					Common.TagDelete(ref template, "paging");
					_operationsTemplate = _operationsTemplate.Replace("[mode]", "NewsAdmin");
					template = template.Replace("[mode]", "NewsAdmin");
				}

				string _ContextMenuTemplate = FindContextMenuTemplate(_operationsTemplate);
				string _ContextMenuIDsTemplate = FindContextMenuIDsTemplate(_operationsTemplate);

				int _p1Post = template.IndexOf("<post>") + "<post>".Length;
				int _p2Post = template.IndexOf("</post>");
				int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
				int _p2Paging = template.IndexOf("</paging>");
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
				
				ArrayList _al = new ArrayList();
				int index = 0;
				Int64 _CurrentUserID = -1;
				Int64 _CurrentRowID = -1;
				while (reader.Read())
				{
					temp = _mainFormat;

					_CurrentRowID = Convert.ToInt64(reader["id"].ToString());
					//if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin)
						_CurrentUserID = Convert.ToInt64(reader["UserID"].ToString());
			
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

					if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin || _ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdminForNewsGroupsMode || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByDate || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByText)
						_al.Add(new Int64[]{_CurrentRowID, _CurrentUserID});


					//=====================================================
					if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByDate || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByText)
					{
						_command.Parameters["?_PostID"].Value = _CurrentRowID;

						_reader = _command.ExecuteReader();

						string groups = null;
						while(_reader.Read())
						{
							groups += _reader["title"].ToString() + "<br>";
						}
						_reader.Close();

						if(groups == null)
						{
							Common.TagDelete(ref temp, "Involved");
						}
						else
						{
							Common.TagDelete(ref temp, "NotInvolved");
							temp = temp.Replace("[groups]", groups);
						}

					}
					//=====================================================
					temp = temp.Replace("[id]", _CurrentRowID.ToString());
					temp = temp.Replace("[PostTitle]", reader["PostTitle"].ToString());
					temp = temp.Replace("[index]", index.ToString());

					dt = (DateTime)reader["PostDate"];
					temp = temp.Replace("[PostDate]", String.Format("{0}/{1}/{2} {3}:{4}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt), pc.GetHour(dt), pc.GetMinute(dt)));

					this.Response.Write(temp);
					this.Response.Flush();
					index++;
				}

				reader.Close();
				if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin)
				{
					if (currentPage == 1)
						this.Session["_ItemNumNewsAdmin"] = (int)command.Parameters["?PostNum"].Value;

					if (_p1Paging > 0 && _p2Paging > 0)
					{
						this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
						//Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
						Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowNewsAdmin", currentPage, Constants.MaxUsersShow, (int)this.Session["_ItemNumNewsAdmin"], Constants.UsersPagingNumber, "ShowItems");
					}
					else
						this.Response.Write(template.Substring(_p2Post + "</post>".Length));
				}
				else
					this.Response.Write(template.Substring(_p2Post + "</post>".Length));

				this.Response.Flush();


				if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdmin || _ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdminForNewsGroupsMode || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByDate || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByText)
				{
					//-------------------------------------------
					command.CommandText = "ListAllNewsGroups_NewsAdminPage_proc";
					command.Parameters.Clear();

					reader = command.ExecuteReader();
					string _NewsGroups = "";
					int _i = 0;
					string _mode = "SelectForNewsGroupNewsAdminMode";
					if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdminForNewsGroupsMode)
						_mode = "SelectForNewsGroupNewsAdminInNewsGropMode";
					while(reader.Read())
					{
						//_NewsGroups += String.Format("<a href=\"javascript:void(0);\" onClick=\"SelectForNewsGroup('{0}','{1}','[PostID]');\">{2}<br></a>", _mode,reader["id"].ToString(), reader["title"].ToString());
						//ItemLoad('[mode]','[id]');                                                            NewsGroupsID,[PostID]
						_NewsGroups += String.Format("<a href=\"javascript:void(0);\" onClick=\"ItemLoad('{0}','{1},[PostID]');\">{2}<br></a>", _mode,reader["id"].ToString(), reader["title"].ToString());
						_i++;
					}
					temp = null;
					string _tempContextMenu = null;
					string _tempNewsGroups = null;
					Int64[] _CurrentIDs = null;
					for (int i = 0 ; i < _al.Count; i++)
					{
						_CurrentIDs = (Int64[])_al[i];
						_tempContextMenu = _ContextMenuTemplate;
						_tempNewsGroups = _NewsGroups;
						_tempNewsGroups = _tempNewsGroups.Replace("[PostID]", _CurrentIDs[0].ToString());
						_tempContextMenu = _tempContextMenu.Replace("[NewsGroups]", _tempNewsGroups);
						EditDeletePostAccessControl(_CurrentIDs[1], _LoginInfo.UserID, _LoginInfo.AccountType, ref _tempContextMenu);
						this.Response.Write(_tempContextMenu.Replace("[id]", _CurrentIDs[0].ToString()));
						this.Response.Flush();
						if (i == _al.Count - 1)
							temp += String.Format("{0}", _CurrentIDs[0].ToString());
						else
							temp += String.Format("{0},", _CurrentIDs[0].ToString());
					}
					this.Response.Write(_ContextMenuIDsTemplate.Replace("[IDs]", temp));
					this.Response.Flush();
				}

				if(_ShowNewsAdminMode == ShowNewsAdminMode.ShowNewsAdminForNewsGroupsMode || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByDate || _ShowNewsAdminMode == ShowNewsAdminMode.SearchByText)
				{
					//-------------------------------------------
					Common.TagDelete(ref _ContextMenuTemplate, "NewsGroups");
					temp = null;
					string _tempContextMenu = null;
					Int64[] _CurrentIDs = null;
					for (int i = 0 ; i < _al.Count; i++)
					{
						_CurrentIDs = (Int64[])_al[i];
						_tempContextMenu = _ContextMenuTemplate;
						EditDeletePostAccessControl(_CurrentIDs[1], _LoginInfo.UserID, _LoginInfo.AccountType, ref _tempContextMenu);
						this.Response.Write(_tempContextMenu.Replace("[id]", _CurrentIDs[0].ToString()));
						this.Response.Flush();
						if (i == _al.Count - 1)
							temp += String.Format("{0}", _CurrentIDs[0].ToString());
						else
							temp += String.Format("{0},", _CurrentIDs[0].ToString());
					}
					this.Response.Write(_ContextMenuIDsTemplate.Replace("[IDs]", temp));
					this.Response.Flush();
				}



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
		private void EditDeletePostAccessControl(Int64 _CurrentUserID, Int64 _SessionUserID, AccountType _AccountType, ref string buffer)
		{
			if(_AccountType == AccountType.Admin || _AccountType == AccountType.MainEditor || _CurrentUserID == _SessionUserID)
			{
				if(_AccountType == AccountType.NewsEditor)
					Common.TagDelete(ref buffer, "NewsGroups");
				return ;
			}
			else
			{
				Common.TagDelete(ref buffer, "delete");
				Common.TagDelete(ref buffer, "edit");
				Common.TagDelete(ref buffer, "NewsGroups");
				return ;
			}
		}
		//--------------------------------------------------------------------------------
		private string FindContextMenuTemplate(string buffer)
		{
			return Common.GetInternalTagContent(buffer, "template");
		}
		//--------------------------------------------------------------------------------
		private string FindContextMenuIDsTemplate(string buffer)
		{
			return Common.GetInternalTagContent(buffer, "ids");
		}
		//--------------------------------------------------------------------------------
		private void NewsAdminDelete()
		{
			Int64 _DeleteID = -1;
			try
			{
				_DeleteID = Convert.ToInt64(this.Request.Form["DeleteID"]);
			}
			catch { return; }

			Common.DeletePost(_DeleteID);

			WriteStringToAjaxRequest("Success");
			return;
		}
		//--------------------------------------------------------------------------------
		private void NewsAdminDeleteForNewsGroupsMode()
		{
			Int64 _DeleteID = -1;
			try
			{
				_DeleteID = Convert.ToInt64(this.Request.Form["DeleteID"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "DeletePngNews_NewsAdminPage_proc"; 

			command.Parameters.Add("?_PostID", MySqlDbType.Int64);
			command.Parameters["?_PostID"].Value = _DeleteID;

			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();

			WriteStringToAjaxRequest("Success");
			return;
		}
		//--------------------------------------------------------------------------------
		private void NewsAdminLoad()
		{
			Int64 _id = -1;
			try
			{
				_id = Convert.ToInt64(this.Request.Form["id"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "NewsLoad_NewsAdminPage_proc";

			command.Parameters.Add("?_id", SqlDbType.BigInt);
			command.Parameters["?_id"].Value = _id;


			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				WriteStringToAjaxRequest(String.Format("{0},{1},{2}", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["PostTitle"].ToString())),
					System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["PostContent"].ToString())),
					System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["NewsSubject"].ToString()))));

				reader.Close();
				command.Dispose();
				connection.Close();
				return;
			}
			else
			{
				WriteStringToAjaxRequest("NoFoundPost");
				reader.Close();
				command.Dispose();
				connection.Close();
				return;
			}
		}
		//--------------------------------------------------------------------------------
		private void LoadForNewsGroupsMode()
		{
			Int64 _id = -1;
			try
			{
				_id = Convert.ToInt64(this.Request.Form["id"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "NewsLoadForNewsGroupsMode_NewsAdminPage_proc";

			command.Parameters.Add("?_id", SqlDbType.BigInt);
			command.Parameters["?_id"].Value = _id;


			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				WriteStringToAjaxRequest(String.Format("{0},{1},{2}", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["PostTitle"].ToString())),
					System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["PostContent"].ToString())),
					System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["NewsSubject"].ToString()))));

				reader.Close();
				command.Dispose();
				connection.Close();
				return;
			}
			else
			{
				WriteStringToAjaxRequest("NoFoundPost");
				reader.Close();
				command.Dispose();
				connection.Close();
				return;
			}
		}
		//--------------------------------------------------------------------------------
		private void Print(bool _isPrint)
		{
			Int64 _id = -1;
			try
			{
				_id = Convert.ToInt64(this.Request.QueryString["id"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			if(this.Request.QueryString["mode"] == "NewsAdmin")
				command.CommandText = "NewsLoad_NewsAdminPage_proc";

			if(this.Request.QueryString["mode"] == "NewsAdminForNewsGroupsMode")
				command.CommandText = "NewsLoadForNewsGroupsMode_NewsAdminPage_proc";

			else
				command.CommandText = "NewsLoad_NewsAdminPage_proc";

			command.Parameters.Add("?_id", SqlDbType.BigInt);
			command.Parameters["?_id"].Value = _id;


			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				string template = "";
				/*if (this.Cache["_template_NewsAdminPrint"] == null)
				{
					StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\NewsAdminPrintTemplate.html");
					template = sr.ReadToEnd();
					this.Cache["_template_NewsAdminPrint"] = template;
					sr.Close();
				}
				else
					template = (string)this.Cache["_template_NewsAdminPrint"];*/

				StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\AjaxTemplates\NewsAdminPrintTemplate.html");
				template = sr.ReadToEnd();
				sr.Close();

				template = template.Replace("[isPrint]", _isPrint.ToString().ToLower());
				template = template.Replace("[PostContent]", reader["PostContent"].ToString());
				template = template.Replace("[PostTitle]", reader["PostTitle"].ToString());

				reader.Close();
				command.Dispose();
				connection.Close();

			    WriteStringToAjaxRequest(template);
				return;
			}
			else
			{
				reader.Close();
				command.Dispose();
				connection.Close();

				WriteStringToAjaxRequest("NoFoundPost");

				return;
			}
		}
		//--------------------------------------------------------------------------------
		private void SelectForNewsGroupNewsAdminMode(LoginInfo _LoginInfo)
		{
			//IN _NewsGroupID BIGINT,IN _PostID BIGINT,IN _PostDate DATETIME,IN _PersianYear INT,IN _PersianMonth INT,IN _PersianDay INT,OUT IsExisted INT
			Int64 _NewsGroupID = -1;
			try
			{
				_NewsGroupID = Convert.ToInt64(this.Request.Form["NewsGroupID"]);
			}
			catch { return; }

			Int64 _PostID = -1;
			try
			{
				_PostID = Convert.ToInt64(this.Request.Form["PostID"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "SelectForNewsGroupNewsAdminMode_NewsAdminPage_proc"; 


			command.Parameters.Add("?_UserID", MySqlDbType.Int64);
			command.Parameters["?_UserID"].Value = _LoginInfo.UserID;

			command.Parameters.Add("?_NewsGroupID", MySqlDbType.Int64);
			command.Parameters["?_NewsGroupID"].Value = _NewsGroupID;

			command.Parameters.Add("?_PostID", MySqlDbType.Int64);
			command.Parameters["?_PostID"].Value = _PostID;

			PersianCalendar pc = new PersianCalendar();
			DateTime dt = DateTime.Now;

			command.Parameters.Add("?_PersianYear", MySqlDbType.Int32);
			command.Parameters["?_PersianYear"].Value = pc.GetYear(dt);

			command.Parameters.Add("?_PersianMonth", MySqlDbType.Int32);
			command.Parameters["?_PersianMonth"].Value = pc.GetMonth(dt);

			command.Parameters.Add("?_PersianDay", MySqlDbType.Int32);
			command.Parameters["?_PersianDay"].Value = pc.GetDayOfMonth(dt);

			command.Parameters.Add("?_PostDate", MySqlDbType.Datetime);
			command.Parameters["?_PostDate"].Value = dt;

			command.Parameters.Add("?IsExisted", MySqlDbType.Int32);
			command.Parameters["?IsExisted"].Direction = ParameterDirection.Output;

			command.ExecuteNonQuery();
		
			if(Convert.ToBoolean((int)command.Parameters["?IsExisted"].Value))
			{
				command.Dispose();
				connection.Close();

				WriteStringToAjaxRequest("Existed");
				return;
			}
			else
			{
				command.Dispose();
				connection.Close();

				WriteStringToAjaxRequest("Success");
				return;
			}
		}
		//--------------------------------------------------------------------------------
		private void SelectForNewsGroupNewsAdminInNewsGropMode(LoginInfo _LoginInfo)
		{
			//IN _NewsGroupID BIGINT,IN _PostID BIGINT,IN _PostDate DATETIME,IN _PersianYear INT,IN _PersianMonth INT,IN _PersianDay INT,OUT IsExisted INT
			Int64 _NewsGroupID = -1;
			try
			{
				_NewsGroupID = Convert.ToInt64(this.Request.Form["NewsGroupID"]);
			}
			catch { return; }

			Int64 _PostID = -1;
			try
			{
				_PostID = Convert.ToInt64(this.Request.Form["PostID"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "SelectForNewsGroupNewsAdminInNewsGropMode_NewsAdminPage_proc"; 


			command.Parameters.Add("?_UserID", MySqlDbType.Int64);
			command.Parameters["?_UserID"].Value = _LoginInfo.UserID;

			command.Parameters.Add("?_NewsGroupID", MySqlDbType.Int64);
			command.Parameters["?_NewsGroupID"].Value = _NewsGroupID;

			command.Parameters.Add("?_PostID", MySqlDbType.Int64);
			command.Parameters["?_PostID"].Value = _PostID;

			PersianCalendar pc = new PersianCalendar();
			DateTime dt = DateTime.Now;

			command.Parameters.Add("?_PersianYear", MySqlDbType.Int32);
			command.Parameters["?_PersianYear"].Value = pc.GetYear(dt);

			command.Parameters.Add("?_PersianMonth", MySqlDbType.Int32);
			command.Parameters["?_PersianMonth"].Value = pc.GetMonth(dt);

			command.Parameters.Add("?_PersianDay", MySqlDbType.Int32);
			command.Parameters["?_PersianDay"].Value = pc.GetDayOfMonth(dt);

			command.Parameters.Add("?_PostDate", MySqlDbType.Datetime);
			command.Parameters["?_PostDate"].Value = dt;

			command.Parameters.Add("?IsExisted", MySqlDbType.Int32);
			command.Parameters["?IsExisted"].Direction = ParameterDirection.Output;

			command.ExecuteNonQuery();
		
			if(Convert.ToBoolean((int)command.Parameters["?IsExisted"].Value))
			{
				command.Dispose();
				connection.Close();

				WriteStringToAjaxRequest("Existed");
				return;
			}
			else
			{
				command.Dispose();
				connection.Close();

				WriteStringToAjaxRequest("Success");
				return;
			}
		}
		//----------------------------------------------------------------------------------
		private void NewsAdminUpdate()
		{
			Int64 _id = -1;
			try
			{
				_id = Convert.ToInt64(this.Request.Form["id"]);
			}
			catch { return; }

			Common.UpdatePost(_id, this.Request.Form["PostTitle"], this.Request.Form["PostContent"], Convert.ToInt32(this.Request.Form["NewsSubject"]));

			WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

			return;
		}
		//----------------------------------------------------------------------------------
		private void UpdateForNewsGroupsMode()
		{
			Int64 _id = -1;
			try
			{
				_id = Convert.ToInt64(this.Request.Form["id"]);
			}
			catch { return; }

			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "UpdateNewsForNewsGroupsMode_NewsAdminPage_proc"; 


			command.Parameters.Add("?PostID", MySqlDbType.Int64);
			command.Parameters["?PostID"].Value = _id;

			command.Parameters.Add("?_PostTitle", MySqlDbType.LongText);
			command.Parameters["?_PostTitle"].Value = this.Request.Form["PostTitle"];

			command.Parameters.Add("?_PostContent", MySqlDbType.LongText);
			command.Parameters["?_PostContent"].Value = this.Request.Form["PostContent"];

			command.Parameters.Add("?_NewsSubject", MySqlDbType.Int32);
			command.Parameters["?_NewsSubject"].Value = Convert.ToInt32(this.Request.Form["NewsSubject"]);


			command.ExecuteNonQuery();
		
			command.Dispose();
			connection.Close();

			WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

			return ;
		}
		//--------------------------------------------------------------------------------
		public static string GetNewsGrroupTitleByNewsGroupID(Int64 _NewsGroupID)
		{
			//return _NewsGroupID.ToString();
			MySqlConnection connection = new MySqlConnection(Constants.ConnectionStringNewsDatabase);
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "GetNewsGrroupTitleByNewsGroupID_NewsGropusPage_proc"; 


			command.Parameters.Add("?NewsGroupID", MySqlDbType.Int64);
			command.Parameters["?NewsGroupID"].Value = _NewsGroupID;

			/*command.Parameters.Add("?NewsGroupTitle", MySqlDbType.LongText);
			command.Parameters["?NewsGroupTitle"].Direction = ParameterDirection.Output;*/


			MySqlDataReader reader = command.ExecuteReader();
		
			string NewsGroupTitle = "";
			if(reader.HasRows)
				if(reader.Read())
					NewsGroupTitle = reader["title"].ToString();

			reader.Close();
			command.Dispose();
			connection.Close();

			if(NewsGroupTitle == "")
				return "";
			else
				return String.Format(" شما در بخش خبری <font color='#FF0000'>\"{0}\"</font> می باشید.", NewsGroupTitle);
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