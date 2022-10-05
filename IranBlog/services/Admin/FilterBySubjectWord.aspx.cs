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

using AlirezaPoshtkoohiLibrary;
using System.Data.SqlClient;

public partial class Admin_FilterBySubjectWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //--------------------------------------------------------------------------------
    protected void _filter_Click(object sender, EventArgs e)
    {
        if (this.Request.Form["verify"] == "000")
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("update usersInfo set f='n' where f='y' AND title LIKE N'%{0}%'", this.Request.Form["_word"].Replace("'", ""));
            command.ExecuteNonQuery();
            connection.Close();

            this.message.Text = "The word was filtered successfully.";
            this.message.Visible = true;
            return;
        }
        else
        {
            this.message.Text = "Incorrect Verify Code.";
            this.message.Visible = true;
            return;
        }
    }
    //--------------------------------------------------------------------------------
    protected void _unfilter_Click(object sender, EventArgs e)
    {
        if (this.Request.Form["verify"] == "000")
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("update usersInfo set f='y' where f='n' AND title LIKE N'%{0}%'", this.Request.Form["_word"].Replace("'", ""));
            command.ExecuteNonQuery();
            connection.Close();

            this.message.Text = "The word was unfiltered successfully.";
            this.message.Visible = true;
            return;
        }
        else
        {
            this.message.Text = "Incorrect Verify Code.";
            this.message.Visible = true;
            return;
        }
    }
    //--------------------------------------------------------------------------------
}
