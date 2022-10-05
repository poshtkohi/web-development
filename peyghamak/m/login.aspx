<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Peyghamak.mobile.login"%>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="apple-touch-icon" href="http://peyghamak.com/logo5757.png" />
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>پیغامک | ورود</title>
<style type="text/css">
body {background-color:#ffffff;color:#333333;font-family:Helvetica,sans-serif;text-align:right;direction:rtl}
body,ul,li,table,tr,td {margin:0;padding:0;}
img {border:0;}
div, p {direction:rtl;}
</style>
</head>
<body style="background-color:#ffffff;color:#333333;font-family:Helvetica,sans-serif;text-align:right;direction:rtl">
<div style="background-color:#D3F2D2;padding:3px;"><a href="#"><img src="http://peyghamak.com/mobilelogo.gif" alt="پیغامک"/></a></div>
<div style="margin:12px 0px 12px 0px">شما می‌توانید لحظات خود، تفكرات خود و نوشته‌های كوتاه خود را در این مكان ثبت كنید.</div>
<div style="background:#E8FECD; border:1px solid #A9BF74;margin: 20px 3px 5px; padding: 6px;">
نام کاربری و كلمه عبور خود را وارد کنید.
<form action="login.aspx?mode=l" method="post">
<b>نام کاربری </b><input type="text" id="username" name="username" style="direction:ltr"/><br/>
<b>کلمه عبور  </b><input type="password" id="password" name="password" style="direction:ltr"/><br/>
<input type="submit" name="submit" value="ورود" id="submit"/>
</form>
<asp:Label ID="message" ForeColor=#FF0000 runat="server"></asp:Label>
<div style="background-color:#D3F2D2;padding:3px;text-align:center;margin-top:3px;font-size:small">پیغامک در حالت <a href="http://peyghamak.com">استاندارد</a><br/>
&copy; 2007-2009 پیغامک</div>
</body>
</html>
