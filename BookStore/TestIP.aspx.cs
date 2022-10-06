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

using System.IO;

public partial class TestIP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Response.Write(this.Request.UserHostAddress);
        StreamWriter sw = new StreamWriter(@"c:\\ip.txt", true);
        sw.WriteLine(this.Request.UserHostAddress);
        sw.Close();
    }
}
