<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="bookstore.admin._Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>::. bookstore Admin Login .::</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="alireza.poshtkohi@gmail.com" name="ASP.NET CMS Programmer and Developer Email">
		<meta content="Alireza Poshtkohi" name="ASP.NET CMS Programmer and Developer">
		<LINK href="v3fa.css" type="text/css" rel="stylesheet">
			<style> 
BODY { SCROLLBAR-FACE-COLOR: #9bb700; SCROLLBAR-HIGHLIGHT-COLOR: #66cc99; SCROLLBAR-SHADOW-COLOR: #ffdfbf; SCROLLBAR-3DLIGHT-COLOR: #9bb700; SCROLLBAR-ARROW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #49692c; SCROLLBAR-DARKSHADOW-COLOR: #9bb700 } 
</style>
			<style type="text/css">
.style12 { FONT-SIZE: 10px }
.v3selectreg { BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; FONT-SIZE: 11px; BORDER-LEFT: #cccccc 1px solid; WIDTH: 200px; COLOR: #333333; BORDER-BOTTOM: #cccccc 1px solid; FONT-FAMILY: verdana, sans-serif; HEIGHT: 18px }
.v3send { BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; FONT-SIZE: 11px; BORDER-LEFT: #cccccc 1px solid; BORDER-BOTTOM: #cccccc 1px solid; FONT-FAMILY: verdana, sans-serif; HEIGHT: 23px; BACKGROUND-COLOR: #f0f0f0 }
.linkl { FONT-SIZE: 13px; COLOR: #666666; FONT-FAMILY: tahoma; TEXT-DECORATION: underline }
.linkl:hover { COLOR: #990000; TEXT-DECORATION: none }
.linkl:visited { FONT-SIZE: 13px; FONT-FAMILY: tahoma; TEXT-DECORATION: none }
</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="678" align="center">
				<!--DWLayoutTable-->
				<tr>
					<td vAlign="top" colSpan="2" height="61"><table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<!--DWLayoutTable-->
							<tr>
								<td class="mainb" vAlign="top" colSpan="5" height="28"><div align="right"><img src="images/admin_02.jpg" width="128" height="54"><img src="images/admin_03.gif" width="82" height="54"></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width="3" height="37"></td>
					<td class="main" vAlign="middle" width="669" align="right"><div align="right">لطفا کلمه 
							کاربری و کلمه عبور خود را وارد نموده و سپس بر روی " ورود به سیستم" کلیک نمایید.
							<span class="mainb"></span></div>
					</td>
				</tr>
				<tr>
					<td height="21"></td>
					<td dir="rtl" vAlign="top" bgColor="#f7f7f7"><div align="right"><IMG height="14" src="images/64-(17).gif" width="14" align="absMiddle">ورود 
							به سیستم مدیریت
						</div>
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2" height="138"><table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<!--DWLayoutTable-->
							<tr>
								<td dir="rtl" vAlign="middle" align="right" colSpan="3" height="57"><div class="mainb">
										<div align="right"><IMG height="11" src="images/iaudio.gif" width="11">جهت ورود 
											به سیستم مدیریت شما باید دارای هویت مدیر وب سایت باشید !</div>
										<div style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 5px"
											align="center">
											<asp:label id="error" runat="server" CssClass="mainb" Font-Bold="True" ForeColor="OrangeRed"
												Visible="False" Width="100%" BackColor="Gold" BorderColor="Yellow" BorderWidth="1px"></asp:label>
										</div>
										<div align="right"></div>
									</div>
									<div class="mainb" style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 16px; PADDING-TOP: 16px"
										align="center"></div>
								</td>
							</tr>
							<tr>
								<td width="218" height="81">&nbsp;</td>
								<td dir="rtl" vAlign="top" width="100%"><div class="mainb" style="PADDING-RIGHT: 0px; PADDING-BOTTOM: 6px; PADDING-TOP: 6px">کلمه 
										کاربری :
										<span class="mainb">
											<asp:textbox id="username" runat="server" CssClass="v3selectreg" Width="200px" MaxLength="50" style="direction: ltr"></asp:textbox>
										</span></div>
									<div class="mainb" style="PADDING-RIGHT: 0px; PADDING-BOTTOM: 6px; PADDING-TOP: 6px">کلمه 
										عبور :<span lang="en-us">&nbsp;&nbsp;&nbsp; </span>&nbsp;<span class="mainb">
											<asp:textbox id="password" runat="server" CssClass="v3selectreg" Width="200px" MaxLength="50" TextMode="Password" style="direction: ltr"></asp:textbox>
										</span></div>
								</td>
								<td width="227">&nbsp;</td>
							</tr>
							<tr>
								<td height="1"><IMG height="1" alt="" src="../spacerP.gif" width="218"></td>
								<td></td>
								<td><IMG height="1" alt="" src="../spacerP.gif" width="227"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="65"></td>
					<td vAlign="top"><table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<!--DWLayoutTable-->
							<tr>
								<td vAlign="top" align="center" width="100%" height="48"><!-- MSCellFormattingType="content" -->
									&nbsp;
									<asp:button id="login" runat="server" CssClass="v3send" Text="Sign In" OnClick="login_Click"></asp:button></td>
							</tr>
							<tr>
								<td vAlign="top" height="17"><TABLE class="main" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<!--DWLayoutTable-->
										<tr>
											<td vAlign="top" width="790" bgColor="#f0f0f0" height="52"><div class="main" dir="rtl" style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px"
													align="center">
													<DIV dir="rtl" align="center"><SPAN lang="EN" style="FONT-SIZE: 8pt; COLOR: #757980; FONT-FAMILY: Tahoma"></SPAN></DIV>
													<div dir="rtl" style="PADDING-RIGHT: 0px; PADDING-LEFT: 1px; PADDING-BOTTOM: 2px; PADDING-TOP: 10px"
										align="center">کلیه حقوق این وب سایت متعلق به وب<span lang="en-us"></span>
										سایت دریا لایب می باشد<span lang="en-us">.</span>
										<p dir="ltr"><span lang="en-us">All rights reserved to ِBookStore <font face="Times New Roman">
													© 2009.</font></span></p>
									</div>
												</div>
											</td>
										</tr>
										<tr>
											<td vAlign="top" height="4"><IMG height="4" src="images/bg-top.gif" width="100%"></td>
										</tr>
										<tr>
											<td height="6"></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<!-- MSCellFormattingTableID="8" -->
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
