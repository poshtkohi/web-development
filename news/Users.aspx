<%@ Page language="c#" Codebehind="Users.aspx.cs" AutoEventWireup="false" Inherits="news.Users" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="Controls/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LoginStatusControl" Src="Controls/LoginStatusControl.ascx" %>
<HTML>
	<HEAD>
		<title>مدیریت کاربران</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<link href="styles/main.css" rel="stylesheet" rev="stylesheet">
		<link href="styles/cp.css" rel="stylesheet" rev="stylesheet">
		<script language="javascript" src="/js/AjaxCore.js"></script>
        <script language="javascript" src="/js/Functions.js"></script>
        <script src="/Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<table width="900" border="0" cellpadding="0" cellspacing="0" align="center">
			<!--DWLayoutTable-->
			<tr>
				<td width="23" rowspan="5" valign="top" background="images/bg_lft.gif"><!--DWLayoutEmptyCell-->&nbsp; 
					</td>
				<td height="167" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF">
						<!--DWLayoutTable-->
		  <tr>
							<td><div>
							  <!--DWLayoutEmptyCell-->
                              <script type="text/javascript">
AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','525','height','170','src','swf/logo','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','swf/logo' ); //end AC code
                            </script>
							  <noscript>
							    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="525" height="170">
                                <param name="movie" value="swf/logo.swf">
                                <param name="quality" value="high">
                                <embed src="swf/logo.swf" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="525" height="170"></embed>
                              </object>
                              </noscript>
		      </div></td>
						  <td width="73" valign="top" bgcolor="#DBEDFF"><!--DWLayoutEmptyCell-->&nbsp; </td>
							<td><div class="header_right"><!--DWLayoutEmptyCell--></div>
							</td>
						</tr>
					</table>
				</td>
				<td width="25" rowspan="5" valign="top" background="images/bg.gif"><!--DWLayoutEmptyCell-->&nbsp; 
					</td>
			</tr>
			<tr>
				<td height="32" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td>
								<div align="center" class="under"><img src="images/titr3.gif" align="middle">&nbsp;</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="5" valign="top" bgcolor="#999999"></td>
			</tr>
			<tr>
				<td height="387" valign="top"><table width="100%" height="590" border="0" cellpadding="0" cellspacing="0" bgcolor="#dbedff">
						<div id="message" class="message" style="DISPLAY:none"></div>
                        <tr>
                        <td width="100%" align="right" valign="middle" class="box"><uc1:LoginStatusControl id="LoginStatusControl" runat="server"></uc1:LoginStatusControl></td>
                        </tr>
						<!--DWLayoutTable-->
						<tr style="DISPLAY:none" id="up">
							<form id="form" onSubmit="return false;">
								<td width="852" height="204" valign="top" bgcolor="#dbedff">
									<table width="100%" border="0" cellpadding="0" cellspacing="0">
										<!--DWLayoutTable-->
										<tr>
											<td width="127" height="27"></td>
											<td width="13"></td>
											<td width="615">&nbsp;</td>
											<td width="13"></td>
											<td width="84"></td>
										</tr>
										<tr>
											<td height="33">&nbsp;</td>
											<td valign="top" background="images/pbx-15.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top" background="images/pbx-18.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top" background="images/pbx-14.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td height="117">&nbsp;</td>
											<td valign="top" background="images/pbx-left.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top">
												<table border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff" style="WIDTH: 615px; HEIGHT: 196px">
													<!--DWLayoutTable-->
													<tr>
														<td height="37" colspan="5" align="right" valign="top"><span style="HEIGHT: 12px"><span class="box">تعریف کاربر جدید  و یا ویرایش کاربر انتخاب شده</span>
																<img src="images/pbx-30.gif"></span>
															<table border="0" cellpadding="0" cellspacing="0">
																<!--DWLayoutTable-->
															</table>
														</td>
													</tr>
													<tr>
														<td valign="top"><!--DWLayoutEmptyCell-->&nbsp; </td>
														<td colspan="2" align="center" valign="middle">
															<input name="username" type="text" maxlength="50" border="1" class="txtBox" id="username">
														</td>
														<td colspan="2" align="left" valign="middle" height="24"><div class="box">: نام کاربری</div>
														</td>
													</tr>
													<tr>
														<td valign="top"><!--DWLayoutEmptyCell-->&nbsp; </td>
														<td colspan="2" align="center" valign="middle">
															<input name="password" type="password" maxlength="50" border="1" class="txtBox" id="password">
														</td>
														<td colspan="2" align="left" valign="middle" height="24"><div class="box">
																: کلمه عبور</div>
														</td>
													</tr>
													<tr>
														<td valign="top"><!--DWLayoutEmptyCell-->&nbsp; </td>
														<td colspan="2" align="center" valign="middle"><input name="ConfirmPassword" type="password" maxlength="50" border="1" class="txtBox"
																id="ConfirmPassword"></td>
														<td colspan="2" align="left" valign="middle" height="22"><div class="box">
																: تایید کلمه عبور</div>
														</td>
													</tr>
													<tr>
														<td valign="top"><!--DWLayoutEmptyCell-->&nbsp; </td>
														<td colspan="2" align="center" valign="middle"><label>
																<select name="AccountType" class="txtBox" id="AccountType" style="WIDTH:140px">
																	<option value="none" selected>.انتخاب کنید</option>
																	<option value="1">مدیر اصلی</option>
																	<option value="2">سر دبیر</option>
																	<option value="3">دبیر خبر</option>
																</select>
															</label></td>
														<td colspan="2" align="left" valign="middle" height="25"><div class="box">
																: نوع دسترسی</div>
														</td>
													</tr>
													<tr>
														<td align="right" valign="middle" width="189"></td>
														<td width="32">&nbsp;</td>
														<td align="left" valign="middle" width="177"><div style="CURSOR:pointer"><img src="images/b_2.gif" onClick="DoPost('users');" id="post" name="post" title="ذخیره"></div>
														</td>
														<td width="33">&nbsp;</td>
														<td width="184" height="51">&nbsp;</td>
													</tr>
												</table>
											</td>
											<td valign="top" background="images/pbx-right.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td><!--DWLayoutEmptyCell-->&nbsp; </td>
										</tr>
										<tr>
											<td height="27"></td>
											<td valign="top" background="images/pbx-17.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top" background="images/pbx-19.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top" background="images/pbx-16.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td></td>
										</tr>
										<tr>
											<td height="39"></td>
											<td></td>
											<td></td>
											<td></td>
											<td></td>
										</tr>
									</table>
								</td>
							</form>
						</tr>
						<tr>
							<td valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#dbedff" id="down">
									<!--DWLayoutTable-->
									<tr>
										<td height="19" colspan="2"><input class="oBtn" title="تعریف کاربر جدید" style="WIDTH: 209px" type="button" value="تعریف کاربر جدید "
												onClick="New('users');" id="new" name="new"> <input id="cancel" class="oBtn" style="DISPLAY: none; HEIGHT: 23px" type="button" value="انصراف و بازگشت"
												onClick="Cancel();">
										</td>
										<td width="56">&nbsp;</td>
									</tr>
									<tr>
										<td width="35" height="276">&nbsp;</td>
										<td width="761" valign="top">
											<div id="loaderImg" align="center"></div>
											<div id="resultText"></div>
											<script language="javascript">
				ShowItems('1', 'ShowUsers');
											</script>
										</td>
										<td>&nbsp;</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="61" valign="top"><uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
				</td>
			</tr>
			<tr>
				<td height="25">&nbsp;</td>
				<td valign="top" background="images/b-b.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
				<td>&nbsp;</td>
			</tr>
		</table>
		</body>
</HTML>
