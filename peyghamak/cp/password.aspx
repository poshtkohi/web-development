<%@ Page Language="C#" AutoEventWireup="true" CodeFile="password.aspx.cs" Inherits="Peyghamak.cp.password" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>تغییر کلمه عبور</title>
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
        <span class="setting_tab_selected">کلمه عبور</span>
        <span class="setting_tab"><a href="picture.aspx">عکس</a></span>
        <span class="setting_tab"><a href="mobile.aspx">موبایل</a></span>
        <span class="setting_tab"><a href="messenger.aspx">مسنجر</a></span>
        <span class="setting_tab"><a href="view.aspx">نما</a></span>
        <span class="setting_tab_right"><a href="default.aspx">مشخصات</a></span>    
    </div>
    <div id="settings">

<form runat="server">
	    <table width="100%" border="0" cellspacing="2" cellpadding="2">
      <tr>
							<td align="right" class="info" colspan="4" dir="rtl"><asp:Label ID="message" runat="server" Visible="false"></asp:Label>
							</td>
						</tr>
      <tr>
        <td colspan="4">
		<hr/></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle" colspan="2"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td align="right" dir="rtl"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LastPassword" ErrorMessage="کلمه عبور قدیمی خالی است."></asp:RequiredFieldValidator></td>
            <td align="right"><asp:TextBox ID="LastPassword" MaxLength="50" runat="server" 
                                  TextMode="Password"></asp:TextBox></td>
            <td width="14">&nbsp;</td>
            <td width="100" align="left">کلمه عبور قدیمی</td>
          </tr>
          <tr>
            <td align="right" dir="rtl"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                  ControlToValidate="NewPassword" ErrorMessage="کلمه عبور جدید خالی است."></asp:RequiredFieldValidator></td>
            <td align="right"><asp:TextBox ID="NewPassword" MaxLength="50" runat="server" 
                                  TextMode="Password"></asp:TextBox></td>
            <td>&nbsp;</td>
            <td align="left">کلمه عبور جدید</td>
          </tr>
          <tr>
            <td align="right" dir="rtl"><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="ConfirmNewPassword"
                                  ControlToValidate="NewPassword" ErrorMessage="کلمه عبور با تکرار کلمه عبور برابر نیست."></asp:CompareValidator></td>
            <td align="right"><asp:TextBox ID="ConfirmNewPassword" MaxLength="50" runat="server" 
                                  TextMode="Password"></asp:TextBox></td>
            <td>&nbsp;</td>
            <td align="left">تکرار کلمه عبور جدید</td>
          </tr>
        </table></td>
        <td width="100px">&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="right" valign="middle" style="width: 387px"><asp:Button ID="save" runat="server" Text="ذخیره" OnClick="save_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        <td align="center" valign="middle">&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle" class="validation_tahoma" colspan="2"><div style="text-align:center"></div></td>
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
