/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Threading;

public partial class RandomImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Create a Bitmap image
        System.Drawing.Bitmap ImageSrc = new System.Drawing.Bitmap(150, 50);
        for (int iX = 0; iX < ImageSrc.Width; iX++)
        {
            for (int iY = 0; iY < ImageSrc.Height; iY++)
            {
                ImageSrc.SetPixel(iX, iY, System.Drawing.Color.White);
            }
        }
        // Fill it randomly with white pixels
        /*for (int iX = 0; iX < ImageSrc.Width ; iX+=2)
        {
            for (int iY = 0; iY < ImageSrc.Height ; iY+=1)
            {
                ImageSrc.SetPixel(iX, iY, System.Drawing.Color.Black);
            }
        }*/

        for (int iX = 0; iX < ImageSrc.Width; iX++)
        {
            for (int iY = 0; iY < ImageSrc.Height; iY++)
            {
                double val = new Random().NextDouble();
                if (val < .3 && val > 0)
                {
                    ImageSrc.SetPixel(iX, iY, System.Drawing.Color.BlanchedAlmond);
                    //System.Threading.Thread.Sleep(0);
                }
                if (val < 0.7 && val > .4 )
                {
                    ImageSrc.SetPixel(iX, iY, System.Drawing.Color.Green);
                    System.Threading.Thread.Sleep(2);
                }
                if (val < 0.9 && val > .7 )
                {
                    ImageSrc.SetPixel(iX, iY, System.Drawing.Color.Aquamarine);
                    System.Threading.Thread.Sleep(1);
                }
            }
        }

        // Create an ImageGraphics Graphics object from bitmap Image
        System.Drawing.Graphics ImageGraphics = System.Drawing.Graphics.FromImage(ImageSrc);

        // Generate random code. 
        string hiddenCode = RandomText();
        this.Session["SecurityCode"] = hiddenCode;
        // Set Session variable
        //Session("hiddenCode") = hiddenCode

        // Draw random code within Image
        System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 20, FontStyle.Italic);
        System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Single x = (float)(5.0 + (new Random().NextDouble() / 1) * (ImageSrc.Width - 130));
        Single y = (float)(5.0 + (new Random().NextDouble() / 1) * (ImageSrc.Height - 30));
        StringFormat drawFormat = new System.Drawing.StringFormat();
        ImageGraphics.DrawString(hiddenCode, drawFont, drawBrush, x, y, drawFormat);

        // Change reponse content MIME type
        this.Response.ContentType = "image/jpeg";

        // Sent Image using Response OutputStream
        ImageSrc.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        // Dispose used Objects 
        drawFont.Dispose();
        drawBrush.Dispose();
        ImageGraphics.Dispose();
        //this.Response.Flush();
        //this.Response.Close();
    }
    //--------------------------------------------------------------------
    /*static public string RandomText()
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string text1 = letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(26)].ToString();
        Thread.Sleep(1);
        string text2 = (new Random(unchecked((int)DateTime.Now.Millisecond))).Next(10).ToString();
        //string text2 = letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(26)].ToString();
        Thread.Sleep(1);
        string text3 = letters[(new Random(unchecked((int)DateTime.Now.Ticks) * 849)).Next(26)].ToString();
        Thread.Sleep(1);
        string text4 = letters[(new Random()).Next(26)].ToString();
        return (text1  + text2  + text3   + text4  + (new Random()).Next(10)  + (new Random(unchecked((int)DateTime.Now.Ticks) * 849)).Next(10));
        //return text1 + text2 + text3 + text4;
    }*/
    //--------------------------------------------------------------------
    private static string RandomText()
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string text = letters[(new Random(unchecked((int)DateTime.Now.Second * 2))).Next(26)].ToString();
        text += letters[(new Random(unchecked((int)DateTime.Now.Minute))).Next(26)].ToString();
        text += letters[(new Random(unchecked((int)DateTime.Now.Ticks))).Next(26)].ToString();
        text += letters[(new Random(unchecked((int)DateTime.Now.Millisecond))).Next(26)].ToString();
        text += letters[(new Random(unchecked((int)DateTime.Now.Ticks * 6))).Next(26)].ToString();
        text += letters[(new Random(unchecked((int)DateTime.Now.TimeOfDay.Ticks))).Next(26)].ToString();
        return text;
    }
    //--------------------------------------------------------------------
}
