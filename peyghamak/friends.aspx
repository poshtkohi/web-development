<%@ Page Language="C#" AutoEventWireup="true" CodeFile="friends.aspx.cs" Inherits="Peyghamak.friends" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title runat="server" id="title">Peyghamak.com</title>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://www.peyghamak.com/js/Functions.js"></script>
</head>

<body>
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
    <div class="menu_message" id="messagee" runat="server">
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
        <div id="ListOutput" runat="server"></div>                    
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
<asp:Label ID="FollowersLable" Text="دنبال کننده گان" runat="server" Visible="False"></asp:Label> <asp:Label ID="FriendsLable" Text="دوستان" runat="server"></asp:Label>
</div>
</div>

<div class="right_sidebar_fill">
<div id="avatar_box">

<div id="ListTopFriends" runat="server" enableviewstate="false"></div>
	<asp:HyperLink ID="ListTopFriendsHyperlink" runat="server" Target="_self" NavigateUrl="friends.aspx" Visible="False" EnableViewState="False">بقیه دوستان</asp:HyperLink>
	</div>
</div>

<div class="right_sidebar_title_fill">
<div class="right_sidebar_text_2">
دیگر لینک ها
</div>
</div>

<div class="right_sidebar_fill">
<div id="avatar_box"><a href="/">صفحه اصلی پیغامک</a><br><asp:HyperLink ID="FriendsHyperlink" runat="server" NavigateUrl="friends.aspx?page=1&mode=friends" Text="صفحه دوستان"></asp:HyperLink><asp:HyperLink ID="FollowersHyperlink" runat="server" NavigateUrl="friends.aspx?page=1&mode=followers" Text="صفحه دنبال کنندگان"></asp:HyperLink>
          <br />
                    </div>
</div>

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

<div class="right_sidebar_btm"></div>
</div>
<!--/right-->


</div>
<!--/body-->

<asp:PlaceHolder ID="GoogleAnalyticsSection" runat="server"></asp:PlaceHolder>
</body>
</html>
