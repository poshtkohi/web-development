<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="Peyghamak.about" %>	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title>درباره پیغامک</title>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />

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
        <div id="container">
        <div id="logo_big"></div>
        
        <p><span lang="fa">
		<img border="0" height="16" src="http://www.peyghamak.com/images/attention.gif" width="16" /></span> 
		گروه پيغامك كار بر روي اين سايت را از نخستين روزهاي<span lang="fa"> سال</span> 
		1386 آغاز كرد و در واپسين روزهاي مردادماه 1386 نسخه آزمايشي سايت آغاز به 
		كار كرد. افراد گروه:<br />
		    <br />
<span lang="fa">مهندس </span>عليرضا پشت كوهي
		</p>
				<p>
			<span lang="fa">
			<img border="0" src="http://www.peyghamak.com/images/attention.gif" width="16" height="16"> 
			</span> پیغامک یک میکرو بلاگ است. اين سيستم در محيط 3.5 Microsoft .NET توسعه پيدا كرده است و در پياده‌سازي آن از AJAX استفاده شده است. طراحي پيغامك به صورت توزيع‌شده انجام شده است و قابليت نصب در سيستم‌هاي خوشه‌اي و مشبك را به خوبي داراست و امكان رشد سيستم به صورت نمايي در طراحي اوليه آن لحاظ شده است. آنچه كه هم‌اكنون موجود است هسته اوليه اين محيط است و به تدريج امكانات لازم به آن اضافه خواهد شد. </p>
			<p>
			&nbsp;</p>
			<p>
			&nbsp;</p>
			<p>
			&nbsp;</p>
    
	</div>
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
<asp:PlaceHolder ID="GoogleAnalyticsSection" runat="server"></asp:PlaceHolder>
</body>
</html>
