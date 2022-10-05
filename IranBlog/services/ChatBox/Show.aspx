<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="services.ChatBox.Show" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
                <META http-equiv=Content-Type content="text/html; charset=UTF-8">
                <LINK href="images/style.css" rel=stylesheet>
                <SCRIPT src="images/cbmgr.js" type=text/javascript></SCRIPT>
				<SCRIPT language="javascript" src="/services/linkbox/farsi.js" type="text/javascript"></SCRIPT>
                <SCRIPT language="javascript" src="/services/linkbox/farsiEditor.js"></SCRIPT>
    <title></title>
</head>
<BODY class=mnbdy><div align="center">
<div id="loaderImg" style="width:15px; height:15px" align="left"></div>
<!--------ChatBoxShowTemplate------------------------>
 <div id="resultText"></div>
<!---------------------------------------------------->
<section runat="server" id="Postbox">
<TABLE border=0 cellPadding=0 cellSpacing=0>
  <!--DWLayoutTable-->
<tbody>
  <tr>
    <td height="49" align=middle valign=top id=tblmid><form onsubmit="return false;"><input 
      type=hidden name=key />
        <input class=frmtb onblur="frmblur(this, 'نام')" 
      onfocus="frmfocus(this, 'نام')" maxlength=30 size=9 value=نام 
      name=name id="name" lang="fa" dir="rtl"/>
      <input class=frmtb onblur="frmblur(this, 'ایمیل')" 
      onfocus="frmfocus(this, 'ایمیل')" maxlength=50 size=9 
      value="ایمیل" name=email id="email" />
      <br />
      <input class=frmtb 
      onblur="frmblur(this, 'پیام')" onfocus="frmfocus(this, 'پیام')" 
      maxlength=200 size=18 value="پیام" name=PostContent id="PostContent" lang="fa" dir="rtl"/>      
      <input class=frmbtn type=submit value=ارسال name=post id="post" onclick="DoPost('<% =this.Request.QueryString["BlogID"] %>');"/></form></td>
  </tr>
  <tr>
    <td align=middle><div align=right><a href="javascript:pop('smilies', 320, 300, 1)" class=lnk>شکلک</a> <span 
      class=lnk>-</span> <a href="javascript:void(0);" onclick="ShowMessages('1', '<% =this.Request.QueryString["BlogID"]%>');" class=lnk>بارگذاری مجدد</a> </div></td>
    </tr>
</tbody>
<TBODY></TBODY></TABLE></section>
</div>
<script language="javascript">
	ShowMessages('1', '<% =this.Request.QueryString["BlogID"]%>');
</script>
</BODY>