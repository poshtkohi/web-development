/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\admin\Stub_test_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'admin\test.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AlirezaPoshtkoohiLibrary;
using System.Data.SqlClient;

namespace services
{
    public partial class Migrated_test : test
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void Button1_Click(object sender, System.EventArgs e)
        {
            /*db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
                         constants.SQLUsername, constants.SQLPassowrd);
            d.del();*/
            /*d.CreateTableLatestUpdated();
            d.CreateTableInfoUsers();
            d.CreateTableComments();*/
        }

        protected void Button2_Click(object sender, System.EventArgs e)
        {
            db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
                constants.SQLUsername, constants.SQLPassowrd);
            d.CreateTableLatestUpdated();
            d.CreateTableInfoUsers();
            d.CreateTableComments();
            d.CreateTableLinks();
            d.CreateTableLinkss();
        }

        protected void Count_Click(object sender, System.EventArgs e)
        {
            /*db d = new db(constants.SQLServerAddress, constants.SQLDatabaseName,
                constants.SQLUsername, constants.SQLPassowrd);
            d.AlterWeblogsTable();*/
        }

        protected void Button3_Click(object sender, System.EventArgs e)
        {
            db regdomain = new db(constants.SQLServerAddress, constants.SQLWeblogsDbName, constants.SQLWeblogsDbUsername,
                    constants.SQLWeblogsDbPassowrd);
            int result = regdomain.CreateTableWeblogs("t");
        }

        protected void backup_Click(object sender, System.EventArgs e)
        {
            string query = "USE general\n" +
            "EXEC sp_addumpdevice 'disk', 'general1'," +
            "     'D:\\vhosts\\iranblog.com\\httpdocs\\services\\blogbuilderv1\\images\\backup\\general1.dat'" +
            "\nUSE general" +
            "\nEXEC sp_addumpdevice 'disk', 'general1Log'," +
            "	'D:\\vhosts\\iranblog.com\\httpdocs\\services\\blogbuilderv1\\images\\backup\\general1Log.dat'" +
            "\nBACKUP DATABASE general TO general1" +
            "\nBACKUP LOG general TO general1Log";

            query += ";USE weblogs\n" +
                "EXEC sp_addumpdevice 'disk', 'weblogs1'," +
                "     'D:\\vhosts\\iranblog.com\\httpdocs\\services\\blogbuilderv1\\images\\backup\\weblogs1.dat'" +
                "\nUSE general" +
                "\nEXEC sp_addumpdevice 'disk', 'weblogs1Log'," +
                "	'D:\\vhosts\\iranblog.com\\httpdocs\\services\\blogbuilderv1\\images\\backup\\weblogs1Log.dat'" +
                "\nBACKUP DATABASE weblogs TO weblogs1" +
                "\nBACKUP LOG weblogs TO weblogs1Log";

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }

        protected void Button4_Click(object sender, System.EventArgs e)
        {
            /*string query = "USE general\n" + 
            "EXEC sp_addumpdevice 'disk', 'general1'," + 
            "     'D:\\vhosts\\iranblog.com\\httpdocs\\general1.dat'" +
            "\nUSE general" +
            "\nEXEC sp_addumpdevice 'disk', 'general1Log'," +
            "	'D:\\vhosts\\iranblog.com\\httpdocs\\general1Log.dat'" +
            "\nBACKUP DATABASE general TO general1" +
            "\nBACKUP LOG general TO general1Log";*/
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "USE general\nDROP DATABASE general,weblogs";
            command.ExecuteNonQuery();
            connection.Close();
        }

        protected void execute_Click(object sender, System.EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = this.TextBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }

        protected void execute_posts_Click(object sender, System.EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = this.TextBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }

        protected void execute_newsletter_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLNewsletterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = this.TextBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }
        protected void execute_comments_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringCommentsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = this.TextBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }
        protected void execute_chatbox_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringChatBoxDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = this.TextBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }
        protected void execute_postimport_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringPostImporterDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = this.TextBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }
        protected void execute_pages_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringPagesDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = this.TextBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }
}
}
