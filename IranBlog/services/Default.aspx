<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
    if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<HTML xmlns="http://www.w3.org/1999/xhtml">
<HEAD>        
		<meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <title>IranBlog.com:  به نسخه جدید سایت ایران بلاگ خوش آمدید</title>
        <link rel="stylesheet" type="text/css" href="images/style.css" />
        <script type="text/javascript" src="/services/js/AjaxCore.js"></script>
        <script type="text/javascript" src="/services/js/MainPagesFunctions.js"></script>
<script type="text/javascript">
			function BeginRefresh(){
				ShowItems('1', 'ShowUpdates');
				return;
				setTimeout("BeginRefresh()",120000);
			}
        </script>
        <style type="text/css">
			.updates
			{
				font-size:8pt; font-family:tahoma; color:#F09;
			}
			.updates a
			{
				font-size:8pt; font-family:tahoma; color:#F09;
			}
			.updates a:hover
			{
				color:#00FF66; font-weight:bold; text-decoration:none;
			}
			.tooltipDiv {
	font-size:11px; font-family:tahoma; width:170px; direction:rtl; text-align:justify; color:#000000; font-weight:bold;
}
		#container #themebar #pagesettings div #LabelTime2 {
	text-align: right;
}
        </style>
		<link href="styles/blue.css" rel="stylesheet" rev="stylesheet">
