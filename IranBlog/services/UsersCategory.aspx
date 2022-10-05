<%@ Page Language="c#" Inherits="services.Migrated_UsersCategory" CodeFile="UsersCategory.aspx.cs" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
<meta http-equiv="Content-Language" content="fa">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="images/style.css" />
<title>کاربران ایران بلاگ</title>
</head>

<body>

<div align="center">
	<table border="0" width="798" cellspacing="0" cellpadding="0">
		<tr>
			<td height="103">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" height="100%" background="images/header-bg.gif">
				<tr>
					<td width="12">
					<img border="0" src="images/header-left.gif" width="12" height="103"></td>
					<td background="images/header-content.gif" class="header-content">
					<table border="0" width="100%" cellspacing="0" cellpadding="0" height="100%">
						<tr>
							<td align="left">
							<img border="0" src="images/iranblog-logo-en.gif" width="194" height="44" class="iranblog-header-logo-en"></td>
							<td align="right">
							<img border="0" src="images/iranblog-logo-fa.gif" width="225" height="54" class="iranblog-header-logo-fa"></td>
						</tr>
					</table>
					</td>
					<td align="right" width="14">
					<img border="0" src="images/header-right.gif" width="14" height="103"></td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td height="2">
			<img border="0" src="images/content-up.gif" width="798" height="2"></td>
		</tr>
		<tr>
			<td background="images/content-bg.gif">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" class="content">
				<tr>
					<td width="18">&nbsp;</td>
					<td dir="rtl">
					<table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/titr-bg.gif" style="margin-bottom: 2">
						<tr>
							<td width="10">&nbsp;</td>
							<td>                            
                            <asp:PlaceHolder ID="MainSiteHeaderControl" runat="server"></asp:PlaceHolder>
                                        </td>
							<td align="left" width="48">
							<img border="0" src="images/titr-left.gif" width="48" height="23"></td>
						</tr>
					</table>
					</td>
					<td width="18">&nbsp;</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td background="images/content-bg.gif">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" class="content">
				<tr>
					<td width="18">&nbsp;</td>
					<td dir="rtl">
					<table border="0" width="100%" cellspacing="0" cellpadding="0">
						<tr>
							<td valign="top">
							<div align="center">
								<table border="0" width="759" cellspacing="0" cellpadding="0" background="images/text-p-bg.gif">
									<tr>
										<td>
										<table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/text-t-bg.gif" style="margin-right: 10">
											<tr>
												<td align="right" width="31">
												<img border="0" src="images/text-t-right.gif" width="31" height="25"></td>
												<td><div class="updates">کاربران ایران بلاگ</div></td>
												<td width="5">&nbsp;</td>
											</tr>
										</table>
										</td>
									</tr>
									<tr>
										<td>
										<img border="0" src="images/text-p-h.gif" width="759" height="8"></td>
									</tr>
									<tr>
										<td>
										<table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td height="21" colspan="2" align="right" valign="top" class="main" dir="rtl">
								در این صفحه میتوانید لیست کلیه گروههای کاربران در ایران بلاگ را ببینید .</td>
						</tr>
						<tr>
						  <td height="300" align="center" valign="middle" class="main" dir="ltr" va>						    <DIV id="Layer1" style="SCROLLBAR-FACE-COLOR: #b44f01; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: #000000; SCROLLBAR-HIGHLIGHT-COLOR: #ccff00; OVERFLOW: auto; WIDTH: 270px; SCROLLBAR-SHADOW-COLOR: #ccff00; COLOR: #ccff00; DIRECTION: rtl; BORDER-TOP-COLOR: #000000; SCROLLBAR-ARROW-COLOR: #ffffff; FONT-FAMILY: Tahoma; SCROLLBAR-BASE-COLOR: #ccff00; HEIGHT: 480px; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: #000000; scrollbar-3d-light-color: #ccff00; scrollbar-dark-shadow-color: #ccff00"
															align="justify">
															<DIV align="center">
																<TABLE id="Main" height="300" cellSpacing="0" cellPadding="0" width="270"
																	border="0">
																	<!--DWLayoutTable-->
																	<TR>
																		<TD width="270" height="500" valign="top"><div align="right" style=" MARGIN-RIGHT:1px">
																				<div align="right" style="MARGIN-TOP:20px;FONT-FAMILY:Tahoma"><%db.CategoryUsers(this);%></div>
																			</div>
																		</TD>
																	</TR>
																</TABLE>
															</DIV>
														</DIV></td>
						</tr>
					</table>
									  </td>
									</tr>
									<tr>
										<td>
										<img border="0" src="images/text-p-f.gif" width="759" height="7"></td>
									</tr>
								</table>
							</div>
							</td>
						</tr>
					</table>
					<p>&nbsp;</td>
					<td width="18">&nbsp;</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td background="images/content-bg.gif" align="center">
			<font face="Verdana"><span style="font-variant: small-caps"><asp:PlaceHolder ID="CopyrightFooterControl" runat="server"></asp:PlaceHolder></span></font></td>
		</tr>
		<tr>
			<td background="images/content-bg.gif">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/footer-bg.gif">
				<tr>
					<td align="left" width="19">
					<img border="0" src="images/footer-left.gif" width="19" height="19"></td>
					<td align="center">&nbsp;</td>
					<td align="right" width="18">
					<img border="0" src="images/footer-right.gif" width="18" height="19"></td>
				</tr>
			</table>
			</td>
		</tr>
	</table>
</div>

</body>

</html>