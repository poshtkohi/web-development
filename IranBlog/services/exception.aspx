<%@ Page Language="c#" Inherits="services.exception" CodeFile="exception.aspx.cs" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<HTML>
	<HEAD>
		<title>**IRANBLOG**(جزییات خطای سرور)</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="The Greatest Weblog Technology" name="description">
		<meta content="iran,iranian,iranblog,iran blog,persian,persia,persian blog,persianblog,weblog,web,blog,forum,netwoking,network software,software,search,microsoftbox,microsoft box,microsoft,C#,ASP.NET,VC++,.NET"
			name="keywords">
		<meta content="Alireza Poshtkoohi" name="author">
		<meta content="Alireza Poshtkoohi" name="coyright">
		<meta content="Microsoft FrontPage 5.0" name="generator">
		<style>
.txtBoxStyleLogin { BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: #000000; COLOR: #990000; BORDER-TOP-COLOR: #000000; BACKGROUND-COLOR: #ffffff; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: #000000 }
A.links { COLOR: #000095; TEXT-DECORATION: none }
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none }
BODY { SCROLLBAR-FACE-COLOR: #b44f01; SCROLLBAR-HIGHLIGHT-COLOR: #f5700a; SCROLLBAR-SHADOW-COLOR: #cc0000; SCROLLBAR-ARROW-COLOR: #ffffff; SCROLLBAR-BASE-COLOR: #b44f01; BACKGROUND-COLOR: #ffffff; scrollbar-3d-light-color: #B44F01; scrollbar-dark-shadow-color: #B44F01 }
        </style>
		<script language="javascript" type="text/javascript">
			/* All rights reserved to Mr. Alireza Poshtkohi (C) 2002-2007. alireza.poshtkohi@gmail.com */
			//---------------------------------------------------------------------------------------------------------------------------
				function BlankField(field, text)
				{
				    if(field.value == "")
					{
					    alert(text);
						field.focus();
						return false;
					}
					else return true;
				}
				//-------------------------------------------
				function ValidateUsername(field)
				{
				   if(!BlankField(field ,".فیلد نام کاربری خالی است"))
				          return false;
				   else
				   {
				          var re = /^[\-0-9a-zA-Z]{1,}$/
				          if(re.test(field.value))
					            return true;
					      else
						  {
						        alert(".حروف فیلد نام کاربری نامعتبر است");
						        field.focus();
						        field.select();
						        return false;
						  }
					}
				}
				//-------------------------------------------
				function ValidatePassword(field)
				{
				   if(!BlankField(field,".فیلد کلمه عبور خالی است"))
				          return false;
				   else return true;
				}
				//-------------------------------------------
				function ValidateForm(form)
				{
					if(!ValidateUsername(form.username))
					      return false;
				    if(!ValidatePassword(form.password))
					      return false;
					else return true;
				}
				//------------------------------------------
				function LinksWindow()
				{
				    if (screen) {
				     leftpos = screen.width / 2 -440;					 
					 toppos = screen.height / 2 - 350;
					 }
				    linksWindow = window.open("/toplink.aspx", "linksWindow", "width=800,height=600,toolbar=1,location=1,directories=1,status=1,menubar=1,scrollbars=1,resizable=1,left="+leftpos+",top="+toppos);
					linksWindow.blur();
					self.focus();
					return true;
				}
				//LinksWindow();
				//---------------------------------------------------------------------------------------------------------------------------
		</script>
	</HEAD>
	<BODY BGCOLOR="#ffffff" LEFTMARGIN="0" TOPMARGIN="0" MARGINWIDTH="0" MARGINHEIGHT="0">
		<TABLE WIDTH="775" BORDER="0" CELLPADDING="0" CELLSPACING="0" align="center">
			<!--DWLayoutTable-->
			<TR>
				<TD COLSPAN="10">
					<IMG SRC="/images/main_01.jpg" ALT="" WIDTH="775" HEIGHT="26" border="0" usemap="#Map2"></TD>
			</TR>
			<TR>
				<TD height="144" COLSPAN="8">
					<IMG SRC="/images/main_02.jpg" WIDTH="515" HEIGHT="144" ALT=""></TD>
				<TD COLSPAN="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td width="260" height="144" valign="top">
								<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
									height="131" width="260" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
									<PARAM NAME="_cx" VALUE="6879">
									<PARAM NAME="_cy" VALUE="3466">
									<PARAM NAME="FlashVars" VALUE="">
									<PARAM NAME="Movie" VALUE="/swf/main.swf">
									<PARAM NAME="Src" VALUE="/swf/main.swf">
									<PARAM NAME="WMode" VALUE="Window">
									<PARAM NAME="Play" VALUE="-1">
									<PARAM NAME="Loop" VALUE="-1">
									<PARAM NAME="Quality" VALUE="High">
									<PARAM NAME="SAlign" VALUE="">
									<PARAM NAME="Menu" VALUE="-1">
									<PARAM NAME="Base" VALUE="">
									<PARAM NAME="AllowScriptAccess" VALUE="always">
									<PARAM NAME="Scale" VALUE="ShowAll">
									<PARAM NAME="DeviceFont" VALUE="0">
									<PARAM NAME="EmbedMovie" VALUE="0">
									<PARAM NAME="BGColor" VALUE="">
									<PARAM NAME="SWRemote" VALUE="">
									<PARAM NAME="MovieData" VALUE="">
									<embed src="/swf/main.swf" quality="high" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
										type="application/x-shockwave-flash" width="260" height="131"> </embed></OBJECT>
							</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD COLSPAN="10">
					<IMG SRC="/images/main_04.jpg" ALT="" WIDTH="775" HEIGHT="38" border="0" usemap="#Map"></TD>
			</TR>
			<TR>
				<TD height="14" COLSPAN="7">
					<IMG SRC="/images/main_05.jpg" WIDTH="210" HEIGHT="14" ALT=""></TD>
				<TD COLSPAN="2" ROWSPAN="9">
					<IMG SRC="/images/main_06.jpg" WIDTH="362" HEIGHT="163" ALT=""></TD>
				<TD width="203" ROWSPAN="6" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" background="/images/main_07.jpg">
						<!--DWLayoutTable-->
						<tr>
							<td width="21" height="26">&nbsp;</td>
							<td width="168">&nbsp;</td>
							<td width="14">&nbsp;</td>
						</tr>
						<tr>
							<td height="45">&nbsp;</td>
							<td valign="top">
								<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
									height="43" width="167" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
									<PARAM NAME="_cx" VALUE="4419">
									<PARAM NAME="_cy" VALUE="1138">
									<PARAM NAME="FlashVars" VALUE="">
									<PARAM NAME="Movie" VALUE="/swf/tab.swf">
									<PARAM NAME="Src" VALUE="/swf/tab.swf">
									<PARAM NAME="WMode" VALUE="Window">
									<PARAM NAME="Play" VALUE="-1">
									<PARAM NAME="Loop" VALUE="-1">
									<PARAM NAME="Quality" VALUE="High">
									<PARAM NAME="SAlign" VALUE="">
									<PARAM NAME="Menu" VALUE="-1">
									<PARAM NAME="Base" VALUE="">
									<PARAM NAME="AllowScriptAccess" VALUE="always">
									<PARAM NAME="Scale" VALUE="ShowAll">
									<PARAM NAME="DeviceFont" VALUE="0">
									<PARAM NAME="EmbedMovie" VALUE="0">
									<PARAM NAME="BGColor" VALUE="">
									<PARAM NAME="SWRemote" VALUE="">
									<PARAM NAME="MovieData" VALUE="">
									<PARAM NAME="_ExtentX" VALUE="4419">
									<PARAM NAME="_ExtentY" VALUE="1138">
									<PARAM NAME="SeamlessTabbing" VALUE="1">
									<PARAM NAME="Profile" VALUE="0">
									<PARAM NAME="ProfileAddress" VALUE="">
									<PARAM NAME="ProfilePort" VALUE="0">
									<PARAM NAME="AllowNetworking" VALUE="all">
									<embed src="/swf/tab.swf" quality="high" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
										type="application/x-shockwave-flash" width="167" height="43"> </embed></OBJECT>
							</td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td height="44">&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD ROWSPAN="7">
					<IMG SRC="/images/main_08.jpg" WIDTH="5" HEIGHT="128" ALT=""></TD>
				<TD COLSPAN="2" ROWSPAN="4">
					<IMG SRC="/images/main_09.jpg" WIDTH="52" HEIGHT="83" ALT=""></TD>
				<TD height="33" COLSPAN="4">
					<IMG SRC="/images/main_10.jpg" WIDTH="153" HEIGHT="33" ALT=""></TD>
			</TR>
			<form id="signin" name="signin" method="post" onSubmit="return ValidateForm(this);" runat="server">
				<TR>
					<TD width="90" ROWSPAN="3" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#d1630a">
							<!--DWLayoutTable-->
							<tr>
								<td width="90" height="24" valign="top">
									<asp:TextBox id="username" Height="20" Width="90" CssClass="txtBoxStyleLogin" MaxLength="12"
										runat="server"></asp:TextBox></td>
							</tr>
							<tr>
								<td height="26" valign="top">
									<asp:TextBox id="password" Height="20" Width="90" CssClass="txtBoxStyleLogin" MaxLength="12"
										runat="server" TextMode="Password">
                                    </asp:TextBox></td>
							</tr>
						</table>
					</TD>
					<TD height="25" COLSPAN="3">
						<IMG SRC="/images/main_12.jpg" WIDTH="63" HEIGHT="25" ALT=""></TD>
				</TR>
				<TR>
					<TD width="7" ROWSPAN="2">
						<IMG SRC="/images/main_13.jpg" WIDTH="7" HEIGHT="25" ALT=""></TD>
					<TD width="50" height="20">
						<asp:ImageButton id="LoginSubmit" runat="server" ImageUrl="/images/main_14.jpg">
                        </asp:ImageButton></TD>
					<TD ROWSPAN="2">
						<IMG SRC="/images/main_15.jpg" WIDTH="6" HEIGHT="25" ALT=""></TD>
				</TR>
			</form>
			<TR>
				<TD height="5">
					<IMG SRC="/images/main_16.jpg" WIDTH="50" HEIGHT="5" ALT=""></TD>
			</TR>
			<TR>
				<TD height="18">
					<IMG SRC="/images/main_17.jpg" WIDTH="5" HEIGHT="18" ALT=""></TD>
				<TD COLSPAN="5">
					<a href="/services/ForgottenPassword.aspx"><img src="/images/main_18.jpg" alt="" width="200" height="18" border="0"></a></TD>
			</TR>
			<TR>
				<TD height="18" COLSPAN="6" background="/images/main_19.jpg" align="center" valign="middle">
					<%
		   //------------------------------------------------------------------------------------------------
		   string i = this.Request.QueryString["i"];
		   if(i != null && i != "")
		   {
		      if(i == "logouted" || i == "userlogout" ||i == "unauthorized")
			  {
		         string error = "";
		         switch(i)
			     {
			         case "logouted":
				         error = ".جلسه شما منقضی شده است";
					     break;
					 case "userlogout":
					     error = ".شدید LogOut شما با موفقیت";
						 break;
				     case "unauthorized":
				         error = ".نام کاربری یا کلمه عبور اشتباه است";
						 break;
			     }
		         this.Response.Write("<div style=\"font-size:9px;font-family:Tahoma; color:#FFFF00\">" + error + "</div>");
			  }
		   }
		   //------------------------------------------------------------------------------------------------
		%>
				</TD>
				<TD ROWSPAN="6" valign="top">
					<table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td width="203" height="60" valign="middle" align="center">
								<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
									height="68" width="190" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
									<PARAM NAME="_cx" VALUE="5027">
									<PARAM NAME="_cy" VALUE="1799">
									<PARAM NAME="FlashVars" VALUE="">
									<PARAM NAME="Movie" VALUE="/swf/netsoftm.swf">
									<PARAM NAME="Src" VALUE="/swf/netsoftm.swf">
									<PARAM NAME="WMode" VALUE="Window">
									<PARAM NAME="Play" VALUE="-1">
									<PARAM NAME="Loop" VALUE="-1">
									<PARAM NAME="Quality" VALUE="High">
									<PARAM NAME="SAlign" VALUE="">
									<PARAM NAME="Menu" VALUE="-1">
									<PARAM NAME="Base" VALUE="">
									<PARAM NAME="AllowScriptAccess" VALUE="always">
									<PARAM NAME="Scale" VALUE="ShowAll">
									<PARAM NAME="DeviceFont" VALUE="0">
									<PARAM NAME="EmbedMovie" VALUE="0">
									<PARAM NAME="BGColor" VALUE="">
									<PARAM NAME="SWRemote" VALUE="">
									<PARAM NAME="MovieData" VALUE="">
									<PARAM NAME="SeamlessTabbing" VALUE="1">
									<PARAM NAME="Profile" VALUE="0">
									<PARAM NAME="ProfileAddress" VALUE="">
									<PARAM NAME="ProfilePort" VALUE="0">
									<PARAM NAME="AllowNetworking" VALUE="all">
									<embed src="/swf/netsoftm.swf" quality="high" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
										type="application/x-shockwave-flash" width="190" height="68"> </embed></OBJECT>
							</td>
						</tr>
						<tr>
							<td height="63" valign="middle" align="center">
								<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
									height="68" width="190" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
									<PARAM NAME="_cx" VALUE="5027">
									<PARAM NAME="_cy" VALUE="1799">
									<PARAM NAME="FlashVars" VALUE="">
									<PARAM NAME="Movie" VALUE="/swf/resalat.swf">
									<PARAM NAME="Src" VALUE="/swf/resalat.swf">
									<PARAM NAME="WMode" VALUE="Window">
									<PARAM NAME="Play" VALUE="-1">
									<PARAM NAME="Loop" VALUE="-1">
									<PARAM NAME="Quality" VALUE="High">
									<PARAM NAME="SAlign" VALUE="">
									<PARAM NAME="Menu" VALUE="-1">
									<PARAM NAME="Base" VALUE="">
									<PARAM NAME="AllowScriptAccess" VALUE="always">
									<PARAM NAME="Scale" VALUE="ShowAll">
									<PARAM NAME="DeviceFont" VALUE="0">
									<PARAM NAME="EmbedMovie" VALUE="0">
									<PARAM NAME="BGColor" VALUE="">
									<PARAM NAME="SWRemote" VALUE="">
									<PARAM NAME="MovieData" VALUE="">
									<PARAM NAME="SeamlessTabbing" VALUE="1">
									<PARAM NAME="Profile" VALUE="0">
									<PARAM NAME="ProfileAddress" VALUE="">
									<PARAM NAME="ProfilePort" VALUE="0">
									<PARAM NAME="AllowNetworking" VALUE="all">
									<embed src="/swf/resalat.swf" quality="high" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
										type="application/x-shockwave-flash" width="190" height="68"> </embed></OBJECT>
							</td>
						</tr>
						<tr>
							<td height="67" valign="top"><!--DWLayoutEmptyCell--> &nbsp;
							</td>
						</tr>
						<tr>
							<td height="72" valign="top"><!--DWLayoutEmptyCell--> &nbsp;
							</td>
						</tr>
						<tr>
							<td height="66" valign="top">&nbsp;</td>
						</tr>
						<tr>
							<td height="70" valign="top">&nbsp;</td>
						</tr>
						<tr>
							<td height="71" valign="top">&nbsp;</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD height="9" COLSPAN="6">
					<IMG SRC="/images/main_21.jpg" WIDTH="205" HEIGHT="9" ALT=""></TD>
			</TR>
			<TR>
				<TD COLSPAN="2" ROWSPAN="4">&nbsp;
				</TD>
				<TD height="21" COLSPAN="4">
					&nbsp;</TD>
				<TD ROWSPAN="4">
					<IMG SRC="/images/main_24.jpg" WIDTH="6" HEIGHT="442" ALT=""></TD>
			</TR>
			<TR>
				<TD height="188" COLSPAN="4" valign="top">
					<table width="100%" border="0" cellpadding="0" cellspacing="0" id="table1">
						<!--DWLayoutTable-->
						<tr>
							<td width="194" height="188" valign="top">
								&nbsp;</td>
						</tr>
					</table>
				</TD>
				<TD COLSPAN="2" ROWSPAN="3" valign="top">
					<table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td width="362" height="375" valign="middle" align="center">
								<strong>Error Details :
									<%=this.Request.QueryString["error"]%>
								</strong>
							</td>
						</tr>
						<tr>
							<td height="46" valign="middle" align="center">
								<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
									height="35" width="120" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
									<PARAM NAME="_cx" VALUE="3175">
									<PARAM NAME="_cy" VALUE="926">
									<PARAM NAME="FlashVars" VALUE="">
									<PARAM NAME="Movie" VALUE="/swf/netsoftmix.swf">
									<PARAM NAME="Src" VALUE="/swf/netsoftmix.swf">
									<PARAM NAME="WMode" VALUE="Window">
									<PARAM NAME="Play" VALUE="-1">
									<PARAM NAME="Loop" VALUE="-1">
									<PARAM NAME="Quality" VALUE="High">
									<PARAM NAME="SAlign" VALUE="">
									<PARAM NAME="Menu" VALUE="-1">
									<PARAM NAME="Base" VALUE="">
									<PARAM NAME="AllowScriptAccess" VALUE="always">
									<PARAM NAME="Scale" VALUE="ShowAll">
									<PARAM NAME="DeviceFont" VALUE="0">
									<PARAM NAME="EmbedMovie" VALUE="0">
									<PARAM NAME="BGColor" VALUE="">
									<PARAM NAME="SWRemote" VALUE="">
									<PARAM NAME="MovieData" VALUE="">
									<PARAM NAME="SeamlessTabbing" VALUE="1">
									<PARAM NAME="Profile" VALUE="0">
									<PARAM NAME="ProfileAddress" VALUE="">
									<PARAM NAME="ProfilePort" VALUE="0">
									<PARAM NAME="AllowNetworking" VALUE="all">
									<embed src="/swf/netsoftmix.swf" quality="high" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
										type="application/x-shockwave-flash" width="120" height="35"> </embed></OBJECT>
							</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD COLSPAN="4">
					<IMG SRC="/images/main_27.jpg" WIDTH="194" HEIGHT="19" ALT=""></TD>
			</TR>
			<TR>
				<TD height="214" COLSPAN="4" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td width="194" height="214" valign="top"><div style="FONT-SIZE:x-small;FONT-FAMILY:Tahoma;TEXT-ALIGN:right;TEXT-DECORATION:none">
									<%db.UpdatedRecords(this);%>
								</div>
							</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD COLSPAN="10">
					<img border="0" src="http://www.iranblog.com/images/main_29.gif" width="768" height="31"></TD>
			</TR>
			<TR>
				<TD>
					<IMG SRC="/images/spacer.gif" WIDTH="5" HEIGHT="1" ALT=""></TD>
				<TD>
					<IMG SRC="/images/spacer.gif" WIDTH="5" HEIGHT="1" ALT=""></TD>
				<TD width="47">
					<IMG SRC="/images/spacer.gif" WIDTH="47" HEIGHT="1" ALT=""></TD>
				<TD>
					<IMG SRC="/images/spacer.gif" WIDTH="90" HEIGHT="1" ALT=""></TD>
				<TD>
					<IMG SRC="/images/spacer.gif" WIDTH="7" HEIGHT="1" ALT=""></TD>
				<TD>
					<IMG SRC="/images/spacer.gif" WIDTH="50" HEIGHT="1" ALT=""></TD>
				<TD>
					<IMG SRC="/images/spacer.gif" WIDTH="6" HEIGHT="1" ALT=""></TD>
				<TD width="305">
					<IMG SRC="/images/spacer.gif" WIDTH="305" HEIGHT="1" ALT=""></TD>
				<TD width="57">
					<IMG SRC="/images/spacer.gif" WIDTH="57" HEIGHT="1" ALT=""></TD>
				<TD>
					<IMG SRC="/images/spacer.gif" WIDTH="203" HEIGHT="1" ALT=""></TD>
			</TR>
		</TABLE>
		<map name="Map">
			<area shape="RECT" coords="562,9,665,31" href="/services/register.aspx">
			<area shape="RECT" coords="498,8,549,32" href="#news.aspx">
			<area shape="RECT" coords="393,8,485,31" href="UsersList.aspx">
			<area shape="RECT" coords="279,9,384,34" href="#">
			<area shape="RECT" coords="186,8,263,31" href="#" target="_blank">
			<area shape="RECT" coords="117,8,168,31" href="about.aspx">
			<area shape="RECT" coords="682,9,768,32" href="/services/">
		</map><map name="Map2">
			<area shape="RECT" coords="624,0,697,27" href="http://www.coolng.com/" target="_blank">
			<area shape="RECT" coords="699,0,774,24" href="#" target="_blank">
		</map>
	</BODY>
</HTML>