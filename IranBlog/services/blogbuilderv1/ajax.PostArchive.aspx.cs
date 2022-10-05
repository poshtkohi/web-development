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
    public partial class PostArchive : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.SubjectedArchiveAccess)
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
                switch (this.Request.Form["mode"])
                {
                    case "ShowAjaxPostArchive":
                        ShowAjaxPostArchive(_SigninSessionInfo);
                        return;
                    case "delete":
                        AjaxPostArchiveDelete(_SigninSessionInfo);
                        return;
                    case "load":
                        AjaxPostArchiveLoad(_SigninSessionInfo);
                        return;
                    case "update":
                        AjaxPostArchiveUpdate(_SigninSessionInfo);
                        return;
                    case "post":
                        DoPost(_SigninSessionInfo);
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void AjaxPostArchiveDelete(SigninSessionInfo _SigninSessionInfo)
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
            command.CommandText = "Delete_AjaxPostArchivePage_proc";

            command.Parameters.Add("@PostArchiveID", SqlDbType.BigInt);
            command.Parameters["@PostArchiveID"].Value = _DeleteID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void AjaxPostArchiveUpdate(SigninSessionInfo _SigninSessionInfo)
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

            string _PostArchiveTitle = this.Request.Form["PostArchiveTitle"];

            //---------user input validations-----------------------------------------------
            if (_PostArchiveTitle != null && _PostArchiveTitle != "")
            {
                if (_PostArchiveTitle.Length > 400)
                {
                    WriteStringToAjaxRequest(".تعداد حروف موضوع نمی تواند از 400 حرف بیشتر باشد");
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
            command.CommandText = "Update_AjaxPostArchivePage_proc";

            command.Parameters.Add("@PostArchiveID", SqlDbType.BigInt);
            command.Parameters["@PostArchiveID"].Value = _id;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@PostArchiveTitle", SqlDbType.NVarChar, 400);
            command.Parameters["@PostArchiveTitle"].Value = _PostArchiveTitle;



            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void DoPost(SigninSessionInfo _SigninSessionInfo)
        {
            string _PostArchiveTitle = this.Request.Form["PostArchiveTitle"];

            //---------user input validations-----------------------------------------------
            if (_PostArchiveTitle != null && _PostArchiveTitle != "")
            {
                if (_PostArchiveTitle.Length > 400)
                {
                    WriteStringToAjaxRequest(".تعداد حروف موضوع نمی تواند از 400 حرف بیشتر باشد");
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
            command.CommandText = "Post_AjaxPostArchivePage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            command.Parameters.Add("@PostArchiveTitle", SqlDbType.NVarChar, 400);
            command.Parameters["@PostArchiveTitle"].Value = _PostArchiveTitle;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".موضوع جدید با موفقیت تعریف شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void AjaxPostArchiveLoad(SigninSessionInfo _SigninSessionInfo)
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
            command.CommandText = "Load_AjaxPostArchivePage_proc";

            command.Parameters.Add("@PostArchiveID", SqlDbType.BigInt);
            command.Parameters["@PostArchiveID"].Value = _id;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    PostArchiveTitle
                */
                WriteStringToAjaxRequest(String.Format("{0}",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes((string)reader["PostArchiveTitle"]))
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
        private void ShowAjaxPostArchive(SigninSessionInfo _SigninSessionInfo)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumPostArchive"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "List_AjaxPostArchivePage_proc";
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxShowPostArchive;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@PostArchiveNum", SqlDbType.Int);
            command.Parameters["@PostArchiveNum"].Direction = ParameterDirection.Output;



            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_ajax.PostArchive"] == null)
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ajax.PostArchive.template.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_ajax.PostArchive"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_ajax.PostArchive"];

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
                    temp = temp.Replace("[PostArchiveID]", reader["PostArchiveID"].ToString());
                    temp = temp.Replace("[PostArchiveTitle]", reader["PostArchiveTitle"].ToString());
                    temp = temp.Replace("[PostArchiveViewAddress]", String.Format("http://{0}.{1}/?mode=SubjectedArchive&id={2}", _SigninSessionInfo.Subdomain, constants.ZoneName, reader["PostArchiveID"].ToString()));

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumPostArchive"] = (int)command.Parameters["@PostArchiveNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowAjaxPostArchive", currentPage, constants.MaxShowPostArchive, (int)this.Session["_ItemNumPostArchive"], constants.ShowPostArchivePagingNumber, "ShowItems");
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
