/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

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

namespace services.ChatBox
{
    public partial class Smilies : System.Web.UI.Page
    {
        //----------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCopyrightFooterControl();
            return;
        }
        //----------------------------------------------------------------
        private void SetCopyrightFooterControl()
        {
            this.CopyrightFooterControl.Controls.Add(LoadControl(@"..\CopyrightFooterControl.ascx"));
            return;
        }
        //----------------------------------------------------------------
    }
}
