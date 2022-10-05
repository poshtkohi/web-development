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
    public partial class home : System.Web.UI.Page
    {
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
        private void HomePageLoginSectionProc()
        {
            if (Login.IsLoginProc(this))
            {
                this.HomePageLoginSection.Visible = false;

                this.LoginedPanel.Visible = true;
                this.UnloginedPanel.Visible = false;
                SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                this.SetttingsHperLink.NavigateUrl = String.Format("http://{0}.{1}/cp/", _info.Username, Constants.BlogDomain);
                this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);
                return;
            }
            else
            {
                this.LoginedPanel.Visible = false;
                this.UnloginedPanel.Visible = true;
                return;
            }
        }
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "ShowLatestPosts":
                        ShowLatestPosts();
                        return;
                    default:
                        return;
                }
            }
            HomePageLoginSectionProc();
            ShowTopComments();
            ShowTopStars();
            ListTopUsersProc();
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
        }
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        private void ShowTopComments()
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "TopComments_HomePage_proc";

            command.Parameters.Add("@TopComments", SqlDbType.Int);
            command.Parameters["@TopComments"].Value = Constants.TopHomePageCommentsShow;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                /*if (this.Cache["_template_TopCommentsHomePage"] == null)
                {*/
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                    template = sr.ReadToEnd();
                    template = template.Replace("[CompleteCommentContent]", "");
                    Login.TagDelete(ref template, "CommentContent");
                    Login.TagDelete(ref template, "paging");
                    Login.TagDelete(ref template, "ForLatestPrivateMessages");
                    this.Cache["_template_TopCommentsHomePage"] = template;
                    sr.Close();
                /*}
                else
                    template = (string)this.Cache["_template_TopCommentsHomePage"];*/

                /*StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                template = sr.ReadToEnd();
                sr.Close();

                template = template.Replace("[CompleteCommentContent]", "");
                Login.TagDelete(ref template, "CommentContent");*/

                string temp = null;
                string _CommentContent = null;
                string _PostContent = null;
                this.TopComments.Text = "";
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
                    if (_PostContent.Length > Constants.MaxStringLengthForHomePageComments)
                        _PostContent = String.Format("{0}...", _PostContent.Substring(0, Constants.MaxStringLengthForHomePageComments));
                    temp = temp.Replace("[PostContent]", _PostContent);
                    this.TopComments.Text += temp;
                }
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return;
        }
        //--------------------------------------------------------------------
        private void ShowTopStars()
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "TopStars_HomePage_proc";

            command.Parameters.Add("@TopStars", SqlDbType.Int);
            command.Parameters["@TopStars"].Value = Constants.TopHomePageStarsShow;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                /*if (this.Cache["_template_TopStarsHomePage"] == null)
                {*/
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                    template = sr.ReadToEnd();
                    template = template.Replace("[CompleteCommentContent]", "");
                    Login.TagDelete(ref template, "CommentContent");
                    Login.TagDelete(ref template, "paging");
                    Login.TagDelete(ref template, "ForLatestPrivateMessages");
                    this.Cache["_template_TopStarsHomePage"] = template;
                    sr.Close();
               /* }
                else
                    template = (string)this.Cache["_template_TopStarsHomePage"];*/

                /*StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\LatestComments.html");
                template = sr.ReadToEnd();
                sr.Close();

                template = template.Replace("[CompleteCommentContent]", "");
                Login.TagDelete(ref template, "CommentContent");*/

                string temp = null;
                string _PostContent = null;
                this.TopStars.Text = "";
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
                    if (_PostContent.Length > Constants.MaxStringLengthForHomePageTopStars)
                        _PostContent = String.Format("{0}...", _PostContent.Substring(0, Constants.MaxStringLengthForHomePageTopStars));
                    temp = temp.Replace("[PostContent]", _PostContent);
                    this.TopStars.Text += temp;
                }
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return;
        }
        //--------------------------------------------------------------------
        private void ShowLatestPosts()
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;

            command.CommandText = "ShowLatestPosts_HomePage_proc";

            command.Parameters.Add("@TopLatestPostsShow", SqlDbType.Int);
            command.Parameters["@TopLatestPostsShow"].Value = Constants.TopLatestPostsShow;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                /*if (this.Cache["_template_LatestPostsShow"] == null)
                {*/
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\MyPostShow.html");
                    template = sr.ReadToEnd();
                    //--delete unnecessary sections--------------
                    Login.TagDelete(ref template, "delete");
                    Login.TagDelete(ref template, "paging");
                    Login.TagDelete(ref template, "star");
                    Login.TagDelete(ref template, "StarredPages");
                    Login.TagDelete(ref template, "StarsDelete");
                    //------------------------------------------
                    this.Cache["_template_LatestPostsShow"] = template;
                    sr.Close();
                /*}
                else
                    template = (string)this.Cache["_template_LatestPostsShow"];*/

                int _p1Post = template.IndexOf("<post>") + "<post>".Length;
                int _p2Post = template.IndexOf("</post>");
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
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();
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
        private void ListTopUsersProc()
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ListTopUsers_HomePage_proc";


            command.Parameters.Add("@TopUsersNum", SqlDbType.Int);
            command.Parameters["@TopUsersNum"].Value = Constants.TopFriendsShow;


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
                        this.ListTopUsers.InnerHtml += tempContent;
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
                    this.ListTopUsers.InnerHtml += tempContent;
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
            return;
        }
        //--------------------------------------------------------------------
    }
}