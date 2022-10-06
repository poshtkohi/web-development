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
    public partial class PurchasesAdmin : System.Web.UI.Page
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
                    case "ShowUnverifiedPurchases":
                        ShowPurchases(false);
                        return;
                    case "ShowVerifiedPurchases":
                        ShowPurchases(true);
                        return;
                    case "PurchaseVerify":
                        PurchaseVerify();
                        return;
                    case "delete":
                        PurchaseDelete();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void PurchaseVerify()
        {
            Int64 _PurchaseID = -1;
            try
            {
                _PurchaseID = Convert.ToInt64(this.Request.Form["PurchaseID"]);
            }
            catch
            {
                WriteStringToAjaxRequest(".(PurchaseID) خطا در درخواست ورودی");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PurchaseVerify_PurchasesAdminPage_proc";

            command.Parameters.Add("@PurchaseID", SqlDbType.BigInt);
            command.Parameters["@PurchaseID"].Value = _PurchaseID;


            command.ExecuteNonQuery();


            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".خرید انتخاب شده با موفقیت تایید شد");
            return;
        }
        //--------------------------------------------------------------------------------
        private void PurchaseDelete()
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
            command.CommandText = "PurchaseDelete_PurchasesAdminPage_proc";

            command.Parameters.Add("@PurchaseID", SqlDbType.BigInt);
            command.Parameters["@PurchaseID"].Value = _DeleteID;


            command.ExecuteNonQuery();


            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowPurchases(bool _ShowVerifiedPurcheses)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumPurchaseAdmin"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PurchasesShow_PurchasesAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxPurchaseAdminShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PurchaseNum", SqlDbType.Int);
            command.Parameters["@PurchaseNum"].Direction = ParameterDirection.Output;


            command.Parameters.Add("@ShowVerifiedPurcheses", SqlDbType.Bit);
            command.Parameters["@ShowVerifiedPurcheses"].Value = _ShowVerifiedPurcheses;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_PurchaseAdmin"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PurchasesAdminTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_PurchaseAdmin"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_PurchaseAdmin"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PurchasesAdminTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                if (!_ShowVerifiedPurcheses)
                    Common.TagDelete(ref template, "delete");
                else
                    Common.TagDelete(ref template, "verify");
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
                    temp = temp.Replace("[PurchaseID]", reader["PurchaseID"].ToString());
                    temp = temp.Replace("[UserID]", reader["UserID"].ToString());
                    temp = temp.Replace("[username]", reader["username"].ToString());
                    temp = temp.Replace("[PursuitCode]", reader["PursuitCode"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumPurchaseAdmin"] = (int)command.Parameters["@PurchaseNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    if(_ShowVerifiedPurcheses)
                        Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowVerifiedPurchases", currentPage, constants.MaxPurchaseAdminShow, (int)this.Session["_ItemNumPurchaseAdmin"], constants.PurchaseAdminPagingNumber, "ShowItems");
                    else
                        Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowUnverifiedPurchases", currentPage, constants.MaxPurchaseAdminShow, (int)this.Session["_ItemNumPurchaseAdmin"], constants.PurchaseAdminPagingNumber, "ShowItems");
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
