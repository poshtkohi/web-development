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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;

namespace services
{
    public partial class rss : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            string subdomain = FindSubdomain(this);
            if (string.Compare(subdomain, "www") == 0 || string.Compare(subdomain, "iranblog") == 0)
            {
                this.Response.Redirect(constants.WeblogUrl, true);
                return;
            }
            else
            {
                //----------www---------------------
                int p = subdomain.IndexOf(".");
                if (p > 0)
                    subdomain = subdomain.Substring(p + 1);
                //----------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT i,title FROM {0} WHERE subdomain='{1}'", constants.SQLUsersInformationTableName, subdomain);
            SqlDataReader reader = command.ExecuteReader();
            Int64 BlogID = -1;
            string title = null;
            if (reader.Read())
            {
                BlogID = (Int64)reader["i"];
                title = (string)reader["title"];
            }
            else
            {
                reader.Close();
                connection.Close();
                command.Dispose();
                this.Response.Redirect(constants.WeblogUrl, true);
                return;
            }
            reader.Close();
            command.Dispose();


            connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT TOP 5 id,subject,content,date FROM {0} WHERE BlogID='{1}' AND IsDeleted=0 ORDER BY id DESC", constants.SQLPostsTableName, BlogID);
            reader = command.ExecuteReader();
            this.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<rss version=\"2.0\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\">\r\n<channel>");
            this.Response.Write(String.Format("<title>{0}</title><link>/</link><description>{0}</description><language>fa</language><generator>iranblog.com</generator>", title, this.Request.Url.Host));
            this.Response.Flush();
            string item = "\r\n<item>\r\n<title>{$$title}</title>\r\n<pubDate>{$$pubDate}</pubDate>\r\n<dc:creator>{$$creator}</dc:creator>\r\n<link>{$$link}</link>\r\n<description>{$$description}</description>\r\n</item>";
            /*string item = "\r\n<item>\r\n<title>{$$title}</title>\r\n<link>{$$link}</link>\r\n<description>{$$description}</description>\r\n<pubDate>{$$pubDate}</pubDate>\r\n<dc:creator>{$$creator}</dc:creator>\r\n</item>";
            this.Response.Write(item);
            this.Response.Write("\r\n</channel>\r\n</rss>");
            this.Response.Flush();
            this.Response.Close();
            return;*/
            string temp = null;
            while (reader.Read())
            {
                temp = item;
                temp = Regex.Replace(temp, "(\\{\\$\\$title\\})", Context.Server.HtmlEncode((string)reader["subject"]));
                temp = Regex.Replace(temp, "(\\{\\$\\$pubDate\\})", ((DateTime)reader["date"]).ToString("r"));
                temp = Regex.Replace(temp, "(\\{\\$\\$creator\\})", subdomain);
                temp = Regex.Replace(temp, "(\\{\\$\\$link\\})", Context.Server.HtmlEncode("/?mode=DirectLink&id=" + (Int64)reader["id"]));
                temp = Regex.Replace(temp, "(\\{\\$\\$description\\})", Context.Server.HtmlEncode((string)reader["content"]));
                this.Response.Write(temp);
                this.Response.Flush();
            }
            reader.Close();
            command.Dispose();

            this.Response.Write("\r\n</channel>\r\n</rss>");
            this.Response.Flush();
            this.Response.End();

            return;
            }

        }
        //--------------------------------------------------------------------------------
        private string FindSubdomain(Page page)
        {
            if (String.Compare(this.Request.Url.Host, constants.DomainBlog) == 0)
                return this.Request.Url.Host.Substring(0, this.Request.Url.Host.IndexOf('.'));
            else
                return this.Request.Url.Host.Substring(0, this.Request.Url.Host.IndexOf("." + constants.DomainBlog));
        }
        //--------------------------------------------------------------------------------
    }
}
