<%@ Page Language="C#" AutoEventWireup="true" CodeFile="domain.aspx.cs" Inherits="services.blogbuilderv1.domain" %>
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
.style30 { FONT-SIZE: 13px; COLOR: #666666; FONT-FAMILY: Tahoma }
</style>
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
				//------------------------------------------
				function ValidateSubject(field)
				{
				    if(!BlankField(field, ".فیلد متن آرشیو موضوعی خالی است"))					    
					  return false;
					else return true;	
				}
				//-------------------------------------------
				function ValidateForm(form)
				{
				    if(!ValidateSubject(form.subject))
					      return false;
					else return true;
				}
				//--------------------------------------------------------------------------------------------------------------
					</script>
				<script language="javascript" type="text/javascript" src="js/common.js"></script>
	</HEAD>
	<body>
    <center>
		<form method="post" runat="server">
        <table id="TableTools" style="FONT-FAMILY: Tahoma" height="553" cellSpacing="0" cellPadding="0"
										width="100%" bgColor="#ffffff" border="0">
										<!--DWLayoutTable-->
										<tr>
											<td vAlign="top" width="559" height="31">
												<table cellSpacing="0" cellPadding="0" width="100%" background="/images/stretch.jpg" border="0">
													<!--DWLayoutTable-->
													<tr>
														<td width="559"
															height="27" background="../../images/bgtitleheader.gif" bgcolor="#f3f3f3" class="v3ibbtn" dir="rtl">
															<div align="center">اضافه کردن 
													  دامنه جدید</div>													  </td>
													</tr>
											  </table>											</td>
										</tr>
										<tr>
											<td align="center" height="30">&nbsp;
												<asp:label id="message" runat="server" Font-Names="Tahoma" Width="93px" Font-Name="Tahoma"
													ForeColor="Red" Visible="False" Font-Size="X-Small" BackColor="Gold"></asp:label></td>
										</tr>
										<tr>
											<td vAlign="top" height="85">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<!--DWLayoutTable-->
													<tr>
														<td vAlign="top" width="559" height="79">
															<table id="tit" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<!--DWLayoutTable-->
																<tr>
																	<td vAlign="top" colSpan="3" height="27">
																		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
																			<tr bgcolor="#ccff00">
																				<td width="176" height="22" align="right"><span dir="ltr">http://www.</span></td>
																				<td width="378" vAlign="top">
																					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="245" height="22"><asp:textbox id="_domain" 
																									runat="server" MaxLength="50" Width="200"></asp:textbox></td>
																							<td width="136" align="center" vAlign="middle">
																							    :نام دامنه یا آدرس وبلاگ																						  </td>
																					  </tr>
																					</table>																			  </td>
																			</tr>
																			<tr>
																				<td height="5"></td>
																				<td></td>
																			</tr>
																		</table>																	</td>
																</tr>
																<tr>
																	<td width="34" height="24"></td>
																	<td vAlign="top" align="center" width="366"><asp:button id="store" runat="server" CssClass="v3ibbtn" ToolTip=".با فشردن این دکمه اطلاعات بالا ذخیره خواهد شد"
																			Text="ذخیره" Width="100%" OnClick="store_Click"></asp:button></td>
																	<td width="159"></td>
																</tr>
																<tr>
																	<td height="6"></td>
																	<td></td>
																	<td></td>
																</tr>
															</table>														</td>
													</tr>
												</table>											</td>
										</tr>
										<tr>
											<td vAlign="top" height="407">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<!--DWLayoutTable-->
													<tr>
														<td width="559" height="23">&nbsp;</td>
													</tr>
													<tr>
														<td
															height="25" vAlign="middle" bgcolor="#f3f3f3" class="v3ibbtn" dir="rtl">
															<div align="center">اصلاح 
																دامنه های 
                قبلی</div>													  </td>
													</tr>
													<tr align="center">
														<td vAlign="middle" height="192"><asp:datagrid id="data" runat="server" Font-Names="tahoma" Width="100%" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False" BorderWidth="1px" BorderColor="Black" OnDeleteCommand="data_DeleteCommand">
																<FooterStyle Font-Names="tahoma"></FooterStyle>
																<SelectedItemStyle Font-Names="tahoma"></SelectedItemStyle>
																<EditItemStyle Font-Names="tahoma"></EditItemStyle>
																<AlternatingItemStyle Font-Names="tahoma"></AlternatingItemStyle>
																<ItemStyle Font-Names="tahoma" BorderStyle="Solid"></ItemStyle>
																<HeaderStyle Font-Names="tahoma" Font-Bold="True" HorizontalAlign="Center" ForeColor="Black"
																	VerticalAlign="Middle"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
																	<asp:BoundColumn DataField="domain">
																		<HeaderStyle Width="500px"></HeaderStyle>
																		<ItemStyle Font-Size="X-Small" HorizontalAlign="Right" ForeColor="#660066" VerticalAlign="Middle"
																			BackColor="White"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:EditCommandColumn ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="ویرایش" Visible="False">
																		<HeaderStyle Font-Names="tahoma"></HeaderStyle>
																		<ItemStyle Font-Names="tahoma"></ItemStyle>
																		<FooterStyle Font-Names="tahoma"></FooterStyle>
																	</asp:EditCommandColumn>
																	<asp:ButtonColumn Text="حذف" ButtonType="PushButton" CommandName="Delete"></asp:ButtonColumn>
																</Columns>
																<PagerStyle VerticalAlign="Middle" NextPageText="صفحه بعدی" Font-Size="Small" Font-Names="tahoma"
																	PrevPageText="         " HorizontalAlign="Left" BackColor="Tan"></PagerStyle>
															</asp:datagrid><asp:label id="GridMessage" runat="server" Font-Names="Tahoma" Width="531px" Font-Name="Tahoma"
																ForeColor="Red" Visible="False" Font-Size="Medium" BackColor="Gold">.هیچ رکورد قبلی برای اصلاح یافت نشد</asp:label></td>
													</tr>
													<tr>
														<td height="97"><asp:label id="LabelFirstLoad" runat="server" ForeColor="White" Font-Size="0pt">true</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelNextID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelPrevoiusID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>&nbsp;&nbsp;
															<asp:label id="LabelTemp" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelJ" runat="server" ForeColor="White" Font-Size="0pt">0</asp:label></td>
													</tr>
											  </table>											</td>
										</tr>
									</table>
	</form>
    <div align="right" dir="rtl"><img height="20" src="/images/56%20(10).png" width="17" align="absMiddle">شما 
می توانید وبلاگ خود را در یک دامنه(Domain) یا سایت کاملا مستقل نمایش دهید.برای 
مثال آدرس وبلاگ شما به جای آنکه به صورت yourname.<span lang="en-us">iranblog</span>.com 
باشد یک دامنه یا سایت مستقل و بصورت yourname.com خواهد بود.<span lang="en-us">
</span>برای اتصال دامنه به وبلاگ ابتدا بایستی نام دامنه را توسط شرکتهای میزبانی 
وب ثبت کنید و سپس تنظمیات زیر را در قسمت DNS های دامنه خود قرار دهید.<br>
&nbsp;<div dir="ltr">
	<span lang="en-us">ns1.iranblog.com<br>
	ns2.iranblog.com<br>
&nbsp;</span></div>
<br>
<img height="20" src="/images/56%20(10).png" width="17" align="absMiddle">نام 
دامنه را بدون www یا http وارد کنید برای مثال mywebsite.com
</div>
  </center>  
	</body>
</HTML>
