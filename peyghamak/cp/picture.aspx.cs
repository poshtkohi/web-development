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
using System.Drawing;
using System.Data.SqlClient;

using Peyghamak.cp.Picture;

namespace Peyghamak.cp
{
    public partial class picture : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login.IsMyCp(this))
                return;
            MetaCopyrightControl();
            SetSiteFooterControl();
            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["action"] == "updated")
                {
                    this.message.Text = "تصویر شما با موفقیت در سیستم به روز رسانی شد.";
                    this.message.Visible = true;
                }
                SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT ImageGuid FROM {0} WHERE id={1}", Constants.AccountsTableName, ((SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string temp = (string)reader["ImageGuid"];
                this.rets.Value = temp;
                reader.Close();

                this.ProfileImage.ImageUrl = Login.Build75x75ImageUrl(this.rets.Value, true);
                return;
            }
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
        protected void save_Click(object sender, EventArgs e)
        {
            if (this.rets.Value == "")
            {
                UploadImageForFirstTime();
                return;
            }
            if (this.file.PostedFile.ContentType.ToLower().IndexOf("image") < 0)
            {
                this.message.Text = "فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد.";
                this.message.Visible = true;
                return;
            }
            if (this.file.PostedFile.ContentLength > Constants.MaxUserImageUploadSize)
            {
                this.message.Text = ".حداکثر حجم فایل تصویر نمی تواند از 256 کیلو بایت بیشتر باشد";
                this.message.Visible = true;
                return;
            }
            Bitmap _fullImage = new Bitmap(this.file.PostedFile.InputStream);
            if (_fullImage.Width < 75 || _fullImage.Height < 75)
            {
                this.message.Text = "حداقل ابعاد تصویر باید 75 پیکسل در 75 پیکسل باشد.";
                this.message.Visible = true;
                _fullImage.Dispose();
                return;
            }
            string[] rets = this.rets.Value.Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
            string _directory = Constants.UserImageManagment.FindPhysicalDirectoryImageServerLocation(rets[0]);
            string _folderPart = rets[1];
            string _guid = rets[2];

            StreamWriter sr75x75 = new StreamWriter(_directory + @"users\full\" + _folderPart + @"\" + _guid + ".jpg");
            StreamWriter sr40x40 = new StreamWriter(_directory + @"users\40x40\" + _folderPart + @"\" + _guid + ".jpg");
            StreamWriter sr38x38 = new StreamWriter(_directory + @"users\38x38\" + _folderPart + @"\" + _guid + ".jpg");

            /*Bitmap _75x75Image = new Bitmap(_fullImage, 75, 75);
            Bitmap _40x40Image = new Bitmap(_fullImage, 40, 40);
            Bitmap _38x38Image = new Bitmap(_fullImage, 38, 38);*/

            Bitmap _75x75Image = (Bitmap)ImageResize.Crop(_fullImage, 75, 75, ImageResize.AnchorPosition.Center);
            Bitmap _40x40Image = (Bitmap)ImageResize.Crop(_fullImage, 40, 40, ImageResize.AnchorPosition.Center);
            Bitmap _38x38Image = (Bitmap)ImageResize.Crop(_fullImage, 38, 38, ImageResize.AnchorPosition.Center);

            _75x75Image.Save(sr75x75.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _40x40Image.Save(sr40x40.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _38x38Image.Save(sr38x38.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);

			_fullImage.Save(_directory + @"users\full\full\" + _guid + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            sr75x75.Close();
            sr40x40.Close();
            sr38x38.Close();

            _fullImage.Dispose();
            _75x75Image.Dispose();
            _40x40Image.Dispose();
            _38x38Image.Dispose();

            this.Response.Redirect("picture.aspx?action=updated", true);
            return;
        }
        //--------------------------------------------------------------------
        private void UploadImageForFirstTime()
        {
            if (this.file.PostedFile.ContentType.ToLower().IndexOf("image") < 0)
            {
                this.message.Text = "فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد";
                this.message.Visible = true;
                return;
            }
            if (this.file.PostedFile.ContentLength > Constants.MaxUserImageUploadSize)
            {
                this.message.Text = ".حداکثر حجم فایل تصویر نمی تواند از 256 کیلو بایت بیشتر باشد";
                this.message.Visible = true;
                return;
            }
            Bitmap _fullImage = new Bitmap(this.file.PostedFile.InputStream);
            if (_fullImage.Width < 75 || _fullImage.Height < 75)
            {
                this.message.Text = "حداقل ابعاد تصویر باید 75 پیکسل در 75 پیکسل باشد.";
                this.message.Visible = true;
                _fullImage.Dispose();
                return;
            }


            SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            Constants.UserImageManagment.ImageInfo info = Constants.UserImageManagment.FindImageServerInformation();

            string guid = Guid.NewGuid().ToString().Replace("-", "");

            StreamWriter sr75x75 = new StreamWriter(Constants.CurrentUsersImageDirectory + @"users\full\" + info.FolderPart + @"\" + guid + ".jpg");
            StreamWriter sr40x40 = new StreamWriter(Constants.CurrentUsersImageDirectory + @"users\40x40\" + info.FolderPart + @"\" + guid + ".jpg");
            StreamWriter sr38x38 = new StreamWriter(Constants.CurrentUsersImageDirectory + @"users\38x38\" + info.FolderPart + @"\" + guid + ".jpg");

            /*Bitmap _75x75Image = new Bitmap(_fullImage, 75, 75);
            Bitmap _40x40Image = new Bitmap(_fullImage, 40, 40);
            Bitmap _38x38Image = new Bitmap(_fullImage, 38, 38);*/
            Bitmap _75x75Image = (Bitmap)ImageResize.Crop(_fullImage, 75, 75, ImageResize.AnchorPosition.Center);
            Bitmap _40x40Image = (Bitmap)ImageResize.Crop(_fullImage, 40, 40, ImageResize.AnchorPosition.Center);
            Bitmap _38x38Image = (Bitmap)ImageResize.Crop(_fullImage, 38, 38, ImageResize.AnchorPosition.Center);

            _75x75Image.Save(sr75x75.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _40x40Image.Save(sr40x40.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _38x38Image.Save(sr38x38.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
			_fullImage.Save(Constants.CurrentUsersImageDirectory + @"users\full\full\" + guid + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            sr75x75.Close();
            sr40x40.Close();
            sr38x38.Close();

            _fullImage.Dispose();
            _75x75Image.Dispose();
            _40x40Image.Dispose();
            _38x38Image.Dispose();

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            string _ImageGuid = String.Format("{0},{1},{2}", info.ServerName, info.FolderPart, guid);
            command.CommandText = string.Format("UPDATE {0} SET ImageGuid='{1}' WHERE id={2}", Constants.AccountsTableName, _ImageGuid, _info.BlogID);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            _info.ImageGuid = _ImageGuid;
            this.Session["SigninSessionInfo"] = _info;

            this.Response.Redirect("picture.aspx?action=updated", true);
            return;
        }
        /*protected void save_Click(object sender, EventArgs e)
        {
            if (this.rets.Value == "")
            {
                UploadImageForFirstTime();
                return;
            }
            if (this.file.PostedFile.ContentType.ToLower().IndexOf("image") < 0)
            {
                this.message.Text = "فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد.";
                this.message.Visible = true;
                return;
            }
            if (this.file.PostedFile.ContentLength > Constants.MaxUserImageUploadSize)
            {
                this.message.Text = ".حداکثر حجم فایل تصویر نمی تواند از 256 کیلو بایت بیشتر باشد";
                this.message.Visible = true;
                return;
            }
            Bitmap _fullImage = new Bitmap(this.file.PostedFile.InputStream);
            if (_fullImage.Width < 75 || _fullImage.Height < 75)
            {
                this.message.Text = "حداقل ابعاد تصویر باید 75 پیکسل در 75 پیکسل باشد.";
                this.message.Visible = true;
                _fullImage.Dispose();
                return;
            }
            string[] rets = this.rets.Value.Split(new char[] { ',' }); //rets[0]=ServerName,ret[1]=FoderPart,ret[2]=guid
            string _directory = Constants.UserImageManagment.FindPhysicalDirectoryImageServerLocation(rets[0]);
            string _folderPart = rets[1];
            string _guid = rets[2];

            StreamWriter sr75x75 = new StreamWriter(_directory + @"users\full\" + _folderPart + @"\" + _guid + ".jpg");
            StreamWriter sr40x40 = new StreamWriter(_directory + @"users\40x40\" + _folderPart + @"\" + _guid + ".jpg");
            StreamWriter sr38x38 = new StreamWriter(_directory + @"users\38x38\" + _folderPart + @"\" + _guid + ".jpg");

            Graphics g = Graphics.FromImage(_fullImage);

            Bitmap _75x75Image = new Bitmap(_fullImage, 75, 75);
            Bitmap _40x40Image = new Bitmap(_fullImage, 40, 40);
            Bitmap _38x38Image = new Bitmap(_fullImage, 38, 38);

            _75x75Image.Save(sr75x75.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _40x40Image.Save(sr40x40.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _38x38Image.Save(sr38x38.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            sr75x75.Close();
            sr40x40.Close();
            sr38x38.Close();

            _fullImage.Dispose();
            _75x75Image.Dispose();
            _40x40Image.Dispose();
            _38x38Image.Dispose();

            this.Response.Redirect("picture.aspx?action=updated", true);
            return;
        }
        //--------------------------------------------------------------------
        private void UploadImageForFirstTime()
        {
            if (this.file.PostedFile.ContentType.ToLower().IndexOf("image") < 0)
            {
                this.message.Text = "فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد";
                this.message.Visible = true;
                return;
            }
            if (this.file.PostedFile.ContentLength > Constants.MaxUserImageUploadSize)
            {
                this.message.Text = ".حداکثر حجم فایل تصویر نمی تواند از 256 کیلو بایت بیشتر باشد";
                this.message.Visible = true;
                return;
            }
            Bitmap _fullImage = new Bitmap(this.file.PostedFile.InputStream);
            if (_fullImage.Width < 75 || _fullImage.Height < 75)
            {
                this.message.Text = "حداقل ابعاد تصویر باید 75 پیکسل در 75 پیکسل باشد.";
                this.message.Visible = true;
                _fullImage.Dispose();
                return;
            }


            SigninSessionInfo _info = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            Constants.UserImageManagment.ImageInfo info = Constants.UserImageManagment.FindImageServerInformation();

            string guid = Guid.NewGuid().ToString().Replace("-", "");

            StreamWriter sr75x75 = new StreamWriter(Constants.CurrentUsersImageDirectory + @"users\full\" + info.FolderPart + @"\" + guid + ".jpg");
            StreamWriter sr40x40 = new StreamWriter(Constants.CurrentUsersImageDirectory + @"users\40x40\" + info.FolderPart + @"\" + guid + ".jpg");
            StreamWriter sr38x38 = new StreamWriter(Constants.CurrentUsersImageDirectory + @"users\38x38\" + info.FolderPart + @"\" + guid + ".jpg");

            Bitmap _75x75Image = new Bitmap(_fullImage, 75, 75);
            Bitmap _40x40Image = new Bitmap(_fullImage, 40, 40);
            Bitmap _38x38Image = new Bitmap(_fullImage, 38, 38);

            _75x75Image.Save(sr75x75.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _40x40Image.Save(sr40x40.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _38x38Image.Save(sr38x38.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            sr75x75.Close();
            sr40x40.Close();
            sr38x38.Close();

            _fullImage.Dispose();
            _75x75Image.Dispose();
            _40x40Image.Dispose();
            _38x38Image.Dispose();

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            string _ImageGuid = String.Format("{0},{1},{2}", info.ServerName, info.FolderPart, guid);
            command.CommandText = string.Format("UPDATE {0} SET ImageGuid='{1}' WHERE id={2}", Constants.AccountsTableName, _ImageGuid, _info.BlogID);
            command.ExecuteNonQuery();
                                        command.Dispose();
                            connection.Close();

            _info.ImageGuid = _ImageGuid;
            this.Session["SigninSessionInfo"] = _info;

            this.Response.Redirect("picture.aspx?action=updated", true);
            return;
        }*/
        //--------------------------------------------------------------------
    }
}
