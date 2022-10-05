<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%@ Page language="c#" Inherits="services.Migrated_ForgottenPassword" CodeFile="ForgottenPassword.aspx.cs" %>
<html>

<head>
<meta http-equiv="Content-Language" content="fa">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="images/style.css" />
<title> بازیابی کلمه عبور</title>
	<script language="javascript" type="text/javascript">
			/* All rights reserved to Mr. Alireza Poshtkoohi (C) 2005-2007. alireza.poshtkohi@gmail.com */
			//---------------------------------------------------------------------------------------------------------------------------
				function BlankField(field, text)
				{
				    if(field.value == "")
					{
					    alert(text);
						field.focus();
						return false;
					}
					else return true;
				}
				//-------------------------------------------
				function ValidateUsername(field)
				{
				   if(!BlankField(field ,".فیلد نام کاربری خالی است"))
				          return false;
				   else
				   {
				          var re = /^[\-0-9a-zA-Z]{1,}$/
				          if(re.test(field.value))
					            return true;
					      else
						  {
						        alert(".حروف فیلد نام کاربری نامعتبر است");
						        field.focus();
						        field.select();
						        return false;
						  }
					}
				}
				//-------------------------------------------
				function ValidatePassword(field)
				{
				   if(!BlankField(field,".فیلد کلمه عبور خالی است"))
				          return false;
				   else return true;
				}
				//-------------------------------------------
				function ValidateEmail(field)
				{
				    if(!BlankField(field, ".فیلد ایمیل خالی است"))
					    return false;
				    var re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
					if(re.test(field.value))
					    return true;
						else
						{
						   alert(".حروف ایمیل نا معتبر است");
						   field.focus();
						   field.select();
						   return false;
						}
				}
				//-------------------------------------------
				function ValidateForm(form)
				{
					if(!ValidateUsername(form.username))
					      return false;
				    if(!ValidatePassword(form.password))
					      return false;
					else return true;
				}
				//-------------------------------------------
				function ValidateForgottenPassword(form)
				{
					if(!ValidateUsername(form.username))
					      return false;
				    if(!ValidateEmail(form.email))
					      return false;
					else return true;
				}
				//---------------------------------------------------------------------------------------------------------------------------
				</script>
		
</head>

