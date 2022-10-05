<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rss.aspx.cs" Inherits="services.blogbuilderv1.rss" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
<meta http-equiv="Content-Language" content="fa">
<link rel="stylesheet" type="text/css" href="../images/style.css" />
<style>
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none }
.style1 {
	color: #FF0000;
	font-size: 14pt;
}
</style>

  </HEAD><body>
<form id="Form1" method="post" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="554" height="5" align="center">
                              </td>
							</tr>
							<tr>
								<td height="27" background="." class="v3ibbtn">
							  <p align="center" dir="rtl">تگ
								<span lang="en-us">HTML</span> استاندارد 
								<span lang="en-us">RSS</span> 
							  وبلاگ های ایران بلاگ</td>
							</tr>
							<tr>
								<td height="97" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
								  <tr>
								    <td width="554" height="145" align="center" valign="middle"><textarea readonly="readonly" style="MARGIN-TOP: 5px; FONT-SIZE: 8pt; WIDTH: 400px; COLOR: #333333; FONT-FAMILY: Tahoma; HEIGHT: 150px; TEXT-ALIGN: left"
										name="code">
<a style="color: #FFFFFF; text-decoration: none; font-family: Arial; font-size: 8pt; border: 1px solid #FF9955;  background-color: #FF6600"  href='http://<%=((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).Subdomain%>.iranblog.com/services/rss.aspx' target="_blank">&nbsp;RSS&nbsp;</a>
</textarea></td>
							      </tr>
							    </table></td>
							</tr>
							<tr>
							  <td height="27">&nbsp;</td>
							</tr>
							<tr>
							  <td height="27" align="center" valign="middle" class="v3ibbtn">
							    <p align="center" dir="rtl">مشاهده
							  <span lang="en-us">RSS</span> وبلاگ</td>
							</tr>
							<tr>
							  <td height="104" align="center" valign="middle"><a style="color: #FFFFFF; text-decoration: none; font-family: Arial; font-size: 8pt; border: 1px solid #FF9955;  background-color: #FF6600" href='http://<%=((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).Subdomain%>.iranblog.com/services/rss.aspx' target="_blank">&nbsp;RSS&nbsp;</a></td>
		  </tr>
							<tr>
							  <td height="40">&nbsp;</td>
		  </tr>
  </table>
</form>
</body>
</HTML>