using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Diagnostics;
using System.IO;

public partial class SwisheResponse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["query"] != null)
        {
            string query = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(this.Request.QueryString["query"]));
            string _directory = new FileInfo(this.Request.PhysicalPath).DirectoryName;
            Process _p = new Process();
            _p.StartInfo.Arguments = String.Format("-m 1000 -f {0}{1} -w \"{2}\"", _directory, @"\search-pack\all2.idx", "title= grid");
            _p.StartInfo.FileName = String.Format("{0}{1}", _directory, @"\search-pack\swishe.exe");

            //_p.StartInfo.Arguments = String.Format("{4} -m {0} -f {1}{2} -w {3}", constants.SwisheMaxReturn, _directory, constants.SwisheIndexPath, _sreachPattern, String.Format("{0}{1}", _directory, constants.SwishePath));
            // _p.StartInfo.Arguments = "-TIME";//@"D:\hshome\c119368\bookstore\search-pack\swishe.exe";
            //_p.StartInfo.FileName = @"c:\WINDOWS\system32\cmd.exe";

            //WriteStringToAjaxRequest(_p.StartInfo.Arguments); return;
            _p.StartInfo.RedirectStandardOutput = true;
            //_p.StartInfo.RedirectStandardInput = true;
            _p.StartInfo.CreateNoWindow = true;
            _p.StartInfo.UseShellExecute = false;
            _p.Start();


            this.Response.Write(_p.StandardOutput.ReadToEnd());

            _p.WaitForExit();
            GC.Collect();
        }
    }
}
