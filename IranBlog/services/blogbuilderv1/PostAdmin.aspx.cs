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

namespace services.blogbuilderv1
{
    public partial class PostAdmin : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.PostAccess)
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
                    case "ShowPostAdmin":
                        ShowPostAdmin(_SigninSessionInfo);
                        return;
                    case "delete":
                        PostAdminDelete(_SigninSessionInfo);
                        return;
                    case "load":
                        PostAdminLoad(_SigninSessionInfo);
                        return;
                    case "update":
                        PostAdminUpdate(_SigninSessionInfo);
                        return;
                    case "post":
                        DoPost(_SigninSessionInfo);
                        return;
                    default:
                        return;
                }
            }
            else
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "LoadSubjectedArchive_PostAdminPage_proc";

                //-------------------------------------------
                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

                SqlDataReader reader = command.ExecuteReader();
                bool HasSubjectedArchive = false;
                if (reader.HasRows)
                {
                    HasSubjectedArchive = true;
                    while (reader.Read())
                    {
                        ListItem item = new ListItem((string)reader["subject"], ((Int64)reader["id"]).ToString());
                        this.CategoryID.Items.Add(item);
                    }
                }
                reader.Close();
				command.Parameters.Clear();
                if (!HasSubjectedArchive)
                {
                    command.CommandText = "CreatePrimarySubjectedArchive_PostAdminPage_proc";
                    //-------------------------------------------
                    command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                    command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

                    command.Parameters.Add("@CategoryID", SqlDbType.BigInt);
                    command.Parameters["@CategoryID"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    ListItem item = new ListItem("عمومی", command.Parameters["@CategoryID"].Value.ToString());
                    this.CategoryID.Items.Add(item);
                }

                command.Dispose();
                connection.Close(); 
                return;
            }
        }
        //--------------------------------------------------------------------------------
        private void PostAdminLoad(SigninSessionInfo _SigninSessionInfo)
        {
            Int64 _id = -1;
            try
            {
                _id = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch { return; }

            //throw new Exception(_id.ToString());

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PostLoad_PostAdminPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _id;

            command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
            command.Parameters["@AuthorID"].Value = _SigninSessionInfo.AuthorID;

            command.Parameters.Add("@IsInAdminMode", SqlDbType.Bit);
            command.Parameters["@IsInAdminMode"].Value = !_SigninSessionInfo.IsInTeamWeblogMode;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                /*  
                    PostTitle
                    PostContent
                    ContinuedPostContent
                    CategoryID
                    comment
                */
                string _PostTitle = (string)reader["PostTitle"];// throw new Exception(_PostTitle); return;
                string _PostContent = (string)reader["PostContent"];
                string _ContinuedPostContent = "";
                if(reader["ContinuedPostContent"] != DBNull.Value)
                    _ContinuedPostContent = (string)reader["ContinuedPostContent"];
                string _CategoryID = reader["CategoryID"].ToString();
                string _comment = "enabled";
                if ((int)reader["comment"] == -1)
                {
                    _comment = "disabled";
                    goto Continue;
                }

                if (!(bool)reader["IsShowCommentsPreVerify"])
                {
                    _comment = "PreverifyActivate";
                    goto Continue;
                }

                
                
        Continue:
                WriteStringToAjaxRequest(String.Format("{0},{1},{2},{3},{4}", 
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_PostTitle)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_PostContent)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_ContinuedPostContent)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_CategoryID)),
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_comment))
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
        private void PostAdminDelete(SigninSessionInfo _SigninSessionInfo)
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

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PostDelete_PostAdminPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _DeleteID;

            command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
            command.Parameters["@AuthorID"].Value = _SigninSessionInfo.AuthorID;

            command.Parameters.Add("@IsInAdminMode", SqlDbType.Bit);
            command.Parameters["@IsInAdminMode"].Value = !_SigninSessionInfo.IsInTeamWeblogMode;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //----------------------------------------------------------------------------------
        private void PostAdminUpdate(SigninSessionInfo _SigninSessionInfo)
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


            string _PostTitle = this.Request.Form["PostTitle"];
            string _PostContent = this.Request.Form["PostContent"];
            string _ContinuedPostContent = this.Request.Form["ContinuedPostContent"];
            string CategoryID = this.Request.Form["CategoryID"];
            string _comment = this.Request.Form["comment"];

            //---------user input validations-----------------------------------------------
            if (_PostTitle != null)
            {
                if (_PostTitle.Length > 200)
                {
                    WriteStringToAjaxRequest(".تعداد حروف عنوان مطلب نمی تواند از 200 حرف بیشتر باشد");
                    return;
                }
            }

            if (_PostTitle == null)
                _PostTitle = "";

            if (_PostContent == null || _PostContent == "")
            {
                WriteStringToAjaxRequest(".متن پست امروز شما خالی است");
                return;
            }

            if (_PostContent != "")
            {
                if (_PostContent.Length > 204800)
                {
                    WriteStringToAjaxRequest(".حجم پست وبلاگ شما نمی تواند از 256 کیلو بایت بیشتر باشد");
                    return;
                }
                if (_ContinuedPostContent != "")
                {
                    if (_ContinuedPostContent.Length > 204800)
                    {
                        WriteStringToAjaxRequest(".حجم ادامه مطلب نمی تواند از 256 کیلو بایت بیشتر باشد");
                        return;
                    }
                }
            }

            Int64 _CategoryID = -1;
            try
            {
                _CategoryID = Convert.ToInt64(CategoryID);
            }
            catch
            {
                WriteStringToAjaxRequest(".(CategoryID) خطا در درخواست ورودی");
                return;
            }

            if (_comment == null || _comment == "" || (_comment != "enabled" & _comment != "disabled" & _comment != "PreverifyActivate"))
            {
                WriteStringToAjaxRequest(".(comment) خطا در درخواست ورودی");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PostAdminUpdate_PostAdminPage_proc";


            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _id;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@CategoryID", SqlDbType.BigInt);
            command.Parameters["@CategoryID"].Value = _CategoryID;

            command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
            command.Parameters["@AuthorID"].Value = _SigninSessionInfo.AuthorID;

            command.Parameters.Add("@PostTitle", SqlDbType.NVarChar);
            command.Parameters["@PostTitle"].Value = _PostTitle;

            command.Parameters.Add("@PostContent", SqlDbType.NText);
            command.Parameters["@PostContent"].Value = _PostContent;

            command.Parameters.Add("@ContinuedPostContent", SqlDbType.NText);
            command.Parameters["@ContinuedPostContent"].IsNullable = true;
            if (_ContinuedPostContent != "")
                command.Parameters["@ContinuedPostContent"].Value = _ContinuedPostContent;

            else
                command.Parameters["@ContinuedPostContent"].Value = DBNull.Value;

            command.Parameters.Add("@comment", SqlDbType.VarChar);
            command.Parameters["@comment"].Value = _comment;

            command.Parameters.Add("@IsInAdminMode", SqlDbType.Bit);
            command.Parameters["@IsInAdminMode"].Value = !_SigninSessionInfo.IsInTeamWeblogMode;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".آیتم انتخاب شده با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void DoPost(SigninSessionInfo _SigninSessionInfo)
        {
            string _PostTitle = this.Request.Form["PostTitle"];
            string _PostContent = this.Request.Form["PostContent"];
            string _ContinuedPostContent = this.Request.Form["ContinuedPostContent"];
            string CategoryID = this.Request.Form["CategoryID"];
            string _comment = this.Request.Form["comment"];

            //---------user input validations-----------------------------------------------
            if (_PostTitle != null)
            {
                if (_PostTitle.Length > 200)
                {
                    WriteStringToAjaxRequest(".تعداد حروف عنوان مطلب نمی توتند از 200 حرف بیشتر باشد");
                    return;
                }
            }

            if (_PostTitle == null)
                _PostTitle = "";

            if (_PostContent == null || _PostContent == "")
            {
                WriteStringToAjaxRequest(".متن پست امروز شما خالی است");
                return;
            }

            if (_PostContent != "")
            {
                if (_PostContent.Length > 204800)
                {
                    WriteStringToAjaxRequest(".حجم پست وبلاگ شما نمی تواند از 256 کیلو بایت بیشتر باشد");
                    return;
                }
                if (_ContinuedPostContent != "")
                {
                    if (_ContinuedPostContent.Length > 204800)
                    {
                        WriteStringToAjaxRequest(".حجم ادامه مطلب نمی تواند از 256 کیلو بایت بیشتر باشد");
                        return;
                    }
                }
            }

            Int64 _CategoryID = -1;
            try
            {
                _CategoryID = Convert.ToInt64(CategoryID);
            }
            catch
            {
                WriteStringToAjaxRequest(".(CategoryID) خطا در درخواست ورودی");
                return;
            }

            if (_comment == null || _comment == "" || (_comment != "enabled" & _comment != "disabled" & _comment != "PreverifyActivate"))
            {
                WriteStringToAjaxRequest(".(comment) خطا در درخواست ورودی");
                return;
            }
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DoPost_PostAdminPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@CategoryID", SqlDbType.BigInt);
            command.Parameters["@CategoryID"].Value = _CategoryID;

            command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
            command.Parameters["@AuthorID"].Value = _SigninSessionInfo.AuthorID;

            command.Parameters.Add("@PostTitle", SqlDbType.NVarChar);
            command.Parameters["@PostTitle"].Value = _PostTitle;

            command.Parameters.Add("@PostContent", SqlDbType.NText);
            command.Parameters["@PostContent"].Value = _PostContent;

            command.Parameters.Add("@ContinuedPostContent", SqlDbType.NText);
            command.Parameters["@ContinuedPostContent"].IsNullable = true;
            if (_ContinuedPostContent != "")
                command.Parameters["@ContinuedPostContent"].Value = _ContinuedPostContent;

            else
                command.Parameters["@ContinuedPostContent"].Value = DBNull.Value;

            command.Parameters.Add("@comment", SqlDbType.VarChar);
            command.Parameters["@comment"].Value = _comment;


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".پست جدید با موفقیت به وبلاگتان ارسال شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void ShowPostAdmin(SigninSessionInfo _SigninSessionInfo)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumPostAdmin"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ListPosts_PostAdminPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@AuthorID", SqlDbType.BigInt);
            command.Parameters["@AuthorID"].Value = _SigninSessionInfo.AuthorID;

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxPostAdminShow;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@IsInAdminMode", SqlDbType.Bit);
            command.Parameters["@IsInAdminMode"].Value = !_SigninSessionInfo.IsInTeamWeblogMode;

            command.Parameters.Add("@PostNum", SqlDbType.Int);
            command.Parameters["@PostNum"].Direction = ParameterDirection.Output;


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_PostAdmin"] == null)
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PostAdminTemplate.html");
                    template = sr.ReadToEnd();
                    this.Cache["_template_PostAdmin"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_PostAdmin"];

                /*StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\PostAdminTemplate.html");
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
                DateTime dt;
                AlirezaPoshtkoohiLibrary.PersianCalendar pc = new AlirezaPoshtkoohiLibrary.PersianCalendar();
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
                    temp = temp.Replace("[PostID]", reader["PostID"].ToString());
                    temp = temp.Replace("[subdomain]", _SigninSessionInfo.Subdomain);
                    temp = temp.Replace("[subject]", reader["subject"].ToString());
                    temp = temp.Replace("[date]", String.Format("{0}/{1}/{2}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt)));

                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumPostAdmin"] = (int)command.Parameters["@PostNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowPostAdmin", currentPage, constants.MaxPostAdminShow, (int)this.Session["_ItemNumPostAdmin"], constants.PostAdminPagingNumber, "ShowItems");
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
