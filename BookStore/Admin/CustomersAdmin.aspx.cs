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
    public partial class CustomersAdmin : System.Web.UI.Page
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
                    case "ShowCustomersAdmin":
                        ShowCustomersAdmin();
                        return;
                    case "delete":
                        CustomersDelete();
                        return;
                    case "load":
                        CustomersLoad();
                        return;
                    case "update":
                        CustomersUpdate();
                        return;
                    case "post":
                        CustomersInsert();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void CustomersLoad()
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
            command.CommandText = "CustomersLoad_CustomersAdminPage_proc";

            command.Parameters.Add("@CustomerID", SqlDbType.Int);
            command.Parameters["@CustomerID"].Value = _id;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    PersianCustomerName
                    EnglishCustomerName
                    CustomerLink
                */
                string _PersianCustomerName = (string)reader["PersianCustomerName"];
                string _EnglishCustomerName = (string)reader["EnglishCustomerName"];
                string _CustomerLink = (string)reader["CustomerLink"];


                WriteStringToAjaxRequest(String.Format("{0},{1},{2}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_PersianCustomerName)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_EnglishCustomerName)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_CustomerLink))
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
        private void CustomersDelete()
        {
            Int64 _DeleteID = -1;
            try
            {
                _DeleteID = Convert.ToInt64(this.Request.Form["DeleteID"]);
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
            command.CommandText = "CustomersDelete_CustomersAdminPage_proc";

            command.Parameters.Add("@CustomersID", SqlDbType.Int);
            command.Parameters["@CustomersID"].Value = _DeleteID;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void CustomersUpdate()
        {
            Int64 _id = -1;
            try
            {
                _id = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch
            {
                WriteStringToAjaxRequest(".(id) خطا در درخواست ورودی");
                return;
            }


            string _PersianCustomerName = this.Request.Form["PersianCustomerName"];
            string _EnglishCustomerName = this.Request.Form["EnglishCustomerName"];
            string _CustomerLink = this.Request.Form["CustomerLink"];
            //---------user input validations-----------------------------------------------
            if (_PersianCustomerName == null || _PersianCustomerName == "")
            {
                WriteStringToAjaxRequest(".نام مشتری خالی است");
                return;
            }

            if (_CustomerLink == null || _CustomerLink == "")
            {
                WriteStringToAjaxRequest(".لینک مشتری خالی است");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CustomersUpdate_CustomersAdminPage_proc";


            command.Parameters.Add("@CustomersID", SqlDbType.Int);
            command.Parameters["@CustomersID"].Value = _id;

            command.Parameters.Add("@PersianCustomerName", SqlDbType.NVarChar);
            command.Parameters["@PersianCustomerName"].Value = _PersianCustomerName;

            command.Parameters.Add("@EnglishCustomerName", SqlDbType.NVarChar);
            command.Parameters["@EnglishCustomerName"].Value = _EnglishCustomerName;

            command.Parameters.Add("@CustomerLink", SqlDbType.NVarChar);
            command.Parameters["@CustomerLink"].Value = _CustomerLink;

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void CustomersInsert()
        {
            string _PersianCustomerName = this.Request.Form["PersianCustomerName"];
            string _EnglishCustomerName = this.Request.Form["EnglishCustomerName"];
            string _CustomerLink = this.Request.Form["CustomerLink"];

            //---------user input validations-----------------------------------------------
            if (_PersianCustomerName == null || _PersianCustomerName == "")
            {
                WriteStringToAjaxRequest(".نام مشتری خالی است");
                return;
            }

            if (_CustomerLink == null || _CustomerLink == "")
            {
                WriteStringToAjaxRequest(".لینک مشتری خالی است");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CustomersInsert_CustomersAdminPage_proc";

            command.Parameters.Add("@PersianCustomerName", SqlDbType.NVarChar);
            command.Parameters["@PersianCustomerName"].Value = _PersianCustomerName;

            command.Parameters.Add("@EnglishCustomerName", SqlDbType.NVarChar);
            command.Parameters["@EnglishCustomerName"].Value = _EnglishCustomerName;

            command.Parameters.Add("@CustomerLink", SqlDbType.NVarChar);
            command.Parameters["@CustomerLink"].Value = _CustomerLink;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".مشتری جدید با موفقیت ارسال شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowCustomersAdmin()
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumCustomersAdmin"] == null)
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
            command.Parameters["@PageSize"].Value = constants.MaxCustomersAdminShow;

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
                    if (this.Cache["_template_CustomersAdmin"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\CustomersAdminTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_CustomersAdmin"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_CustomersAdmin"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\CustomersAdminTemplate.html");
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
                string _PersianCustomerName = "";
                string _EnglishCustomerName = "";
                while (reader.Read())
                {
                    temp = _mainFormat;

                    _PersianCustomerName = reader["PersianCustomerName"].ToString();
                    _EnglishCustomerName = reader["EnglishCustomerName"].ToString();

                    if (_PersianCustomerName != "" && _EnglishCustomerName != "")
                        _PersianCustomerName = String.Format("{0}<br>{1}", _PersianCustomerName, _EnglishCustomerName);
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
                    temp = temp.Replace("[CustomerName]", _PersianCustomerName);
                    temp = temp.Replace("[CustomerLink]", reader["CustomerLink"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumCustomersAdmin"] = (int)command.Parameters["@CustomersNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowCustomersAdmin", currentPage, constants.MaxCustomersAdminShow, (int)this.Session["_ItemNumCustomersAdmin"], constants.CustomersAdminPagingNumber, "ShowItems");
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
