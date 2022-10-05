<%@ Page Language="C#" AutoEventWireup="true" CodeFile="signin.aspx.cs" Inherits="Peyghamak.signin"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title>ورود به پیغامک</title>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
</head>

<body>
<!--body-->
<div id="body">

<!--menu inner-->
<div id="menu">
    <div id="menu_inner">
   <asp:Panel ID="UnloginedPanel" runat="server" EnableViewState="False">
            <a href="http://www.peyghamak.com/signup.aspx" target="_self">عضویت</a>
        </asp:Panel>
</div>
    <div class="menu_message">
    	
    </div>
</div>
<!--/menu inner-->

<!--left-->
<div id="left">

	<!--top left-->
	<div id="left_top">
    
    </div>
    <!--/top left-->
    
    <!--left fill-->
    <div id="left_fill">
    <p>
    <div id="signin_flowers">
    <div id="login">	
      	<form runat="server">
            نام کاربری:
              
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="username"
                                  ErrorMessage="*" BackColor="White" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                                      ID="RegularExpressionValidator3" runat="server" ControlToValidate="username"
                                      ErrorMessage="*" ValidationExpression="^[\-0-9a-zA-Z]{1,}$" BackColor="White" setfocusonerror="True"></asp:RegularExpressionValidator>
<asp:TextBox ID="username" MaxLength="30" runat="server" TextMode="SingleLine" CssClass="login_input"/>
            کلمه عبور:
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="password"
                                  ErrorMessage="*" BackColor="White" SetFocusOnError="True"></asp:RequiredFieldValidator>
			<asp:TextBox ID="password" MaxLength="30" runat="server" TextMode="Password" style="direction:ltr" CssClass="login_input"/>
            <asp:Button ID="submit" runat="server" Text="ورود" OnClick="submit_Click" cssclass="login_btn"/>
        	<asp:CheckBox ID="cookieEnabled" runat="server" Text="مرا به خاطر بسپار؟"/>
        	</form>
        <hr />
        <label>
        	<a href="signup.aspx">ثبت نام در جامعه پیغامک</a><br>
             <a href="ForgottenPassword.aspx">کلمه عبور خود را فراموش کرده اید؟</a>
         </label>
        <br/>
        <span class="error">
			<asp:Label ID="message" runat="server"></asp:Label>
        </span>
    </div>
    </div>
    </p>   
    </div>
<!--/left fill-->
   <div id="left_bottom">
   	<span class="bottom_menus">
	   <asp:PlaceHolder ID="SiteFooterSection" runat="server"></asp:PlaceHolder>
    </span>
   </div>    
</div>
<!--/left-->

<!--right-->
<div id="right">

<div class="right_sidebar_top">
<div class="right_sidebar_text">

</div>
</div>

<div class="right_sidebar_fill">
    <p>&nbsp;</p>

    <a href="#"><div id="join_us"></div></a>
    <p>&nbsp;</p>
	</div>
    

<div class="right_sidebar_btm">&nbsp;&nbsp;&nbsp;&nbsp;</div>
</div>
<!--/right-->


</div>
<!--/body-->

</body>
</html>