</HEAD>
<BODY onLoad="BeginRefresh();">
<div id="MainDiv">
            <DIV id=container>
            <DIV class=ct>
              <div style="float: right; width: 240px; height: 101px; background: transparent url(images/iran.png) no-repeat right  center;"><img src="images/ib5.png"  align="bottom"></div>
             <div style=" float:left; width:200; height:100;"><img src="images/blog.png" align="left"></div>
             </DIV>
            
            <DIV id=toolbar>
            <DIV id=tools>
            <UL id=tool>
                               <asp:PlaceHolder ID="MainSiteHeaderControl" runat="server"></asp:PlaceHolder>
                  </UL></DIV></DIV>
            <DIV id=themebar>
            <DIV id=pagesettings align="right"><asp:Label ID="LabelTime" runat="server"></asp:Label> | <asp:Label ID="Onlines" runat="server">تعداد کل کاربران در حال استفاده از سرویس های ایران بلاگ: <span style="font-weight:bold; color:#F00"><%=(blogcontent.Onlines()+200).ToString()%></span></asp:Label>
            </DIV></DIV>
            
            <DIV id=sidebar>
            <DIV id=homeupdateblog>
            <DIV class=title onClick="BeginRefresh();" title="بار گزاری مجدد" style="cursor:pointer"><img src="images/weblog.png"></DIV>
                <div id="loaderImg" align="center"><img src="images/loading.gif"/></div>
                <div id="resultText"></div>
                <br>
            </DIV>
            
            <DIV id=homeupdateblog>
            <DIV class=title><img src="images/news copy.png"></DIV>
            <DIV>
            <UL>
                          <li><a href="http://alireza.iranblog.com/?mode=DirectLink&id=475317" target="_blank"><img src="images/dot.png"> &nbsp;طراحي جديد بخش "کلمه عبور فراموش شده"</a><br><br>
              <li><a href="http://alireza.iranblog.com/?mode=DirectLink&id=462399" target="_blank"><img src="images/dot.png"> &nbsp;کاربرانی که با سایت ایران بلاگ مشکل دارند حتما بخوانند</a><br><br>
              <li><a href="http://alireza.iranblog.com/?mode=DirectLink&id=461902" target="_blank"><img src="images/dot.png"> &nbsp;سال نو بر همه ايرانيان مبارك</a><br><br>
              <LI><a href="http://alireza.iranblog.com/?mode=DirectLink&id=460265" target="_blank"><img src="images/dot.png"> &nbsp;نسخه جدید ایران بلاگ راه اندازی شد</a><li><br><br>
               </UL>
            </DIV></DIV>
            
            
            </DIV>
            
            
            <DIV id=sidebar2>
            <HomePageLoginSection id="HomePageLoginSection" runat="server">
                <DIV id=homeupdateblog>
                <DIV class=title><img src="images/vorod.png" align="right"></DIV>
                <DIV>
                <!-- div id="message" class="message" style="display:none" align="center"></div -->
                    <td align="right" valign="middle" dir="rtl">
                                        <%
                       //------------------------------------------------------------------------------------------------
                       string i = this.Request.QueryString["i"];
                       if(i != null && i != "")
                       {
                          if(i == "logouted" || i == "userlogout" ||i == "unauthorized")
                          {
                             string error = "";
                             switch(i)
                             {
                                 case "logouted":
                                     error = ".جلسه شما منقضی شده است";
                                     break;
                                 case "userlogout":
                                     error = ".شدید LogOut شما با موفقیت";
                                     break;
                                 case "unauthorized":
                                     error = ".نام کاربری یا کلمه عبور اشتباه است";
                                     break;
                             }
                             this.Response.Write("<div style=\"font-size:8px;font-family:Tahoma; color:#FF0000\" dir=\"ltr\" align=center>" + error + "</div>");
                          }
                       }
                       //------------------------------------------------------------------------------------------------
                    %>
                 <form id="form" name="form" onSubmit="return LoginFormValidation();" method="post" runat="server">
                <br>
                 <table width="170" border="0" cellpadding="0" cellspacing="0">
                  <tbody><tr>
                                            <th>&nbsp;نام کاربری</th>
                                <td><input name="username" id="username" dir="ltr" style="width: 80px;" type="text" maxlength="12"></td>
                                        </tr>
                                        <tr>
                                          <th>&nbsp;کلمه عبور</th>
                                            <td><input name="password" id="password" dir="ltr" style="width: 80px;" maxlength="12" type="password"></td>
                                        </tr>
                                            <tr style="display:none" id="TeamWeblogSection">
                                            <th>&nbsp;وبلاگ</th>
                                                <td><input name="weblog" id="weblog" maxlength="30" dir="ltr" style="width: 80px;" type="text"></td>
                                            </tr>                   
                                            <tr>
                                            <th colspan="2" align="left" dir="rtl"><asp:CheckBox ID="cookieEnabled" runat="server" Text="مرا به خاطر بسپار؟" style="font-family: Tahoma;font-size: 7pt;color:#000000;width:100px"/></th>
                                            </tr>
                                        <tr>
                                            <th>&nbsp;</th>
                                          <td  valign="middle">
                                                <input type="image" src="images/enter.png" id="login_" name="login_" title="ورود"/><br>
                                          </td>
                                        </tr>
                            </tbody>
                </table>
                 
                 </form>
                 <p align="center">
                    <a href="javascript:MakeTeamWeblogSection(true);" id="addtb">ورود به وبلاگ های گروهی</a><br>
                    <a href="javascript:MakeTeamWeblogSection(false);" id="removetb" style="display:none">ورود مدیر اصلی وبلاگ</a>
                    <a href="javascript:void(0);" onClick="ShowPassForgetLayer(true);">کلمه عبور فراموش شده</a>
                 </p>
                 <p><br>
                 </p>
                </DIV></DIV>
            </HomePageLoginSection>
            <HomePageToolbarSection id="HomePageToolbarSection" runat="server">
                <DIV id=homeupdateblog>
                <DIV class=title><img src="images/menu.png"></DIV>
                <DIV>
                <UL>
                    <li><a href="blogbuilderv1/"><img src="images/dot.png"> &nbsp;میز کار کاربری شما</a><br><br>
                    <li><a href="http://<%=((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).Subdomain%>.iranblog.com/" target="_blank"><img src="images/dot.png"> &nbsp;نمایش وبلاگ</a><br><br>
                    <li><a href="blogbuilderv1/signout.aspx" target="_self"><img src="images/dot.png"> &nbsp;خروج از سایت</a>
                  </UL>
                </DIV></DIV>
            </HomePageToolbarSection>
            
            
            <DIV id=homeupdateblog>
            <DIV class=title><img src="images/tabligh.png"></DIV>
            <DIV align="center">
              <p><a href="http://www.peyghamak.com/" target="_blank" ><img width="120" src="http://www.peyghamak.com/Images/logo.png" alt="میکروبلاگ پیغامک-حتما عضو بشید" border="0" /></a></p>
              <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
              <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
            </DIV></DIV>
            
            
            
            
            </DIV>
            
            
            
            
            <DIV id=main>
            <DIV id=homebsdesc style="width:415px">ایرانبلاگ 
                                                            قویترین ابزار برای ساخت و مدیریت 
                                                            وبلاگ فارسی است . و به شما کمک 
                                                            میکند تا با سرعت و سهولت 
                                                            اطلاعات، خاطرات، مطالب و مقالات 
                                                            خود را در اینترنت منتشر کنید .</p>
                                                            <p align="center">
                                                            اختصاص یک وبلاگ رایگان فارسی&nbsp;&nbsp; 
                                                            http://Yourname.iranblog.com</p>
                                                            <p align="center">
                                                            <a href="http://www.iranblog.com/services/register.aspx"><br>
                                                            <img src="images/create.png"></a></DIV>
            <DIV id=homenews>
            <DIV class=title><img src="images/emkanat.png"></DIV>
            <span class="comment">
            » سرعت بسیار بالا - نداشتن تبلیغات در وبلاگها<br>
            » امکان آپلود نامحدود فایل و عکس<br>
            » امکان انتقال همه مطالب وبلاگهای بلاگفا و پرشین بلاگ به ایرانبلاگ <br>
            » چت باکس (تالار گفتمان وبلاگ) در وبلاگها (با امکان فعالسازی و عدم فعالسازی آن در کنترل پنل) <br>
            » امکان اضافه کردن صفحات اضافی به وبلاگ <br>
            » سیستم بسیار پیشرفته و انعطاف پذیر مدیریت لینک باکس وبلاگها<br>
            » امكان اضافه كردن نويسنده هاي وبلاگ با شناسه هاي متفاوت (وبلاگ گروهي)<br>
            » امكان موضوع بندي كردن وبلاگ<br>
            » سيستم قدرتمند جستجو گر<br>
            » امكان ارسال نظر خصوصي<br>
            » سيستم هاي هوشمند لينك دوني , لينكستان , خبرنامه ,نظر سنجي و ...<br>
            » امكان تغيير قالب نظر سنجي و قالب اصلي وبلاگ<br>
            » سيستم آماري بسيار دقيق و حرفه اي <br>
            » امكان استفاده از چندين آدرس پيش فرض براي وبلاگ<br>
            » امکان بروز رسانی وبلاگ از طریق sms <br>
            » و 10 ها امکان منحصر به فرد دیگر<br>
             </span></DIV>
            <DIV style="CLEAR: both; WIDTH: 450px; PADDING-TOP: 10px; HEIGHT: 60px; TEXT-ALIGN: center">
            <DIV style="TEXT-ALIGN: center"></DIV>
            </DIV></DIV>
            <div class=cb>
              <div class=cl>
                <div id=terms>
                  <asp:PlaceHolder ID="CopyrightFooterControl" runat="server"></asp:PlaceHolder>
                </div>
              </div>
            </div>
            </DIV>
</div>
                    <div id="ForgetPassLayer" style="position:absolute; visibility:hidden"></div>
</BODY></HTML>