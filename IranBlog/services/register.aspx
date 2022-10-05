<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="AlirezaPoshtkoohiLibrary.register" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<HTML xmlns="http://www.w3.org/1999/xhtml"><HEAD>        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <title>ثبت وبلاگ شخصی جدید در جامعه بزرگ ایران بلاگ</title>
		<link href="styles/blue.css" rel="stylesheet" rev="stylesheet">
        		<link rel="stylesheet" type="text/css" href="images/style.css" />
				<SCRIPT language="javascript" src="/services/linkbox/farsi.js" type="text/javascript"></SCRIPT>
                <SCRIPT language="javascript" src="/services/linkbox/farsiEditor.js"></SCRIPT>
				<script language="javascript" type="text/javascript">
			/* All rights reserved to Mr. Alireza Poshtkohi (C) 2002-2009. alireza.poshtkohi@gmail.com */
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
				function ValidateDropDownList(field, text)
				{
				    txt = field.options[field.selectedIndex].value;
					if(txt == "none")
					{
					   alert(text);
					   return false;
					}
					else return true;
				}
				//-------------------------------------------
				function ValidateCheckBox(field, text)
				{
				   if(!field.checked)
				   {
				       alert(text);
					   field.focus();
					   field.select();
				       return false;
				   } 
				   else return true;
				}
				//-------------------------------------------
				function ValidateMobileNumber(field)
				{
				    if(field.value == "")
					    return true;
					else
					{
					      if(field.value.length < 11 || field.value.length > 11)
					      {
					         alert(".تعداد رقم های فیلد شماره موبایل نا معتبر است");
							 field.focus();
						     field.select();
					         return false;
					      }
				          if(field.value.charAt(0) != "0")
					      {
					          alert(".فیلد شماره موبایل بایستی با رقم صفر آغاز شود");
							  field.focus();
						      field.select();
					          return false;
					      }
					      else
					      {
					          for(i=0 ; i < field.value.length; i++)
					          {
					              if(field.value.charAt(i) < "0" || field.value.charAt(i) > "9")
						          {
						              alert(".فیلد شمار ه موبایل نا معتبر است");
									  field.focus();
						              field.select();
						              return false;
						          }
					          }
					          return true;
					      }
					 }
				}
				//-------------------------------------------
				function ValidateUsername(field)
				{
				   if(!BlankField(field ,".فیلد شناسه کاربری خالی است"))
				          return false;
				   else
				   {
				          var re = /^[\-0-9a-zA-Z]{1,}$/
				          if(re.test(field.value))
					            return true;
					      else
						  {
						        alert(".حروف فیلد شناسه کاربری نامعتبر است");
						        field.focus();
						        field.select();
						        return false;
						  }
					}
				}
				//-------------------------------------------
				function ValidateConfirmPassowrd(form)
				{
				    if(!BlankField(form.confirmPassword, ".فیلد تکرار کلمه عبورخالی است"))
					      return false;
					else
					{
					    if(form.confirmPassword.value == form.password.value)
						    return true;
						else
						{
						        alert(".فیلد کلمه عبور با فیلد تکرارکلمه عبوربرابرنیست");
						        form.confirmPassword.focus();
						        form.confirmPassword.select();
						        return false;
						}
					}
				}
				//-------------------------------------------
				function ValidateConfirmEmail(form)
				{
				    if(!BlankField(form.confirmEmail, ".فیلد تکرار ایمیل خالی است"))
					      return false;
					else
					{
					    if(form.confirmEmail.value != form.email.value)
						{
						        alert(".فیلد ایمیل با فیلد تکرار ایمیل برابر نیست");
						        form.confirmEmail.focus();
						        form.confirmEmail.select();
						        return false;
						}
						else return true;
					}
				}
				//-------------------------------------------
				function ValidatePassword(field)
				{
				   if(!BlankField(field,".فیلد کلمه عبور خالی است"))
				          return false;
				   else
				   {
				          var re = /^[\!\-\~\&\;\:\,]{1,}$/
				          if(re.test(field.value))
						  {
						      alert(".حروف فیلد کلمه عبور نامعتبر است");
						      field.focus();
						      field.select();
						      return false;
						  }
						  else return true;
					}
				}
				//-------------------------------------------
				function ValidateFirstName(field)
				{
				     if(field.value == "")
					    return true;
				    else
				    {
				          var re = /^[\!\-\~\&\;\:\,]{1,}$/
				          if(re.test(field.value))
						  {
						      alert(".حروف فیلد نام نا معتبر است");
						      field.focus();
						      field.select();
						      return false;
						  }
						  else return true;
					 }
				}
				//-------------------------------------------
				function ValidateLastName(field)
				{
				    if(field.value == "")
					    return true;
					else
				    {
				          var re = /^[\!\-\~\&\;\:\,]{1,}$/
				          if(re.test(field.value))
						  {
						      alert(".حروف فیلد نام خانوادگی نا معتبر است");
						      field.focus();
						      field.select();
						      return false;
						  }
						  else return true;
					 }
				}
				//-------------------------------------------
				function ValidateََAcknowledgePhrase(field)
				{
				   if(!BlankField(field, ".فیلد کلمه تایید خالی است"))
				          return false;
				   if(field.value.length != 6)
				   {
				       alert(".تعداد حروف فیلد کلمه تایید کم است");
					   return false;
				   }		  
				   else return true; 
				}
				//----------------------------------------------------------------
				  function ValidateSubdomain(field)
				  {
				      if(!BlankField(field, ".فیلد آدرس وبلاگ خالی است"))
					      return false;
					  else
				      {
				          var re = /^[\-0-9a-zA-Z]{1,}$/
				          if(re.test(field.value))
					            return true;
					      else
						  {
						        alert(".فیلد آدرس وبلاگ نا معتبر است");
						        field.focus();
						        field.select();
						        return false;
						  }
					  }
				  }
				  //----------------------------------------------------------------
				  function ValidateTitle(field)
				  {
				      if(!BlankField(field, ".فیلد عنوان وبلاگ خالی است"))
					      return false;
					  else
				      {
				          var re = /^[\!\-\~\&\;\:\,]{1,}$/
				          if(re.test(field.value))
						  {
						        alert(".فیلد عنوان وبلاگ نا معتبر است");
						        field.focus();
						        field.select();
								return false
						  }
					      else return true;
					  }
				  }
				//----------------------------------------------------------------
				function ValidateMaxPostShow(field)
				{
				    if(!BlankField(field, ".تعداد پست ها خالی است"))
					    return false;
					var re = /^[0-9]{1,2}/
					if(!re.test(field.value))
					{
						alert('.تعداد پستها میتواند بین یک تا سی باشد');
						field.focus();
						return false;
					}
					if(parseInt(field.value) > 30 || parseInt(field.value) == 0)
					{
						alert('.تعداد پستها میتواند بین یک تا سی باشد');
						field.focus();
						return false;
					}
					else return true;	
				}
				//-------------------------------------------
				function ValidateForm(form)
				{
				    if(!ValidateFirstName(form.first_name))
					      return false;
					if(!ValidateLastName(form.last_name))
					      return false;
					if(!ValidateUsername(form.username))
					      return false;
				    if(!ValidatePassword(form.password))
					      return false;
					if(!ValidateConfirmPassowrd(form))
					      return false;
				    if(!ValidateEmail(form.email))
					      return false;
					if(!ValidateConfirmEmail(form))
					      return false;
					if(!ValidateSubdomain(form.subdomain))
					       return false;
					if(!ValidateTitle(form.title))
					       return false;
				    if(!ValidateMaxPostShow(form.MaxPostShow))
					       return false;
					if(!ValidateََAcknowledgePhrase(form.acknowledge))
					      return false;
					if(!ValidateCheckBox(form.agreement, ".شما باید موافقتنامه را خوانده و آن را بپذیرید"))
					      return false;
					else
					{
						//document.getElementById('submitBtn').disabled = true;
						document.getElementById('submitBtn').style.display='none';
                        form.submit();
						return true;
					}
				}
				//---------------------------------------------------------------------------------------------------------------------------
				</script>
