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

using System.Data.SqlClient;
using System.IO;
using bookstore;
using bookstore.Enums;

using System.Diagnostics;
using System.Net;

namespace bookstore
{
    public partial class search : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                bool _SiteLanguageIsPersian = (bool)this.Session["SiteLanguageIsPersian"];
                bool _IsLogined = Common.IsLoginProc(this);
                //DoSwisheSearch("\"title= grid\"");
                if (this.Request.Form["mode"] != null)
                {
                    switch (this.Request.Form["mode"])
                    {
                        case "ShowByBookSearch":
                            ShowByBookSearch(_IsLogined, _SiteLanguageIsPersian);
                            return;
                        default:
                            return;
                    }
                }
                PageSettings();
            }
        }
        //--------------------------------------------------------------------
        private void PageSettings()
        {
            LoginPanelControlLoad();
            MainMenuControlLoad();
        }
        //--------------------------------------------------------------------
        private void LoginPanelControlLoad()
        {
            this.LoginPanelControl.Controls.Add(LoadControl("LoginControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MainMenuControlLoad()
        {
            this.MainMenuControl.Controls.Add(LoadControl("MainMenuControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void ShowByBookSearch(bool _IsLogined, bool _SiteLanguageIsPersian)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_searches"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }
            if (currentPage == 1)
            {
                Hashtable _ht = new Hashtable();

                if (this.Request.Form["title"] != null && this.Request.Form["title"] != "")
                {
                    //_ht.Add("isbn", this.Request.Form["title"]);
                    _ht.Add("title", this.Request.Form["title"]);
                }

                if (this.Request.Form["writer"] != null && this.Request.Form["writer"] != "")
                    _ht.Add("creator", this.Request.Form["writer"]);

                if (this.Request.Form["publisher"] != null && this.Request.Form["publisher"] != "")
                    _ht.Add("publisher", this.Request.Form["publisher"]);

                if (this.Request.Form["date"] != null && this.Request.Form["date"] != "")
                    _ht.Add("date", this.Request.Form["date"]);

                string _sreachPattern = "";
                bool _ForIsbnSearch = true;

                if (_ht.Count > 0)
                {
                    foreach (DictionaryEntry de in _ht)
                    {
                        //_sreachPattern += String.Format("\"{0}= ({1})\" and \"subject= ({1})\" and \"description= ({1})\" ", de.Key, _rr[i]);
                        if (de.Key.ToString() == "title")
                        {
                            _sreachPattern += String.Format("\"{0}= ({1})\" or \"subject= ({1})\" or \"description= ({1})\" ", de.Key, de.Value);
                            continue;
                            /*string[] _rr = de.Value.ToString().Split(' ');
                            if (_rr.Length > 1)
                            {
                                _ForIsbnSearch = false;
                                for (int i = 0; i < _rr.Length; i++)
                                {
                                    if(i == _rr.Length - 1)
                                        _sreachPattern += String.Format("\"{0}= {1}\" ", de.Key, _rr[i]);
                                    else
                                        _sreachPattern += String.Format("\"{0}= {1}\" and ", de.Key, _rr[i]);
                                }
                                continue;
                            }*/
                        }
                        if (_ht.Count == 1)
                            _sreachPattern += String.Format("\"{0}= {1}\" ", de.Key, de.Value);
                        else
                            _sreachPattern += String.Format("\"{0}= {1}\" and ", de.Key, de.Value);


                    }
                    if (_ForIsbnSearch)
                        if (this.Request.Form["title"] != null && this.Request.Form["title"] != "")
                            //_sreachPattern += String.Format(" or \"isbn= {0}\" ", this.Request.Form["title"]);
                    //this.WriteStringToAjaxRequest(_sreachPattern);return;
                    DoSwisheSearch(_sreachPattern);
                    //this.Session["_ItemNumShowByBookSearch"] = ((ArrayList)this.Session["_searches"]).Count;
                    //WriteStringToAjaxRequest(this.Request.Form["title"]);*/
                }
                else
                {
                    WriteStringToAjaxRequest("NoFoundPost");
                    return;
                }
            }

            ArrayList _searches = (ArrayList)this.Session["_searches"];
            if (_searches == null)
                _searches = new ArrayList();

            if (_searches.Count > 0)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_Search"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\SearchTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_Search"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_Search"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\SearchTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                if (_IsLogined)
                {
                    if (_SiteLanguageIsPersian)
                    {
                        Common.TagDelete(ref template, "english");
                        Common.TagDelete(ref template, "english");
                        Common.TagDelete(ref template, "MainEnglish");
                    }
                    else
                    {
                        Common.TagDelete(ref template, "persian");
                        Common.TagDelete(ref template, "persian");
                        Common.TagDelete(ref template, "MainPersian");
                    }
                }
                else
                {
                    if (_SiteLanguageIsPersian)
                    {
                        Common.TagDelete(ref template, "english");
                        Common.TagDelete(ref template, "english");
                        Common.TagDelete(ref template, "MainEnglish");
                    }
                    else
                    {
                        Common.TagDelete(ref template, "persian");
                        Common.TagDelete(ref template, "persian");
                        Common.TagDelete(ref template, "MainPersian");
                    }
                    Common.TagDelete(ref template, "logined");
                }
				
                template = template.Replace("[num]", _searches.Count.ToString());
										
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ShowSearchBookByISBN_SearchPage_proc";

                command.Parameters.Add("@BookISBN", SqlDbType.NVarChar);

                SqlDataReader reader = null;


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


                string temp = null;

                int index = (currentPage - 1) * constants.MaxShowByBookSearch;

                for (int i = index; i < index + constants.MaxShowByBookSearch; i++)
                //for (int i = 0; i < _searches.Count ; i++)
                {
                    if (i >= _searches.Count)
                        break;
                    temp = _mainFormat;

                    command.Parameters["@BookISBN"].Value = (string)_searches[i];

                    //this.Response.Write((string)_searches[i]+"<br>");

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        temp = temp.Replace("[BookID]", reader["BookID"].ToString());
                        if (this.Request.Form["title"] != null)
                        {
                            string __temp1 = this.Request.Form["title"].ToLower();
                            string __temp2 = reader["BookTitle"].ToString().ToLower();
                            string __temp3 = reader["BookTitle"].ToString();
                            string output = "";
                            int p = __temp2.IndexOf(__temp1);
                            if (p >= 0)
                            {
                                for (int j = 0; j < p ; j++)
                                    output += __temp3[j].ToString();

                                output += "<span style='background-color:#FFFF00'>";

                                for (int j = p; j < p + __temp1.Length ; j++)
                                    output += __temp3[j].ToString();

                                output += "</span>";

                                for (int j = p + __temp1.Length ; j < __temp2.Length; j++)
                                    output += __temp3[j].ToString();

                                temp = temp.Replace("[BookTitle]", output);
                            }
                            else
                                temp = temp.Replace("[BookTitle]", reader["BookTitle"].ToString());
                        }
                        temp = temp.Replace("[BookWriter]", reader["BookWriter"].ToString());
                        temp = temp.Replace("[BookISBN]", (string)_searches[i]);
                        temp = temp.Replace("[BookAbstract]", reader["BookAbstract"].ToString());
                        int BookPublishDate = (int)reader["BookPublishDate"];
                        if (BookPublishDate <= 0)
                            temp = temp.Replace("[BookPublishDate]", "-");
                        else
                           temp = temp.Replace("[BookPublishDate]", reader["BookPublishDate"].ToString());

                        int BookPages = (int)reader["BookPages"];
                        if (BookPages <= 0)
                            temp = temp.Replace("[BookPages]", "-");
                        else
                            temp = temp.Replace("[BookPages]", reader["BookPages"].ToString());

                        temp = temp.Replace("[BookPublisher]", reader["BookPublisher"].ToString());

                        if ((Language)Convert.ToInt32(reader["BookLanguage"].ToString()) == Language.Persian)
                        {
                            temp = temp.Replace("[dir]", "rtl");
                            temp = temp.Replace("[align]", "right");
                        }
                        else
                        {
                            temp = temp.Replace("[dir]", "ltr");
                            temp = temp.Replace("[align]", "left");
                        }
                        string BookImage = (string)reader["BookImage"];
                        /*if (BookImage == "default")
                            temp = temp.Replace("[img]", String.Format("{0}book-thumbs/defaults/middle.gif", constants.BookImagesURLPath));
                        else
                            temp = temp.Replace("[img]", String.Format("{0}BookImageHandler.aspx?guid={1}&mode=middle", constants.BookImagesURLPath, BookImage));*/
                        temp = temp.Replace("[img]", String.Format("{0}/{1}.jpg", constants.BookImagesURLPath, (string)reader["IDENTIFIER"]));
                        this.Response.Write(temp);
                        this.Response.Flush();
                    }
                    //else
                      //  this.Response.Write("false");
                    //temp = temp.Replace("[id]", (string)_searches[i]);
                    //temp = temp.Replace("[id]", i.ToString());
                    reader.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                }

                /*if (currentPage == 1)
                    this.Session["_ItemNumAjaxLinks"] = 0;//(int)command.Parameters["@LinkNum"].Value;*/

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowByBookSearch", currentPage, constants.MaxShowByBookSearch, _searches.Count, constants.ShowByBookSearchPagingNumber, "ShowItems");
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

                return;
            }
        }
        //--------------------------------------------------------------------
        private void DoSwisheSearch(string _sreachPattern)
        {
            if (this.Session["_searches"] == null)
            {
                ArrayList _searches = new ArrayList();
                this.Session.Add("_searches", _searches);
            }
            /*Process[] localByName = Process.GetProcessesByName("swishe.exe");
            if (localByName != null)
                if (localByName.Length > 0)
                    for (int i = 0; i < localByName.Length; i++)
                        localByName[i].Close();*/
            if (constants.SwisheIsLocal)
            {
                string _directory = new FileInfo(this.Request.PhysicalPath).DirectoryName;
                Process _p = new Process();
                _p.StartInfo.Arguments = String.Format("-m {0} -f {1}{2} -w \"{3}\"", constants.SwisheMaxReturn, _directory, constants.SwisheIndexPath, _sreachPattern);
                _p.StartInfo.FileName = String.Format("{0}{1}", _directory, constants.SwishePath);

                //_p.StartInfo.Arguments = String.Format("{4} -m {0} -f {1}{2} -w {3}", constants.SwisheMaxReturn, _directory, constants.SwisheIndexPath, _sreachPattern, String.Format("{0}{1}", _directory, constants.SwishePath));
                // _p.StartInfo.Arguments = "-TIME";//@"D:\hshome\c119368\bookstore\search-pack\swishe.exe";
                //_p.StartInfo.FileName = @"c:\WINDOWS\system32\cmd.exe";

                //WriteStringToAjaxRequest(_p.StartInfo.Arguments); return;
                _p.StartInfo.RedirectStandardOutput = true;
                //_p.StartInfo.RedirectStandardInput = true;
                _p.StartInfo.CreateNoWindow = true;
                _p.StartInfo.UseShellExecute = false;
                _p.Start();
                //string _output = _p.StandardOutput.ReadToEnd();
                string _temp = "";
                int p1, p2 = -1;
                ArrayList __searches = (ArrayList)this.Session["_searches"];
                __searches.Clear();

                while (true)
                {
                    _temp = _p.StandardOutput.ReadLine();
                    /*this.Response.Write(_temp+"<br>");
                    this.Response.Flush();
                    if (_temp == null)
                        break;*/

                    if (_temp == null)
                        break;
                    p1 = _temp.IndexOf('"');
                    if (p1 >= 0)
                    {
                        p2 = _temp.IndexOf('"', p1 + 1);
                        if (p2 > p1)
                        {
                            string[] rets = _temp.Substring(p1 + 1, p2 - p1 - 1).Split(':');
                            if (rets != null)
                                if (rets.Length == 2)
                                    __searches.Add(rets[0]);

                            //this.Response.Write(_temp + " " + rets[0] + "<br>");
                            //this.Response.Flush();
                        }
                    }
                }
                this.Session["_searches"] = __searches;

                _p.StandardOutput.Close();


                _p.WaitForExit();
                GC.Collect();
                //_p.Close();
                //_p = null;
            }
            else
            {
                WebRequest request = WebRequest.Create(constants.SwisheRemoteURL + "?query=" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_sreachPattern)));
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                //string responseFromServer = reader.ReadToEnd();

                string _temp = "";
                int p1, p2 = -1;

                ArrayList __searches = (ArrayList)this.Session["_searches"];
                __searches.Clear();

                while (true)
                {
                    _temp = reader.ReadLine();

                    if (_temp == null)
                        break;
                    p1 = _temp.IndexOf('"');
                    if (p1 >= 0)
                    {
                        p2 = _temp.IndexOf('"', p1 + 1);
                        if (p2 > p1)
                        {
                            string[] rets = _temp.Substring(p1 + 1, p2 - p1 - 1).Split(':');
                            if (rets != null)
                                if (rets.Length == 2)
                                    __searches.Add(rets[0]);

                            //this.Response.Write(_temp + " " + rets[0] + "<br>");
                            //this.Response.Flush();
                        }
                    }
                }
                this.Session["_searches"] = __searches;



                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();

                /*this.Response.Write(responseFromServer);
                this.Response.Flush();
                this.Response.End();*/
            }
        }
        //--------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------
    }
}
