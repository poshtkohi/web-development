<%@ Page language="c#" Inherits="services.linkbox.Compose" CodeFile="Compose.aspx.cs" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>.:: Iranblog ::. PowerFul Tools For Bloggers ::. لینک باکس ::.</TITLE>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="The Greatest Weblog Technology" name="description">
		<meta content="iran,iranian,iranblog,iran blog,persian,persia,persian blog,persianblog,weblog,web,blog,forum,netwoking,network software,software"
			name="keywords">
		<meta content="Alireza Poshtkohi" name="author">
		<meta content="Alireza Poshtkohi" name="coyright">
		<LINK href="/services/linkbox/composestyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<FORM runat="server">
			<DIV align="center">
				<CENTER>
					<TABLE dir="rtl" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
						cellPadding="0" width="100%" border="0">
						<!--DWLayoutTable-->
						<TBODY>
							<TR>
								<TD vAlign="top" align="center" width="100%">
									<FIELDSET style="BORDER-RIGHT: #316d94 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #316d94 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; BORDER-LEFT: #316d94 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #316d94 1px solid">
										<LEGEND>
											<br>
										</LEGEND>
										<DIV class="title"><SPAN lang="fa">افزودن لینک جدید</SPAN></DIV>
										<br>
										<br>
										<br>
										<asp:label id="message" dir="ltr" runat="server" Text=".برای ارسال لینک جدید، تنها مدیر این وبلاگ می تواند از طریق کنترل پنل ایران بلاگ لینک جدید خود را وارد کند"
											BackColor="Gold" Font-Size="X-Small" ForeColor="Red" Font-Name="Tahoma" Width="100%" Font-Names="Tahoma">.برای ارسال لینک جدید، تنها مدیر این وبلاگ می تواند از طریق کنترل پنل ایران بلاگ لینک جدید خود را وارد کند</asp:label>
									</FIELDSET>
									<br>									<br>									<br>									<br>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center" width="100%"></TD>
							</TR>
						</TBODY></TABLE>
				</CENTER>
			</DIV>
		</FORM>
	</BODY>
</HTML>
