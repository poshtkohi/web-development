<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="news.Login" %>
<HTML>
	<HEAD>
		<title>ورود کاربران</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<link href="styles/main.css" rel="stylesheet" rev="stylesheet">
		<link href="styles/cp.css" rel="stylesheet" rev="stylesheet">
		<script language="javascript" src="/js/Login.js"></script>
      	<script src="/Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<table width="900" border="0" cellpadding="0" cellspacing="0" align="center">
			<!--DWLayoutTable-->
			<tr>
				<td width="24" rowspan="5" valign="top" background="images/bg_lft.gif"><!--DWLayoutEmptyCell-->&nbsp;
					
				</td>
				<td height="167" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF">
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
				<td width="26" rowspan="5" valign="top" background="images/bg.gif"><!--DWLayoutEmptyCell-->&nbsp;
					
				</td>
			</tr>
			<tr>
				<td height="32" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td>
								<div align="center" class="under"><img src="images/titr5.gif" width="119" height="35"></div>
						  </td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width="343" height="5" valign="top" bgcolor="#999999"></td>
				<td width="507" valign="top" bgcolor="#999999"></td>
			</tr>
			<tr>
				<td height="387" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td width="346" height="83" valign="top" bgcolor="#dbedff"><!--DWLayoutEmptyCell-->&nbsp;
								
							</td>
						</tr>
						<tr>
							<td><div class="pic"><!--DWLayoutEmptyCell--> &nbsp;</div>
							</td>
						</tr>
					</table>
				</td>
				<td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#dbedff">
						<!--DWLayoutTable-->
						<tr>
							<td height="387"><div class="Staple"><!--DWLayoutEmptyCell--> &nbsp;</div>
							</td>
							<td width="468" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#dbedff">
									<!--DWLayoutTable-->
									<tr>
										<td width="96" height="16"></td>
										<td width="168"></td>
										<td width="119"></td>
										<td width="85"></td>
									</tr>
									<tr>
										<td height="29"></td>
										<td></td>
										<td colspan="2" align="right" valign="middle" bgcolor="#dbedff"><!--DWLayoutEmptyCell-->&nbsp;
											
										</td>
									</tr>
									<tr>
										<td height="33">&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td height="225">&nbsp;</td>
										<td colspan="2" valign="top">
											<form runat="server" method="post" onSubmit="return LoginFormValidation();">
												<table width="100%" border="0" cellpadding="0" cellspacing="0" id="TableLogin">
													<!--DWLayoutTable-->
													<tr>
														<td width="13" height="25" valign="top" background="images/pbx-15.gif"><!--DWLayoutEmptyCell-->&nbsp;
															
														</td>
														<td colspan="2" valign="top" background="images/pbx-18.gif"><!--DWLayoutEmptyCell-->&nbsp;
															
														</td>
														<td width="13" valign="top" background="images/pbx-14.gif"><!--DWLayoutEmptyCell-->&nbsp;
															
														</td>
													</tr>
													<tr>
														<td rowspan="5" valign="top" background="images/pbx-left.gif"><!--DWLayoutEmptyCell-->&nbsp;
															
														</td>
														<td height="26" colspan="2" align="center" valign="top" bgcolor="#ffffff" dir="rtl">
													  <asp:Label id="message" runat="server" CssClass="message" Visible="False" Font-Size="XX-Small"
																BorderColor="Yellow"></asp:Label></td>
												  <td rowspan="5" valign="top" background="images/pbx-right.gif"><!--DWLayoutEmptyCell-->&nbsp;
															
														</td>
													</tr>
													<tr>
														<td width="167" height="26" valign="top" bgcolor="#ffffff"><input name="username" type="text" maxlength="50" border="1" class="txtBox" id="username"></td>
														<td width="94" valign="top" bgcolor="#ffffff"><div class="box">: نام کاربری <img src="images/flesh.gif" width="12" height="12"></div>
													  </td>
													</tr>
													<tr>
														<td height="26" valign="top" bgcolor="#ffffff"><input name="password" type="password" maxlength="50" border="1" class="txtBox" id="password"></td>
														<td valign="top" bgcolor="#ffffff"><div class="box">
																: کلمه عبور <img src="images/flesh.gif" width="12" height="12"></div>
													  </td>
													</tr>
													<tr>
														<td height="35" colspan="2" valign="bottom" bgcolor="#ffffff">
															<asp:ImageButton id="signin" runat="server" ImageUrl="images/login.gif" ToolTip="ورود"></asp:ImageButton></td>
													</tr>
													<tr>
														<td height="47" bgcolor="#ffffff">&nbsp;</td>
														<td bgcolor="#ffffff">&nbsp;</td>
													</tr>
													<tr>
														<td height="27" valign="top" background="images/pbx-17.gif"><!--DWLayoutEmptyCell-->&nbsp;
															
														</td>
														<td colspan="2" rowspan="2" valign="top" background="images/pbx-19.gif"><!--DWLayoutEmptyCell-->&nbsp;
															
														</td>
														<td valign="top" background="images/pbx-16.gif"><!--DWLayoutEmptyCell-->&nbsp; 
														</td>
													</tr>
													<tr>
														<td height="1"></td>
														<td></td>
													</tr>
												</table>
											</form>
										</td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td height="82">&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="47" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0"  background="images/obs.png">
        <!--DWLayoutTable-->
        <tr>
          <td width="497" height="61">&nbsp;</td>
            <td><!--DWLayoutEmptyCell-->&nbsp;</td>
          </tr>
        
      </table>
			  </td>
			</tr>
			<tr>
				<td height="25">&nbsp;</td>
				<td colspan="2" valign="top" background="images/b-b.gif"><!--DWLayoutEmptyCell-->&nbsp;
					
				</td>
				<td>&nbsp;</td>
			</tr>
		</table>
	</body>
</HTML>
