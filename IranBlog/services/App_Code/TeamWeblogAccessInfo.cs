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

namespace services.blogbuilderv1
{
    /// <summary>
    /// Summary description for TeamWeblogInfo
    /// </summary>
    public class TeamWeblogAccessInfo
    {
        private bool postAccess = false;
        private bool othersPostAccess = false;
        private bool subjectedArchiveAccess = false;
        private bool weblogLinksAccess = false;
        private bool dailyLinksAccess = false;
        private bool templateAccess = false;
        private bool pollAccess = false;
        private bool linkBoxAccess = false;
        private bool newsletterAccess = false;
        private bool fullAccess = false;
        //------------------------------------------------------------
        public TeamWeblogAccessInfo(bool PostAccess, bool OthersPostAccess, bool SubjectedArchiveAccess, 
                              bool WeblogLinksAccess, bool DailyLinksAccess, bool TemplateAccess, 
                              bool PollAccess, bool LinkBoxAccess, bool NewsletterAccess, bool FullAccess)
        {
            this.postAccess = PostAccess;
            this.othersPostAccess = OthersPostAccess;
            this.subjectedArchiveAccess = SubjectedArchiveAccess;
            this.weblogLinksAccess = WeblogLinksAccess;
            this.dailyLinksAccess = DailyLinksAccess;
            this.templateAccess = TemplateAccess;
            this.pollAccess = PollAccess;
            this.linkBoxAccess = LinkBoxAccess;
            this.newsletterAccess = NewsletterAccess;
            this.fullAccess = FullAccess;
        }
        //------------------------------------------------------------
        public TeamWeblogAccessInfo()
        {
        }
        //------------------------------------------------------------
        public bool PostAccess
        {
            get
            {
                return this.postAccess;
            }
            set
            {
                this.postAccess = value;
            }
        }

        public bool OthersPostAccess
        {
            get
            {
                return this.othersPostAccess;
            }
            set
            {
                this.othersPostAccess = value;
            }
        }

        public bool SubjectedArchiveAccess
        {
            get
            {
                return this.subjectedArchiveAccess;
            }
            set
            {
                this.subjectedArchiveAccess = value;
            }
        }

        public bool WeblogLinksAccess
        {
            get
            {
                return this.weblogLinksAccess;
            }
            set
            {
                this.weblogLinksAccess = value;
            }
        }

        public bool DailyLinksAccess
        {
            get
            {
                return this.dailyLinksAccess;
            }
            set
            {
                this.dailyLinksAccess = value;
            }
        }

        public bool TemplateAccess
        {
            get
            {
                return this.templateAccess;
            }
            set
            {
                this.templateAccess = value;
            }
        }

        public bool PollAccess
        {
            get
            {
                return this.pollAccess;
            }
            set
            {
                this.pollAccess = value;
            }
        }

        public bool LinkBoxAccess
        {
            get
            {
                return this.linkBoxAccess;
            }
            set
            {
                this.linkBoxAccess = value;
            }
        }

        public bool NewsletterAccess
        {
            get
            {
                return this.newsletterAccess;
            }
            set
            {
                this.newsletterAccess = value;
            }
        }

        public bool FullAccess
        {
            get
            {
                return this.fullAccess;
            }
            set
            {
                this.fullAccess = value;
            }
        }
        //------------------------------------------------------------
    }
}
