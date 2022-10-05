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

using System.Text.RegularExpressions;
using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;
using System.IO;
using IranBlog.Classes.Security;

namespace services.ChatBox
{
    public partial class Show : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 _BlogID = -1;
            try
            {
                _BlogID = Convert.ToInt64(this.Request.Form["BlogID"]);
            }
            catch { }
            if (_BlogID <= 0)
            {
                try
                {
                    _BlogID = Convert.ToInt64(this.Request.QueryString["BlogID"]);
                }
                catch { return; }
            }
            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "show":
                        ShowMessages(_BlogID);
                        return;
                    case "delete":
                        MessageDelete(_BlogID);
                        return;
                    default:
                        return;
                }
            }
            if (this.Request.Form["PostContent"] != null && this.Request.Form["PostContent"] != "")
            {
                DoPost(_BlogID);
                return;
            }
            if (HasLoginProc(this, _BlogID))
                this.Postbox.Visible = false;
        }
        //--------------------------------------------------------------------------------
        private void DoPost(Int64 _BlogID)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringChatBoxDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DoPost_ChatBox_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter PostNameParam = new SqlParameter("@name", SqlDbType.NVarChar);
            SqlParameter PostEmailParam = new SqlParameter("@email", SqlDbType.VarChar);
            SqlParameter PostContentParam = new SqlParameter("@PostContent", SqlDbType.NVarChar);
            SqlParameter DateParam = new SqlParameter("@date", SqlDbType.DateTime);
            SqlParameter IsUserExistedParam = new SqlParameter("@IsUserExisted", SqlDbType.Bit);
            IsUserExistedParam.Direction = ParameterDirection.Output;
            //------Linked Server settings---------------
            /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
            //-------------------------------------------

            BlogIDParam.Value = _BlogID;
            PostNameParam.Value = this.Request.Form["name"];
            PostEmailParam.Value = this.Request.Form["email"];
            PostContentParam.Value = Context.Server.HtmlEncode(this.Request.Form["PostContent"]);
            DateParam.Value = DateTime.Now;

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(PostNameParam);
            command.Parameters.Add(PostEmailParam);
            command.Parameters.Add(PostContentParam);
            command.Parameters.Add(DateParam);
            command.Parameters.Add(IsUserExistedParam);

            command.ExecuteNonQuery();

            bool _IsUserExisted = (bool)IsUserExistedParam.Value;

            command.Dispose();
            connection.Close();

            if (_IsUserExisted)
                WriteStringToAjaxRequest(".پیام شما با موفقیت ارسال شد");
            else
                WriteStringToAjaxRequest(".این چت باکس در سیستم ثبت نشده است");
            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowMessages(Int64 _BlogID)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["ChatBox_Num"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringChatBoxDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowMessages_ChatBox_proc";


            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxChatBoxMessageShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@MessageNum", SqlDbType.Int);
            command.Parameters["@MessageNum"].Direction = ParameterDirection.Output;

            //------Linked Server settings---------------
            //command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            //command.Parameters["@IsPosts1DbLinkedServer"].Value = constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_chatbox"] == null)
                {
                    StreamReader sr = new StreamReader(constants.RootPath + @"\services\ChatBox\ChatBoxShowTemplate.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_chatbox"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_chatbox"];

                if (!HasLoginProc(this, _BlogID))
                {
                    TagDelete(ref template, "delete");
                    template = template.Replace("[height]", "100px");
                }
                else
                {
                    template = template.Replace("[BlogID]", ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID.ToString());
                    template = template.Replace("[height]", "170px");
                }

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
                bool next = false;
                while (reader.Read())
                {
                    temp = _mainFormat;

                    if (!next)
                    {
                        temp = temp.Replace("[f1]", "");
                        temp = temp.Replace("[f2]", "2");
                        next = true;
                    }
                    else
                    {
                        temp = temp.Replace("[f1]", "2");
                        temp = temp.Replace("[f2]", "");
                        next = false;
                    }
                    //------
                    temp = temp.Replace("[date]", PersianDate((DateTime)reader["date"]));
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    temp = temp.Replace("[email]", (string)reader["email"]);
                    SmileyFormat(ref temp, DoHyperlink((string)reader["PostContent"]));
                    temp = temp.Replace("[id]", reader["id"].ToString());
                    //------
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                if (currentPage == 1)
                    this.Session["ChatBox_Num"] = (int)command.Parameters["@MessageNum"].Value;

                command.Dispose();
                connection.Close();

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ChatBox_Num"], _BlogID);
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
        private void MessageDelete(Int64 _BlogID)
        {
            if (!HasLoginProc(this, _BlogID))
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }

            Int64 _DeleteID = -1;
            try
            {
                _DeleteID = Convert.ToInt32(this.Request.Form["DeleteID"]);
            }
            catch { return; }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringChatBoxDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "MessageDelete_ChatBox_proc";

            command.Parameters.Add("@MesseageID", SqlDbType.BigInt);
            command.Parameters["@MesseageID"].Value = _DeleteID;

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
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------------------
        private static string PersianDate(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return String.Format("{2}/{3}/{4} ساعت {0}:{1}", pc.GetHour(dt), pc.GetMinute(dt), pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt));
        }
        //--------------------------------------------------------------------------------
        public static void PagingFormat(Page page, string paging, int currentPage, int _PostNum, Int64 _BlogID)
        {
            DoPaging(ref paging, page, currentPage, _PostNum, _BlogID);
            page.Response.Write(paging);
            return;
            //this.Response.Flush();
        }
        //--------------------------------------------------------------------------------
        private static void DoPaging(ref string buffer, Page page, int currentPage, int PostNum, Int64 _BlogID)
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
                    next = String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"ShowMessages('{0}','{1}');\">»</a>", n * constants.ChatBoxPagingNumber + 1, _BlogID);
                if (n1 == n && n != 1) //last page
                    previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"ShowMessages('{0}','{1}');\">«</a>", (n - 1) * constants.ChatBoxPagingNumber, _BlogID);
                else //middle pages
                {
                    if (n != 1 && n1 != n)
                    {
                        next = String.Format("&nbsp;<a href=\"javascript:void(0);\" onclick=\"ShowMessages('{0}','{1}');\">»</a>", n * constants.ChatBoxPagingNumber + 1, _BlogID);
                        previous = String.Format("<a href=\"javascript:void(0);\" onclick=\"ShowMessages('{0}','{1}');\">«</a>", (n - 1) * constants.ChatBoxPagingNumber, _BlogID);
                    }
                }

                if (previous != null)
                    pagingStr += previous;

                for (int i = ((n - 1) * constants.ChatBoxPagingNumber) + 1; i <= end; i++)
                {
                    if (i == currentPage)
                        pagingStr += String.Format("<u> {0}</u>", i);
                    else
                        pagingStr += String.Format("<a href=\"javascript:void(0);\" onclick=\"ShowMessages('{0}','{1}');\"> {0}</a>", i, _BlogID);
                }

                if (next != null)
                    pagingStr += next;
                //pagingStr += "<br>n: ' + n + "<br>n1: "+ n1;
            }
            buffer = buffer.Replace("[paging]", pagingStr);
        }
        //--------------------------------------------------------------------------------
        public static string DoHyperlink(string s)
        {
            Regex urlregex = new Regex(@"(http:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            s = urlregex.Replace(s, "<a href=\"$1\" target=\"_blank\">$1</a>");
            return s;
        }
        //--------------------------------------------------------------------------------
        private static void SmileyFormat(ref string buffer, string PostContent)
        {
            PostContent = PostContent.Replace(":)", "<IMG title=':)' src='images/smilies/1/smile.gif'>");
            PostContent = PostContent.Replace(":biggrin:", "<IMG title=':biggrin:' src='images/smilies/1/biggrin.gif'>");
            PostContent = PostContent.Replace(":cool:", "<IMG title=':cool:' src='images/smilies/1/toocool.gif'>");
            PostContent = PostContent.Replace(":D", "<IMG title=':D' src='images/smilies/1/grin.gif'>");
            PostContent = PostContent.Replace(":glad:", "<IMG title=':glad:' src='images/smilies/1/glad.gif'>");
            PostContent = PostContent.Replace(":lol:", "<IMG title='نیشخند' src='images/smilies/1/lol.gif'>");
            PostContent = PostContent.Replace(":P", "<IMG title=':P' src='images/smilies/1/tongue.gif'>");
            PostContent = PostContent.Replace(";)", "<IMG title=';)' src='images/smilies/1/wink.gif'>");
            PostContent = PostContent.Replace(":confused:", "<IMG title=':confused:' src='images/smilies/1/confused.gif'>");
            PostContent = PostContent.Replace(":cyclops:", "<IMG title=':cyclops:' src='images/smilies/1/cyclops.gif'>");
            PostContent = PostContent.Replace(":nuts:", "<IMG title=':nuts:' src='images/smilies/1/nuts.gif'>");
            PostContent = PostContent.Replace(":o", "<IMG title=':o' src='images/smilies/1/surprised.gif'>");
            PostContent = PostContent.Replace(":quizzical:", "<IMG title=':quizzical:' src='images/smilies/1/quizzical.gif'>");
            PostContent = PostContent.Replace(":roll:", "<IMG title=':roll:' src='images/smilies/1/rollseyes.gif'>");
            PostContent = PostContent.Replace(":tired:", "<IMG title=':tired:' src='images/smilies/1/tired.gif'>");
            PostContent = PostContent.Replace(":zonked:", "<IMG title=':zonked:' src='images/smilies/1/zonked.gif'>");
            PostContent = PostContent.Replace(":/", "<IMG title=':/' src='images/smilies/1/unsure.gif'>");
            PostContent = PostContent.Replace(":(", "<IMG title=':(' src='images/smilies/1/sad.gif'>");
            PostContent = PostContent.Replace(":aggrieved:", "<IMG title=':aggrieved:' src='images/smilies/1/aggrieved.gif'>");
            PostContent = PostContent.Replace(":aghast:", "<IMG title=':aghast:' src='images/smilies/1/aghast.gif'>");
            PostContent = PostContent.Replace(":cry:", "<IMG title=':cry:' src='images/smilies/1/cry.gif'>");
            PostContent = PostContent.Replace(":furious:", "<IMG title=':furious:' src='images/smilies/1/furious.gif'>");
            PostContent = PostContent.Replace(":nervous:", "<IMG title=':nervous:' src='images/smilies/1/nervous.gif'>");
            PostContent = PostContent.Replace(":x", "<IMG title=':x' src='images/smilies/1/angry.gif'>");
            PostContent = PostContent.Replace(":|", "<IMG title=':|' src='images/smilies/1/frown.gif'>");
            PostContent = PostContent.Replace(":heart:", "<IMG title=':heart:' src='images/smilies/1/heart.gif'>");
            PostContent = PostContent.Replace(":thebox:", "<IMG title=':thebox:' src='images/smilies/1/thebox.gif'>");
            buffer = buffer.Replace("[PostContent]", PostContent);
        }
        //--------------------------------------------------------------------------------
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
            }
            return;
        }
        //--------------------------------------------------------------------------------
        public static bool HasLoginProc(Page page, Int64 _BlogID)
        {
            if (Common.IsLoginProc(page))
            {
                SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)page.Session["SigninSessionInfo"];
                if (_SigninSessionInfo.BlogID == _BlogID)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        //--------------------------------------------------------------------------------
    }
}