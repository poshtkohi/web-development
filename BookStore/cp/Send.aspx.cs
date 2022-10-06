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


namespace bookstore.cp
{

    public partial class Send : System.Web.UI.Page
    {

        public string TotalAmount = string.Empty;
        public string ReservationNumber = string.Empty;
        public string MerchantID = string.Empty;
        public string RedirectURL = string.Empty;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.IsLoginProc(this))
            {
                this.Response.Redirect("/?i=logouted", true);
                return;
            }
            PageSettings();
            /*
            * =======================<< When you read  info from Session  >>===============================
            GetParametersFromSession();
        
            * =======================<<  When you read  info from Request  >>================================= 
           GetParametersFromRequest();
         
             */

            // Just as an Example
            /*TotalAmount = "1";
            ReservationNumber = "3";
            MerchantID = "00227331-24207";
            RedirectURL = "http://localhost:1284/WebSite1/GetBack.aspx";*/

            /*TotalAmount = "1";
            ReservationNumber = "3";
            MerchantID = "00243297-39570";
            RedirectURL = "http://www.bookstore/GetBack.aspx";*/

            // registration of information in the DB
            // registerPurchase(...)

            GetParametersFromSession();
        }

        // Get info from Session
        private void GetParametersFromSession()
        {
            TotalAmount = Session["TotalAmount"].ToString();
            ReservationNumber = Session["reservationcode"].ToString();
            MerchantID = Session["merchantid"].ToString();
            RedirectURL = Session["redirecturl"].ToString();
        }

        // Get info from Request
        private void GetParametersFromRequest()
        {
            TotalAmount = Request["TotalAmount"].ToString();
            ReservationNumber = Request["reservationcode"].ToString();
            MerchantID = Request["merchantid"].ToString();
            RedirectURL = Request["redirecturl"].ToString();
        }


    }
}