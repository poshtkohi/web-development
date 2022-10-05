<%@ Page Language="C#" AutoEventWireup="true" CodeFile="guest.aspx.cs" Inherits="Peyghamak.mobile.guest"%>
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
<div style="margin-top:12px"><b><asp:Label ID="NameLabel" runat="server"/></b></div>
<div class="m"><br/>
<hr/>
<div style="margin: 20px 3px 5px; padding: 6px;">
<ul>
<asp:Label ID="resultText" runat="server"/></ul>
</div>
</div>
<div style="margin-top:5px;">
  <p><a href="friends.aspx" accesskey="7">دوستان</a> | <a href="friends.aspx?mode=followers" accesskey="6">دنبال‌كنندگان</a> | <a href="signout.aspx" accesskey="5">خروج</a> | <a href="my.aspx" accesskey="4">پیغامک من</a></p>
</div>
<div style="background-color:#D3F2D2;padding:3px;text-align:center;margin-top:3px;font-size:small">پیغامک در حالت <a href="http://peyghamak.com">استاندارد</a><br/>
&copy; 2007-2009 پیغامک</div>
</body>
</html>
