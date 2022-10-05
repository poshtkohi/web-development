<%@ Page language="c#" AutoEventWireup="false"%>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if(blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>نمایش پیوند های روزانه</title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="The Greatest Weblog Technology" name="description">
		<meta content="iran,iranian,iranblog,iran blog,persian,persia,persian blog,persianblog,weblog,web,blog,forum,netwoking,network software,software,search,C#,ASP.NET,VC++,.NET"
			name="keywords">
		<meta content="Alireza Poshtkohi" name="author">
		<meta content="Alireza Poshtkohi" name="coyright">
		<meta content="Macromedia Dreamweaver 8" name="generator">
		<style>
		A:link    {FONT-SIZE: 9pt; COLOR: blue; FONT-FAMILY: Tahoma; TEXT-DECORATION: none }
		A:visited {FONT-SIZE: 9pt; COLOR: blue; FONT-FAMILY: Tahoma; TEXT-DECORATION: none }
		A:hover   {FONT-SIZE: 9pt; COLOR: red; FONT-FAMILY: Tahoma; TEXT-DECORATION: none }
		</style> 
	</HEAD>
	<body topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0">
		<div dir="rtl">
			<div style="FONT-SIZE:8pt; WIDTH:100%; COLOR:#ffffff; FONT-FAMILY:Tahoma; HEIGHT:22px; BACKGROUND-COLOR:#83a3de; TEXT-ALIGN:center">آرشیو پیوندهای روزانه</div>
			<div style="PADDING-RIGHT: 5px;PADDING-LEFT: 5px;FONT-SIZE: 9pt;PADDING-BOTTOM: 10px;WIDTH: 100%;LINE-HEIGHT: 150%;PADDING-TOP: 10px;FONT-FAMILY: Tahoma;TEXT-ALIGN: right">
			<!-------------------------------------------------------------------------------------->
			<% AlirezaPoshtkoohiLibrary.db.ShowLinks(this); %>
			<!-------------------------------------------------------------------------------------->
			</div>
			<div align="center" style="FONT-SIZE: 9pt; COLOR: black; FONT-FAMILY: Tahoma;" dir=ltr >
			
			</div>
		</div>
	</body>
</HTML>