</HEAD>
<BODY>
<DIV id=container>
<DIV class=ct>
 <div style="float: right; width: 240px; height: 101px; background: transparent url(images/iran.png) no-repeat right  center;"><img src="images/ib5.png"  align="bottom"></div>
 <div style=" float:left; width:200; height:100;"><img src="images/blog.png" align="left"></div>
 </DIV>

<DIV id=toolbar>
<DIV id=tools>
<UL id=tool>
                   <asp:PlaceHolder ID="MainSiteHeaderControl" runat="server"></asp:PlaceHolder>
                        </UL></DIV></DIV>
<DIV id=themebar>
<DIV id=pagesettings><asp:Label ID="LabelTime" runat="server"></asp:Label>
</DIV></DIV>
<!--<DIV id=navigation >
<DIV class=title><img src="images/tik.png" align="right">&nbsp; صفحه اصلی&nbsp;&nbsp;</DIV>
<div class="weblog"><img src="images/tik.png" align="right"> &nbsp;&nbsp;یک شنبه 5/7/1378  ساعت 12:12&nbsp;&nbsp;</div>

</DIV>-->
<DIV id=main>
<center>
 <table width="700px" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff" dir="ltr">
			<tr>
				<td width="193" rowspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" style="BORDER-RIGHT:#e6e6e6 1px dotted">
						<tr>
							<td width="192" height="26" valign="middle" background="images/text-t-bg.gif" dir="rtl"><img src="../images/marrow.png" width="11" height="11"><span class="updates">راهنمای 
								ثبت وبلاگ</span></td>
					  </tr>
						<tr>
							<td height="406" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td width="192" height="406" align="justify" valign="top" background="../images/bgmainrl.png" dir="rtl"><div class="main" style="PADDING-RIGHT:2px; PADDING-LEFT:2px; PADDING-BOTTOM:2px; PADDING-TOP:5px" align="justify"><img src="../images/56%20(10).png" width="17" height="20" align="absMiddle">
												لطفا فرم را به دقت پرکنید و دکمه ارسال را فشار دهید.</div>										  
                                                <div class="main" style="PADDING-RIGHT:2px; PADDING-LEFT:2px; PADDING-BOTTOM:2px; PADDING-TOP:5px" align="justify"><img src="../images/56%20(10).png" width="17" height="20" align="absMiddle">
					  برای تغییر زبان هم می توانید با فشردن کلید های 

