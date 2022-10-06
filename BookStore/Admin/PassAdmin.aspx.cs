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
using System.IO;
using bookstore;

namespace bookstore.admin
{
    public partial class PassAdmin : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
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

        }
        //--------------------------------------------------------------------------------
        protected void save_Click(object sender, EventArgs e)
        {
            if (this.Request.Form["LastPassword"] == null || this.Request.Form["LastPassword"] == "")
            {
                this.message.Text = "کلمه عبور قدیمی خالی است.";
                this.message.Visible = true;
                return;
            }
            if (this.Request.Form["NewPassword"] != this.Request.Form["ConfirmNewPassword"])
            {
                this.message.Text = "کلمه عبور جدید با تکرار کلمه عبور جدید برابر نیست.";
                this.message.Visible = true;
                return;
            }

            if (this.Request.Form["LastPassword"] != constants.AdminPassword)
            {
                this.message.Text = "کلمه عبور قدیمی اشتباه است.";
                this.message.Visible = true;
            }
            else
            {
                string path = this.Request.PhysicalApplicationPath + @"\App_Code\constants.cs";

                StreamReader sr = new StreamReader(path);
                string temp = sr.ReadToEnd();
                sr.Close();

                temp = temp.Replace(this.Request.Form["LastPassword"], this.Request.Form["NewPassword"]);

                StreamWriter sw = new StreamWriter(path);
                sw.Write(temp);
                sw.Close();

                this.message.Text = "کلمه تایید با موفقیت به روز رسانی شد.";
                this.message.Visible = true;

                this.Session["AdminUsername"] = constants.AdminUsername;
                return;
            }
        }
        //--------------------------------------------------------------------------------
}
}
