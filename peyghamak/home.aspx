<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="Peyghamak.home" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title> به جامعه پیغامک خوش آمدید </title>

<link href="http://www.peyghamak.com/Styles/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://www.peyghamak.com/js/AjaxCore.js"></script>
<script type="text/javascript" src="http://www.peyghamak.com/js/Functions.js"></script>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
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
              <asp:HyperLink ID="SetttingsHperLink" Text="تنظیمات" runat="server" Target="_self" style=""></asp:HyperLink> | 
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
    <p>
    <div id="peyghamak"></div>
    </p>   
    
    <p class="simple_text">
    شما می‌توانید لحظات خود، تفكرات خود و نوشته‌های كوتاه خود را در این مكان ثبت كنید.<br/>
شما می‌توانید با انتخاب موضوعی در پیغامك نوشته‌های كوتاه خود را در رابطه با آن موضوع بیان كنید.<br/>
شما می‌توانید از پیغامك به عنوان صفحه اطلاع رسانی سازمان و یا شركت خود استفاده كنید. 	
  	 <br/>
شما می‌توانید نوشته‌های خود را از طریق وب و پیامك(SMS) ارسال كنید و به اشتراك بگذارید. <br/>
شما می‌توانید در پیغامك نوشته‌های افراد مورد علاقه خود را طریق وب و پیامك(SMS) دریافت كنید.
    </p> 
    <p>
    <p>&nbsp;</p>
    <p class="title" onclick="ShowItems('1', 'ShowLatestPosts');" style="cursor:pointer">آخرین پیغامک های دوستان شما:</p>
<!-------------------------------------------------- -->
                    <div id="loaderImg" align="center"><img src="http://www.peyghamak.com/images/loading.gif"/></div>
                    <div id="resultText"></div>
<!-------------------------------------------------- -->
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
<HomePageLoginSection id="HomePageLoginSection" runat="server">
<div class="right_sidebar_fill">
    <p>&nbsp;</p>
  	<form action="signin.aspx?mode=h" method="post">
    <div id="login_compact">
    نام کاربری:
    <input type="text" name="username" id="username"  class="login_txtbox"/>
    کلمه عبور:
    <input type="password" name="password" id="username" class="login_txtbox" />
    <input type="submit" value="ورود" class="login_btn"/>
    </div>
    </form>
    <a href="signup.aspx"><div id="join_us"></div></a>
</div>
</HomePageLoginSection>

<div class="right_sidebar_title_fill">
    <div class="right_sidebar_text_2">
    محبوب ترین پیغامک ها
    </div>
</div>

<div class="right_sidebar_fill">
    
	<div class="gap2px"></div>
	<asp:Label EnableViewState="false" ForeColor="#FF0000" ID="TopStars" runat="server" Text='پیغامک محبوبی وجود ندارد.'></asp:Label> 
    <p>&nbsp;</p>     
</div>

<div class="right_sidebar_title_fill">
    <div class="right_sidebar_text_2">
    پرنظر ترین پیغامک ها
    </div>
</div>

<div class="right_sidebar_fill">

	<div class="gap2px"></div>
	<asp:Label EnableViewState="false" ForeColor="#FF0000" ID="TopComments" runat="server" Text="نظری وجود ندارد."></asp:Label>       
    <p>&nbsp;</p>
</div>

<div class="right_sidebar_title_fill">
    <div class="right_sidebar_text_2">
    آخرین دوستان ما
    </div>
</div>
<div class="right_sidebar_fill">
	<div class="gap2px"></div>
	<div id="avatar_box">
	<div id="ListTopUsers" runat="server" enableviewstate="false"></div>     
    </div>
</div>

<div class="right_sidebar_btm">&nbsp;&nbsp;&nbsp;&nbsp;</div>
</div>
<!--/right-->


</div>
<!--/body-->
<script language="javascript">
	ShowItems('1', 'ShowLatestPosts');
</script>
<asp:PlaceHolder ID="GoogleAnalyticsSection" runat="server"></asp:PlaceHolder><strong></strong>
</body>
</html>
