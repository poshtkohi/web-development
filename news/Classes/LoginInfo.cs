/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;

using news.Classes.Enums;
namespace news.Classes
{
	//---------------------------------------------------------------------------------------------------
	public class LoginInfo
	{

		private AccountType _AccountType = AccountType.Admin;
		private string _username = "";
		private Int64 _UserID = -1;
		//--------------------------------------------------------------------------------
		public LoginInfo()
		{
		}
		//--------------------------------------------------------------------------------
		public AccountType AccountType
		{
			get
			{
				return this._AccountType;
			}
			set
			{
				this._AccountType = value;
			}
		}
		//--------------------------------------------------------------------------------
		public string Username
		{
			get
			{
				return this._username;
			}
			set
			{
				this._username = value;
			}
		}
		//--------------------------------------------------------------------------------
		public Int64 UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this._UserID = value;
			}
		}
		//--------------------------------------------------------------------------------
	}
	//---------------------------------------------------------------------------------------------------
}
