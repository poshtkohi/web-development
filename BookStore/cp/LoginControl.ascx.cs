
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

namespace bookstore.cp
{
    public partial class LoginControl : System.Web.UI.UserControl
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Common.IsLoginProc(this.Page))
            {
                this.LoginedPanel.Visible = true;
                this.UnLogginedPanel.Visible = false;
            }
            else
            {
                this.LoginedPanel.Visible = false;
                this.UnLogginedPanel.Visible = true;
            }*/
        }
        //--------------------------------------------------------------------
    }
}
