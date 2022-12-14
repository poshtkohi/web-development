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

namespace bookstore
{
    public partial class _Default : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _SiteLanguageIsPersian = (bool)this.Session["SiteLanguageIsPersian"];
            bool _IsLogined = Common.IsLoginProc(this);
            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "ShowNewsHomePage":
                        ShowNewsHomePage(_SiteLanguageIsPersian);
                        return;
                    case "ShowNewsDetails":
                        ShowNewsDetails(_SiteLanguageIsPersian);
                        return;
                    case "ShowTopNews":
                        ShowTopNews(_SiteLanguageIsPersian);
                        return;
                    case "ShowAllBooks":
                        ShowAllBooks(_IsLogined, _SiteLanguageIsPersian);
                        return;
                    case "LoadBookDetails":
                        LoadBookDetails(_IsLogined, _SiteLanguageIsPersian);
                        return;
                    case "ListBooksByCategory":
                        ListBooksByCategory(_IsLogined, _SiteLanguageIsPersian);
                        return;
                    case "ShowMainPageCustomers":
                        ShowMainPageCustomers(_SiteLanguageIsPersian);
                        return;
                    case "ShowMainPageSitePages":
                        ShowMainPageSitePages(_SiteLanguageIsPersian);
                        return;
                    case "ShowMainPageSitePageDetailes":
                        ShowMainPageSitePageDetailes(_SiteLanguageIsPersian);
                        return;
                    case "ShowSiteStat":
                        ShowSiteStat(_SiteLanguageIsPersian);
                        return;
                    case "AddToShoppingCart":
                        AddToShoppingCart(_IsLogined, _SiteLanguageIsPersian);
                        return;
                    case "ShowRandomBooks":
                        ShowRandomBooks();
                        return;
                    default:
                        return;
                }
            }
            PageSettings();
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
        private void ShowRandomBooks()
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "RandomBooks_HomePage_proc";

            command.Parameters.Add("@RandomNumbers", SqlDbType.Int);
            command.Parameters["@RandomNumbers"].Value = constants.RandomNumbers;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    images
                    ids
                */
                string images = "new Array(";
                string ids = "new Array(";
                while (reader.Read())
                {
                    images += string.Format("\"{0}\",", String.Format("{0}/{1}.jpg", constants.BookImagesURLPath, reader["IDENTIFIER"].ToString()));
                    ids += string.Format("\"{0}\",", reader["BookID"].ToString());
                }

                images += ")";
                ids += ")";

                images = images.Replace(",)", ")");
                ids = ids.Replace(",)", ")");


                WriteStringToAjaxRequest(String.Format("{0},{1}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(images)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ids))
                    ));

                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void AddToShoppingCart(bool _IsLogined, bool _SiteLanguageIsPersian)
        {
            if (!_IsLogined)
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }
            Int64 _BookID = 1;
            try
            {
                _BookID = Convert.ToInt64(this.Request.Form["BookID"]);
            }
            catch
            {
                if (_SiteLanguageIsPersian)
                {
                    WriteStringToAjaxRequest("خطا در در خواست ورودی");
                    return;
                }
                else
                {
                    WriteStringToAjaxRequest("Error in Input Request");
                    return;
                }
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddToShoppingCart_HomePage_proc";
            //-------------------------------------------

            command.Parameters.Add("@BookID", SqlDbType.BigInt);
            command.Parameters["@BookID"].Value = _BookID;

            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];

            command.Parameters.Add("@RecentlySelected", SqlDbType.Bit);
            command.Parameters["@RecentlySelected"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            if ((bool)command.Parameters["@RecentlySelected"].Value)
            {
                if (_SiteLanguageIsPersian)
                    WriteStringToAjaxRequest(".این کتاب قبلا به سبد خرید شما اضافه شده است");
                else
                    WriteStringToAjaxRequest("This book has been recently added to your shopping cart.");
            }
            else
            {
                if (_SiteLanguageIsPersian)
                    WriteStringToAjaxRequest(".این کتاب با موفقیت به سبد خرید شما اضافه شد");
                else
                    WriteStringToAjaxRequest("This book was added to your shopping cart successfully.");
            }

            connection.Close();
            command.Dispose();

            return;
        }
        //--------------------------------------------------------------------
        private void ShowNewsHomePage(bool _SiteLanguageIsPersian)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemShowNewsHomePage"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "NewsShow_NewsAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxShowNewsHomePage;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@NewsNum", SqlDbType.Int);
            command.Parameters["@NewsNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@NewsLanguage", SqlDbType.Int);
            if (_SiteLanguageIsPersian)
                command.Parameters["@NewsLanguage"].Value = Language.Persian;
            else
                command.Parameters["@NewsLanguage"].Value = Language.English;

            command.Parameters.Add("@IsTopNews", SqlDbType.Bit);
            command.Parameters["@IsTopNews"].Value = false;



            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowNewsHomePage"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowNewsHomePageTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowNewsHomePage"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowNewsHomePage"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowNewsHomePageTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }
                //--Persian or English Language Chane
                //template = template.Replace("dir", "rtl");
                if (_SiteLanguageIsPersian)
                {
                    Common.TagDelete(ref template, "english");
                    template = template.Replace("[dir]", "rtl");
                    //template = template.Replace("[align]", "right");
                }
                else
                {
                    Common.TagDelete(ref template, "persian");
                    template = template.Replace("[dir]", "ltr");
                    //template = template.Replace("[align]", "left");
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

                string temp = null;
                bool boxing = true;
                DateTime dt;
                PersianCalendar pc = new PersianCalendar();
                while (reader.Read())
                {
                    temp = _mainFormat;

                    if (boxing)
                    {
                        temp = temp.Replace("[boxing]", constants.boxing1);
                        boxing = false;
                    }
                    else
                    {
                        temp = temp.Replace("[boxing]", constants.boxing2);
                        boxing = true;
                    }
                    dt = (DateTime)reader["date"];
                    temp = temp.Replace("[NewsID]", reader["NewsID"].ToString());
                    temp = temp.Replace("[NewsTitle]", reader["NewsTitle"].ToString());
                    temp = temp.Replace("[date]", String.Format("{0}/{1}/{2}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt)));

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemShowNewsHomePage"] = (int)command.Parameters["@NewsNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowNewsHomePage", currentPage, constants.MaxShowNewsHomePage, (int)this.Session["_ItemShowNewsHomePage"], constants.ShowNewsHomePagePagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                connection.Close();
                command.Dispose();
                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();
                command.Dispose();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowAllBooks(bool _IsLogined, bool _SiteLanguageIsPersian)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumShowAllBooks"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowAllBooks_HomePage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxShowAllBooks;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@BookNum", SqlDbType.Int);
            command.Parameters["@BookNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowAllBooks"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowAllBooksTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowAllBooks"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowAllBooks"];
                }
                else
                {

                    StreamReader sr_ = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowAllBooksTemplate.html");
                    template = sr_.ReadToEnd();
                    sr_.Close();
                }

                if (_IsLogined)
                {
                    if (_SiteLanguageIsPersian)
                    {
                        Common.TagDelete(ref template, "english");
                        Common.TagDelete(ref template, "english");
                        template = template.Replace("[dir-all]", "rtl");
                    }
                    else
                    {
                        Common.TagDelete(ref template, "persian");
                        Common.TagDelete(ref template, "persian");
                        template = template.Replace("[dir-all]", "ltr");
                    }
                }
                else
                    Common.TagDelete(ref template, "logined");

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
                string BookAbstract = null;
                string BookImage = null;

                while (reader.Read())
                {
                    temp = _mainFormat;
                    BookAbstract = (string)reader["BookTitle"];
                    BookImage = (string)reader["BookImage"];

                    temp = temp.Replace("[BookID]", reader["BookID"].ToString());

                    string _title = reader["BookTitle"].ToString();
                    if (_title.Length > 30)
                        _title = _title.Substring(0, 30);
                    temp = temp.Replace("[BookTitle]", _title);

                    string BookWriter = "";
                    BookWriter = reader["BookWriter"].ToString();
                    if (BookWriter.Length > 30)
                        BookWriter = String.Format("{0} ...", BookWriter.Substring(0, 30));

                    temp = temp.Replace("[BookWriter]", BookWriter);

                    temp = temp.Replace("[ISBN]", reader["BookISBN"].ToString());
                    int _year = (int)reader["BookPublishDate"];
                    if (_year <= 0)
                        Common.TagDelete(ref temp, "year");
                    else
                        temp = temp.Replace("[PublishDate]", _year.ToString());

                    if (BookAbstract.Length > 30)
                        BookAbstract = String.Format("{0} ...", BookAbstract.Substring(0, 30));

                    temp = temp.Replace("[BookAbstract]", BookAbstract);

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

                    /*if (BookImage == "default")
                        temp = temp.Replace("[img]", String.Format("{0}book-thumbs/defaults/small.gif", constants.BookImagesURLPath));
                    else
                        temp = temp.Replace("[img]", String.Format("{0}BookImageHandler.aspx?guid={1}&mode=small", constants.BookImagesURLPath, BookImage));*/

                    temp = temp.Replace("[img]", String.Format("{0}/{1}.jpg", constants.BookImagesURLPath, (string)reader["IDENTIFIER"]));

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumShowAllBooks"] = (int)command.Parameters["@BookNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowAllBooks", currentPage, constants.MaxShowAllBooks, (int)this.Session["_ItemNumShowAllBooks"], constants.ShowAllBooksPagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                connection.Close();
                command.Dispose();
                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();
                command.Dispose();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowNewsDetails(bool _SiteLanguageIsPersian)
        {
            //throw new Exception("hi");
            int _NewsID = 1;
            try
            {
                _NewsID = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (_NewsID == 0)
                _NewsID++;

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowNewsDetails_HomePage_proc";
            //-------------------------------------------

            command.Parameters.Add("@NewsID", SqlDbType.Int);
            command.Parameters["@NewsID"].Value = _NewsID;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_NewsDetailsTemplate"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\NewsDetailsTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_NewsDetailsTemplate"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_NewsDetailsTemplate"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\NewsDetailsTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                PersianCalendar pc = new PersianCalendar();
                reader.Read();

                DateTime dt = (DateTime)reader["date"];
                template = template.Replace("[NewsTitle]", reader["NewsTitle"].ToString());
                template = template.Replace("[image]", String.Format("{0}news/{1}.jpg", constants.UploadedImagesPath, reader["NewsImageGuid"].ToString()));
                template = template.Replace("[NewsContent]", reader["NewsContent"].ToString());
                template = template.Replace("[date]", String.Format("{0}/{1}/{2}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt)));

                this.Response.Write(template);
                this.Response.Flush();

                reader.Close();
                connection.Close();
                command.Dispose();

                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();
                command.Dispose();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowTopNews(bool _SiteLanguageIsPersian)
        {
            //throw new Exception("hi");
            int _NewsID = 1;
            try
            {
                _NewsID = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (_NewsID == 0)
                _NewsID++;

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowTopNews_HomePage_proc";
            //-------------------------------------------

            command.Parameters.Add("@NewsLanguage", SqlDbType.Int);
            if (_SiteLanguageIsPersian)
                command.Parameters["@NewsLanguage"].Value = Language.Persian;
            else
                command.Parameters["@NewsLanguage"].Value = Language.English;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowTopNews"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowTopNewsTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowTopNews"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowTopNews"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowTopNewsTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                if (_SiteLanguageIsPersian)
                {
                    //Common.TagDelete(ref template, "english");
                    template = template.Replace("[dir]", "rtl");
                    template = template.Replace("[align]", "right");
                }
                else
                {
                    //Common.TagDelete(ref template, "persian");
                    template = template.Replace("[dir]", "ltr");
                    template = template.Replace("[align]", "left");
                }

                reader.Read();

                DateTime dt = (DateTime)reader["date"];
                template = template.Replace("[NewsTitle]", reader["NewsTitle"].ToString());
                template = template.Replace("[image]", String.Format("{0}news/{1}.jpg", constants.UploadedImagesPath, reader["NewsImageGuid"].ToString()));
                template = template.Replace("[NewsContent]", reader["NewsContent"].ToString());
                if (_SiteLanguageIsPersian)
                {
                    PersianCalendar pc = new PersianCalendar();
                    template = template.Replace("[date]", String.Format("{0}/{1}/{2}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt)));
                }
                else
                    template = template.Replace("[date]", String.Format("{2}/{1}/{0}", dt.Year, dt.Month, dt.Day));

                this.Response.Write(template);
                this.Response.Flush();

                reader.Close();
                connection.Close();
                command.Dispose();

                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();
                command.Dispose();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowMainPageSitePageDetailes(bool _SiteLanguageIsPersian)
        {
            Int64 _PageID = 1;
            try
            {
                _PageID = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (_PageID == 0)
                _PageID++;

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageLoad_PagesAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageID", SqlDbType.BigInt);
            command.Parameters["@PageID"].Value = _PageID;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowMainPageSitePageDetailes"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowMainPageSitePageDetailesTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowMainPageSitePageDetailes"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowMainPageSitePageDetailes"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowMainPageSitePageDetailesTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }



                if (_SiteLanguageIsPersian)
                {
                    template = template.Replace("[dir]", "rtl");
                    template = template.Replace("[align]", "right");
                }
                else
                {
                    template = template.Replace("[dir]", "ltr");
                    template = template.Replace("[align]", "left");
                }
                reader.Read();

                template = template.Replace("[PageTitle]", reader["PageTitle"].ToString());
                template = template.Replace("[PageContent]", reader["PageContent"].ToString());

                this.Response.Write(template);
                this.Response.Flush();

                reader.Close();
                connection.Close();
                command.Dispose();

                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();
                command.Dispose();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void LoadBookDetails(bool _IsLogined, bool _SiteLanguageIsPersian)
        {
            Int64 _BookID = 1;
            try
            {
                _BookID = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch { }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BooksLoad_BooksAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@BookID", SqlDbType.BigInt);
            command.Parameters["@BookID"].Value = _BookID;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowBoolDetailsTemplate"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowBoolDetailsTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowBoolDetailsTemplate"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowBoolDetailsTemplate"];

                }
                else
                {
                    StreamReader sr_ = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowBoolDetailsTemplate.html");
                    template = sr_.ReadToEnd();
                    sr_.Close();
                }

                template = template.Replace("[BookID]", _BookID.ToString());

                if (_IsLogined)
                {
                    if (_SiteLanguageIsPersian)
                        Common.TagDelete(ref template, "english");
                    else
                        Common.TagDelete(ref template, "persian");
                }
                else
                {
                    Common.TagDelete(ref template, "logined");
                    if (_SiteLanguageIsPersian)
                        Common.TagDelete(ref template, "english");
                    else
                        Common.TagDelete(ref template, "persian");
                }

                reader.Read();

                //template = template.Replace("[EnglishCategory]", Common.CategoriesGenerator(reader["EnglishCategory"].ToString(), Language.English));
                //template = template.Replace("[PersianCategory]", Common.CategoriesGenerator(reader["PersianCategory"].ToString(), Language.Persian));

                template = template.Replace("[Title]", reader["Title"].ToString());
                template = template.Replace("[Writer]", reader["Writer"].ToString());
                template = template.Replace("[Translator]", reader["Translator"].ToString());
                template = template.Replace("[Publisher]", reader["Publisher"].ToString());

                int PublishDate = (int)reader["PublishDate"];
                if (PublishDate <= 0)
                    template = template.Replace("[PublishDate]", "-");
                else
                    template = template.Replace("[PublishDate]", PublishDate.ToString());

                int pages = (int)reader["Pages"];
                if(pages <= 0)
                    template = template.Replace("[Pages]", "-");
                else
                    template = template.Replace("[Pages]", pages.ToString());

                template = template.Replace("[ISBN]", reader["ISBN"].ToString());
                template = template.Replace("[FileType]", reader["FileType"].ToString());
                template = template.Replace("[FileSize]", reader["FileSize"].ToString());
                template = template.Replace("[Price]", reader["Price"].ToString());
                template = template.Replace("[Abstract]", reader["Abstract"].ToString());
                template = template.Replace("[filename]", reader["filename"].ToString());
                if ((Language)Convert.ToInt32(reader["Language"].ToString()) == Language.Persian)
                {
                    template = template.Replace("[dir]", "rtl");
                    template = template.Replace("[align]", "right");
                }
                else
                {
                    template = template.Replace("[dir]", "ltr");
                    template = template.Replace("[align]", "left");
                }
                /*string BookImage = (string)reader["BookImage"];
                if (BookImage == "default")
                    template = template.Replace("[img]", String.Format("{0}book-thumbs/defaults/middle.gif", constants.BookImagesURLPath));
                else
                    template = template.Replace("[img]", String.Format("{0}BookImageHandler.aspx?guid={1}&mode=middle", constants.BookImagesURLPath, BookImage));*/

                template = template.Replace("[img]", String.Format("{0}/{1}.jpg", constants.BookImagesURLPath, reader["IDENTIFIER"].ToString()));

                this.Response.Write(template);
                this.Response.Flush();

                reader.Close();
                connection.Close();
                command.Dispose();

                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();
                command.Dispose();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowMainPageCustomers(bool _SiteLanguageIsPersian)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumShowMainPageCustomers"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CustomersShow_CustomersAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxShowMainPageCustomers;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@CustomersNum", SqlDbType.Int);
            command.Parameters["@CustomersNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowMainPageCustomers"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowMainPageCustomersTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowMainPageCustomers"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowMainPageCustomers"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowMainPageCustomersTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                if (_SiteLanguageIsPersian)
                {
                    template = template.Replace("[dir]", "rtl");
                    template = template.Replace("[align]", "right");
                }
                else
                {
                    template = template.Replace("[dir]", "ltr");
                    template = template.Replace("[align]", "left");
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

                string temp = null;
                bool boxing = true;
                while (reader.Read())
                {
                    temp = _mainFormat;


                    if (boxing)
                    {
                        temp = temp.Replace("[boxing]", constants.boxing1);
                        boxing = false;
                    }
                    else
                    {
                        temp = temp.Replace("[boxing]", constants.boxing2);
                        boxing = true;
                    }
                    temp = temp.Replace("[CustomerID]", reader["CustomerID"].ToString());

                    if (_SiteLanguageIsPersian)
                        temp = temp.Replace("[CustomerName]", reader["PersianCustomerName"].ToString());
                    else
                        temp = temp.Replace("[CustomerName]", reader["EnglishCustomerName"].ToString());
                    temp = temp.Replace("[CustomerLink]", reader["CustomerLink"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumShowMainPageCustomers"] = (int)command.Parameters["@CustomersNum"].Value;

                if ((int)this.Session["_ItemNumShowMainPageCustomers"] / constants.MaxShowMainPageCustomers > 1)
                {
                    if (_p1Paging > 0 && _p2Paging > 0)
                    {
                        this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                        //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                        Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowMainPageCustomers", currentPage, constants.MaxShowMainPageCustomers, (int)this.Session["_ItemNumShowMainPageCustomers"], constants.ShowMainPageCustomersPagingNumber, "ShowItems");
                    }
                    else
                        this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                }
                else
                {
                    if (_p1Paging > 0 && _p2Paging > 0)
                    {
                        template = template.Replace("[paging]", "        ");
                    }
                    else
                        this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                }
                this.Response.Flush();

                connection.Close();
                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowMainPageSitePages(bool _SiteLanguageIsPersian)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumShowMainPageSitePages"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowMainPageSitePages_HomePage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxShowMainPageSitePages;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PagesNum", SqlDbType.Int);
            command.Parameters["@PagesNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@PageLanguage", SqlDbType.Int);
            if (_SiteLanguageIsPersian)
                command.Parameters["@PageLanguage"].Value = (int)Language.Persian;
            else
                command.Parameters["@PageLanguage"].Value = (int)Language.English;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowMainPageSitePages"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowMainPageSitePagesTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowMainPageSitePages"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowMainPageCustomers"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowMainPageSitePagesTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                if (_SiteLanguageIsPersian)
                {
                    template = template.Replace("[dir]", "rtl");
                    template = template.Replace("[align]", "right");
                }
                else
                {
                    template = template.Replace("[dir]", "ltr");
                    template = template.Replace("[align]", "left");
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

                string temp = null;
                bool boxing = true;
                while (reader.Read())
                {
                    temp = _mainFormat;

                    if (boxing)
                    {
                        temp = temp.Replace("[boxing]", constants.boxing1);
                        boxing = false;
                    }
                    else
                    {
                        temp = temp.Replace("[boxing]", constants.boxing2);
                        boxing = true;
                    }
                    temp = temp.Replace("[PageID]", reader["PageID"].ToString());
                    temp = temp.Replace("[PageTitle]", reader["PageTitle"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumShowMainPageSitePages"] = (int)command.Parameters["@PagesNum"].Value;

                if ((int)this.Session["_ItemNumShowMainPageSitePages"] / constants.MaxShowMainPageSitePages > 1)
                {
                    if (_p1Paging > 0 && _p2Paging > 0)
                    {
                        this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                        //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                        Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowMainPageSitePages", currentPage, constants.MaxShowMainPageSitePages, (int)this.Session["_ItemNumShowMainPageSitePages"], constants.ShowMainPageSitePagesPagingNumber, "ShowItems");
                    }
                    else
                    {
                        this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                    }
                }
                else
                {
                    if (_p1Paging > 0 && _p2Paging > 0)
                    {
                        template = template.Replace("[paging]", "        ");
                    }
                    else
                        this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                }
                this.Response.Flush();

                connection.Close();
                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void ShowSiteStat(bool _SiteLanguageIsPersian)
        {
            string template = "";
            if (constants.CacheTemplateEnbaled)
            {
                if (this.Cache["_template_SiteStat"] == null)
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\SiteStatTemplate.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_SiteStat"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_SiteStat"];
            }
            else
            {
                StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\SiteStatTemplate.html");
                template = sr.ReadToEnd();
                sr.Close();
            }

            if (_SiteLanguageIsPersian)
            {
                Common.TagDelete(ref template, "english");
                template = template.Replace("[dir]", "rtl");
                template = template.Replace("[align]", "right");
            }
            else
            {
                Common.TagDelete(ref template, "farsi");
                template = template.Replace("[dir]", "ltr");
                template = template.Replace("[align]", "left");
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SiteStat_HomePage_proc";
            //-------------------------------------------

            command.Parameters.Add("@BooksNum", SqlDbType.BigInt);
            command.Parameters["@BooksNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@UsersNum", SqlDbType.BigInt);
            command.Parameters["@UsersNum"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            template = template.Replace("[BooksNum]", command.Parameters["@BooksNum"].Value.ToString());
            template = template.Replace("[UsersNum]", command.Parameters["@UsersNum"].Value.ToString());

            connection.Close();
            command.Dispose();

            WriteStringToAjaxRequest(template);
            return;
        }
        //--------------------------------------------------------------------
        private void ListBooksByCategory(bool _IsLogined, bool _SiteLanguageIsPersian)
        {
            WriteStringToAjaxRequest("NoFoundPost");
            return;
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