Ctrl+Shift و هم از طریق کلیک بر 
					  روی منوی<span class="v3color_green"> FA</span>
					  از زبان فارسی برای تکمیل فرم ها استفاده کنید.</div>										  
                      <div class="main" style="PADDING-RIGHT:2px; PADDING-LEFT:2px; PADDING-BOTTOM:2px; PADDING-TOP:5px" align="justify"><img src="../images/56%20(10).png" width="17" height="20" align="absMiddle">
						  بعد از ارسال اطلاعات خود، نام کاربری و کلمه عبور شما به ایمیلتان 

فرستاده خواهد 
						  شد .</div>										  <div class="main" style="PADDING-RIGHT:2px; PADDING-LEFT:2px; PADDING-BOTTOM:2px; PADDING-TOP:5px" align="justify"><span class="main" style="PADDING-RIGHT:2px; PADDING-LEFT:2px; PADDING-BOTTOM:2px; PADDING-TOP:5px"><img src="../images/56%20(10).png" width="17" height="20" align="absmiddle" /> برای
						      کلمه تایید باید کد درون عکس را وارد کنید.</span><br><img src="../images/56%20(10).png" width="17" height="20" align="absMiddle">
							  فیلد کلمه عبور نمیتواند دارای حروف فارسی باشد.</div>										  
				    <div class="main" style="PADDING-RIGHT:2px; PADDING-LEFT:2px; PADDING-BOTTOM:2px; PADDING-TOP:5px" align="justify"><img src="../images/56%20(10).png" width="17" height="20" align="absMiddle">
								  لطفاً موافقتنامه عضویت در ایران بلاگ برای استفاده از منابع آن را 
