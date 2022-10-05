<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Peyghamak.cp.view" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>انتخاب نمای سایت</title>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://www.peyghamak.com/js/Functions.js"></script>
<title>آپلود عکس خود</title>
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
</head>

<body>
<!--body-->
<div id="body">

<!--menu inner-->
<div id="menu">
    <div id="menu_inner">
        <asp:Panel ID="LoginedPanel" runat="server" EnableViewState="False">
          <asp:HyperLink ID="MyPageHperLink" Text="پیغامک خودم" runat="server" Target="_self" NavigateUrl="/"></asp:HyperLink> | 
              <asp:HyperLink ID="SignoutHyperLink" runat="server" Target="_self" Text="خروج از سایت" NavigateUrl="/signout.aspx"></asp:HyperLink>
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
    <p>&nbsp;</p>
    <div id="settings_tabs">
        <span class="setting_tab"><a href="password.aspx">کلمه عبور</a></span>
        <span class="setting_tab"><a href="picture.aspx">عکس</a></span>
        <span class="setting_tab"><a href="mobile.aspx">موبایل</a></span>
        <span class="setting_tab"><a href="messenger.aspx">مسنجر</a></span>
        <span class="setting_tab_selected">نما</span>
        <span class="setting_tab_right"><a href="default.aspx">مشخصات</a></span>    
    </div>
    <div id="settings">
      <div id="setting_messages">یکی از این نماها را برای پیغامک خود انتخاب کنید.</div>
      <div id="view"> <a href="?theme=ff1&id=2" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('ff1');" onmouseout="imageFadeIn('ff1');"><img id="ff1" name="ff1" src="../theme/bg/thumb_ff1.jpg" /></a> <a href="?theme=circles1&id=3" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('circles1');" onmouseout="imageFadeIn('circles1');"><img id="circles1" name="circles1" src="../theme/bg/thumb_circle1.jpg" /></a> <a href="?theme=blood1&id=4" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('blood1');" onmouseout="imageFadeIn('blood1');"><img id="blood1" name="blood1" src="../theme/bg/thumb_blood1.jpg" /></a> <a href="?theme=branches1&id=5" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('branches1');" onmouseout="imageFadeIn('branches1');"><img id="branches1" name="branches1" src="../theme/bg/thumb_branches1.jpg" /></a> <a href="?theme=cont1&id=6" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('cont1');" onmouseout="imageFadeIn('cont1');"><img id="cont1" name="cont1" src="../theme/bg/thumb_cont1.jpg" /></a> <a href="?theme=cont2&id=7" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('cont2');" onmouseout="imageFadeIn('cont2');"><img id="cont2" name="cont2" src="../theme/bg/thumb_cont2.jpg" /></a> <a href="?theme=music1&id=8" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('music1');" onmouseout="imageFadeIn('music1');"><img id="music1" name="music1" src="../theme/bg/thumb_music1.jpg" /></a> <a href="?theme=chaost1&id=9" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('chaost1');" onmouseout="imageFadeIn('chaost1');"><img id="chaost1" name="chaost1" src="../theme/bg/thumb_chiaos1.jpg" /></a> <a href="?theme=default&id=1" title="انتخاب این نما برای پیغامکتان" onmouseover="imageFadeOut('default');" onmouseout="imageFadeIn('default');"><img id="default" name="default" src="../theme/bg/thumb_default.jpg" /></a> </div>
      <p></p>
      <div style="text-align:center;"></div>
      <p></p>
    </div>
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
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>          
</div>
<!--
<div class="right_sidebar_title_fill">
    <div class="right_sidebar_text_2">
    
    </div>
</div>
-->

<div class="right_sidebar_btm">&nbsp;&nbsp;&nbsp;&nbsp;</div>
</div>
<!--/right-->


</div>
<!--/body-->

</body>
</html>
