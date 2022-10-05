<%@ Page Language="C#" AutoEventWireup="true" CodeFile="commenting.aspx.cs" Inherits="services.commenting" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<HEAD>
<TITLE dir="rtl">نظرات</TITLE>
<META http-equiv=content-type content="text/html; charset=utf-8">
<LINK href="commenting/comments.css" type="text/css" rel="stylesheet">
<LINK href="commenting/commentsDefault.css" type="text/css" rel="stylesheet">
<SCRIPT language=javascript src="commenting/farsiEditor.js" type="text/javascript"></SCRIPT>
<SCRIPT language=javascript src="commenting/comments.js" type="text/javascript"></SCRIPT>
</HEAD>
<BODY>
<center>
	<div id="loaderImg" class="ajax"></div>
	<div id="resultText" class="ajax" runat="server"></div>
<form runat="server">
    <DIV ID="LogoutPanel" dir="rtl" class=CommentForm runat="server" style="font-size:8pt">
        <a href="javascript:void(0);" onClick="PreverifyActivation('<%=this.Request.QueryString["BlogID"]%>','<%=this.Request.QueryString["PostID"]%>','activate')" id="PreverifyActivate" style="display:<%=this.HiddenActivateDisplay.Value%>">فعال سازی نمایش نظرات این پست، قبل از تایید</a> 
        <a href="javascript:void(0);" onClick="PreverifyActivation('<%=this.Request.QueryString["BlogID"]%>','<%=this.Request.QueryString["PostID"]%>','deactivate')" id="PreverifyDeactivate" style="display:<%=this.HiddenFieldDeactivateDisplay.Value%>">غیرفعال  سازی نمایش نظرات این پست، قبل از تایید</a>
    	<a href="?action=logout&BlogID=<%=this.Request.QueryString["BlogID"]%>&PostID=<%=this.Request.QueryString["PostID"]%>">خروج از بخش مدیریت نظرات</a>
</DIV>
    <DIV class=CommentForm dir="rtl" runat="server" id="LoginPanel">
        <DIV ID="َAdminEnterPanel" dir="rtl" class="ajax" style="width:250px;text-align:center">
      <a href="javascript:void(0);" style="display:block" onClick="document.getElementById('TableLogin').style.display='block';document.getElementById('َAdminEnterPanel').style.display='none';">ورود مدیر این وبلاگ برای مدیریت نظرات این پست</a>
</DIV>
<TABLE id="TableLogin" class=CommentForm cellSpacing=0 cellPadding=0>
  <TBODY>
  <TR>
    <TD class=form><SPAN class=form onclick=username.focus()>نام کاربری</SPAN></TD>
    <TD><asp:TextBox runat="server" ID="username" MaxLength="12" size="30" dir="ltr"/></TD>
    <TD>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="username"
            ErrorMessage="نام کاربری خالی است." Font-Size="XX-Small"></asp:RequiredFieldValidator></TD></TR>
  <TR>
    <TD class=form><SPAN class=form onclick=password.focus()>کلمه عبور</SPAN></TD>
    <TD><asp:TextBox runat="server" ID="password" MaxLength="12" size="30" dir="ltr" TextMode="Password"/></TD>
    <TD>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="password"
            ErrorMessage="کلمه عبور خالی است." Font-Size="XX-Small"></asp:RequiredFieldValidator></TD></TR>
      <TR>
    <TD style="height: 25px"><asp:HiddenField ID="HiddenPostID" runat="server" /></TD>
    <TD style="PADDING-TOP: 5px; height: 25px;">
    <asp:Button runat="server" CssClass="button" ID="DoLogin" Text="ورود" OnClick="DoLogin_Click"/>
    </TD>
    <TD style="height: 25px">
        <asp:HiddenField ID="HiddenBlogID" runat="server" />
    </TD></TR>
    </TBODY></TABLE><asp:HiddenField ID="HiddenActivateDisplay" runat="server" /><asp:HiddenField ID="HiddenFieldDeactivateDisplay" runat="server" />
