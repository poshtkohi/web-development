<%@ Page Language="C#" AutoEventWireup="true" CodeFile="comments.aspx.cs" Inherits="Peyghamak.mobile.comments"%>
<?xml version="1.0" encoding="UTF-8"?><!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="fa">

<head>
<title id="title" runat="server"></title>
<style type="text/css">
body {background-color:#ffffff;color:#333333;font-family:Helvetica,sans-serif;text-align:right;direction:rtl}
body,ul,li,table,tr,td {margin:0;padding:0;}
img {border:0;}
div, p {direction:rtl;}
a {text-decoration:none; color:#006699;}
ul li {list-style:none; border-bottom:#999999 solid 1px;}
small {color:#666666; font-size:x-small;}
div.m input{background-color:#FFFFFF; border:none; color:#0066cC;}
</style>
</head>

<body style="background-color:#ffffff;color:#333333;font-family:Helvetica,sans-serif;text-align:right;direction:rtl">
<div style="background-color:#D3F2D2;padding:3px;"><a href="#"><img src="http://peyghamak.com/mobilelogo.gif" alt="پیغامک"/></a></div>
<div style="margin-top:12px;"><b><asp:Label ID="NameLabel" runat="server"/></b></div>
<div class="m">
<div style="margin:12px 5px 12px 5px;">
<span style="color:#003366">
<div id="comment_post" style="direction:rtl; text-align:right"><asp:Label id="PostContent" runat="server"></asp:Label></div>
<br/>
<small><div id="comments_date"><asp:Label ID="PostDate" runat="server"></asp:Label></div></small>
</span>
</div>
<br/>
<hr/>
<div style="margin: 20px 3px 5px; padding: 6px;">
<ul>
<asp:Label ID="resultText" runat="server"/></div>
</ul>
</div>
<div style="margin: 20px 3px 5px; padding: 6px; text-align:center;">
<small><asp:Label ID="NumCommentsLable" runat="server">0</asp:Label> نظر</small>
<br/>
<asp:Panel ID="PostPanel" runat="server" EnableViewState="False">
<form action="comments.aspx?mode=post" method="post">
<input type="text" class="i" id="PostContent" name="PostContent" maxlength="450"/>
<br/>
<asp:Label ID="h" runat="server"/>
<input type="submit" value="بفرست"/>
</form>
</asp:Panel>
<asp:Panel ID="MustLoginToCommentPanel" runat="server" EnableViewState="False">
<div align="center" class="error" dir="rtl">برای ارسال نظر شما باید یکی از اعضای سایت باشید. 
برای ورود به سایت <a href="http://www.peyghamak.com/m/login.aspx" style="text-decoration:none" >اینجا</a> و برای ثبت نام در سایت
<a href="http://www.peyghamak.com/signup.aspx" style="text-decoration:none">اینجا</a> را کلیک کنید.</div>
</asp:Panel>
</div>
<div style="margin-top:5px;">
  <p><a href="friends.aspx" accesskey="7">دوستان</a> | <a href="friends.aspx?mode=followers" accesskey="6">دنبال‌كنندگان</a> | <a href="signout.aspx" accesskey="5">خروج</a> | <a href="my.aspx" accesskey="4">پیغامک من</a></p>
</div>
<div style="background-color:#D3F2D2;padding:3px;text-align:center;margin-top:3px;font-size:small">پیغامک در حالت <a href="http://peyghamak.com">استاندارد</a><br/>
&copy; 2007-2009 پیغامک</div>
</body>
</html>
