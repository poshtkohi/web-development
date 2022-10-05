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

namespace services.blogbuilderv1.PostImporterExceptions
{
    /// <summary>
    /// Summary description for InvalidPostImporterTagException
    /// </summary>
    public class InvalidPostImporterTagException : Exception
    {
        public InvalidPostImporterTagException()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public override string Message
        {
            get
            {
                return "Invalid PostImporter Tag.";
            }
        }

    }
}
