<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsletter.aspx.cs" Inherits="services.newsletter" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD>
<title>.:: Iranblog ::. PowerFul Tools For Bloggers ::. خبرنامه وبلاگ ::.</title>
<meta http-equiv="Content-Language" content="fa">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta content="Alireza Poshtkohi" name="author">
<meta name="coyright" content="Alireza Poshtkohi">
  </HEAD>
<body>
<br>
<div align=center>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="message" runat="server" BackColor="#FFFFC0" Font-Names="Tahoma" Font-Size="X-Small" Width="100%" ForeColor="Red"></asp:Label></div>
    </form>
    </div>
</body>
</html>
