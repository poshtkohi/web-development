<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginControl.ascx.cs" Inherits="bookstore.LoginControl" %>
 <PersianPanel runat="server" id="PersianPanel">
     <UnLogginedPanel runat="server" id="UnLogginedPanel"><a href="/">صفحه اصلی</a> | <a href="signin.aspx">ورود</a> | <a href="signup.aspx">ثبت نام</a></UnLogginedPanel>
     <LoginedPanel runat="server" id="LoginedPanel"><span style="color:White; font-size:x-small">خوش آمدی <% this.Response.Write(this.Session["username"]);%></span>&nbsp;&nbsp;<a href="/">صفحه اصلی</a> | <a href="cp/account.aspx">کنترل پنل کاربری</a> | <a href="/signout.aspx">خروج از سایت</a></LoginedPanel>
 </PersianPanel>