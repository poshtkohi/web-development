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
    public partial class NewsAdmin : System.Web.UI.Page
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
                    case "ShowNewsAdmin":
                        ShowNewsAdmin();
                        return;
                    case "delete":
                        NewsDelete();
                        return;
                    case "load":
                        NewsLoad();
                        return;
                    case "update":
                        NewsUpdate();
                        return;
                    case "post":
                        NewsInsert();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void NewsLoad()
        {
            Int32 _id = -1;
            try
            {
                _id = Convert.ToInt32(this.Request.Form["id"]);
            }
            catch { return; }

            //throw new Exception(_id.ToString());

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "NewsLoad_NewsAdminPage_proc";

            command.Parameters.Add("@NewsID", SqlDbType.Int);
            command.Parameters["@NewsID"].Value = _id;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    NewsTitle
                    NewsContent
                    NewsLanguage
                    IsTopNews
                */
                string _NewsTitle = (string)reader["NewsTitle"];
                string _NewsContent = (string)reader["NewsContent"];
                string _NewsLanguage = reader["NewsLanguage"].ToString();
                string _IsTopNews = "0";
                if ((bool)reader["IsTopNews"])
                    _IsTopNews = "1";


                WriteStringToAjaxRequest(String.Format("{0},{1},{2},{3}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_NewsTitle)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_NewsContent)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_NewsLanguage)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_IsTopNews))
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
        private void NewsDelete()
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
            command.CommandText = "NewsDelete_NewsAdminPage_proc";

            command.Parameters.Add("@NewsID", SqlDbType.Int);
            command.Parameters["@NewsID"].Value = _DeleteID;

            command.Parameters.Add("@NewsImageGuid", SqlDbType.VarChar, 50);
            command.Parameters["@NewsImageGuid"].Direction = ParameterDirection.Output;


            command.ExecuteNonQuery();

            string _guid = (string)command.Parameters["@NewsImageGuid"].Value;

            command.Dispose();
            connection.Close();

            if (_guid != "default")
            {
                string _imageFilename = this.Request.PhysicalApplicationPath + @"\images\uploads\news\" + _guid + ".jpg";
                if (File.Exists(_imageFilename))
                    File.Delete(_imageFilename);
            }
            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void NewsUpdate()
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


            string _NewsTitle = this.Request.Form["NewsTitle"];
            string _NewsContent = this.Request.Form["NewsContent"];
            string _NewsLanguage = this.Request.Form["NewsLanguage"];
            string _IsTopNews = this.Request.Form["IsTopNews"];
            //---------user input validations-----------------------------------------------
            if (_NewsTitle == null || _NewsTitle == "")
            {
                WriteStringToAjaxRequest(".عنوان خبر خالی است");
                return;
            }

            if (_NewsTitle.Length > 400)
            {
                WriteStringToAjaxRequest(".تعداد حروف عنوان خبر نمی تواند از 400 حرف بیشتر باشد");
                return;
            }

            if (_NewsContent == null || _NewsContent == "")
            {
                WriteStringToAjaxRequest(".محتوی خبر خالی است");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "NewsUpdate_NewsAdminPage_proc";


            command.Parameters.Add("@NewsID", SqlDbType.Int);
            command.Parameters["@NewsID"].Value = _id;

            command.Parameters.Add("@NewsTitle", SqlDbType.NVarChar);
            command.Parameters["@NewsTitle"].Value = _NewsTitle;

            command.Parameters.Add("@NewsContent", SqlDbType.NText);
            command.Parameters["@NewsContent"].Value = _NewsContent;

            command.Parameters.Add("@NewsLanguage", SqlDbType.Int);
            command.Parameters["@NewsLanguage"].Value = Convert.ToInt32(_NewsLanguage);

            command.Parameters.Add("@IsTopNews", SqlDbType.Bit);
            if (_IsTopNews == "1")
                command.Parameters["@IsTopNews"].Value = true;
            else
                command.Parameters["@IsTopNews"].Value = false;

            command.Parameters.Add("@TitleIsExisted", SqlDbType.Bit);
            command.Parameters["@TitleIsExisted"].Direction = ParameterDirection.Output;



            command.ExecuteNonQuery();

            bool _success = true;
            if ((bool)command.Parameters["@TitleIsExisted"].Value)
                _success = false;

            command.Dispose();
            connection.Close();

            if (!_success)
                WriteStringToAjaxRequest(".عنوان خبر وجود دارد");
            else
                WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void NewsInsert()
        {
            string _NewsTitle = this.Request.Form["NewsTitle"];
            string _NewsContent = this.Request.Form["NewsContent"];
            string _NewsLanguage = this.Request.Form["NewsLanguage"];
            string _IsTopNews = this.Request.Form["IsTopNews"];

            //---------user input validations-----------------------------------------------
            if (_NewsTitle == null || _NewsTitle == "")
            {
                WriteStringToAjaxRequest(".عنوان خبر خالی است");
                return;
            }

            if (_NewsTitle.Length > 400)
            {
                WriteStringToAjaxRequest(".تعداد حروف عنوان خبر نمی تواند از 400 حرف بیشتر باشد");
                return;
            }

            if (_NewsContent == null || _NewsContent == "")
            {
                WriteStringToAjaxRequest(".محتوی خبر خالی است");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "NewsInsert_NewsAdminPage_proc";

            command.Parameters.Add("@NewsTitle", SqlDbType.NVarChar);
            command.Parameters["@NewsTitle"].Value = _NewsTitle;

            command.Parameters.Add("@NewsContent", SqlDbType.NText);
            command.Parameters["@NewsContent"].Value = _NewsContent;

            command.Parameters.Add("@NewsLanguage", SqlDbType.Int);
            command.Parameters["@NewsLanguage"].Value = Convert.ToInt32(_NewsLanguage);

            command.Parameters.Add("@IsTopNews", SqlDbType.Bit);
            if (_IsTopNews == "1")
                command.Parameters["@IsTopNews"].Value = true;
            else
                command.Parameters["@IsTopNews"].Value = false;

            command.Parameters.Add("@TitleIsExisted", SqlDbType.Bit);
            command.Parameters["@TitleIsExisted"].Direction = ParameterDirection.Output;



            command.ExecuteNonQuery();

            bool _success = true;
            if ((bool)command.Parameters["@TitleIsExisted"].Value)
                _success = false;

            command.Dispose();
            connection.Close();

            if (!_success)
                WriteStringToAjaxRequest(".عنوان خبر وجود دارد");
            else
                WriteStringToAjaxRequest(".خبر جدید با موفقیت ارسال شد");
            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowNewsAdmin()
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumNewsAdmin"] == null)
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
            command.Parameters["@PageSize"].Value = constants.MaxNewsAdminShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@NewsNum", SqlDbType.Int);
            command.Parameters["@NewsNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@NewsLanguage", SqlDbType.Int);
            command.Parameters["@NewsLanguage"].Value = -1;

            command.Parameters.Add("@IsTopNews", SqlDbType.Bit);
            command.Parameters["@IsTopNews"].Value = false;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_NewsAdmin"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\NewsAdminTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_NewsAdmin"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_NewsAdmin"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\NewsAdminTemplate.html");
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
                    this.Session["_ItemNumNewsAdmin"] = (int)command.Parameters["@NewsNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowNewsAdmin", currentPage, constants.MaxNewsAdminShow, (int)this.Session["_ItemNumNewsAdmin"], constants.NewsAdminPagingNumber, "ShowItems");
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
