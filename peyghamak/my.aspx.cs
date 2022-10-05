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

namespace Peyghamak
{
    public partial class my : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Login.IsLoginProc(this);
            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            if (_IsLogin)
            {
                if (this.Request.Form["mode"] != null && this.Request.Form["mode"] == "staring")
                    goto StringContinue;
                string _subdomain = Login.FindSubdomain(this);
                if (String.Compare(_subdomain, info.Username) != 0)
                {
                    this.Response.Redirect(String.Format("http://{0}.{1}/Default.aspx", _subdomain, Constants.BlogDomain), true);
                    return;
                }
            }
    StringContinue :
            if (this.Request.Form["mode"] != null && _IsLogin)
            {
                switch (this.Request.Form["mode"])
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
                    case "ShowStarredPosts":
                        ShowStarredPosts(info);
                        return;
                    case "ShowAllComments":
                        ShowAllComments(info);
                        return;
                    case "ShowAllStarredComments":
                        ShowAllStarredComments(info);
                        return;
                    case "staring":
                        DoStar();
                        return;
                    case "MyPostDelete":
                        MyPostDelete(info);
                        return;
                    case "PrivateMesseageDelete":
                        PrivateMesseageDelete(info);
                        return;
                    case "StarredPostDeleteFromMyPage":
                        StarredPostDelete(info);
                        return;
                    case "StarredPostDeleteFromGuestPage":
                        StarredPostDelete(info);
                        return;
                    case "post":
                        DoPost(info);
                        return;
                    default:
                        return;
                }
            }
            if (this.Request.Form["mode"] != null && !_IsLogin)
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }
            if (!_IsLogin)// the user has not already logined into sysem.
            {
                this.Response.Redirect(Constants.LoginPageUrl, true);
                return;
            }
            //--------
            PageSettings(info);
            //--------
        }
        //--------------------------------------------------------------------
        private void PageSettings(SigninSessionInfo info)
        {
            this.MyImage.ImageUrl = Login.Build75x75ImageUrl(info.ImageGuid, false);
            this.MyImage.ToolTip = info.Name;
            this.MyNameLabel.Text = info.Name;
            ListTopFriendsAndWeblogStatProc(info);
            this.title.InnerText = info.Name;
            this.feed.Attributes.Add("title", String.Format("{0} (RSS Feed)", info.Name));
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
            //ShowLatestPrivateMessages(info);
            //ShowLatestStarredComments(info);
        }
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            this.SiteFooterSection.Controls.Add(LoadControl("SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            this.MetaCopyrightSection.Controls.Add(LoadControl("MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void GoogleAnalyticsControl()
        {
            this.GoogleAnalyticsSection.Controls.Add(LoadControl("GoogleAnalyticsControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void ShowAllComments(SigninSessionInfo info)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumAllComments"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringComments1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowAllComments_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = Constants.MaxAllCommentsShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@CommentNum", SqlDbType.Int);
            command.Parameters["@CommentNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_AllComments"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                    template = sr.ReadToEnd();
                    Login.TagDelete(ref template, "ForLatestPrivateMessages");
                    this.Cache["_template_AllComments"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_AllComments"];

                /*StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                template = sr.ReadToEnd();
                sr.Close();*/

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
                string _CommentContent = null;
                string _PostContent = null;
                while (reader.Read())
                {
                    temp = _mainFormat;

                    if ((bool)reader["CommentAlign"]) //right
                    {
                        temp = temp.Replace("[CommentAlign]", "right");
                        temp = temp.Replace("[comment-dir]", "rtl");
                    }
                    else
                    {
                        temp = temp.Replace("[CommentAlign]", "left");
                        temp = temp.Replace("[comment-dir]", "ltr");
                    }

                    if ((bool)reader["PostAlign"]) //right
                    {
                        temp = temp.Replace("[PostAlign]", "right");
                        temp = temp.Replace("[post-dir]", "rtl");
                    }
                    else
                    {
                        temp = temp.Replace("[PostAlign]", "left");
                        temp = temp.Replace("[post-dir]", "ltr");
                    }

                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl(reader["ImageGuid"].ToString(), false));
                    temp = temp.Replace("[name]", reader["name"].ToString());
                    temp = temp.Replace("[url]", Login.BuildUserUrl(info.Username));
                    temp = temp.Replace("[id]", reader["PostID"].ToString());
                    _CommentContent = reader["CommentContent"].ToString();
                    _PostContent = reader["PostContent"].ToString();
                    temp = temp.Replace("[CompleteCommentContent]", _CommentContent);
                    //temp = temp.Replace("[CompletePostContent]", _PostContent);
                    if (_CommentContent.Length > Constants.MaxStringLengthForLatestComments)
                        _CommentContent = String.Format("{0}...", _CommentContent.Substring(0, Constants.MaxStringLengthForLatestComments));
                    if (_PostContent.Length > Constants.MaxStringLengthForLatestComments)
                        _PostContent = String.Format("{0}...", _PostContent.Substring(0, Constants.MaxStringLengthForLatestComments));
                    temp = temp.Replace("[CommentContent]", _CommentContent);
                    temp = temp.Replace("[PostContent]", _PostContent);

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                if (currentPage == 1)
                    this.Session["_ItemNumAllComments"] = (int)command.Parameters["@CommentNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Login.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowAllComments", currentPage, Constants.MaxAllCommentsShow, (int)this.Session["_ItemNumAllComments"], Constants.PagingNumberAllComments, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowAllStarredComments(SigninSessionInfo info)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumAllStarredComments"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringComments1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowAllStarredComments_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;

            command.Parameters.Add("@IsStarredPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsStarredPosts1DbLinkedServer"].Value = Constants.IsStarredPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = Constants.MaxAllCommentsShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@CommentNum", SqlDbType.Int);
            command.Parameters["@CommentNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_AllComments"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                    template = sr.ReadToEnd();
                    Login.TagDelete(ref template, "ForLatestPrivateMessages");
                    this.Cache["_template_AllComments"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_AllComments"];

                /*StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                template = sr.ReadToEnd();
                sr.Close();*/

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
                string _CommentContent = null;
                string _PostContent = null;
                while (reader.Read())
                {
                    temp = _mainFormat;

                    if ((bool)reader["CommentAlign"]) //right
                    {
                        temp = temp.Replace("[CommentAlign]", "right");
                        temp = temp.Replace("[comment-dir]", "rtl");
                    }
                    else
                    {
                        temp = temp.Replace("[CommentAlign]", "left");
                        temp = temp.Replace("[comment-dir]", "ltr");
                    }

                    if ((bool)reader["PostAlign"]) //right
                    {
                        temp = temp.Replace("[PostAlign]", "right");
                        temp = temp.Replace("[post-dir]", "rtl");
                    }
                    else
                    {
                        temp = temp.Replace("[PostAlign]", "left");
                        temp = temp.Replace("[post-dir]", "ltr");
                    }

                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl(reader["ImageGuid"].ToString(), false));
                    temp = temp.Replace("[name]", reader["name"].ToString());
                    temp = temp.Replace("[url]", Login.BuildUserUrl(reader["StarredUsername"].ToString()));
                    temp = temp.Replace("[id]", reader["PostID"].ToString());
                    _CommentContent = reader["CommentContent"].ToString();
                    _PostContent = reader["PostContent"].ToString();
                    temp = temp.Replace("[CompleteCommentContent]", _CommentContent);
                    //temp = temp.Replace("[CompletePostContent]", _PostContent);
                    if (_CommentContent.Length > Constants.MaxStringLengthForLatestComments)
                        _CommentContent = String.Format("{0}...", _CommentContent.Substring(0, Constants.MaxStringLengthForLatestComments));
                    if (_PostContent.Length > Constants.MaxStringLengthForLatestComments)
                        _PostContent = String.Format("{0}...", _PostContent.Substring(0, Constants.MaxStringLengthForLatestComments));
                    temp = temp.Replace("[CommentContent]", _CommentContent);
                    temp = temp.Replace("[PostContent]", _PostContent);

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                if (currentPage == 1)
                    this.Session["_ItemNumAllStarredComments"] = (int)command.Parameters["@CommentNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Login.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowAllStarredComments", currentPage, Constants.MaxAllCommentsShow, (int)this.Session["_ItemNumAllStarredComments"], Constants.PagingNumberAllComments, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        /*private void ShowLatestStarredComments(SigninSessionInfo info)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringComments1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowStarredLatestComments_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;

            command.Parameters.Add("@IsStarredPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsStarredPosts1DbLinkedServer"].Value = Constants.IsStarredPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@TopLatestComments", SqlDbType.Int);
            command.Parameters["@TopLatestComments"].Value = Constants.TopLatestStarredCommentsShow;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_StarredLatestComments"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                    template = sr.ReadToEnd();
                    Login.TagDelete(ref template, "paging");
                    Login.TagDelete(ref template, "ForLatestPrivateMessages");
                    this.Cache["_template_StarredLatestComments"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_StarredLatestComments"];

                string temp = null;
                string _CommentContent = null;
                string _PostContent = null;
                this.LatestStarredComments.Text = "";
                while (reader.Read())
                {
                    temp = template;

                    if ((bool)reader["CommentAlign"]) //right
                    {
                        temp = temp.Replace("[CommentAlign]", "right");
                        temp = temp.Replace("[comment-dir]", "rtl");
                    }
                    else
                    {
                        temp = temp.Replace("[CommentAlign]", "left");
                        temp = temp.Replace("[comment-dir]", "ltr");
                    }

                    if ((bool)reader["PostAlign"]) //right
                    {
                        temp = temp.Replace("[PostAlign]", "right");
                        temp = temp.Replace("[post-dir]", "rtl");
                    }
                    else
                    {
                        temp = temp.Replace("[PostAlign]", "left");
                        temp = temp.Replace("[post-dir]", "ltr");
                    }

                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl(reader["ImageGuid"].ToString(), false));
                    temp = temp.Replace("[name]", reader["name"].ToString());
                    temp = temp.Replace("[url]", Login.BuildUserUrl(reader["StarredUsername"].ToString()));
                    temp = temp.Replace("[id]", reader["PostID"].ToString());
                    _CommentContent = reader["CommentContent"].ToString();
                    _PostContent = reader["PostContent"].ToString();
                    temp = temp.Replace("[CompleteCommentContent]", _CommentContent);
                    //temp = temp.Replace("[CompletePostContent]", _PostContent);
                    if (_CommentContent.Length > Constants.MaxStringLengthForLatestComments)
                        _CommentContent = String.Format("{0}...", _CommentContent.Substring(0, Constants.MaxStringLengthForLatestComments));
                    if (_PostContent.Length > Constants.MaxStringLengthForLatestComments)
                        _PostContent = String.Format("{0}...", _PostContent.Substring(0, Constants.MaxStringLengthForLatestComments));
                    temp = temp.Replace("[CommentContent]", _CommentContent);
                    temp = temp.Replace("[PostContent]", _PostContent);
                    this.LatestStarredComments.Text += temp;
                }
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return;
        }*/
        //--------------------------------------------------------------------
        /*private void ShowLatestPrivateMessages(SigninSessionInfo info)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPrivateMessages1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowLatestPrivateMessages_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@TopLatestPrivateMessages", SqlDbType.Int);
            command.Parameters["@TopLatestPrivateMessages"].Value = Constants.TopLatestPrivateMessagesShow;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_LatestPrivateMessages"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                    template = sr.ReadToEnd();
                    Login.TagDelete(ref template, "paging");
                    Login.TagDelete(ref template, "CommentContent");
                    Login.TagDelete(ref template, "ForCommenting");
                    template = template.Replace("[CompleteCommentContent]", "");
                    this.Cache["_template_LatestPrivateMessages"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_LatestPrivateMessages"];

                string temp = null;
                string _PostContent = null;
                this.LatestPrivateMesages.Text = "";
                while (reader.Read())
                {
                    temp = template;

                    if ((bool)reader["PostAlign"]) //right
                    {
                        temp = temp.Replace("[PostAlign]", "right");
                        temp = temp.Replace("[post-dir]", "rtl");
                    }
                    else
                    {
                        temp = temp.Replace("[PostAlign]", "left");
                        temp = temp.Replace("[post-dir]", "ltr");
                    }

                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl(reader["ImageGuid"].ToString(), false));
                    temp = temp.Replace("[name]", reader["name"].ToString());
                    temp = temp.Replace("[url]", Login.BuildUserUrl(reader["username"].ToString()));
                    temp = temp.Replace("[id]", reader["id"].ToString());
                    _PostContent = reader["PostContent"].ToString();
                    if (_PostContent.Length > Constants.MaxStringLengthForLatestPrivateMessages)
                        _PostContent = String.Format("{0}...", _PostContent.Substring(0, Constants.MaxStringLengthForLatestPrivateMessages));
                    temp = temp.Replace("[PostContent]", _PostContent);

                    this.LatestPrivateMesages.Text += temp;
                }
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return;
        }*/
        //--------------------------------------------------------------------
        private void DoPost(SigninSessionInfo info)
        {
            byte _LanguageType = (byte)LanguageType.Persian;
            bool _PostAlign = Convert.ToBoolean(PostAlign.Right);
            if (this.Request.Form["LanguageType"] != null && this.Request.Form["LanguageType"] != "")
            {
                try { _LanguageType = Convert.ToByte(this.Request.Form["LanguageType"]); }
                catch { }
                if (!Enum.IsDefined(typeof(LanguageType), (LanguageType)_LanguageType))
                    _LanguageType = (byte)LanguageType.Persian;
            }

            if (this.Request.Form["PostAlign"] != null && this.Request.Form["PostAlign"] != "")
            {
                try { _PostAlign = Convert.ToBoolean(Convert.ToInt32(this.Request.Form["PostAlign"])); }
                catch { }
                if (!Enum.IsDefined(typeof(PostAlign), (PostAlign)Convert.ToInt32(_PostAlign)))
                    _PostAlign = Convert.ToBoolean(PostAlign.Right);
            }
            //throw new Exception(_PostAlign.ToString());
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
            PostContentParam.Value = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);
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

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void MyPostDelete(SigninSessionInfo info)
        {
            Int64 _PostID = -1;
            try
            {
                _PostID = Convert.ToInt64(this.Request.Form["DeleteID"]);
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

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void StarredPostDelete(SigninSessionInfo info)
        {
            Int64 _StarredID = -1;
            try
            {
                _StarredID = Convert.ToInt64(this.Request.Form["DeleteID"]);
            }
            catch { return; }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringStarredPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "StarredPostDelete_MyPage_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@StarredID", SqlDbType.BigInt);
            command.Parameters["@StarredID"].Value = _StarredID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void DoStar()
        {
            Int64 _PostID = -1;
            try
            {
                _PostID = Convert.ToInt64(this.Request.Form["PostID"]);
            }
            catch { return; }
            if (_PostID <= 0)
                return;

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringStarredPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "StaringPost_MyAndGuestPages_proc";

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void PrivateMesseageDelete(SigninSessionInfo info)
        {
            Int64 _MessageID = -1;
            try
            {
                _MessageID = Convert.ToInt64(this.Request.Form["DeleteID"]);
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

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void ShowMyPosts(SigninSessionInfo info)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
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
            command.Parameters["@PageSize"].Value = Constants.MaxPostShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PostNum", SqlDbType.Int);
            command.Parameters["@PostNum"].Direction = ParameterDirection.Output;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_MyPostShow"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\MyPostShow.html");
                    template = sr.ReadToEnd();
                    Login.TagDelete(ref template, "star");
                    Login.TagDelete(ref template, "StarsDelete");
                    this.Cache["_template_MyPostShow"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_MyPostShow"];

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
                Login.TagDelete(ref _mainFormat, "StarredPages");
                _mainFormat = _mainFormat.Replace("[StarredPostDeleteMode]", "StarredPostDeleteFromGuestPage");
                _mainFormat = _mainFormat.Replace("[PostDeleteMode]", "MyPostDelete");
                _mainFormat = _mainFormat.Replace("[url]", "");
                _mainFormat = _mainFormat.Replace("[name]", info.Name);
                _mainFormat = _mainFormat.Replace("[image]", Login.Build40x40ImageUrl(info.ImageGuid, false));
                string temp = "";
                DateTime dtTemp = DateTime.Now;
                DateTime dtCurrent;
                bool firstLoop = true;
                while (reader.Read())
                {
                    dtCurrent = (DateTime)reader["PostDate"];
                    //------
                    temp = _mainFormat;
                    if (dtCurrent.Year == dtTemp.Year && dtCurrent.Month == dtTemp.Month && dtCurrent.Day == dtTemp.Day && !firstLoop && reader.RecordsAffected != 1)
                        Login.PostDateFormat(ref temp, dtCurrent, true);
                    else
                    {
                        Login.PostDateFormat(ref temp, dtCurrent, false);
                        dtTemp = dtCurrent;
                        firstLoop = false;
                    }
                    //------
                    /*if ((int)reader["Starred"] == 1)
                        Login.TagDelete(ref temp, "unstarred");
                    else
                        Login.TagDelete(ref temp, "starred");*/
                    //------
                    Login.PostContentFormat(ref temp, (string)reader["PostContent"], (bool)reader["PostAlign"], (PostType)Convert.ToInt32(reader["PostType"].ToString()), (Int64)reader["id"]);
                    //------
                    Login.CommentsFormat(ref temp, (int)reader["NumComments"], (bool)reader["CommentEnabled"]);
                    //TagDelete(ref temp, "comments");
                    //------
                    Login.LastTimeFormat(ref temp, dtCurrent);
                    //------
                    //------
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                int _CurrentPostNum = (int)command.Parameters["@PostNum"].Value;
                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, _CurrentPostNum);
                    Login.AjaxDoPagingEx(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowMyPosts", currentPage, Constants.MaxPostShow, _CurrentPostNum, Constants.PagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowMyFriendsPosts(SigninSessionInfo info)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 1 || this.Session["ItemNum"] == null)
            {
                int _PostNum = ComputeFriendsPostNum(info.BlogID);
                if (_PostNum == 0)
                {
                    WriteStringToAjaxRequest("NoFoundPost");
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
            command.Parameters["@PageSize"].Value = Constants.MaxPostShow;

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
                string template = "";
                if (this.Cache["_template_MyFriendsPostShow"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\MyPostShow.html");
                    template = sr.ReadToEnd();
                    //template = template.Replace("[starredDeleteMode]", "starred");
                    template = template.Replace("[StarredPostDeleteMode]", "StarredPostDeleteFromGuestPage");
                    template = template.Replace("[PostDeleteMode]", "");
                    Login.TagDelete(ref template, "StarsDelete");
                    this.Cache["_template_MyFriendsPostShow"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_MyFriendsPostShow"];

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
                Login.TagDelete(ref _mainFormat, "delete");
                Login.TagDelete(ref _mainFormat, "StarredPages");
                //_mainFormat = _mainFormat.Replace("[image]", Login.Build40x40ImageUrl(info.ImageGuid, false));
                string temp = "";
                DateTime dtTemp = DateTime.Now;
                DateTime dtCurrent;
                bool firstLoop = true;
                while (reader.Read())
                {
                    dtCurrent = (DateTime)reader["PostDate"];
                    //------
                    temp = _mainFormat;
                    if (dtCurrent.Year == dtTemp.Year && dtCurrent.Month == dtTemp.Month && dtCurrent.Day == dtTemp.Day && !firstLoop && reader.RecordsAffected != 1)
                        Login.PostDateFormat(ref temp, dtCurrent, true);
                    else
                    {
                        Login.PostDateFormat(ref temp, dtCurrent, false);
                        dtTemp = dtCurrent;
                        firstLoop = false;
                    }
                    //------
                    if ((int)reader["Starred"] == 1)
                    {
                        Login.TagDelete(ref temp, "unstarred");
                        temp = temp.Replace("[StarredID]", reader["StarredID"].ToString());
                    }
                    else
                    {
                        Login.TagDelete(ref temp, "starred");
                        //Login.TagDelete(ref template, "StarsDelete");
                    }
                    //------
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    //------
                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl((string)reader["ImageGuid"], false));
                    //------
                    temp = temp.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
                    //------
                    Login.PostContentFormat(ref temp, (string)reader["PostContent"], (bool)reader["PostAlign"], (PostType)Convert.ToInt32(reader["PostType"].ToString()), (Int64)reader["id"]);
                    //------
                    Login.CommentsFormat(ref temp, (int)reader["NumComments"], (bool)reader["CommentEnabled"]);
                    //TagDelete(ref temp, "comments");
                    //------
                    Login.LastTimeFormat(ref temp, dtCurrent);
                    //------
                    //------
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                int _CurrentPostNum = (int)this.Session["ItemNum"];
                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    Login.AjaxDoPagingEx(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowMyFriendsPosts", currentPage, Constants.MaxPostShow, _CurrentPostNum, Constants.PagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowStarredPosts(SigninSessionInfo info)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["ItemNum"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringStarredPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowStarredPosts_MyPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = Constants.MaxPostShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@StarredPostNum", SqlDbType.Int);
            command.Parameters["@StarredPostNum"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;

            command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_StarredPostsShow"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\MyPostShow.html");
                    template = sr.ReadToEnd();
                    Login.TagDelete(ref template, "delete");
                    Login.TagDelete(ref template, "star");
                    Login.TagDelete(ref template, "NormalPages");
                    this.Cache["_template_StarredPostsShow"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_StarredPostsShow"];

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
                //Login.TagDelete(ref _mainFormat, "comments");
                _mainFormat = _mainFormat.Replace("[StarredPostDeleteMode]", "StarredPostDeleteFromMyPage");
                _mainFormat = _mainFormat.Replace("[PostDeleteMode]", "");
                string temp = "";
                DateTime dtTemp = DateTime.Now;
                DateTime dtCurrent;
                bool firstLoop = true;
                while (reader.Read())
                {
                    dtCurrent = (DateTime)reader["PostDate"];
                    //------
                    temp = _mainFormat;
                    if (dtCurrent.Year == dtTemp.Year && dtCurrent.Month == dtTemp.Month && dtCurrent.Day == dtTemp.Day && !firstLoop && reader.RecordsAffected != 1)
                        Login.PostDateFormat(ref temp, dtCurrent, true);
                    else
                    {
                        Login.PostDateFormat(ref temp, dtCurrent, false);
                        dtTemp = dtCurrent;
                        firstLoop = false;
                    }
                    //------
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    //------
                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl((string)reader["ImageGuid"], false));
                    //------
                    temp = temp.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
                    //------
                    Login.PostContentFormat(ref temp, (string)reader["PostContent"], (bool)reader["PostAlign"], (PostType)Convert.ToInt32(reader["PostType"].ToString()), (Int64)reader["id"]);
                    //------
                    temp = temp.Replace("[PostID]", reader["PostID"].ToString());
                    Login.CommentsFormat(ref temp, (int)reader["NumComments"], (bool)reader["CommentEnabled"]);
                    //TagDelete(ref temp, "comments");
                    //------
                    Login.LastTimeFormat(ref temp, dtCurrent);
                    //------
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                if (currentPage == 1)
                    this.Session["ItemNum"] = (int)command.Parameters["@StarredPostNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    Login.AjaxDoPagingEx(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowStarredPosts", currentPage, Constants.MaxPostShow, (int)this.Session["ItemNum"], Constants.PagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowPrivateMessages(SigninSessionInfo info)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["ItemNum"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
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
            command.Parameters["@PageSize"].Value = Constants.MaxPostShow;

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
                string template = "";
                if (this.Cache["_template_MyPostShow"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\MyPostShow.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_MyPostShow"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_MyPostShow"];

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
                Login.TagDelete(ref _mainFormat, "comments");
                Login.TagDelete(ref _mainFormat, "star");
                _mainFormat = _mainFormat.Replace("[StarredPostDeleteMode]", "");
                _mainFormat = _mainFormat.Replace("[PostDeleteMode]", "PrivateMesseageDelete");
                //_mainFormat = _mainFormat.Replace("[image]", Login.Build40x40ImageUrl(info.ImageGuid, false));
                string temp = "";
                DateTime dtTemp = DateTime.Now;
                DateTime dtCurrent;
                bool firstLoop = true;
                while (reader.Read())
                {
                    dtCurrent = (DateTime)reader["PostDate"];
                    //------
                    temp = _mainFormat;
                    if (dtCurrent.Year == dtTemp.Year && dtCurrent.Month == dtTemp.Month && dtCurrent.Day == dtTemp.Day && !firstLoop && reader.RecordsAffected != 1)
                        Login.PostDateFormat(ref temp, dtCurrent, true);
                    else
                    {
                        Login.PostDateFormat(ref temp, dtCurrent, false);
                        dtTemp = dtCurrent;
                        firstLoop = false;
                    }
                    //------
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    //------
                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl((string)reader["ImageGuid"], false));
                    //------
                    temp = temp.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
                    //------
                    Login.PostContentFormat(ref temp, (string)reader["PostContent"], (bool)reader["PostAlign"], (PostType)Convert.ToInt32(reader["PostType"].ToString()), (Int64)reader["id"]);
                    //------
                    Login.LastTimeFormat(ref temp, dtCurrent);
                    //------
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                if (currentPage == 1)
                    this.Session["ItemNum"] = (int)command.Parameters["@PrivateMessagesNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    Login.AjaxDoPagingEx(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowPrivateMessages", currentPage, Constants.MaxPostShow, (int)this.Session["ItemNum"], Constants.PagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void ListTopFriendsAndWeblogStatProc(SigninSessionInfo info)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ListTopFriendsAndWeblogStat_MyPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = info.BlogID;

            command.Parameters.Add("@TopFriendsShow", SqlDbType.Int);
            command.Parameters["@TopFriendsShow"].Value = Constants.TopFriendsShow;

            command.Parameters.Add("@PostNum", SqlDbType.Int);
            command.Parameters["@PostNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@FriendNum", SqlDbType.Int);
            command.Parameters["@FriendNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@FollowerNum", SqlDbType.Int);
            command.Parameters["@FollowerNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@PrivateMessagesNum", SqlDbType.Int);
            command.Parameters["@PrivateMessagesNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@HasReadPrivateMessages", SqlDbType.Bit);
            command.Parameters["@HasReadPrivateMessages"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_ListTopFriends"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\ListTopFriends.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_ListTopFriends"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_ListTopFriends"];

                int _p1Column = template.IndexOf("<column>") + "<column>".Length;
                int _p2Column = template.IndexOf("</column>");
                string start = template.Substring(0, _p1Column - "<column>".Length);
                string end = template.Substring(_p2Column + "</column>".Length);
                string _mainFormat = template.Substring(_p1Column, _p2Column - _p1Column);
                string _mainformat1 = System.Text.RegularExpressions.Regex.Replace(_mainFormat, "(</main>)|(<main>)", "");
                int counter = 0;
                string temp = "";
                string tempContent = "";
                while (reader.Read())
                {
                    counter++;
                    temp = _mainformat1.Replace("[image]", Login.Build38x38ImageUrl((string)reader["ImageGuid"], false));
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    temp = temp.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
                    tempContent += temp;
                    if (counter == Constants.TopFriendsShowColumnNum)
                    {
                        tempContent = start + tempContent + end;
                        this.ListTopFriends.InnerHtml += tempContent;
                        tempContent = "";
                        counter = 0;
                    }
                }
                if (counter != 0)
                {
                    string empty = _mainFormat;
                    Login.TagDelete(ref empty, "main");
                    for (int i = 0; i < Constants.TopFriendsShowColumnNum - counter; i++)
                        tempContent += empty;
                    tempContent = start + tempContent + end;
                    this.ListTopFriends.InnerHtml += tempContent;
                }
            }
            reader.Close();

            this.PostNumLabel.Text = command.Parameters["@PostNum"].Value.ToString();
            this.PrivateMessagesNumLabel.Text = command.Parameters["@PrivateMessagesNum"].Value.ToString();
            int _FriendNum = (int)command.Parameters["@FriendNum"].Value;
            this.FriendNumLabel.Text = _FriendNum.ToString();
            //if (_FriendNum > Constants.TopFriendsShow)
            if (_FriendNum > 0)
                this.ListTopFriendsHyperlink.Visible = true;
            int _FollowerNum = (int)command.Parameters["@FollowerNum"].Value;
            this.FollowerNumLabel.Text = _FollowerNum.ToString();
            if (_FollowerNum == 0)
                this.FollowerNumHyperlink.NavigateUrl = "";

            if (!(bool)command.Parameters["@HasReadPrivateMessages"].Value)
            {
                this.AlertSection.InnerHtml = this.AlertSection.InnerHtml.Replace("[AlertMessageText]", AlertMessages.HasNewPrivateMessages);
                this.AlertSection.Visible = true;
                this.NoAlertSection.Visible = false;
            }
            else
            {
                this.AlertSection.Visible = false;
                this.NoAlertSection.Visible = true;
            }

            command.Dispose();
            connection.Close();
            return;
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
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------
    }
}