<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetBack.aspx.cs" Inherits="bookstore.cp.GetBack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>نتیجه خرید</title>
<link href="../style/calass.css" rel="stylesheet" type="text/css" />
<link href="../styles/addons.css" rel="stylesheet" type="text/css" />
<SCRIPT language="javascript" src="../js/farsi.js"></SCRIPT>
</head>
<body>
<div id="skl">
	<div id="top"><img src="../images/top.jpg" /></div>
    <div id="topmenu"><asp:PlaceHolder ID="LoginControl" runat="server"></asp:PlaceHolder></div>
    <div id="topspace"></div>
    <div id="center-s1">
    <div id="register">
    <form id="form1" name="form1" method="post" runat="server">
    <right>
            <table border="0" cellspacing="4" cellpadding="2" style=" margin-left:100px" width="100%">
       <tr>
							<td align="right" class="validation_tahoma" colspan="4" dir="rtl"><asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
							</td>
						</tr>
      <tr>
        <td colspan="4">
		<hr/></td>
      </tr>
      <tr>
        <td width="1">&nbsp;</td>
        <td align="center" valign="middle" colspan="2"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td colspan="4" align="right" dir="rtl">    <div><br />
    
    </div>
    <p>

    </p></td>
          </tr>
          </table></td>
        <td width="100">&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td width="266" align="center" valign="middle">&nbsp;</td>
        <td width="97" align="center" valign="middle">&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="center" valign="middle" class="validation_tahoma" colspan="2"><div style="text-align:center"></div></td>
        <td>&nbsp;</td>
      </tr>
            </table></right>
</form>
     </div>               
     <div id="width-spacer">&nbsp;</div>
    </div>
      
  <div id="right">
        <asp:PlaceHolder ID="UserMenuControl" runat="server"></asp:PlaceHolder>  
  </div>
     
</div>
<div id="btm"></div> 
</body>
</html>
