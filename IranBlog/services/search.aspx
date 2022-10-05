<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%@ Page language="c#" Inherits="services.search" CodeFile="search.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>.:: Iranblog ::. PowerFul Tools For Bloggers ::. جستجو در  ایران بلاگ ::.</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="The Greatest Weblog Technology" name="description">
		<meta content="iran,iranian,iranblog,iran blog,persian,persia,persian blog,persianblog,weblog,web,blog,forum,netwoking,network software,software,search,C#,ASP.NET,VC++,.NET"
			name="keywords">
		<meta content="Alireza Poshtkohi" name="author">
		<meta name="coyright" content="Alireza Poshtkohi">
		<LINK href="v3fa.css" type="text/css" rel="stylesheet">
		<LINK href="menu.css" type="text/css" rel="stylesheet">
		<style>
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none }
		</style>
		<script language="javascript" type="text/javascript">
			/* All rights reserved to Mr. Alireza Poshtkohi (C) 2005-2007. alireza.poshtkohi@gmail.com */
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
				//---------------------------------------------------------------------------------------------------------------------------
		</script>
	</HEAD><body>
		<table width="750" border="0" cellpadding="0" cellspacing="0" align="center">
			<!--DWLayoutTable-->
			<tr>
				<td height="77" colspan="3" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td width="379" height="75" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/ib--title.png"
									style="BACKGROUND-REPEAT:no-repeat">
									<tr>
										<td width="89" height="55">&nbsp;</td>
										<td width="290" valign="top"><!--DWLayoutEmptyCell-->&nbsp; 
										</td>
									</tr>
									<tr>
										<td height="20" colspan="2" valign="top" dir="rtl">
<div style="PADDING-RIGHT:159px; PADDING-LEFT:0px; PADDING-BOTTOM:0px; PADDING-TOP:1px">»<a href="/services/Help.aspx">
													راهنما</a><span lang="en-us">&nbsp;&nbsp;&nbsp; </span>&nbsp;» <a href="/services/contactus.aspx">
													تماس با ما</a>
												<span lang="en-us">&nbsp;&nbsp;&nbsp; </span>»<a href="/services/search.aspx"> جستجو</a>

                                          </div>
										</td>
									</tr>
								</table>
							</td>
							<td width="371" valign="top"><div align="right"><img src="../images/ib--logo.png" width="204" height="75"></div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="28" colspan="3" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td width="750" height="28" align="center" valign="middle"><div id="tabs5">
									<ul>
										<li>
											<a href="/services/about.aspx" target="_self">
												<span>   درباره ایران بلاگ   </span>
											</a></li>
											<li>
											<a href="/services/ads.aspx" target="_self">
												<span>تبلیغات</span></a></li>
												<li>
											<a href="http://up.iranblog.com/" target="_blank">
												<span>آپلود مجانی فایل و عکس</span></a></li>
											<li>
											<a href="http://forum.iranblog.com/" target="_blank">
												<span> انجمن گفتگو </span></a>
										</li>
										<li>
											<a href="/services/UsersList.aspx" target="_self">
												<span> فهرست کاربران </span></a></li>
										<li>
											<a href="http://news.iranblog.com" target="_self">
												<span>اخبار سایت</span></a></li>
										<li>
											<a href="/services/register.aspx" target="_self">
												<span> ثبت وبلاگ جدید </span></a></li>
										<li>
											<a href="/services/" target="_self">
												<span>صفحه اصلی</span></a></li>
									</ul>
								</div>
		  </td>
        </tr>
      </table>
				</td>
			</tr>
			<tr>
				<td width="193" rowspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" style="BORDER-RIGHT:#e6e6e6 1px dotted">
						<!--DWLayoutTable-->
						<tr>
							<td height="432" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
									<!--DWLayoutTable-->
									<tr>
											<td width="192" height="527" valign="top" background="../images/bgmainrl.png" dir="rtl" class="v3bg-title"><div class="main2 v3color_green"></div>
										  </td>
										</tr>
									<tr>
										<td height="28" align="right" valign="middle" dir="rtl"><img src="../images/marrow.png" width="11" height="11"><a href="http://news.iranblog.com/" target="_self">
												وبلاگ مدیران ایران بلاگ </a>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
				<td width="364" height="562" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td width="364" height="54" align="center" valign="middle"><BR>
								<BR>
								<BR>
								<a href="/services/register.aspx"><img src="../images/newblog.png" alt="ایجاد وبلاگ جدید شما در جامعه بزرگ ایران بلاگ"
										width="183" height="32" border="0"></a></td>
						</tr>
						<tr>
						  <td height="168">&nbsp;</td>
						</tr>
						<tr>
						  <td height="42" align="right" valign="top" class="main" dir="rtl">
							<div align="center">
                   <form action="http://www.google.com/search" method="get" target="_self">
  &nbsp;&nbsp;&nbsp;
                <input  type="submit" value="جستجو" name="btnG" style="height:20px;font-name:tahoma; font-family:Tahoma; font-size:8pt">
&nbsp;
              <input  dir="rtl" style="FONT-FAMILY: nesf2, tahoma" size="20" name="q" height="20">
              <input type="hidden" name="as_sitesearch" value="iranblog.com">
              <input type="hidden" name="ie" value="utf8">
              <input type="hidden" name="oe" value="fa">
              <input type="hidden" name="hl" value="fa">
              </form> </div></td>
						</tr>
						<tr>
						  <td height="284">&nbsp;</td>
				  </tr>
					</table>
				</td>
				<td width="193" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" style="BORDER-LEFT:#e6e6e6 1px dotted">
						<!--DWLayoutTable-->
						<tr>
							<td width="192" height="30" valign="middle" bgcolor="#f3f3f3" dir="rtl" background="../images/bgtitleheader.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
						</tr>
						<tr>
							<td height="532" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
									<!--DWLayoutTable-->
									<tr>
										<td width="192" height="504" valign="top" background="../images/bgmainrl.png" dir="rtl"><span class="v3color_green" style="LINE-HEIGHT:1.5">
												
												<br>
											</span>
										</td>
									</tr>
									<tr>
										<td height="28" valign="middle" align="right" dir="rtl"><!--DWLayoutEmptyCell-->&nbsp; 
											</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="2"></td>
				<td></td>
			</tr>
			<tr>
				<td height="42" colspan="3" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" style="BORDER-TOP:#c6db00 1px solid; BORDER-BOTTOM:#d3d3d3 1px solid">
						<!--DWLayoutTable-->
						<tr>
							<td width="192" height="41" valign="top"><img src="../images/map.gif" width="31" height="18" align="absMiddle">
								Iranblog weblog services.<BR>
								© 2002-2007 .All rights reserved.</td>
							<td width="558" valign="top" dir="rtl">کلیه حقوق این وب سایت متعلق به گروه ایران 
								بلاگ می باشد.
							</td>
						</tr>
					</table>
				</td>
			</tr>
			</TBODY></table>
		
	</body>
</HTML>
