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

namespace Peyghamak.mobile
{
    public partial class friends : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            string _subdomain = Login.FindSubdomain(this);
            if (_subdomain == "")
            {
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
            bool _IsLogin = Login.IsLoginProc(this);
            bool _IsMyFriendsPage = IsMyFriendsPage(_subdomain, _IsLogin);
            bool _IsOtherFriendsPage = false;
            if (!_IsMyFriendsPage)
                _IsOtherFriendsPage = IsOtherFriendsPage(_subdomain);
            if (!_IsMyFriendsPage && !_IsOtherFriendsPage)
            {
                this.Response.Redirect(Constants.MainPageUrl, true);
                return;
            }
			
            bool _friendsMode = true;
            if (this.Request.QueryString["mode"] != null)
            {
                if (this.Request.QueryString["mode"] == "followers")
                    _friendsMode = false;
            }			
			
            ListFriends(_IsLogin, _IsMyFriendsPage, _IsOtherFriendsPage, _subdomain,_friendsMode);
            PageSettings(_IsLogin, _IsMyFriendsPage, _IsOtherFriendsPage, _friendsMode);
        }
        //--------------------------------------------------------------------
        private void ListFriends(bool _IsLogin, bool _IsMyFriendsPage, bool _IsOtherFriendsPage, string _subdomain, bool _friendsMode)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.QueryString["page"]);
            }
            catch { return; }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["ItemNum_friends_or_followers"] == null)
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
            command.Parameters["@PageSize"].Value = 16;

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
				string _mainFormat = "<li><a href=\"[url]m\">[name]</a></li>";
				

                //if(!_IsLogin)    // for private message
					//Login.TagDelete(ref template, "messagee");

                PersianCalendar pc = new PersianCalendar();
				string resultText="";
                while (reader.Read())
                {
					string text = _mainFormat;
					
                    Int64 _FriendBlogID = (Int64)reader["FriendBlogID"];
					
                    //if (_IsLogin)  // for private message
                        //text = text.Replace("[FriendBlogID]", _FriendBlogID.ToString());
						
                    text = text.Replace("[name]", (string)reader["name"]);
					
                    text = text.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
					/*
                    if ((bool)reader["ShowAgeEnabled"])
                        text = text.Replace("[age]", String.Format("{0}", pc.GetYear(DateTime.Now) - (int)reader["BirthYear"]));
                    else
                        Login.TagDelete(ref text, "age");
                    
					if ((bool)reader["ShowCityEnabled"])
                        text = text.Replace("[city]", (string)reader["city"]);
                    else
                        Login.TagDelete(ref text, "city");
						
                    text = text.Replace("[country]", (string)reader["country"]);
					*/
					
					resultText += text;
                }

                reader.Close();

	int _CurrentFriendsNum;
                if (currentPage == 1)
	{
                    if (_friendsMode)
		_CurrentFriendsNum = (int)command.Parameters["@FriendNum"].Value;
                    else
		_CurrentFriendsNum = (int)command.Parameters["@FollowerNum"].Value;
	this.Session["ItemNum_friends_or_followers"] = _CurrentFriendsNum;
	}
	else
	   _CurrentFriendsNum = (int)this.Session["ItemNum_friends_or_followers"];

                command.Dispose();
                connection.Close();				
				
	string pageTemplate = "<div style=\"direction:ltr;font-size:small\">[paging]</div>";
	if (_friendsMode)
		Login.DoPaging(ref pageTemplate, "friends", currentPage, 16, _CurrentFriendsNum);
	else
		Login.DoPaging(ref pageTemplate, "followers", currentPage, Constants.TopFriendsOrFollowersShow, _CurrentFriendsNum);
	resultText += pageTemplate;


              this.resultText.Text = resultText;  
	return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                this.resultText.Text = "موردي پيدا نشد";
                return;
            }
        }
        //--------------------------------------------------------------------
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
        private void PageSettings(bool _IsLogin, bool _IsMyFriendsPage, bool _IsOtherFriendsPage, bool _friendsMode)
        {
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
			string friend_or_follower;
			if(_friendsMode)
				friend_or_follower = "دوستان ";
			else
				friend_or_follower = "دنبال‌كنندگان ";
			
            if (_IsMyFriendsPage)
            {
                SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
				this.title.InnerText = friend_or_follower + _info.Name;
				this.NameLabel.Text  = friend_or_follower + _info.Name;
                //this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                //this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);
                //this.FriendsHyperlink.NavigateUrl = "";
                //this.FollowersHyperlink.NavigateUrl = "";				
            }
            else
            {
				this.title.InnerText = friend_or_follower + (string)this.Session["GuestName"];
				this.NameLabel.Text  = friend_or_follower + (string)this.Session["GuestName"];
                //this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                //this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);
                //this.FriendsHyperlink.NavigateUrl = "";
                //this.FollowersHyperlink.NavigateUrl = "";				
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
   }
}