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

namespace Peyghamak
{
    public partial class rss : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Login.IsLoginProc(this);
            string _subdomain = Login.FindSubdomain(this);
            bool _IsMyCommentsPage = IsMyCommentsPage(_subdomain, _IsLogin);
            bool _IsOtherCommentsPage = false;
            if (!_IsMyCommentsPage)
                _IsOtherCommentsPage = IsOtherCommentsPage(_subdomain);
            if (this.Request.QueryString["mode"] != null)
            {
                RssBurner(_subdomain, _IsMyCommentsPage, _IsOtherCommentsPage);
                return;
            }
            ShowMyPostsRss(_subdomain, _IsMyCommentsPage, _IsOtherCommentsPage);
            return;
        }
        //--------------------------------------------------------------------
        private void ShowMyPostsRss(string _subdomain, bool _IsMyCommentsPage, bool _IsOtherCommentsPage)
        {
            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowMyPostsRss_MyPage_proc";


            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            if (_IsMyCommentsPage)
                command.Parameters["@BlogID"].Value = info.BlogID;
            if (_IsOtherCommentsPage)
                command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@Top", SqlDbType.Int);
            command.Parameters["@Top"].Value = Constants.MaxRssShow;

            //this.Response.AddHeader("Content-Type", "application/rss+xml");
            this.Response.ContentType = "application/rss+xml;charset=utf-8";
            this.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<rss version=\"2.0\">\r\n<channel>\r\n");
            if (_IsMyCommentsPage)
                this.Response.Write(String.Format("<title>{0}</title>\r\n<link>http://{1}/</link>\r\n<description>{0}</description>\r\n<language>fa</language>\r\n<generator>{2}</generator>", info.Name, this.Request.Url.Host, Constants.BlogDomain));
            if (_IsOtherCommentsPage)
                this.Response.Write(String.Format("<title>{0}</title>\r\n<link>http://{1}/</link>\r\n<description>{0}</description>\r\n<language>fa</language>\r\n<generator>{2}</generator>", (string)this.Session["GuestName"], this.Request.Url.Host, Constants.BlogDomain));

            this.Response.Flush();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string item = "\r\n<item>\r\n<comments>[comments]</comments>\r\n<author>[author]</author>\r\n<title>[title]</title>\r\n<pubDate>[pubDate]</pubDate>\r\n<link>[link]</link>\r\n<guid>[link]</guid>\r\n<description>[description]</description>\r\n</item>";
                if (_IsMyCommentsPage)
                    item = item.Replace("[author]", info.Name);
                if (_IsOtherCommentsPage)
                    item = item.Replace("[author]", (string)this.Session["GuestName"]);
                string temp = null;
                while (reader.Read())
                {
                    temp = item;
                    temp = temp.Replace("[comments]", String.Format("http://{0}/comments.aspx?PostID={1}", this.Request.Url.Host, reader["id"]));
                    //-----------
                    string _PostContent = (string)reader["PostContent"];
                    int _index = 0;
                    if (SpaceNumbersCalculator(_PostContent, 7, ref _index) > 7)
                    {
                        temp = temp.Replace("[title]", _PostContent.Substring(0, _index));
                    }
                    else
                        temp = temp.Replace("[title]", _PostContent);
                    //-----------
                    temp = temp.Replace("[pubDate]", ((DateTime)reader["PostDate"]).ToString("r"));
                    temp = temp.Replace("[creator]", _subdomain);
                    temp = temp.Replace("[link]", String.Format("http://{0}/comments.aspx?PostID={1}", this.Request.Url.Host, reader["id"]));
                    temp = temp.Replace("[description]", _PostContent);
                    this.Response.Write(temp);
                    this.Response.Flush();
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Response.Write("\r\n</channel>\r\n</rss>");
                this.Response.Flush();
                this.Response.End();
                return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Response.Write("\r\n</channel>\r\n</rss>");
                this.Response.Flush();
                this.Response.End();
                return;
            }

        }
        //--------------------------------------------------------------------
        private static int SpaceNumbersCalculator(string str, int desiredOccurnce, ref int index)
        {
            int _occurances = 0;
            for(int i = 0 ; i < str.Length ; i++)
            {
                if (str[i] == ' ')
                {
                    _occurances++;
                    if (_occurances == desiredOccurnce)
                        index = i;
                }
            }
            return _occurances;
        }
        //--------------------------------------------------------------------
        private void RssBurner(string _subdomain, bool _IsMyCommentsPage, bool _IsOtherCommentsPage)
        {
            SigninSessionInfo info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowMyPostsRss_MyPage_proc";


            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            if (_IsMyCommentsPage)
                command.Parameters["@BlogID"].Value = info.BlogID;
            if (_IsOtherCommentsPage)
                command.Parameters["@BlogID"].Value = (Int64)this.Session["GuestBlogID"];

            command.Parameters.Add("@Top", SqlDbType.Int);
            command.Parameters["@Top"].Value = Constants.MaxRssShow;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this.Response.Write(string.Format("<a href='http://{0}/' target='_blank'>{1}</a><br>", this.Request.Url.Host, (string)reader["PostContent"]));
                    this.Response.Flush();
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                this.Response.End();
                return;
            }
            else
            {
                this.Response.Flush();
                this.Response.End();
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
                command.CommandText = String.Format("SELECT [id],name,ImageGuid FROM {0} WHERE username='{1}'", Constants.AccountsTableName, _subdomain);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.Session["GuestBlogUsername"] = _subdomain;
                    this.Session["GuestBlogID"] = (Int64)reader["id"];
                    this.Session["GuestName"] = (string)reader["name"];
                    this.Session["GuestImageGuid"] = (string)reader["ImageGuid"];
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
