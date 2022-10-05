/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
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
using services;
using IranBlog.Classes.Security;
using System.IO;
using System.Text.RegularExpressions;

namespace services.blogbuilderv1.ajax
{
    public partial class account : System.Web.UI.Page
    {
        //------------------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
                return false;
            else
                return true;
        }
        //------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, System.EventArgs e)
        {
            bool _IsLogin = Common.IsLoginProc(this);
            if (!_IsLogin)
            {
                if (this.Request.Form["_mode"] != null)
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

            if (this.file != null)
            {
                if (this.file.PostedFile != null)
                    if (this.file.PostedFile.FileName != "")
                    {
                        SaveBlogAvator(_SigninSessionInfo);
                        return;
                    }
            }

            if (this.Request.Form["_mode"] != null)
            {
                switch (this.Request.Form["_mode"])
                {
                    case "ajax.account.save.settings":
                        SaveBlogSettings(_SigninSessionInfo);
                        return;
                    case "ajax.account.save.password":
                        ChangePassword(_SigninSessionInfo);
                        return;
                    default:
                        return;
                }
            }

            LoadBlogSettings(_SigninSessionInfo);

        }
        //--------------------------------------------------------------------------------
        private void ChangePassword(SigninSessionInfo _SigninSessionInfo)
        {
            string _NewPassword = this.Request.Form["NewPassword"];
            string _LastPassword = this.Request.Form["LastPassword"];

            //---------user input validations-----------------------------------------------
            if (_NewPassword != null && _NewPassword != "")
            {
                if (_NewPassword.Length > 12)
                {
                    WriteStringToAjaxRequest(".تعداد حروف کلمه عبور جدید نمی تواند از 12 حرف بیشتر باشد");
                    return;
                }
            }
            else
                return;

            if (_LastPassword != null && _LastPassword != "")
            {
                if (_LastPassword.Length > 12)
                {
                    WriteStringToAjaxRequest(".تعداد حروف کلمه عبور قدیمی نمی تواند از 12 حرف بیشتر باشد");
                    return;
                }
            }
            else
                return;
            //------------------------------------------------------------------------------
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ChangePassword_AjaxAccountPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            command.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 12);
            command.Parameters["@NewPassword"].Value = _NewPassword;

            command.Parameters.Add("@LastPassword", SqlDbType.NVarChar, 12);
            command.Parameters["@LastPassword"].Value = _LastPassword;

            command.Parameters.Add("@NumAffectedRows", SqlDbType.Int);
            command.Parameters["@NumAffectedRows"].Direction = ParameterDirection.Output;


            command.ExecuteNonQuery();

            if ((int)command.Parameters["@NumAffectedRows"].Value == 1)
            {
                command.Dispose();
                connection.Close();

                WriteStringToAjaxRequest(".کلمه عبور جدید با موفقیت به روز رسانی شد");

                return;
            }
            else
            {
                command.Dispose();
                connection.Close();

                WriteStringToAjaxRequest(".کلمه عبور قدیمی اشتباه است");

                return;
            }
        }
        //--------------------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------------------
        private void LoadBlogSettings(SigninSessionInfo _SigninSessionInfo)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "LoadBlogSettings_AjaxAccountPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                this.BlogTitle.Text = reader["BlogTitle"].ToString();
                this.BlogAbout.Text = reader["BlogAbout"].ToString();
                this.BlogMaxPostShow.Text = reader["BlogMaxPostShow"].ToString();
                this.BlogFirstName.Text = reader["BlogFirstName"].ToString();
                this.BlogLastName.Text = reader["BlogLastName"].ToString();
                this.BlogEmail.Text = reader["BlogEmail"].ToString();

                if ((bool)reader["BlogEmailEnable"])
                {
                    this.BlogEmailEnable.SelectedItem.Selected = false;
                    this.BlogEmailEnable.Items.FindByValue("enable").Selected = true;
                }
                else
                {
                    this.BlogEmailEnable.SelectedItem.Selected = false;
                    this.BlogEmailEnable.Items.FindByValue("disable").Selected = true;
                }

                if ((bool)reader["BlogArciveDisplayMode"])
                {
                    this.BlogArciveDisplayMode.SelectedItem.Selected = false;
                    this.BlogArciveDisplayMode.Items.FindByValue("true").Selected = true;
                }
                else
                {
                    this.BlogArciveDisplayMode.SelectedItem.Selected = false;
                    this.BlogArciveDisplayMode.Items.FindByValue("false").Selected = true;
                }

                this.BlogCategory.SelectedItem.Selected = false;
                this.BlogCategory.Items.FindByValue((string)reader["BlogCategory"]).Selected = true;

                _SigninSessionInfo.ImageGuid = (string)reader["BlogImageGuid"];

