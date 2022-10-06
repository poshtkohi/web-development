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

namespace bookstore
{
    public partial class signout : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session.Clear();
            this.Session.Abandon();
            this.Response.Redirect("/?i=userlogout", true);
        }
        //--------------------------------------------------------------------
    }
}
