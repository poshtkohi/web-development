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
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Peyghamak
{
    public class Login
    {
        //--------------------------------------------------------------------
        public static string Build75x75ImageUrl(string guid, bool withRandom)
        {
            if (guid == "")
                return String.Format("http://www.{0}/images/users/full/default.jpg", Constants.BlogDomain);
            string[] rets = guid.Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
            if (withRandom)
                return String.Format("http://{0}.{1}/images/users/full/{2}/{3}.jpg?i={4}", rets[0], Constants.BlogDomain, rets[1], rets[2], new Random().Next());
            else
                return String.Format("http://{0}.{1}/images/users/full/{2}/{3}.jpg", rets[0], Constants.BlogDomain, rets[1], rets[2]);
        }
        //--------------------------------------------------------------------
        public static string Build40x40ImageUrl(string guid, bool withRandom)
        {
            if (guid == "")
                return String.Format("http://www.{0}/images/users/40x40/default.jpg", Constants.BlogDomain);
            string[] rets = guid.Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
            if (withRandom)
                return String.Format("http://{0}.{1}/images/users/40x40/{2}/{3}.jpg?i={4}", rets[0], Constants.BlogDomain, rets[1], rets[2], new Random().Next());
            else
                return String.Format("http://{0}.{1}/images/users/40x40/{2}/{3}.jpg", rets[0], Constants.BlogDomain, rets[1], rets[2]);
        }
        //--------------------------------------------------------------------
        public static string Build38x38ImageUrl(string guid, bool withRandom)
        {
            if (guid == "")
                return String.Format("http://www.{0}/images/users/38x38/default.jpg", Constants.BlogDomain);
            string[] rets = guid.Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
            if (withRandom)
                return String.Format("http://{0}.{1}/images/users/38x38/{2}/{3}.jpg?i={4}", rets[0], Constants.BlogDomain, rets[1], rets[2], new Random().Next());
            else
                return String.Format("http://{0}.{1}/images/users/38x38/{2}/{3}.jpg", rets[0], Constants.BlogDomain, rets[1], rets[2]);
        }
        //--------------------------------------------------------------------
        public static string BuildUserUrl(string username)
        {
            return String.Format("http://{0}.{1}/", username, Constants.BlogDomain);
        }
        //--------------------------------------------------------------------
        public static void PagingFormat(Page page, string paging, int currentPage, int _PostNum)
        {
            baShowUpDoPaging(ref paging, page, currentPage, _PostNum);
            page.Response.Write(paging);
            return;
            //this.Response.Flush();
        }
        //--------------------------------------------------------------------
        /*public static void LastTimeFormat(ref string buffer, DateTime _PostDate)
        {
            TimeSpan elpased = DateTime.Now - _PostDate;
            if (elpased.Days == 0)
            {
                if (elpased.Hours == 0)
                {
                    if (elpased.Minutes == 0)
                    {
                        buffer = buffer.Replace("[LastTime]", String.Format("در {0} ثانیه پیش", elpased.Seconds));
                        return;
                    }
                    else
                    {
                        buffer = buffer.Replace("[LastTime]", String.Format("در {0} دقیقه پیش", elpased.Minutes));
                        return;
                    }
                }
                else
                {
                    buffer = buffer.Replace("[LastTime]", String.Format("در {0} ساعت پیش", elpased.Hours));
                    return;
                }
            }
            else
            {
                buffer = buffer.Replace("[LastTime]", String.Format("در {0} روز پیش", elpased.Days));
                return;
            }*/
            /*TimeSpan elpased = DateTime.Now - _PostDate;
            if (elpased.Minutes == 0)
            {
                buffer = buffer.Replace("[LastTime]", String.Format("در {0} ثانیه پیش", elpased.Seconds));
                return;
            }
            if (elpased.Hours == 0)
            {
                buffer = buffer.Replace("[LastTime]", String.Format("در {0} دقیقه پیش", elpased.Minutes));
                return;
            }
            if (elpased.Days == 0)
            {
                buffer = buffer.Replace("[LastTime]", String.Format("در {0} ساعت پیش", elpased.Hours));
                return;
            }
            else
            {
                buffer = buffer.Replace("[LastTime]", String.Format("در {0} روز پیش", elpased.Days));
                return;
            }*/
        //}
        public static void LastTimeFormat(ref string buffer, DateTime _PostDate)
        {
            TimeSpan elpased = DateTime.Now - _PostDate;
            if (elpased.Days == 0)
            {
                if (elpased.Hours == 0)
                {
                    if (elpased.Minutes == 0)
                    {
                        buffer = buffer.Replace("[LastTime]", String.Format("در {0} ثانیه پیش", elpased.Seconds));
                    }
                    else
                    {
                        buffer = buffer.Replace("[LastTime]", String.Format("در {0} دقیقه پیش", elpased.Minutes));
                    }
                }
                else
                {
                    buffer = buffer.Replace("[LastTime]", String.Format("در {0} ساعت پیش", elpased.Hours));
                }
            }
            else
            {
                if (elpased.Days <= 365) // less than one year
                {
                    uint m = (uint)(elpased.Days / 30);
                    uint d = (uint)(elpased.Days % 30);
                    if (m == 0) // less than one month
                        buffer = buffer.Replace("[LastTime]", String.Format("در {0} روز پیش", elpased.Days));
                    else  // more than one month
                    {
                        uint w = d / 7;
                        if (w == 0) // the first week of the month
                            buffer = buffer.Replace("[LastTime]", String.Format("در {0} ماه پیش", m));
                        else
                            buffer = buffer.Replace("[LastTime]", String.Format("در {0} ماه و {1} هفته پیش", m, w));
                    }
                }
                else // more than one year
                {
                    uint y = (uint)(elpased.Days / 365);
                    uint d = (uint)(elpased.Days % 365);
                    uint m = d / 30;
                    if (m == 0) // the first month of the year
                        buffer = buffer.Replace("[LastTime]", String.Format("در {0} سال پیش", y));
                    else
                        buffer = buffer.Replace("[LastTime]", String.Format("در {0} سال و {1} ماه پیش", y, m));
                }
            }
        }
        //--------------------------------------------------------------------
        public static void PostDateFormat(ref string buffer, DateTime _PostDate, bool DoDelete)
        {
            if (DoDelete)
            {
                TagDelete(ref buffer, "date");
                return;
            }
            else
            {
                int p1 = buffer.IndexOf("<date>") + "<date>".Length;
                int p2 = buffer.IndexOf("</date>");
                if (p1 >= 0 && p2 >= 0)
                {
                    string buff = buffer.Substring(p1, p2 - p1);
                    string temp = "";
                    temp = buff.Replace("[date]", PersianDate(_PostDate));
                    p1 -= "<date>".Length;
                    p2 += "</date>".Length;
                    buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), temp);
                }
            }
        }
        //--------------------------------------------------------------------
        public static void SmiliesFormat(ref string buffer)
        {
            buffer = System.Web.HttpContext.Current.Server.HtmlDecode(buffer);
            buffer = buffer.Replace(">:)", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/6.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace("o:)", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/1.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace("X(", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/2.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(@":/)", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/3.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":((", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/5.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":(", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/4.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":-(", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/4.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":))", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/7.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":)", String.Format("<img dir=\"ltr\" title=':)' align=\"texttop\" src=\"http://www.{0}/images/smilies/8.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":-)", String.Format("<img dir=\"ltr\" title=':)' align=\"texttop\" src=\"http://www.{0}/images/smilies/8.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":x", String.Format("<img dir=\"ltr\" title=':x' align=\"texttop\" src=\"http://www.{0}/images/smilies/9.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":X", String.Format("<img dir=\"ltr\" title=':x' align=\"texttop\" src=\"http://www.{0}/images/smilies/9.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":|", String.Format("<img dir=\"ltr\" title=':|' align=\"texttop\" src=\"http://www.{0}/images/smilies/10.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":p", String.Format("<img dir=\"ltr\" title=':p' align=\"texttop\" src=\"http://www.{0}/images/smilies/11.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":P", String.Format("<img dir=\"ltr\" title=':p' align=\"texttop\" src=\"http://www.{0}/images/smilies/11.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":پی", String.Format("<img dir=\"ltr\" title=':p' align=\"texttop\" src=\"http://www.{0}/images/smilies/11.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":d", String.Format("<img dir=\"ltr\" title=':d' align=\"texttop\" src=\"http://www.{0}/images/smilies/12.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":D", String.Format("<img dir=\"ltr\" title=':d' align=\"texttop\" src=\"http://www.{0}/images/smilies/12.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":دی", String.Format("<img dir=\"ltr\" title=':d' align=\"texttop\" src=\"http://www.{0}/images/smilies/12.png\" />", Constants.BlogDomain));

            buffer = buffer.Replace(":*", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/13.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(":*", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/13.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(";)", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/14.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(";-)", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/14.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(";>", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/15.png\" />", Constants.BlogDomain));
            buffer = buffer.Replace(";->", String.Format("<img dir=\"ltr\" title='' align=\"texttop\" src=\"http://www.{0}/images/smilies/15.png\" />", Constants.BlogDomain));
            //buffer = System.Web.HttpContext.Current.Server.HtmlEncode(buffer);

            return;
        }
        //--------------------------------------------------------------------
        public static void CommentsFormat(ref string buffer, int _NumComments, bool CommentEnabled)
        {
            if (!CommentEnabled)
            {
                TagDelete(ref buffer, "comments");
                return;
            }
            else
            {
                int p1 = buffer.IndexOf("<comments>") + "<comments>".Length;
                int p2 = buffer.IndexOf("</comments>");
                if (p1 >= 0 && p2 >= 0)
                {
                    string buff = buffer.Substring(p1, p2 - p1);
                    string temp = "";
                    temp = buff.Replace("[NumComments]", _NumComments.ToString());
                    p1 -= "<comments>".Length;
                    p2 += "</comments>".Length;
                    buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), temp);
                }
            }
        }
        //--------------------------------------------------------------------
        public static void PostContentFormat(ref string buffer, string _PostContent, bool _PostAlign, PostType _PostType, Int64 id)
        {
            _PostContent = _PostContent.Replace("\n", "<br>");
            _PostContent = _PostContent.Replace("  ", " &nbsp;");
            buffer = buffer.Replace("[id]", id.ToString());
            if (_PostAlign) //right
            {
                buffer = buffer.Replace("[align]", "right");
                buffer = buffer.Replace("[dir]", "rtl");
            }
            else
            {
                buffer = buffer.Replace("[align]", "left");
                buffer = buffer.Replace("[dir]", "ltr");
            }
            switch (_PostType)
            {
                case PostType.FromWeb:
                    buffer = buffer.Replace("[from]", "وب");
                    break;
                case PostType.FromMobile:
                    buffer = buffer.Replace("[from]", "موبایل");
                    break;
                case PostType.FromMessenger:
                    buffer = buffer.Replace("[from]", "مسنجر");
                    break;
                default:
                    buffer = buffer.Replace("[from]", "وب");
                    break;
            }
            buffer = buffer.Replace("[PostContent]", DoHyperlink(_PostContent));
            SmiliesFormat(ref buffer);
            return;
        }
        //--------------------------------------------------------------------
        private static string DoHyperlink(string s)
        {
            Regex urlregex = new Regex(@"(http:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            s = urlregex.Replace(s, "<a href=\"$1\" target=\"_blank\">$1</a>");
            return s;
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
        //--------------------------------------------------------------------
        public static string PersianDate(DateTime dt)
        {
            PersianCalendar pcal = new PersianCalendar();
            string[] PersianMonthNames = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            string[] PersianWeekNames = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            string pdate = PersianWeekNames[Convert.ToInt32(pcal.GetDayOfWeek(dt))];
            pdate += "، " + pcal.GetDayOfMonth(dt).ToString();
            pdate += " " + PersianMonthNames[pcal.GetMonth(dt) - 1];
            pdate += " " + pcal.GetYear(dt);
            return pdate;
        }
        //--------------------------------------------------------------------------------
        public static void DoPaging(ref string buffer, string _mode, int currentPage, int _MaxItemShow, int _TotalItems)
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

                int n = currentPage / Constants.PagingNumber; //current section
                /*if(pages < PagingNumber)
                    n++;*/
                if (currentPage % Constants.PagingNumber != 0)
                    n++;


                //-------end section--------
                int end;
                int n1 = total / (_MaxItemShow * Constants.PagingNumber); // total paging sections
                int n2 = total % (_MaxItemShow * Constants.PagingNumber);
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
                    end = ((n - 1) * Constants.PagingNumber) + Constants.PagingNumber;
                //end = pages;
                //--------------------------
                string next = null;
                string previous = null;
                if (n == 1 && n1 != 1) //first page
                    next = String.Format("<a href=\"?page={0}&mode={1}\">»</a>&nbsp;", n * Constants.PagingNumber + 1, _mode);
                if (n1 == n && n != 1) //last page
                    previous = String.Format("<a href=\"?page={0}&mode={1}\">«</a>&nbsp;", (n - 1) * Constants.PagingNumber, _mode);
                else //middle pages
                {
                    if (n != 1 && n1 != n)
                    {
                        next = String.Format("<a href=\"?page={0}&mode={1}\">»</a>&nbsp;", n * Constants.PagingNumber + 1, _mode);
                        previous = String.Format("<a href=\"?page={0}&mode={1}\">«</a>&nbsp;", (n - 1) * Constants.PagingNumber, _mode);
                    }
                }

                if (previous != null)
                    pagingStr += previous;

                for (int i = ((n - 1) * Constants.PagingNumber) + 1; i <= end; i++)
                {
                    if (i == currentPage)
                        pagingStr += String.Format("<u>{0}</u>&nbsp;", i);
                    else
                        pagingStr += String.Format("<a href=\"?page={0}&mode={1}\">{0}</a>&nbsp;", i, _mode);
                }

                if (next != null)
                    pagingStr += next;
                //pagingStr += "<br>n: ' + n + "<br>n1: "+ n1;
            }
            buffer = buffer.Replace("[paging]", pagingStr);
        }
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
        //--------------------------------------------------------------------------------
        /// <summary>
        /// AjaxDoPaging with Ajax Function name.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="paging"></param>
        /// <param name="_mode"></param>
        /// <param name="currentPage"></param>
        /// <param name="_MaxItemShow"></param>
        /// <param name="_TotalItems"></param>
        /// <param name="_PagingNumber"></param>
        /// <param name="_ajaxShowFunctionName"></param>
        public static void AjaxDoPagingEx(Page page, string paging, string _mode, int currentPage, int _MaxItemShow, int _TotalItems, int _PagingNumber, string _ajaxShowFunctionName)
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
        //--------------------------------------------------------------------------------
        private static void baShowUpDoPaging(ref string buffer, Page page, int currentPage, int PostNum)
        {
            string _ShowMode = page.Request.Form["ShowMode"];
            if (_ShowMode == "comments")
                _ShowMode += String.Format("&PostID={0}", page.Request.Form["PostID"]);
            string pagingStr = "";
            if (PostNum != 0)
            {
                int total = PostNum;
                int a = total / Constants.MaxPostShow;
                int b = total % Constants.MaxPostShow;
                int pages = a;
                if (b != 0)
                    pages++;

                if (pages < currentPage || currentPage <= 0)
                    currentPage = 1;

                int n = currentPage / Constants.PagingNumber; //current section
                /*if(pages < PagingNumber)
                    n++;*/
                if (currentPage % Constants.PagingNumber != 0)
                    n++;


                //-------end section--------
                int end;
                int n1 = total / (Constants.MaxPostShow * Constants.PagingNumber); // total paging sections
                int n2 = total % (Constants.MaxPostShow * Constants.PagingNumber);
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
                    end = ((n - 1) * Constants.PagingNumber) + Constants.PagingNumber;
                //end = pages;
                //--------------------------
                string next = null;
                string previous = null;
                if (n == 1 && n1 != 1) //first page
                    next = String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"baShowUp('{0}','{1}', 'loaderImg', 'pContainer', 'resultText');\">»</a>", _ShowMode, n * Constants.PagingNumber + 1);
                if (n1 == n && n != 1) //last page
                    previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"baShowUp('{0}','{1}', 'loaderImg', 'pContainer', 'resultText');\">«</a>", _ShowMode, (n - 1) * Constants.PagingNumber);
                else //middle pages
                {
                    if (n != 1 && n1 != n)
                    {
                        next = String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"baShowUp('{0}','{1}', 'loaderImg', 'pContainer', 'resultText');\">»</a>", _ShowMode, n * Constants.PagingNumber + 1);
                        previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"baShowUp('{0}','{1}', 'loaderImg', 'pContainer', 'resultText');\">«</a>", _ShowMode, (n - 1) * Constants.PagingNumber);
                    }
                }

                if (previous != null)
                    pagingStr += previous;

                for (int i = ((n - 1) * Constants.PagingNumber) + 1; i <= end; i++)
                {
                    if (i == currentPage)
                        pagingStr += String.Format("<u> {0}</u>", i);
                    else
                        pagingStr += String.Format("<a href=\"javascript:void(0);\" onclick=\"baShowUp('{0}','{1}', 'loaderImg', 'pContainer', 'resultText');\"> {1}</a>", _ShowMode, i);
                }

                if (next != null)
                    pagingStr += next;
                //pagingStr += "<br>n: ' + n + "<br>n1: "+ n1;
            }
            buffer = buffer.Replace("[paging]", pagingStr);
        }
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
                    if (page.Request.Cookies["userInfo"]["BlogID"] != null)
                    {
                        //---for the future system developments, here must transfer the session to remote IIS Web server
                        string _username = ((SigninSessionInfo)page.Session["SigninSessionInfo"]).Username;
                        if (_username == null || _username == "" || _username != Login.FindSubdomain(page))
                        {
                            EncryptedCookie ec = new EncryptedCookie(Constants.RijndaelKey, Constants.RijndaelIV);
                            Int64 _BlogID = (Int64)ec.DecryptWithMd5Hash(page.Request.Cookies["userInfo"]["BlogID"]);
                            //page.Response.Write(_BlogID);
                            //return false;
                            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            command.Connection = connection;
                            command.CommandText = String.Format("SELECT TOP 1 [username],[name],ImageGuid,(SELECT ThemeString FROM themes WHERE themes.[id]=accounts.ThemeID) AS ThemeString FROM {0} WHERE id={1} AND IsDeleted=0", Constants.AccountsTableName, _BlogID);
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                SigninSessionInfo _signinInfo = new SigninSessionInfo();
                                _signinInfo.BlogID = _BlogID;
                                _signinInfo.Username = (string)reader["username"];
                                _signinInfo.Name = (string)reader["name"];
                                _signinInfo.ImageGuid = (string)reader["ImageGuid"];
                                _signinInfo.ThemeString = (string)reader["ThemeString"];
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
                aCookie.Domain = Constants.BlogDomain;
                page.Response.Cookies.Add(aCookie);
            }
            return;
        }
        //--------------------------------------------------------------------
        public static bool IsMyCp(Page page)
        {
            string _subdomain = Login.FindSubdomain(page);
            if (_subdomain == "")
            {
                page.Response.Redirect(Constants.MainPageUrl, true);
                return false;
            }
            if (Login.IsLoginProc(page))
            {
                SigninSessionInfo _info = (SigninSessionInfo)page.Session["SigninSessionInfo"];
                if (_info.Username == _subdomain)
                    return true;
                else
                {
                    page.Response.Redirect(Login.BuildUserUrl(_info.Username) + "cp/", true);
                    return false;
                }
            }
            else
            {
                page.Response.Redirect("/", true);
                return false;
            }
        }
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        public static string FindSubdomain(Page page)
        {
            string[] parts = page.Request.Url.Host.Split(new char[] { '.' });
            if (parts.Length == 3)
            {
                if (parts[0] != "www")
                    return parts[0];
                else
                    return "";
            }
            if (parts.Length == 4)
                return parts[1];
            if (parts.Length == 1 || parts.Length == 2)
                return "";
            else
                return "";
        }
        //--------------------------------------------------------------------
    }
}