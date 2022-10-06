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

using System.Web;
using System.IO;
using System.Net;
using System.Text;


public partial class IpTestRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebRequest request = WebRequest.Create("http://www.peyghamak.com/TestIp.aspx");
        // If required by the server, set the credentials.
        request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        // Display the status.
        Console.WriteLine(response.StatusDescription);
        // Get the stream containing content returned by the server.
        Stream dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.
        string responseFromServer = reader.ReadToEnd();
        // Display the content.
        Console.WriteLine(responseFromServer);
        // Cleanup the streams and the response.
        reader.Close();
        dataStream.Close();
        response.Close();

    }
}
