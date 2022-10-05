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
using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;

namespace services
{
    public partial class commenting : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 _BlogID = -1;
            try
            {
                if (this.Request.QueryString["BlogID"] != null)
                    _BlogID = Convert.ToInt64(this.Request.QueryString["BlogID"]);
                else
                    _BlogID = Convert.ToInt64(this.Request.Form["BlogID"]);
                if (_BlogID <= 0)
                {
                    this.Response.Redirect(constants.WeblogUrl, true);
                    return;
                }
            }
            catch
            {
                this.Response.Redirect(constants.WeblogUrl, true);
                return;
            }

            Int64 _PostID = -1;
            try
            {
                if (this.Request.QueryString["PostID"] != null)
                    _PostID = Convert.ToInt64(this.Request.QueryString["PostID"]);
                else
                    _PostID = Convert.ToInt64(this.Request.Form["PostID"]);
                if (_PostID <= 0)
                {
                    this.Response.Redirect(constants.WeblogUrl, true);
                    return;
                }
            }
            catch
            {
                this.Response.Redirect(constants.WeblogUrl, true);
                return;
            }
            if (this.HiddenBlogID.Value == "")
            {
                this.HiddenBlogID.Value = _BlogID.ToString();
                this.HiddenPostID.Value = _PostID.ToString();
            }
            bool _IsLogined = IsLoginProc(_BlogID, _PostID);
            if (this.Request.Form["action"] != null)
            {
                if (this.Request.Form["action"] == "delete")
                {
                    CommentDelete(_IsLogined, _BlogID, _PostID);
                    return;
                }
                if (this.Request.Form["action"] == "verify")
                {
                    CommentVerify(_IsLogined, _BlogID, _PostID);
                    return;
                }
                if (this.Request.Form["action"] == "activate")
                {
                    CommentPreActivation(_IsLogined, _BlogID, _PostID, true);
                    return;
                }
                if (this.Request.Form["action"] == "deactivate")
                {
                    CommentPreActivation(_IsLogined, _BlogID, _PostID, false);
                    return;
                }
                else
                    return;
            }
            if (this.Request.Form["ShowMode"] != null && this.Request.Form["ShowMode"] != "")
            {
                if (this.Request.Form["ShowMode"] == "comments")
                    ShowComments(_IsLogined, _BlogID, _PostID);
                return;
            }
            if (this.Request.Form["CommentContent"] != null && this.Request.Form["CommentContent"] == "")
            {
                WriteStringToAjaxRequest("EmptyContent");
                return;
            }
            if (this.Request.Form["CommentContent"] != null && this.Request.Form["CommentContent"] != "")
            {
                PostComment(_BlogID, _PostID);
                return;
            }
            if (this.Request.QueryString["action"] != null && this.Request.QueryString["action"] == "logout")
            {
                this.Session["_LogindBlogID_CommentingSection"] = null;
                this.resultText.InnerHtml = "خروج از مرکز مدیریت نظرات با موفقیت انجام شد.";
                _IsLogined = false;
            }
            PageSettings(_IsLogined, _BlogID, _PostID);
            return;
        }
        //--------------------------------------------------------------------
        private void PageSettings(bool _IsLogined, Int64 _BlogID, Int64 _PostID)
        {
            if (!_IsLogined)
            {
                this.LoginPanel.Visible = true;
                this.LogoutPanel.Visible = false;
                if ((bool)this.Session["_IsShowCommentsPreVerify"])
                    this.mesage.Visible = false;
                else
                    this.mesage.Visible = true;
            }
            else
            {
                if ((bool)this.Session["_IsShowCommentsPreVerify"])
                {
                    this.HiddenActivateDisplay.Value = "none";
                    this.HiddenFieldDeactivateDisplay.Value = "block";
                }
                else
                {
                    this.HiddenActivateDisplay.Value = "block";
                    this.HiddenFieldDeactivateDisplay.Value = "none";
                }
                this.LoginPanel.Visible = false;
                this.LogoutPanel.Visible = true;
                this.mesage.Visible = false;
            }
            return;
        }
        //--------------------------------------------------------------------
        private void PostComment(Int64 _BlogID, Int64 _PostID)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringCommentsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PostComment_CommentsPage_proc";

            bool _IsPrivateComment = false;
            try { _IsPrivateComment = Convert.ToBoolean(this.Request.Form["IsPrivateComment"]); }
            catch { }

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@CommentDate", SqlDbType.DateTime);
            command.Parameters["@CommentDate"].Value = DateTime.Now;

            command.Parameters.Add("@CommenterName", SqlDbType.NVarChar, 50);
            command.Parameters["@CommenterName"].Value = this.Request.Form["name"];

            command.Parameters.Add("@CommenterUrl", SqlDbType.NVarChar, 100);
            command.Parameters["@CommenterUrl"].Value = this.Request.Form["url"];

            command.Parameters.Add("@CommenterEmail", SqlDbType.VarChar, 50);
            command.Parameters["@CommenterEmail"].Value = this.Request.Form["email"].Trim().ToLower();

            command.Parameters.Add("@CommentContent", SqlDbType.NVarChar);
            command.Parameters["@CommentContent"].Value = this.Request.Form["CommentContent"];

            command.Parameters.Add("@IsPrivateComment", SqlDbType.Bit);
            command.Parameters["@IsPrivateComment"].Value = _IsPrivateComment;


            //------Linked Server settings---------------
            /*command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;

            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;*/
            //-------------------------------------------

            command.ExecuteNonQuery();
            command.Dispose();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void ShowComments(bool _IsLogined, Int64 _BlogID, Int64 _PostID)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringCommentsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowComments_CommentsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@IsLogined", SqlDbType.Bit);
            command.Parameters["@IsLogined"].Value = _IsLogined;

            command.Parameters.Add("@IsShowCommentsPreVerify", SqlDbType.Bit);
            command.Parameters["@IsShowCommentsPreVerify"].Value = (bool)this.Session["_IsShowCommentsPreVerify"];


            //------Linked Server settings---------------
            /*command.Parameters.Add("@IsPosts1DbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsPosts1DbLinkedServer"].Value = Constants.IsPosts1DbLinkedServer;

            command.Parameters.Add("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            command.Parameters["@IsAccountsDbLinkedServer"].Value = Constants.IsAccountsDbLinkedServer;*/
            //-------------------------------------------

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (this.Cache["_commentsTemplate"] != null)
                    template = (string)this.Cache["_commentsTemplate"];
                else
                {
                    StreamReader sr = new StreamReader(this.Request.PhysicalApplicationPath + @"\commenting\CommentTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                    this.Cache.Insert("_commentsTemplate", template);
                }

                if (!_IsLogined)
                {
                    TagDelete(ref template, "delete");
                    TagDelete(ref template, "verify");
                }
                template = template.Replace("[BlogID]", _BlogID.ToString());
                template = template.Replace("[PostID]", _PostID.ToString());
                string temp = "";
                while (reader.Read())
                {

                    temp = template;
                    //---
                    if (_IsLogined && (bool)reader["IsVerified"])
                        TagDelete(ref temp, "verify");
                    FieldFormat(ref temp, "name", (string)reader["CommenterName"]);
                    FieldFormat(ref temp, "url", (string)reader["CommenterUrl"]);
                    FieldFormat(ref temp, "email", (string)reader["CommenterEmail"]);
                    FieldFormat(ref temp, "date", PersianDate((DateTime)reader["CommentDate"]));

                    //---
                    temp = temp.Replace("[CommentID]", reader["id"].ToString());
                    //---
                    CommentContentFormat(ref temp, (string)reader["CommentContent"]);
                    //---
                    this.Response.Write(temp);
                    this.Response.Flush();
                }

                reader.Close();
                reader.Dispose();

                this.Response.Flush();

                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void CommentVerify(bool _IsLogined, Int64 _BlogID, Int64 _PostID)
        {
            if (!_IsLogined)
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringCommentsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CommentVerify_CommentsPage_proc";

            command.Parameters.Add("@CommentID", SqlDbType.BigInt);
            command.Parameters["@CommentID"].Value = Convert.ToInt64(this.Request.Form["CommentID"]);

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.ExecuteNonQuery();
            command.Dispose();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void CommentDelete(bool _IsLogined, Int64 _BlogID, Int64 _PostID)
        {
            if (!_IsLogined)
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringCommentsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CommentDelete_CommentsPage_proc";

            command.Parameters.Add("@CommentID", SqlDbType.BigInt);
            command.Parameters["@CommentID"].Value = Convert.ToInt64(this.Request.Form["CommentID"]);

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.ExecuteNonQuery();
            command.Dispose();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void CommentPreActivation(bool _IsLogined, Int64 _BlogID, Int64 _PostID, bool activate)
        {
            if (!_IsLogined)
            {
                WriteStringToAjaxRequest("Logouted");
                return;
            }

            if (activate)
                this.Session["_IsShowCommentsPreVerify"] = true;
            else
                this.Session["_IsShowCommentsPreVerify"] = false;

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CommentPreActivation_CommentsPage_proc";

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = _PostID;

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = _BlogID;

            command.Parameters.Add("@IsShowCommentsPreVerify", SqlDbType.Bit);
            command.Parameters["@IsShowCommentsPreVerify"].Value = activate;

            command.ExecuteNonQuery();
            command.Dispose();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        protected void DoLogin_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AdminLogin_CommentsPage_proc";

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Value = Convert.ToInt64(this.HiddenBlogID.Value);

            command.Parameters.Add("@PostID", SqlDbType.BigInt);
            command.Parameters["@PostID"].Value = Convert.ToInt64(this.HiddenPostID.Value);

            command.Parameters.Add("@username", SqlDbType.VarChar, 12);
            command.Parameters["@username"].Value = this.Request.Form["username"].Trim().ToLower();

            command.Parameters.Add("@password", SqlDbType.NVarChar, 12);
            command.Parameters["@password"].Value = this.Request.Form["password"];

            command.Parameters.Add("@IsLogined", SqlDbType.Bit);
            command.Parameters["@IsLogined"].Direction = ParameterDirection.Output;

            /*command.Parameters.Add("@IsShowCommentsPreVerify", SqlDbType.Bit);
            command.Parameters["@IsShowCommentsPreVerify"].Direction = ParameterDirection.Output;*/

            command.ExecuteNonQuery();

            if ((bool)command.Parameters["@IsLogined"].Value)
            {
                this.resultText.InnerHtml = "هم اکنون می توانید به ویرایش نظرات این پست بپردازید.";
                this.LoginPanel.Visible = false;
                this.LogoutPanel.Visible = true;
                if ((bool)this.Session["_IsShowCommentsPreVerify"])
                {
                    this.HiddenActivateDisplay.Value = "none";
                    this.HiddenFieldDeactivateDisplay.Value = "block";
                }
                else
                {
                    this.HiddenActivateDisplay.Value = "block";
                    this.HiddenFieldDeactivateDisplay.Value = "none";
                }
                command.Dispose();

                this.Session["_LogindBlogID_CommentingSection"] = Convert.ToInt64(this.HiddenBlogID.Value);
                return;
            }
            else
            {
                this.resultText.InnerHtml = "نام کاربری یا کلمه عبور اشتباه است یا شما مدیر این وبلاگ نیستید.";
                this.LoginPanel.Visible = true;
                command.Dispose();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------
        private static void CommentContentFormat(ref string buffer, string CommentContent)
        {
            CommentContent = CommentContent.Replace("[لبخند]", "<IMG title='لبخند' dir='rtl' src='commenting/smiling.gif'>");
            CommentContent = CommentContent.Replace("[ناراحت]", "<IMG title='ناراحت' dir='rtl' src='commenting/sad.gif'>");
            CommentContent = CommentContent.Replace("[خنده]", "<IMG title='خنده' dir='rtl' src='commenting/laughing.gif'>");
            CommentContent = CommentContent.Replace("[گریه]", "<IMG title='گریه' dir='rtl' src='commenting/crying.gif'>");
            CommentContent = CommentContent.Replace("[چشمک]", "<IMG title='چشمک' dir='rtl' src='commenting/winking.gif'>");
            CommentContent = CommentContent.Replace("[نیشخند]", "<IMG title='نیشخند' dir='rtl' src='commenting/big-grin.gif'>");
            CommentContent = CommentContent.Replace("[زبان]", "<IMG title='زبان' dir='rtl' src='commenting/tongue.gif'>");
            CommentContent = CommentContent.Replace("[خجالت]", "<IMG title='خجالت' dir='rtl' src='commenting/blushing.gif'>");
            CommentContent = CommentContent.Replace("[قلب]", "<IMG title='قلب' dir='rtl' src='commenting/love-struck.gif'>");
            CommentContent = CommentContent.Replace("[ماچ]", "<IMG title='ماچ' dir='rtl' src='commenting/kiss.gif'>");
            CommentContent = CommentContent.Replace("[تعجب]", "<IMG title='تعجب' dir='rtl' src='commenting/surprised.gif'>");
            CommentContent = CommentContent.Replace("[عصبانی]", "<IMG title='عصبانی' dir='rtl' src='commenting/angry.gif'>");
            CommentContent = CommentContent.Replace("[سوال]", "<IMG title='سوال' dir='rtl' src='commenting/confused.gif'>");
            CommentContent = CommentContent.Replace("[عینک]", "<IMG title='عینک' dir='rtl' src='commenting/cool.gif'>");
            CommentContent = CommentContent.Replace("[شیطان]", "<IMG title='شیطان' dir='rtl' src='commenting/devil.gif'>");
            CommentContent = CommentContent.Replace("[سبز]", "<IMG title='سبز' dir='rtl' src='commenting/sick.gif'>");
            CommentContent = CommentContent.Replace("[گل]", "<IMG title='گل' dir='rtl' src='commenting/rose.gif'>");
            CommentContent = CommentContent.Replace("[خداحافظ]", "<IMG title='خداحافظ' dir='rtl' src='commenting/buy.gif'>");
            CommentContent = CommentContent.Replace("[تحسین]", "<IMG title='تحسین' dir='rtl' src='commenting/hand.gif'>");
            CommentContent = CommentContent.Replace("[قاه قاه]", "<IMG title='قاه قاه' dir='rtl' src='commenting/laugh.gif'>");
            buffer = buffer.Replace("[content]", CommentContent);
        }
        //--------------------------------------------------------------------
        private static void FieldFormat(ref string buffer, string fieldName, string fieldValue)
        {
            if (fieldValue == null || fieldValue == "")
            {
                TagDelete(ref buffer, fieldName);
                return;
            }
            else
            {
                buffer = buffer.Replace(String.Format("[{0}]", fieldName), fieldValue);
                return;
            }
        }
        //--------------------------------------------------------------------
        private static void TagDelete(ref string buffer, string tagName)
        {
            string start = String.Format("<{0}>", tagName);
            string end = String.Format("</{0}>", tagName);
            int p1 = buffer.IndexOf(start) + start.Length;
            int p2 = buffer.IndexOf(end);
            if (p1 >= 0 && p2 >= 0)
            {
                p1 -= start.Length;
                p2 += end.Length;
                buffer = buffer.Remove(p1, p2 - p1);
                //buffer = buffer.Replace(buffer.Substring(p1, p2 - p1), "");
            }
            return;
        }
        //--------------------------------------------------------------------
        private static string PersianDate(DateTime dt)
        {
            PersianCalendar pcal = new PersianCalendar();
            string[] PersianMonthNames = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            string[] PersianWeekNames = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            string pdate = PersianWeekNames[Convert.ToInt32(pcal.GetDayOfWeek(dt))];
            pdate += "، " + pcal.GetDayOfMonth(dt).ToString();
            pdate += " " + PersianMonthNames[pcal.GetMonth(dt) - 1];
            pdate += " " + pcal.GetYear(dt);
            pdate += String.Format(" - {0}:{1}", pcal.GetHour(dt), pcal.GetMinute(dt));
            return pdate;
        }
        //--------------------------------------------------------------------
        private bool IsLoginProc(Int64 _CurrentBlogID, Int64 _PostID)
        {
            if (this.Session["_CurrentBlogID"] == null)
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "IsShowCommentsPreVerifyLoad_CommentsPage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = _CurrentBlogID;

                command.Parameters.Add("@PostID", SqlDbType.BigInt);
                command.Parameters["@PostID"].Value = _PostID;

                command.Parameters.Add("@IsShowCommentsPreVerify", SqlDbType.Bit);
                command.Parameters["@IsShowCommentsPreVerify"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                this.Session["_IsShowCommentsPreVerify"] = (bool)command.Parameters["@IsShowCommentsPreVerify"].Value;
                this.Session["_CurrentBlogID"] = _CurrentBlogID;

                command.Dispose();
                goto Continue;
            }

            if (this.Session["_CurrentBlogID"] != null && (Int64)this.Session["_CurrentBlogID"] != _CurrentBlogID)
            {
                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLWeblogsDatabase);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "IsShowCommentsPreVerifyLoad_CommentsPage_proc";

                command.Parameters.Add("@BlogID", SqlDbType.BigInt);
                command.Parameters["@BlogID"].Value = _CurrentBlogID;

                command.Parameters.Add("@PostID", SqlDbType.BigInt);
                command.Parameters["@PostID"].Value = _PostID;

                command.Parameters.Add("@IsShowCommentsPreVerify", SqlDbType.Bit);
                command.Parameters["@IsShowCommentsPreVerify"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                this.Session["_IsShowCommentsPreVerify"] = (bool)command.Parameters["@IsShowCommentsPreVerify"].Value;
                this.Session["_CurrentBlogID"] = _CurrentBlogID;

                command.Dispose();
            }
        Continue:
            if (this.Session["_LogindBlogID_CommentingSection"] == null)
                return false;
            else
            {
                if ((Int64)this.Session["_LogindBlogID_CommentingSection"] == _CurrentBlogID)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        //--------------------------------------------------------------------
    }
}