<body>
<form name="ForgottenPassword" method="post" runat="server" onSubmit="return ValidateForgottenPassword(this);">
<div align="center">
	<table border="0" width="798" cellspacing="0" cellpadding="0">
		<tr>
			<td height="103">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" height="100%" background="images/header-bg.gif">
				<tr>
					<td width="12">
					<img border="0" src="images/header-left.gif" width="12" height="103"></td>
					<td background="images/header-content.gif" class="header-content">
					<table border="0" width="100%" cellspacing="0" cellpadding="0" height="100%">
						<tr>
							<td align="left">
							<img border="0" src="images/iranblog-logo-en.gif" width="194" height="44" class="iranblog-header-logo-en"></td>
							<td align="right">
							<img border="0" src="images/iranblog-logo-fa.gif" width="225" height="54" class="iranblog-header-logo-fa"></td>
						</tr>
					</table>
					</td>
					<td align="right" width="14">
					<img border="0" src="images/header-right.gif" width="14" height="103"></td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td height="2">
			<img border="0" src="images/content-up.gif" width="798" height="2"></td>
		</tr>
		<tr>
			<td background="images/content-bg.gif">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" class="content">
				<tr>
					<td width="18">&nbsp;</td>
					<td dir="rtl">
					<table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/titr-bg.gif" style="margin-bottom: 2">
						<tr>
							<td width="10">&nbsp;</td>
							<td>                            
                            <asp:PlaceHolder ID="MainSiteHeaderControl" runat="server"></asp:PlaceHolder>
                                        </td>
							<td align="left" width="48">
							<img border="0" src="images/titr-left.gif" width="48" height="23"></td>
						</tr>
					</table>
					</td>
					<td width="18">&nbsp;</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td background="images/content-bg.gif">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" class="content">
				<tr>
					<td width="18">&nbsp;</td>
					<td dir="rtl">
					<table border="0" width="100%" cellspacing="0" cellpadding="0">
						<tr>
							<td valign="top">
							<div align="center">
								<table border="0" width="759" cellspacing="0" cellpadding="0" background="images/text-p-bg.gif">
									<tr>
										<td>
										<table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/text-t-bg.gif" style="margin-right: 10">
											<tr>
												<td align="right" width="31">
												<img border="0" src="images/text-t-right.gif" width="31" height="25"></td>
												<td><div class="updates">بازیابی کلمه عبور</div></td>
												<td width="5">&nbsp;</td>
											</tr>
										</table>
										</td>
									</tr>
									<tr>
										<td>
										<img border="0" src="images/text-p-h.gif" width="759" height="8"></td>
									</tr>
									<tr>
										<td>
										<div align="center">
											<table border="0" width="98%" cellspacing="0" cellpadding="0">
												<tr>
													<td><BR />
                                                      &nbsp;<table width="750" border="0" cellpadding="0" cellspacing="0" align="center">
				<!--DWLayoutTable-->
				<tr>
					<td width="193" rowspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" style="BORDER-RIGHT:#e6e6e6 1px dotted">
							<!--DWLayoutTable-->
							<tr>
								<td width="192" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<!--DWLayoutTable-->
										<tr>
											<td valign="top" dir="rtl" class="v3bg-title"><div class="main2 v3color_green"></div>
										  </td>
										</tr>
										<!--DWLayoutTable-->
										<tr>
											<td height="28" align="right" valign="middle" dir="rtl"><!--DWLayoutEmptyCell-->&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td valign="top"><table width="85%" border="0" cellpadding="0" cellspacing="0" dir="ltr">
							<!--DWLayoutTable-->
							<tr>
								<td height="76" colspan="2" align="right" valign="top" class="main" dir="rtl"><img src="../images/reply_head.gif" width="11" height="7">
									در صورت فراموش کردن کلمه عبور ورود خود به سایت میتوانید نام کاربری و ایمیلی که 
									در هنگام ثبت نام در ایران بلاگ وارد کرده اید را در فرم زیر وارد کنید تا کلمه 
									عبور جدید به شما ایمیل زده شود. توجه داشته باشید که اگر این اطلاعات با اطلاعات 
									زمان ثبت نام شما متفاوت باشند شما ایمیلی دریافت نخواهید کرد.</td>
							</tr>
							<tr>
								<td width="356" height="23" align="right" valign="middle" class="main" dir="ltr"><asp:TextBox ID="username" runat="server" MaxLength="12" CssClass="v3headercontactform" ToolTip=".نام کاربری خود را در این قسمت وارد کنید"
										style="MARGIN-TOP:5px"></asp:TextBox></td>
								<td width="60" align="right" valign="middle" class="main" dir="rtl">نام کاربری:								</td>
							</tr>
							<tr>
								<td height="23" align="right" valign="middle" class="main" dir="ltr"><asp:TextBox ID="email" runat="server" MaxLength="50" CssClass="v3headercontactform" ToolTip=".ایمیل خود را در این قسمت وارد کنید"
										style="MARGIN-TOP:5px"></asp:TextBox></td>
								<td align="right" valign="middle" class="main" dir="rtl">آدرس ایمیل:</td>
							</tr>
							<tr>
								<td height="40" colspan="2" align="right" valign="middle" class="main" dir="rtl"><asp:Button ID="submit" runat="server" CssClass="v3ibbtn" ToolTip="تایید" Text="تایید" Width="120"
										Font-Names="Tahoma" onclick="submit_Click"></asp:Button></td>
							</tr>
						</table>
					</td>
					<td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" style="BORDER-LEFT:#e6e6e6 1px dotted">
							<!--DWLayoutTable-->
							<tr>
								<td width="192" height="30" valign="middle" dir="rtl"><!--DWLayoutEmptyCell-->&nbsp;</td>
							</tr>
							<tr>
								<td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<!--DWLayoutTable-->
										<tr>
											<td valign="top"  class="v3bg-title" dir="rtl">&nbsp;											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="2"></td>
					<td></td>
				</tr>
			</table>
													</td>
												</tr>
											</table>
										</div>
										</td>
									</tr>
									<tr>
										<td>
										<img border="0" src="images/text-p-f.gif" width="759" height="7"></td>
									</tr>
								</table>
							</div>
							</td>
						</tr>
					</table>
					<p>&nbsp;</td>
					<td width="18">&nbsp;</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td background="images/content-bg.gif" align="center">
			<font face="Verdana"><span style="font-variant: small-caps"><asp:PlaceHolder ID="CopyrightFooterControl" runat="server"></asp:PlaceHolder></span></font></td>
		</tr>
		<tr>
			<td background="images/content-bg.gif">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/footer-bg.gif">
				<tr>
					<td align="left" width="19">
					<img border="0" src="images/footer-left.gif" width="19" height="19"></td>
					<td align="center">&nbsp;</td>
					<td align="right" width="18">
					<img border="0" src="images/footer-right.gif" width="18" height="19"></td>
				</tr>
			</table>
			</td>
		</tr>
	</table>
</div>
</form>

</body>

</html>