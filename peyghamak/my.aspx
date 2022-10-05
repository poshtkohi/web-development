<%@ Page Language="C#" AutoEventWireup="true" CodeFile="my.aspx.cs" Inherits="Peyghamak.my" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title id="title" runat="server"></title>
<link rel="alternate" type="application/rss+xml" href="/rss.aspx" runat="server" id="feed"/>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<script type="text/javascript" src="http://www.peyghamak.com/js/AjaxCore.js"></script>
<script type="text/javascript" src="http://www.peyghamak.com/js/Functions.js"></script>

</head>

<section id="AlertSection" runat="server">
<body onload="TagToTip('T2tDirectAlertMessage', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8, FIX, [450, 30]); setTimeout('UnTip()',20000);">
<script type="text/javascript" src="http://www.peyghamak.com/js/wz_tooltip.js"></script> 
<script type="text/javascript" src="http://www.peyghamak.com/js/tip_balloon.js"></script>
<div id="T2tDirectAlertMessage" style="display:none">
	<div class="tooltipDiv" id="AlertMessageText">
		[AlertMessageText]
		</div>
</div>
</section>

<section id="NoAlertSection" runat="server">
<body>
</section>  

<form id="form" runat="server" onsubmit="return false;">
<!--body-->
<div id="body">

<!--menu inner-->
<div id="menu">
    <div id="menu_inner">
    	<a href="cp/">تنظیمات</a> | <a href="signout.aspx">خروج از سایت</a> | <a href="http://www.peyghamak.com/home.aspx">صفحه اصلی</a>
    </div>
    <div class="menu_message" id="messagee" style="display:none">
    	>> به لیست دوستانتان اضافه شد.
    </div>
</div>
<!--/menu inner-->

<!--left-->
<div id="left">

	<!--post-->
	<div id="post"> 
    
		<asp:Image runat="server" ID="MyImage" Width="75" Height="75" class="avatar"/>
    
    <div id="message_box"></div>
    
    <textarea class="message_box" id="PostContent" name="PostContent" onkeydown="FKeyDown();" onkeypress="FKeyPress();" onkeyup="charCounter('PostContent','counter');" style="text-align:right; direction:rtl"></textarea>
    
    <button id="dopost" class="do_post" onclick="DoPost('my');">بفرست</button>
    
    <div id="counter">450</div>
    
    <div id="buttons_bar">
    <span class="help_btn" title="راهنما"></span>
    <span class="ltr_btn" title="از چپ به راست" onclick="changeLanguage('PostContent',false);"></span>
    <span class="rtl_btn" title="از راست به چپ" onclick="changeLanguage('PostContent',true);"></span>
    </div>
    
	<div id="tabs_bar">
    <span class="friends_tab" id="friends" onclick="ShowItems('1', 'ShowMyFriendsPosts');" style="cursor:pointer" title="دوستان"></span>
    <span class="my_tab" id="my" onclick="ShowItems('1', 'ShowMyPosts');" style="cursor:pointer" title="خودم"></span>
    <span class="pm_tab" id="private" onclick="ShowItems('1', 'ShowPrivateMessages');" style="cursor:pointer" title="پیغام خصوصی"></span>
    <span class="favorites_tab" id="starred" onclick="ShowItems('1', 'ShowStarredPosts');" style="cursor:pointer" title="پیغامک های منتخب"></span>
    </div>
    
    </div>
    <!--/post-->
    
    <!--left fill-->
    <div id="left_fill">
    <div align="right" class="date">
                    خوش آمدی <asp:Label ID="MyNameLabel" runat="server" /></div>
    <div id="loaderImg" align="center"><img src="http://www.peyghamak.com/images/loading.gif"/></div>
    <div id="resultText"></div>
    <p>&nbsp;</p>
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
	آمار
</div>
</div>

<div class="right_sidebar_fill">
<div id="avatar_box">
پیغامک ها:
          <asp:Label ID="PostNumLabel" runat="server" EnableViewState="False" >0</asp:Label>
          <br />
          پیغام های خصوصی:
          <asp:Label ID="PrivateMessagesNumLabel" runat="server" EnableViewState="False" >0</asp:Label>
          <br />
          دوستان:
          <asp:Label ID="FriendNumLabel" runat="server" EnableViewState="False" >0</asp:Label>
          <br />
	<asp:HyperLink ID="FollowerNumHyperlink" runat="server" Target="_self" NavigateUrl="friends.aspx?mode=followers" EnableViewState="False" Text="دنبال کنندگان: "></asp:HyperLink>
	<asp:Label ID="FollowerNumLabel" runat="server" EnableViewState="False" >0</asp:Label>
                    </div>
</div>

<div class="right_sidebar_title_fill">
<div class="right_sidebar_text_2">
<a href="javascript:void(0);" onclick="ShowItems('1', 'ShowAllComments');">نظرات پیغامک شما</a></div>
</div>

<div class="right_sidebar_fill">
        	<div id="loaderImgShowAllComments" align="center"><img src="http://www.peyghamak.com/images/loading.gif"/></div>
            <div id="resultTextShowAllComments"></div>      
            <script language="javascript">
				ShowItems('1', 'ShowAllComments');
			</script>           
</div>

<div class="right_sidebar_title_fill">
<div class="right_sidebar_text_2">
<a href="javascript:void(0);" onclick="ShowItems('1', 'ShowAllStarredComments')">نظرات پیغامک های برگزیده شما</a></div>
</div>

<div class="right_sidebar_fill">
			<div id="loaderImgShowAllStarredComments" align="center"><img src="http://www.peyghamak.com/images/loading.gif"/></div>
            <div id="resultTextShowAllStarredComments"></div>      
            <script language="javascript">
				ShowItems('1', 'ShowAllStarredComments');
			</script> 	   
</div>

<div class="right_sidebar_btm">&nbsp;&nbsp;&nbsp;&nbsp;</div>
</div>
<!--/right-->


</div>
<!--/body-->
<script language="javascript">
	ShowItems('1', 'ShowMyPosts');
</script>
	<asp:PlaceHolder ID="GoogleAnalyticsSection" runat="server"></asp:PlaceHolder><strong></strong>

</form>

</body>
</html>
