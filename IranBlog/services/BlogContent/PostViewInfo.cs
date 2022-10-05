/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.Text;

namespace AlirezaPoshtkoohiLibrary
{
    //----------------------------------------------------------------------------------------------------------------
    public class PostViewInfo
    {
        /*private struct ItemInfo
        {
            private Int64 _id;
            private string _name;
            public ItemInfo(Int64 id, string name)
            {
                this._id = id;
                this._name = name;
            }
            public Int64 Id
            {
                get
                {
                    return this._id;
                }
            }
            public string Name
            {
                get
                {
                    return this._name;
                }
            }
        }
        private ArrayList authors;
        private ArrayList categories;*/
        private Hashtable authors;
        private Hashtable categories;
        private int _maxPostShow;
        private Int64 _blogID;
        private int _postNum;
        private string _subdomain;
        private bool _ArciveDisplayMode;
        private bool _ChatBoxIsEnabled;
        //--------------------------------------------------------------------
        public PostViewInfo()
        {
            /*authors = new ArrayList();
            categories = new ArrayList();*/
            this.authors = new Hashtable();
            this.categories = new Hashtable();
            this._maxPostShow = 10;
            this._ArciveDisplayMode = true;
        }
        //--------------------------------------------------------------------
        public void AddAuthor(Int64 id, string author)
        {
            this.authors.Add(id, author);
            return;
        }
        //--------------------------------------------------------------------
        public void AddCategory(Int64 id, string category)
        {
            this.categories.Add(id, category);
            return;
        }
        //--------------------------------------------------------------------
        public int MaxPostShow
        {
            get
            {
                return this._maxPostShow;
            }
            set
            {
                this._maxPostShow = value;
            }
        }
        //--------------------------------------------------------------------
        public Int64 BlogID
        {
            get
            {
                return this._blogID;
            }
            set
            {
                this._blogID = value;
            }
        }
        //--------------------------------------------------------------------
        public int PostNum
        {
            get
            {
                return this._postNum;
            }
            set
            {
                this._postNum = value;
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
        public string GetAuthorById(Int64 id)
        {
            if (this.authors.ContainsKey(id))
                return (string)this.authors[id];
            else
                return "";
        }
        //--------------------------------------------------------------------
        public string GetCategoryById(Int64 id)
        {
            if (this.categories.ContainsKey(id))
                return (string)this.categories[id];
            else
                return "";
        }
        //--------------------------------------------------------------------
        public bool ArciveDisplayMode
        {
            get
            {
                return this._ArciveDisplayMode;
            }
            set
            {
                this._ArciveDisplayMode = value;
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

    }
    //----------------------------------------------------------------------------------------------------------------
}
