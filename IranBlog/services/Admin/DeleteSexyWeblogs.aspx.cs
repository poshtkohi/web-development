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
using System.IO;
using AlirezaPoshtkoohiLibrary;
using ServerManagement;
using System.Management;
using System.DirectoryServices;
using System.Threading;

namespace services.Admin
{
    public partial class DeleteSexyWeblogs : System.Web.UI.Page
    {
        private struct WeblogInfo
        {
            private Int64 id;
            private string subdomain;
            public WeblogInfo(Int64 id, string subdomain)
            {
                this.id = id;
                this.subdomain = subdomain;
            }
            public Int64 ID
            {
                get
                {
                    return this.id;
                }
            }

            public string Subdomain
            {
                get
                {
                    return this.subdomain;
                }
            }
        }
        //------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //------------------------------------------------------------
        protected void delete_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("SELECT i,subdomain,title FROM {0}", constants.SQLUsersInformationTableName);
            SqlDataReader reader = command.ExecuteReader();
            string title = null;
            while (reader.Read())
            {
                if (!reader.IsDBNull(1) && !reader.IsDBNull(2))
                {
                    title = (string)reader["title"];//.ToString().ToLower();
                    if (title.IndexOf("۱۲۳") == 0 || title.IndexOf("میهن بلاگ") >= 0)
                        al.Add(new WeblogInfo((Int64)reader["i"], (string)reader["subdomain"]));
                }
            }
            reader.Close();
            /*IIS iis = new IIS(constants.password);
            DNS dns = new DNS(constants.password);*/
            for (int i = 0; i < al.Count; i++)
            {
                WeblogInfo temp = (WeblogInfo)al[i];
                /*iis.DeleteWebSite(temp.Subdomain, constants.ZoneName);
                iis.DeleteWebSite("www." + temp.Subdomain, constants.ZoneName);
                dns.DeleteRecord(constants.DNSServerAddress, constants.ZoneName, temp.Subdomain);
                dns.DeleteRecord(constants.DNSServerAddress, constants.ZoneName, "www." + temp.Subdomain);*/
              
                if (Directory.Exists(constants.RootDircetoryWeblogs + "\\" + temp.Subdomain))
                    Directory.Delete(constants.RootDircetoryWeblogs + "\\" + temp.Subdomain, true);
                command.CommandText = string.Format("UPDATE {0} SET IsWeblogExpired=1 WHERE i={1}", constants.SQLUsersInformationTableName, temp.ID);
                command.ExecuteNonQuery();
            }
            //iis.Dispose();
            //dns.Dispose();
            command.Dispose();
        }
        //------------------------------------------------------------
    }
}
