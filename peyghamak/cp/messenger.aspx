<%@ Page Language="C#" AutoEventWireup="true" CodeFile="messenger.aspx.cs" Inherits="Peyghamak.cp.messenger" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>تنظیمات ياهو مسنجر</title>
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
        <span class="setting_tab"><a href="mobile.aspx">موبایل</a></span>
        <span class="setting_tab_selected">مسنجر</span>
        <span class="setting_tab"><a href="view.aspx">نما</a></span>
        <span class="setting_tab_right"><a href="default.aspx">مشخصات</a></span>    
    </div>
    <div id="settings">

<form runat="server">
    <table width="100%" border="0" cellspacing="2" cellpadding="2">
      <tr>
        <td>&nbsp;</td>
        <td align="right" class="info" colspan="2" dir="rtl"><asp:Label ID="message" runat="server" Visible="false"></asp:Label></td>
        <td width="30px">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="3"><hr  /></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle"> شناسه ياهو</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle">
        <div style="direction:rtl;">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="YahooId"
                         ErrorMessage="شناسه ياهو خالی است." Display="Dynamic"></asp:RequiredFieldValidator>
                     <br />
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                         ControlToValidate="YahooId" ErrorMessage="شناسه ياهو نامعتبر است، مثال: yahooid" ValidationExpression="[a-zA-Z0-9_.]*" Display="Dynamic"></asp:RegularExpressionValidator></div>
        <asp:TextBox ID="YahooId" MaxLength="20" runat="server" Enabled="False" style="text-align:left"></asp:TextBox></td>
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
