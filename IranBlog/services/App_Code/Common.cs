/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;
using IranBlog.Classes.Security;
using services.blogbuilderv1;

namespace services
{
    public class Common
    {
        //--------------------------------------------------------------------------------
        public static void AjaxDoPaging(Page page, string paging, string _mode, int currentPage, int _MaxItemShow, int _TotalItems, int _PagingNumber, string _ajaxShowFunctionName)
        {
            string pagingStr = "";
            if (_TotalItems != 0)
            {
                int total = _TotalItems;
                int a = total / _MaxItemShow;
                int b = total % _MaxItemShow;
                int pages = a;
                if (b != 0)
                    pages++;

                if (pages < currentPage || currentPage <= 0)
                    currentPage = 1;

                int n = currentPage / _PagingNumber; //current section
                /*if(pages < PagingNumber)
                    n++;*/
                if (currentPage % _PagingNumber != 0)
                    n++;


                //-------end section--------
                int end;
                int n1 = total / (_MaxItemShow * _PagingNumber); // total paging sections
                int n2 = total % (_MaxItemShow * _PagingNumber);
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
                    end = ((n - 1) * _PagingNumber) + _PagingNumber;
                //end = pages;
                //--------------------------
                string next = null;
                string previous = null;
                if (n == 1 && n1 != 1) //first page
                    next = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">»</a>&nbsp;", _ajaxShowFunctionName, n * _PagingNumber + 1, _mode);
                if (n1 == n && n != 1) //last page
                    previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">«</a>&nbsp;", _ajaxShowFunctionName, (n - 1) * _PagingNumber, _mode);
                else //middle pages
                {
                    if (n != 1 && n1 != n)
                    {
                        next = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">»</a>&nbsp;", _ajaxShowFunctionName, n * _PagingNumber + 1, _mode);
                        previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">«</a>&nbsp;", _ajaxShowFunctionName, (n - 1) * _PagingNumber, _mode);
                    }
                }

                if (previous != null)
                    pagingStr += previous;

                for (int i = ((n - 1) * _PagingNumber) + 1; i <= end; i++)
                {
                    if (i == currentPage)
                        pagingStr += String.Format("<u>{0}</u>&nbsp;", i);
                    else
                        pagingStr += String.Format("<a href=\"javascript:void(0);\" onclick=\"{0}('{1}','{2}');\">{1}</a>&nbsp;", _ajaxShowFunctionName, i, _mode);
                }

                if (next != null)
                    pagingStr += next;
                //pagingStr += "<br>n: ' + n + "<br>n1: "+ n1;
            }
            paging = paging.Replace("[paging]", pagingStr);
            page.Response.Write(paging);
            return;
        }
        //--------------------------------------------------------------------
        public static void TagDelete(ref string buffer, string tagName)
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
                //buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), "");
            }
            return;
        }
        //--------------------------------------------------------------------------------
        public static string GetInternalTagContent(string buffer, string tagName)
        {
            string start = String.Format("<{0}>", tagName);
            string end = String.Format("</{0}>", tagName);

            int _p1 = buffer.IndexOf(start) + start.Length;
            int _p2 = buffer.IndexOf(end);

            if (_p1 >= 0 && _p2 > _p1)
                return buffer.Substring(_p1, _p2 - _p1);
            else
                return null;
        }
        //--------------------------------------------------------------------
        /*public static bool IsLoginProc(Page page)
        {
            bool _IsLogined = false;
            if (page.Session["LoginInfo"] != null)
            {
                LoginInfo _LoginInfo = (LoginInfo)page.Session["LoginInfo"];
                if (_LoginInfo.Username == "")
                    _IsLogined = false;
                else
                    _IsLogined = true;
            }
            else
                _IsLogined = false;
            if (_IsLogined)
                return true;
            else
            {
                if (page.Request.Form["mode"] != null)
                    WriteStringToAjaxRequest("Logouted", page);
                else
                    page.Response.Redirect("/Login.aspx?mode=logouted", true);
                return false;
            }
            return true;
        }*/
        //--------------------------------------------------------------------
        public static bool IsLoginProc(Page page)
        {
            /*if (page.Session["IsLogined"] != null && (bool)page.Session["IsLogined"])
            {
                //---for the future system developments, here must transfer the session to remote IIS Web server
                return true;
            }
            else
            {*/
            if (page.Request.Cookies["userInfo"] != null)
            {
                if (page.Request.Cookies["userInfo"]["info"] != null && page.Request.Cookies["userInfo"]["info"] != null)
                {
                    //---for the future system developments, here must transfer the session to remote IIS Web server
                    string _username = ((SigninSessionInfo)page.Session["SigninSessionInfo"]).Username;
                    if (_username == null || _username == "")
                    {

                        EncryptedCookie ec = new EncryptedCookie(constants.key, constants.IV);
                        string _decryptedInfo = (string)ec.DecryptWithMd5Hash(page.Request.Cookies["userInfo"]["info"]);
                        string[] rets = _decryptedInfo.Split(',');
                        Int64 _AuthorID = Convert.ToInt64(rets[0]);
                        bool _IsInTeamWeblogMode = false;
                        if (rets[1] == "true")
                            _IsInTeamWeblogMode = true;
                        //Int64 _BlogID = (Int64)
                        //page.Response.Write(_BlogID);
                        //return false;
                        SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "LoadSigninSessionInfo_CommonPage_proc";

                        //-------------------------
                        command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
                        command.Parameters["@AuthorID"].Value = _AuthorID;

                        command.Parameters.Add("@IsInTeamWeblogMode", SqlDbType.BigInt);
                        command.Parameters["@IsInTeamWeblogMode"].Value = _IsInTeamWeblogMode;
                        //-------------------------

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            SigninSessionInfo _signinInfo = new SigninSessionInfo();
                            _signinInfo.BlogID = (Int64)reader["BlogID"];
							_signinInfo.AuthorID = _AuthorID;
                            _signinInfo.Username = (string)reader["username"];
                            _signinInfo.Subdomain = (string)reader["subdomain"];
                            _signinInfo.ChatBoxIsEnabled = (bool)reader["ChatBoxIsEnabled"];
                            if (_IsInTeamWeblogMode)
                            {
                                TeamWeblogAccessInfo info = new TeamWeblogAccessInfo();

                                info.PostAccess = (bool)reader["PostAccess"];
                                info.SubjectedArchiveAccess = (bool)reader["SubjectedArchiveAccess"];
                                info.WeblogLinksAccess = (bool)reader["WeblogLinksAccess"];
                                info.DailyLinksAccess = (bool)reader["DailyLinksAccess"];
                                info.TemplateAccess = (bool)reader["TemplateAccess"];
                                info.NewsletterAccess = (bool)reader["NewsletterAccess"];
                                info.OthersPostAccess = (bool)reader["OthersPostAccess"];
                                info.LinkBoxAccess = (bool)reader["LinkBoxAccess"];
                                info.PollAccess = (bool)reader["PollAccess"];
                                info.FullAccess = (bool)reader["FullAccess"];

                                _signinInfo.TeamWeblogAccessInfo = info;
                                _signinInfo.IsInTeamWeblogMode = true;
                                //page.Response.Write(_decryptedInfo);
                            }
                            page.Session["SigninSessionInfo"] = _signinInfo;
                            page.Session["IsLogined"] = true;
                            reader.Close();
                            command.Dispose();
                            connection.Close();
                            /*if ((string)page.Request.Cookies["userInfo"]["SessionMode"] == "true")
                                page.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(Constants.SessionTimeoutMinutes);*/
                            return true;
                        }
                        else
                        {
                            reader.Close();
                            command.Dispose();
                            connection.Close();
                            CookieAbandon(page);
                            return false;
                        }
                    }
                    else
                        return true;
                }
                else
                {
                    CookieAbandon(page);
                    return false;
                }
            }
            else
                return false;
            //}
        }
        //--------------------------------------------------------------------
        public static void CookieAbandon(Page page)
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = page.Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = page.Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                //aCookie.Domain = "." + Constants.BlogDomain;
                aCookie.Domain = constants.DomainBlog;
                page.Response.Cookies.Add(aCookie);
            }
            return;
        }
        //--------------------------------------------------------------------------------
        public static string PersianDate(DateTime dt)
        {
            PersianCalendar pcal = new PersianCalendar();
            string[] PersianMonthNames = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            string[] PersianWeekNames = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            string pdate = PersianWeekNames[Convert.ToInt32(pcal.GetDayOfWeek(dt))];
            pdate += "، " + pcal.GetDayOfMonth(dt).ToString();
            pdate += " " + PersianMonthNames[pcal.GetMonth(dt) - 1];
            pdate += " " + pcal.GetYear(dt);
            pdate += String.Format(" ساعت {0}:{1}", pcal.GetHour(dt), pcal.GetMinute(dt));
            return pdate;
        }
        //--------------------------------------------------------------------------------
        public static void WriteStringToAjaxRequest(string str, Page page)
        {
            page.Response.Write(str);
            page.Response.Flush();
            //this.Response.Close();
            page.Response.End();
        }
        //--------------------------------------------------------------------------------
    }
}
