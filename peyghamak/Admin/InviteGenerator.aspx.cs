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
    public partial class Admin_InviteGenerator : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //--------------------------------------------------------------------
        protected void generate_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;

            for (int i = 1000 ; i < 2000 ; i++)
            {
                string _guid = Guid.NewGuid().ToString().Replace("-", "");
                command.CommandText = String.Format("INSERT INTO {0} (_ref) VALUES('{1}');", Constants.InvitationsTableName, _guid);
                command.ExecuteNonQuery();
                this.Response.Write(String.Format("http://www.{0}/signup.aspx?_ref={1}&i={2}", Constants.BlogDomain, _guid, i+1) + "<br>");
            }

            command.Dispose();
            connection.Close();
        }
        //--------------------------------------------------------------------
    }
}