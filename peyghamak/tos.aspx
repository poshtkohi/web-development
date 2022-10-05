<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tos.aspx.cs" Inherits="Peyghamak.tos" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title>شرایط استفاده از پیغامک</title>
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
        
        <p><strong>شرايط به‌كارگيري پيغامك به قرار زير است:</strong><br />
            1- حداقل سن 13 سال<br />
            2- رعايت كامل قوانين كشور جمهوري اسلامي ايران و قوانين بين‌المللي.<br />
            3- عدم درج نوشته‌هايي برخلاف قوانين كشور.<br />
            4- عدم به كارگيري مطالب و تصاوير ركيك و مبتذل در نوشته‌ها و نيز به عنوان نام كاربري<br />
            5- عدم درج نوشته‌هايي حاوي كپي‌رايت<br />
            6- مسووليت نوشته‌ها به عهده كاربر است و پيغامك هيچ مسووليتي در اين رابطه ندارد.<br />
            7- پيغامك اين حق را براي خود محفوظ مي‌داند كه نوشته‌هاي كاربراني را كه  قوانين را رعايت نكنند حذف كند و در صورت تكرار حق حذف كاربر را نيز دارد.<br />
            8- كپي‌رايت نوشته‌ها براي كاربر محفوظ است.<br />
            9- اطلاعت كاربر در نزد سايت محفوظ است و در اختيار غير قرار نخواهد گرفت.<br />
            10- پيغامك اين حق را براي خود محفوظ مي‌داند كه نسبت به كارگيري آگهي اقدام كند.</p>
    
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
