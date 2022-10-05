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

using System.Web.Configuration;
using System.Text;

using AlirezaPoshtkoohiLibrary;
using System.IO;
using DotGrid.DotSec;

namespace services
{
    public partial class sa : System.Web.UI.Page
    {
        //----------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["action"] != null && this.Request.QueryString["action"] == "updated")
            {
                RijndaelEncryption _rijndael = new RijndaelEncryption(RijndaelEncryption.Base64StringToBinary(constants.key), RijndaelEncryption.Base64StringToBinary(constants.IV));
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyAppRoot");
                AppSettingsSection _appSettings = (AppSettingsSection)config.GetSection("appSettings");
                this.Cache.Insert("_db_p", RijndaelEncryption.BinaryToString(_rijndael.decrypt(RijndaelEncryption.Base64StringToBinary(_appSettings.Settings["db_p"].Value))));
                this.message.Text  = "Password saved successfully.";
                this.message.Visible = true;
            }
            //this.Response.Write(Cache["_db_p"]);
        }
        //----------------------------------------------------------------------------------
        protected void submit_Click(object sender, EventArgs e)
        {
            if (this._saPassword.Text == "")
            {
                this.message.Text = "New Password is empty.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["verify"] == "000")
            {
                RijndaelEncryption _rijndael = new RijndaelEncryption(RijndaelEncryption.Base64StringToBinary(constants.key), RijndaelEncryption.Base64StringToBinary(constants.IV));
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyAppRoot");
                AppSettingsSection _appSettings = (AppSettingsSection)config.GetSection("appSettings");
                FileStream _fsConfig = new FileStream(constants.RootPath + "web.config", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(_fsConfig);
                string _webConfig = sr.ReadToEnd();
                _webConfig = _webConfig.Replace(String.Format("<add key=\"db_p\" value=\"{0}\"/>", _appSettings.Settings["db_p"].Value), String.Format("<add key=\"db_p\" value=\"{0}\"/>", RijndaelEncryption.BinaryToBase64String(_rijndael.encrypt(RijndaelEncryption.StringToBinary(this._saPassword.Text)))));
                _fsConfig.SetLength(0);
                StreamWriter sw = new StreamWriter(_fsConfig);
                sw.Write(_webConfig);
                sw.Close();
                sr.Close();
                _fsConfig.Close();
                this.Response.Redirect("sa.aspx?action=updated", true);
                return;
            }
            else
            {
                this.message.Text = "Incorrect Verify Code.";
                this.message.Visible = true;
                return;
            }
        }
        //----------------------------------------------------------------------------------
    }
}
