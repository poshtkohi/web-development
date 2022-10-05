/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using AlirezaPoshtkoohiLibrary;
using System.IO;
using System.Data.SqlClient;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class pages : System.Web.UI.Page
    {
        //------------------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        //------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Common.IsLoginProc(this);
            if (!_IsLogin)
            {
                if (this.Request.Form["mode"] != null)
                    Common.WriteStringToAjaxRequest("Logouted", this);
                else
                    this.Response.Redirect("Logouted.aspx", true);
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }

            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "show":
                        ShowPages(_SigninSessionInfo);
                        return;
                    case "delete":
                        PageDelete(_SigninSessionInfo);
                        return;
                    case "load":
                        PageLoad(_SigninSessionInfo);
                        return;
                    case "update":
                        PageUpdate(_SigninSessionInfo);
                        return;
                    case "post":
                        if (this.Request.Form["PostContent"] != null)
                        {
                            DoPost(_SigninSessionInfo);
                            return;
                        }
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void DoPost(SigninSessionInfo _SigninSessionInfo)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringPagesDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DoPost_PagesPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter ThemeContentParam = new SqlParameter("@ThemeContent", SqlDbType.NText);
            SqlParameter PostContentParam = new SqlParameter("@PostContent", SqlDbType.NText);
            SqlParameter TitleParam = new SqlParameter("@title", SqlDbType.NVarChar);
            //------Linked Server settings---------------
            /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
            //-------------------------------------------

            BlogIDParam.Value = _SigninSessionInfo.BlogID;
            ThemeContentParam.Value = this.Request.Form["ThemeContent"];//System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.Form["ThemeContent"]));
            PostContentParam.Value = this.Request.Form["PostContent"];//System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.Form["PostContent"]));
            TitleParam.Value = this.Request.Form["title"];//System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.Form["title"]));

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(ThemeContentParam);
            command.Parameters.Add(PostContentParam);
            command.Parameters.Add(TitleParam);


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".صفحه جدید با موفقیت ایجاد شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowPages(SigninSessionInfo _SigninSessionInfo)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["Pages_Num"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringPagesDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowPages_PagesPage_proc";


            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxChatBoxMessageShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PagesNum", SqlDbType.Int);
            command.Parameters["@PagesNum"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            //command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            //command.Parameters["@IsPosts1DbLinkedServer"].Value = constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_pages"] == null)
                {
                    StreamReader sr = new StreamReader(constants.RootPath + @"\services\blogbuilderv1\AjaxTemplates\PagesTemplate.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_pages"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_pages"];


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

                while (reader.Read())
                {
                    temp = _mainFormat;
                    //------
                    temp = temp.Replace("[title]", (string)reader["title"]);
                    temp = temp.Replace("[id]", reader["id"].ToString());
                    temp = temp.Replace("[username]", _SigninSessionInfo.Subdomain);
                    //------
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                if (currentPage == 1)
                    this.Session["Pages_Num"] = (int)command.Parameters["@PagesNum"].Value;

                command.Dispose();
                connection.Close();

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["Pages_Num"]);
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
        //--------------------------------------------------------------------------------
        private static void PagingFormat(Page page, string paging, int currentPage, int _PostNum)
        {
            DoPaging(ref paging, page, currentPage, _PostNum);
            page.Response.Write(paging);
            return;
            //this.Response.Flush();
        }
        //--------------------------------------------------------------------------------
        private void PageDelete(SigninSessionInfo _SigninSessionInfo)
        {
            Int64 _DeleteID = -1;
            try
            {
                _DeleteID = Convert.ToInt64(this.Request.Form["DeleteID"]);
            }
            catch { return; }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringPagesDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageDelete_PagesPage_proc";

            command.Parameters.Add("@PageID", SqlDbType.BigInt);
            command.Parameters["@PageID"].Value = _DeleteID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            //------Linked Server settings---------------
            //command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            //command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------------------
        private void PageLoad(SigninSessionInfo _SigninSessionInfo)
        {
            Int64 _PageID = -1;
            try
            {
                _PageID = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch { return; }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringPagesDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageLoad_PagesPage_proc";

            command.Parameters.Add("@PageID", SqlDbType.BigInt);
            command.Parameters["@PageID"].Value = _PageID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            //------Linked Server settings---------------
            //command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            //command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //string str = "title,ThemeContent,PostContent";
                WriteStringToAjaxRequest(String.Format("{0},{1},{2}", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["title"].ToString())),
                            System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["ThemeContent"].ToString())),
                            System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["PostContent"].ToString()))));

                reader.Close();
                reader.Dispose();
                command.Dispose();
                connection.Close();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                command.Dispose();
                connection.Close();
                return;
            }
        }
        //--------------------------------------------------------------------------------
        private void PageUpdate(SigninSessionInfo _SigninSessionInfo)
        {
            Int64 _PageID = -1;
            try
            {
                _PageID = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch { return; }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringPagesDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageUpdate_PagesPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter PageIDParam = new SqlParameter("@PageID", SqlDbType.BigInt);
            SqlParameter ThemeContentParam = new SqlParameter("@ThemeContent", SqlDbType.NText);
            SqlParameter PostContentParam = new SqlParameter("@PostContent", SqlDbType.NText);
            SqlParameter TitleParam = new SqlParameter("@title", SqlDbType.NVarChar);
            //------Linked Server settings---------------
            /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
            //-------------------------------------------

            BlogIDParam.Value = _SigninSessionInfo.BlogID;
            PageIDParam.Value = _PageID;
            ThemeContentParam.Value = this.Request.Form["ThemeContent"];
            PostContentParam.Value = this.Request.Form["PostContent"];
            TitleParam.Value = this.Request.Form["title"];

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(PageIDParam);
            command.Parameters.Add(ThemeContentParam);
            command.Parameters.Add(PostContentParam);
            command.Parameters.Add(TitleParam);


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".صفحه انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private static void DoPaging(ref string buffer, Page page, int currentPage, int PostNum)
        {
            string pagingStr = "";
            if (PostNum != 0)
            {
                int total = PostNum;
                int a = total / constants.MaxChatBoxMessageShow;
                int b = total % constants.MaxChatBoxMessageShow;
                int pages = a;
                if (b != 0)
                    pages++;

                if (pages < currentPage || currentPage <= 0)
                    currentPage = 1;

                int n = currentPage / constants.ChatBoxPagingNumber; //current section
                /*if(pages < ChatBoxPagingNumber)
                    n++;*/
                if (currentPage % constants.ChatBoxPagingNumber != 0)
                    n++;


                //-------end section--------
                int end;
                int n1 = total / (constants.MaxChatBoxMessageShow * constants.ChatBoxPagingNumber); // total paging sections
                int n2 = total % (constants.MaxChatBoxMessageShow * constants.ChatBoxPagingNumber);
                if (n1 == 0)
                    n1++;
                else
                {
                    if (n2 != 0)
                        n1++;
                }

                //if(n1 == n && n1 != 1)
                if (n1 == n)
                    end = pages;
                else
                    end = ((n - 1) * constants.ChatBoxPagingNumber) + constants.ChatBoxPagingNumber;
                //end = pages;
                //--------------------------
                string next = null;
                string previous = null;
                if (n == 1 && n1 != 1) //first page
                    next = String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"ShowPages('{0}');\">»</a>", n * constants.ChatBoxPagingNumber + 1);
                if (n1 == n && n != 1) //last page
                    previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"ShowPages('{0}');\">«</a>", (n - 1) * constants.ChatBoxPagingNumber);
                else //middle pages
                {
                    if (n != 1 && n1 != n)
                    {
                        next = String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"ShowPages('{0}');\">»</a>", n * constants.ChatBoxPagingNumber + 1);
                        previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"ShowPages('{0}');\">«</a>", (n - 1) * constants.ChatBoxPagingNumber);
                    }
                }

                if (previous != null)
                    pagingStr += previous;

                for (int i = ((n - 1) * constants.ChatBoxPagingNumber) + 1; i <= end; i++)
                {
                    if (i == currentPage)
                        pagingStr += String.Format("<u> {0}</u>", i);
                    else
                        pagingStr += String.Format("<a href=\"javascript:void(0);\" onclick=\"ShowPages('{0}');\"> {0}</a>", i);
                }

                if (next != null)
                    pagingStr += next;
                //pagingStr += "<br>n: ' + n + "<br>n1: "+ n1;
            }
            buffer = buffer.Replace("[paging]", pagingStr);
        }
        //--------------------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------------------
        private static void TagDelete(ref string buffer, string tagName)
        {
            string start = String.Format("<{0}>", tagName);
            string end = String.Format("</{0}>", tagName);
            int p1 = buffer.IndexOf(start) + start.Length;
            int p2 = buffer.IndexOf(end);
            if (p1 >= 0 && p2 >= 0)
            {
                p1 -= start.Length;
                p2 += end.Length;
                buffer = buffer.Remove(p1, p2 - p1);
            }
            return;
        }
        //--------------------------------------------------------------------------------
    }
}
