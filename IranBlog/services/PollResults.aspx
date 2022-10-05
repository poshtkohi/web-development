<%@ Page language="c#" Inherits="services.PollResults" CodeFile="PollResults.aspx.cs" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>.:: Iranblog ::. PowerFul Tools For Bloggers ::. نظرسنجی ویلاگ ::.</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="The Greatest Weblog Technology" name="description">
		<meta content="iran,iranian,iranblog,iran blog,persian,persia,persian blog,persianblog,weblog,web,blog,forum,netwoking,network software,software,search"
			name="keywords">
		<meta content="Alireza Poshtkohi" name="author">
		<meta content="Alireza Poshtkohi" name="coyright">
		
		<STYLE>UNKNOWN { BORDER-TOP: medium none; BORDER-BOTTOM: medium none }
	UNKNOWN { BORDER-TOP: medium none }
	TD { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	P { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	HTML { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	TABLE { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	A { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	HTML { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	BODY { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	INPUT { MARGIN: 0px; FONT: 11px/150% Tahoma; COLOR: #000066; TEXT-DECORATION: none }
	.collapsed { DISPLAY: none }
	UNKNOWN { MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px }
	</STYLE>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Label id="LabelOutput" runat="server"></asp:Label>
		</form>
	</body>
</HTML>
