<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax.TemplateAdmin.aspx.cs" Inherits="services.blogbuilderv1.ajax.TemplateAdmin" validateRequest="false"%>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="/services/styles/cp.css" />
		<script type="text/javascript" src="/services/js/AjaxCore.js"></script>
		<script type="text/javascript" src="/services/js/Functions.js"></script>
        <script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white">
<center>
			<form id="form" name="form" onSubmit="return false;" method="post" runat="server">
				<div id="message" class="message" style="display:none"></div>
		      <div class="box" style="WIDTH:550px; display:none" id="up">
				<div class="content" align="text-align:right">
			  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">ویرایش قالب وبلاگ
                        </div>
                            <tr>
                              <td colspan="2" align="center"><textarea name="TemplateContent" id="TemplateContent" class="Textbox" style="WIDTH: 100%; HEIGHT: 500px; overflow:scroll" wrap="physical"></textarea></td>
                            </tr>
		
						<tr>
							<td colspan="2" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="46%"><img src="/services/images/save.gif" onClick="DoPost('ajax.TemplateAdmin.save');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td width="54%" height="20" align="center" dir="rtl">&nbsp;</td>
				</tr>
					</table>
				</div>
			</div>
            </form>
            <DIV class=box style="WIDTH: 550px">
            <tr>
				<td width="100%" height="20" colspan="2" align="left">
                	<input class="oBtn" title="ویرایش قالب وبلاگتان" style="WIDTH: 209px;" type="submit" value="ویرایش قالب وبلاگتان" onClick="ItemLoad('ajax.TemplateAdmin','-1');" id="new" name="new" />
                	<input id="cancel" class="oBtn" style="width: 100%px; height: 23px; display:none" type="button" value="انصراف و بازگشت" onClick="Cancel();" /></td>
			</tr>
            <DIV class=header>
            <DIV class=title>قالب های ایران بلاگ</DIV></DIV>
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
	ShowItems('1', 'ShowAjaxTemplates');
</script>