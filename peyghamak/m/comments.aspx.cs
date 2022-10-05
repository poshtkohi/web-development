/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Peyghamak.mobile
{
    public partial class comments : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Login.IsLoginProc(this);
			
            string _subdomain = Login.FindSubdomain(this);			

            if (_subdomain == "")
            {
				this.Response.Redirect(String.Format("http://www.{0}/m/login.aspx", Constants.BlogDomain), true);
                return;
            }

            bool _IsMyCommentsPage = IsMyCommentsPage(_subdomain, _IsLogin);
            bool _IsOtherCommentsPage = false;
            if (!_IsMyCommentsPage)
                _IsOtherCommentsPage = IsOtherCommentsPage(_subdomain);			
			            
			string mode = this.Request.QueryString["mode"];
			if( mode==null ) mode = this.Request.Form["mode"];
			if( mode==null ) mode = "ShowPostComments";

			string PostID = this.Request.QueryString["PostID"];
			if( PostID==null ) PostID = this.Request.Form["PostID"];
			Int64 _PostID = Convert.ToInt64(PostID);
			if (_PostID <= 0)
			{
				this.Response.Redirect(String.Format("http://www.{0}/m/login.aspx", Constants.BlogDomain), true);
				return;
			}
			
			PageSettings(_IsMyCommentsPage, _IsOtherCommentsPage, _IsLogin, _PostID);
			
            if( !CheckCorrectComment(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage) )
            {
                this.Response.Redirect(String.Format("http://www.{0}/m/login.aspx", Constants.BlogDomain), true);
                return;
            }			
							
			switch (mode)
			{
				case "ShowPostComments":
					ShowPostComments(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
					return;
				case "CommentDelete":
					CommentDelete(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage, _subdomain);
					return;
				case "post":
					DoPost(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage, _subdomain);
					return;
				default:
					return;
			}
        }
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            //this.SiteFooterSection.Controls.Add(LoadControl("SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            //this.MetaCopyrightSection.Controls.Add(LoadControl("MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void GoogleAnalyticsControl()
        {
            //this.GoogleAnalyticsSection.Controls.Add(LoadControl("GoogleAnalyticsControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void CommentDelete(Int64 _PostID, bool _IsMyCommentsPage, bool _IsOtherCommentsPage, string _subdomain)
        {
            if (!_IsMyCommentsPage && _IsOtherCommentsPage)
                return;

	string DeleteID = this.Request.QueryString["DeleteID"];
	if( DeleteID==null ) DeleteID = this.Request.Form["DeleteID"];

            Int64 _CommentID = -1;
            try
            {
                _CommentID = Convert.ToInt32(DeleteID);
            }
            catch { return; }

//this.resultText.Text = Convert.ToString(_CommentID);
//return;

            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringComments1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CommentDelete_CommentsPage_proc";

            command.Parameters.Add("@CommentID", SqlDbType.BigInt);
            command.Parameters["@CommentID"].Value = _CommentID;

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;

            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

	//this.NumCommentsLable.Text = Convert.ToString(Convert.ToInt32(this.NumCommentsLable.Text) - 1);
	//ShowPostComments(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
	this.Response.Redirect(String.Format("http://{0}.{1}/m/comments.aspx?PostID={2}", _subdomain, Constants.BlogDomain,_PostID), true);
            return;
        }
        //--------------------------------------------------------------------
        private void DoPost(Int64 _PostID, bool _IsMyCommentsPage, bool _IsOtherCommentsPage, string _subdomain)
        {
            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            byte _LanguageType = (byte)LanguageType.Persian;
            bool _PostAlign = Convert.ToBoolean(PostAlign.Right);

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringComments1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PostComment_CommentsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            if (_IsMyCommentsPage)
                command.Parameters["@BlogID"].Value = info.BlogID;
            if (_IsOtherCommentsPage)
                command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@CommenterBlogID", SqlDbType.BigInt);
            command.Parameters["@CommenterBlogID"].Value = info.BlogID;

            command.Parameters.Add("@PostDate", SqlDbType.DateTime);
            command.Parameters["@PostDate"].Value = DateTime.Now;

            command.Parameters.Add("@PostType", SqlDbType.Char);
            command.Parameters["@PostType"].Value = (byte)PostType.FromWeb;

            command.Parameters.Add("@PostLanguage", SqlDbType.Char);
            command.Parameters["@PostLanguage"].Value = _LanguageType;

            command.Parameters.Add("@PostAlign", SqlDbType.Bit);
            command.Parameters["@PostAlign"].Value = _PostAlign;

            command.Parameters.Add("@PostContent", SqlDbType.NVarChar);
            string ss = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);
            if (ss.Length > 450) ss = ss.Substring(0, 450);
            command.Parameters["@PostContent"].Value = ss;

            command.Parameters.Add("@HasPicture", SqlDbType.Bit);
            command.Parameters["@HasPicture"].Value = false;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;

            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

	//this.NumCommentsLable.Text = Convert.ToString(Convert.ToInt32(this.NumCommentsLable.Text) + 1);
	//ShowPostComments(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
	this.Response.Redirect(String.Format("http://{0}.{1}/m/comments.aspx?PostID={2}", _subdomain, Constants.BlogDomain,_PostID), true);
            return;
        }
        //--------------------------------------------------------------------
        private void ShowPostComments(Int64 _PostID, bool _IsMyCommentsPage, bool _IsOtherCommentsPage)
        {
            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];

			string page = this.Request.QueryString["page"];
			if( page==null ) page = this.Request.Form["page"];
		
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(page);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;
			
            if (currentPage > 1 && this.Session["_Num_Comments"] == null)
            {
				this.Response.Redirect(String.Format("http://www.{0}/m/comments.aspx?mode=ShowPostComments", Constants.BlogDomain), true);
                return;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringComments1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowComments_CommentsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            if (_IsMyCommentsPage)
                command.Parameters["@BlogID"].Value = info.BlogID;
            if (_IsOtherCommentsPage)
                command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = 8;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@CommentNum", SqlDbType.Int);
            command.Parameters["@CommentNum"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;

            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                //string _mainFormat = "<li><b>[name]:</b> [PostContent]<div><small>[LastTime] - از [from] - <delete><a href=\"[url]my.aspx?mode=[PostDeleteMode]&DeleteID=[id]\">حذف</a> - </delete><a href=\"[url]comments.aspx?PostID=[id]\">[NumComments] نظر</a></small></div></li><br>";
                string _mainFormat = "";
                //if (this.Cache["_mainFormat"] == null)
                //{
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\m\post.htm");
                    _mainFormat = sr.ReadToEnd();
                    //this.Cache["_mainFormat"] = _mainFormat;
                    sr.Close();
                //}
                //else
                    //_mainFormat = (string)this.Cache["_mainFormat"];

	_mainFormat = _mainFormat.Replace("[name]", "<a href=\"[url]m\">[name]</a>");
					
				Login.TagDelete(ref _mainFormat, "comments");
                if (_IsOtherCommentsPage)
                    Login.TagDelete(ref _mainFormat, "delete");
	else
	{
		_mainFormat = _mainFormat.Replace("<delete>", "");
		_mainFormat = _mainFormat.Replace("</delete>", "");
		_mainFormat = _mainFormat.Replace("[PostDeleteMode]", "CommentDelete");	
	}
				
			
				string resultText="";
                while (reader.Read())
                {
					string post = _mainFormat;
					
                    DateTime dtCurrent = (DateTime)reader["PostDate"];
                    Login.LastTimeFormat(ref post, dtCurrent);
					
					post = post.Replace("[name]", (string)reader["name"]);
					post = post.Replace("[urld]", "comments.aspx");
	post = post.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));

					
                    string _PostContent = (string)reader["PostContent"];
	Regex urlregex = new Regex(@"(https?:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
	_PostContent = urlregex.Replace(_PostContent, "<a href=\"$1\" target=\"_blank\">$1</a>");
                    _PostContent = _PostContent.Replace("\n", "<br>");
                    _PostContent = _PostContent.Replace("  ", " &nbsp;");
                    post = post.Replace("[PostContent]", _PostContent);

                    PostType _PostType = (PostType)Convert.ToInt32(reader["PostType"].ToString());
                    switch (_PostType)
                    {
                        case PostType.FromWeb:
                            post = post.Replace("[from]", "وب");
                            break;
                        case PostType.FromMobile:
                            post = post.Replace("[from]", "موبایل");
                            break;
                        case PostType.FromMessenger:
                            post = post.Replace("[from]", "مسنجر");
                            break;
                        default:
                            post = post.Replace("[from]", "وب");
                            break;
                    }					
					
              Int64 _id = (Int64)reader["id"];      
	post = post.Replace("[id]", _id.ToString()+"&PostID="+_PostID.ToString());
                    
					resultText += post;
                }

                reader.Close();
                reader.Dispose();
				
				int _CurrentCommentsNum;
                if (currentPage == 1)
				{
                    _CurrentCommentsNum = (int)command.Parameters["@CommentNum"].Value;
					this.Session["_Num_Comments"] = _CurrentCommentsNum;
				}
				else
					_CurrentCommentsNum = (int)this.Session["_Num_Comments"];
					
				string pageTemplate = "<div style=\"direction:ltr;font-size:small\">[paging]</div>";
				Login.DoPaging(ref pageTemplate, "ShowPostComments&PostID="+Convert.ToString(_PostID), currentPage, 8, _CurrentCommentsNum);
				resultText += pageTemplate;
				
                command.Dispose();
                connection.Close();

				this.resultText.Text = resultText;
                return;
            }
            else
            {
                this.resultText.Text = "نظري وجود ندارد";
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private bool CheckCorrectComment(Int64 _PostID, bool _IsMyCommentsPage, bool _IsOtherCommentsPage)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CheckCorrectComment_CommentsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            if (_IsMyCommentsPage)
                command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID ;
            if (_IsOtherCommentsPage)
                command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string _PostContent = (string)reader["PostContent"];
	Regex urlregex = new Regex(@"(https?:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
	_PostContent = urlregex.Replace(_PostContent, "<a href=\"$1\" target=\"_blank\">$1</a>");                _PostContent = _PostContent.Replace("\n", "<br>");
                _PostContent = _PostContent.Replace("  ", " &nbsp;");
                this.PostContent.Text = _PostContent;
                DateTime _PostDate = (DateTime)reader["PostDate"];
                this.PostDate.Text = String.Format("{2} ، ساعت {0}:{1}", _PostDate.Hour, _PostDate.Minute, Login.PersianDate(_PostDate));
                this.NumCommentsLable.Text = reader["NumComments"].ToString();
                reader.Close();
                command.Dispose();
                connection.Close();
                return true;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                return false;
            }
        }
        //--------------------------------------------------------------------
        private void PageSettings(bool _IsMyCommentsPage, bool _IsOtherCommentsPage, bool _IsLogin, Int64 _PostID)
        {
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();

			this.h.Text = "<input name=\"PostId\" type=\"hidden\" id=\"PostId\" value=\"";
			this.h.Text += _PostID.ToString();
			this.h.Text += "\"/>";

            if (_IsLogin)
            {
                SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                //this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                //this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);

                this.PostPanel.Visible = true;
                this.MustLoginToCommentPanel.Visible = false;
            }
            else
            {
                this.PostPanel.Visible = false;
                this.MustLoginToCommentPanel.Visible = true;
            }
			
            if (_IsMyCommentsPage)
            {
				SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
				this.title.InnerText = "نظرات پيغامك " + _info.Name;
				this.NameLabel.Text = _info.Name;
            }
            if (_IsOtherCommentsPage)
            {
				this.title.InnerText = "نظرات پيغامك " + (string)this.Session["GuestName"];
				this.NameLabel.Text = (string)this.Session["GuestName"];
            }
        }
        //--------------------------------------------------------------------
        /// <summary>
        /// Check whether this page has been called by the logined user and this page is his/her personal friends page. If 
        /// the user already has not logined into system, this method invoke the login procedure to probably login the user by 
        /// his/her browser cookie.
        /// </summary>
        /// <returns></returns>
        private bool IsMyCommentsPage(string _subdomain, bool _IsLogin)
        {
            if (_IsLogin)
            {
                if (((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username == _subdomain)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        //--------------------------------------------------------------------
        private bool IsOtherCommentsPage(string _subdomain)
        {
            if (this.Session["GuestBlogUsername"] == null || (string)this.Session["GuestBlogUsername"] != _subdomain)
            {
                SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = String.Format("SELECT [id],name,ImageGuid,(SELECT ThemeString FROM themes WHERE themes.[id]=accounts.ThemeID) AS ThemeString FROM {0} WHERE username='{1}'", Constants.AccountsTableName, _subdomain);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.Session["GuestBlogUsername"] = _subdomain;
                    this.Session["GuestBlogID"] = (Int64)reader["id"];
                    this.Session["GuestName"] = (string)reader["name"];
                    this.Session["GuestImageGuid"] = (string)reader["ImageGuid"];
                    this.Session["GuestThemeString"] = (string)reader["ThemeString"];
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        //--------------------------------------------------------------------
    }
}
