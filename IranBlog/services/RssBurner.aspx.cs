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



using System.Net;
using System.IO;

public partial class RssBurner : System.Web.UI.Page
{
    //------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = this.Request.QueryString["username"];
        if (username != null || username != "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}.peyghamak.com/rss.aspx?mode=RssBurner", username));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            this.Response.Write(reader.ReadToEnd());
            reader.Close();
            response.Close();
            request.Abort();
            this.Response.Flush();
            this.Response.End();
        }
        else
        {
            this.Response.Write("");
            this.Response.Flush();
            this.Response.End();
        }
    }
    //------------------------------------------------------------
}