</DIV>
</form>
<DIV class=CommentForm dir="rtl">
<DIV id=divLanguageButton onMouseOver="hoverLanguageButton('in')" 
onclick=toggleLanguage() onMouseOut="hoverLanguageButton('out')">FA</DIV>
<TABLE class=CommentForm id=Table1 cellSpacing=0 cellPadding=0>
  <TBODY>
  <TR>
    <TD class=form><SPAN class=form onclick=name.focus()>نام</SPAN></TD>
    <TD><input onKeyPress=FKeyPress() id=name onBlur=hideLanguageButton() 
      onFocus=showLanguageButton(this) maxlength=50 size=50 
      name=name></TD>
    <TD style="WIDTH: 86px"></TD></TR>
  <TR>
    <TD class=form><SPAN class=form onclick=email.focus()>پست 
    الکترونیکی</SPAN></TD>
    <TD><INPUT id=email dir=ltr maxLength=50 size=50 name=email></TD>
    <TD></TD></TR>
  <TR>
    <TD class=form><SPAN class=form onclick=url.focus()>وب سایت</SPAN></TD>
    <TD><INPUT id=url dir=ltr maxLength=100 size=50 
      name=url></TD>
    <TD></TD></TR>
  <TR>
    <TD class=form><SPAN class=form onclick=CommentContent.focus()>متن 
    پیام</SPAN></TD>
    <TD><TEXTAREA onKeyPress="if (this.value.length > 2048) return false; FKeyPress()" onpaste="return pasteComment()" id="CommentContent" onBlur="hideLanguageButton()" onFocus="showLanguageButton(this)" name="CommentContent" rows="6"></TEXTAREA></TD>
    <TD>
    	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="لبخند" onClick="insertSmiley('لبخند')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/smiling.gif">
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="ناراحت" onClick="insertSmiley('ناراحت')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/sad.gif"> 
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="خنده" onClick="insertSmiley('خنده')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/laughing.gif"> 
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="گریه" onClick="insertSmiley('گریه')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/crying.gif">
        <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="چشمک" onClick="insertSmiley('چشمک')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/winking.gif">
        <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="نیشخند" onClick="insertSmiley('نیشخند')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/big-grin.gif">
	    <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="زبان" onClick="insertSmiley('زبان')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/tongue.gif">
        <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="خجالت" onClick="insertSmiley('خجالت')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/blushing.gif">
        <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="قلب" onClick="insertSmiley('قلب')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/love-struck.gif"> 
        <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="ماچ" onClick="insertSmiley('ماچ')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/kiss.gif">
        <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="تعجب" onClick="insertSmiley('تعجب')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/surprised.gif">
        <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="عصبانی" onClick="insertSmiley('عصبانی')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/angry.gif">
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="سوال" onClick="insertSmiley('سوال')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/confused.gif"> 
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="عینک" onClick="insertSmiley('عینک')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/cool.gif">
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="شیطان" onClick="insertSmiley('شیطان')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/devil.gif">
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="سبز" onClick="insertSmiley('سبز')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/sick.gif">
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="گل" onClick="insertSmiley('گل')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/rose.gif">
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="خداحافظ" onClick="insertSmiley('خداحافظ')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/buy.gif">
      	<IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="تحسین" onClick="insertSmiley('تحسین')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/hand.gif">
   	  <IMG class="smiley" dir="rtl" onMouseOver="hoverSmiley(this, 'in')" title="قاه قاه" onClick="insertSmiley('قاه قاه')" onMouseOut="hoverSmiley(this, 'out')" src="commenting/laugh.gif">      </TD></TR>
  <TR>
    <TD colspan="2" align="right"><label>
      <input type="checkbox" name="IsPrivateComment" id="IsPrivateComment">
      نظر بصورت خصوصی برای نویسنده وبلاگ ارسال شود</label></TD>
    <TD></TD></TR>
      <TR>
    <TD style="height: 25px"></TD>
    <TD style="PADDING-TOP: 5px; height: 25px;">
    <INPUT class="button" id="postBtn" name="postBtn" type="submit" value="ارسال پیام" onClick="return PostComment('postBtn','name','email','url','CommentContent','resultText','loaderImg','<%=this.Request.QueryString["BlogID"]%>','<%=this.Request.QueryString["PostID"]%>','IsPrivateComment');"> 
	<INPUT class="button" name="clearBtn" id="clearBtn" onClick="ClearFields('name','url','email','CommentContent','IsPrivateComment')" value="پاک کردن" type="reset">    </TD>
    <TD style="height: 25px"></TD></TR>
    </TBODY></TABLE>
    </DIV>
    <asp:Label ID="mesage" runat="server" Font-Names="Tahoma" Font-Size="XX-Small" ForeColor="Red" style="text-align:center" Visible="False">نظر شما در مورد این مطلب پس از تایید نویسنده وبلاگ نمایش داده خواهد شد</asp:Label>
<!--------------------------------------------------!-->
<div id="pContainer" class="ajax"></div>
<!--------------------------------------------------!-->

<DIV class=Footer align="center"> <a href="http://www.iranblog.com/" target="_blank">IranBlog.com</a> ©
  2002-2008.</DIV>
  </center>
<script language="javascript">
	if(document.getElementById('TableLogin') != null)
		document.getElementById('TableLogin').style.display='none';
	baShowUp('<%=this.Request.QueryString["BlogID"]%>','<%=this.Request.QueryString["PostID"]%>', 'loaderImg', 'pContainer', 'resultText');
</script>
</BODY></HTML>