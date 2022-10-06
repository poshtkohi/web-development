<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsImage.aspx.cs" Inherits="bookstore.admin.NewsImage" validateRequest="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>عکس خبر</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link href="../styles/cp.css" rel="stylesheet" type="text/css" />
        <link href="../styles/addons.css" rel="stylesheet" type="text/css" />
	</HEAD>
<body>
<form runat="server">
<table width="500px" border="0" cellspacing="2" cellpadding="2" align="center">
<tr>
        <td align="center" class="info" colspan="4" dir="ltr"><asp:Label ID="message" runat="server" Visible="false" CssClass="message"></asp:Label></td>
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
    <td align="right" valign="middle" class="simple">حداقل ابعاد تصویر 75x75 می 
        باشد.(.jpg, .gif, .png)<br />
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
	</body>
</HTML>
