<%@ Page Language="C#" AutoEventWireup="true" CodeFile="comments.aspx.cs" Inherits="Peyghamak.comments" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title>نظرات</title>
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/Styles/temp.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://www.peyghamak.com/js/AjaxCore.js"></script>
<script type="text/javascript" src="http://www.peyghamak.com/js/Functions.js"></script>
<script type="text/javascript">
	<% this.Response.Write(String.Format("_currentPostID='{0}';", this.Request.QueryString["PostID"])); %>
</script>
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
    <div class="menu_message" id="messagee">
    	
    </div>
</div>
<!--/menu inner-->

<!--left-->
<div id="left">

	<!--post-->
	<div id="comment"> 
    	<div class="comment_right_box">
        	<asp:Image runat="server" ID="UserImage" Width="75" Height="75" EnableViewState="false" class="avatar_comment"/>
        <div id="op_compact">
        </div>
        </div>
        
        <div id="comment_detail"><a href="/">
                    <asp:Label ID="NameLabel" runat="server" EnableViewState="False" />                    
                  </a>:</div>
        
        <div id="comment_post" style="direction:rtl; text-align:right"><div id="PostContent" runat="server"></div></div>
    <div id="comments_date"><asp:Label ID="PostDate" runat="server"></asp:Label></div>
    
    </div>
    <!--/post-->
    
    <!--left fill-->
    <div id="left_fill">
    
    <p>
     <div id="loaderImg" align="center"><img src="http://www.peyghamak.com/images/loading.gif"/></div>
    <div id="resultText"></div>
    </p>
    <section id="PostSection" runat="server">
        <p>&nbsp;</p>
        <div id="comment_message_box">
       		 <textarea id="message" name="message" onkeydown="FKeyDown();" onkeypress="FKeyPress();" onkeyup="charCounter('message','commentCounter');" style="text-align:right; direction:rtl"></textarea>
            <span class="comment_message_box_conter" id="commentCounter">450</span>
        </div>
        <div id="comment_message_box_directions">
            <span class="comment_ltr" title="از چپ به راست" onclick="changeLanguage('message',false);"></span>
            <span class="comment_rtl" title="از راست به چپ" onclick="changeLanguage('message',true);"></span>
        </div>
            <div id="do_post_comments">
    	<button id="dopost" class="do_post_comments" onclick="DoPost('comments');">بفرست</button>
    </div>
        <p></p>
    </section>

      <section id="MustLoginToCommentSection" runat="server">
        <div align="center" class="error" dir="rtl">برای ارسال نظر شما باید یکی از اعضای سایت باشید. 
        برای ورود به سایت <a href="http://www.peyghamak.com/signin.aspx" style="text-decoration:none" class="singup_textbox">اینجا</a> و برای ثبت نام در سایت
        <a href="http://www.peyghamak.com/signup.aspx" class="singup_textbox" style="text-decoration:none">اینجا</a> را کلیک کنید.</div>
      </section>
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
<div class="right_sidebar_title_fill">
<div class="right_sidebar_text_2">
لینک ها</div>
</div>

<div class="right_sidebar_fill">
	<div id="avatar_box"> 
	<p>
   		<a href="/">صفحه اصلی پیغامک</a><br />
        <a href="friends.aspx?page=1&mode=friends">دوستان این شخص</a><br />
        <a href="friends.aspx?page=1&mode=followers">دنبال کنندگان این شخص</a><br />
        <a href="javascript:void(0);" onclick="ShowItems('1', 'ShowPostComments');">بار گذاری مجدد نظرات این پیغامک</a><br />
        <br />
    </p><!--bezar bemone-->
    <p>&nbsp;</p>
    </div>
</div>

<div class="right_sidebar_title_fill">
<div class="right_sidebar_text_2">
	<span lang="fa">تعداد نظرات این پیغامک: <asp:Label ID="NumCommentsLable" runat="server">0</asp:Label></span>
	<p><br /><br /></p>
	<!--bezar bemone-->
    <p>&nbsp;</p>
	</div>
</div>

<div class="right_sidebar_btm">&nbsp;&nbsp;&nbsp;&nbsp;</div>
</div>
<!--/right-->


</div>
<!--/body-->
<script language="javascript">
	ShowItems('1', 'ShowPostComments');
</script>
<asp:PlaceHolder ID="GoogleAnalyticsSection" runat="server"></asp:PlaceHolder>
</body>
</html>
