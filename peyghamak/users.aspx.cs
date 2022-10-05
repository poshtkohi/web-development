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

using System.IO;
using System.Data.SqlClient;

namespace Peyghamak
{
    public partial class users : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            this.SiteFooterSection.Controls.Add(LoadControl("SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            this.MetaCopyrightSection.Controls.Add(LoadControl("MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void GoogleAnalyticsControl()
        {
            this.GoogleAnalyticsSection.Controls.Add(LoadControl("GoogleAnalyticsControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["mode"] != null && this.Request.QueryString["mode"] == "search")

                this.SearchSection.Visible = true;
            else
            {
                ListUsers(false);
                this.SearchSection.Visible = false;
            }
            PageSettings(Login.IsLoginProc(this));
        }
        //--------------------------------------------------------------------
        private void ListUsers(bool fromSearch)
        {
            int currentPage = 1;
            if (!fromSearch)
            {
                try
                {
                    currentPage = Convert.ToInt32(this.Request.QueryString["page"]);
                }
                catch { }

                if (currentPage == 0)
                    currentPage++;

                if (currentPage > 1 && this.Session["ItemNum_users"] == null)
                {
                    this.Response.Redirect("users.aspx", true);
                    return;
                }
            }
                        if (fromSearch && this.Session["ItemNum_users"] == null)
            {
		this.Session["ItemNum_users"] = 0;
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            if (!fromSearch)
            {
                command.CommandText = "ListUsers_UsersPage_proc";

                command.Parameters.Add("@PageSize", SqlDbType.Int);
                command.Parameters["@PageSize"].Value = Constants.MaxUsersShow;

                command.Parameters.Add("@PageNumber", SqlDbType.Int);
                command.Parameters["@PageNumber"].Value = currentPage;

                command.Parameters.Add("@UsersNum", SqlDbType.BigInt);
                command.Parameters["@UsersNum"].Direction = ParameterDirection.Output;
            }
            else
            {
                command.CommandText = "ListUsersBySearchQuery_UsersPage_proc";

                command.Parameters.Add("@query", SqlDbType.NVarChar, 30);
                command.Parameters["@query"].Value = this.Request.Form["query"];

                command.Parameters.Add("@PageSize", SqlDbType.Int);
                command.Parameters["@PageSize"].Value = Constants.MaxUsersShowBySearchQuery;
            }


            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_template_users"] == null)
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\ListFiriends.html");
                    template = sr.ReadToEnd();
                    Login.TagDelete(ref template, "add");
                    Login.TagDelete(ref template, "remove");
                    Login.TagDelete(ref template, "block");
                    Login.TagDelete(ref template, "heblocked");
                    Login.TagDelete(ref template, "meblocked");
                    Login.TagDelete(ref template, "message");
                    this.Cache["_template_users"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["_template_users"];

                /*StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\Templates\ListFiriends.html");
                template = sr.ReadToEnd();
                sr.Close();*/

                int _p1List = template.IndexOf("<list>") + "<list>".Length;
                int _p2List = template.IndexOf("</list>");
                int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
                int _p2Paging = template.IndexOf("</paging>");
                if (_p1List <= 0 || _p2List <= 0)
                {
                    this.Response.Write(template);
                    this.Response.OutputStream.Flush();
                    return;
                }
                this.Response.Write(template.Substring(0, _p1List - "<list>".Length));
                this.Response.Flush();
                string _mainFormat = template.Substring(_p1List, _p2List - _p1List);

                string temp = "";
                PersianCalendar pc = new PersianCalendar();

                while (reader.Read())
                {
                    temp = _mainFormat.Replace("[image]", Login.Build75x75ImageUrl((string)reader["ImageGuid"], false));
                    temp = temp.Replace("[name]", (string)reader["name"]);
                    temp = temp.Replace("[url]", Login.BuildUserUrl((string)reader["username"]));
                    if ((bool)reader["ShowAgeEnabled"])
                        temp = temp.Replace("[age]", String.Format("{0}", pc.GetYear(DateTime.Now) - (int)reader["BirthYear"]));
                    else
                        Login.TagDelete(ref temp, "age");
                    if ((bool)reader["ShowCityEnabled"])
                        temp = temp.Replace("[city]", (string)reader["city"]);
                    else
                        Login.TagDelete(ref temp, "city");
                    temp = temp.Replace("[country]", (string)reader["country"]);

                    this.ListOutput.InnerHtml += temp;

                    if (fromSearch)
                    {
                        this.Session["ItemNum_users"] = (int)this.Session["ItemNum_users"] + 1;
                    }
                }

                reader.Close();

                if (!fromSearch)
                {
                    if (currentPage == 1)
                        this.Session["ItemNum_users"] = (int)(Int64)command.Parameters["@UsersNum"].Value;
                }

                command.Dispose();
                connection.Close();
                if (!fromSearch)
                {
                    if (_p1Paging > 0 && _p2Paging > 0)
                    {
                        this.ListOutput.InnerHtml += template.Substring(_p2List + "</list>".Length, _p1Paging - (_p2List + "</list>".Length));
                        string _paging = template.Substring(_p1Paging, _p2Paging - _p1Paging);
                        Login.DoPaging(ref _paging, "", currentPage, Constants.MaxUsersShow, (int)this.Session["ItemNum_users"]);
                        this.ListOutput.InnerHtml += _paging;
                    }
                    else
                        this.ListOutput.InnerHtml += template.Substring(_p2List + "</list>".Length);
                }

                this.UsersNum.Text = this.Session["ItemNum_users"].ToString();
                return;
            }
            else
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                if (fromSearch)
                {
                    this.UsersNum.Text = "0";
                    this.ListOutput.InnerHtml = "<div align='center' class='messageEx'>کاربری با عبارت جستجو شده یافت نشد.</div>";
                }
                else
                    this.Response.Redirect("/", true);
                return;
            }
        }
        //--------------------------------------------------------------------
        private void PageSettings(bool _IsLogin)
        {
            MetaCopyrightControl();
            SetSiteFooterControl();
            GoogleAnalyticsControl();
            if (_IsLogin)
            {
                SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];
                this.LoginedPanel.Visible = true;
                this.UnloginedPanel.Visible = false;
                this.MyPageHperLink.NavigateUrl = String.Format("http://{0}.{1}/my.aspx", _info.Username, Constants.BlogDomain);
                this.SignoutHyperLink.NavigateUrl = String.Format("http://{0}.{1}/signout.aspx", _info.Username, Constants.BlogDomain);
            }
            else
            {
                this.LoginedPanel.Visible = false;
                this.UnloginedPanel.Visible = true;
            }
        }
        //--------------------------------------------------------------------
        protected void search_Click(object sender, EventArgs e)
        {
            ListUsers(true);
        }
        //--------------------------------------------------------------------
    }
}
