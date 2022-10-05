using System;

using DotGrid.DotSec;
using System.Configuration;
using System.Web.Configuration;

namespace AlirezaPoshtkoohiLibrary
{
	public class constants
	{
		//----------------------------------
		public const string ServerManagerAddress = "127.0.0.1";
		public const int ServerManagerPort = 8000;
		//----------------------------------
		public const string WeblogUrl = "http://www.iranblog.com/services/";
		public const string CommentsUrlPage = "http://www.iranblog.com/comments/";
		public const string LoginPageUrl = "http://www.iranblog.com/services/login.aspx";
		public const string AdSensePageUrl = "http://news.iranblog.com/";
		public const string IranBlogEmailManager = "info@iranblog.com";
		public const string DomainBlog = "iranblog.com";
		public const string SQLServerAddress = "192.168.17.134";//"localhost";
		public const string DNSServerAddress = "localhost";
		public const string IISServer_IP = "127.0.0.1";
		public const string ZoneName = "iranblog.com";

        public const string IranBlogWelcomeEmailSmtpAddress = "mail.iranblog.com";
        public const string IranBlogWelcomeEmail = "Welcome to IranBlog<info@iranblog.com>";
        public const string IranBlogWelcomeEmailUsername = "info@iranblog.com";
        public const string IranBlogWelcomeEmailPassword = "tttt";

        public const int SessionTimeoutMinutes = 120; // means 120 minutes session timeout
        public const int CookieTimeoutDays = 2;

        public const int MaxPostAdminShow = 20;
        public const int PostAdminPagingNumber = 10;

        public const int MaxShowAjaxLinks = 20;
        public const int ShowAjaxLinksPagingNumber = 10;

        public const int MaxShowPostArchive = 20;
        public const int ShowPostArchivePagingNumber = 10;

        public const int MaxShowAjaxTemplates = 9;
        public const int ShowAjaxTemplatesPagingNumber = 10;

        public const string boxing1 = "bTD";
        public const string boxing2 = "bTDA";


		/*public const string SQLUsername = "sa";
		public const string SQLPassowrd = "241433409870*m2000";
		public const string SQLWeblogsDbUsername = "sa";
		public const string SQLWeblogsDbPassowrd = "241433409870*m2000";*/

		public const string SQLUsername = "sa";
		//public const string SQLPassowrd = "salamazizam";
		public const string SQLWeblogsDbUsername = "sa";
		//public const string SQLWeblogsDbPassowrd = "salamazizam";
        public static string SQLPassowrd
        {
            get
            {
                if (System.Web.HttpRuntime.Cache["_db_p"] == null)
                {
                    RijndaelEncryption _rijndael = new RijndaelEncryption(RijndaelEncryption.Base64StringToBinary(constants.key), RijndaelEncryption.Base64StringToBinary(constants.IV));
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyAppRoot");
                    AppSettingsSection _appSettings = (AppSettingsSection)config.GetSection("appSettings");
                    System.Web.HttpRuntime.Cache.Insert("_db_p", RijndaelEncryption.BinaryToString(_rijndael.decrypt(RijndaelEncryption.Base64StringToBinary(_appSettings.Settings["db_p"].Value))));
                }
                return (string)System.Web.HttpRuntime.Cache["_db_p"];
            }
        }
        public static string SQLWeblogsDbPassowrd
        {
            get
            {
                return SQLPassowrd;
            }
        }

        public static string DnsDomainIPBlog
        {
            get
            {
                if (System.Web.HttpRuntime.Cache["_ip"] == null)
                {
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyAppRoot");
                    AppSettingsSection _appSettings = (AppSettingsSection)config.GetSection("appSettings");
                    System.Web.HttpRuntime.Cache.Insert("_ip", _appSettings.Settings["_ip"].Value);
                }
                return (string)System.Web.HttpRuntime.Cache["_ip"];
            }
        }

        public const string SQLDatabaseName = "general";
        public const string SQLWeblogsDbName = "weblogs";
        public const string SQLNewsletterDatabaseName = "iranblog-newsletter";
        public const string SQLCommentsDatabaseName = "iranblog-comments";
        public const string SQLChatBoxDatabaseName = "iranblog-chatbox";
        public const string SQLPostImporterDatabaseName = "iranblog-postimporter";
        public const string SQLPagesDatabaseName = "iranblog-pages";

