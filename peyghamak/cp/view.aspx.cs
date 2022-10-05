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
using System.Data.SqlClient;

namespace Peyghamak.cp
{
    public partial class view : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login.IsMyCp(this))
                return;
            if (this.Request.QueryString["id"] != null && this.Request.QueryString["id"] != "")
                SaveThemeID(this.Request.QueryString["id"]);
            MetaCopyrightControl();
            SetSiteFooterControl();
        }
        //--------------------------------------------------------------------
        private void SetSiteFooterControl()
        {
            this.SiteFooterSection.Controls.Add(LoadControl("../SiteFooterControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void MetaCopyrightControl()
        {
            this.MetaCopyrightSection.Controls.Add(LoadControl("../MetaCopyrightControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void SaveThemeID(string id)
        {
            int _ThemeID = 1;

            try { _ThemeID = Convert.ToInt32(id); }
            catch
            {
                if (_ThemeID <= 0)
                    _ThemeID = 1;
            }
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SaveThemeID_ViewPage_proc";
            command.CommandType = CommandType.StoredProcedure;

            //--------------------
            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            command.Parameters.Add("@ThemeID", SqlDbType.Int);
            command.Parameters["@ThemeID"].Value = _ThemeID;

            command.Parameters.Add("@ThemeString", SqlDbType.NVarChar, 50);
            command.Parameters["@ThemeString"].Direction = ParameterDirection.Output;
            //--------------------

            command.ExecuteNonQuery();

            _SigninSessionInfo.ThemeString = (string)command.Parameters["@ThemeString"].Value;

            command.Dispose();
            connection.Close();

            this.Session["SigninSessionInfo"] = _SigninSessionInfo;

            return;
        }
        //--------------------------------------------------------------------
    }
}
