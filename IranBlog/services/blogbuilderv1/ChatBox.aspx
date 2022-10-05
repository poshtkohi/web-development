<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatBox.aspx.cs" Inherits="services.blogbuilderv1.ChatBox" %>
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
<link rel="stylesheet" type="text/css" href="../images/style.css" />
<style>
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none }
.style1 {
	color: #FF0000;
	font-size: 14pt;
}
</style>
		<script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD><body>
  <center>
      <form id="form1" runat="server">
<table width="600px" border="0" cellpadding="0" cellspacing="0">
  <tr>
            <td width="600" valign="middle" class="v3ibbtn" align="center">فعال سازی یا عدم فعال سازی نمایش تالار گفتمان وبلاگ</td>
          </tr>
          <tr>
            <td height="50" align="center" valign="middle">
                <asp:Button ID="ChatBoxIsEnabler" 
                    runat="server" onclick="ChatBoxIsEnabler_Click" Font-Bold="True"></asp:Button></td>
  </tr>
          <tr>
            <td height="20" valign="top">&nbsp;</td>
          </tr>
          <tr>
            <td width="600" valign="middle" class="v3ibbtn" align="center">مشاهده و یا حذف پیام 
            های تالار گفتمان وبلاگ</td>
          </tr>
            <tr>
            <td width="600" valign="middle" class="v3ibbtn" align="center"><div style="WIDTH: 100%" align="center">
              <iframe id="frameLeft" border=0 name="frameLeft" marginwidth=0 marginheight=0 src="/services/ChatBox/Show.aspx?BlogID=<% =((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).BlogID.ToString() %>" frameborder=0 width=100% scrolling="no" height="200px"></iframe>
            </DIV></td>
          </tr>
    </table>
      </form>
      </center>
</body>
</HTML>
