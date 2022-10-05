/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for CheckExisitingPostTransferSessionInfo
/// </summary>
namespace services.blogbuilderv1.PostImporterClasses
{
    public class CheckExisitingPostTransferSessionInfo
    {
        private bool _IsExisted = false;
        private string _domain;
        private int _TotalPosts = 0;
        private int _CurrentFetchedPost = 0;
        private DateTime _LastFetchedPostDate;
        private BlogType _BlogType;
        private Int64 _CategoryID = 0;
        private string _PostImporterTag;
        private string _PostTag;
        private string _PostTitleTag;
        private string _PostContentTag;
        private string _DirectLinkTag; 
        private string _ContinuedPostTag;
        private bool _IsFirstTimeStarted = false;
        public CheckExisitingPostTransferSessionInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //--------------------------------------------
        public bool IsExisted
        {
            get
            {
                return this._IsExisted;
            }
            set
            {
                this._IsExisted = value;
            }
        }
        //--------------------------------------------
        public string Domain
        {
            get
            {
                return this._domain;
            }
            set
            {
                this._domain = value;
            }
        }
        //--------------------------------------------
        public int TotalPosts
        {
            get
            {
                return this._TotalPosts;
            }
            set
            {
                this._TotalPosts = value;
            }
        }
        //--------------------------------------------
        public int CurrentFetchedPost
        {
            get
            {
                return this._CurrentFetchedPost;
            }
            set
            {
                this._CurrentFetchedPost = value;
            }
        }
        //--------------------------------------------
        public DateTime LastFetchedPostDate
        {
            get
            {
                return this._LastFetchedPostDate;
            }
            set
            {
                this._LastFetchedPostDate = value;
            }
        }
        //--------------------------------------------
        public BlogType BlogType
        {
            get
            {
                return this._BlogType;
            }
            set
            {
                this._BlogType = value;
            }
        }
        //--------------------------------------------
        public Int64 CategoryID
        {
            get
            {
                return this._CategoryID;
            }
            set
            {
                this._CategoryID = value;
            }
        }
        //--------------------------------------------
        public string PostImporterTag
        {
            get
            {
                return this._PostImporterTag;
            }
            set
            {
                this._PostImporterTag = value;
            }
        }
        //--------------------------------------------
        public string PostTag
        {
            get
            {
                return this._PostTag;
            }
            set
            {
                this._PostTag = value;
            }
        }
        //--------------------------------------------
        public string PostTitleTag
        {
            get
            {
                return this._PostTitleTag;
            }
            set
            {
                this._PostTitleTag = value;
            }
        }
        //--------------------------------------------
        public string PostContentTag
        {
            get
            {
                return this._PostContentTag;
            }
            set
            {
                this._PostContentTag = value;
            }
        }
        //--------------------------------------------
        public string DirectLinkTag
        {
            get
            {
                return this._DirectLinkTag;
            }
            set
            {
                this._DirectLinkTag = value;
            }
        }
        //--------------------------------------------
        public string ContinuedPostTag
        {
            get
            {
                return this._ContinuedPostTag;
            }
            set
            {
                this._ContinuedPostTag = value;
            }
        }
        //--------------------------------------------
        public bool IsFirstTimeStarted
        {
            get
            {
                return this._IsFirstTimeStarted;
            }
            set
            {
                this._IsFirstTimeStarted = value;
            }
        }
        //--------------------------------------------
    }
}
