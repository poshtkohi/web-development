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
    public partial class guest : System.Web.UI.Page
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _CheckGuestBlogID = CheckGuestBlogID();
            bool _Islogined = Login.IsLoginProc(this);
			
            if (!_CheckGuestBlogID)
            {
                if (!_Islogined)
                    this.Response.Redirect(String.Format("{0}/m",Constants.MainPageUrl), true);
                else
                {
                    SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                    this.Response.Redirect(String.Format("http://{0}.{1}/m/my.aspx", info.Username, Constants.BlogDomain), true);
                }
                return;
            }
			
			PageSetting(_CheckGuestBlogID, _Islogined);
			ShowGuestPosts();
			
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
        }
        //--------------------------------------------------------------------
		private void PageSetting(bool _CheckGuestBlogID, bool _Islogined)
		{
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
                this.title.InnerText = "پيغامك " + _name;                

                //this.AboutLabel.Text = (string)reader["about"];
                //this.UrlHyperlink.Text = (string)reader["url"];
				
				string _country = (string)reader["country"];
                string _age = "";
				PersianCalendar pc = new PersianCalendar();
                if ((bool)reader["ShowAgeEnabled"])
                    _age = String.Format("{0} ساله ", (pc.GetYear(DateTime.Now) - (int)reader["BirthYear"]).ToString());
                string _city = "";
                if ((bool)reader["ShowCityEnabled"])
                {
                    if (reader["city"] != DBNull.Value)
                        _city = String.Format("-{0}", (string)reader["city"]);
                }
                this.NameLabel.Text = _name + " ";
                this.NameLabel.Text += string.Format("{0} از {1}{2}", _age, _country, _city);
				
                reader.Close();
                command.Dispose();
                connection.Close();
				
                //-------Friend section: add friend and view the guest page friends and also private message ----
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
					
					// Private Message section, must be added later
					/*
                    //this._idMy.Value = command.Parameters["@_idMy"].Value.ToString();
                    //this._idHe.Value = command.Parameters["@_idHe"].Value.ToString();
*/
                    if ((bool)command.Parameters["@_IsFollower"].Value)
                    {
						string pm = " <a href=\"message.aspx?PersonID=[PersonID]\">ارسال پيغام خصوصي</a>";
						Int64 _BlogID = (Int64)this.Session["GuestBlogID"];
						pm = pm.Replace("[PersonID]", Convert.ToString(_BlogID));
						this.NameLabel.Text += pm;
                    }					
					
                }
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Response.Redirect(String.Format("{0}/m",Constants.MainPageUrl), true);
            }
		}
		//--------------------------------------------------------------------
        private void ShowGuestPosts()
        {
			Int64 _BlogID = (Int64)this.Session["GuestBlogID"];
			
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
            command.Parameters["@BlogID"].Value = _BlogID;

            Int64 _MyBlogID = _BlogID;
            if (Login.IsLoginProc(this))
                _MyBlogID = ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID;

            command.Parameters.Add("@MyBlogID", SqlDbType.BigInt);
            command.Parameters["@MyBlogID"].Value = _MyBlogID;

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

	//_mainFormat = _mainFormat.Replace("[name]", "<a href=\"[url]m\">[name]</a>");					

                Login.TagDelete(ref _mainFormat, "delete");
				
                _mainFormat = _mainFormat.Replace("<comments>", "");
                _mainFormat = _mainFormat.Replace("</comments>", "");
				
                _mainFormat = _mainFormat.Replace("[urlc]", "comments.aspx");				
                _mainFormat = _mainFormat.Replace("[name]", (string)this.Session["GuestName"]);
	//_mainFormat = _mainFormat.Replace("[url]", Login.BuildUserUrl((string)this.Session["GuestName"]));

                string resultText="";
                while (reader.Read())
                {
                    string post = _mainFormat;

                    DateTime dtCurrent = (DateTime)reader["PostDate"];
                    Login.LastTimeFormat(ref post, dtCurrent);

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
                    post = post.Replace("[id]", _id.ToString());

                    int _NumComments = (int)reader["NumComments"];
                    post = post.Replace("[NumComments]", _NumComments.ToString());

                    resultText += post;				
                }

                reader.Close();
                reader.Dispose();

				int _CurrentPostNum = (int)command.Parameters["@PostNum"].Value;
				string pageTemplate = "<div style=\"direction:ltr;font-size:small\">[paging]</div>";
				Login.DoPaging(ref pageTemplate, "ShowGuestPosts", currentPage, 8, _CurrentPostNum);
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
    }
}