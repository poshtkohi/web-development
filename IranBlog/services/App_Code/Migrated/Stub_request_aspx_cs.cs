/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//===========================================================================
// This file was generated as part of an ASP.NET 2.0 Web project conversion.
// This code file 'App_Code\Migrated\Stub_request_aspx_cs.cs' was created and contains an abstract class 
// used as a base class for the class 'Migrated_request' in file 'request.aspx.cs'.
// This allows the the base class to be referenced by all code files in your project.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;


namespace services
 {


abstract public class request :  System.Web.UI.Page
{
		static public string RandomText()
		{
			const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string text1 = letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(26)].ToString();
			Thread.Sleep(1);
			//string text2 = (new Random(unchecked((int)DateTime.Now.Millisecond))).Next(10).ToString();
			string text2 = letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(26)].ToString();
			Thread.Sleep(1);
			string text3 = letters[(new Random(unchecked((int)DateTime.Now.Ticks)*849)).Next(26)].ToString();
			Thread.Sleep(1);
			string text4 = letters[(new Random()).Next(26)].ToString();
			//return (text1 + text2 + text3 + text4 + (new Random()).Next(10) + (new Random(unchecked((int)DateTime.Now.Ticks)*849)).Next(10));
			return text1 + text2 + text3 + text4;
		}


}



}
