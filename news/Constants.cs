/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;

namespace news
{
	public class Constants
	{

		public const string DbUsername = "root";
		public const string DbPassword = "000";


		public const int MaxUsersShow = 20;
		public const int UsersPagingNumber = 20;

		public const string boxing1 = "18";
		public const string boxing2 = "24";

		public static string[] AccessibleAdminPages = new string[]{"Users.aspx".ToLower(),"NewsGroups.aspx".ToLower()};
		public const string NoneAdminAccessPageForward = "/NewsAdmin.aspx";
		//---------DB names----------------------------------------
		private const string NewsDbName = "news";


		//-------DB connection strings----------------------------
		public const string ConnectionStringNewsDatabase = "database=" + NewsDbName + ";server=" + DbAddress +
			";User Id=" + DbUsername + ";Password=" + DbPassword + ";Connect Timeout=30;charset=utf8";

		//-------Security keys------------------------------------
		public const string AdminUsername = "admin";
		public const string AdminPassword = "admin";

		public const string RijndaelKey = "MUJejvsCAhuzqWCbxZldWbtVJWDl9ML8h+dFYjIVlcI=";
		public const string RijndaelIV = "ZjPAUxY8q7mw/S+9gslTkQ==";
		//--------------------------------------------------------
	}
}
