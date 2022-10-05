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
    public partial class my : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Login.IsLoginProc(this);
            if (!_IsLogin)// the user has not already logined into sysem.
            {
                this.Response.Redirect(String.Format("http://www.{0}/m/login.aspx", Constants.BlogDomain), true);
                return;
            }

            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            string _subdomain = Login.FindSubdomain(this);                
            if (String.Compare(_subdomain, info.Username) != 0)
            {
                this.Response.Redirect(String.Format("http://www.{0}/m/login.aspx", Constants.BlogDomain), true);
                return;
            }

            PageSettings(info);
			
	string mode = this.Request.QueryString["mode"];
	if( mode==null ) mode = this.Request.Form["mode"];


            if( mode == null )
            {
                ShowMyPosts(info);
                return;
            }
            else
            {
                switch( mode )
                {
                    case "ShowMyPosts":
                        ShowMyPosts(info);
                        return;
                    case "ShowMyFriendsPosts":
                        ShowMyFriendsPosts(info);
                        return;
                    case "ShowPrivateMessages":
                        ShowPrivateMessages(info);
                        return;
                    case "post":
                        DoPost(info);
                        return;
                    case "MyPostDelete":
                        MyPostDelete(info);
                        return;
                    case "PrivateMesseageDelete":
                        PrivateMesseageDelete(info);
                        return;
                    default:
                        return;
                }
            }            
        }
        //--------------------------------------------------------------------
        private void PageSettings(SigninSessionInfo info)
        {
            this.MyNameLabel.Text = info.Name;
            this.title.InnerText = info.Name + " | پيغامك";
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
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
        private void DoPost(SigninSessionInfo info)
        {
            byte _LanguageType = (byte)LanguageType.Persian;
            bool _PostAlign = Convert.ToBoolean(PostAlign.Right);
			
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DoPost_MyPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter PostDateParam = new SqlParameter("@PostDate", SqlDbType.DateTime);
            SqlParameter PostTypeParam = new SqlParameter("@PostType", SqlDbType.Char);
            SqlParameter PostLanguageParam = new SqlParameter("@PostLanguage", SqlDbType.Char);
            SqlParameter PostAlignParam = new SqlParameter("@PostAlign", SqlDbType.Bit);
            SqlParameter PostContentParam = new SqlParameter("@PostContent", SqlDbType.NVarChar);
            SqlParameter CommentEnabledParam = new SqlParameter("@CommentEnabled", SqlDbType.Bit);
            SqlParameter HasPictureParam = new SqlParameter("@HasPicture", SqlDbType.Bit);
            //------Linked Server settings---------------
            SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = Constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);
            //-------------------------------------------

            BlogIDParam.Value = info.BlogID;
            PostDateParam.Value = DateTime.Now;
            PostTypeParam.Value = (byte)PostType.FromWeb;
            PostLanguageParam.Value = _LanguageType;
            PostAlignParam.Value = _PostAlign;
            string ss = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);
            if (ss.Length > 450) ss = ss.Substring(0, 450);
            PostContentParam.Value = ss;
            CommentEnabledParam.Value = true;
            HasPictureParam.Value = false;

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(PostDateParam);
            command.Parameters.Add(PostTypeParam);
            command.Parameters.Add(PostLanguageParam);
            command.Parameters.Add(PostAlignParam);
            command.Parameters.Add(PostContentParam);
            command.Parameters.Add(CommentEnabledParam);
            command.Parameters.Add(HasPictureParam);

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
			
	ShowMyPosts(info);
        }
        //--------------------------------------------------------------------
        private void MyPostDelete(SigninSessionInfo info)
        {
	string DeleteID = this.Request.QueryString["DeleteID"];
	if( DeleteID==null ) DeleteID = this.Request.Form["DeleteID"];
		
            Int64 _PostID = -1;
            try
            {
                _PostID = Convert.ToInt64(DeleteID);
            }
            catch { return; }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PostDelete_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsStarredPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsStarredPosts1DbLinkedServer"].Value = Constants.IsStarredPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            ShowMyPosts(info);
        }
        //--------------------------------------------------------------------
        private void PrivateMesseageDelete(SigninSessionInfo info)
        {
	string DeleteID = this.Request.QueryString["DeleteID"];
	if( DeleteID==null ) DeleteID = this.Request.Form["DeleteID"];
		
            Int64 _MessageID = -1;
            try
            {
                _MessageID = Convert.ToInt64(DeleteID);
            }
            catch { return; }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPrivateMessages1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PrivateMessageDelete_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@MessageID", SqlDbType.BigInt);
            command.Parameters["@MessageID"].Value = _MessageID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
			
            ShowPrivateMessages(info);
        }
        //--------------------------------------------------------------------
        private void ShowMyPosts(SigninSessionInfo info)
        {
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

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowMyPosts_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsStarredPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsStarredPosts1DbLinkedServer"].Value = Constants.IsStarredPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@MyBlogID", SqlDbType.BigInt);
            command.Parameters["@MyBlogID"].Value = info.BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = 8;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PostNum", SqlDbType.Int);
            command.Parameters["@PostNum"].Direction = ParameterDirection.Output;

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

                //Login.TagDelete(ref _mainFormat, "delete");
                _mainFormat = _mainFormat.Replace("<delete>", "");
                _mainFormat = _mainFormat.Replace("</delete>", "");

                _mainFormat = _mainFormat.Replace("<comments>", "");
                _mainFormat = _mainFormat.Replace("</comments>", "");

                _mainFormat = _mainFormat.Replace("[PostDeleteMode]", "MyPostDelete");
                _mainFormat = _mainFormat.Replace("[urld]", "my.aspx");
                _mainFormat = _mainFormat.Replace("[urlc]", "comments.aspx");
                _mainFormat = _mainFormat.Replace("[name]", info.Name);

                string resultText="";
                while (reader.Read())
                {
                    string post = _mainFormat;

                    DateTime dtCurrent = (DateTime)reader["PostDate"];
                    Login.LastTimeFormat(ref post, dtCurrent);

                    string _PostContent = (string)reader["PostContent"];
                    _PostContent = _PostContent.Replace("<", "&lt;");
                    _PostContent = _PostContent.Replace(">", "&gt;");
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
                    post = post.Replace("[id]", _id.ToString());

                    int _NumComments = (int)reader["NumComments"];
                    post = post.Replace("[NumComments]", _NumComments.ToString());

                    resultText += post;
                }

                reader.Close();
                reader.Dispose();

	 int _CurrentPostNum = (int)command.Parameters["@PostNum"].Value;
	 string pageTemplate = "<div style=\"direction:ltr;font-size:small\">[paging]</div>";
	 Login.DoPaging(ref pageTemplate, "ShowMyPosts", currentPage, 8, _CurrentPostNum);
	 resultText += pageTemplate;
	
                this.resultText.Text = resultText;
                return;
            }
            else
            {
                this.resultText.Text = "پيغامكي وجود ندارد";
                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowMyFriendsPosts(SigninSessionInfo info)
        {
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
				
            if (currentPage == 1 || this.Session["ItemNum"] == null)
            {
                int _PostNum = ComputeFriendsPostNum(info.BlogID);
                if (_PostNum == 0)
                {
                    this.resultText.Text = "پيغامكي وجود ندارد";
                    return;
                }
                this.Session["ItemNum"] = _PostNum;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowMyFriendsPost_MyPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = 8;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsFriends1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsFriends1DbLinkedServer"].Value = Constants.IsFriends1DbLinkedServer;

            command.Parameters.Add("@IsStarredPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsStarredPosts1DbLinkedServer"].Value = Constants.IsStarredPosts1DbLinkedServer;
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

                Login.TagDelete(ref _mainFormat, "delete");
                //_mainFormat = _mainFormat.Replace("<delete>", "");
                //_mainFormat = _mainFormat.Replace("</delete>", "");
	//_mainFormat = _mainFormat.Replace("[PostDeleteMode]", "MyPostDelete");

                _mainFormat = _mainFormat.Replace("<comments>", "");
                _mainFormat = _mainFormat.Replace("</comments>", "");

                string resultText="";					
                while (reader.Read())
                {
	string post = _mainFormat;
					
                    post = post.Replace("[name]", (string)reader["name"]);
                    post = post.Replace("[urlc]", Login.BuildUserUrl((string)reader["username"])  + "m/comments.aspx" );
	      post = post.Replace("[url]", Login.BuildUserUrl((string)reader["username"]) );
					
					////////////////////////////////////////////////////////////
                    DateTime dtCurrent = (DateTime)reader["PostDate"];
                    Login.LastTimeFormat(ref post, dtCurrent);

                    string _PostContent = (string)reader["PostContent"];
                    _PostContent = _PostContent.Replace("<", "&lt;");
                    _PostContent = _PostContent.Replace(">", "&gt;");
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
                    post = post.Replace("[id]", _id.ToString());

                    int _NumComments = (int)reader["NumComments"];
                    post = post.Replace("[NumComments]", _NumComments.ToString());

                    resultText += post;					
                }

                reader.Close();
                reader.Dispose();

	//int _CurrentPostNum = (int)command.Parameters["@PostNum"].Value;
	int _CurrentPostNum = (int)this.Session["ItemNum"];
	string pageTemplate = "<div style=\"direction:ltr;font-size:small\">[paging]</div>";
	Login.DoPaging(ref pageTemplate, "ShowMyFriendsPosts", currentPage, 8, _CurrentPostNum);
	resultText += pageTemplate;

	this.resultText.Text = resultText;
            }
            else
            {
                this.resultText.Text = "پيغامكي وجود ندارد";
                return;
            }

        }
        //--------------------------------------------------------------------
        private void ShowPrivateMessages(SigninSessionInfo info)
        {
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

            if (currentPage > 1 && this.Session["ItemNumPrivate"] == null)
            {
  	 this.Response.Redirect(String.Format("http://www.{0}/m/my.aspx?mode=ShowPrivateMessages", Constants.BlogDomain), true);
                return;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPrivateMessages1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowPrivateMessages_MyPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = 8;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PrivateMessagesNum", SqlDbType.Int);
            command.Parameters["@PrivateMessagesNum"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
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
                //Login.TagDelete(ref _mainFormat, "delete");
                _mainFormat = _mainFormat.Replace("<delete>", "");
                _mainFormat = _mainFormat.Replace("</delete>", "");
                _mainFormat = _mainFormat.Replace("[PostDeleteMode]", "PrivateMesseageDelete");

                _mainFormat = _mainFormat.Replace("[urld]", "my.aspx");
				
                string resultText="";
                while (reader.Read())
                {
                    string post = _mainFormat;

                    DateTime dtCurrent = (DateTime)reader["PostDate"];
                    Login.LastTimeFormat(ref post, dtCurrent);
					
                    post = post.Replace("[name]", (string)reader["name"]);
                    post = post.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));

                    string _PostContent = (string)reader["PostContent"];
                    _PostContent = _PostContent.Replace("<", "&lt;");
                    _PostContent = _PostContent.Replace(">", "&gt;");
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
                    post = post.Replace("[id]", _id.ToString());

                    resultText += post;
                }

                reader.Close();
                reader.Dispose();

	int _CurrentPostNum;
              if (currentPage == 1)
	{
                   _CurrentPostNum = (int)command.Parameters["@PrivateMessagesNum"].Value;
     	    this.Session["ItemNumPrivate"] = _CurrentPostNum;
	}
	else
	    _CurrentPostNum = (int)this.Session["ItemNumPrivate"];
	string pageTemplate = "<div style=\"direction:ltr;font-size:small\">[paging]</div>";
	Login.DoPaging(ref pageTemplate, "ShowPrivateMessages", currentPage, 8, _CurrentPostNum);
	resultText += pageTemplate;
	
                this.resultText.Text = resultText;
                return;
            }
            else
            {
                this.resultText.Text = "پيغامك خصوصي وجود ندارد";
                return;
            }

        }
        //--------------------------------------------------------------------
        private int ComputeFriendsPostNum(Int64 _BlogID)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ComputeFriendsPostNum_MyPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.Parameters.Add("@PostNum", SqlDbType.Int);
            command.Parameters["@PostNum"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsFriends1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsFriends1DbLinkedServer"].Value = Constants.IsFriends1DbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();

            if (command.Parameters["@PostNum"].Value == DBNull.Value)
            {
                command.Dispose();
                connection.Close();
                return 0;
            }
            else
            {
                int _PostNum = (int)command.Parameters["@PostNum"].Value;
                command.Dispose();
                connection.Close();
                return _PostNum;
            }
        }
        //--------------------------------------------------------------------	
    }
}