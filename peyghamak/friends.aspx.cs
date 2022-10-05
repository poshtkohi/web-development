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
    public partial class friends : System.Web.UI.Page
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
            /*for (int i = 0; i < this.Request.Form.Count; i++)
                this.Response.Write(this.Request.Form.GetKey(i) + ":" + this.Request.Form[i] + "<br>");
            return;*/
            string _subdomain = Login.FindSubdomain(this);
            if (_subdomain == "")
            {
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
            bool _IsLogin = Login.IsLoginProc(this);
            //---------friend management operations----------------------
            if (_IsLogin && this.Request.Form["action"] != null && this.Request.Form["FriendBlogID"] != null)
            {
                Int64 _FriendBlogID = -1;
                try { _FriendBlogID = Convert.ToInt64(this.Request.Form["FriendBlogID"]); }
                catch{ this.Response.Redirect("friends.aspx", true); return; }
                switch (this.Request.Form["action"])
                {
                    case "add":
                        AddFriend(_FriendBlogID);
                        this.Response.Redirect("friends.aspx?action=added&mode=" + this.Request.Form["mode"], true);
                        return;
                    case "remove":
                        RemoveFriend(_FriendBlogID);
                        this.Response.Redirect("friends.aspx?action=removed&mode=" + this.Request.Form["mode"], true);
                        return;
                    case "block":
                        BlockFriend(_FriendBlogID);
                        this.Response.Redirect("friends.aspx?action=blocked&mode=" + this.Request.Form["mode"], true);
                        return;
                    default:
                        break;
                }
            }
            if (this.Request.QueryString["action"] != null)
            {
                switch (this.Request.QueryString["action"])
                {
                    case "blocked":
                        {
                            this.messagee.InnerHtml = ">> این شخص از لیست شما مسدود شد.";
                            this.messagee.Visible = true;
                            break;
                        }
                    case "removed":
                        {
                            this.messagee.InnerHtml = ">> این شخص از لیست دوستان شما حذف شد.";
                            this.messagee.Visible = true;
                            break;
                        }
                    case "added":
                        {
                            this.messagee.InnerHtml = ">> این شخص به لیست دوستان شما اضافه شد.";
                            this.messagee.Visible = true;
                            break;
                        }
                    default:
                        {
                            this.messagee.InnerHtml = "";
                            this.messagee.Visible = false;
                            break;
                        }
                }
            }
            //-----------------------------------------------------------
            bool _IsMyFriendsPage = IsMyFriendsPage(_subdomain, _IsLogin);
            bool _IsOtherFriendsPage = false;
            if (!_IsMyFriendsPage)
                _IsOtherFriendsPage = IsOtherFriendsPage(_subdomain);
            if (!_IsMyFriendsPage && !_IsOtherFriendsPage)
            {
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
            ListFriends(_IsLogin, _IsMyFriendsPage, _IsOtherFriendsPage, _subdomain);
            PageSettings(_IsLogin);
        }
        //--------------------------------------------------------------------
        private void ListFriends(bool _IsLogin, bool _IsMyFriendsPage, bool _IsOtherFriendsPage, string _subdomain)
        {
            int currentPage = 1;
            bool _friendsMode = true;
            if (this.Request.QueryString["mode"] != null)
            {
                if (this.Request.QueryString["mode"] == "followers")
                    _friendsMode = false;
            }
            try
            {
                currentPage = Convert.ToInt32(this.Request.QueryString["page"]);
            }
            catch { return; }

            if (currentPage == 0)
                currentPage++;

            if (_friendsMode)
            {
                this.FriendsLable.Visible = true;
                this.FollowersLable.Visible = false;
                this.FriendsHyperlink.Visible = false;
                this.FollowersHyperlink.Visible = true;
                this.title.InnerText = _subdomain + " " + this.FriendsLable.Text;
            }
            else
            {
                this.FriendsLable.Visible = false;
                this.FollowersLable.Visible = true;
                this.FriendsHyperlink.Visible = true;
                this.FollowersHyperlink.Visible = false;
                this.title.InnerText = _subdomain + " " + this.FollowersLable.Text;
            }

            if (currentPage > 1 && this.Session["ItemNum"] == null)
            {
                if (_friendsMode)
                    this.Response.Redirect("friends.aspx?page=1&mode=friends", true);
                else
                    this.Response.Redirect("friends.aspx?page=1&mode=followers", true);
                return;
            }

            SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            if (_friendsMode)
                command.CommandText = "ListFriends_FriendsPage_proc";
            else
                command.CommandText = "ListFollowers_FriendsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            if (_IsLogin && _IsMyFriendsPage && !_IsOtherFriendsPage)
                command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;
            if (_IsLogin && !_IsMyFriendsPage && _IsOtherFriendsPage)
                command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];
            if (!_IsLogin && !_IsMyFriendsPage && _IsOtherFriendsPage)
                command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@MyLoginedBlogID", SqlDbType.BigInt);
            if (_IsLogin)
                command.Parameters["@MyLoginedBlogID"].Value = _info.BlogID;
            else
                command.Parameters["@MyLoginedBlogID"].Value = -1;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = Constants.TopFriendsOrFollowersShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            if (_friendsMode)
            {
                command.Parameters.Add("@FriendNum", SqlDbType.Int);
                command.Parameters["@FriendNum"].Direction = ParameterDirection.Output;
            }
            else
            {
                command.Parameters.Add("@FollowerNum", SqlDbType.Int);
                command.Parameters["@FollowerNum"].Direction = ParameterDirection.Output;
            }

            command.Parameters.Add("@_IsLogin", SqlDbType.Bit);
            command.Parameters["@_IsLogin"].Value = _IsLogin;

            command.Parameters.Add("@_IsMyFriendsPage", SqlDbType.Bit);
            command.Parameters["@_IsMyFriendsPage"].Value = _IsMyFriendsPage;

            command.Parameters.Add("@_IsOtherFriendsPage", SqlDbType.Bit);
            command.Parameters["@_IsOtherFriendsPage"].Value = _IsOtherFriendsPage;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_firiends"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\ListFiriends.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_firiends"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_firiends"];

                //this section can be removed for block friend operation in the next system developments with the /**/ sections
                Login.TagDelete(ref template, "block");
                Login.TagDelete(ref template, "heblocked");
                Login.TagDelete(ref template, "meblocked");
                if (!_friendsMode)
                    Login.TagDelete(ref template, "remove");
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //----conditions------------------------------------------
                if (!_IsLogin && !_IsMyFriendsPage && _IsOtherFriendsPage)
                {
                    Login.TagDelete(ref template, "add");
                    Login.TagDelete(ref template, "remove");
                    /*                  Login.TagDelete(ref template, "block");
                                        Login.TagDelete(ref template, "heblocked");
                                        Login.TagDelete(ref template, "meblocked");
                     */
                }
                if (_IsLogin && _IsMyFriendsPage && !_IsOtherFriendsPage && _friendsMode)
                {
                    Login.TagDelete(ref template, "add");
                    //Login.TagDelete(ref template, "remove");
                    /*                  //Login.TagDelete(ref template, "block");
                                        Login.TagDelete(ref template, "heblocked");
                                        Login.TagDelete(ref template, "meblocked");
                     */
                }
                if (_IsLogin)
                {
                    if (_friendsMode)
                    {
                        template = template.Replace("[mode]", "friends");
                        Login.TagDelete(ref template, "messagee");
                    }
                    else
                    {
                        template = template.Replace("[mode]", "followers");
                        if (!_IsMyFriendsPage)
                            Login.TagDelete(ref template, "messagee");
                    }
                }
                else
                    Login.TagDelete(ref template, "messagee");

                int _p1List = template.IndexOf("<list>") + "<list>".Length;
                int _p2List = template.IndexOf("</list>");
                int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
                int _p2Paging = template.IndexOf("</paging>");
                if (_p1List <= 0 || _p2List <= 0)
                {
                    this.Response.Write(template);
                    this.Response.OutputStream.Flush();
                    return;
                }
                this.Response.Write(template.Substring(0, _p1List - "<list>".Length));
                this.Response.Flush();
                string _mainFormat = template.Substring(_p1List, _p2List - _p1List);

                string temp = "";
                PersianCalendar pc = new PersianCalendar();
                while (reader.Read())
                {
                    temp = _mainFormat.Replace("[image]", Login.Build75x75ImageUrl((string)reader["ImageGuid"], false));
                    //----conditions------------------------------------------
                    Int64 _FriendBlogID = (Int64)reader["FriendBlogID"];
                    if (_IsLogin)
                        temp = temp.Replace("[FriendBlogID]", _FriendBlogID.ToString());
                    if ((_IsLogin && _IsOtherFriendsPage) || (_IsLogin && !_friendsMode))
                    {
                        if (_info.BlogID == _FriendBlogID)
                        {
                            Login.TagDelete(ref temp, "add");
                            Login.TagDelete(ref temp, "remove");
                            /*                          Login.TagDelete(ref temp, "block");
                                                        Login.TagDelete(ref temp, "heblocked");
                                                        Login.TagDelete(ref temp, "meblocked");
                             */
                        }
                        //STR(IsFriend)+STR(IHaveBlocked)+STR(HeHasBlocked) AS FriendshipStatus
                        if (reader["FriendshipStatus"] != DBNull.Value) //100
                        {
                            string FriendshipStatus = ((string)reader["FriendshipStatus"]).Replace(" ", "");
                            bool IsFriend = CharToBoolean(FriendshipStatus[0]);
                            /*                          bool IHaveBlocked = CharToBoolean(FriendshipStatus[1]);
                                                        bool HeHasBlocked = CharToBoolean(FriendshipStatus[2]);
                                                        if (HeHasBlocked)
                                                        {
                                                            Login.TagDelete(ref temp, "add");
                                                            Login.TagDelete(ref temp, "remove");
                                                            Login.TagDelete(ref temp, "block");
                                                            //Login.TagDelete(ref temp, "heblocked");
                                                            Login.TagDelete(ref temp, "meblocked");
                                                            goto Continue;
                                                        }
 
                                                        if (IHaveBlocked)
                                                        {
                                                            Login.TagDelete(ref temp, "add");
                                                            Login.TagDelete(ref temp, "remove");
                                                            Login.TagDelete(ref temp, "block");
                                                            Login.TagDelete(ref temp, "heblocked");
                                                            //Login.TagDelete(ref temp, "meblocked");
                                                            goto Continue;
                                                        }
                             */
                            if (IsFriend)
                            {
                                Login.TagDelete(ref temp, "add");
                                //Login.TagDelete(ref temp, "remove");
                                /*                               //Login.TagDelete(ref temp, "block");
                                                                Login.TagDelete(ref temp, "heblocked");
                                                                Login.TagDelete(ref temp, "meblocked");
                                 */
                                goto Continue;
                            }
                            if (!IsFriend && _friendsMode)
                            {
                                //Login.TagDelete(ref temp, "add");
                                Login.TagDelete(ref temp, "remove");
                                /*                              Login.TagDelete(ref temp, "block");
                                                                Login.TagDelete(ref temp, "heblocked");
                                                                Login.TagDelete(ref temp, "meblocked");
                                 */
                                goto Continue;
                            }
                            if (!IsFriend && !_friendsMode)
                            {
                                //Login.TagDelete(ref temp, "add");
                                Login.TagDelete(ref temp, "remove");
                                /*                              Login.TagDelete(ref temp, "block");
                                                                Login.TagDelete(ref temp, "heblocked");
                                                                Login.TagDelete(ref temp, "meblocked");
                                 */
                                goto Continue;
                            }
                        }
                        else
                        {
                            //Login.TagDelete(ref temp, "add");
                            Login.TagDelete(ref temp, "remove");
                            /*                          Login.TagDelete(ref temp, "block");
                                                        Login.TagDelete(ref temp, "heblocked");
                                                        Login.TagDelete(ref temp, "meblocked");
                             */
                        }
                    }
                Continue:
                    //--------------------------------------------------------
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    temp = temp.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
                    if ((bool)reader["ShowAgeEnabled"])
                        temp = temp.Replace("[age]", String.Format("{0}", pc.GetYear(DateTime.Now) - (int)reader["BirthYear"]));
                    else
                        Login.TagDelete(ref temp, "age");
                    if ((bool)reader["ShowCityEnabled"])
                        temp = temp.Replace("[city]", (string)reader["city"]);
                    else
                        Login.TagDelete(ref temp, "city");
                    temp = temp.Replace("[country]", (string)reader["country"]);
                    ///////////////////////////////////////////////

                    this.ListOutput.InnerHtml += temp;
                }

                reader.Close();

                if (currentPage == 1)
                {
                    if (_friendsMode)
                        this.Session["ItemNum_friends_or_followers"] = (int)command.Parameters["@FriendNum"].Value;
                    else
                        this.Session["ItemNum_friends_or_followers"] = (int)command.Parameters["@FollowerNum"].Value;
                }

                command.Dispose();
                connection.Close();

                if (_friendsMode)
                {
                    if (_p1Paging > 0 && _p2Paging > 0)
                    {
                        this.ListOutput.InnerHtml += template.Substring(_p2List + "</list>".Length, _p1Paging - (_p2List + "</list>".Length));
                        string _paging = template.Substring(_p1Paging, _p2Paging - _p1Paging);
                        Login.DoPaging(ref _paging, "friends", currentPage, Constants.TopFriendsOrFollowersShow, (int)this.Session["ItemNum_friends_or_followers"]);
                        this.ListOutput.InnerHtml += _paging;
                    }
                    else
                        this.ListOutput.InnerHtml += template.Substring(_p2List + "</list>".Length);
                }
                else
                {
                    if (_p1Paging > 0 && _p2Paging > 0)
                    {
                        this.ListOutput.InnerHtml += template.Substring(_p2List + "</list>".Length, _p1Paging - (_p2List + "</list>".Length));
                        string _paging = template.Substring(_p1Paging, _p2Paging - _p1Paging);
                        Login.DoPaging(ref _paging, "followers", currentPage, Constants.TopFriendsOrFollowersShow, (int)this.Session["ItemNum_friends_or_followers"]);
                        this.ListOutput.InnerHtml += _paging;
                    }
                    else
                        this.ListOutput.InnerHtml += template.Substring(_p2List + "</list>".Length);
                }
                //this.FriendsOrFollowersNum.Text = this.Session["ItemNum"].ToString();
                return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close(); ;
                this.Response.Redirect("/", true);
                return;
            }
        }
        //--------------------------------------------------------------------
        /// <summary>
        /// Check whether this page has been called by the logined user and this page is his/her personal friends page. If 
        /// the user already has not logined into system, this method invoke the login procedure to probably login the user by 
        /// his/her browser cookie.
        /// </summary>
        /// <returns></returns>
        private bool IsMyFriendsPage(string _subdomain, bool _IsLogin)
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
        private bool IsOtherFriendsPage(string _subdomain)
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
        private void PageSettings(bool _IsLogin)
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
            }
            else
            {
                this.LoginedPanel.Visible = false;
                this.UnloginedPanel.Visible = true;
            }
        }
        //--------------------------------------------------------------------
        private bool CharToBoolean(char c)
        {
            if (c == '0')
                return
                    false;
            if (c == '1')
                return
                    true;
            else
                throw new InvalidCastException("the input char mus be 0 or 1.");
        }
        //--------------------------------------------------------------------
        private void AddFriend(Int64 _FriendBlogID)
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
            command.Parameters["@FriendBlogID"].Value = _FriendBlogID;

            command.Parameters.Add("@_idMy", SqlDbType.BigInt);
            command.Parameters["@_idMy"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@_idHe", SqlDbType.BigInt);
            command.Parameters["@_idHe"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();

            //this._idMy.Value = command.Parameters["@_idMy"].Value.ToString();
            //this._idHe.Value = command.Parameters["@_idHe"].Value.ToString();

            command.Dispose();
            connection.Close();

            return;
        }
        //--------------------------------------------------------------------
        private void RemoveFriend(Int64 _FriendBlogID)
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
            command.Parameters["@FriendBlogID"].Value = _FriendBlogID;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            return;
        }
        //--------------------------------------------------------------------
        private void BlockFriend(Int64 _FriendBlogID)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringFriends1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BlockUser_FriendsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@FriendBlogID", SqlDbType.BigInt);
            command.Parameters["@FriendBlogID"].Value = _FriendBlogID;

            //------Linked Server settings---------------
            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            return;
        }
        //--------------------------------------------------------------------
    }
}