<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamWeblogLogin.aspx.cs" Inherits="TeamWeblogLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
<meta http-equiv="Content-Language" content="fa">
<meta http-equiv="Content-Type" content="text/html; 

charset=utf-8">
<link rel="stylesheet" type="text/css" 

href="images/style.css" />
<title>ورود به بخش مدیریت وبلاگ های گروهی</title>
</head>

<body>
<form id="Form1" runat="server" method="post">
<div align="center">
	<table border="0" width="798" cellspacing="0" 

cellpadding="0">
		<tr>
			<td height="103">
			<table border="0" width="100%" 

cellspacing="0" cellpadding="0" height="100%" 

background="images/header-bg.gif">
				<tr>
					<td width="12">
					<img border="0" 

src="images/header-left.gif" width="12" 

height="103"></td>
					<td 

background="images/header-content.gif" 

class="header-content">
					<table 

border="0" width="100%" cellspacing="0" cellpadding="0" 

height="100%">
						<tr>
							

<td align="left">
							

<img border="0" src="images/iranblog-logo-en.gif" 

width="194" height="44" 

class="iranblog-header-logo-en"></td>
							

<td align="right">
							

<img border="0" src="images/iranblog-logo-fa.gif" 

width="225" height="54" 

class="iranblog-header-logo-fa"></td>
						</tr>
					</table>
					</td>
					<td 

align="right" width="14">
					<img border="0" 

src="images/header-right.gif" width="14" 

height="103"></td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td height="2">
			<img border="0" 

src="images/content-up.gif" width="798" height="2"></td>
		</tr>
		<tr>
			<td 

background="images/content-bg.gif">
			<table border="0" width="100%" 

cellspacing="0" cellpadding="0" class="content">
				<tr>
					<td 

width="18">&nbsp;</td>
					<td dir="rtl">
					<table 

border="0" width="100%" cellspacing="0" cellpadding="0" 

background="images/titr-bg.gif" style="margin-bottom: 

2">
						<tr>
							

<td width="10">&nbsp;</td>
							

<td align="right">                            
                            <asp:PlaceHolder 

ID="MainSiteHeaderControl" 

runat="server"></asp:PlaceHolder>

                          </td>
							

<td align="left" width="48">
							

<img border="0" src="images/titr-left.gif" width="48" 

height="23"></td>
						</tr>
					</table>
					</td>
					<td 

width="18">&nbsp;</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td 

background="images/content-bg.gif">
			<table border="0" width="100%" 

cellspacing="0" cellpadding="0" class="content">
				<tr>
					<td 

width="18">&nbsp;</td>
					<td dir="rtl">
					<table 

border="0" width="100%" cellspacing="0" cellpadding="0">
						<tr>
							

<td valign="top">
							

<div align="center">
								

<table border="0" width="759" cellspacing="0" 

cellpadding="0" background="images/text-p-bg.gif">
								

	<tr>
								

		<td>
								

		<table border="0" width="100%" 

cellspacing="0" cellpadding="0" 

background="images/text-t-bg.gif" style="margin-right: 

10">
								

			<tr>
								

				<td align="right" 

width="31">
								

				<img border="0" 

src="images/text-t-right.gif" width="31" 

height="25"></td>
								

				<td align="right"><div 

class="updates">ورود به بخش مدیریت وبلاگ های گروهی</div></td>
								

				<td 

width="5">&nbsp;</td>
								

			</tr>
								

		</table>
								

		</td>
								

	</tr>
								

	<tr>
								

		<td>
								

		<img border="0" 

src="images/text-p-h.gif" width="759" height="8"></td>
								

	</tr>
								

	<tr>
								

		<td>
								

		<div align="center">
								

			<table border="0" width="98%" 

cellspacing="0" cellpadding="0">
								

				<tr>
								

					<td>
								

					<div 

align="center">
								

						<div 

id="Container" align="left">
								

							

<div id="left">
								

								

<div id="Content" dir="rtl" align="center">
								

								

	<div class="box" style="WIDTH: 420px">
								

								

		<div class="header">
								

								

		</div>
								

								

		<div class="content">
 
								

								

				<div align="center">
								

								

					<table 

class="cTable" dir="rtl" cellPadding="5">
								

								

						<tr>
								

								

							

<td vAlign="top" width="110" height="22">
								

								

							

<p align="left">وبلاگ:</td>
								

								

							

<td vAlign="top" width="300" height="22"><asp:TextBox 

ID="weblog_team" runat="server" MaxLength="30" dir="ltr" 

style="WIDTH: 140px">
                                                         

               </asp:TextBox>&nbsp;</td>
								

								

						</tr>
								

								

						<tr 

id="trAuthor">
								

								

							

<td vAlign="top" width="110" height="22">
								

								

							

<p align="left">نام کاربری نویسنده:</td>
								

								

							

