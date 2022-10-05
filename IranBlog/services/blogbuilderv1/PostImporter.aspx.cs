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

using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using AlirezaPoshtkoohiLibrary;
using services.blogbuilderv1.PostImporterExceptions;
using services.blogbuilderv1.PostImporterClasses;

using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class PostImporter : System.Web.UI.Page
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
            CheckExisitingPostTransferSessionInfo _info = CheckExisitingPostTransferSession(_SigninSessionInfo);
            if (_info.IsExisted)
            {
                this.status.Visible = true;

                this.TemplateSection.Visible = true;

                this.domain.Enabled = false;

                this.CreateNewSession.Visible = false;
                this.CreateNewSession.Enabled = false;

                if (_info.IsFirstTimeStarted)//consider-date
                {
                    if (IsExisitingWorkingThread())
                    {
                        this.status.Text = String.Format("<div>پست های شما در حال انتقال به ایران بلاگ است. تا کنون از مجموع <font color='#00CC00'>{0}</font> پست <font color='#00CC00'>{1}</font>پست به ایران بلاگ منتقل شده است.</div>", _info.TotalPosts, _info.TotalPosts - _info.CurrentFetchedPost);
                        this.CotntinueSession.Visible = false;
                        this.CotntinueSession.Enabled = false;
                    }
                    else
                    {
                        this.status.Text = "<div>جلسه انتقال پست های شما متوقف شده است. برای ادامه انتقال پست هایتان بر روی دکمه <font color='#00CC00'>ادامه عملیات انتقال</font> کلیک کنید</div>";
                        this.CotntinueSession.Visible = true;
                        this.CotntinueSession.Enabled = true;

                        this.TemplateSection.Visible = false;
                    }

                    this.StartSession.Visible = false;
                    this.StartSession.Enabled = false;
                }
                else
                {
                    if (_info.BlogType == BlogType.BlogFA)
                        this.status.Text = "<div>ابتدا کد قالب زیر را در بخش <font color='#00CC00'>ویرایش قالب سایت بلاگفا</font> کپی کنید.سپس بر روی دکمه <font color='#00CC00'>آغاز عملیات انتقال</font> کلیک کنید.</div>";
                    else
                        this.status.Text = "<div>ابتدا کد قالب زیر را در بخش <font color='#00CC00'>ویرایش قالب سایت پرشین بلاگ</font> کپی کنید.سپس بر روی دکمه <font color='#00CC00'>آغاز عملیات انتقال</font> کلیک کنید.</div>";
                    this.StartSession.Visible = true;
                    this.StartSession.Enabled = true;

                    this.CotntinueSession.Visible = false;
                    this.CotntinueSession.Enabled = false;
                }


                this.DeleteExisitingSession.Visible = true;
                this.DeleteExisitingSession.Enabled = true;

                this.domain.Text = _info.Domain;

                this.template.InnerText = TemplateGenerate(_info.BlogType, _info.PostImporterTag,
                    _info.PostTag, _info.PostTitleTag, _info.PostContentTag,
                    _info.DirectLinkTag, _info.ContinuedPostTag);
            }
            else
            {
                this.status.Text = "<div>ابتدا آدرس وبلاگ خود را وارد کنید. سپس بر روی دکمه <font color='#00CC00'>تعریف انتقال جدید</font> کلیک کنید.</div>";
                this.status.Visible = true;
                this.TemplateSection.Visible = false;

                this.domain.Enabled = true;

                this.CreateNewSession.Visible = true;
                this.CreateNewSession.Enabled = true;

                this.StartSession.Visible = false;
                this.StartSession.Enabled = false;

                this.CotntinueSession.Visible = false;
                this.CotntinueSession.Enabled = false;

                this.DeleteExisitingSession.Visible = false;
                this.DeleteExisitingSession.Enabled = false;
            }
            //this.template.InnerText = TemplateGenerate(BlogType.BlogFA, null, null, null, null, null, null);
        }
        //------------------------------------------------------------------------------------------
        private CheckExisitingPostTransferSessionInfo CheckExisitingPostTransferSession(SigninSessionInfo _SigninSessionInfo)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringPostImporterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CheckExisitingPostTransferSession_PostImporterPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter IsExistedParam = new SqlParameter("@IsExisted", SqlDbType.Bit);
            IsExistedParam.Direction = ParameterDirection.Output;
            SqlParameter DomainParam = new SqlParameter("@domain", SqlDbType.NVarChar, 50);
            DomainParam.Direction = ParameterDirection.Output;
            SqlParameter TotalPostsParam = new SqlParameter("@TotalPosts", SqlDbType.Int);
            TotalPostsParam.Direction = ParameterDirection.Output;
            SqlParameter CurrentFetchedPostParam = new SqlParameter("@CurrentFetchedPost", SqlDbType.Int);
            CurrentFetchedPostParam.Direction = ParameterDirection.Output;
            SqlParameter LastFetchedPostDateParam = new SqlParameter("@LastFetchedPostDate", SqlDbType.DateTime);
            LastFetchedPostDateParam.Direction = ParameterDirection.Output;
            SqlParameter BlogTypeParam = new SqlParameter("@BlogType", SqlDbType.Int);
            BlogTypeParam.Direction = ParameterDirection.Output;
            SqlParameter CategoryIDParam = new SqlParameter("@CategoryID", SqlDbType.BigInt);
            CategoryIDParam.Direction = ParameterDirection.Output;
            SqlParameter PostImporterTagParam = new SqlParameter("@PostImporterTag", SqlDbType.VarChar, 16);
            PostImporterTagParam.Direction = ParameterDirection.Output;
            SqlParameter PostTagParam = new SqlParameter("@PostTag", SqlDbType.VarChar, 16);
            PostTagParam.Direction = ParameterDirection.Output;
            SqlParameter PostTitleTagParam = new SqlParameter("@PostTitleTag", SqlDbType.VarChar, 16);
            PostTitleTagParam.Direction = ParameterDirection.Output;
            SqlParameter PostContentTagParam = new SqlParameter("@PostContentTag", SqlDbType.VarChar, 16);
            PostContentTagParam.Direction = ParameterDirection.Output;
            SqlParameter DirectLinkTagParam = new SqlParameter("@DirectLinkTag", SqlDbType.VarChar, 16);
            DirectLinkTagParam.Direction = ParameterDirection.Output;
            SqlParameter ContinuedPostTagParam = new SqlParameter("@ContinuedPostTag", SqlDbType.VarChar, 16);
            ContinuedPostTagParam.Direction = ParameterDirection.Output;
            SqlParameter IsFirstTimeStartedParam = new SqlParameter("@IsFirstTimeStarted", SqlDbType.Bit);
            IsFirstTimeStartedParam.Direction = ParameterDirection.Output;

            BlogIDParam.Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(IsExistedParam);
            command.Parameters.Add(DomainParam);
            command.Parameters.Add(TotalPostsParam);
            command.Parameters.Add(CurrentFetchedPostParam);
            command.Parameters.Add(LastFetchedPostDateParam);
            command.Parameters.Add(BlogTypeParam);
            command.Parameters.Add(CategoryIDParam);
            command.Parameters.Add(PostImporterTagParam);
            command.Parameters.Add(PostTagParam);
            command.Parameters.Add(PostTitleTagParam);
            command.Parameters.Add(PostContentTagParam);
            command.Parameters.Add(DirectLinkTagParam);
            command.Parameters.Add(ContinuedPostTagParam);
            command.Parameters.Add(IsFirstTimeStartedParam);
            //------Linked Server settings---------------
            /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
            //-------------------------------------------

            command.ExecuteNonQuery();

            CheckExisitingPostTransferSessionInfo _info = new CheckExisitingPostTransferSessionInfo();
            if ((bool)command.Parameters["@IsExisted"].Value)
            {
                _info.BlogType = (BlogType)(int)command.Parameters["@BlogType"].Value;
                _info.CategoryID = (Int64)command.Parameters["@CategoryID"].Value;
                _info.CurrentFetchedPost = (int)command.Parameters["@CurrentFetchedPost"].Value;
                _info.Domain = (string)command.Parameters["@domain"].Value;
                _info.IsExisted = (bool)command.Parameters["@IsExisted"].Value;
                _info.LastFetchedPostDate = (DateTime)command.Parameters["@LastFetchedPostDate"].Value;
                _info.TotalPosts = (int)command.Parameters["@TotalPosts"].Value;
                _info.PostImporterTag = (string)command.Parameters["@PostImporterTag"].Value;
                _info.PostTag = (string)command.Parameters["@PostTag"].Value;
                _info.PostTitleTag = (string)command.Parameters["@PostTitleTag"].Value;
                _info.PostContentTag = (string)command.Parameters["@PostContentTag"].Value;
                _info.DirectLinkTag = (string)command.Parameters["@DirectLinkTag"].Value;
                _info.ContinuedPostTag = (string)command.Parameters["@ContinuedPostTag"].Value;
                _info.IsFirstTimeStarted = (bool)command.Parameters["@IsFirstTimeStarted"].Value;
            }
            else
                _info.IsExisted = false;


            command.Dispose();
            connection.Close();

            return _info;
        }
        //------------------------------------------------------------------------------------------
        private int ComputePostNumber(string subdomain, BlogType _BlogType, string __PostImporterTag, string __DirectLinkTag)
        {
            string _html = "";
            int _PostNumber = 0;
            if (_BlogType == BlogType.PersianBlog)
                _html = GetHtmlContentViaTcpClientForPersianblogFirstPage(subdomain, 80);
            if (_BlogType == BlogType.BlogFA)
                _html = GetHtmlContentViaHttpWebRequest(String.Format("http://{0}/", subdomain));
            //throw new Exception(_html);
            string _DirectLinkTag = GetDirectLinkTagValue(GetPostImporterTag(_html, __PostImporterTag), __DirectLinkTag);
            if (_BlogType == BlogType.PersianBlog)
            {
                int p = _DirectLinkTag.LastIndexOf('/');
                if (p < 0)
                    return _PostNumber;
                if (p == _DirectLinkTag.Length - 1)
                    return _PostNumber;
                else
                    return Convert.ToInt32(_DirectLinkTag.Substring(p + 1));
            }
            if (_BlogType == BlogType.BlogFA)
            {
                int p1 = _DirectLinkTag.IndexOf('-');
                if (p1 < 0)
                    return _PostNumber;
                int p2 = _DirectLinkTag.LastIndexOf('.');
                if (p2 < 0 || p2 <= p1)
                    return _PostNumber;
                if (p2 == _DirectLinkTag.Length - 1)
                    return _PostNumber;
                p1++;
                return Convert.ToInt32(_DirectLinkTag.Substring(p1, p2 - p1).Trim());
            }
            return _PostNumber;
        }
        //------------------------------------------------------------------------------------------
        private string GetHtmlContentViaTcpClientForPersianblogFirstPage(string domain, int port)
        {
            TcpClient client = new TcpClient(domain, port);
            //client.ReceiveTimeout = 1000;
            //client.SendTimeout = client.ReceiveTimeout;
            string message = String.Format("GET / HTTP/1.1\r\nHost: {0}\r\nConnection: close\r\nUser-Agent: Mozilla/4.0 (compatible; MSIE 7.0b; Windows NT 6.0)\r\n\r\n", domain);
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            data = new byte[1204 * 256];
            int i = 0;
            int temp;
            while (true)
            {
                temp = stream.ReadByte();
                if (temp == -1 || i == data.Length)
                    break;
                data[i] = (byte)temp;
                i++;
            }
            stream.Close();
            //int n = stream.Read(data, 0, data.Length);
            if (i > 0)
            {
                return System.Text.Encoding.UTF8.GetString(data, 0, i);
            }
            else
                throw new InvalidPostImporterTagException();

        }
        //------------------------------------------------------------------------------------------
        private string GetHtmlContentViaHttpWebRequest(string _url)
        {
            HttpWebRequest _HttpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
            // *** Set properties
            //_HttpWebRequest.Timeout = 10000;     // 10 secs
            _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0b; Windows NT 6.0";
            // *** Retrieve request info headers
            HttpWebResponse _HttpWebResponse = (HttpWebResponse)_HttpWebRequest.GetResponse();
            Encoding enc = Encoding.UTF8;  // Windows default Code Page
            StreamReader loResponseStream = new StreamReader(_HttpWebResponse.GetResponseStream(), enc);
            string lcHtml = loResponseStream.ReadToEnd();
            _HttpWebResponse.Close();
            return lcHtml;
        }
        //------------------------------------------------------------------------------------------
        /*private string GetHtmlContentViaWinHttpRequest(string _url)
        {
            WinHttpRequest _WinHttpRequest = new WinHttpRequest();
            _WinHttpRequest.Open("GET", _url, false);
            _WinHttpRequest.Send("");
            return _WinHttpRequest.ResponseText;
        }*/
        //------------------------------------------------------------------------------------------
        private string GetDirectLinkTagValue(string buffer, string _DirectLinkTag)
        {
            int p1;
            int p2;
            if (_DirectLinkTag != null)
            {
                p1 = buffer.IndexOf(String.Format("<{0}>", _DirectLinkTag)) + _DirectLinkTag.Length + 2;
                p2 = buffer.IndexOf(String.Format("</{0}>", _DirectLinkTag), p1);
            }
            else
            {
                p1 = buffer.IndexOf("<DirectLink>") + "<DirectLink>".Length;
                p2 = buffer.IndexOf("</DirectLink>", p1);
            }
            if (p1 >= 0 && p2 >= 0)
                return buffer.Substring(p1, p2 - p1);
            else
                throw new InvalidDirectLinkTagException();
        }
        //------------------------------------------------------------------------------------------
        private string GetPostContentTagValue(string buffer, string _PostContentTag)
        {
            int p1;
            int p2;
            if (_PostContentTag != null)
            {
                p1 = buffer.IndexOf(String.Format("<{0}>", _PostContentTag)) + _PostContentTag.Length + 2;
                p2 = buffer.IndexOf(String.Format("</{0}>", _PostContentTag), p1);
            }
            else
            {
                p1 = buffer.IndexOf("<PostContent>") + "<PostContent>".Length;
                p2 = buffer.IndexOf("</PostContent>", p1);
            }
            if (p1 >= 0 && p2 >= 0)
                return buffer.Substring(p1, p2 - p1);
            else
                throw new InvalidDirectLinkTagException();
        }
        //------------------------------------------------------------------------------------------
        private string GetPostTitleTagValue(string buffer, string _PostTitleTag)
        {
            int p1;
            int p2;
            if (_PostTitleTag != null)
            {
                p1 = buffer.IndexOf(String.Format("<{0}>", _PostTitleTag)) + _PostTitleTag.Length + 2;
                p2 = buffer.IndexOf(String.Format("</{0}>", _PostTitleTag), p1);
            }
            else
            {
                p1 = buffer.IndexOf("<PostTitle>") + "<PostTitle>".Length;
                p2 = buffer.IndexOf("</PostTitle>", p1);
            }
            if (p1 >= 0 && p2 >= 0)
                return buffer.Substring(p1, p2 - p1);
            else
                throw new InvalidDirectLinkTagException();
        }
        //------------------------------------------------------------------------------------------
        private string GetPostImporterTag(string buffer, string _PostImporterTag)
        {
            int p1;
            int p2;

            if (_PostImporterTag != null)
            {
                p1 = buffer.IndexOf(String.Format("<{0}>", _PostImporterTag)) + _PostImporterTag.Length + 2;
                p2 = buffer.IndexOf(String.Format("</{0}>", _PostImporterTag), p1);
            }
            else
            {
                p1 = buffer.IndexOf("<PostImporter>") + "<PostImporter>".Length;
                p2 = buffer.IndexOf("</PostImporter>", p1);
            }
            //throw new Exception(p1.ToString() + "," + p2.ToString());
            if (p1 >= 0 && p2 >= 0)
            {
                if (_PostImporterTag != null)
                {
                    p1 -= _PostImporterTag.Length + 2;
                    p2 += _PostImporterTag.Length + 3;
                }
                else
                {
                    p1 -= "<PostImporter>".Length;
                    p2 += "</PostImporter>".Length;
                }
                return buffer.Substring(p1, p2 - p1);
            }
            else
                throw new InvalidPostImporterTagException();
        }
        //------------------------------------------------------------------------------------------
        private string TemplateGenerate(BlogType _BlogType, string _PostImporterTag, string _PostTag, string _PostTitleTag, string _PostContentTag, string _DirectLinkTag, string _ContinuedPostTag)
        {
            string template = "";
            if (this.Cache["_template_postimporter"] == null)
            {
                StreamReader sr = new StreamReader(constants.RootPath + @"\services\blogbuilderv1\PostImporterTemplate.html");
                template = sr.ReadToEnd();
                this.Cache["_template_postimporter"] = template;
                sr.Close();
            }
            else
                template = (string)this.Cache["_template_postimporter"];
            switch (_BlogType)
            {
                case BlogType.BlogFA:
                    template = template.Replace("__ServiceProviderTag", "BLOGFA");
                    break;
                case BlogType.PersianBlog:
                    template = template.Replace("__ServiceProviderTag", "BlogPost");
                    break;
                default:
                    template = template.Replace("__ServiceProviderTag", "BLOGFA");
                    break;
            }
            if (_PostImporterTag != null)
                template = template.Replace("__PostImporter", _PostImporterTag);
            if (_PostTag != null)
                template = template.Replace("__post", _PostTag);
            if (_PostTitleTag != null)
                template = template.Replace("__PostTitle", _PostTitleTag);
            if (_PostContentTag != null)
                template = template.Replace("__PostContent", _PostContentTag);
            if (_DirectLinkTag != null)
                template = template.Replace("__DirectLink", _DirectLinkTag);
            if (_ContinuedPostTag != null)
                template = template.Replace("__ContinuedPost", _ContinuedPostTag);

            return template;

        }
        //------------------------------------------------------------------------------------------
        protected void CreateNewSession_Click(object sender, EventArgs e)
        {
            if (this.Request.Form["domain"] == null || this.Request.Form["domain"] == "")
            {
                this.status.Text = "آدرس وبلاگ خالی است.";
                this.status.Visible = true;
                return;
            }
            Regex rex = new Regex(@"[a-zA-Z0-9]+[\.](persianblog\.ir|blogfa\.com){1}$");
            if (!rex.IsMatch(this.Request.Form["domain"]))
            {
                this.status.Text = "آدرس وبلاگ نامعتبر است.";
                this.status.Visible = true;
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringPostImporterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CreateNewSession_PostImporterPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter DomainParam = new SqlParameter("@domain", SqlDbType.NVarChar, 50);
            SqlParameter BlogTypeParam = new SqlParameter("@BlogType", SqlDbType.Int);
            SqlParameter PostImporterTagParam = new SqlParameter("@PostImporterTag", SqlDbType.VarChar, 16);
            SqlParameter PostTagParam = new SqlParameter("@PostTag", SqlDbType.VarChar, 16);
            SqlParameter PostTitleTagParam = new SqlParameter("@PostTitleTag", SqlDbType.VarChar, 16);
            SqlParameter PostContentTagParam = new SqlParameter("@PostContentTag", SqlDbType.VarChar, 16);
            SqlParameter DirectLinkTagParam = new SqlParameter("@DirectLinkTag", SqlDbType.VarChar, 16);
            SqlParameter ContinuedPostTagParam = new SqlParameter("@ContinuedPostTag", SqlDbType.VarChar, 16);
            SqlParameter SubjectParam = new SqlParameter("@subject", SqlDbType.NVarChar, 400);


            BlogIDParam.Value = _SigninSessionInfo.BlogID;

            string _domain = this.Request.Form["domain"].ToLower();
            DomainParam.Value = _domain;
            BlogType _BlogType;
            if (_domain.IndexOf(".blogfa.com") >= 0)
                _BlogType = BlogType.BlogFA;
            else
                _BlogType = BlogType.PersianBlog;
            BlogTypeParam.Value = (int)_BlogType;

            PostImporterTagParam.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            PostTagParam.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            PostTitleTagParam.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            PostContentTagParam.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            DirectLinkTagParam.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            ContinuedPostTagParam.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

            SubjectParam.Value = "پست های انتقالی";

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(DomainParam);
            command.Parameters.Add(BlogTypeParam);
            command.Parameters.Add(PostImporterTagParam);
            command.Parameters.Add(PostTagParam);
            command.Parameters.Add(PostTitleTagParam);
            command.Parameters.Add(PostContentTagParam);
            command.Parameters.Add(DirectLinkTagParam);
            command.Parameters.Add(ContinuedPostTagParam);
            command.Parameters.Add(SubjectParam);
            //------Linked Server settings---------------
            /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
            //-------------------------------------------

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            //this.status.Text = "جلسه انتقال آغاز شد. برای پیگیری وضعیت چند دقیقه بعد دوباره به این صفحه مراجعه کنید.";
            if (_BlogType == BlogType.BlogFA)
                this.status.Text = "<div>ابتدا کد قالب زیر را در بخش <font color='#00CC00'>ویرایش قالب سایت بلاگفا</font> کپی کنید.سپس بر روی دکمه <font color='#00CC00'>آغاز عملیات انتقال</font> کلیک کنید.</div>";
            else
                this.status.Text = "<div>ابتدا کد قالب زیر را در بخش <font color='#00CC00'>ویرایش قالب سایت پرشین بلاگ</font> کپی کنید.سپس بر روی دکمه <font color='#00CC00'>آغاز عملیات انتقال</font> کلیک کنید.</div>";
            this.status.Visible = true;

            this.TemplateSection.Visible = true;

            this.domain.Enabled = false;

            this.CreateNewSession.Visible = false;
            this.CreateNewSession.Enabled = false;

            this.StartSession.Visible = true;
            this.StartSession.Enabled = true;

            this.CotntinueSession.Visible = false;
            this.CotntinueSession.Enabled = false;

            this.DeleteExisitingSession.Visible = true;
            this.DeleteExisitingSession.Enabled = true;

            this.template.InnerText = TemplateGenerate(_BlogType, (string)PostImporterTagParam.Value,
                (string)PostTagParam.Value, (string)PostTitleTagParam.Value, (string)PostContentTagParam.Value,
                (string)DirectLinkTagParam.Value, (string)ContinuedPostTagParam.Value);
            return;
        }
        //------------------------------------------------------------------------------------------
        protected void StartSession_Click(object sender, EventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if(IsExisitingWorkingThreadProc())
                return;
            //throw new Exception();
            CheckExisitingPostTransferSessionInfo _info = CheckExisitingPostTransferSession(_SigninSessionInfo);
            //throw new Exception(_info.PostImporterTag);
            if (_info.IsExisted && !_info.IsFirstTimeStarted)
            {
                int _postNum = 0;
                bool _IsTagException = false;
                bool _IsNetworkException = false;
                try
                {
                    _postNum = ComputePostNumber(_info.Domain, _info.BlogType, _info.PostImporterTag, _info.DirectLinkTag);
                }
                catch (InvalidPostImporterTagException)
                {
                    _IsTagException = true;
                }
                catch (InvalidDirectLinkTagException)
                {
                    _IsTagException = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    _IsTagException = true;
                }
                catch (ArgumentException)
                {
                    _IsTagException = true;
                }
                catch (Exception)
                {
                    _IsNetworkException = true;
                }
                if (_IsTagException)
                {
                    this.status.Text = "<div><font color='#00CC00'>خطا:</font> قالب وبلاگ را در وبلاگ خود قرار نداده اید و یا وبلاگ شما فاقد هر گونه پستی میباشد.</div>";
                    this.status.Visible = true;
                    return;
                }
                if (_IsNetworkException)
                {
                    this.status.Text = "<div><font color='#00CC00'>خطا:</font> مشکلات شبکه ایی پیش آمده برای سرویس دهنده بلاگفا و یا پرشین بلاگ انتخابی شما، فعلا پست های شما را غیر قابل دسترسی کرده است. بعدا تلاش کنید.</div>";
                    this.status.Visible = true;
                    return;
                }
                //this.Response.Write(_postNum.ToString());

                _info.TotalPosts = _postNum;


                Thread worker = new Thread(this.PostTransferWorkerProc);
                worker.Start(_info);

                //this.Response.Write(_info.TotalPosts.ToString());

                this.status.Text = "<div>عملیات انتقال پست های وبلاگ شما آغاز شد.چند دقیقه صبر کنید و دوباره به این صفحه مراجعه کنید تا از چگونگی روند انتقال پست هایتان به ایران بلاگ آگاهی یابید(در صورتیکه تعداد پست های انتقالی زیاد باشد). در صورتیکه همه پست های وبلاگ شما به ایران بلاگ منتقل شده باشد، در این صفحه هیچ پیامی مشاهده نخواهید کرد و سیستم آماده از سرگیری یک جلسه انتقال جدید خواهد بود. می توانید پست های انتقال داده شده اتان را با آرشیو موضوعی <font color='#00CC00'>پست های انتقالی</font> در وبلاگتان مشاهده کنید. در ضمن عنوان این آرشیو موضوعی به وجود آمده به طور خودکار را می توانید در بخش <font color='#00CC00'>آرشیو موضوعی</font> در کنترل پنل کاربری ایران بلاگ تغییر دهید.</div>";
                this.status.Visible = true;

                this.TemplateSection.Visible = false;


                this.domain.Enabled = false;

                this.CreateNewSession.Visible = false;
                this.CreateNewSession.Enabled = false;

                this.StartSession.Visible = false;
                this.StartSession.Enabled = false;

                this.CotntinueSession.Visible = false;
                this.CotntinueSession.Enabled = false;

                this.DeleteExisitingSession.Visible = true;
                this.DeleteExisitingSession.Enabled = true;

                return;
            }
            else
            {
                this.Response.Redirect("PostImporter.aspx", true);
                return;
            }
        }
        //------------------------------------------------------------------------------------------
        protected void CotntinueSession_Click(object sender, EventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            if (IsExisitingWorkingThreadProc())
                return;
            CheckExisitingPostTransferSessionInfo _info = CheckExisitingPostTransferSession(_SigninSessionInfo);
            if (_info.IsExisted && _info.IsFirstTimeStarted)
            {
                Thread worker = new Thread(this.PostTransferWorkerProc);
                worker.Start(_info);

                //this.Response.Write(_info.TotalPosts.ToString());

                this.status.Text = "عملیات انتقال پست های وبلاگ شما آغاز شد.چند دقیقه صبر کنید و دوباره به این صفحه مراجعه کنید تا از چگونگی روند انتقال پست هایتان به ایران بلاگ آگاهی یابید.";
                this.status.Visible = true;

                this.TemplateSection.Visible = false;


                this.domain.Enabled = false;

                this.CreateNewSession.Visible = false;
                this.CreateNewSession.Enabled = false;

                this.StartSession.Visible = false;
                this.StartSession.Enabled = false;

                this.CotntinueSession.Visible = false;
                this.CotntinueSession.Enabled = false;

                this.DeleteExisitingSession.Visible = true;
                this.DeleteExisitingSession.Enabled = true;

                return;
            }
            else
            {
                this.Response.Redirect("PostImporter.aspx", true);
                return;
            }
        }
        //------------------------------------------------------------------------------------------
        protected void DeleteExisitingSession_Click(object sender, EventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            Int64 _BlogID = _SigninSessionInfo.BlogID;

            SqlConnection connection = new SqlConnection(constants.ConnectionStringPostImporterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DeleteSession_PostImporterPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);


            BlogIDParam.Value = _BlogID;

            command.Parameters.Add(BlogIDParam);

            //------Linked Server settings---------------
            /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
            //-------------------------------------------

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            this.status.Text = "جلسه انتقال حذف شد.";
            this.status.Visible = true;

            this.TemplateSection.Visible = false;
            this.template.InnerText = "";

            this.domain.Enabled = true;
            this.domain.Text = "";

            this.CreateNewSession.Visible = true;
            this.CreateNewSession.Enabled = true;

            this.StartSession.Visible = false;
            this.StartSession.Enabled = false;

            this.CotntinueSession.Visible = false;
            this.CotntinueSession.Enabled = false;

            this.DeleteExisitingSession.Visible = false;
            this.DeleteExisitingSession.Enabled = false;

            string _postimporter_cache_id = String.Format("_postimporter_session_{0}", _BlogID);
            if (this.Cache[_postimporter_cache_id] != null)
                this.Cache.Remove(_postimporter_cache_id);

            return;
        }
        //------------------------------------------------------------------------------------------
        private void SetInitialPostTransferState(int _TotalPosts)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringPostImporterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SetInitialPostTransferState_PostImporterPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter TotalPostsParam = new SqlParameter("@TotalPosts", SqlDbType.Int);


            BlogIDParam.Value = _SigninSessionInfo.BlogID;
            TotalPostsParam.Value = _TotalPosts;

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(TotalPostsParam);

            //------Linked Server settings---------------
            /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
            //-------------------------------------------

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            return;
        }
        //------------------------------------------------------------------------------------------
        private bool IsExisitingWorkingThreadProc()
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            string _postimporter_cache_id = String.Format("_postimporter_session_{0}", _SigninSessionInfo.BlogID);
            if (this.Cache[_postimporter_cache_id] != null)
            {
                //throw new Exception(this.Cache[_postimporter_cache_id].ToString());
                this.status.Text = "جلسه انتقال قبلا آغاز شده اشت.";
                this.status.Visible = true;

                this.TemplateSection.Visible = false;


                this.domain.Enabled = false;

                this.CreateNewSession.Visible = false;
                this.CreateNewSession.Enabled = false;

                this.StartSession.Visible = false;
                this.StartSession.Enabled = false;

                this.CotntinueSession.Visible = false;
                this.CotntinueSession.Enabled = false;

                this.DeleteExisitingSession.Visible = true;
                this.DeleteExisitingSession.Enabled = true;

                return true;
            }
            else
                return false;
        }
        //------------------------------------------------------------------------------------------
        private bool IsExisitingWorkingThread()
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            string _postimporter_cache_id = String.Format("_postimporter_session_{0}", _SigninSessionInfo.BlogID);
            if (this.Cache[_postimporter_cache_id] != null)
                return true;
            else
                return false;
        }
        //------------------------------------------------------------------------------------------
        private void PostTransferWorkerProc(object info)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            Int64 _BlogID = _SigninSessionInfo.BlogID;
            Int64 _AuthorID = _SigninSessionInfo.AuthorID;
            string _postimporter_cache_id = String.Format("_postimporter_session_{0}", _BlogID);
            if (this.Cache[_postimporter_cache_id] == null)
            {
                this.Cache[_postimporter_cache_id] = true ;
            }
            else
                return;
            
            CheckExisitingPostTransferSessionInfo _info = (CheckExisitingPostTransferSessionInfo)info;
            /*if (_info.TotalPosts != _info.CurrentFetchedPost)
            {
                System.IO.StreamWriter _sw = new StreamWriter(constants.RootPath + "tt.txt");
                for (int i = _info.TotalPosts - _info.CurrentFetchedPost ; i >= 1 ; i--)
                {
                    _sw.WriteLine(i);
                }
                _sw.Close();
            }*/
            /*string _html = "";
            System.IO.StreamWriter _sw = new StreamWriter(constants.RootPath + "tt.txt");
            for (int i = _info.TotalPosts - _info.CurrentFetchedPost; i >= 1; i--)
            {
                if (_info.BlogType == BlogType.PersianBlog)
                    _html = GetHtmlContentViaHttpWebRequest(String.Format("http://{0}/post/{1}", _info.Domain, i));
                if (_info.BlogType == BlogType.BlogFA)
                    _html = GetHtmlContentViaHttpWebRequest(String.Format("http://{0}/post-{1}.aspx", _info.Domain, i));
                _sw.WriteLine(GetPostTitleTagValue(_html, _info.PostTitleTag)+","+GetPostContentTagValue(_html, _info.PostContentTag));
            }
            _sw.Close();
            return;*/
            if (_info.TotalPosts != _info.CurrentFetchedPost || _info.TotalPosts == 0)
            {
                string _html = "";
                if (!_info.IsFirstTimeStarted)
                {
                    SetInitialPostTransferState(_info.TotalPosts);
                    _info.IsFirstTimeStarted = true;
                }
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertNewPost_PostImporterPage_proc";

                SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
                SqlParameter SubjectParam = new SqlParameter("@subject", SqlDbType.NVarChar, 200);
                SqlParameter ContentParam = new SqlParameter("@content", SqlDbType.NText);
                SqlParameter CategoryIDParam = new SqlParameter("@CategoryID", SqlDbType.BigInt);
                SqlParameter AuthorIDParam = new SqlParameter("@AuthorID", SqlDbType.BigInt);

                BlogIDParam.Value = _BlogID;
                AuthorIDParam.Value = _AuthorID;
                CategoryIDParam.Value = _info.CategoryID;

                command.Parameters.Add(BlogIDParam);
                command.Parameters.Add(SubjectParam);
                command.Parameters.Add(ContentParam);
                command.Parameters.Add(CategoryIDParam);
                command.Parameters.Add(AuthorIDParam);
                //------Linked Server settings---------------
                /*SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
                IsAccountsDbLinkedServerParam.Value = constants.IsAccountsDbLinkedServer;
                command.Parameters.Add(IsAccountsDbLinkedServerParam);*/
                //-------------------------------------------
                //for (int i = _info.TotalPosts - _info.CurrentFetchedPost; i >= 1; i--)
                for (int i = _info.CurrentFetchedPost + 1; i <= _info.TotalPosts ; i++)
                {
                    if (this.Cache[_postimporter_cache_id] == null)
                        break;
                    if (_info.BlogType == BlogType.PersianBlog)
                        _html = GetHtmlContentViaHttpWebRequest(String.Format("http://{0}/post/{1}", _info.Domain, i));
                    if (_info.BlogType == BlogType.BlogFA)
                        _html = GetHtmlContentViaHttpWebRequest(String.Format("http://{0}/post-{1}.aspx", _info.Domain, i));
                    SubjectParam.Value = GetPostTitleTagValue(_html, _info.PostTitleTag);
                    ContentParam.Value = GetPostContentTagValue(_html, _info.PostContentTag);
                    command.ExecuteNonQuery();
                }

                command.Dispose();
                connection.Close();

                if(this.Cache[_postimporter_cache_id] != null)
                    this.Cache.Remove(_postimporter_cache_id);

                return;

            }
        }
        //------------------------------------------------------------------------------------------
    }
}
