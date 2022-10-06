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

using System.Data.SqlClient;
using System.IO;
using bookstore;

namespace bookstore.admin
{
    public partial class BooksAdmin : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
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

            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "ShowBooksAdmin":
                        ShowBooksAdmin();
                        return;
                    case "delete":
                        BooksDelete();
                        return;
                    case "load":
                        BooksLoad();
                        return;
                    case "update":
                        BooksUpdate();
                        return;
                    case "post":
                        BooksInsert();
                        return;
                    default:
                        return;
                }
            }
            /*else
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BooksLoadCategories_BooksAdminPage_proc";

                //-------------------------------------------
                SqlDataReader reader = command.ExecuteReader();
                bool HasSubjectedArchive = false;
                if (reader.HasRows)
                {
                    string _EnglishCategory = "" ;
                    string _PersianCategory = "";
                    while (reader.Read())
                    {
                        _EnglishCategory = reader["EnglishCategory"].ToString();
                        _PersianCategory = reader["PersianCategory"].ToString();

                        if (_EnglishCategory != "" && _PersianCategory != "")
                            _EnglishCategory = String.Format("{0}\t{1}", _EnglishCategory, _PersianCategory);
                        ListItem item = new ListItem(_EnglishCategory, ((Int64)reader["CategoryID"]).ToString());
                        this.CategoryID.Items.Add(item);
                    }
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                return;
            }*/
        }
        //--------------------------------------------------------------------------------
        private void BooksLoad()
        {
            Int64 _id = -1;
            try
            {
                _id = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch { return; }

            //throw new Exception(_id.ToString());

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BooksLoad_BooksAdminPage_proc";

            command.Parameters.Add("@BookID", SqlDbType.BigInt);
            command.Parameters["@BookID"].Value = _id;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    EnglishCategory
                    PersianCategory
                    Title
                    Writer
                    Translator
                    Publisher
                    PublishDate
                    Pages
                    ISBN
                    FileType
                    FileSize
                    Price
                    Abstract
                    filename
                    Language
                */

                string EnglishCategory = reader["EnglishCategory"].ToString();
                string PersianCategory = reader["PersianCategory"].ToString();
                string Title = reader["Title"].ToString();
                string Writer = reader["Writer"].ToString();
                string Translator = reader["Translator"].ToString();
                string Publisher = reader["Publisher"].ToString();
                string PublishDate = reader["PublishDate"].ToString();
                string Pages = reader["Pages"].ToString();
                string ISBN = reader["ISBN"].ToString();
                string FileType = reader["FileType"].ToString();
                string FileSize = reader["FileSize"].ToString();
                string Price = reader["Price"].ToString();
                string Abstract = reader["Abstract"].ToString();
                string filename = reader["filename"].ToString();
                string Language = reader["Language"].ToString();


                WriteStringToAjaxRequest(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(EnglishCategory)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(PersianCategory)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Title)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Writer)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Translator)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Publisher)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(PublishDate)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Pages)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ISBN)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(FileType)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(FileSize)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Price)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Abstract)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(filename)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Language))
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
        //--------------------------------------------------------------------------------
        private void BooksDelete()
        {
            Int32 _DeleteID = -1;
            try
            {
                _DeleteID = Convert.ToInt32(this.Request.Form["DeleteID"]);
            }
            catch
            {
                WriteStringToAjaxRequest(".(DeleteID) خطا در درخواست ورودی");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BooksDelete_BooksAdminPage_proc";

            command.Parameters.Add("@BookID", SqlDbType.Int);
            command.Parameters["@BookID"].Value = _DeleteID;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void BooksUpdate()
        {
            Int32 _id = -1;
            try
            {
                _id = Convert.ToInt32(this.Request.Form["id"]);
            }
            catch
            {
                WriteStringToAjaxRequest(".(id) خطا در درخواست ورودی");
                return;
            }

            string EnglishCategory = "";
            string PersianCategory = "";
            string Title = "";
            string Writer = "";
            string Translator = "";
            string Publisher = "";
            int PublishDate = 2000;
            int Pages = 0;
            string ISBN = "";
            int FileType = 0;
            int FileSize = 0;
            int Price = 0;
            string Abstract = "";
            string filename = "";
            int Language = 0;

            EnglishCategory = this.Request.Form["EnglishCategory"];
            PersianCategory = this.Request.Form["PersianCategory"];
            Title = this.Request.Form["Title"];
            Writer = this.Request.Form["Writer"];
            Translator = this.Request.Form["Translator"];
            Publisher = this.Request.Form["Publisher"];
            try { PublishDate = Convert.ToInt32(this.Request.Form["PublishDate"]); } catch { }
            try { Pages = Convert.ToInt32(this.Request.Form["Pages"]); }
            catch { }
            ISBN = this.Request.Form["ISBN"];
            try { FileType = Convert.ToInt32(this.Request.Form["FileType"]); } catch { }
            try { FileSize = Convert.ToInt32(this.Request.Form["FileSize"]); } catch { }
            try { Price = Convert.ToInt32(this.Request.Form["Price"]); } catch { }
            Abstract = this.Request.Form["Abstract"];
            filename = this.Request.Form["filename"];
            try { Language = Convert.ToInt32(this.Request.Form["Language"]); }
            catch { }
            //---------user input validations-----------------------------------------------
            if (Title == null || Title == "")
            {
                WriteStringToAjaxRequest(".عنوان کتاب خالی است");
                return;
            }
            /*if (CategoryID <= 0)
            {
                WriteStringToAjaxRequest(".یک مقوله انتخاب کنید");
                return;
            }*/
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BooksUpdate_BooksAdminPage_proc";


            command.Parameters.Add("@BookID", SqlDbType.BigInt);
            command.Parameters["@BookID"].Value = _id;

            command.Parameters.Add("@EnglishCategory", SqlDbType.NVarChar);
            command.Parameters["@EnglishCategory"].Value = EnglishCategory;

            command.Parameters.Add("@PersianCategory", SqlDbType.NVarChar);
            command.Parameters["@PersianCategory"].Value = PersianCategory;

            command.Parameters.Add("@Title", SqlDbType.NVarChar);
            command.Parameters["@Title"].Value = Title;

            command.Parameters.Add("@Writer", SqlDbType.NVarChar);
            command.Parameters["@Writer"].Value = Writer;


            command.Parameters.Add("@Translator", SqlDbType.NVarChar);
            command.Parameters["@Translator"].Value = Translator;

            command.Parameters.Add("@Publisher", SqlDbType.NVarChar);
            command.Parameters["@Publisher"].Value = Publisher;

            command.Parameters.Add("@PublishDate", SqlDbType.Int);
            command.Parameters["@PublishDate"].Value = PublishDate;

            command.Parameters.Add("@Pages", SqlDbType.Int);
            command.Parameters["@Pages"].Value = Pages;

            command.Parameters.Add("@ISBN", SqlDbType.NVarChar);
            command.Parameters["@ISBN"].Value = ISBN;

            command.Parameters.Add("@FileType", SqlDbType.Int);
            command.Parameters["@FileType"].Value = FileType;

            command.Parameters.Add("@FileSize", SqlDbType.Int);
            command.Parameters["@FileSize"].Value = FileSize;

            command.Parameters.Add("@Price", SqlDbType.Int);
            command.Parameters["@Price"].Value = Price;

            command.Parameters.Add("@Abstract", SqlDbType.NText);
            command.Parameters["@Abstract"].Value = Abstract;

            command.Parameters.Add("@filename", SqlDbType.NText);
            command.Parameters["@filename"].Value = filename;

            command.Parameters.Add("@Language", SqlDbType.Int);
            command.Parameters["@Language"].Value = Language;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void BooksInsert()
        {
            string EnglishCategory = "";
            string PersianCategory = "";
            string Title = "";
            string Writer = "";
            string Translator = "";
            string Publisher = "";
            int PublishDate = 2000;
            int Pages = 0;
            string ISBN = "";
            int FileType = 0;
            int FileSize = 0;
            int Price = 0;
            string Abstract = "";
            string filename = "";
            int Language = 0;

            EnglishCategory = this.Request.Form["EnglishCategory"];
            PersianCategory = this.Request.Form["PersianCategory"];
            Title = this.Request.Form["Title"];
            Writer = this.Request.Form["Writer"];
            Translator = this.Request.Form["Translator"];
            Publisher = this.Request.Form["Publisher"];
            try { PublishDate = Convert.ToInt32(this.Request.Form["PublishDate"]); }
            catch{ }
            try { Pages = Convert.ToInt32(this.Request.Form["Pages"]); }
            catch { }
            ISBN = this.Request.Form["ISBN"];
            try { FileType = Convert.ToInt32(this.Request.Form["FileType"]); }
            catch { }
            try { FileSize = Convert.ToInt32(this.Request.Form["FileSize"]); }
            catch { }
            try { Price = Convert.ToInt32(this.Request.Form["Price"]); }
            catch { }
            Abstract = this.Request.Form["Abstract"];
            filename = this.Request.Form["filename"];
            try { Language = Convert.ToInt32(this.Request.Form["Language"]); }
            catch { }
            //---------user input validations-----------------------------------------------
            if (Title == null || Title == "")
            {
                WriteStringToAjaxRequest(".عنوان کتاب خالی است");
                return;
            }
            /*if (CategoryID <= 0)
            {
                WriteStringToAjaxRequest(".یک مقوله انتخاب کنید");
                return;
            }*/
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BooksInsert_BooksAdminPage_proc";

            //command.Parameters.Add("@CategoryID", SqlDbType.BigInt);
            //command.Parameters["@CategoryID"].Value = CategoryID;

            command.Parameters.Add("@EnglishCategory", SqlDbType.NVarChar);
            command.Parameters["@EnglishCategory"].Value = EnglishCategory;

            command.Parameters.Add("@PersianCategory", SqlDbType.NVarChar);
            command.Parameters["@PersianCategory"].Value = PersianCategory;

            command.Parameters.Add("@Title", SqlDbType.NVarChar);
            command.Parameters["@Title"].Value = Title;

            command.Parameters.Add("@Writer", SqlDbType.NVarChar);
            command.Parameters["@Writer"].Value = Writer;


            command.Parameters.Add("@Translator", SqlDbType.NVarChar);
            command.Parameters["@Translator"].Value = Translator;

            command.Parameters.Add("@Publisher", SqlDbType.NVarChar);
            command.Parameters["@Publisher"].Value = Publisher;

            command.Parameters.Add("@PublishDate", SqlDbType.Int);
            command.Parameters["@PublishDate"].Value = PublishDate;

            command.Parameters.Add("@Pages", SqlDbType.Int);
            command.Parameters["@Pages"].Value = Pages;

            command.Parameters.Add("@ISBN", SqlDbType.NVarChar);
            command.Parameters["@ISBN"].Value = ISBN;

            command.Parameters.Add("@FileType", SqlDbType.Int);
            command.Parameters["@FileType"].Value = FileType;

            command.Parameters.Add("@FileSize", SqlDbType.Int);
            command.Parameters["@FileSize"].Value = FileSize;

            command.Parameters.Add("@Price", SqlDbType.Int);
            command.Parameters["@Price"].Value = Price;

            command.Parameters.Add("@Abstract", SqlDbType.NText);
            command.Parameters["@Abstract"].Value = Abstract;

            command.Parameters.Add("@filename", SqlDbType.NText);
            command.Parameters["@filename"].Value = filename;

            command.Parameters.Add("@Language", SqlDbType.Int);
            command.Parameters["@Language"].Value = Language;



            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".کتاب جدید با موفقیت ارسال شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowBooksAdmin()
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumBooksAdmin"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BooksShow_BooksAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxBooksAdminShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@BooksNum", SqlDbType.Int);
            command.Parameters["@BooksNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_BooksAdmin"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\BooksAdminTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_BooksAdmin"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_BooksAdmin"];

                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\BooksAdminTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
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
                    temp = temp.Replace("[BookID]", reader["BookID"].ToString());
                    temp = temp.Replace("[Title]", reader["Title"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumBooksAdmin"] = (int)command.Parameters["@BooksNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowBooksAdmin", currentPage, constants.MaxBooksAdminShow, (int)this.Session["_ItemNumBooksAdmin"], constants.BooksAdminPagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
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
        //--------------------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------------------
    }
}