<td vAlign="top" width="300" height="22"><asp:TextBox 

ID="username_team" runat="server" MaxLength="12" 

dir="ltr" style="WIDTH: 140px">
                                                         

               </asp:TextBox></td>
								

								

						</tr>
								

								

						<tr>
								

								

							

<td vAlign="top" width="110" height="22">
								

								

							

<p align="left">کلمه عبور:</td>
								

								

							

<td vAlign="top" width="300" height="22">
<asp:TextBox 

ID="password_team" runat="server" MaxLength="12" 

dir="ltr" style="WIDTH: 140px" TextMode="Password"> 

</asp:TextBox></td>
								

								

					    </tr>
								

								

						<tr>
								

								

							

<td vAlign="top" width="110" height="22">
								

								

							

<p align="left">&nbsp;</td>
								

								

							

<td vAlign="top" width="300" height="22"><asp:CheckBox ID="cookieEnabled" runat="server" Text="مرا به خاطر بسپار؟" style="font-family: Tahoma;font-size: 7pt;color:#000000;width:100px"/></td>
								

								

					  </tr>
								

								

						<tr>
								

								

							

<td style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; HEIGHT: 

31px" vAlign="top" width="100%" colSpan="2" height="31">
								

								

							

<p align="center"><asp:Button 
        ID="login_team_weblog" runat="server" Text="ورود به بخش اعضاء" 
        style="WIDTH: 115px" onclick="login_team_weblog_Click"/>
                            </td>
								

								

						</tr>
								

								

						<tr>
								

								

							

<td vAlign="top" align="middle" width="100%" colSpan="2" 

height="22"><a id="linkLoginType" href="/">ورود به وبلاگهایی با یک نویسنده</a>								

								

							

</td>
								

								

						</tr>
								

								

						<tr>
								

								

							

<td vAlign="top" align="middle" width="100%" colSpan="2" 

height="22"><a href="ForgottenPassword.aspx">کلمه عبور را فراموش کرده ام 

(ارسال مجدد کلمه عبور به ایمیل)</a> </td>
								

								

					  </tr>
								

								

						<tr>
								

								

							

<td style="FONT-SIZE: 8pt; FONT-FAMILY: Tahoma" 

vAlign="top" width="100%" colSpan="2" height="22" align="right">
								

								

							

<p style="LINE-HEIGHT: 180%"><b><font 

color="#800000">»</font></b> کلمه عبور نسبت به کوچکی یا بزرگی حروف حساس است بنابراین هنگام 

ورود کلمه عبور نسبت به روشن یا خاموش بودن کلید CAPS LOCK&nbsp; اطمینان حاصل کنید.<br>
								

								

							

<b><font color="#800000">»</font></b>نویسندگان وبلاگهای گروهی میبایست از گزینه 
ورود نویسندگان وبلاگهای گروهی استفاده کنند<span lang="en-us">.</span><br>
								

								

							

<b><font color="#800000">»</font></b> نام کاربری به نسبت به حروف کوچک و بزرگ 

حساس نیست.<br>
								

								

							

<b><font color="#800000">»</font></b> در صورت فراموشی کلمه عبور در بخش 

&quot;کلمه عبور را فراموش کرده ام&quot; نام کاربری خود را تایپ کنید ، کلمه عبور به ایمیل خصوصی شما ارسال خواهد 

شد.<br>
&nbsp;</td>
								

								

						</tr>
								

								

					</table>
								

								

			  </div>
								

								

		</div>
								

								

	</div>
								

								

</div>
								

							

</div>
								

						</div>
								

					</div>
								

					</td>
								

				</tr>
								

			</table>
								

		</div>
								

		</td>
								

	</tr>
								

	<tr>
								

		<td>
								

		<img border="0" 

src="images/text-p-f.gif" width="759" height="7"></td>
								

	</tr>
								

</table>
							

</div>
							

</td>
						</tr>
					</table>
					<p>&nbsp;</td>
					<td 

width="18">&nbsp;</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td 

background="images/content-bg.gif" align="center">
			<font face="Verdana"><span 

style="font-variant: small-caps"><asp:PlaceHolder 

ID="CopyrightFooterControl" 

runat="server"></asp:PlaceHolder></span></font></td>
		</tr>
		<tr>
			<td 

background="images/content-bg.gif">
			<table border="0" width="100%" 

cellspacing="0" cellpadding="0" 

background="images/footer-bg.gif">
				<tr>
					<td align="left" 

width="19">
					<img border="0" 

src="images/footer-left.gif" width="19" 

height="19"></td>
					<td 

align="center">&nbsp;</td>
					<td 

align="right" width="18">
					<img border="0" 

src="images/footer-right.gif" width="18" 

height="19"></td>
				</tr>
			</table>
			</td>
		</tr>
	</table>
</div>
</form>
</body>

</html>