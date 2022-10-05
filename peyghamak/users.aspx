<%@ Page Language="C#" AutoEventWireup="true" CodeFile="users.aspx.cs" Inherits="Peyghamak.users" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title id="title">لیست کاربران پیغامک</title>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://www.peyghamak.com/js/Functions.js"></script>
</head>

<body>
<form id="mainform" runat="server" onsubmit="return ListUsersBySearchQueryValidation();">
<!--body-->
<div id="body">

<!--menu inner-->
<div id="menu">
    <div id="menu_inner">
    	<asp:Panel ID="UnloginedPanel" runat="server" EnableViewState="False">
            <a href="http://www.peyghamak.com/signin.aspx" target="_self">ورود</a> | <a href="http://www.peyghamak.com/signup.aspx" target="_self">عضویت</a>
        </asp:Panel>
        <asp:Panel ID="LoginedPanel" runat="server" EnableViewState="False">
          <asp:HyperLink ID="MyPageHperLink" Text="پیغامک خودم" runat="server" Target="_self" style=""></asp:HyperLink> | 
              <asp:HyperLink ID="SignoutHyperLink" runat="server" Target="_self" Text="خروج از سایت"></asp:HyperLink>
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
    <div id="users"> 
    <section id="SearchSection" runat="server">
		<script type="text/javascript" src="http://www.peyghamak.com/js/wz_tooltip.js"></script> 
		<script type="text/javascript" src="http://www.peyghamak.com/js/tip_balloon.js"></script>
		<div id="T2tDirectAlertMessage" style="display:none">
			<div class="tooltipDiv" id="AlertMessageText">
				جستجو بر اساس نام، نام کاربری، شهر یا کشور کاربران سایت صورت می گیرد.
				</div>
		</div>    	
		<div align="center" id="singup">
            <div dir="rtl">
              <asp:TextBox ID="query" runat="server" style="width:200px" dir="rtl" MaxLength="30" onmouseover="TagToTip('T2tDirectAlertMessage', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
                        onmouseout="UnTip()"></asp:TextBox>
              &nbsp;<asp:Button ID="search" runat="server" Text="جستجو" OnClick="search_Click"/>&nbsp;
              <br>
            </div>
         </div>
    </section>
        <div id="ListOutput" runat="server" EnableViewState="false"></div>                    
    </div>
    
    <p>&nbsp;</p>
    <p>&nbsp;</p>


</div>
<!--/left fill-->
    <div id="left_bottom">
    	<span class="left_bottom">
    	 	<asp:PlaceHolder ID="SiteFooterSection" runat="server"></asp:PlaceHolder>     
        </span>
    </div>    
</div>
<!--/left-->

<!--right-->
<div id="right">

<div class="right_sidebar_top">
<div class="right_sidebar_text">
تعداد کاربران</div>
</div>

<div class="right_sidebar_fill">
<div id="avatar_box">
<div id="ListTopFriends" runat="server" enableviewstate="false"></div>
<div dir="rtl">تعداد: <asp:Label ID="UsersNum" runat="server">0</asp:Label></div>
	</div>
</div>

<div class="right_sidebar_fill"></div>

<div class="right_sidebar_fill">
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>    
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>    
</div>

<div class="right_sidebar_btm">&nbsp;&nbsp;&nbsp;&nbsp;</div>
</div>
<!--/right-->


</div>
<!--/body-->

<asp:PlaceHolder ID="GoogleAnalyticsSection" runat="server"></asp:PlaceHolder>
</form>
</body>
</html>
