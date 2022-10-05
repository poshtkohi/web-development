<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stat.aspx.cs" Inherits="services.blogbuilderv1.stat" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
<meta http-equiv="Content-Language" content="fa">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="../images/style.css" />
<style>
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none }
.style1 {
	color: #FF0000;
	font-size: 14pt;
}
</style>
<script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD><body>
<form method="post" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="554" height="5" align="center">
                                    <asp:Label ID="message" runat="server" BackColor="Gold" Font-Name="Tahoma" Font-Size="X-Small"
                                        ForeColor="Red" Visible="False" Width="100%" Font-Names="Tahoma"></asp:Label></td>
							</tr>
							<tr>
								<td height="27" class="v3ibbtn">
									<p align="center" class="main">آمار وبلاگ </p>
							  </td>
							</tr>
							<tr>
								<td height="97" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
								  <tr>
								    <td height="4" colspan="2"></td>
							      </tr>
								  <tr>
								    <td width="450" align="right" valign="middle" style="height: 17px">
                                    <asp:Label ID="TodayVisits" runat="server" Font-Names="Tahoma" Font-Size="Small" ForeColor="Red" Font-Bold="True">0</asp:Label></td>
							      <td width="104" align="right" valign="middle" style="height: 17px">: بازديد هاي امروز</td>
								  </tr>
								  <tr>
								    <td width="450" height="17" align="right" valign="middle">
                                        <asp:Label ID="YesterdayVisits" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"
                                            ForeColor="Red">0</asp:Label></td>
							      <td width="104" align="right" valign="middle">: بازديد هاي ديروز</td>
								  </tr>
								  <tr>
								    <td width="450" height="17" align="right" valign="middle">
                                        <asp:Label ID="ThisMonthVisits" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"
                                            ForeColor="Red">0</asp:Label></td>
							      <td width="104" align="right" valign="middle">: بازديد هاي این ماه</td>
								  </tr>
								  <tr>
								    <td width="450" height="17" align="right" valign="middle">
                                        <asp:Label ID="TotalVisits" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"
                                            ForeColor="Red">0</asp:Label></td>
							      <td width="104" align="right" valign="middle">: كل بازديد ها </td>
								  </tr>
								  <tr>
								    <td height="73" colspan="2">&nbsp;</td>
							      </tr>
							    </table></td>
							</tr>
							<tr>
							  <td height="27" align="center" valign="middle" class="v3submit">
							  <p align="center" class="main" dir="rtl">تگ
								<span lang="en-us">HTML</span> استاندارد آمار 
							  وبلاگ های ایران بلاگ</td>
							</tr>
							<tr>
							  <td height="104" align="center" valign="middle"><textarea readonly="readonly" style="MARGIN-TOP: 5px; FONT-SIZE: 8pt; WIDTH: 400px; COLOR: #333333; FONT-FAMILY: Tahoma; HEIGHT: 150px; TEXT-ALIGN: left"
										name="code"><{stat}><br />
		بازديد هاي امروز : <font color="#FF0000">{$$TodayVisits}</font><br />
		بازديد هاي ديروز : <font color="red">{$$YesterdayVisits}</font><br />
		بازديد هاي این ماه : <font color="red">{$$ThisMonthVisits}</font><br />
		كل مطالب : <font color="red">{$$posts}</font><br />
		كل بازديد ها : <font color="red">{$$TotalVisits}</font><br />
		ايجاد صفحه : <font color="red">{$$GenerataionTime} </font>
		<span lang="fa">ثانیه</span><br />
		</{stat}></textarea></td>
  </table>
</form>
</body>
</HTML>
