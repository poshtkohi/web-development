/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;

//using DotGrid.DotSec;
using System.Configuration;
using System.Web.Configuration;

namespace bookstore
{
    public class constants
    {
        public const string EmailSmtpAddress = "example.com";
        public const string Email = "example@example.com";
        public const string EmailUsername = "bookstore";
        public const string EmailPassword = "000";

        public const int EachBookPrice = 30000;


        public const string UploadedImagesPath = @"/images/uploads/";
        public const int MaxUploadedImagesSize = 256 * 1024;

        public const int RandomNumbers = 10;


        public const string SwishePath = @"\search-pack\swishe.exe";
        public const string SwisheIndexPath = @"\search-pack\all2.idx";
        public const int SwisheMaxReturn = 1000;
        public const bool SwisheIsLocal = false;
        public const string SwisheRemoteURL = "http://000";

        public const int SessionTimeoutMinutes = 120; // means 120 minutes session timeout
        public const int CookieTimeoutDays = 2;

        public const int MaxNewsAdminShow = 20;
        public const int NewsAdminPagingNumber = 10;

        public const int MaxShowNewsHomePage = 4;
        public const int ShowNewsHomePagePagingNumber = 5;

        public const int MaxCategoryAdminShow = 20;
        public const int CategoryAdminPagingNumber = 10;

        public const int MaxBooksAdminShow = 20;
        public const int BooksAdminPagingNumber = 10;

        public const int MaxShowAllBooks = 9;
        public const int ShowAllBooksPagingNumber = 10;

        public const int MaxCustomersAdminShow = 20;
        public const int CustomersAdminPagingNumber = 10;

        public const int MaxShowMainPageCustomers = 6;
        public const int ShowMainPageCustomersPagingNumber = 5;

        public const int MaxPageAdminShow = 20;
        public const int PageAdminPagingNumber = 10;

        public const int MaxShowMainPageSitePages = 20;
        public const int ShowMainPageSitePagesPagingNumber = 10;

        public const int MaxShowByBookSearch = 10;
        public const int ShowByBookSearchPagingNumber = 10;

        public const int MaxShowShoppingCart = 20;
        public const int ShowShoppingCartPagingNumber = 20;

        public const int MaxPurchaseAdminShow = 20;
        public const int PurchaseAdminPagingNumber = 10;

        public const int MaxUserAdminShow = 20;
        public const int UserAdminPagingNumber = 10;

        public const int MaxTransactionsAdminShow = 20;
        public const int TransactionsAdminPagingNumber = 10;

        public const bool CacheTemplateEnbaled = false;

        public const string BookImagesURLPath = "http://000";

        public const string boxing1 = "bTD";
        public const string boxing2 = "bTDA";

        public static string[] AdminPages = new string[] { "BooksAdmin.aspx", "CategoryAdmin.aspx", "cp.aspx", "NewsAdmin.aspx", "CustomersAdmin.aspx", "PagesAdmin.aspx", "BookFileUpload.aspx", "BookImageUpload.aspx", "NewsAdmin.aspx", "TopPostAdmin.aspx", "PurchaseDetails.aspx", "PurchasesAdmin.aspx", "UsersAdmin.aspx", "UserDetails.aspx", "NewsImage.aspx", "TransactionsAdmin.aspx", "PassAdmin.aspx" };
        public const string AdminUsername = "admin";
        public const string AdminPassword = "000";


        private const string SQLUsername = "user";
        private const string SQLPassowrd = "path";
        private const string SQLBookStoreDatabaseName = "db";
        public const string SQLServerAddress = "localhost";


        
        public static string ConnectionStringSQLDatabase = "database=" + SQLBookStoreDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + SQLUsername + ";Password=" + SQLPassowrd + ";Connect Timeout=30;";


        public const string key = "MUJejvsCAhuzqWCbxZldWbtVJWDl9ML8h+dFYjIVlcI=";
        public const string IV = "ZjPAUxY8q7mw/S+9gslTkQ==";
        public const string password = "pYt%#0lq!lku89$*l;prtuopploflkj";
        public const string cryptedPassword = "iKn5325Hmf+66QJkO9650YIzRvr4Nx1X2n4Rj87NSdQ=";

        //----------------------------------
        public const string MarchantID = "0000";
        public const string RedirectUrl = "http://bookstore/cp/GetBack.aspx";
        public const string MarchantPassword = "0000";
        //----------------------------------
    }
}