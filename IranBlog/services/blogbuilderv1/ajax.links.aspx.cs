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
using AlirezaPoshtkoohiLibrary;
using services;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1.ajax
{
    public partial class links : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.DailyLinksAccess)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
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

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }

            if (this.Request.Form["mode"] != null)
            {
                bool _IsAjaxLinks = true;
                switch (this.Request.Form["_mode"])
                {
                    case "ajax.linkss":
                        _IsAjaxLinks = false;
                        break;
                    default:
                        break;
                }
                switch (this.Request.Form["mode"])
                {
                    case "ShowAjaxLinks":
                        ShowAjaxLinks(_SigninSessionInfo, _IsAjaxLinks);
                        return;
                    case "delete":
                        AjaxLinksDelete(_SigninSessionInfo, _IsAjaxLinks);
                        return;
                    case "load":
                        AjaxLinksLoad(_SigninSessionInfo, _IsAjaxLinks);
                        return;
                    case "update":
                        AjaxLinksUpdate(_SigninSessionInfo, _IsAjaxLinks);
                        return;
                    case "post":
                        DoPost(_SigninSessionInfo, _IsAjaxLinks);
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void AjaxLinksDelete(SigninSessionInfo _SigninSessionInfo, bool _IsAjaxLinks)
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
            command.CommandText = "Delete_AjaxLinksPage_proc";

            command.Parameters.Add("@LinkID", SqlDbType.BigInt);
            command.Parameters["@LinkID"].Value = _DeleteID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@IsAjaxLinks", SqlDbType.Bit);
            command.Parameters["@IsAjaxLinks"].Value = _IsAjaxLinks;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void AjaxLinksUpdate(SigninSessionInfo _SigninSessionInfo, bool _IsAjaxLinks)
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

            string _LinkTitle = this.Request.Form["LinkTitle"];
            string _LinkAddress = this.Request.Form["LinkAddress"];

            //---------user input validations-----------------------------------------------
            if (_LinkTitle != null && _LinkTitle != "")
            {
                if (_LinkTitle.Length > 400)
                {
                    WriteStringToAjaxRequest(".تعداد حروف عنوان پیوند نمی تواند از 400 حرف بیشتر باشد");
                    return;
                }
            }
            else
                return;

            if (_LinkAddress != null && _LinkAddress != "")
            {
                if (_LinkAddress.Length > 400)
                {
                    WriteStringToAjaxRequest(".تعداد حروف آدرس پیوند نمی توتند از 400 حرف بیشتر باشد");
                    return;
                }
            }
            else
                return;
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Update_AjaxLinksPage_proc";

            command.Parameters.Add("@LinkID", SqlDbType.BigInt);
            command.Parameters["@LinkID"].Value = _id;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@LinkTitle", SqlDbType.NVarChar, 400);
            command.Parameters["@LinkTitle"].Value = _LinkTitle;

            command.Parameters.Add("@LinkAddress", SqlDbType.NVarChar, 400);
            command.Parameters["@LinkAddress"].Value = _LinkAddress;

            command.Parameters.Add("@IsAjaxLinks", SqlDbType.Bit);
            command.Parameters["@IsAjaxLinks"].Value = _IsAjaxLinks;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void DoPost(SigninSessionInfo _SigninSessionInfo, bool _IsAjaxLinks)
        {
            string _LinkTitle = this.Request.Form["LinkTitle"];
            string _LinkAddress = this.Request.Form["LinkAddress"];

            //---------user input validations-----------------------------------------------
            if (_LinkTitle != null && _LinkTitle != "")
            {
                if (_LinkTitle.Length > 400)
                {
                    WriteStringToAjaxRequest(".تعداد حروف عنوان پیوند نمی تواند از 400 حرف بیشتر باشد");
                    return;
                }
            }
            else
                return;

            if (_LinkAddress != null && _LinkAddress != "")
            {
                if (_LinkAddress.Length > 400)
                {
                    WriteStringToAjaxRequest(".تعداد حروف آدرس پیوند نمی توتند از 400 حرف بیشتر باشد");
                    return;
                }
            }
            else
                return;

            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Post_AjaxLinksPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            command.Parameters.Add("@LinkTitle", SqlDbType.NVarChar, 400);
            command.Parameters["@LinkTitle"].Value = _LinkTitle;

            command.Parameters.Add("@LinkAddress", SqlDbType.NVarChar, 400);
            command.Parameters["@LinkAddress"].Value = _LinkAddress;

            command.Parameters.Add("@IsAjaxLinks", SqlDbType.Bit);
            command.Parameters["@IsAjaxLinks"].Value = _IsAjaxLinks;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".پیوند جدید با موفقیت به وبلاگتان ارسال شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void AjaxLinksLoad(SigninSessionInfo _SigninSessionInfo, bool _IsAjaxLinks)
        {
            Int64 _id = -1;
            try
            {
                _id = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch { return; }


            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Load_AjaxLinksPage_proc";

            command.Parameters.Add("@LinkID", SqlDbType.BigInt);
            command.Parameters["@LinkID"].Value = _id;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@IsAjaxLinks", SqlDbType.Bit);
            command.Parameters["@IsAjaxLinks"].Value = _IsAjaxLinks;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    LinkTitle
                    LinkAddress
                */
                WriteStringToAjaxRequest(String.Format("{0},{1}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes((string)reader["LinkTitle"])),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes((string)reader["LinkAddress"]))
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
        private void ShowAjaxLinks(SigninSessionInfo _SigninSessionInfo, bool _IsAjaxLinks)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumAjaxLinks"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "List_AjaxLinksPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxShowAjaxLinks;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@LinkNum", SqlDbType.Int);
            command.Parameters["@LinkNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@IsAjaxLinks", SqlDbType.Bit);
            command.Parameters["@IsAjaxLinks"].Value = _IsAjaxLinks;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_ajax.links"] == null)
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ajax.links.template.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_ajax.links"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_ajax.links"];

                /*StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ajax.links.template.html");
                template = sr.ReadToEnd();
                sr.Close();*/

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

                if (_IsAjaxLinks)
                    _mainFormat = _mainFormat.Replace("[mode]", "ajax.links");
                else
                    _mainFormat = _mainFormat.Replace("[mode]", "ajax.linkss");

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
                    temp = temp.Replace("[LinkID]", reader["LinkID"].ToString());
                    temp = temp.Replace("[LinkTitle]", reader["LinkTitle"].ToString());
                    temp = temp.Replace("[LinkAddress]", reader["LinkAddress"].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumAjaxLinks"] = (int)command.Parameters["@LinkNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowAjaxLinks", currentPage, constants.MaxShowAjaxLinks, (int)this.Session["_ItemNumAjaxLinks"], constants.ShowAjaxLinksPagingNumber, "ShowItems");
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
