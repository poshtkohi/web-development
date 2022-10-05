<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cp.aspx.cs" Inherits="services.blogbuilderv1.cp" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<HTML xmlns="http://www.w3.org/1999/xhtml"><HEAD>
<META http-equiv=content-type content="text/html; charset=utf-8">
<link href="../styles/blue.css" rel="stylesheet" rev="stylesheet">
<title>میز کاربری ایران بلاگ</title>
</HEAD>
<BODY>
 <form id="form1" runat="server">
<DIV id=container>
<DIV class=ct>
 <div style="float: right; width: 240px; height: 101px; background: transparent url(../images/iran.png) no-repeat right  center;"><img src="../images/ib5.png"  align="bottom"></div>
 <div style=" float:left; width:200; height:100;"><img src="../images/blog.png" align="left"></div>
 </DIV>

<DIV id=toolbar>
<DIV id=tools>
<UL id=tool>
<asp:PlaceHolder ID="ControlPanelHeadrControl" runat="server"></asp:PlaceHolder></UL></DIV></DIV>
<DIV id=themebar>
<DIV id=pagesettings><asp:Label ID="LabelTime" runat="server"></asp:Label>
</DIV></DIV>
<DIV id=sidebar>
<DIV id=homeupdateblog>
<DIV class=title><img src="../images/menu.png"></DIV>
<DIV>
<UL>
	<li><a href="ajax.desk.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;میز کار کاربری شما</a><br><br>
	<li><a href="PostAdmin.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت پست های وبلاگ</a><br><br>  
	<li><a href="RssBurner.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;پیامک بلاگ</a><br><br>
	<li><a href="ajax.PostArchive.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت آرشیو موضوعی</a><br><br>
	<li><a href="ajax.TemplateAdmin.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت قالب وبلاگ</a><br><br>  
	<li><a href="ajax.links.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت پیوند های روزانه</a><br><br>
	<li><a href="ajax.links.aspx?_mode=ajax.linkss" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت لینکستان وبلاگ</a><br><br>
	<li><a href="comments.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;راهنمای نظرات وبلاگ</a><br><br>  
	<li><a href="TeamWeblog.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت نویسندگان وبلاگ</a><br><br>
	<li><a href="pages.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;صفحات اضافی مدیریت</a><br><br>
	<li><a href="PostImporter.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;انتقال مطالب</a><br><br>  
	<li><a href="ChatBox.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت تالار گفتمان</a><br><br>
	<li><a href="LinkBox.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت لینک باکس وبلاگ</a><br><br>
	<li><a href="poll.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت نظرسنجی وبلاگ</a><br><br>  
	<li><a href="NewsLetter.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت خبرنامه وبلاگ</a><br><br>
	<li><a href="stat.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت آمار وبلاگ</a><br><br>
	<li><a href="domain.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;اتصال دامنه به وبلاگ</a><br><br>  
	<li><a href="http://upload.iranblog.com/" target="frameLeft"><img src="../images/dot.png"> &nbsp;آپلود عکس و فایل</a><br><br>
	<li><a href="ajax.account.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;مدیریت تنظیمات وبلاگ</a><br><br>
	<li><a href="delete.aspx" target="frameLeft"><img src="../images/dot.png"> &nbsp;حذف عضویت</a><br><br>  
	<li><a href="http://<%=((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).Subdomain%>.iranblog.com/" target="_blank"><img src="../images/dot.png"> &nbsp;نمایش وبلاگ</a><br><br>
	<li><a href="signout.aspx"><img src="../images/dot.png"> &nbsp;خروج از سایت</a>
  </UL>
</DIV></DIV>

</DIV>
<DIV id=main2>
  <DIV id=homenews4>
  <center><iframe id="frameLeft" border=0 name="frameLeft" marginwidth=0 marginheight=0 src="ajax.desk.aspx" frameborder=0 scrolling="no" height=500px width="100%" align=""></iframe></center>
</DIV>
</DIV>
<div class=cb>
  <div class=cl>
    <div id=terms><asp:PlaceHolder ID="CopyrightFooterControl" runat="server"></asp:PlaceHolder></div>
  </div>
</div>
</DIV>
    </form>
</BODY></HTML>