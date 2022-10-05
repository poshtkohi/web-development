<%@ Page Language="C#" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="Peyghamak.faq" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title>سوال و جواب</title>
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
        
        <p><strong>پيغامك چيست؟</strong><br />
پيغامك يك سيستم پیغامک كوچك است كه در آن مي‌توانيد نوشته‌هاي خود را در  حداكثر 450 كاراكتر ثبت كنيد. شما در پيغامك مي‌توانيد علاوه بر وب از  طريق موبايل خود نيز به وسيله پيامك (SMS) پیغامک خود را به روز كنيد و در  آينده نزديك مي‌توانيد نوشته‌هاي افراد مورد علاقه خود را نيز از طريق  پيامك بر روي موبايل خود دريافت كنيد. لازم به ذكر است كه پيغامك روايت  ايراني‌شده‌ي سيستم‌هاي ميني پیغامک مانند twitter و jaiku مي‌باشد كه در  اين راه تلاش شده است ويژگي‌هاي مورد نياز براي كاربر ايراني فراهم شود. </p>
    
	    <p> <strong> صفحه پیغامک من در پيغامك به چه صورت است؟</strong> <br />
	      پس از ثبت نام با توجه به نام كاربري شما(yourid) يك آدرس به صورت  yourid.peyghhmak.com به شما اختصاص پيدا مي‌كند. شما در صفحه شخصي خود  امكان ثبت نوشته‌هاي خود را داريد و در ضمن مي‌توانيد نوشته‌هاي دوستان و  افراد مورد علاقه خود را دنبال كنيد.</p>
	    <p> <strong> مفهوم دوست و دنبال كننده چيست؟</strong> <br />
	      شما مي‌توانيد نوشته‌هاي هر پیغامک پيغامك را مشترك شويد و در هنگام ورود  به صفحه شخصي خود در بخش &quot;دوستان&quot; آخرين نوشته‌هاي دوستان خود را به ترتيب  زماني مشاهده كنيد. هنگامي كه شما مشترك نوشته‌هاي شخصي مي‌شويد در حقيقت  نوشته‌هاي آن شخص را در صفحه شخصي خود دنبال مي‌كنيد اين مفهوم در پيغامك  با اصطلاح دوست شناخته مي‌شود. در حقيقت دوست شما كسي است كه شما تمايل  داريد نوشته‌هاي او را در صفحه شخصي خود دنبال كنيد. شايد واژه بهتر براي  دوست واژه دنبال كردن مي‌بود ولي در پيغامك به خاطر بار معنايي بالاتر  واژه دوست، از اين واژه استفاده شده است. قابل ذكر است كه در آينده نزديك  امكان اشتراك نوشته‌هاي دوستان و افراد مورد علاقه شما از طريق ارسال به  موبايل فراهم خواهد شد. به همين ترتيب مفهوم دنبال‌كننده تعريف مي‌شود به  اين معنا كه چه افرادي مشترك نوشته‌هاي شما هستند و شما را به عنوان دوست  خود برگزيده‌اند. </p>
	    <p><strong>پيغام خصوصي چيست؟</strong><br />
	      در پيغامك شما مي‌توانيد براي افرادي كه نوشته‌هاي شما را دنبال مي‌كنند  پيغام خصوصي بفرستيد. براي اين كار كافي است به صفحه دنبال‌كنندگان خود  برويد و گزينه &quot;پيغام بفرست&quot; را انتخاب كنيد. در ضمن پيغام‌هاي خصوصي  ارسال شده براي شما در بخش &quot;پيغام شخصي&quot; قابل مشاهده است. </p>
	    <p><strong>آيا امكان نوشتن نظر براي نوشته‌هاي كاربران وجود دارد؟</strong><br />
	      بله هر كسي كه در پيغامك وارد شده باشد مي‌تواند نظر خود را در رابطه با  نوشته‌هاي كاربران ديگر در سيستم ثبت كنند و امكان مشاهده نظرات ديگران را  نيز دارد.</p>
	    <p> <strong> شماره پيامك( SMS ) پيغامك چيست؟</strong> <br />
	      شماره پيامك (SMS) سايت پيغامك 30007654321010 مي‌باشد. شما مي‌توانيد با ورود  به بخش تنظيمات موبايل در صفحه شخصي خود شماره موبايل خود را وارد كنيد و  پس از تاييد آن به راحتي مي‌توانيد با ارسال نوشته‌هاي خود به اين شماره  پيغامك خود را به روز كنيد. خوشبختانه به تازگي امكان ارسال از طريق هر سه  اپراتور همراه اول، تاليا و ايرانسل فراهم شده است.</p>
	    <p><strong>آيا نوشته‌هاي من در پيغامك براي همه آشكار است؟</strong><br />
	      بله تمامي نوشته‌هاي عمومي شما در پيغامك براي همگان آشكار است ولي  پيغام‌هاي خصوصي كه براي دنبال‌كنندگان خود مي‌فرستيد غير قابل مشاهده است.</p>
	    <p> <strong> آيا در پيغامك مي‌توانم فرد خاصي را مسدود كنم به اين معنا كه آن شخص نتواند نوشته‌هاي من را دريافت كند؟</strong> <br />
	      در پيغامك تصميم به اين گرفته شده است كه امكان مسدود كردن افراد وجود  نداشته باشد هر چند در سايت‌هاي مشابه اين امكان وجود دارد ولي در پيغامك  تصميم گرفته شده است كه ماهيت كار به يك پیغامک معمولي نزديكتر باشد.</p>
	    <p><strong>هزينه به كارگيري پيغامك چيست؟</strong><br />
	      پيغامك به عنوان يك سايت ايراني همواره در حال گسترش خواهد بود و همواره  امكانات پايه‌ي آن براي همگان رايگان خواهد بود. در آينده با پياده‌سازي  برخي از ويژگي‌هاي خاص امكان دريافت هزينه براي به كارگيري آن ويژگي‌ها  وجود دارد.</p>
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
