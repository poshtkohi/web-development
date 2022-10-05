<%@ Page Language="C#" AutoEventWireup="true" CodeFile="guest.aspx.cs" Inherits="Peyghamak.guest" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="alternate" type="application/rss+xml" href="/rss.aspx" runat="server" id="feed"/>
<title id="title" runat="server"></title>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<script type="text/javascript" src="http://www.peyghamak.com/js/AjaxCore.js"></script>
<script type="text/javascript" src="http://www.peyghamak.com/js/Functions.js"></script>
</head>

<body>
<form id="Form1" runat="server" onsubmit="return UserBlock();">
<asp:HiddenField ID="_idMy" runat="server" Value="-1" />
<asp:HiddenField ID="_idHe" runat="server" Value="-1" />
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

	<!--post-->
	<div id="post"> 
    	<div class="comment_right_box">
        <asp:Image runat="server" ID="UserImage" Width="75" Height="75" class="avatar_comment"/>
        <div id="op_compact">
            <asp:ImageButton ID="AddFriendImageButton" ImageUrl="/theme/images/green/add_btn.png" runat="server" CssClass="add_btn" ToolTip="اضافه کردن این شخص به لیست دوستان خود" Enabled="False" OnClick="AddFriendHyperlink_Click" Visible="False" />
            <asp:ImageButton ID="RemoveFriendImageButton" ImageUrl="/theme/images/green/remove_btn.png" runat="server" CssClass="remove_btn" ToolTip="خارج کردن این شخص از لیست دوستان خود" Enabled="False" OnClick="RemoveFriendImageButton_Click" Visible="False" />
            <asp:ImageButton ID="PrivateMessageImageButton" ImageUrl="/theme/images/green/pm_btn.png" runat="server" CssClass="pm_btn" ToolTip="به این شخص پیغام شخصی ارسال کنید" Enabled="False" OnClick="PrivateMessageImageButton_Click" Visible="False" />
        </div>
        </div>
        
        <div id="profile_name"><a href="#">
          <asp:Label ID="NameLabel" runat="server"></asp:Label>
        </a></div>
        
      <div id="profile_text" style="direction:rtl; text-align:right"><asp:Label ID="AboutLabel" runat="server" EnableViewState="False"></asp:Label></div>
    <div id="profile_info"><asp:Label ID="ProfileLabel" runat="server"></asp:Label> | <asp:HyperLink ID="UrlHyperlink" runat="server" Target="_blank" EnableViewState="False" dir="ltr"></asp:HyperLink> 
    | <asp:Label ID="CurrentDate" runat="server"></asp:Label></div>
    
  </div>
    <!--/post-->
    
    <!--left fill-->
    <div id="left_fill">
    
    <div id="loaderImg" align="center"><img src="http://www.peyghamak.com/images/loading.gif"/></div>
    <div id="resultText"></div>
           
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
دوستان
</div>
</div>

<div class="right_sidebar_fill">
<div id="avatar_box">

<div id="ListTopFriends" runat="server" enableviewstate="false"></div>
	<asp:HyperLink ID="ListTopFriendsHyperlink" runat="server" Target="_self" NavigateUrl="friends.aspx" Visible="False" EnableViewState="False" dir="rtl">بیشتر ...</asp:HyperLink>
	</div><br/>
</div>

<div class="right_sidebar_title_fill">
<div class="right_sidebar_text_2">
آمار پیغامک
</div>
</div>

<div class="right_sidebar_fill">
<div id="avatar_box">پیغامک ها:
          <asp:Label ID="PostNumLabel" runat="server" EnableViewState="False" >0</asp:Label>
          <br />
          دوستان:
          <asp:Label ID="FriendNumLabel" runat="server" EnableViewState="False" >0</asp:Label>
          <br />
          <asp:HyperLink ID="FollowerNumHyperlink" runat="server" Target="_self" NavigateUrl="friends.aspx?mode=followers" EnableViewState="False" Text="دنبال کنندگان: "></asp:HyperLink>
          <asp:Label ID="FollowerNumLabel" runat="server" EnableViewState="False" >0</asp:Label>
          <br />
                    </div>
</div>
<section id="Friendship" runat="server" Visible="False">
 <asp:LinkButton ID="AddFriendHyperlink" runat="server" Enabled="False" OnClick="AddFriendHyperlink_Click"
            ToolTip="اضافه کردن این شخص به لیست دوستان خود" Visible="False">اضافه کردن این شخص به لیست دوستان خود</asp:LinkButton>
        <asp:LinkButton ID="RemoveFriendHyperlink" runat="server" Enabled="False" OnClick="RemoveFriendHyperlink_Click"
            ToolTip="خارج کردن این شخص از لیست دوستان خود" Visible="False">خارج کردن این شخص از لیست دوستان خود</asp:LinkButton>&nbsp;
        <asp:LinkButton ID="BlockFriendHyperlink" runat="server" OnClick="BlockFriendHyperlink_Click"
			ToolTip="بلوکه کردن این شخص" Enabled="False" Visible="False">مسدود کردن این شخص</asp:LinkButton>
        <asp:LinkButton ID="UnBlockFriendHyperlink" runat="server"
			ToolTip="از بلوکه خارج کردن این شخص" Enabled="False" OnClick="UnblockFriendHyperlink_Click" Visible="False">از حالت مسدود خارج کردن این شخص</asp:LinkButton>
            </section>

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

<!--/body-->
</form>
<script language="javascript">
	ShowItems('1', 'ShowGuestPosts');
</script>
<asp:PlaceHolder ID="GoogleAnalyticsSection" runat="server"></asp:PlaceHolder>
</body>
</html>
