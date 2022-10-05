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
			<script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD><body><center>
<form id="Form1" method="post" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
      						<tr>
								<td width="554" height="5" align="center">                                    </td>
							</tr>
							<tr>
								<td height="27" class="v3ibbtn">
							  <p align="center" dir="rtl"> راهنمای نظرات وبلاگ</td>
							</tr>
							<tr>
								<td height="558" valign="top" dir="rtl" align="right">
								&nbsp;<p>از تاریخ <span dir="rtl">1386/6/5</span> سیستم جدید نظرات وبلاگ ها بر 
								روی ایران بلاگ نصب شد. این سیستم تنها برای پست 
								هایی که توسط مدیران وبلاگ ها از این تاریخ به بعد 
								نوشته شده اند معتبر است. سیستم جدید نظرات برای 
								افزایش سرعت و راحتی بیشتر خوانندگان و مدیران 
								وبلاگ ها بر اساس تکنولوژی آزاکس طراحی شده است. 
								مدیران وبلاگ ها برای مدیریت نظرات هر پست لازم 
								است در وبلاگ خود بر روی لینک نظرات پست مورد 
								انتخاب خود کلیک کرده و برای مدیریت نظرات آن از 
								بخش <font color="#FF0000">&quot;ورود مدیر این وبلاگ 
								برای مدیریت نظرات این پست&quot;</font>، استفاده کرده 
								و نام کاربری و کلمه عبور خود را برای ورود به بخش 
								مدیریت نظرات وارد نمایند. در بخش زیر به توضیح 
								اجمالی گزینه های موجود در بخش مدیریت نظرات وبلاگ 
								ها که بعد از ورود مدیر وبلاگ نمایش داده می شوند 
								می پردازیم:</p>
								<p><br>
								<img height="20" src="../../images/56%20(10).png" width="17" align="absMiddle"><font color="#FF0000">خروج 
								از بخش مدیریت نظرات</font><span lang="en-us">:
								</span>از این گزینه می توانید برای خروج از بخش 
								مدیریت نظرات پست انتخاب شده استفاده کنید. توصیه 
								می کنیم برای بالا بردن امنیت نظرات وبلاگ خود، پس 
								از ویرایش نظرات بر روی این گزینه کلیک کرده تا 
								جلسه شما منقضی شده و دیگران نتوانند وارد این بخش 
								گردند. </p>
								<p> <br>
								<img height="20" src="../../images/56%20(10).png" width="17" align="absMiddle"><font color="#FF0000">فعال سازی نمایش نظرات این پست، قبل از تایید</font><span lang="en-us">:
								</span>با انتخاب این گزینه، نظرات کاربران قبل از 
								تایید شما در بخش نظرات پست مربوطه به همه 
								بازدیدکنندگان وبلاگ شما نمایش داده می شود.&nbsp;
								</p>
								<p> <br>
								<img height="20" src="../../images/56%20(10).png" width="17" align="absMiddle"><font color="#FF0000">غیرفعال  سازی نمایش نظرات این پست، قبل از تایید</font><span lang="en-us">:
								</span>با انتخاب این گزینه، نظرات کاربران قبل از 
								تایید شما در بخش نظرات پست مربوطه در وبلاگتان 
								نمایش داده نمی شود.&nbsp;&nbsp; </p>
								<p> <br>
								<img height="20" src="../../images/56%20(10).png" width="17" align="absMiddle"><font color="#FF0000">تایید این نظر</font><span lang="en-us">:
								</span>در صورت انتخاب این گزینه که برای هر نظر 
								در زیر آن نمایش داده می شود، ایران بلاگ این نظر 
								را از سوی مدیر وبلاگ تایید شده دانسته و به تمامی 
								بازدید کنندکان نظرات وبلاگ شما آن را نمایش می 
								دهد.</p>
								<p>&nbsp; <br>
								<img height="20" src="../../images/56%20(10).png" width="17" align="absMiddle"><font color="#FF0000">حذف این نظر</font><span lang="en-us">:
								</span>یکی از امکانات منحصر به فرد نسخه جدید 
								نظرات وبلاگ های ایران بلاگ گزینه &quot;حذف این نظر&quot; 
								می باشد. شما می توانید در هر زمانی و نظر هر پستی 
								را که می خواهید با انتخاب این گزینه از بخش نظرات 
								وبلاگ خود حذف کنید.<br></td>
							</tr>
							
  </table>
</form>
</center>
</body>
</HTML>