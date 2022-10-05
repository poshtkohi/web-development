<%@ Page Language="C#" AutoEventWireup="true" CodeFile="comments.aspx.cs" Inherits="services.blogbuilderv1.comments" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
<meta http-equiv="Content-Language" content="fa">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="../images/style.css" />
<style>
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none }
.style1 {
	color: #FF0000;
	font-size: 14pt;
}

</style>
<script type="text/javascript">
var temp = 	'';
function RssBurnerCodeGenerator()
{
	if(document.getElementById('username').value == "")
	{
		document.getElementById('code').disabled = true;
		alert("نام کاربری شما که باید آن را در سایت پیغامک ثبت کرده باشید، خالی است");
		document.getElementById('username').focus();
		return ;
	}
	document.getElementById('code').innerHTML = temp.replace("[username]", document.getElementById('username').value);
	document.getElementById('code').innerHTML = document.getElementById('code').innerHTML.replace("[username]", document.getElementById('username').value);
	document.getElementById('code').disabled = false;
}
</script>
<script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD>
<body>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
      						<tr>
								<td width="554" height="5" align="center">                                    </td>
							</tr>
							<tr>
							  <td height="127" align="right" valign="top" dir="rtl">
&nbsp;								<img height="20" src="../../images/56%20(10).png" width="17" align="absmiddle" />هم اکنون شما کاربر محترم ایران بلاگ می 
								توانید از طریق ارسال پیامک <span lang="en-us">(SMS)
								</span>وبلاگ خود در ایران بلاگ را به روز کنید. 
								این سرویس جدید با توجه به سرویس میکروبلاگ پیغامک 
								طراحی شده توسط مهندس پشتکوهی، برای شما کاربران 
								محترم ایران بلاگ فراهم شده است. برای استفاده از 
								این سرویس جدید ایران بلاگ ابتدا در سایت 
<a target="_blank" href="http://www.peyghamak.com/" title="برای ثبت نام در پیغامک بر روی این لینک کلیک کنید">
<font color="#FF0000">پیغامک 
								ثبت نام</font></a> کرده و در صفحه شخصی خود در پیغامک در بخش 
								تنظیمات شماره موبایل خود را وارد کرده و پس از 
								تایید شماره موبایل خود در سایت پیغامک نام کاربری 
								خود را در بخش زیر وارد کرده و دکمه تایید را فشار 
								دهید. در زیر دکمه تایید و در بخش کد پیامک، 
								محتوای آن را کپی کرده و در بخش ویرایش قالب وبلاگ از 
								منوی ابزار ایران بلاگ این کد را در محل مناسبی ار 
								قالب وبلاگ خود قرار دهید. پس از نمایش وبلاگ خود 
								اگر شما از طریق موبایل به سایت پیغامک پیامک
				<span lang="en-us">(SMS)</span> ارسال کرده بشید، 
								پست های ارسالی شما در مکان مورد نظر در قالب 
								وبلاگتان نمایش داده خواهد شد.<span lang="en-us"> 
شماره پيامك (SMS) سايت پيغامك 3000990099 مي‌باشد </span>که پس از انجام&nbsp; 
مراحل بالا شما می توانید پیام های کوتاهتان را به این شماره&nbsp; اس ام اس کنید 
تا ایران بلاگ آن را در وبلاگ شما به نمایش در آورد. توجه کنید که این سرویس جدید 
ایران بلاگ با استفاده از تکنولوژی آژاکس طراحی شده است و بعد از لود شدن کامل صفحه 
وبلاگ شما محتوای اس ام اس بلاگتان نمایش داده خواهد شد. یک نمونه اس ام اس بلاگ 
استفاده شده را می توانید در وبلاگ <span lang="en-us">
<a target="_blank" href="http://alireza.iranblog.com">
http://alireza.iranblog.com</a></span> مشاهده کنید.<br>
				<br> 
			    				</td>
							</tr>
							<tr>
							  <td height="262" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
							    <tr>
							      <td width="193" height="58">&nbsp;</td>
						        <td width="170">&nbsp;</td>
							    <td width="191">&nbsp;</td>
							    </tr>
							    <tr bgcolor="#EBEBEB">
							      <td height="23" valign="top">&nbsp;</td>
							      <td valign="top"><input type="text" name="username" id="username" style="TEXT-ALIGN:left;width:150px"/></td>
							      <td valign="middle" dir="rtl">نام کاربری شما در سایت پیغامک : </td>
							    </tr>
								<tr>
							      <td height="23" valign="top">&nbsp;</td>
							      <td valign="top"><input type="submit" name="Submit" value="تایید" onclick="RssBurnerCodeGenerator();"/></td>
							      <td valign="middle" dir="rtl">تایید نام کاربری بالا : </td>
							    </tr>
								<tr bgcolor="#EBEBEB">
							      <td height="23" valign="top">&nbsp;</td>
							      <td valign="top">
								  <textarea readonly="readonly" style="MARGIN-TOP: 5px; FONT-SIZE: 8pt; WIDTH:180px; COLOR: #333333; FONT-FAMILY: Tahoma; HEIGHT: 150px; TEXT-ALIGN: left" name="code" id="code" cols="30" rows="20" disabled="true">
<script type="text/javascript" src="/Scripts/RssBurner.js"></script>
<div id="loaderdiv"></div>
<div id="PeyghamakRssContainer"></div>
<script type="text/javascript">RssBurner("[username]");</script>
</textarea></td>
							      <td valign="top" dir="rtl">پس از فشردن دکمه تایید، 
									کل محتویات این بخش را در جای مناسبی در قالب وبلاگ خود قرار دهید 
									(مثلا در بخش پیوند های روزانه قالب وبلاگتان) : </td>
							    </tr>
						      </table></td>
  </tr>
    </table>
</body>
<script type="text/javascript">
temp = 	document.getElementById('code').innerHTML;
</script>
</HTML>