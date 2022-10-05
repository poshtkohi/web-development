<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax.links.aspx.cs" Inherits="services.blogbuilderv1.ajax.links" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<%
  string _mode = "ajax.links";
  switch(this.Request.QueryString["_mode"])
  {
  	case "ajax.linkss":
		_mode = "ajax.linkss";
		break;
	default:
		break;
  }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="/services/styles/cp.css" />
		<script type="text/javascript" src="/services/js/AjaxCore.js"></script>
		<script type="text/javascript" src="/services/js/Functions.js"></script>
		<script type="text/javascript" src="/services/js/farsi.js"></script>
        <script language="javascript" type="text/javascript" src="js/common.js"></script>
        <%
			if(_mode == "ajax.linkss")
				this.Response.Write("<script type=\"text/javascript\">_IsAjaxLinks=false;</script>");
		%>

  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white" onUnload="_editorIsDefined=false;">
<center>
			<form id="form" name="form" onSubmit="return false;" method="post">
				<div id="message" class="message" style="display:none"></div>
			    <div class="box" style="WIDTH:550px; display:none" id="up">
				<div class="content" align="text-align:right">
			  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">ارسال پیوند های
						(روزانه) وبلاگ جدید و یا ویرایش پیوند انتخاب شده</div>
                        </div>
						<tr>
						  <td width="112">&nbsp;</td>
					      <td width="431" align="center" valign="middle"><input name="LinkTitle" type="text" id="LinkTitle" class="Textbox" style="width:300px;" maxlength="400" lang="fa"/>
				          <span class="title"> :عنوان پیوند</span></td>
						</tr>
						<tr>
						  <td width="112">&nbsp;</td>
					      <td width="431" align="center" valign="middle"><input name="LinkAddress" type="text" class="Textbox" id="LinkAddress" style="width:300px;direction:ltr;text-align:left" lang="en" value="http://" maxlength="400"/>
				          <span class="title"> :آدرس پیوند</span></td>
						</tr>		
		
						<tr>
							<td colspan="2" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="112"><img src="/services/images/save.gif" onClick="DoPost('<% =_mode %>');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td height="20" align="center" dir="rtl">&nbsp;</td>
				</tr>
					</table>
				</div>
			</div>
            </form>
            <DIV class=box style="WIDTH: 550px">
            <tr align="left">
				<td width="100%" height="20" colspan="2" align="left">
                	<input class="oBtn" title="ارسال پیوند جدید" style="WIDTH: 209px;" type="submit" value="ارسال پیوند جدید" onClick="New('<% =_mode %>');" id="new" name="new"/>
                	<input id="cancel" class="oBtn" style="width: 100%px; height: 23px; display:none" type="button" value="انصراف و بازگشت" onClick="Cancel();" /></td>
			</tr>
            <DIV class=header>
            <DIV class=title>پیوند های قبلی</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg" align="center"><img src="/services/images/loading.gif"/></div>
                    <div id="resultText"></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script type="text/javascript">
	ShowItems('1', 'ShowAjaxLinks');
</script>