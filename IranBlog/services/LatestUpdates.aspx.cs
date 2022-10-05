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

namespace services
{
    public partial class LatestUpdates : System.Web.UI.Page
    {
        //------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //------------------------------------------------------------------------------------------
        public static void ShowLatestUpdates(Page page)
        {
            try
            {
                string query = string.Format("SELECT TOP 50 subdomain,subject,date FROM {0} ORDER BY date DESC", constants.SQLLatestUpdatedWeblogsTableName);
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                TimeSpan time;
                int i = 1;
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        time = DateTime.Now - read.GetDateTime(2);
                        page.Response.Write(String.Format("<div class='subitem{3}'><a href='http://{0}.iranblog.com/' target='_blank'><span class='si-1'>»</span> {1} </a><br /><br />به روز شده در <span class='si-2'>{2}</span> دقيقه   پيش</div>", read.GetString(0), read.GetString(1), Math.Abs(time.Minutes), i));
                        if (i == 1)
                            i = 2;
                        else
                            i = 1;
                    }
                }
                read.Close();
                //connection.Close();
                command.Dispose();
                return;
            }
            catch
            {
                page.Response.Write("SQL Server Internal Error.");
                return;
            }
        }
        //------------------------------------------------------------------------------------------
    }
}
