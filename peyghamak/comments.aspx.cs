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
    public partial class comments : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Login.IsLoginProc(this);
            /*if (!_IsLogin && this.Request.Form["content"] == null)// the user has not already logined into sysem.
            {
                this.Response.Redirect(Constants.LoginPageUrl, true);
                return;
            }*/
            string _subdomain = Login.FindSubdomain(this);
            if (_subdomain == "")
            {
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
            Int64 _PostID = -1;
            try
            {
                if (this.Request.QueryString["PostID"] != null)
                    _PostID = Convert.ToInt64(this.Request.QueryString["PostID"]);
                else
                    _PostID = Convert.ToInt64(this.Request.Form["PostID"]);
                if (_PostID <= 0)
                {
                    this.Response.Redirect("/", true);
                    return;
                }
            }
            catch
            {
                this.Response.Redirect("/", true);
                return;
            }
            /*if (this.Request.Form["content"] != null && this.Request.Form["content"] == "")
            {
                WriteStringToAjaxRequest("EmptyContent");
                return;
            }*/
            bool _IsMyCommentsPage = IsMyCommentsPage(_subdomain, _IsLogin);
            bool _IsOtherCommentsPage = false;
            if (!_IsMyCommentsPage)
                _IsOtherCommentsPage = IsOtherCommentsPage(_subdomain);
            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "ShowPostComments":
                        ShowPostComments(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
                        return;
                    case "CommentDelete":
                        CommentDelete(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
                        return;
                    case "post":
                        DoPost(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
                        return;
                    default:
                        return;
                }
            }
            /*if (!_IsLogin && this.Request.Form["content"] != null)// the user session was expired while typing his/her message.
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }
            string _subdomain = Login.FindSubdomain(this);
            if (_subdomain == "")
            {
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
            Int64 _PostID = -1;
            try
            {
                if (this.Request.QueryString["PostID"] != null)
                    _PostID = Convert.ToInt64(this.Request.QueryString["PostID"]);
                else
                    _PostID = Convert.ToInt64(this.Request.Form["PostID"]);
                if (_PostID <= 0)
                {
                    this.Response.Redirect("/", true);
                    return;
                }
            }
            catch
            {
                this.Response.Redirect("/", true);
                return;
            }
            if (this.Request.Form["content"] != null && this.Request.Form["content"] == "")
            {
                WriteStringToAjaxRequest("EmptyContent");
                return;
            }
            bool _IsMyCommentsPage = IsMyCommentsPage(_subdomain, _IsLogin);
            bool _IsOtherCommentsPage = false;
            if(!_IsMyCommentsPage)
                _IsOtherCommentsPage = IsOtherCommentsPage(_subdomain);
            if (this.Request.Form["ShowMode"] != null && this.Request.Form["ShowMode"] != "")
            {
                if (this.Request.Form["ShowMode"] == "comments")
                    ShowComments(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
                return;
            }
            if (_IsLogin && this.Request.Form["DeleteID"] != null && this.Request.Form["DeleteID"] != "")
            {
                CommentDelete(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
                return;
            }
            if (_IsLogin && this.Request.Form["content"] != null && this.Request.Form["content"] != "")
            {
                PostComment(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage);
                return;
            }*/
            if (!CheckCorrectComment(_PostID, _IsMyCommentsPage, _IsOtherCommentsPage))
            {
                this.Response.Redirect("/", true);
                return;
            }
            PageSettings(_IsMyCommentsPage, _IsOtherCommentsPage, _IsLogin, _PostID);
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
        private void CommentDelete(Int64 _PostID, bool _IsMyCommentsPage, bool _IsOtherCommentsPage)
        {
            if (!_IsMyCommentsPage && _IsOtherCommentsPage)
                return;
            Int64 _CommentID = -1;
            try
            {
                _CommentID = Convert.ToInt32(this.Request.Form["DeleteID"]);
            }
            catch { return; }

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

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void DoPost(Int64 _PostID, bool _IsMyCommentsPage, bool _IsOtherCommentsPage)
        {
            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
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
            command.Parameters["@PostContent"].Value = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);

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

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void ShowPostComments(Int64 _PostID, bool _IsMyCommentsPage, bool _IsOtherCommentsPage)
        {
            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_Num_Comments"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
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
            command.Parameters["@PageSize"].Value = Constants.MaxPostShow;

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
                string template = "";
                if (this.Cache["_template_comments"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\CommentTemplate.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_comments"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_comments"];

                if (_IsOtherCommentsPage)
                    Login.TagDelete(ref template, "delete");
                //------
                //DoPaging(ref template, this, 1000);
                //------
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
                    temp = temp.Replace("[PostID]", _PostID.ToString());
                    //------
                    temp = temp.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    temp = temp.Replace("[image]", Login.Build40x40ImageUrl((string)reader["ImageGuid"], false));
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
                    this.Session["_Num_Comments"] = (int)command.Parameters["@CommentNum"].Value;

                command.Dispose();
                connection.Close();

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["_Num_Comments"]);
                    Login.AjaxDoPagingEx(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowPostComments", currentPage, Constants.MaxPostShow, (int)this.Session["_Num_Comments"], Constants.PagingNumber, "ShowItems");
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
                string _PostFormat = "<div align='[align]' dir='[dir]' id='div_[id]'>[PostContent]</div>";
                Login.PostContentFormat(ref _PostFormat, (string)reader["PostContent"], (bool)reader["PostAlign"], (PostType)Convert.ToInt32(reader["PostType"].ToString()), (Int64)reader["id"]);
                this.PostContent.InnerHtml = _PostFormat;
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
            if (_IsLogin)
            {
                SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                this.LoginedPanel.Visible = true;
                this.UnloginedPanel.Visible = false;
                this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);

                this.PostSection.Visible = true;
                this.MustLoginToCommentSection.Visible = false;
                this.PostSection.InnerHtml = this.PostSection.InnerHtml.Replace("[PostID]", _PostID.ToString());
            }
            else
            {
                this.LoginedPanel.Visible = false;
                this.UnloginedPanel.Visible = true;

                this.PostSection.Visible = false;
                this.MustLoginToCommentSection.Visible = true;
            }
            if (_IsMyCommentsPage)
            {
                SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                this.UserImage.ImageUrl = Login.Build75x75ImageUrl(_info.ImageGuid, false);
                this.UserImage.ToolTip = _info.Name;
                this.NameLabel.Text = _info.Name;
            }
            if (_IsOtherCommentsPage)
            {
                this.UserImage.ImageUrl = Login.Build75x75ImageUrl((string)this.Session["GuestImageGuid"], false);
                this.UserImage.ToolTip = (string)this.Session["GuestName"];
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
