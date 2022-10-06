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
public partial class SwisheTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AppDomain.Unload(AppDomain.CurrentDomain); return;
        Process[] localByName = Process.GetProcessesByName("w3wp");
        if (localByName != null)
            if (localByName.Length > 0)
                for (int i = 0; i < localByName.Length; i++)
                    localByName[i].Close();
                    //this.Response.Write(localByName[i].ProcessName+"<br>");

        return;
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(@"D:\\hshome\\c119368\\bookstore\\search-pack\\swishe.exe");
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardInput = true;
        psi.RedirectStandardError = true;
        psi.WorkingDirectory = "D:\\hshome\\c119368\\bookstore\\search-pack\\";

        psi.Arguments = "-m 1000 -f D:\\hshome\\c119368\\bookstore\\search-pack\\all2.idx -w title= grid";
        // Start the process
        System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);


        System.IO.StreamReader sOut = proc.StandardOutput;

        // Attach the in for writing
        System.IO.StreamWriter sIn = proc.StandardInput;
        sIn.Write("-m 1000 -f D:\\hshome\\c119368\\bookstore\\search-pack\\all2.idx -w title= grid");


        // Write each line of the batch file to standard input
        /*while (strm.Peek() != -1)
        {
            sIn.WriteLine(strm.ReadLine());
        }*/

        //strm.Close();

        // Exit CMD.EXE
        //string stEchoFmt = "# {0} run successfully. Exiting";

        //sIn.WriteLine(String.Format(stEchoFmt, strFilePath));
        //sIn.WriteLine("EXIT");

        // Close the process
        //proc.Close();

        // Read the sOut to a string.

        string results;
        //        string results = sOut.ReadToEnd().Trim();

        while (true)
        {
            results = sOut.ReadLine();
            if (results == null)
                break;
            this.Response.Write(results + "<br>");

        }


        // Close the io Streams;
        sIn.Close();
        sOut.Close();
        // Get the full file path
        /*string strFilePath = @"D:\hshome\c119368\bookstore\search-pack\test.bat";

        // Create the ProcessInfo object
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe");
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardInput = true;
        psi.RedirectStandardError = true;
        psi.WorkingDirectory = @"D:\hshome\c119368\bookstore\search-pack\";

        // Start the process
        System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);


        // Open the batch file for reading
        System.IO.StreamReader strm = System.IO.File.OpenText(strFilePath);

        // Attach the output for reading
        System.IO.StreamReader sOut = proc.StandardOutput;

        // Attach the in for writing
        System.IO.StreamWriter sIn = proc.StandardInput;


        // Write each line of the batch file to standard input
        while (strm.Peek() != -1)
        {
            sIn.WriteLine(strm.ReadLine());
        }

        strm.Close();

        // Exit CMD.EXE
        string stEchoFmt = "# {0} run successfully. Exiting";

        sIn.WriteLine(String.Format(stEchoFmt, strFilePath));
        sIn.WriteLine("EXIT");

        // Close the process
        proc.Close();

        // Read the sOut to a string.

        string results;
//        string results = sOut.ReadToEnd().Trim();

        while (true)
        {
            results = sOut.ReadLine();
            if (results == null)
                break;
            this.Response.Write(results + "<br>");

        }


        // Close the io Streams;
        sIn.Close();
        sOut.Close();


        // Write out the results.
        //string fmtStdOut = "<font face=courier size=0>{0}</font>";
        //this.Response.Write(String.Format(fmtStdOut, results.Replace(System.Environment.NewLine, "<br>")));*/

    }
}