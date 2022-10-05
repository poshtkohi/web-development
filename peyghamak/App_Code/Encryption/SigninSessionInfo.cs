/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Peyghamak
{
    public class SigninSessionInfo
    {
        private string _username = null;
        private Int64 _BlogID = -1;
        private string _name = null;
        private string _ImageGuid = null;
        private string _ThemeString = null;
        //--------------------------------------------------------------------
        public SigninSessionInfo()
        {
        }
        //--------------------------------------------------------------------
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
        //--------------------------------------------------------------------
        public Int64 BlogID
        {
            get
            {
                return this._BlogID;
            }
            set
            {
                this._BlogID = value;
            }
        }
        //--------------------------------------------------------------------
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        //--------------------------------------------------------------------
        public string ImageGuid
        {
            get
            {
                return this._ImageGuid;
            }
            set
            {
                this._ImageGuid = value;
            }
        }
        //--------------------------------------------------------------------
        public string ThemeString
        {
            get
            {
                return this._ThemeString;
            }
            set
            {
                this._ThemeString = value;
            }
        }
        //--------------------------------------------------------------------
    }
}
