<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsLetter.aspx.cs" Inherits="services.blogbuilderv1.NewsLetter" validateRequest="false"%>
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
</style>
					<script language="javascript" src="/js/farsi.js" type="text/javascript"></script>
					<script language="javascript" type="text/javascript">
			/* All rights reserved to Mr. Alireza Poshtkohi (C) 2002-2007. alireza.poshtkohi@gmail.com */
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
				function ValidateName(field)
				{
				    if(!BlankField(field, ".فیلد نام خالی است"))					    
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
				    if(!ValidateName(form.name))
					      return false;
					 if(!ValidateEmail(form.email))
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
<table width="100%" border="0" cellpadding="0" cellspacing="0">
							<!--DWLayoutTable-->
							<tr>
								<td width="554" height="419" align="center" valign="top">&nbsp;
								  <asp:label id="message" runat="server" Font-Names="Tahoma" Width="93px" Font-Name="Tahoma"
													ForeColor="Red" Visible="False" Font-Size="Small" BackColor="Gold"></asp:label><table id="TableTools" style="FONT-FAMILY: Tahoma" height="407" cellSpacing="0" cellPadding="0"
										width="100%" bgColor="#ffffff" border="0">
										<!--DWLayoutTable-->
										<tr>
											<td vAlign="top" width="555" height="31">
												<table cellSpacing="0" cellPadding="0" width="100%" background="/images/stretch.jpg" border="0">
													<!--DWLayoutTable-->
											  <tr>
														<td width="559"
															height="27" class="v3ibbtn" dir="rtl">
													  <div align="center">مدیریت خبرنامه</div>														</td>
												  </tr>
												</table>											</td>
										</tr>
										<tr>
										  <td height="14"></td>
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
																	<td width="555" height="54" vAlign="top">
																		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
															  <tr>
																		<td width="35" height="22"></td>
																	    <td width="520" vAlign="top">
											  <table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="229" valign="top" style="height: 22px"><asp:Button ID="send" runat="server" CssClass="v3ibbtn" 
																			Text="ارسال خبرنامه جدید به اعضای خبر نامه" Width="100%" OnClick="send_Click"></asp:Button></td>
																					  <td width="21" style="height: 22px">&nbsp;</td>
																				      <td width="251" vAlign="top" style="height: 22px"><asp:Button ID="download" runat="server" CssClass="v3ibbtn" 
																			Text="دانلود ایمیل اعضای خبر نامه در یک فایل متنی" Width="100%" OnClick="download_Click"></asp:Button></td>
																					  <td width="19" style="height: 22px">&nbsp;</td>
																					  </tr>
															    </table>																			  </td>
																		  </tr>
																			<tr>
																				<td height="5"></td>
																				<td></td>
																			</tr>
																		</table>				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
															  <tr>
																		<td width="35" height="22"></td>
																	    <td width="520" vAlign="top">
											  <table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="229" valign="top" style="height: 22px"><asp:Button ID="DeleteAll" runat="server" CssClass="v3ibbtn" 
																			Text="حذف تمامی اعضای خبر نامه" Width="100%" OnClick="DeleteAll_Click"></asp:Button></td>
																					  <td width="21" style="height: 22px">&nbsp;</td>
																				      <td width="251" vAlign="top" style="height: 22px"><!--DWLayoutEmptyCell-->&nbsp;</td>
																				      <td width="19" style="height: 22px">&nbsp;</td>
																					  </tr>
															    </table>																			  </td>
																		  </tr>
																			<tr>
																				<td height="5"></td>
																				<td></td>
																			</tr>
																		</table>													</td>
																</tr>
																<tr>
														<td
															height="20" vAlign="middle" dir="rtl">
														  <div align="center" class="v3ibbtn">اضافه کردن عضو جدید</div></td>
													</tr>
																<tr>
																  <td
															height="12">&nbsp;</td>
															  </tr>
													  </table>													  </td>
													</tr>
												</table>											</td>
										</tr>
										<tr>
											<td height="276" vAlign="top">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<!--DWLayoutTable-->
											  <tr>
														<td vAlign="top" width="555">
															<table id="tit" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<!--DWLayoutTable-->
																<tr>
																	<td vAlign="top" colSpan="3" height="27">
																		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
																			<tr bgcolor="#ccff00">
																				<td width="35" height="22"></td>
																				<td width="520" vAlign="top">
																					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="362" style="height: 22px"><asp:textbox onkeypress="FKeyPress()" id="name" onkeydown="FBlogKeyDown()" style="TEXT-ALIGN: right"
																									runat="server" MaxLength="50" CssClass="v3txtboxReg155form"
																									Width="100%"></asp:textbox></td>
																							<td vAlign="top" width="158" style="height: 22px">
																								<P class="style26" align="center"><FONT color="black">:
																			
																			
																								     نام</FONT></P>																							</td>
																						</tr>
                                                                                        <tr>
																							<td width="362" style="height: 22px"><asp:textbox runat="server" MaxLength="50" CssClass="v3txtboxReg155form"
																									Width="100%" style="TEXT-ALIGN: left; direction:ltr" id="email"></asp:textbox></td>
																							<td vAlign="top" width="158" style="height: 22px">
																								<P class="style26" align="center"><FONT color="black">:
																			
																			
																			
																			
																								     ایمیل</FONT></P>																							</td>
																						</tr>
																					</table>																				</td>
																			</tr>
																			<tr>
																				<td height="5"></td>
																				<td></td>
																			</tr>
																		</table>																	</td>
																</tr>
																<tr>
																	<td width="34" style="height: 24px"></td>
																	<td vAlign="top" align="center" width="366" style="height: 24px"><asp:button id="add" runat="server" CssClass="v3ibbtn" ToolTip=".با فشردن این دکمه اطلاعات بالا ذخیره خواهد شد"
																			Text="ذخیره" Width="100%" onclick="add_Click"></asp:button></td>
																	<td width="159" style="height: 24px"></td>
																</tr>
															</table>														</td>
													</tr>
													<tr>
														<td
															height="25" vAlign="middle" class="v3ibbtn" dir="rtl">
													  <div align="center">مشاهده و ویرایش اعضای خبرنامه</div>														</td>
												  </tr>
													<tr align="center">
														<td vAlign="middle"><asp:datagrid id="data" runat="server" Font-Names="tahoma" Width="100%" GridLines="Horizontal"
																AllowPaging="True" AllowCustomPaging="True" AutoGenerateColumns="False" BorderWidth="1px" BorderColor="Black" OnDeleteCommand="data_DeleteCommand" OnPageIndexChanged="data_PageIndexChanged">
										      <FooterStyle Font-Names="tahoma"></FooterStyle>
																<SelectedItemStyle Font-Names="tahoma"></SelectedItemStyle>
																<EditItemStyle Font-Names="tahoma"></EditItemStyle>
																<AlternatingItemStyle Font-Names="tahoma"></AlternatingItemStyle>
																<ItemStyle Font-Names="tahoma" BorderStyle="Solid"></ItemStyle>
																<HeaderStyle Font-Names="tahoma" Font-Bold="True" HorizontalAlign="Center" ForeColor="Black"
																	VerticalAlign="Middle"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
																	<asp:BoundColumn DataField="name" HeaderText="نام">
																		<HeaderStyle Width="150px"></HeaderStyle>
																		<ItemStyle Font-Size="X-Small" HorizontalAlign="Center" ForeColor="#660066" VerticalAlign="Middle"
																			BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
																	</asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="email" HeaderText="ایمیل">
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" ForeColor="#3366CC" />
                                                                        <HeaderStyle Width="350px" />                                                                    </asp:BoundColumn>
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
																ForeColor="Red" Font-Size="Medium" BackColor="Gold">.هیچ رکورد قبلی برای اصلاح یافت نشد</asp:label></td>
													</tr>
													<tr>
														<td valign="top"><asp:label id="LabelFirstLoad" runat="server" ForeColor="White" Font-Size="0pt">true</asp:label>														  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelNextID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>															&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelPrevoiusID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>															&nbsp;&nbsp;
															<asp:label id="LabelTemp" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>															&nbsp;&nbsp;&nbsp;
														<asp:label id="LabelJ" runat="server" ForeColor="White" Font-Size="0pt">0</asp:label></td>
												  </tr>
                                          </table></td>
								  </tr>
							  </table>							  </td>
    </tr>
						</table>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<!--DWLayoutTable-->
							<tr>
							  <td width="554" height="27" align="center" valign="middle" class="v3ibbtn">
							  <p align="center" class="main" dir="rtl">تگ
								<span lang="en-us">HTML</span> استاندارد خبرنامه 
							  وبلاگ های ایران بلاگ</td>
							</tr>
							<tr>
							  <td align="center" valign="middle">
								<textarea style="MARGIN-TOP: 5px; FONT-SIZE: 8pt; WIDTH: 400px; COLOR: #333333; FONT-FAMILY: Tahoma; HEIGHT: 150px; TEXT-ALIGN: left"
										name="code" cols="100" rows="30"><{newsletter}>
                                        <form method="POST" action='{$$newsletter}' target="Mail" onSubmit="window.open('', 'Mail', 'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=0,width=400,height=100')">
<div dir='rtl'>
	<input type='text' name='name' size='20' dir='ltr' maxlength="50">:نام&nbsp;<br>
	<input type='text' name='email' size='20' dir='ltr' maxlength="50">:ایمیل&nbsp;<br>
    <input type='submit' value='انجام' name='submit'><BR>
    <input type='radio' value='add' checked name='MailAction'>اضافه
    <input type='radio' name='MailAction' value='delete'>حذف</div>
</form>
</{newsletter}></textarea></td>
		  </tr>
	  </table>
	</form>
    </center>
	</body>
</HTML>