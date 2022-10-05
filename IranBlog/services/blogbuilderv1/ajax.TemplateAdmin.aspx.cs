/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

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

using System.Data.SqlClient;
using System.IO;
using AlirezaPoshtkoohiLibrary;
using services;
using IranBlog.Classes.Security;

namespace services.blogbuilderv1.ajax
{
    public partial class TemplateAdmin : System.Web.UI.Page
    {
        //--------------------------------------------------------------------------------
        private bool TeamWeblogAccessControl(SigninSessionInfo _SigninSessionInfo)
        {
            if (_SigninSessionInfo.IsInTeamWeblogMode)
            {
                if (_SigninSessionInfo.TeamWeblogAccessInfo.FullAccess)
                    return true;
                if (_SigninSessionInfo.TeamWeblogAccessInfo.TemplateAccess)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        //--------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = Common.IsLoginProc(this);

            if (!_IsLogin)
            {
                if (this.Request.Form["mode"] != null)
                    Common.WriteStringToAjaxRequest("Logouted", this);
                else
                    this.Response.Redirect("Logouted.aspx", true);
                return;
            }

            SigninSessionInfo _SigninSessionInfo = (SigninSessionInfo)this.Session["SigninSessionInfo"];

            if (!TeamWeblogAccessControl(_SigninSessionInfo))
            {
                this.Response.Redirect("AccessLimited.aspx", true);
                return;
            }

            if (this.Request.Form["mode"] != null)
            {
                switch (this.Request.Form["mode"])
                {
                    case "ShowAjaxTemplates":
                        ShowAjaxTemplates(_SigninSessionInfo);
                        return;
                    case "load":
                        AjaxTemplateLoad(_SigninSessionInfo);
                        return;
                    case "TemplateSelect":
                        TemplateSelect(_SigninSessionInfo);
                        return;
                    case "TemplateSave":
                        TemplateSave(_SigninSessionInfo);
                        return;
                    default:
                        return;
                }
            }
        }
        //----------------------------------------------------------------------------------
        private void TemplateSave(SigninSessionInfo _SigninSessionInfo)
        {
            //throw new Exception();
            string _TemplateContent = this.Request.Form["TemplateContent"];

            //---------user input validations-----------------------------------------------
            if (_TemplateContent == null || _TemplateContent == "")
            {
                WriteStringToAjaxRequest(".محتویات قالب وبلاگ شما نمی تواند خالی باشد");
                return;
            }
            if (_TemplateContent.Length > 256*1024)
            {
                WriteStringToAjaxRequest(".حجم قالب وبلاگ شما نمی تواند از 256 کیلو بایت بیشتر باشد");
                return;
            }
            //------------------------------------------------------------------------------

            StreamWriter _sw = File.CreateText(constants.RootDircetoryWeblogs + "\\" + _SigninSessionInfo.Subdomain + "\\Default.html");
            _sw.Write(_TemplateContent);
            _sw.Close();

            WriteStringToAjaxRequest(".قالب وبلاگتان با موفقیت به روز رسانی شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void TemplateSelect(SigninSessionInfo _SigninSessionInfo)
        {
            int _id = -1;
            try
            {
                _id = Convert.ToInt32(this.Request.Form["SelectedTemplateID"]);
            }
            catch { return; }

            if(_id <= 0)
                _id = 56;
				
			string dir = constants.RootDircetoryWeblogs + "\\" + _SigninSessionInfo.Subdomain;
			
            if(!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
				
            File.Copy(constants.TemplatesPath + "Template" + _id.ToString() + ".html",
                dir +  "\\Default.html", true);

            WriteStringToAjaxRequest(".قالب انتخاب شده با موفقیت جایگزین قالب قبلیتان شد");

            return;
        }
        //--------------------------------------------------------------------------------
        private void AjaxTemplateLoad(SigninSessionInfo _SigninSessionInfo)
        {
            Int64 _id = -1;
            try
            {
                _id = Convert.ToInt64(this.Request.Form["id"]);
            }
            catch { return; }

            string _filename = constants.RootDircetoryWeblogs + "\\" + _SigninSessionInfo.Subdomain + "\\Default.html";
  
            if (File.Exists(_filename))
            {
                /*  
                    TemplateContent
                */

                StreamReader _sr = File.OpenText(_filename);
                string _TemplateContent = _sr.ReadToEnd();
                _sr.Close();

                WriteStringToAjaxRequest(String.Format("{0},",
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_TemplateContent))
                    ));

                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");
                return;
            }
        }
        //--------------------------------------------------------------------------------
        public static int[] SortingInt32ArrayByLINQ(int[] _intArray, bool _descending)
        {
            if (_descending)
            {
                var sortedCollection = from c in _intArray orderby c descending select c;
                return (int[])sortedCollection.ToArray();
            }
            else
            {
                var sortedCollection = from c in _intArray orderby c ascending select c;
                return (int[])sortedCollection.ToArray();
            }
        }
        //--------------------------------------------------------------------------------
        private void ShowAjaxTemplates(SigninSessionInfo _SigninSessionInfo)
        {
            int[] _templateIDs = null;
            if (this.Cache["_templateIDs"] == null)
            {
                ArrayList _al = new ArrayList();
                string[] files = Directory.GetFiles(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\templates\");
                for (int i = 0; i < files.Length; i++)
                {
                    int p2 = files[i].LastIndexOf(".html");
                    if (p2 > 0)
                    {
                        int p1 = files[i].LastIndexOf("Template");
                        //this.Response.Write(String.Format("{0}<br>", files[i].Substring(p1 + "Template".Length, p2 - p1 - "Template".Length)));
                        if (p1 > 0)
                            _al.Add(Convert.ToInt32(files[i].Substring(p1 + "Template".Length, p2 - p1 - "Template".Length)));
                    }
                }
                _templateIDs = (Int32[])_al.ToArray(typeof(int));
                _templateIDs = SortingInt32ArrayByLINQ(_templateIDs, true);
                /*this.Response.Write(String.Format("{0}<br>", _templateIDs.Count));
                for (int i = 0; i < _templateIDs.Length; i++)
                {
                    this.Response.Write(String.Format("{0}<br>", _templateIDs[i]));
                }*/
            }
            else
                _templateIDs = (int[])this.Cache["_templateIDs"];

            int currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(this.Request.Form["page"]);
            }
            catch { }

            if (currentPage == 0)
                currentPage++;

            /*if (currentPage > 1 && index >= _templateIDs.Length)
            {
                WriteStringToAjaxRequest("DoRefresh");
                return;
            }*/



            if (_templateIDs != null)
            {
                string template = "";
                if (this.Cache["ajax.TemplateAdmin.template"] == null)
                {
                    StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ajax.TemplateAdmin.template.html");
                    template = sr.ReadToEnd();
                    this.Cache["ajax.TemplateAdmin.template"] = template;
                    sr.Close();
                }
                else
                    template = (string)this.Cache["ajax.TemplateAdmin.template"];

                /*StreamReader sr = new StreamReader(new FileInfo(this.Request.PhysicalPath).DirectoryName + @"\AjaxTemplates\ajax.TemplateAdmin.template.html");
                template = sr.ReadToEnd();
                sr.Close();*/

                int _p1Post = template.IndexOf("<post>") + "<post>".Length;
                int _p2Post = template.IndexOf("</post>");
                int _p1Paging = template.IndexOf("<paging>") + "<paging>".Length;
                int _p2Paging = template.IndexOf("</paging>");
                if (_p1Post <= 0 || _p2Post <= 0)
                {
                    this.Response.Write(template);
                    this.Response.OutputStream.Flush();
                    return;
                }
                this.Response.Write(template.Substring(0, _p1Post - "<post>".Length));
                this.Response.Flush();
                string _mainFormat = template.Substring(_p1Post, _p2Post - _p1Post);


                string temp = null;

                int index = (currentPage - 1) * constants.MaxShowAjaxTemplates;

                for (int i = index ; i < index + constants.MaxShowAjaxTemplates ; i++ )
                {
                    if (i >= _templateIDs.Length)
                        break;
                    temp = _mainFormat;
                    temp = temp.Replace("[id]", _templateIDs[i].ToString());

                    this.Response.Write(temp);
                    this.Response.Flush();
                }


                /*if (currentPage == 1)
                    this.Session["_ItemNumAjaxLinks"] = 0;//(int)command.Parameters["@LinkNum"].Value;*/

                if (_p1Paging > 0 && _p2Paging > 0)
                {
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length, _p1Paging - (_p2Post + "</post>".Length)));
                    //Login.PagingFormat(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), currentPage, (int)this.Session["ItemNum"]);
                    Common.AjaxDoPaging(this, template.Substring(_p1Paging, _p2Paging - _p1Paging), "ShowAjaxTemplates", currentPage, constants.MaxShowAjaxTemplates, _templateIDs.Length, constants.ShowAjaxTemplatesPagingNumber, "ShowItems");
                }
                else
                    this.Response.Write(template.Substring(_p2Post + "</post>".Length));
                this.Response.Flush();

                //this.Response.Close();
                this.Response.End();
                return;
            }
            else
            {
                WriteStringToAjaxRequest("NoFoundPost");

                return;
            }
        }
        //--------------------------------------------------------------------------------
        private void WriteStringToAjaxRequest(string str)
        {
            this.Response.Write(str);
            this.Response.Flush();
            //this.Response.Close();
            this.Response.End();
        }
        //--------------------------------------------------------------------------------
    }
}

