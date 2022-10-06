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
using System.IO;
using bookstore;
using bookstore.Enums;


namespace bookstore.cp
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        //--------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.IsLoginProc(this))
            {
                this.Response.Redirect("/signin.aspx?i=logouted", true);
                return;
            }
            bool _SiteLanguageIsPersian = (bool)this.Session["SiteLanguageIsPersian"];
            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "ShowShoppingCart":
                        ShowShoppingCart(_SiteLanguageIsPersian);
                        return;
                    case "delete":
                        BookDeleteFromShoppingCart();
                        return;
                    case "DoPurchase":
                        DoPurchase(_SiteLanguageIsPersian);
                        return;
                    default:
                        return;
                }
            }
            PageSettings();
        }
        //--------------------------------------------------------------------------------
        private void DoPurchase(bool _SiteLanguageIsPersian)
        {
            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DoPurchase_ShoppingCartPage_proc";

            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];

            command.Parameters.Add("@EachBookPrice", SqlDbType.Int);
            command.Parameters["@EachBookPrice"].Value = constants.EachBookPrice;

            string _guid = Guid.NewGuid().ToString().Replace("-", "");
            command.Parameters.Add("@PursuitCode", SqlDbType.VarChar);
            command.Parameters["@PursuitCode"].Value = _guid;

            command.Parameters.Add("@HaveItemsToPurchase", SqlDbType.Bit);
            command.Parameters["@HaveItemsToPurchase"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@HaveEnoughCredits", SqlDbType.Bit);
            command.Parameters["@HaveEnoughCredits"].Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            if (!(bool)command.Parameters["@HaveItemsToPurchase"].Value)
            {
                Common.AjaxMultiLanguageReport("سبد خرید شما خالی است", "Your shopping cart is empty.", this, _SiteLanguageIsPersian);
                goto End;
            }
            if (!(bool)command.Parameters["@HaveEnoughCredits"].Value)
            {
                Common.AjaxMultiLanguageReport("اعتبار شما برای انجام این تراکنش کافی نیست. اعتبار خود را افزایش دهید", "Your credits are not enough to do this transaction. Please increase your credits.", this, _SiteLanguageIsPersian);
                goto End;
            }
            else
            {
                Common.AjaxMultiLanguageReport(String.Format("اقلام خریداری شده شما حداکثر تا 72 ساعت آینده ارسال خواهند شد. کد زیر را برای پیگیری خرید خود یادداشت کنید:<br>{0}", _guid),
                    String.Format("Your purchased items will be sent to your address up to next 72 hours. Please write the following code for pursuit of your purchase in future:<br>{0}", _guid), 
                    this, 
                    _SiteLanguageIsPersian);
                goto End;
            }

        End:
            command.Dispose();
            connection.Close();
            return;
        }
        //--------------------------------------------------------------------------------
        private void BookDeleteFromShoppingCart()
        {
            Int64 _DeleteID = -1;
            try
            {
                _DeleteID = Convert.ToInt64(this.Request.Form["DeleteID"]);
            }
            catch
            {
                WriteStringToAjaxRequest(".(DeleteID) خطا در درخواست ورودی");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DeleteShoppingCart_ShoppingCartPage_proc";

            command.Parameters.Add("@ShopingCartID", SqlDbType.BigInt);
            command.Parameters["@ShopingCartID"].Value = _DeleteID;

            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            WriteStringToAjaxRequest("Success");
            return;
        }
        //--------------------------------------------------------------------
        private void ShowShoppingCart(bool _SiteLanguageIsPersian)
        {
            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            if (currentPage > 1 && this.Session["_ItemNumShowShoppingCart"] == null)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowShoppingCart_ShoppingCartPage_proc";
            //-------------------------------------------

            command.Parameters.Add("@PageSize", SqlDbType.Int);
            command.Parameters["@PageSize"].Value = constants.MaxShowShoppingCart;

            command.Parameters.Add("@PageNumber", SqlDbType.Int);
            command.Parameters["@PageNumber"].Value = currentPage;

            command.Parameters.Add("@ShoppingCartNum", SqlDbType.Int);
            command.Parameters["@ShoppingCartNum"].Direction = ParameterDirection.Output;

            command.Parameters.Add("@UserID", SqlDbType.BigInt);
            command.Parameters["@UserID"].Value = (Int64)this.Session["UserID"];



            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string template = "";
                if (constants.CacheTemplateEnbaled)
                {
                    if (this.Cache["_template_ShowShoppingCart"] == null)
                    {
                        StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowShoppingCartTemplate.html");
                        template = sr.ReadToEnd();
                        this.Cache["_template_ShowShoppingCart"] = template;
                        sr.Close();
                    }
                    else
                        template = (string)this.Cache["_template_ShowShoppingCart"];
                }
                else
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ShowShoppingCartTemplate.html");
                    template = sr.ReadToEnd();
                    sr.Close();
                }
                //--Persian or English Language Chane
                //template = template.Replace("dir", "rtl");
                if (_SiteLanguageIsPersian)
                {
                    Common.TagDelete(ref template, "english");
                    template = template.Replace("[dir]", "rtl");
                    template = template.Replace("[align]", "right");
                }
                else
                {
                    Common.TagDelete(ref template, "persian");
                    template = template.Replace("[dir]", "ltr");
                    template = template.Replace("[align]", "left");
                }
                int _p1Post = template.IndexOf("<post>") + "<post>".Length;
                int _p2Post = template.IndexOf("</post>");
                int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
                int _p2Paging = template.IndexOf("</paging>");
                if (_p1Post <= 0 || _p2Post <= 0)
                {
                    this.Response.Write(template);
                    this.Response.OutputStream.Flush();
                    return;
                }
                this.Response.Write(template.Substring(0, _p1Post - "<post>".Length));
                this.Response.Flush();
                string _mainFormat = template.Substring(_p1Post, _p2Post - _p1Post);

                string temp = null;
                bool boxing = true;
                int _i = 0;
                while (reader.Read())
                {
                    temp = _mainFormat;

                    if (boxing)
                    {
                        temp = temp.Replace("[boxing]", constants.boxing1);
                        boxing = false;
                    }
                    else
                    {
                        temp = temp.Replace("[boxing]", constants.boxing2);
                        boxing = true;
                    }
                    temp = temp.Replace("[ShopingCartID]", reader["ShopingCartID"].ToString());
                    temp = temp.Replace("[BookTitle]", reader["BookTitle"].ToString());
                    temp = temp.Replace("[BookISBN]", reader["BookISBN"].ToString());
                    temp = temp.Replace("[i]", _i.ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                    _i++;
                }

                reader.Close();

                if (currentPage == 1)
                    this.Session["_ItemNumShowShoppingCart"] = (int)command.Parameters["@ShoppingCartNum"].Value;

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowShoppingCart", currentPage, constants.MaxShowShoppingCart, (int)this.Session["_ItemNumShowShoppingCart"], constants.ShowShoppingCartPagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                connection.Close();
                command.Dispose();
                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                reader.Close();
                connection.Close();
                command.Dispose();

                return;
            }
        }
        //--------------------------------------------------------------------
        private void PageSettings()
        {
            UserMenuControlLoad();
            LoginControlLoad();
        }
        //--------------------------------------------------------------------
        private void UserMenuControlLoad()
        {
            this.UserMenuControl.Controls.Add(LoadControl("UserMenuControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void LoginControlLoad()
        {
            this.LoginControl.Controls.Add(LoadControl("LoginControl.ascx"));
            return;
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
    }
}