		public const string SQLUsersInformationTableName = "usersInfo";
		public const string SQLLinksTableName = "links";
		public const string SQLLinkssTableName = "linkss";
		public const string SQLSubjectedArchiveTableName = "SubjectedArchive";
		public const string SQLPollResponsesTableName = "PollResponses";
		public const string SQLPollQuestionsTableName = "PollQuestions";
		public const string SQLLinkBoxTableName = "linkbox";
        public const string SQLTeamWeblogName = "TeamWeblog";
        public const string SQLDomainsTableName = "domains";
        public const string SQLNewsletterTableName = "newsletter";

		public const string SQLPostsTableName = "posts";
		public const string SQLCommentsTableName = "comments";
		public const string SQLLatestUpdatedWeblogsTableName = "updated";

		public static string ConnectionStringSQLDatabase = "database=" +  SQLDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + constants.SQLUsername + ";Password=" + constants.SQLPassowrd + ";Connect Timeout=30;";

		public static string ConnectionStringSQLWeblogsDatabase = "database=" +  SQLWeblogsDbName + ";server=" + SQLServerAddress +
            ";User Id=" + SQLWeblogsDbUsername + ";Password=" + constants.SQLWeblogsDbPassowrd + ";Connect Timeout=30;";

        public static string ConnectionStringSQLNewsletterDatabase = "database=" + SQLNewsletterDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + constants.SQLUsername + ";Password=" + constants.SQLPassowrd + ";Connect Timeout=30;";

        public static string ConnectionStringCommentsDatabase = "database=" + SQLCommentsDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + constants.SQLUsername + ";Password=" + constants.SQLPassowrd + ";Connect Timeout=30;";

        public static string ConnectionStringChatBoxDatabase = "database=" + SQLChatBoxDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + constants.SQLUsername + ";Password=" + constants.SQLPassowrd + ";Connect Timeout=30;";

        public static string ConnectionStringPostImporterDatabase = "database=" + SQLPostImporterDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + constants.SQLUsername + ";Password=" + constants.SQLPassowrd + ";Connect Timeout=30;";

        public static string ConnectionStringPagesDatabase = "database=" + SQLPagesDatabaseName + ";server=" + SQLServerAddress +
            ";User Id=" + constants.SQLUsername + ";Password=" + constants.SQLPassowrd + ";Connect Timeout=30;";

		public const int SQLMaxUpdatedWeblogsShow = 20;
        public const int SQLMaxUpdatedWeblogsTotal = 100;

        public const int MaxUserImageUploadSize = 262144; // 256KB

        public const int AllowedNewsletterMemberNum = 1000;
        public const int AllowedDomainNum = 10;

        public const int MaxChatBoxMessageShow = 10;
        public const int ChatBoxPagingNumber = 5;

        public const string RootDircetoryWeblogs = @"D:\Projects\IranBlog\weblogs";
        public const string TemplatesPath = @"D:\Projects\IranBlog\services\blogbuilderv1\templates\";
        public const string LinkBoxTemplatePath = @"D:\Projects\IranBlog\services\linkbox\";
        public const string UserProfileImagesPath = @"D:\Projects\IranBlog\users\images\";
        public const string RootPath = @"D:\Projects\IranBlog\";

        /*public const string RootDircetoryWeblogs = @"C:\Inetpub\vhost6s\iranblog2.com\private\weblogs";
		public const string TemplatesPath = @"C:\Inetpub\vhosts\iranblog.com\httpdocs\services\blogbuilderv1\templates\";
		public const string LinkBoxTemplatePath = @"C:\Inetpub\vhosts\iranblog.com\httpdocs\services\linkbox\";
        public const string UserProfileImagesPath = @"C:\Inetpub\vhosts\iranblog.com\httpdocs\users\images\";
        public const string RootPath = @"C:\Inetpub\vhosts\iranblog.com\httpdocs\";*/


        public const string key = "MUJejvsCAhuzqWCbxZldWbtVJWDl9ML8h+dFYjIVlcI=";
		public const string IV = "ZjPAUxY8q7mw/S+9gslTkQ==";
		public const string password = "pYt%#0lq!lku89$*l;prtuopploflkj";
		public const string cryptedPassword = "iKn5325Hmf+66QJkO9650YIzRvr4Nx1X2n4Rj87NSdQ=";
	   //----------------------------------
	}
}