<%@ Page Language="C#" CodeFile="signin.aspx.cs" Inherits="bookstore.signin"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>ورود به سایت</title>
        <link href="style/calass.css" rel="stylesheet" type="text/css" />
        <link href="styles/cp.css" rel="stylesheet" type="text/css" />
        <link href="styles/addons.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="skl">
	<div id="top"><img src="images/top.jpg" /></div>
    <div id="topmenu"><asp:PlaceHolder ID="LoginPanelControl" runat="server"></asp:PlaceHolder></div>
    <div id="topspace"></div>
    <div id="center-s1">
    <div id="register">
        <form id="form1" runat="server">
        <asp:Label ID="message" runat="server" class="validation_tahoma"></asp:Label><br/><br/>
      <label>نام کاربری</label>
      <asp:TextBox ID="username" MaxLength="50" runat="server" TextMode="SingleLine"/>
            &nbsp;<label>کلمه عبور</label>
			<asp:TextBox ID="password" MaxLength="30" runat="server" TextMode="Password" style="direction:ltr" CssClass="login_input"/>
            &nbsp;<asp:Button ID="submit" runat="server" Text="ورود" OnClick="submit_Click" style=" height:20px"/>
        	&nbsp;<label><a href="forgett.aspx">کلمه عبور خود را فراموش کرده اید؟</a></label>
        </form>
     </div>               
     <div id="width-spacer">&nbsp;</div>
    </div>
      
    <div id="right">
     <div id="booklist">
      <asp:PlaceHolder ID="MainMenuControl" runat="server"></asp:PlaceHolder>   
    </div>
     
</div>
     
</div>
<div id="btm"></div> 
</body>
</html>