                this.Session["SigninSessionInfo"] = _SigninSessionInfo;
            }

            reader.Close();
            command.Dispose();
            connection.Close();

            string[] rets = _SigninSessionInfo.ImageGuid.Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
            //this.ProfileImage.ImageUrl = String.Format("http://{0}.{1}/images/users/48x48/{2}/{3}.jpg?i={4}", rets[0], constants.BlogDomain, rets[1], rets[2], new Random().Next());
            this.ProfileImage.ImageUrl = String.Format("http://w3plus.{0}/users/images/{1}.jpg", constants.ZoneName, rets[2]);

            return;
        }
        //--------------------------------------------------------------------------------
        private void SaveBlogSettings(SigninSessionInfo _SigninSessionInfo)
        {
            string _BlogEmail = this.Request.Form["BlogEmail"] + "";
            string _BlogFirstName = this.Request.Form["BlogFirstName"] + "";
            string _BlogLastName = this.Request.Form["BlogLastName"] + "";
            string _BlogTitle = this.Request.Form["BlogTitle"] + "";
            string _BlogAbout = this.Request.Form["BlogAbout"] + "";
            string _BlogEmailEnable = this.Request.Form["BlogEmailEnable"] + "";
            string _BlogCategory = this.Request.Form["BlogCategory"] + "";
            string _BlogMaxPostShow = this.Request.Form["BlogMaxPostShow"] + "";
            string _BlogArciveDisplayMode = this.Request.Form["BlogArciveDisplayMode"] + "";

            if (_BlogTitle == "")
            {
                WriteStringToAjaxRequest(".عنوان وبلاگ خالی است");
                return;
            }

            if (_BlogEmail == "")
            {
                WriteStringToAjaxRequest(".آدرس ایمیل خالی است");
                return;
            }
            Regex rex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!rex.IsMatch(_BlogEmail))
            {
                WriteStringToAjaxRequest(".آدرس ایمیل نامعتبر است");
                return;
            }

            int __BlogMaxPostShow = 10;
            try
            {
                __BlogMaxPostShow = Convert.ToInt32(_BlogMaxPostShow);
            }
            catch {}

            if (__BlogMaxPostShow <= 0 || __BlogMaxPostShow > 30)
                __BlogMaxPostShow = 10;


            bool __BlogEmailEnable = true;

            if (_BlogEmailEnable == "disable")
                __BlogEmailEnable = false;

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SaveBlogSettings_AjaxAccountPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


            command.Parameters.Add("@BlogEmail", SqlDbType.VarChar, 50);
            command.Parameters["@BlogEmail"].Value = _BlogEmail;

            command.Parameters.Add("@BlogFirstName", SqlDbType.NVarChar, 30);
            command.Parameters["@BlogFirstName"].Value = _BlogFirstName;

            command.Parameters.Add("@BlogLastName", SqlDbType.NVarChar, 30);
            command.Parameters["@BlogLastName"].Value = _BlogLastName;

            command.Parameters.Add("@BlogTitle", SqlDbType.NVarChar, 100);
            command.Parameters["@BlogTitle"].Value = _BlogTitle;

            command.Parameters.Add("@BlogAbout", SqlDbType.NVarChar, 200);
            command.Parameters["@BlogAbout"].Value = _BlogAbout;

            command.Parameters.Add("@BlogEmailEnable", SqlDbType.Bit);
            command.Parameters["@BlogEmailEnable"].Value = __BlogEmailEnable;

            command.Parameters.Add("@BlogCategory", SqlDbType.VarChar, 50);
            command.Parameters["@BlogCategory"].Value = _BlogCategory;

            command.Parameters.Add("@BlogMaxPostShow", SqlDbType.Int);
            command.Parameters["@BlogMaxPostShow"].Value = __BlogMaxPostShow;

            command.Parameters.Add("@BlogArciveDisplayMode", SqlDbType.Bit);
            command.Parameters["@BlogArciveDisplayMode"].Value = Convert.ToBoolean(_BlogArciveDisplayMode);


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest(".تنظیمات وبلاگ با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void SaveBlogAvator(SigninSessionInfo _SigninSessionInfo)
        {
            if (this.file.PostedFile.ContentType.ToLower().IndexOf("image") < 0)
            {
                WriteStringToAjaxRequest(".فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد");
                return;
            }
            if (this.file.PostedFile.ContentLength > constants.MaxUserImageUploadSize)
            {
                WriteStringToAjaxRequest(".حداکثر حجم فایل تصویر نمی تواند از 256 کیلو بایت بیشتر باشد");
                return;
            }
            Bitmap fullImage = new Bitmap(this.file.PostedFile.InputStream);
            if (fullImage.Width < 48 || fullImage.Height < 48)
            {
                WriteStringToAjaxRequest(".حداقل ابعاد تصویر باید 48 پیکسل در 48 پیکسل باشد");
                fullImage.Dispose();
                return;
            }

            string[] rets = _SigninSessionInfo.ImageGuid.Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
            if (rets[2] == "default")
            {
                string guid = Guid.NewGuid().ToString().Replace("-", "");
                _SigninSessionInfo.ImageGuid = String.Format("img1,1,{0}", guid);
                rets[2] = guid;

                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SaveBlogAvator_AjaxAccountPage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = _SigninSessionInfo.BlogID;


                command.Parameters.Add("@BlogImageGuid", SqlDbType.VarChar, 50);
                command.Parameters["@BlogImageGuid"].Value = String.Format("img1,1,{0}", guid);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

            }
            string _guid = rets[2];
            StreamWriter srFull = new StreamWriter(constants.UserProfileImagesPath + _guid + ".jpg");
            fullImage.Save(srFull.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            srFull.Close();
            fullImage.Dispose();

            this.ProfileImage.ImageUrl = String.Format("http://w3plus.{0}/users/images/{1}.jpg?i={2}", constants.ZoneName, rets[2], new Random().Next());

            return;
        }
        //------------------------------------------------------------------------------------------
    }
}
