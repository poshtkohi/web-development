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

using System.Data.SqlClient;

namespace Peyghamak.messenger
{
    public partial class yahooIn : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            string from = "none";
            string to = "none";
            string message = "none";

            if (this.Request.QueryString["from"] != null)
                from = this.Request.QueryString["from"];
            else
                if (this.Request.Form["from"] != null)
                    from = this.Request.Form["from"];

            if (this.Request.QueryString["to"] != null)
                to = this.Request.QueryString["to"];
            else
                if (this.Request.Form["to"] != null)
                    to = this.Request.Form["to"];

            if (this.Request.QueryString["message"] != null)
                message = this.Request.QueryString["message"];
            else
                if (this.Request.Form["message"] != null)
                    message = this.Request.Form["message"];

            if (from != "none")
            {
                from = from.Trim();
                Int64 _BlogID = -1;
                bool _IsExisted = false;
                bool _IsVerified = false;

                CheckExistingYahooIdOrVerifyYahooId(from, message, ref _IsExisted, ref _IsVerified, ref _BlogID);
                if (!_IsExisted && _IsVerified) //YahooId verification request
                {
                    //this.Response.Write("verify");
                    this.Response.End();
                    return;
                }
                if (_IsExisted && _IsVerified) //New post request 
                {
                    DoPost(message, _BlogID);
                    //this.Response.Write("dopost");
                    this.Response.End();
                    return;
                }
            }
            else
            {
                //this.Response.Write("none");
                this.Response.End();
                return;
            }
        }
        //--------------------------------------------------------------------
        private void CheckExistingYahooIdOrVerifyYahooId(string from, string message, ref bool _IsExisted, ref bool _IsVerified, ref Int64 _BlogID)
        {
            SqlConnection connection = new SqlConnection(Constants.ConnectionStringAccountsDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            ////command.CommandText = "VerifyMobileNumber_MobilePage_proc";
            command.CommandText = "VerifyYahooId_MessengerPage_proc";

            command.Parameters.Add("@YahooId", SqlDbType.NVarChar, 20);
            command.Parameters["@YahooId"].Direction = ParameterDirection.Input;
            command.Parameters["@YahooId"].Value = from;

            command.Parameters.Add("@VerificationCode", SqlDbType.VarChar, 10);
            command.Parameters["@VerificationCode"].Direction = ParameterDirection.Input;

            if (message.Length > command.Parameters["@VerificationCode"].Size)
                command.Parameters["@VerificationCode"].Value = message.Substring(0, command.Parameters["@VerificationCode"].Size).Trim().ToLower();
            else
                command.Parameters["@VerificationCode"].Value = message.Trim().ToLower();

            command.Parameters.Add("@BlogID", SqlDbType.BigInt);
            command.Parameters["@BlogID"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@IsExisted", SqlDbType.Bit);
            command.Parameters["@IsExisted"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@IsVerified", SqlDbType.Bit);
            command.Parameters["@IsVerified"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            _IsExisted = (bool)command.Parameters["@IsExisted"].Value;
            _IsVerified = (bool)command.Parameters["@IsVerified"].Value;

            if ((_IsExisted && _IsVerified) || (!_IsExisted && _IsVerified))
                _BlogID = (Int64)command.Parameters["@BlogID"].Value;

            command.Dispose();
            connection.Close();
            return;
        }
        //--------------------------------------------------------------------
        private void DoPost(string message, Int64 _BlogID)
        {
            message = message.Trim();
            byte _LanguageType = (byte)LanguageType.English;
            bool _PostAlign = Convert.ToBoolean(PostAlign.Left);

            if (message.Length > 280)
                message = message.Substring(0, 280);

            /*message = "آ";
            this.Response.Write((int)message[0]);
            return;*/
            /*   ی , ه حروف الفبا. همه, آ, ا, ب, پ, ت, ث, ج, چ, ح, خ, د, ذ, ر, ز, ژ, س, ش, ص, ض, ط, ظ, ع, غ, ف, ق, ک, گ, ل, م, ن, و, ه*/

            int len = message.Length;
            int i, c;
            for (i = 0; i < len; i++)
            {
                c = (int)message[i];
                if ((int)('ی') >= c && c >= (int)('آ'))
                {
                    _LanguageType = (byte)LanguageType.Persian;
                    _PostAlign = Convert.ToBoolean(PostAlign.Right);
                    break;
                }
            }

            SqlConnection connection = new SqlConnection(Constants.ConnectionStringPosts1Database);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DoPost_MyPage_proc";

            SqlParameter BlogIDParam = new SqlParameter("@BlogID", SqlDbType.BigInt);
            SqlParameter PostDateParam = new SqlParameter("@PostDate", SqlDbType.DateTime);
            SqlParameter PostTypeParam = new SqlParameter("@PostType", SqlDbType.Char);
            SqlParameter PostLanguageParam = new SqlParameter("@PostLanguage", SqlDbType.Char);
            SqlParameter PostAlignParam = new SqlParameter("@PostAlign", SqlDbType.Bit);
            SqlParameter PostContentParam = new SqlParameter("@PostContent", SqlDbType.NVarChar);
            SqlParameter CommentEnabledParam = new SqlParameter("@CommentEnabled", SqlDbType.Bit);
            SqlParameter HasPictureParam = new SqlParameter("@HasPicture", SqlDbType.Bit);
            //------Linked Server settings---------------
            SqlParameter IsAccountsDbLinkedServerParam = new SqlParameter("@IsAccountsDbLinkedServer", SqlDbType.Bit);
            IsAccountsDbLinkedServerParam.Value = Constants.IsAccountsDbLinkedServer;
            command.Parameters.Add(IsAccountsDbLinkedServerParam);
            //-------------------------------------------

            BlogIDParam.Value = _BlogID;
            PostDateParam.Value = DateTime.Now;
            PostTypeParam.Value = (byte)PostType.FromMessenger;
            PostLanguageParam.Value = _LanguageType;
            PostAlignParam.Value = _PostAlign;
            PostContentParam.Value = Context.Server.HtmlEncode(message);
            CommentEnabledParam.Value = true;
            HasPictureParam.Value = false;

            command.Parameters.Add(BlogIDParam);
            command.Parameters.Add(PostDateParam);
            command.Parameters.Add(PostTypeParam);
            command.Parameters.Add(PostLanguageParam);
            command.Parameters.Add(PostAlignParam);
            command.Parameters.Add(PostContentParam);
            command.Parameters.Add(CommentEnabledParam);
            command.Parameters.Add(HasPictureParam);

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            return;
        }
        //--------------------------------------------------------------------
    }
}
