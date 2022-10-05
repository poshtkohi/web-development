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
using AlirezaPoshtkoohiLibrary;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1
{
    public partial class ChatBox : System.Web.UI.Page
    {
        //----------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        //----------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Common.IsLoginProc(this);
            if (!_IsLogin)
            {
                if (this.Request.Form["mode"] != null)
                    Common.WriteStringToAjaxRequest("Logouted", this);
                else
                    this.Response.Redirect("Logouted.aspx", true);
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }
            if (!_SigninSessionInfo.ChatBoxIsEnabled)
                this.ChatBoxIsEnabler.Text = "فعال سازی نمایش تالار گفتمان وبلاگ";
            else
                this.ChatBoxIsEnabler.Text = "غیر فعال سازی نمایش تالار گفتمان وبلاگ";
        }
        //----------------------------------------------------------------
        protected void ChatBoxIsEnabler_Click(object sender, EventArgs e)
        {
            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ChatBoxEnableDisable_ChatBox_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;

            bool _newState = !((bool)this.Session["ChatBoxIsEnabled"]);
            this.Session["ChatBoxIsEnabled"] = _newState;
            command.Parameters.Add("@ChatBoxIsEnabled", SqlDbType.Bit);
            command.Parameters["@ChatBoxIsEnabled"].Value = _newState;

            //------Linked Server settings---------------
            //command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            //command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            if (!_newState)
                this.ChatBoxIsEnabler.Text = "فعال سازی نمایش تالار گفتمان وبلاگ";
            else
                this.ChatBoxIsEnabler.Text = "غیر فعال سازی نمایش تالار گفتمان وبلاگ";
            return;
        }
        //----------------------------------------------------------------c
    }
}
