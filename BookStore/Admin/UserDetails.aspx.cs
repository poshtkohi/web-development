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
    public partial class UserDetails : System.Web.UI.Page
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
                    case "ShowUserDetails":
                        ShowUserDetails();
                        return;
                    default:
                        return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        private void ShowUserDetails()
        {
            Int64 _UserID = -1;

            try { _UserID = Convert.ToInt64(this.Request.Form["page"]); }
            catch { _UserID = -1; }
            if (_UserID < 0)
            {
                WriteStringToAjaxRequest(".(UserID) خطا در درخواست ورودی");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "ShowShoppingCartByUserID_UserDetailsPage_proc";
            command.CommandText = "ShowUserInfoByUserID_UsersDetailsPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = _UserID;



            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_UserDetails"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\UserDetailsTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_UserDetails"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_UserDetails"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\UserDetailsTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }

                reader.Read();
                template = template.Replace("[username]", reader["Username"].ToString());
                template = template.Replace("[password]", reader["password"].ToString());
                template = template.Replace("[name]", reader["name"].ToString());
                template = template.Replace("[address]", reader["address"].ToString());
                template = template.Replace("[tel]", reader["tel"].ToString());
                template = template.Replace("[PostalCode]", reader["PostalCode"].ToString());
                template = template.Replace("[email]", reader["email"].ToString());
                template = template.Replace("[UserCredits]", reader["UserCredits"].ToString());

                reader.Close();
                connection.Close();
                command.Dispose();

                WriteStringToAjaxRequest(template);

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
