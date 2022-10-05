<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax.desk.aspx.cs" Inherits="services.blogbuilderv1.ajax.desk" %>
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
        <link rel="stylesheet" type="text/css" href="/services/styles/cp.css" />
   		<script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white">
<center>
            <DIV class=box style="WIDTH: 550px">

            <DIV class=header>
            <DIV class=title>میز کار کاربری شما</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
              			<div class="content">
				<table class="hometb" style="font-size: 8pt; color: rgb(145, 145, 145); font-family: Tahoma;" dir="rtl" width="100%" border="0" cellpadding="2" cellspacing="3">
					<tbody><tr>
						<td width="32">
							<img src="../images/post.gif" width="29" border="0" height="32"></td>
						<td><a href="PostAdmin.aspx">
						مدیریت پست های وبلاگ</a><br>
							نوشتن مطلب جدید و انتشار آن در وبلاگ</td>
					</tr>
					
					
					
					
					<tr>
						<td width="32">
							<img src="../images/link1.gif" width="32" border="0" height="32"></td>
						<td><a href="ajax.links.aspx">پیوندهای روزانه</a><br>
							درج پیوند (لینک) جدید یا ویرایش پیوندهای ثبت شده در بخش پیوندهای روزانه</td>
					</tr>
					<tr>
						<td width="32">
							<img src="../images/link2.gif" width="32" border="0" height="32"></td>
						<td><a href="ajax.links.aspx?_mode=ajax.linkss">
						مدیریت لینکستان وبلاگ</a><br>
							درج پیوند جدید یا ویرایش پیوندهای ثابت وبلاگ (وبلاگ دوستان ، سایتهای مورد علاقه 
							و...)</td>
					</tr>
	<tr>
		<td width="32" align="center">
		<img src="../images/myblog.gif" width="25" border="0" height="32"></td>
		<td>
		<a href="http://<%=((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).Subdomain%>.iranblog.com/" target="_blank">مشاهده وبلاگ</a><br>
		مشاهده صفحه نخست وبلاگ ( <%=((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).Subdomain%>.<span lang="en-us">iranblog</span>.com ) </td>
	</tr>
	
							<tr>
						<td width="32">
							<img src="../images/profile.gif" width="32" border="0" height="32"></td>
						<td>
							<a href="ajax.account.aspx">
							مدیریت تنظیمات وبلاگ </a><br>
							در این بخش می توانید&nbsp; تنظیمات وبلاگ خود همانند 
							تعداد پست های نمایش و آواتور وبلاگتان را تغییر دهید 
						</td>
					</tr>
						
										
					<tr>
						<td width="32">
							<img src="../images/logoff.gif" width="32" border="0" height="31"></td>
						<td>
							<a href="signout.aspx" target="_top">خروج</a><br>
							خروج از بخش مدیریت پس از اتمام کار نویسنده
						</td>
					</tr>
				</tbody></table></div>
              </DIV>
            </DIV>
            </DIV>
            
            
            
            
            
            
                       <DIV class=box style="WIDTH: 550px">

            <DIV class=header>
            <DIV class=title>تابلوی اعلانات سایت ایران بلاگ</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
              			<div class="content">
				<table class="hometb" style="font-size: 8pt; color: rgb(145, 145, 145); font-family: Tahoma;" dir="rtl" width="100%" border="0" cellpadding="2" cellspacing="3">
					<tbody><tr>
						<td>پيغامك چيست؟<br>پيغامك يك سيستم پیغامک كوچك است كه 
						در آن مي‌توانيد نوشته‌هاي خود را در حداكثر 450 كاراكتر 
						ثبت كنيد. شما در پيغامك مي‌توانيد علاوه بر وب از طريق 
						موبايل خود نيز به وسيله پيامك (SMS) پیغامک خود را به روز 
						كنيد و در آينده نزديك مي‌توانيد نوشته‌هاي افراد مورد 
						علاقه خود را نيز از طريق پيامك بر روي موبايل خود دريافت 
						كنيد. لازم به ذكر است كه پيغامك روايت ايراني‌شده‌ي 
						سيستم‌هاي ميني پیغامک مانند twitter و jaiku مي‌باشد كه 
						در اين راه تلاش شده است ويژگي‌هاي مورد نياز براي كاربر 
						ايراني فراهم شود. <br><br>برای عضویت در سایت پیغامک که 
						یکی از امکانات سایت ایران بلاگ نیز می باشد بر روی لینک 
						زیر کلیک نمایید:<br><span lang="en-us">
						<a href="http://www.peyghamak.com" target="_blank">
						http://www.peyghamak.com</a></span></td>
					</tr>
					
					
					
					
				</tbody></table></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script language="javascript">
dynaframe();
</script>