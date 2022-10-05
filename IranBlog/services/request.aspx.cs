/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_request_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'request.aspx' was also modified to refer to the new class name.
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
	//-----------------------------------------------------------------------------
	public partial class Migrated_request : request
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string query = Request.QueryString["i"];
			if(query == null) query = "";
			switch(query)
			{
				case "randimg":
					RandomImageGeneration();
					break;
				default:
					break;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
		//--------------------------------------------------------------------
		private void RandomImageGeneration()
		{
			/*if(Session["acknowledge"] == null || (string)Session["acknowledge"] == "")
                Session["acknowledge"] = NewRandomText();*/
            Session["acknowledge"] = NewRandomText();
			Bitmap bmp = new Bitmap(58, 35);
			Graphics grfx = Graphics.FromImage(bmp);
			//------------------------
			int cx =58, cy = 47;
			Font font = new Font("Tahoma", 40, FontStyle.Bold);
			SizeF sizef = grfx.MeasureString((string)Session["acknowledge"], font);
			PointF ptf    = new PointF((cx - sizef.Width) / 2,
				(cy - sizef.Height) / 2);
			RectangleF rectf = new RectangleF(ptf, sizef);
			LinearGradientBrush lgbrush = new LinearGradientBrush(rectf, 
				Color.Green, Color.GreenYellow, 
				LinearGradientMode.Vertical);
			grfx.Clear(Color.GreenYellow);
			float fScaleHorz = 60/sizef.Width;
			float fScaleVert = 40/sizef.Height;
			grfx.ScaleTransform(fScaleHorz, fScaleVert);
			/*int m = -1;
			if((new Random(unchecked((int)DateTime.Now.Millisecond))).Next(2) == 0)
			{
				m = 1;
			}
			else m = -1;
			grfx.RotateTransform(m*(new Random(unchecked((int)DateTime.Now.Second))).Next(6));*/
			grfx.DrawString((string)Session["acknowledge"], font, lgbrush, -1,-1);
			//------------------------
			Response.ContentType="image/gif";
			bmp.Save(Response.OutputStream, ImageFormat.Gif);
			bmp.Dispose();
			font.Dispose();
			grfx.Dispose();
		}
		//--------------------------------------------------------------------
		/*static public string RandomText()
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
		}*/
        static public string NewRandomText()
        {
            const string letters = "0123456789";
            string text1 = letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(10)].ToString();
            Thread.Sleep(1);
            //string text2 = (new Random(unchecked((int)DateTime.Now.Millisecond))).Next(10).ToString();
            string text2 = letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(10)].ToString();
            Thread.Sleep(1);
            string text3 = letters[(new Random(unchecked((int)DateTime.Now.Ticks) * 849)).Next(10)].ToString();
            Thread.Sleep(1);
            string text4 = letters[(new Random()).Next(10)].ToString();
            return (text1 + text2 + text3 + text4 + (new Random()).Next(10) + (new Random(unchecked((int)DateTime.Now.Ticks)*849)).Next(10));
            //return text1 + text2 + text3 + text4;
        }
		//--------------------------------------------------------------------
	}
}