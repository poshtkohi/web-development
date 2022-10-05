<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostImporter.aspx.cs" Inherits="services.blogbuilderv1.PostImporter" validateRequest=false %>
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
<SCRIPT LANGUAGE="JavaScript">
function ClipBoard() 
{
	Copied = document.getElementById("template").createTextRange();
	Copied.execCommand("RemoveFormat");
	Copied.execCommand("Copy");
	//window.clipboardData.setData('text',document.getElementById("template").innerText);

}
</SCRIPT> 
<script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD><body>
  <center>
      <form runat="server" style="width: 100%">
<table width="100%" border="0" cellpadding="0" cellspacing="0" height="">
  <tr>
            <td height="24" colspan="6" align="center" valign="middle" class="v3ibbtn">تعریف 
			یا بررسی وضعیت انتقال پست ها</td>
    </tr>
          <tr>
            <td height="12" colspan="6" align="justify" valign="middle" bgcolor="#FFFF66" dir="rtl">                
            <asp:Label ID="status" runat="server" CssClass="v3color_orange" BackColor="#FFFF66" Font-Size="X-Small" Height="100%" Visible="False" style="width:100%; text-align:justify"></asp:Label>            </td>
  </tr>
          <tr bgcolor="#F4F4F4">
            <td width="201" rowspan="2" align="right" valign="middle" dir="rtl"> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="domain"
                                  ErrorMessage="آدرس وبلاگ خالی است." Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
              <br>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                  ControlToValidate="domain" ErrorMessage="آدرس وبلاگ نامعتبر است." ValidationExpression="[a-zA-Z0-9]+[\.](persianblog\.ir|blogfa\.com){1}$" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
            <td width="24" rowspan="2" valign="top">&nbsp;</td>
            <td width="185" height="24" valign="middle">http://<asp:TextBox ID="domain" runat="server" Enabled="false" MaxLength="50"></asp:TextBox>            </td>
    <td width="68" valign="top">http://example.blogfa.com<br>
      http://example.persianblog.ir</td>
    <td width="68" valign="top" class="v3color_orange" align="right" dir="rtl">مثال ها:</td>
    </مثال ها:></td>
    </tr>
          <tr>
            <td height="7" colspan="4" bgcolor="#F4F4F4"></td>
          </tr>
          <tr>
            <td height="40" colspan="6" align="center" valign="middle">
              <asp:Button ID="CreateNewSession" runat="server" Text="تعریف انتقال جدید" 
                     Visible="false" onclick="CreateNewSession_Click" Enabled="False"/>&nbsp;              
              <asp:Button ID="StartSession" runat="server" Text="آغازعملیات انتقال" 
                     Visible="false" Enabled="False" onclick="StartSession_Click"/>&nbsp;   
              <asp:Button ID="CotntinueSession" runat="server" Text="ادامه عملیات انتقال" 
                     Visible="false" Enabled="False" onclick="CotntinueSession_Click"/>&nbsp;            
              <asp:Button ID="DeleteExisitingSession" runat="server" Text="حذف عملیات انتقال" 
                    Visible="false" onclick="DeleteExisitingSession_Click" Enabled="False"/>              </td>
    </tr>
    </table>
       </form>     
       <section id="TemplateSection" runat="server" Visible="False">
         <table width="100%" border="0" cellpadding="0" cellspacing="0" height="">
           <tr>
             <td height="24" align="center" valign="middle" class="v3ibbtn" dir="rtl">کد قالب استاندارد (کد قالب زیر را جایگزین کد قالب خود در بلاگفا یا پرشین بلاگ کنید) </td>
             <td height="12" align="right" valign="middle" dir="rtl"><!--DWLayoutEmptyCell-->&nbsp;</td>
           </tr>
           <tr bgcolor="#F4F4F4">
             <td height="24" align="center" valign="middle"><label>
               <textarea name="template" id="template" style=" width:400px; height:150px" runat="server" readonly="readonly"></textarea>
             </label></td>
           </tr>
         </table>
         <table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td align="center" valign="middle" height="40px">
                <input type="submit" name="copy" id="copy" value="کپی قالب" onclick="ClipBoard();"/>            </td>
    </tr>
    </table>
    </section>
    </center>
  </body>
</HTML>