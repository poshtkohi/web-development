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

using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using bookstore;

namespace bookstore.admin
{
    public partial class NewsImage : System.Web.UI.Page
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
            Int64 _NewsID = -1;

            try { _NewsID = Convert.ToInt64(this.Request.QueryString["NewsID"]);}
            catch{_NewsID = -1;}
            if(_NewsID < 0)
            {
                this.message.Text = "خطا در درخواست ورودی";
                this.message.Visible = true;
                return ;
            }
            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["action"] == "updated")
                {
                    this.message.Text = "تصویر خبر با موفقیت در سیستم به روز رسانی شد.";
                    this.message.Visible = true;
                }
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT NewsImageGuid FROM news WHERE id={0}", _NewsID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string temp = (string)reader["NewsImageGuid"];
                    this.rets.Value = temp;
                    reader.Close();
                    connection.Close();
                    command.Dispose();

                    this.ProfileImage.ImageUrl = String.Format("{0}news/{1}.jpg", constants.UploadedImagesPath, this.rets.Value);
                    return;
                }
                else
                {
                    this.message.Text = "خطا در درخواست ورودی";
                    this.message.Visible = true;
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    return;
                }
            }
        }
        //--------------------------------------------------------------------------------
        protected void save_Click(object sender, EventArgs e)
        {
            Int64 _NewsID = -1;

            try { _NewsID = Convert.ToInt64(this.Request.QueryString["NewsID"]); }
            catch { _NewsID = -1; }
            if (_NewsID < 0)
            {
                this.message.Text = "خطا در درخواست ورودی";
                this.message.Visible = true;
                return;
            }
            if (this.rets.Value == "" || this.rets.Value == "default")
            {
                UploadImageForFirstTime(_NewsID);
                return;
            }
            if (this.file.PostedFile.ContentType.ToLower().IndexOf("image") < 0)
            {
                this.message.Text = "فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد.";
                this.message.Visible = true;
                return;
            }
            if (this.file.PostedFile.ContentLength > constants.MaxUploadedImagesSize)
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

            string _guid = this.rets.Value;

            StreamWriter _sr_small = new StreamWriter(this.Request.PhysicalApplicationPath + @"\images\uploads\news\" + _guid + ".jpg");

            Bitmap _smallImage = (Bitmap)ImageResize.Crop(_fullImage, 180, 220, ImageResize.AnchorPosition.Center);
            _smallImage.Save(_sr_small.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _sr_small.Close();

            _fullImage.Dispose();
            _smallImage.Dispose();

            this.Response.Redirect("NewsImage.aspx?action=updated&NewsID=" + _NewsID.ToString(), true);
            return;
        }
        //--------------------------------------------------------------------------------
        private void UploadImageForFirstTime(Int64 _NewsID)
        {
            if (this.file.PostedFile.ContentType.ToLower().IndexOf("image") < 0)
            {
                this.message.Text = "فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد.";
                this.message.Visible = true;
                return;
            }
            if (this.file.PostedFile.ContentLength > constants.MaxUploadedImagesSize)
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


            string _guid = Guid.NewGuid().ToString().Replace("-", "");
            this.rets.Value = _guid;

            StreamWriter _sr_small = new StreamWriter(this.Request.PhysicalApplicationPath + @"\images\uploads\news\" + _guid + ".jpg");

            Bitmap _smallImage = (Bitmap)ImageResize.Crop(_fullImage, 180, 220, ImageResize.AnchorPosition.Center);
            _smallImage.Save(_sr_small.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _sr_small.Close();

            _fullImage.Dispose();
            _smallImage.Dispose();

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = string.Format("UPDATE news SET NewsImageGuid='{0}' WHERE id={1}", _guid, _NewsID);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            this.Response.Redirect("NewsImage.aspx?action=updated&NewsID="+_NewsID.ToString(), true);
            return;
        }
        //--------------------------------------------------------------------------------
}
}

