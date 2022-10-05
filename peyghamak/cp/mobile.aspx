<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mobile.aspx.cs" Inherits="Peyghamak.cp.mobile" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>تنظیمات موبایل</title>
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
        <span class="setting_tab"><a href="picture.aspx">عکس</a></span>
        <span class="setting_tab_selected">موبایل</span>
        <span class="setting_tab"><a href="messenger.aspx">مسنجر</a></span>
        <span class="setting_tab"><a href="view.aspx">نما</a></span>
        <span class="setting_tab_right"><a href="default.aspx">مشخصات</a></span>    
    </div>
    <div id="settings">

<form runat="server">
    <table width="100%" border="0" cellspacing="2" cellpadding="2">
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle" class="info" dir="rtl"><asp:Label ID="message" runat="server" Visible="false"></asp:Label></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td width="500" align="center" valign="middle"><hr  /></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle"> شماره موبایل</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle">
        <div dir="rtl">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="MobileNumber"
                         ErrorMessage="شماره موبایل خالی است." Display="Dynamic"></asp:RequiredFieldValidator>
                         <br/>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                         ControlToValidate="MobileNumber" ErrorMessage="شماره موبایل نامعتبر است، مثال: 09127852176" ValidationExpression="(09)?[0-9]{9}" Display="Dynamic"></asp:RegularExpressionValidator></div>
        <asp:TextBox ID="MobileNumber" MaxLength="20" runat="server" Enabled="False" style="text-align:left"></asp:TextBox></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle"><asp:Button CssClass="settings_input" ID="add" runat="server" Text="ذخیره" Enabled="False" OnClick="add_Click" />             
                     <asp:Button CssClass="settings_input" ID="delete" runat="server" Text="حذف" Enabled="False" OnClick="delete_Click" /></td>
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
