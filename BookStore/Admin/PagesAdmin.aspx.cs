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
    public partial class PagesAdmin : System.Web.UI.Page
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
                    case "ShowPagesAdmin":
                        ShowPagesAdmin();
                        return;
                    case "delete":
                        PageDelete();
                        return;
                    case "load":
                        PageLoad();
                        return;
                    case "update":
                        PageUpdate();
                        return;
                    case "post":
                        PageInsert();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void PageLoad()
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
            command.CommandText = "PageLoad_PagesAdminPage_proc";

            command.Parameters.Add("@PageID", SqlDbType.BigInt);
            command.Parameters["@PageID"].Value = _id;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    PageTitle
                    PageContent
                    PageLanguage
                */
                string _PageTitle = reader["PageTitle"].ToString();
                string _PageContent = reader["PageContent"].ToString();
                string _PageLanguage = reader["PageLanguage"].ToString();


                WriteStringToAjaxRequest(String.Format("{0},{1},{2}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_PageTitle)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_PageContent)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_PageLanguage))
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
        private void PageDelete()
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
            command.CommandText = "PageDelete_PagesAdminPage_proc";

            command.Parameters.Add("@PageID", SqlDbType.Int);
            command.Parameters["@PageID"].Value = _DeleteID;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void PageUpdate()
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


            string _PageTitle = this.Request.Form["PageTitle"];
            string _PageContent = this.Request.Form["PageContent"];
            string _PageLanguage = this.Request.Form["PageLanguage"];
            //---------user input validations-----------------------------------------------
            if (_PageTitle == null || _PageTitle == "")
            {
                WriteStringToAjaxRequest(".عنوان صفحه خالی است");
                return;
            }

            if (_PageContent == null || _PageContent == "")
            {
                WriteStringToAjaxRequest(".محتوی صفحه خالی است");
                return;
            }
            if (_PageLanguage == null || _PageLanguage == "")
            {
                WriteStringToAjaxRequest(".زبان صفحه باید انتخاب شود");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageUpdate_PagesAdminPage_proc";


            command.Parameters.Add("@PageID", SqlDbType.Int);
            command.Parameters["@PageID"].Value = _id;

            command.Parameters.Add("@PageTitle", SqlDbType.NVarChar);
            command.Parameters["@PageTitle"].Value = _PageTitle;

            command.Parameters.Add("@PageContent", SqlDbType.NText);
            command.Parameters["@PageContent"].Value = _PageContent;

            command.Parameters.Add("@PageLanguage", SqlDbType.Int);
            command.Parameters["@PageLanguage"].Value = Convert.ToInt32(_PageLanguage);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void PageInsert()
        {
            string _PageTitle = this.Request.Form["PageTitle"];
            string _PageContent = this.Request.Form["PageContent"];
            string _PageLanguage = this.Request.Form["PageLanguage"];
            //---------user input validations-----------------------------------------------
            if (_PageTitle == null || _PageTitle == "")
            {
                WriteStringToAjaxRequest(".عنوان صفحه خالی است");
                return;
            }

            if (_PageContent == null || _PageContent == "")
            {
                WriteStringToAjaxRequest(".محتوی صفحه خالی است");
                return;
            }
            if (_PageLanguage == null || _PageLanguage == "")
            {
                WriteStringToAjaxRequest(".زبان صفحه باید انتخاب شود");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageInsert_PagesAdminPage_proc";

            command.Parameters.Add("@PageTitle", SqlDbType.NVarChar);
            command.Parameters["@PageTitle"].Value = _PageTitle;

            command.Parameters.Add("@PageContent", SqlDbType.NText);
            command.Parameters["@PageContent"].Value = _PageContent;

            command.Parameters.Add("@PageLanguage", SqlDbType.Int);
            command.Parameters["@PageLanguage"].Value = Convert.ToInt32(_PageLanguage);


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".صفحه جدید با موفقیت ارسال شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowPagesAdmin()
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumPageAdmin"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PageShow_PagesAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxPageAdminShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PagesNum", SqlDbType.Int);
            command.Parameters["@PagesNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_PageAdmin"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PagesAdminTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_PageAdmin"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_PageAdmin"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PagesAdminTemplate.html");
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
                    temp = temp.Replace("[PageID]", reader["PageID"].ToString());
                    temp = temp.Replace("[PageTitle]", reader["PageTitle"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumPageAdmin"] = (int)command.Parameters["@PagesNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowPagesAdmin", currentPage, constants.MaxPageAdminShow, (int)this.Session["_ItemNumPageAdmin"], constants.PageAdminPagingNumber, "ShowItems");
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
