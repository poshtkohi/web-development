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
using AlirezaPoshtkoohiLibrary;
using services.blogbuilderv1;
using IranBlog.Classes.Security;
using services;

public partial class TeamWeblogLogin : System.Web.UI.Page
{
    //----------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Common.IsLoginProc(this))
            this.Response.Redirect("/services/", true);
        SetSiteHeaderControl();
        SetCopyrightFooterControl();
        return;
    }
    //----------------------------------------------------------------
    private void SetSiteHeaderControl()
    {
        this.MainSiteHeaderControl.Controls.Add(LoadControl("MainSiteHeaderControl.ascx"));
        return;
    }
    //----------------------------------------------------------------
    private void SetCopyrightFooterControl()
    {
        this.CopyrightFooterControl.Controls.Add(LoadControl("CopyrightFooterControl.ascx"));
        return;
    }
    //----------------------------------------------------------------
    protected void login_team_weblog_Click(object sender, EventArgs e)
    {
        if (this.username_team.Text.IndexOf("'") >= 0 || this.password_team.Text.IndexOf("'") >= 0 || this.weblog_team.Text.IndexOf("'") >= 0)
        {
            Session.Abandon();
            this.Response.Redirect("/services/?i=unauthorized");
            return;
        }
        SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.Connection = connection;
        command.CommandText = string.Format("SELECT id,BlogID,ChatBoxIsEnabled,PostAccess,OthersPostAccess,SubjectedArchiveAccess,WeblogLinksAccess,DailyLinksAccess,TemplateAccess,PollAccess,LinkBoxAccess,NewsletterAccess,FullAccess FROM {0},usersInfo WHERE {0}.username='{1}' AND {0}.password='{2}' AND {0}.subdomain='{3}' AND usersInfo.i=BlogID", constants.SQLTeamWeblogName, this.username_team.Text, this.password_team.Text, this.weblog_team.Text);
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            //connection.Close();
            command.Dispose();
            Session.Abandon();
            this.Response.Redirect("/services/?i=unauthorized", true);
            return;
        }

        reader.Read();

        /*TeamWeblogAccessInfo info = new TeamWeblogAccessInfo();

        this.Session["username"] = this.username_team.Text;
        this.Session["subdomain"] = this.weblog_team.Text;*/
        Int64 _AuthorID = -1;
        _AuthorID = (Int64)reader["id"];
        /*this.Session["id"] = (Int64)reader["BlogID"];
        this.Session["ChatBoxIsEnabled"] = (bool)reader["ChatBoxIsEnabled"];
        info.PostAccess = (bool)reader["PostAccess"];
        info.SubjectedArchiveAccess = (bool)reader["SubjectedArchiveAccess"];
        info.WeblogLinksAccess = (bool)reader["WeblogLinksAccess"];
        info.DailyLinksAccess = (bool)reader["DailyLinksAccess"];
        info.TemplateAccess = (bool)reader["TemplateAccess"];
        info.NewsletterAccess = (bool)reader["NewsletterAccess"];
        info.OthersPostAccess = (bool)reader["OthersPostAccess"];
        info.LinkBoxAccess = (bool)reader["LinkBoxAccess"];
        info.PollAccess = (bool)reader["PollAccess"];
        info.FullAccess = (bool)reader["FullAccess"];*/

        reader.Close();
        connection.Close();
        command.Dispose();

        /*this.Session["TeamWeblogAccessInfo"] = info;
        this.Session["IsTeamWeblogSession"] = true;*/

        Session.Abandon();
        //---cookie-----
        EncryptedCookie ec = new EncryptedCookie(constants.key, constants.IV);
        this.Response.Cookies["userInfo"]["info"] = ec.EncryptWithMd5Hash(String.Format("{0},true", _AuthorID));
        this.Response.Cookies["userInfo"].Domain = constants.DomainBlog;
        if (this.cookieEnabled.Checked)
        {
            //this.Response.Cookies["userInfo"]["SessionMode"] = "false";
            this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(2);
        }
        else
        {
            //this.Response.Cookies["userInfo"]["SessionMode"] = "true";
            this.Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(constants.SessionTimeoutMinutes);
        }

        //--------------

        this.Response.Redirect("blogbuilderv1/cp.aspx", true);
        return;
    }
}