بدقت مطالعه 
								  نموده و در صورت موافقت بر روی گزینه موافقتنامه کلیک کنید.</div>										  <div class="main" style="PADDING-RIGHT:2px; PADDING-LEFT:2px; PADDING-BOTTOM:2px; PADDING-TOP:5px"
												align="justify"><img src="../images/56%20(10).png" width="17" height="20" align="absMiddle">
					  پس از ارسال اطلاعات ثبت نام،&nbsp; 
					  وبلاگ شما با یک آدرس شبیه&nbsp;
					  <span class="v3color_green">http://Yourname.iranblog.com</span>&nbsp;&nbsp; در سیستم 
												وبلاگ نویسی ایران بلاگ ایجاد 
												خواهد شد.<br>
					<img src="../images/56%20(10).png" width="17" height="20" align="absmiddle" /> در قسمت آدرس و عنوان وبلاگ ، آدرس وبلاگ خود و عنوانی را که بعدا در وبلاگ شما نمایش داده خواهد شد را وارد نمایید. 
                                        </div></td>
							  </tr>
									<!--DWLayoutTable-->
								</table>
							</td>
					  </tr>
					</table>
				</td>
				<td width="557" height="562" valign="top">
					<form id="Form1" name="register" method="post" runat="server" onSubmit="return ValidateForm(this);">
						<table width="100%" border="0" cellpadding="0" cellspacing="0">
					  <tr align="right">
								<td width="557" height="26" valign="middle" background="images/text-t-bg.gif" dir="rtl"><img src="../images/marrow.png" width="11" height="11"><span class="updates"> ثبت نام و 
									ایجاد وبلاگ شخصی</span></td>
						  </tr>
							<tr>
								<td height="49" align="right" valign="top" class="main" dir="rtl"><img src="../images/reply_head.gif" width="11" height="7">
									کاربر گرامی از اینکه ایران بلاگ را برای معرفی خود به دنیای بزرگ ایرانیان در 
									داخل و خارج از کشور عزیزمان انتخاب نموده اید کمال امتنان را داریم. با تشکر از 
									انتخاب خود , به شما اطمینان می دهیم در ایران بلاگ تمامی تلاش خود را بر ارتقای 
									جایگاه شما در جهان وب .بکار خواهیم بست. از هم اکنون ورودتان را به دنیای ایران 
									بلاگ تبریک می گوییم.
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td height="21" align="center" valign="middle">
                                            <strong>
													<asp:Label BackColor="Gold" CssClass="txtBoxStyleLogin" Font-Size="XX-Small" ForeColor="Red" ID="message" runat="server" Visible="False" Width="100%"></asp:Label>
											</strong>			
											</td>
											<td width="140" align="justify" valign="top" bgcolor="#fdfdfd" dir="rtl"><img src="../images/marrow.png" width="11" height="11">
												تکمیل اطلاعات مدیر وبلاگ
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:TextBox  lang="fa" ID="first_name" style="TEXT-ALIGN: right" runat="server" ToolTip="در این قسمت نام کوچک خود را وارد کنید." MaxLength="30" CssClass="v3txtboxReg155form"></asp:TextBox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">نام:
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox  lang="fa" id="last_name" style="TEXT-ALIGN: right" runat="server" ToolTip="در این قسمت نام خانوادگی خود را وارد کنید." MaxLength="30" CssClass="v3txtboxReg155form"></asp:textbox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl"> نام خانوادگی:
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox id="username" runat="server" ToolTip="در این قسمت نام کاربری خود را وارد کنید."
													MaxLength="12" CssClass="v3txtboxReg155form"></asp:textbox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*نام کاربری :</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox id="password" runat="server" ToolTip="در این قسمت کلمه عبور خود را وارد کنید." MaxLength="12" CssClass="v3txtboxReg155form" TextMode="Password"></asp:textbox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*کلمه عبور :</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox id="confirmPassword" runat="server" ToolTip="در این قسمت کلمه عبور خود را مجددا وارد کنید."
													MaxLength="12" CssClass="v3txtboxReg155form" TextMode="Password"></asp:textbox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*تکرار کلمه عبور:</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox id="email" runat="server" ToolTip="در این قسمت ایمیل خود را وارد کنید." MaxLength="50"
													CssClass="v3txtboxReg155form"></asp:textbox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*آدرس ایمیل:</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox id="confirmEmail" runat="server" ToolTip="در این قسمت ایمیل خود را مجددا وارد کنید."
													MaxLength="50" CssClass="v3txtboxReg155form"></asp:textbox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*تکرار آدرس ایمیل:</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
										  <td width="150" height="21" align="right" valign="middle"> Http://</td>
										  <td width="131" valign="top"><asp:textbox id="subdomain" runat="server" ToolTip="آدرس وبلاگ خود را در این قسمت وارد کنید." Width="250" CssClass="v3txtboxReg155form" MaxLength="30"></asp:textbox></td>
										  <td width="139" align="justify" valign="top"> .iranblog.com</td>
										  <td width="137" align="right" valign="top" dir="ltr">:آدرس وبلاگ*</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox  lang="fa" id="title" style="TEXT-ALIGN: right" runat="server" ToolTip="عنوان وبلاگ خود را در این قسمت وارد کنید." Width="250" CssClass="v3txtboxReg155form" MaxLength="100"></asp:textbox></td>
											<td width="137" height="21" align="right" valign="top" dir="ltr">:عنوان وبلاگ*</td>
										</tr>
									</table>
								</td>
							</tr>
                            <tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:textbox id="MaxPostShow" runat="server" ToolTip="تعداد پستها میتواند بین یک تا سی باشد." CssClass="v3txtboxReg155form" MaxLength="2" style="TEXT-ALIGN:right;width:30px">10</asp:textbox></td>
											<td width="137" height="21" align="right" valign="top" dir="ltr">:تعداد نمایش پستها در صفحات وبلاگ*</td>
										</tr>
									</table>
								</td>
							</tr>
                            <tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:dropdownlist CssClass="v3txtboxReg155form" id="ArciveDisplayMode" runat="server" Height="100%">
                                              <asp:ListItem Value="true" Selected="True">آرشیو
                                                مطالب بصورت ماهانه</asp:ListItem>
                                              <asp:ListItem Value="false">آرشیو
                                                مطالب بصورت هفتگی</asp:ListItem>
                                            </asp:dropdownlist></td>
											<td width="137" height="21" align="right" valign="top" dir="ltr">:نحوه آرشیو مطالب وبلاگ*</td>
										</tr>
									</table>
								</td>
							</tr>
                            <tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<!--DWLayoutTable-->
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:Image Height="30px" id="RandomImage" ImageUrl="request.aspx?i=randimg" runat="server"
													Width="157px"></asp:Image></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*کلمه تایید:</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<!--DWLayoutTable-->
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:TextBox ID="acknowledge" runat="server" ToolTip="در این قسمت کد درون عکس را وارد کنید."
													MaxLength="6" CssClass="v3txtboxReg155form"></asp:TextBox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*کلمه تایید را وارد کنید:</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="557" height="21" align="justify" valign="top" bgcolor="#fdfdfd" dir="rtl"><img src="../images/marrow.png" width="11" height="11">
												اصول و قوانین ایران بلاگ :</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="557" height="21" align="justify" valign="top" dir="rtl">
												<textarea name="textarea" readonly class="v3txtboxcomentform" cols="50" dir="rtl" rows="50">    قوانین و مقررات استفاده از سایت ایران بلاگ     

