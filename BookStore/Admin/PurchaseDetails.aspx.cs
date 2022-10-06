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
    public partial class PurchaseDetails : System.Web.UI.Page
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
                    case "ShowPurchaseDetails":
                        ShowPurchaseDetails();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void ShowPurchaseDetails()
        {
            Int64 _PurchaseID = -1;

            try { _PurchaseID = Convert.ToInt64(this.Request.Form["page"]); }
            catch { _PurchaseID = -1; }
            if (_PurchaseID < 0)
            {
                WriteStringToAjaxRequest(".(PurchaseID) خطا در درخواست ورودی");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "ShowShoppingCartByPurchaseID_PurchaseDetailsPage_proc";
            command.CommandText = "ShowPurchaseInfoByPurchaseID_PurchaseDetailsPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PurchaseID", SqlDbType.BigInt);
            command.Parameters["@PurchaseID"].Value = _PurchaseID;



            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_PurchaseDetails"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PurchaseDetailsTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_PurchaseDetails"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_PurchaseDetails"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PurchaseDetailsTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                reader.Read();
                template = template.Replace("[CustomerUsername]", reader["CustomerUsername"].ToString());
                template = template.Replace("[CustomerName]", reader["CustomerName"].ToString());
                template = template.Replace("[CustomerAddress]", reader["CustomerAddress"].ToString());
                template = template.Replace("[CustomerTel]", reader["CustomerTel"].ToString());
                template = template.Replace("[CustomerPostalCode]", reader["CustomerPostalCode"].ToString());
                template = template.Replace("[CustomerEmail]", reader["CustomerEmail"].ToString());
                template = template.Replace("[PurchaseDate]", reader["PurchaseDate"].ToString());
                template = template.Replace("[PursuitCode]", reader["PursuitCode"].ToString());

                reader.Close();
                command.CommandText = "ShowShoppingCartByPurchaseID_PurchaseDetailsPage_proc";
                reader = command.ExecuteReader();

                int _p1Post = template.IndexOf("<post>") + "<post>".Length;
                int _p2Post = template.IndexOf("</post>");
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
                    temp = temp.Replace("[BookTitle]", reader["BookTitle"].ToString());
                    temp = temp.Replace("[BookISBN]", reader["BookISBN"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

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
