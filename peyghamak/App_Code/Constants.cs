/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace Peyghamak
{
	/// <summary>
	/// Summary description for Constants.
	/// </summary>/
	public class Constants
	{
        public const string MobileNumber = "00000000000";;
        public const string BlogDomain = "peyghamak.com";
        public const string DefaultThemeString = "default-bg";
        public const string MainPageUrl = "http://www.peyghamak.com/home.aspx";
        public const string LoginPageUrl = "http://www.peyghamak.com/signin.aspx";
        public const string LogoutPageUrl = "http://www.peyghamak.com/signout.aspx";
		public const string SmtpServer = "localhost";
        public const string DbAddress = "192.168.17.134";//"localhost";
		public const string AdminEmail = "example@example`.com";
        public const string AdminEmailPassword = "0000";

        public const string DnsServerAddress = "localhost";

	    public const string DnsServerIP = "127.0.0.1";
        public const string DbUsername = "sa";
        public const string DbPassword = "xxx";



        public const string DnsServerUsername = null;
        public const string DnsServerPassword = null;

        public const string IISServerUsername = null;
        public const string IIServerPassword = null; 
        //---------User image settings-----------------------------
        /*public const string CurrentUsersImageDirectory = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageDirectory1 = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageServer1 = "img1";
        public const string UsersImageDirectory2 = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageServer2 = "img2";
        public const string UsersImageDirectory3 = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageServer3 = "img3";

        //---------User subdomain settings-------------------------
        public const string CurrentUsersSubdomainServer = "serv1";
        public const string UsersSubdomainServer1 = "serv1";
        public const string UsersSubdomainServerIP1 = "127.0.0.1";
        public const string UsersSubdomainServer2 = "serv2";
        public const string UsersSubdomainServerIP2 = "127.0.0.1";
        public const string UsersSubdomainServer3 = "serv3";
        public const string UsersSubdomainServerIP3 = "127.0.0.1";
        public const string CurrentUsersSubdomainServerIP = "127.0.0.1";
        private const int MaxUserSubdomainNumPerServer = 100000;*/

        public const string CurrentUsersSubdomainServer = "serv1";
        public const string UsersSubdomainServer1 = "serv1";
        public const string UsersSubdomainServerIP1 = DnsServerIP;
        public const string UsersSubdomainServer2 = "serv2";
        public const string UsersSubdomainServerIP2 = DnsServerIP;
        public const string UsersSubdomainServer3 = "serv3";
        public const string UsersSubdomainServerIP3 = DnsServerIP;
        public const string CurrentUsersSubdomainServerIP = DnsServerIP;
        private const int MaxUserSubdomainNumPerServer = 100000;

        public const string CurrentUsersImageDirectory = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageDirectory1 = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageServer1 = "img1";
        public const string UsersImageDirectory2 = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageServer2 = "img2";
        public const string UsersImageDirectory3 = @"D:\Projects\Peyghamak.com\images\";
        public const string UsersImageServer3 = "img3";

        public const string CurrentUsersImageServer = "img1";
        private const int MaxImageNumPerDirectory = 10000;

        public class UserImageManagment
        {
            public struct ImageInfo
            {
                private string _serverName;
                private int _folderPart;
                public ImageInfo(string serverName, int folderPart)
                {
                    this._serverName = serverName;
                    this._folderPart = folderPart;
                }
                public string ServerName
                {
                    get
                    {
                        return this._serverName;
                    }
                }

                public int FolderPart
                {
                    get
                    {
                        return this._folderPart;
                    }
                }
            }
            //========================
            public static string FindPhysicalDirectoryImageServerLocation(string serverName)
            {
                switch (serverName)
                {
                    case UsersImageServer1:
                        return UsersImageDirectory1;
                    case UsersImageServer2:
                        return UsersImageDirectory2;
                    case UsersImageServer3:
                        return UsersImageDirectory3;
                    default: return UsersImageDirectory1;
                }
            }
            //========================
            public static ImageInfo FindImageServerInformation()
            {
                SqlConnection connection = new SqlConnection(Constants.ConnectionStringServersInformationDbName);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT TOP 1 id,ServerName,FolderPart,ImageNum FROM {0} ORDER BY id DESC", Constants.ImageServersInformationTableName);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int id = (int)reader["id"];
                    string _serverName = (string)reader["ServerName"];
                    int _folderPart = (int)reader["FolderPart"];
                    int _imageNum = (int)reader["ImageNum"];
                    reader.Close();
                    if (_serverName == CurrentUsersImageServer) // check the adquate server image resources to insert the new user profile images
                    {
                        if (_imageNum == MaxImageNumPerDirectory) // check the adquate folder part to insert the new user profile images
                        {
                            command.CommandText = string.Format("UPDATE {0} SET FolderPart=FolderPart+1,ImageNum=0 WHERE id={1}", Constants.ImageServersInformationTableName, id);
                            command.ExecuteNonQuery();
                            command.Dispose();
                            connection.Close();
                            Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\full\" + _folderPart);
                            Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\38x38\" + _folderPart);
                            Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\40x40\" + _folderPart);
                            File.Copy(CurrentUsersImageDirectory + @"users\full\default.jpg", CurrentUsersImageDirectory + @"users\full\" + _imageNum + @"\default.jpg");
                            File.Copy(CurrentUsersImageDirectory + @"users\38x38\default.jpg", CurrentUsersImageDirectory + @"users\38x38\" + _imageNum + @"\default.jpg");
                            File.Copy(CurrentUsersImageDirectory + @"users\40x40\default.jpg", CurrentUsersImageDirectory + @"users\40x40\" + _imageNum + @"\default.jpg");
                            return new ImageInfo(_serverName, _folderPart + 1);
                        }
                        else
                        {
                            command.CommandText = string.Format("UPDATE {0} SET ImageNum=ImageNum+1 WHERE id={1}", Constants.ImageServersInformationTableName, id);
                            command.ExecuteNonQuery();
                            command.Dispose();
                            connection.Close(); ;
                            return new ImageInfo(_serverName, _folderPart);
                        }
                    }
                    else
                    {
                        command.CommandText = string.Format("INSERT INTO {0} (ServerName,FolderPart,ImageNum) VALUES('{1}',1,1)", Constants.ImageServersInformationTableName, Constants.CurrentUsersImageServer);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();
                        Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\full\1");
                        Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\38x38\1");
                        Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\40x40\1");
                        File.Copy(CurrentUsersImageDirectory + @"users\full\default.jpg", CurrentUsersImageDirectory + @"users\full\1\default.jpg");
                        File.Copy(CurrentUsersImageDirectory + @"users\38x38\default.jpg", CurrentUsersImageDirectory + @"users\38x38\1\default.jpg");
                        File.Copy(CurrentUsersImageDirectory + @"users\40x40\default.jpg", CurrentUsersImageDirectory + @"users\40x40\1\default.jpg");
                        return new ImageInfo(Constants.CurrentUsersImageServer, 1);
                    }
                }
                else
                {
                    reader.Close();
                    command.CommandText = string.Format("INSERT INTO {0} (ServerName,FolderPart,ImageNum) VALUES('{1}',1,1)", Constants.ImageServersInformationTableName, Constants.CurrentUsersImageServer);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                    Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\full\1");
                    Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\38x38\1");
                    Directory.CreateDirectory(CurrentUsersImageDirectory + @"users\40x40\1");
                    File.Copy(CurrentUsersImageDirectory + @"users\full\default.jpg", CurrentUsersImageDirectory + @"users\full\1\default.jpg");
                    File.Copy(CurrentUsersImageDirectory + @"users\38x38\default.jpg", CurrentUsersImageDirectory + @"users\38x38\1\default.jpg");
                    File.Copy(CurrentUsersImageDirectory + @"users\40x40\default.jpg", CurrentUsersImageDirectory + @"users\40x40\1\default.jpg");
                    return new ImageInfo(Constants.CurrentUsersImageServer, 1);
                }
            }
            //========================
        }


        public class UsersSubdomainManagment
        {
            //========================
            public static string FindSubdomainServerIP(string serverName)
            {
                switch (serverName)
                {
                    case UsersSubdomainServer1:
                        return UsersSubdomainServerIP1;
                    case UsersSubdomainServer2:
                        return UsersSubdomainServerIP2;
                    case UsersSubdomainServer3:
                        return UsersImageDirectory3;
                    default: return UsersSubdomainServerIP1;
                }
            }
            //========================
            /// <summary>
            /// Gets the new subdomain server name that has the adquate resources to define the new subdoamin.
            /// </summary>
            /// <returns></returns>
            public static string FindNewSubdomainServerName()
            {
                SqlConnection connection = new SqlConnection(Constants.ConnectionStringServersInformationDbName);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = string.Format("SELECT TOP 1 id,ServerName,UserNum FROM {0} ORDER BY id DESC", Constants.SubdomainServersInformationTableName);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int id = (int)reader["id"];
                    string _serverName = (string)reader["ServerName"];
                    int _userNum = (int)reader["UserNum"];
                    reader.Close();
                    if (_serverName == CurrentUsersSubdomainServer) // check the adquate server resources to insert the new subdomain
                    {
                        command.CommandText = string.Format("UPDATE {0} SET UserNum=UserNum+1 WHERE id={1}", Constants.SubdomainServersInformationTableName, id);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();
                        return _serverName;
                    }
                    else
                    {
                        command.CommandText = string.Format("INSERT INTO {0} (ServerName,UserNum) VALUES('{1}',1)", Constants.SubdomainServersInformationTableName, Constants.CurrentUsersSubdomainServer);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();
                        return Constants.CurrentUsersSubdomainServer;
                    }
                }
                else
                {
                    reader.Close();
                    command.CommandText = string.Format("INSERT INTO {0} (ServerName,UserNum) VALUES('{1}',1)", Constants.SubdomainServersInformationTableName, Constants.CurrentUsersSubdomainServer);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                    return Constants.CurrentUsersSubdomainServer;
                }
            }
            //========================
        }


        public const int MaxPostShow = 15;//6;
        public const int MaxRssShow = 10;
		public const int PagingNumber = 10;//5
        public const int TopLatestPostsShow = 8;

        public const int MaxAllCommentsShow = 5; // for my.aspx
        public const int PagingNumberAllComments = 5; // for my.aspx
        public const int TopLatestStarredCommentsShow = 5; // for my.aspx
        public const int TopLatestPrivateMessagesShow = 2;

        public const int MaxStringLengthForLatestComments = 25; // for my.aspx
        public const int MaxUserImageUploadSize = 262144; // 256KB

        public const int TopFriendsShow = 20; //for my.aspx and guest.aspx pages
        public const int TopFriendsShowColumnNum = 5; //for my.aspx and guest.aspx pages

        public const int TopFriendsOrFollowersShow = 48; //for friens.aspx and followers.aspx pages
        public const int TopFriendsOrFollowersShowColumnNum = 2;

        public const int MaxUsersShow = 60; //for users.aspx page
        public const int MaxUsersShowBySearchQuery = 1000; //for users.aspx?mode=search page
        public const int MaxUsersColumnNum = 3;



        public const int SessionTimeoutMinutes = 120; // means 120 minutes session timeout
        public const int CookieTimeoutDays = 7;


        public const int TopHomePageCommentsShow = 3;
        public const int TopHomePageStarsShow = 3;
        public const int MaxStringLengthForHomePageComments= 70; // for home.aspx
        public const int MaxStringLengthForHomePageTopStars = 70; // for home.aspx
        public const int MaxStringLengthForLatestPrivateMessages = 70; // for my.aspx
        //---------DB names----------------------------------------
        private const string AccountsDbName = "peyghamak-accounts";
        private const string Posts1DbName = "peyghamak-posts-1";
        private const string Comments1DbName = "peyghamak-comments-1";
        private const string Friends1DbName = "peyghamak-friends-1";
        private const string PrivateMesages1DbName = "peyghamak-private-messages-1";
        private const string StarredPosts1DbName = "peyghamak-StarredPosts-1";
        private const string ServersInformationDbName = "peyghamak-server-information";


        //---------Linked servers----------------------------------
        public const bool IsAccountsDbLinkedServer = false; // ACCOUNTSDB
        public const bool IsPosts1DbLinkedServer = false; // POSTS1DB
        public const bool IsFriends1DbLinkedServer = false; // FRIENDS1DB
        public const bool IsStarredPosts1DbLinkedServer = false; // STARREDPOSTS1DB


        //--------Table names--------------------------------------
        public const string AccountsTableName = "accounts";
        public const string ImageServersInformationTableName = "ImageServersInformation";
        public const string SubdomainServersInformationTableName = "SubdomainServersInformation";
        public const string PostsTableName = "posts";
        public const string InvitationsTableName = "invitations";

        //-------DB connection strings----------------------------
        public const string ConnectionStringAccountsDatabase = "database=" + AccountsDbName + ";server=" + DbAddress +
			";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;";

        public const string ConnectionStringPosts1Database = "database=" + Posts1DbName + ";server=" + DbAddress +
            ";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;";

        public const string ConnectionStringComments1Database = "database=" + Comments1DbName + ";server=" + DbAddress +
           ";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;";

        public const string ConnectionStringFriends1Database = "database=" + Friends1DbName + ";server=" + DbAddress +
           ";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;";

        public const string ConnectionStringPrivateMessages1Database = "database=" + PrivateMesages1DbName + ";server=" + DbAddress +
            ";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;";

        public const string ConnectionStringStarredPosts1Database = "database=" + StarredPosts1DbName + ";server=" + DbAddress +
            ";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;";

        public const string ConnectionStringServersInformationDbName = "database=" + ServersInformationDbName + ";server=" + DbAddress +
          ";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;";

		//public const string ConnectionString = "workstation id=dotgrid;packet size=4096;integrated security=SSPI;data source=dotgrid;persist security info=False;initial catalog=Tebyan-Zn";
        //-------Security keys------------------------------------
		public const string AdminUsername = "test111";
		public const string AdminPassword = "test111";

        public const string RijndaelKey = "MUJejvsCAhuzqWCbxZldWbtVJWDl9ML8h+dFYjIVlcI=";
        public const string RijndaelIV = "ZjPAUxY8q7mw/S+9gslTkQ==";
        //--------------------------------------------------------
	}
}