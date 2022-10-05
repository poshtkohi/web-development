/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;
using System.Text.RegularExpressions;
using ServerManagement;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class domain : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                /*TeamWeblogAccessInfo info = (TeamWeblogAccessInfo)this.Session["TeamWeblogAccessInfo"];
                if (info.FullAccess)
                    return true;
                else
                    return false;*/
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
            if (LabelFirstLoad.Text == "true")
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT count(*) FROM {0} WHERE BlogID={1} AND IsDeleted=0", constants.SQLDomainsTableName, _SigninSessionInfo.BlogID);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int total = reader.GetInt32(0);
                this.data.DataBind();
                reader.Close();
                command.Dispose();
                // Set the virtual item count, which specifies the total number
                // items displayed in the DataGrid control when custom paging
                // is used.
                this.data.VirtualItemCount = total;
                // Retrieve the segment of data to display on the page from the
                // data source and bind it to the DataGrid control.
                if (total > 0)
                {
                    BindGrid();
                    this.LabelFirstLoad.Text = "false";
                    this.data.Visible = true;
                    this.GridMessage.Visible = false;
                }
                else
                {
                    this.data.Visible = false;
                    this.GridMessage.Visible = true;
                }
            }
            return;
        }
        //--------------------------------------------------------------------------------
        protected void store_Click(object sender, EventArgs e)
        {
            string __domain = this.Request.Form["_domain"].Trim().ToLower();
            if (__domain == null || __domain == "")
            {
                this.message.Text = ".نام دامنه خالی است";
                this.message.Visible = true;
                return;
            }
            Regex rex = new Regex(@"^[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}$");
            if (!rex.IsMatch(__domain) || __domain.IndexOf(constants.ZoneName) >= 0 || __domain.IndexOf("www.") == 0)
            {
                this.message.Text = ".نام دامنه نامعتبر است";
                this.message.Visible = true;
                return;
            }
            else
            {
                this.message.Text = "";
                this.message.Visible = false;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "domain_add_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@subdomain", SqlDbType.VarChar);
            command.Parameters["@subdomain"].Value = _SigninSessionInfo.Subdomain;

            command.Parameters.Add("@domain", SqlDbType.VarChar);
            command.Parameters["@domain"].Value = __domain;

            command.Parameters.Add("@DomainNum", SqlDbType.Int);
            command.Parameters["@DomainNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@AllowedDomainNum", SqlDbType.Int);
            command.Parameters["@AllowedDomainNum"].Value = constants.AllowedDomainNum;

            command.Parameters.Add("@IsExists", SqlDbType.Bit);
            command.Parameters["@IsExists"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@IsDeleted", SqlDbType.Bit);
            command.Parameters["@IsDeleted"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            if ((int)command.Parameters["@DomainNum"].Value > constants.AllowedDomainNum)
            {
                command.Dispose();
                this.message.Text = ".تعداد دامنه های قابل اتصال نمی تواند بیشتر از 10 عدد باشد";
                this.message.Visible = true;
                return;
            }
            if (!(bool)command.Parameters["@IsExists"].Value && !(bool)command.Parameters["@IsDeleted"].Value)
            {
                command.Dispose();

                DNS dns = new DNS(constants.password);
                dns.CreateZone(constants.DNSServerAddress, __domain, 0);
                dns.CreateRecord(constants.DNSServerAddress, __domain, "", constants.DnsDomainIPBlog);
                dns.CreateRecord(constants.DNSServerAddress, __domain, "www", constants.DnsDomainIPBlog);
                dns.Dispose();

                IIS iis = new IIS(constants.password);
                iis.DomainBind(__domain, constants.DnsDomainIPBlog, "80", constants.ZoneName);
                iis.Dispose();

                this.message.Text = ".دامنه جدید شما با موفقیت به وبلاگتان متصل شد";
                this.message.Visible = true;
                this._domain.Text = "";
                BindGrid();
                this.data.Visible = true;
                this.GridMessage.Visible = false;
                return;
            }
            if ((bool)command.Parameters["@IsExists"].Value && (bool)command.Parameters["@IsDeleted"].Value)
            {
                command.Dispose();
                this.message.Text = ".دامنه جدید شما با موفقیت به وبلاگتان متصل شد";
                this.message.Visible = true;
                this._domain.Text = "";
                BindGrid();
                this.data.Visible = true;
                this.GridMessage.Visible = false;
                return;
            }
            if ((bool)command.Parameters["@IsExists"].Value && !(bool)command.Parameters["@IsDeleted"].Value)
            {
                command.Dispose();
                this.message.Text = ".این دامنه قبلا استفاده شده است، از دامنه دیگری استفاده کنید";
                this.message.Visible = true;
                this._domain.Text = "";
                return;
            }
        }
        //--------------------------------------------------------------------------------
        private void BindGrid()
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT id,domain FROM {0} WHERE BlogID={1} AND IsDeleted=0  ORDER BY id DESC", constants.SQLDomainsTableName, _SigninSessionInfo.BlogID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("domain", typeof(string)));
                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["id"] = reader["id"].ToString();
                    dr["domain"] = (string)reader["domain"];
                    dt.Rows.Add(dr);
                }
                DataView dv = new DataView(dt);
                this.data.DataSource = dv;
                this.data.DataBind();
            }
            else
            {
                this.GridMessage.Visible = true;
                this.data.DataBind();
            }
            reader.Close();
            command.Dispose();
            return;
        }
        //--------------------------------------------------------------------------------
        protected void data_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            //this.Response.Redirect("TeamWeblog.aspx?id=" + e.Item.Cells[0].Text, true);
            //return;
        }
        //--------------------------------------------------------------------------------
        protected void data_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("UPDATE {0} SET IsDeleted=1 WHERE id={1} AND BlogID={2}", constants.SQLDomainsTableName, e.Item.Cells[0].Text, _SigninSessionInfo.BlogID);
            command.ExecuteNonQuery();
            command.Dispose();
            this.message.Text = ".دامنه انتخاب شده با موفقیت از سیستم حذف شد";
            this.message.Visible = true;
            BindGrid();
            return;
        }
        //--------------------------------------------------------------------------------
    }
}
