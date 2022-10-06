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

using System.IO;

using bookstore.Enums;

namespace bookstore
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
            int p2 = buffer.IndexOf(end, p1);
            if (p1 >= 0 && p2 > p1)
            {
                p1 -= start.Length;
                p2 += end.Length;
                buffer = buffer.Remove(p1, p2 - p1);
                //buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), "");
            }
            return;
        }
        //--------------------------------------------------------------------
        public static string CategoriesGenerator(string categories, Language language)
        {
            string[] _cats = categories.Split(new char[]{'|'});
            //throw new Exception(_cats.Length.ToString());
            string temp = "";
            if (_cats != null)
            {
                string[] _sub_cats = null;
                for (int i = 0; i < _cats.Length; i++)
                {
                    if (_cats[i].Trim() != "")
                    {
                        _sub_cats = _cats[i].Split('*');
                        if (_sub_cats != null)
                        {
                            for (int j = 0; j < _sub_cats.Length; j++)
                            {
                                if (_sub_cats.Length == 1 || j == _sub_cats.Length - 1)
                                    temp += String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"_listBooksByCategory='{0}';_listBooksByCategoryLanguage={1};ShowItems('1','ListBooksByCategory');\">{0}</a>", _sub_cats[j], (int)language);
                                else
                                    temp += String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"_listBooksByCategory='{0}';_listBooksByCategoryLanguage={1};ShowItems('1','ListBooksByCategory');\">{0}</a> > ", _sub_cats[j], (int)language);
                            }
                        }
                        if (_cats.Length != 1 && i != _cats.Length - 1)
                            temp += "<br>";
                    }
                }
            }
            return temp;
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
        public static bool IsLoginProc(Page page)
        {
            if (page.Session["AdminUsername"] != null)
            {
                FileInfo _info = new FileInfo(page.Request.PhysicalPath);
                if ((string)page.Session["AdminUsername"] == constants.AdminUsername && constants.AdminPages.Contains(_info.Name))
                    return true;
                if ((string)page.Session["AdminUsername"] != constants.AdminUsername && constants.AdminPages.Contains(_info.Name))
                    return false;
                else
                    goto Continue;
            }
        Continue:
            if (page.Session["username"] != null)
            {
                if ((string)page.Session["username"] != "")
                    return true;
                else
                    return false;
            }
            else
                return false;
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
        public static void AjaxMultiLanguageReport(string persianReport, string englishReport, Page page, bool _SiteLanguageIsPersian)
        {
            if (_SiteLanguageIsPersian)
                WriteStringToAjaxRequest(persianReport, page);
            else
                WriteStringToAjaxRequest(englishReport, page);
            return;
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
