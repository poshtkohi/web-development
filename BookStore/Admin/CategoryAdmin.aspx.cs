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
    public partial class CategoryAdmin : System.Web.UI.Page
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
                    case "ShowCategoryAdmin":
                        ShowCategoryAdmin();
                        return;
                    case "delete":
                        CategoryDelete();
                        return;
                    case "load":
                        CategoryLoad();
                        return;
                    case "update":
                        CategoryUpdate();
                        return;
                    case "post":
                        CategoryInsert();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void CategoryLoad()
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
            command.CommandText = "CategoryLoad_CategoryAdminPage_proc";

            command.Parameters.Add("@CategoryID", SqlDbType.Int);
            command.Parameters["@CategoryID"].Value = _id;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    EnglishCategory
                    PersianCategory
                */
                string _EnglishCategory = (string)reader["EnglishCategory"];// throw new Exception(_EnglishCategory); return;
                string _PersianCategory = (string)reader["PersianCategory"];


                WriteStringToAjaxRequest(String.Format("{0},{1}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_EnglishCategory)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_PersianCategory))
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
        private void CategoryDelete()
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
            command.CommandText = "CategoryDelete_CategoryAdminPage_proc";

            command.Parameters.Add("@CategoryID", SqlDbType.Int);
            command.Parameters["@CategoryID"].Value = _DeleteID;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void CategoryUpdate()
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


            string _EnglishCategory = this.Request.Form["EnglishCategory"];
            string _PersianCategory = this.Request.Form["PersianCategory"];
            //---------user input validations-----------------------------------------------
            if ((_EnglishCategory == null || _EnglishCategory == "") && (_PersianCategory == null || _PersianCategory == ""))
            {
                WriteStringToAjaxRequest(".مقوله کتاب خالی است");
                return;
            }

            if (_EnglishCategory == null)
            {
                _EnglishCategory = "";
            }

            if (_PersianCategory == null)
            {
                _PersianCategory = "";
            }

            if (_EnglishCategory.Length > 400)
            {
                WriteStringToAjaxRequest(".تعداد حروف مقوله به انگلیسی نمی تواند از 400 حرف بیشتر باشد");
                return;
            }

            if (_PersianCategory.Length > 400)
            {
                WriteStringToAjaxRequest(".تعداد حروف مقوله به فارسی نمی تواند از 400 حرف بیشتر باشد");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CategoryUpdate_CategoryAdminPage_proc";


            command.Parameters.Add("@CategoryID", SqlDbType.Int);
            command.Parameters["@CategoryID"].Value = _id;

            command.Parameters.Add("@EnglishCategory", SqlDbType.NVarChar);
            command.Parameters["@EnglishCategory"].Value = _EnglishCategory;

            command.Parameters.Add("@PersianCategory", SqlDbType.NVarChar);
            command.Parameters["@PersianCategory"].Value = _PersianCategory;

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void CategoryInsert()
        {
            string _EnglishCategory = this.Request.Form["EnglishCategory"];
            string _PersianCategory = this.Request.Form["PersianCategory"];

            //---------user input validations-----------------------------------------------
            if ((_EnglishCategory == null || _EnglishCategory == "") && (_PersianCategory == null || _PersianCategory == ""))
            {
                WriteStringToAjaxRequest(".مقوله کتاب خالی است");
                return;
            }

            if (_EnglishCategory == null)
            {
                _EnglishCategory = "";
            }

            if (_PersianCategory == null)
            {
                _PersianCategory = "";
            }

            if (_EnglishCategory.Length > 400)
            {
                WriteStringToAjaxRequest(".تعداد حروف مقوله به انگلیسی نمی تواند از 400 حرف بیشتر باشد");
                return;
            }

            if (_PersianCategory.Length > 400)
            {
                WriteStringToAjaxRequest(".تعداد حروف مقوله به فارسی نمی تواند از 400 حرف بیشتر باشد");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CategoryInsert_CategoryAdminPage_proc";

            command.Parameters.Add("@EnglishCategory", SqlDbType.NVarChar);
            command.Parameters["@EnglishCategory"].Value = _EnglishCategory;

            command.Parameters.Add("@PersianCategory", SqlDbType.NVarChar);
            command.Parameters["@PersianCategory"].Value = _PersianCategory;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".مقوله جدید کتاب با موفقیت ارسال شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowCategoryAdmin()
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumCategoryAdmin"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CategoryShow_CategoryAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxCategoryAdminShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@CategoryNum", SqlDbType.Int);
            command.Parameters["@CategoryNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_CategoryAdmin"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\CategoryAdminTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_CategoryAdmin"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_CategoryAdmin"];
                }

                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\CategoryAdminTemplate.html");
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
                string _EnglishCategory = "" ;
                string _PersianCategory = "";
                while (reader.Read())
                {
                    temp = _mainFormat;

                    _EnglishCategory = reader["EnglishCategory"].ToString();
                    _PersianCategory = reader["PersianCategory"].ToString();

                    if (_EnglishCategory != "" && _PersianCategory != "")
                        _EnglishCategory = String.Format("{0}<br>{1}", _EnglishCategory, _PersianCategory);

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
                    temp = temp.Replace("[CategoryID]", reader["CategoryID"].ToString());
                    temp = temp.Replace("[CategoryText]", _EnglishCategory);

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumCategoryAdmin"] = (int)command.Parameters["@CategoryNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowCategoryAdmin", currentPage, constants.MaxCategoryAdminShow, (int)this.Session["_ItemNumCategoryAdmin"], constants.CategoryAdminPagingNumber, "ShowItems");
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
