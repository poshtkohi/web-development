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

using System.Data.SqlClient;
using System.IO;
using bookstore;

namespace bookstore.admin
{
    public partial class TransactionsAdmin : System.Web.UI.Page
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
                    case "ShowTransactions":
                        ShowTransactions();
                        return;
                    case "ShowTotalAmount":
                        ShowTotalAmount();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void ShowTotalAmount()
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowTotalAmount_TransactionsAdminPage_proc";
            //-------------------------------------------
            command.Parameters.Add("@ShowTotalAmount", SqlDbType.BigInt);
            command.Parameters["@ShowTotalAmount"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            //WriteStringToAjaxRequest("ewrewrw");

            string output = String.Format("<br/><div align=right>{0}</div></br>", command.Parameters["@ShowTotalAmount"].Value.ToString());
            WriteStringToAjaxRequest(output);

            connection.Close();
            command.Dispose();

            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowTransactions()
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumTransactionsAdmin"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "TransactionsShow_TransactionsAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxTransactionsAdminShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@TransactionsNum", SqlDbType.Int);
            command.Parameters["@TransactionsNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_TransactionsAdmin"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\TransactionsAdminTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_TransactionsAdmin"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_TransactionsAdmin"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\TransactionsAdminTemplate.html");
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
                    temp = temp.Replace("[TransactionsID]", reader["TransactionsID"].ToString());
                    temp = temp.Replace("[UserID]", reader["UserID"].ToString());
                    temp = temp.Replace("[username]", reader["username"].ToString());
                    temp = temp.Replace("[date]", reader["date"].ToString());
                    temp = temp.Replace("[amount]", reader["amount"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumTransactionsAdmin"] = (int)command.Parameters["@TransactionsNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowTransactions", currentPage, constants.MaxTransactionsAdminShow, (int)this.Session["_ItemNumTransactionsAdmin"], constants.TransactionsAdminPagingNumber, "ShowItems");
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
