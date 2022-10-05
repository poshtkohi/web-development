<%@ Reference Page="~/exception.aspx" %>
<%@ Page language="c#" Inherits="services.blogbuilderv1.exception" CodeFile="exception.aspx.cs" %>
<html>
<head>
<meta http-equiv="Content-Language" content="fa">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<LINK href="v3.css" type="text/css" rel="stylesheet">
<title>جزییات خطای سرور</title>
<style>
<!--
.txtBoxStyleLogin{border-color:#000000; border-width:1px; background-color:#FFFFFF; color:#990000}
A.links {
	color: #000095; text-decoration: none;font-family:Tahoma
}
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; text-decoration:none}
BODY {background-color: #FFFFFF;SCROLLBAR-FACE-COLOR: #B44F01; SCROLLBAR-HIGHLIGHT-COLOR: #F5700A; SCROLLBAR-SHADOW-COLOR: #cc0000; SCROLLBAR-ARROW-COLOR: #ffffff; SCROLLBAR-BASE-COLOR: #B44F01; scrollbar-3d-light-color: #B44F01; scrollbar-dark-shadow-color: #B44F01 }
.style11 {
	FONT-WEIGHT: bold;
	COLOR: #FFFFFF;
	FONT-FAMILY: Tahoma;
	font-size: 12px;
}
.style16 { FONT-SIZE: 12px; COLOR: #ffffff; FONT-FAMILY: Georgia, "Times New Roman", Times, serif }
.style12 {FONT-FAMILY: Tahoma; font-size: x-small;}
.style13 {
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
	color: #FFFFFF;
}
-->
</style>
</head><body>
<table width="775" border="0" cellpadding="0" cellspacing="0" align="center">
  <!--DWLayoutTable-->
  <tr>
    <td height="171" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
      <!--DWLayoutTable-->
      <tr>
        <td width="775" height="27" valign="top">
        <img src="http://www.iranblog.com/images/main_01.jpg" width="775" height="26" border="0" usemap="#Map"></td>
      </tr>
      <tr>
        <td height="139" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          <!--DWLayoutTable-->
          <tr>
            <td width="430" height="144" valign="top"><img src="/images/main_02.jpg" width="515" height="144"></td>
            <td width="260" valign="top"><object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="262" height="131">
              <param name="movie" value="/swf/main.swf">
              <param name=quality value=high>
              <embed src="/swf/main.swf" quality=high pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="262" height="131"></embed></object></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="39" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
        <!--DWLayoutTable-->
        <tr>
          <td width="614" height="38" valign="top" background="/images/component_04.jpg"><img style="margin-top:7px; margin-left:10px" src="/images/folder_open.gif" width="16" height="16"> <span class="style16">&nbsp;<span style="margin-top:-8px ">Internal Server Error </span></span></td>
        <td width="163" valign="middle" bordercolor="#000000" bgcolor="#FFFFFF"><div align="center"><a href="?i=logout" title="Logout" target="_self" class="style20 style19" style="TEXT-DECORATION:none;"><strong>خروج از سایت</strong></a></div></td>
        </tr>
        <tr>
          <td height="1" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <!--DWLayoutTable-->
              <tr>
                <td width="777" height="1"></td>
              </tr>
          </table></td>
        </tr>
            </table></td>
  </tr>
  <tr>
    <td width="559" height="38">&nbsp;</td>
  <td width="218" rowspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
    <!--DWLayoutTable-->
    <tr>
      <td width="218" height="351" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <!--DWLayoutTable-->
              <tr>
                <td width="16" height="39">&nbsp;</td>
              <td width="202">&nbsp;</td>
              </tr>
              <tr>
                <td height="294"></td>
                <td valign="top" id="iranblog_menu">
											<DIV class="v3butsubmenu_selected">
												ابزار های ایران بلاگ <img src="loc_icon.gif" width="9" height="9"></DIV>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on';&#13;&#10;&#9;&#9;  "
													onclick="document.location.href='/services/blogbuilderv1/home.aspx';" onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/home.aspx" target="_self">
														صفحه اصلی </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on';&#13;&#10;&#9;&#9;  "
													onclick="document.location.href='/services/blogbuilderv1/template.aspx';" onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/template.aspx" target="_self">
														انتخاب قالب </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/BasicEditor/basicPostAdmin.aspx" target="_self">
														ارسال مطلب </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/links.aspx" target="_self">
														پیوند های روزانه </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/linkss.aspx" target="_self">لینکستان وبلاگ</a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/settings.aspx" target="_self">
														تنظیمات </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on';" onclick="document.location.href='/about/contactus.aspx';"
													onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/update.aspx" target="_self">
														تغییر مشخصات </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on';" onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/delete.aspx" target="_self">
														حذف عضویت </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on';" onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/BasicEditor/LastPostEdit.aspx" target="_self">
														متن های قبلی </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on';" onmouseout="this.className='v3butsubmenu_off';"><a href="/services/blogbuilderv1/EditTemplate.aspx" target="_self">
														ویرایش قالب </a>
												</DIV>
											</div>
											<div class="v3butsubmenu_end">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on';" onmouseout="this.className='v3butsubmenu_off';"><a style="FONT-FAMILY:Tahoma" href='http://<%=(string)Session["subdomain"]%>.iranblog.com/' title="useful links related with MERDCI " target="_blank">
														نمایش وبلاگ </a>
												</DIV>
											</div>
											<DIV class="v3butsubmenu_selected">
												پیوند های ایران بلاگ <img src="loc_icon.gif" width="9" height="9"></DIV>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="/services/" target="_blank">
														صفحه اصلی </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="http://admin.iranblog.com/" target="_blank">
														اخبار ایران بلاگ </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="/services/UsersList.aspx" target="_blank">
														فهرست کاربران </a>
												</DIV>
											</div>
											<div class="v3butsubmenu">
												<DIV class="v3butsubmenu_off" onmouseover="this.className='v3butsubmenu_on'; " onmouseout="this.className='v3butsubmenu_off';"><a href="/services/about.aspx" target="_blank">
														درباره ما </a>
												</DIV>
											</div>
										</td>
              </tr>
              <tr>
                <td height="18"></td>
                <td></td>
              </tr>
      </table></td>
    </tr>
    <tr>
      <td height="233" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
            <!--DWLayoutTable-->
            <tr>
              <td width="17" height="19"></td>
              <td width="201" align="center" valign="top"><img src="/images/UsersList_27.jpg" width="194" height="19"></td>
            </tr>
            <tr>
              <td height="214">&nbsp;</td>
              <td valign="top"><div align="right" class="style12">شما 
													این صفحه را زمانی مشاهده میکنید که سرور دچار خطای داخلی میشودو جزِِییات این خطا 
													در این صفحه نمایش داده میشود
												</div></td>
            </tr>
      </table></td>
    </tr>
  </table></td>
  </tr>
  <tr>
    <td height="546" valign="top"><table width="100%" height="553" border="0" cellpadding="0" cellspacing="0" bgcolor="#C2AD3D"
							id="TableTools" >
							<!--DWLayoutTable-->
							<tr>
							  <td height="31" colspan="3" valign="top" bgcolor="#FFE8C6"><table width="100%" border="0" cellpadding="0" cellspacing="0">
									  <!--DWLayoutTable-->
									  <tr>
									    <td width="559" height="27" background="/images/stretch.jpg" bgcolor="#FFE8C6"><div align="center"><span class="style11">جزییات خطای سرور</span></div></td>
									  </tr>
							  </table></td>
							</tr>
							<tr bgcolor="#FFE8C6">
							  <td width="100" height="187">&nbsp;</td>
	    <td width="368">&nbsp;</td>
	                          <td width="91">&nbsp;</td>
		</tr>
							<tr>
							  <td height="154" bgcolor="#FFE8C6">&nbsp;</td>
							  <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
							    <!--DWLayoutTable-->
							    <tr>
							      <td width="368" height="35" bgcolor="#FFE8C6">&nbsp;</td>
						        </tr>
							    <tr>
							      <td height="72" align="center" valign="middle" bgcolor="#FFE8C6"><strong>Error Details : <%=this.Request.QueryString["error"]%> </strong> </td>
						        </tr>
							    <tr>
							      <td height="47" bgcolor="#FFE8C6">&nbsp;</td>
						        </tr>
						      </table></td>
							  <td bgcolor="#FFE8C6">&nbsp;</td>
	    </tr>
							<tr bgcolor="#FFE8C6">
							  <td height="181">&nbsp;</td>
							  <td>&nbsp;</td>
							  <td>&nbsp;</td>
	    </tr>
	    </table></td>
  </tr>
  <tr>
    <td height="38" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
      <!--DWLayoutTable-->
      <tr>
        <td width="777" height="26">
        <IMG SRC="http://www.iranblog.com/images/main_29.gif" ALT=""></td>
      </tr>
    </table></td>
  </tr>
</table>
<map name="Map">
  <area shape="rect" coords="703, 4, 774, 25" href="http://forum.persianweb.com" target="_blank">
  <area shape="rect" coords="624, 4, 705, 27" href="http://www.iranblog.com/services/search.aspx" target="_blank">
  <area shape="rect" coords="546, 3, 624, 28" href="http://www.iranblog.com/Help.aspx">
  <area shape="rect" coords="454, 3, 545, 25" href="http://www.iranblog.com/Help.aspx">
</map>
</body>
</html>