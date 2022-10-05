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
    public partial class guest : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _CheckGuestBlogID = CheckGuestBlogID();
            if (this.Request.Form["mode"] != null)
            {
                if (_CheckGuestBlogID)
                {
                    Int64 _BlogID = (Int64)this.Session["GuestBlogID"];
                    if (_BlogID == -1)
                        return;
                    switch (this.Request.Form["mode"])
                    {
                        case "ShowGuestPosts":
                            ShowGuestPosts(_BlogID);
                            return;
                        default:
                            return;
                    }
                }
            }
            bool _Islogined = Login.IsLoginProc(this);
            if (!_CheckGuestBlogID)
            {
                if (!_Islogined)
                    this.Response.Redirect(Constants.MainPageUrl, true);
                else
                {
                    SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                    this.Response.Redirect(String.Format("http://{0}.{1}/my.aspx", info.Username, Constants.BlogDomain), true);
                }
                return;
            }

            this.feed.Attributes.Add("title", String.Format("{0} (RSS Feed)", (string)this.Session["GuestName"]));
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UserProfileInfo_GuestPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string _name = (string)reader["name"];
                this.title.InnerText = _name;
                this.UserImage.ImageUrl = Login.Build75x75ImageUrl((string)reader["ImageGuid"], false);
                this.UserImage.ToolTip = _name;
                PersianCalendar pc = new PersianCalendar();

                this.AboutLabel.Text = (string)reader["about"];
                this.UrlHyperlink.Text = (string)reader["url"];
                if (this.UrlHyperlink.Text.ToLower() != "http://")
                    this.UrlHyperlink.NavigateUrl = this.UrlHyperlink.Text;
                if (this.UrlHyperlink.Text == "")
                {
                    this.UrlHyperlink.Text = String.Format("http://{0}/", this.Request.Url.Host);
                    this.UrlHyperlink.NavigateUrl = String.Format("http://{0}/", this.Request.Url.Host);
                }
                this.PostNumLabel.Text = reader["PostNum"].ToString();
                int _FriendNum = (int)reader["FriendNum"];
                this.FriendNumLabel.Text = _FriendNum.ToString();
                int _FollowerNum = (int)reader["FollowerNum"];
                this.FollowerNumLabel.Text = _FollowerNum.ToString();
                if (_FollowerNum == 0)
                    this.FollowerNumHyperlink.NavigateUrl = "";
                string _country = (string)reader["country"];
                string _age = "";
                if ((bool)reader["ShowAgeEnabled"])
                    _age = String.Format("{0} ساله ", (pc.GetYear(DateTime.Now) - (int)reader["BirthYear"]).ToString());
                string _city = "";
                if ((bool)reader["ShowCityEnabled"])
                {
                    if (reader["city"] != DBNull.Value)
                        _city = String.Format("-{0}", (string)reader["city"]);
                }
                this.NameLabel.Text = _name;
                this.ProfileLabel.Text = string.Format("{0} از {1}{2}", _age, _country, _city);
                reader.Close();
                command.Dispose();
                connection.Close();
                this.CurrentDate.Text = "امروز " + Login.PersianDate(DateTime.Now);
                //--------Friend section: add friend and view the guest page friends-----------
                connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
                connection.Open();
                command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                if (_Islogined)
                {
                    //---check friendship
                    SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                    command.CommandText = "BlockCheck_GuestPage_proc";

                    command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                    command.Parameters["@BlogID"].Value = _info.BlogID;

                    command.Parameters.Add("@FriendBlogID", SqlDbType.BigInt);
                    command.Parameters["@FriendBlogID"].Value = (Int64)this.Session["GuestBlogID"];

                    command.Parameters.Add("@_IHaveBlocked", SqlDbType.Bit);
                    command.Parameters["@_IHaveBlocked"].Direction = ParameterDirection.Output;

                    command.Parameters.Add("@_HeHasBlocked", SqlDbType.Bit);
                    command.Parameters["@_HeHasBlocked"].Direction = ParameterDirection.Output;

                    command.Parameters.Add("@_IsFriend", SqlDbType.Bit);
                    command.Parameters["@_IsFriend"].Direction = ParameterDirection.Output;

                    command.Parameters.Add("@_IsFollower", SqlDbType.Bit);
                    command.Parameters["@_IsFollower"].Direction = ParameterDirection.Output;

                    command.Parameters.Add("@_IsExisted", SqlDbType.Bit);
                    command.Parameters["@_IsExisted"].Direction = ParameterDirection.Output;

                    command.Parameters.Add("@_idMy", SqlDbType.BigInt);
                    command.Parameters["@_idMy"].Direction = ParameterDirection.Output;

                    command.Parameters.Add("@_idHe", SqlDbType.BigInt);
                    command.Parameters["@_idHe"].Direction = ParameterDirection.Output;

                    //-------------------------------------------
                    command.ExecuteNonQuery();

                    this._idMy.Value = command.Parameters["@_idMy"].Value.ToString();
                    this._idHe.Value = command.Parameters["@_idHe"].Value.ToString();


                    /////////////
                    if ((bool)command.Parameters["@_IsFollower"].Value)
                    {
                        this.PrivateMessageImageButton.Visible = true;
                        this.PrivateMessageImageButton.Enabled = true;
                    }
                    ///////////




                    if (!(bool)command.Parameters["@_IsExisted"].Value)
                    {
                        this.AddFriendImageButton.Visible = true;
                        this.AddFriendImageButton.Enabled = true;
                        this.RemoveFriendImageButton.Visible = false;
                        this.RemoveFriendImageButton.Enabled = false;
                        this.UnBlockFriendHyperlink.Visible = false;
                        this.UnBlockFriendHyperlink.Enabled = false;
                        this.BlockFriendHyperlink.Visible = false;
                        this.BlockFriendHyperlink.Enabled = false;
                        command.Parameters.Clear();
                        goto Continue;
                    }


                    /*
                    //this section provides the blocking friend procedure which can be added evrey time to system
                    if ((bool)command.Parameters["@_HeHasBlocked"].Value)
                    {
                        this.AddFriendImageButton.Visible = false;
                        this.AddFriendImageButton.Enabled = false;
                        this.RemoveFriendImageButton.Visible = false;
                        this.RemoveFriendImageButton.Enabled = false;
                        this.UnBlockFriendHyperlink.Visible = false;
                        this.UnBlockFriendHyperlink.Enabled = false;
                        this.BlockFriendHyperlink.Visible = false;
                        this.BlockFriendHyperlink.Enabled = false;
                        this.message.InnerHtml = "<img src='images/attention.png' class='attentionImage'>این شخص قبلا شما را مسدود کرده است !";
                        this.message.Visible = true;
                        command.Parameters.Clear();
                        goto Continue;
                    }
                    if ((bool)command.Parameters["@_IHaveBlocked"].Value)
                    {
                        this.AddFriendImageButton.Visible = false;
                        this.AddFriendImageButton.Enabled = false;
                        this.RemoveFriendImageButton.Visible = false;
                        this.RemoveFriendImageButton.Enabled = false;
                        this.BlockFriendHyperlink.Visible = false;
                        this.BlockFriendHyperlink.Enabled = false;
                        this.UnBlockFriendHyperlink.Visible = true;
                        this.UnBlockFriendHyperlink.Enabled = true;
                        this.message.InnerHtml = "<img src='images/attention.png' class='attentionImage'>شما قبلا این شخص را مسدود کرده اید !";
                        this.message.Visible = true;
                        command.Parameters.Clear();
                        goto Continue;
                    }*/
                    if ((bool)command.Parameters["@_IsFriend"].Value)
                    {
                        this.AddFriendImageButton.Visible = false;
                        this.AddFriendImageButton.Enabled = false;
                        this.RemoveFriendImageButton.Visible = true;
                        this.RemoveFriendImageButton.Enabled = true;
                        /*this.BlockFriendHyperlink.Visible = true;
                        this.BlockFriendHyperlink.Enabled = true;
                        this.UnBlockFriendHyperlink.Visible = false;
                        this.UnBlockFriendHyperlink.Enabled = false;*/
                        this.messagee.InnerHtml = "";
                        this.messagee.Visible = false;
                        command.Parameters.Clear();
                        goto Continue;
                    }
                    if (!(bool)command.Parameters["@_IsFriend"].Value)
                    {
                        this.AddFriendImageButton.Visible = true;
                        this.AddFriendImageButton.Enabled = true;
                        this.RemoveFriendImageButton.Visible = false;
                        this.RemoveFriendImageButton.Enabled = false;
                        /*this.BlockFriendHyperlink.Visible = false;
                        this.BlockFriendHyperlink.Enabled = false;
                        this.UnBlockFriendHyperlink.Visible = false;
                        this.UnBlockFriendHyperlink.Enabled = false;*/
                        this.messagee.InnerHtml = "";
                        this.messagee.Visible = false;
                        command.Parameters.Clear();
                        goto Continue;
                    }

                Continue:
                    this.LoginedPanel.Visible = true;
                    this.UnloginedPanel.Visible = false;
                    this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                    this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);

                    if (this.Request.QueryString["action"] != null)
                    {
                        switch (this.Request.QueryString["action"])
                        {
                            case "added":
                                {
                                    this.messagee.InnerHtml = ">> این شخص به لیست دوستان شما اضافه شد.";
                                    this.messagee.Visible = true;
                                    break;
                                }
                            case "removed":
                                {
                                    this.messagee.InnerHtml = ">> این شخص از لیست دوستان شما خارج شد.";
                                    this.messagee.Visible = true;
                                    break;
                                }
                            case "blocked":
                                {
                                    this.messagee.InnerHtml = ">> این شخص از لیست شما مسدود شد.";
                                    this.messagee.Visible = true;
                                    break;
                                }
                            case "unblocked":
                                {
                                    this.messagee.InnerHtml = ">> این شخص به لیست دوستان شما اضافه شد.";
                                    this.messagee.Visible = true;
                                    break;
                                }
                            default:
                                {
                                    //this.messagee.InnerHtml = "";
                                    //this.messagee.Visible = false;
                                    break;
                                }
                        }
                    }
                }
                if (!_Islogined)
                {
                    this.LoginedPanel.Visible = false;
                    this.UnloginedPanel.Visible = true;
                }
                ListTopFriendsProc(command, _FriendNum);//
                //-----------------------------------------------------------------------------

                return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowGuestPosts(Int64 _BlogID)
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
            command.Parameters["@BlogID"].Value = _BlogID;

            Int64 _MyBlogID = _BlogID;
            if (Login.IsLoginProc(this))
                _MyBlogID = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@MyBlogID", SqlDbType.BigInt);
            command.Parameters["@MyBlogID"].Value = _MyBlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = Constants.MaxPostShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PostNum", SqlDbType.Int);
            command.Parameters["@PostNum"].Direction = ParameterDirection.Output;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                bool _IsLogin = Login.IsLoginProc(this);
                string template = "";
                if (this.Cache["_template_GuestPostShow"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\MyPostShow.html");
                    template = sr.ReadToEnd();
                    template = template.Replace("[starredDeleteMode]", "StarredPostDeleteFromGuestPage");
                    Login.TagDelete(ref template, "StarsDelete");
                    this.Cache["_template_GuestPostShow"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_GuestPostShow"];

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
                _mainFormat = _mainFormat.Replace("[url]", "");
                //--delete unnecessary sections--------------
                Login.TagDelete(ref _mainFormat, "delete");
                Login.TagDelete(ref _mainFormat, "StarredPages");
                if (!_IsLogin)
                    Login.TagDelete(ref _mainFormat, "star");
                //------------------------------------------
                _mainFormat = _mainFormat.Replace("[name]", (string)this.Session["GuestName"]);
                _mainFormat = _mainFormat.Replace("[image]", Login.Build40x40ImageUrl((string)this.Session["GuestImageGuid"], false));
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
                    if (_IsLogin)
                    {
                        if ((int)reader["Starred"] == 1)
                        {
                            Login.TagDelete(ref temp, "unstarred");
                            temp = temp.Replace("[StarredID]", reader["StarredID"].ToString());
                        }
                        else
                            Login.TagDelete(ref temp, "starred");
                    }
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
                    Login.AjaxDoPagingEx(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowGuestPosts", currentPage, Constants.MaxPostShow, _CurrentPostNum, Constants.PagingNumber, "ShowItems");
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
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
            return;
        }
        //--------------------------------------------------------------------
        private void ListTopFriendsProc(SqlCommand command, int _FriendNum)
        {
            command.Parameters.Clear();
            command.CommandText = "ListTopFriends_GuestPage_proc";

            //this.Response.Write((Int64)this.Session["GuestBlogID"]);
            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@TopFriendsShow", SqlDbType.Int);
            command.Parameters["@TopFriendsShow"].Value = Constants.TopFriendsShow;

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
                reader.Close();
                //if (_FriendNum > Constants.TopFriendsShow)
                if (_FriendNum > 0)
                    this.ListTopFriendsHyperlink.Visible = true;
                return;
            }
            else
            {
                reader.Close();
                return;
            }
        }
        //--------------------------------------------------------------------
        protected void AddFriendHyperlink_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddFriend_GuestPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@FriendBlogID", SqlDbType.BigInt);
            command.Parameters["@FriendBlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@_idMy", SqlDbType.BigInt);
            command.Parameters["@_idMy"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@_idHe", SqlDbType.BigInt);
            command.Parameters["@_idHe"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();

            this._idMy.Value = command.Parameters["@_idMy"].Value.ToString();
            this._idHe.Value = command.Parameters["@_idHe"].Value.ToString();

            command.Dispose();
            connection.Close();
            this.Response.Redirect("guest.aspx?action=added", true);
            return;
        }
        //--------------------------------------------------------------------
        protected void RemoveFriendHyperlink_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "RemoveFriend_FriendsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@FriendBlogID", SqlDbType.BigInt);
            command.Parameters["@FriendBlogID"].Value = (Int64)this.Session["GuestBlogID"];

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
            this.Response.Redirect("guest.aspx?action=removed", true);
            return;
        }
        //--------------------------------------------------------------------
        protected void BlockFriendHyperlink_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BlockUser_GuestPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@FriendBlogID", SqlDbType.BigInt);
            command.Parameters["@FriendBlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@_idMy", SqlDbType.BigInt);
            command.Parameters["@_idMy"].Value = Convert.ToInt64(this._idMy.Value);

            command.Parameters.Add("@_idHe", SqlDbType.BigInt);
            command.Parameters["@_idHe"].Value = Convert.ToInt64(this._idHe.Value);

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            this.Response.Redirect("guest.aspx?action=blocked", true);
            return;
        }
        //--------------------------------------------------------------------
        protected void UnblockFriendHyperlink_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UnBlockUser_GuestPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@FriendBlogID", SqlDbType.BigInt);
            command.Parameters["@FriendBlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@_idMy", SqlDbType.BigInt);
            command.Parameters["@_idMy"].Value = Convert.ToInt64(this._idMy.Value);

            command.Parameters.Add("@_idHe", SqlDbType.BigInt);
            command.Parameters["@_idHe"].Value = Convert.ToInt64(this._idHe.Value);

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            this.Response.Redirect("guest.aspx?action=unblocked", true);
            return;
        }
        //--------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns>If false returns, this means theat the user has called the guest of main domain.In this case , the calling method must redirect the user request to main url page.</returns>
        private bool CheckGuestBlogID()
        {
            string _subdomain = Login.FindSubdomain(this);
            // this.Response.Write(_subdomain + "<br>");
            //this.Response.Write(((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username);
            if (_subdomain == "")
                return false;
            if (this.Session["SigninSessionInfo"] != null)
            {
                if (((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username == _subdomain)
                {
                    //throw new Exception();
                    return false;
                }
            }
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
        protected void AddFriendHyperlink_Click(object sender, ImageClickEventArgs e)
        {
            this.AddFriendHyperlink_Click(sender, (EventArgs)e);
        }
        //--------------------------------------------------------------------
        protected void RemoveFriendImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.RemoveFriendHyperlink_Click(sender, (EventArgs)e);
        }
        //--------------------------------------------------------------------
        protected void PrivateMessageImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.PrivateMessageImage_Click(sender, (EventArgs)e);
        }
        //--------------------------------------------------------------------
        protected void PrivateMessageImage_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(String.Format("{0}message.aspx?PersonID={1}",Login.BuildUserUrl(((SigninSessionInfo)this.Session["SigninSessionInfo"]).Username), (Int64)this.Session["GuestBlogID"]), true);
            return;
        }
        //--------------------------------------------------------------------
    }
}