1.مدیریت سایت IranBlog.com ضمن دفاع از آزادی بیان و قلم از کاربران خود می خواهد تا شرایط و قوانین جمهوری اسلامی ایران را نیز در نظر داشته باشند تا از این جهت برای وب سایت و یا وبلاگ نویسنده مشکلی بوجود نیاید.
2.اطلاعات خصوصی کاربران و همچنین ایمیلهای ایشان نزد سایت محفوظ است و در اختیار اشخاص حقیقی یا حقوقی قرار نخواهد داد.
3.وبلاگهایی که اقدام به انتشار تصاویر یا مطالب مستهجن  کنند و  یا به مقامات جمهوری اسلامی ایران ، ادیان و اقوام و نژادها توهین کنند  مسدود خواهند شد.
4.وبلاگ کاربرانی که به هر شکلی (مانند صرف بیهوده منابع سایت ،هک، حذف آگهی و...) منافع مالی یا امنیت سایت را دچار خطر کنند برخورد و وبلاگ آنها مسدود خواهد شد.
5.در جهت حفط منابع سایت و همچنین جلوگیری از سوء استفاده  وبلاگهای آزمایشی ، اسپم و وبلاگهای غیر فعال و  بدون محتوا حذف خواهد شد.</textarea>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="21" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="420" height="21" align="justify" valign="top" dir="rtl"><asp:CheckBox CssClass="v3txtboxReg155form" ID="agreement" runat="server" Text="می پذیرم." ToolTip=".موافقت نامه را بخوانید و در صورت تائید گزینه را انتخاب نمائید" style="DIRECTION: ltr" Checked="True"></asp:CheckBox></td>
											<td width="137" height="21" align="justify" valign="top" dir="rtl">
												*موافقتنامه :
                                            </td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="34" valign="middle" align="center"><asp:Button ID="submitBtn" runat="server" ToolTip="ارسال اطلاعات و ورود به مرحه بعد" Text="ارسال اطلاعات و ورود به مرحه بعد" dir="rtl" class="v3ibbtn" onclick="submit_Click"/></td>
							</tr>
						</table>
					</form>
				</td>
			</tr>
			<tr>
				<td height="2"></td>
			</tr>
		</table></center></DIV>
<div class=cb>
  <div class=cl>
    <div id=terms><asp:PlaceHolder ID="CopyrightFooterControl" runat="server"></asp:PlaceHolder></div>
  </div>
</div>
</DIV>
</BODY></HTML>