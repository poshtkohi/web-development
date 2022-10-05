<%@ Page Language="C#" AutoEventWireup="true" CodeFile="picture.aspx.cs" Inherits="Peyghamak.cp.picture" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>آپلود عکس خود</title>
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
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
        <span class="setting_tab_selected">عکس</span>
        <span class="setting_tab"><a href="mobile.aspx">موبایل</a></span>
        <span class="setting_tab"><a href="messenger.aspx">مسنجر</a></span>
        <span class="setting_tab"><a href="view.aspx">نما</a></span>
        <span class="setting_tab_right"><a href="default.aspx">مشخصات</a></span>    
    </div>
    <div id="settings">

<form runat="server">
<table width="100%" border="0" cellspacing="2" cellpadding="2">
<tr>
        <td align="center" class="info" colspan="4" dir="ltr"><asp:Label ID="message" runat="server" Visible="false"></asp:Label></td>
      </tr>
  <tr>
    <td><asp:HiddenField ID="rets" runat="server" /></td>
    <td width="300" align="center" valign="middle" height="120">
    <div id="avatar">    
		<asp:Image Height="75px" ID="ProfileImage" runat="server" Width="75px" />
    </div>    
    </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="center" valign="middle"><hr  /></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="right" valign="middle" class="simple">حداقل ابعاد تصویر 75x75 می باشد.(.jpg, .gif, .png)<br />
حداکثر حجم عکس، 256 کیلوبایت است. </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td width="150px"><div dir="rtl"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="file"
                                  ErrorMessage="یک فایل انتخاب کنید." SetFocusOnError="True"></asp:RequiredFieldValidator></div></td>
    <td align="center" valign="middle"><input type="file" class="settings_input" id="file" runat="server"/></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="center" valign="middle"><asp:Button id="save" runat="server" onclick="save_Click" text="ذخیره" /></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="center" valign="middle" class="validation_tahoma"><div style="text-align:center"></div></td>
    <td>&nbsp;</td>
  </tr>
</table>
</form>

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
