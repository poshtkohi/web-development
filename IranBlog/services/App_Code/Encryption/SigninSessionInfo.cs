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

using services.blogbuilderv1;

namespace IranBlog.Classes.Security
{
    public class SigninSessionInfo
    {
        private string _username = null;
        private string _subdomain = null;
        private Int64 _BlogID = -1;
        private Int64 _AuthorID = -1;
        private TeamWeblogAccessInfo _TeamWeblogAccessInfo = new TeamWeblogAccessInfo();
        private bool _IsInTeamWeblogMode = false;
        private bool _ChatBoxIsEnabled = true;
        private string _ImageGuid = null;
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
        public string Subdomain
        {
            get
            {
                return this._subdomain;
            }
            set
            {
                this._subdomain = value;
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
        public Int64 AuthorID
        {
            get
            {
                return this._AuthorID;
            }
            set
            {
                this._AuthorID = value;
            }
        }
        //--------------------------------------------------------------------
        public TeamWeblogAccessInfo TeamWeblogAccessInfo
        {
            get
            {
                return this._TeamWeblogAccessInfo;
            }
            set
            {
                this._TeamWeblogAccessInfo = value;
            }
        }
        //--------------------------------------------------------------------
        public bool IsInTeamWeblogMode
        {
            get
            {
                return this._IsInTeamWeblogMode;
            }
            set
            {
                this._IsInTeamWeblogMode = value;
            }
        }
        //--------------------------------------------------------------------
        public bool ChatBoxIsEnabled
        {
            get
            {
                return this._ChatBoxIsEnabled;
            }
            set
            {
                this._ChatBoxIsEnabled = value;
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
    }
}
