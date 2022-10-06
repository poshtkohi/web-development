/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using System.Diagnostics;
using System.IO;

namespace bookstore
{
    public partial class UnRar : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
        }
        protected void unrar_Click(object sender, EventArgs e)
        {
            //D:\hshome\c119368\bookstore\admin\test.aspx 
            string _directory = new FileInfo(this.Request.PhysicalPath).DirectoryName;
            Process _p = new Process();
            _p.StartInfo.Arguments = String.Format("x {0} {1}", this.from.Text, this.to.Text);
            //_p.StartInfo.Arguments = @"e D:\hshome\c119368\bookstore\images\uploads\covers.rar";
            _p.StartInfo.FileName = String.Format("{0}{1}", _directory, @"\UnRar.exe");
            _p.StartInfo.RedirectStandardOutput = true;
            //_p.StartInfo.CreateNoWindow = true;
            _p.StartInfo.UseShellExecute = false;
            _p.Start();
            string _output = _p.StandardOutput.ReadToEnd();

            _p.WaitForExit();

            this.Response.Write(_output);
        }
        protected void path_Click(object sender, EventArgs e)
        {
            this.Response.Write(this.Request.PhysicalPath);
        }
}

}