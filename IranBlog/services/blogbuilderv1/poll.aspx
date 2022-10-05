<%@ Page language="c#" Inherits="services.blogbuilderv1.poll" CodeFile="poll.aspx.cs" %>
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
.style1 { FONT-SIZE: 10pt }
.style13 { FONT-SIZE: 7pt; COLOR: #ff0000 }
</style>
					<script language="javascript" src="/js/farsi.js" type="text/javascript">
 /* All rights reserved to Mr. Alireza Poshtkohi (C) 2005-2007. alireza.poshtkohi@gmail.com */
					</script>
		<script language="javascript" type="text/javascript" src="js/common.js"></script>
	</HEAD>
	<body>
    <center>
		<form id="s" name="s" method="post" runat="server">
<table width="100%" height="553" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"
										id="TableTools" style=" FONT-FAMILY:Tahoma">
										<!--DWLayoutTable-->
										<tr>
											<td width="559" height="31" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" background="/images/stretch.jpg">
													<!--DWLayoutTable-->
													<tr>
														<td width="559" height="27" dir="rtl"><div align="center" class="v3ibbtn">
																مدیریت نظرسنجی وبلاگ</div>
													  </td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td height="30" align="center">&nbsp;
												<asp:Label id="message" runat="server" BackColor="Gold" Font-Size="Small" Visible="False" ForeColor="Red"
													Font-Name="Tahoma" Width="93px" Font-Names="Tahoma"></asp:Label></td>
										</tr>
										<tr>
											<td height="85" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
													<!--DWLayoutTable-->
													<tr>
														<td width="559" height="79" valign="top"><table id="tit" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<!--DWLayoutTable-->
																<tr>
																	<td height="27" vAlign="top">
																		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
																			<tr bgcolor="#cccccc">
																				<td width="35" height="22" bgcolor="#ccff00"></td>
																				<td width="520" vAlign="top" bgcolor="#ccff00">
																					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="362" height="22"><asp:textbox onkeypress="FKeyPress()" id="question" onkeydown="FBlogKeyDown()" runat="server"
																									ToolTip=".در این قسمت سوال نظرسنجی خود را تایپ کنید" CssClass="v3txtboxReg155form" MaxLength="400" style="TEXT-ALIGN:right"
																									Width="100%"></asp:textbox></td>
																							<td width="158" vAlign="top">
																								<P align="center" class="style26"><FONT color="black">: سوال</FONT></P>
																							</td>
																						</tr>
																					</table>
																				</td>
																			</tr>
																			<tr>
																				<td height="5"></td>
																				<td></td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td height="28" vAlign="top" bgcolor="#cccccc"><table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
																			<tr>
																				<td width="554" height="22" vAlign="top">
																					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="554" height="22" align="center" vAlign="middle" bgcolor="#cccccc">
																								<asp:Button class="v3ibbtn" id="define" runat="server" ToolTip=".با فشردن این دکمه نظرسنجی وبلاگ شما به همراه سوال آن ایجاد و تعریف خواهد شد"
																									Height="24px" Text="تعریف سوال" onclick="define_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																								<asp:Button class="v3ibbtn" id="modify" runat="server" ToolTip=".با فشردن این دکمه متن سوال نظرسنجی وبلاگ شما تغییر خواهد کرد"
																									Height="24px" Text="اصلاح سوال" onclick="modify_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																								<asp:Button class="v3ibbtn" id="delete" runat="server" ToolTip=".با فشردن این دکمه نظرسنجی وبلاگ شما به همراه تمامی اطلاعات آن از سیستم حذف خواهد شد"
																									Height="24px" Text="حذف این نظرسنجی" onclick="delete_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;</td>
																						</tr>
																					</table>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td width="554" height="30"><br>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td height="407" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
													<!--DWLayoutTable-->
													<tr>
														<td width="554" height="23">&nbsp;</td>
													</tr>
													<tr>
														<td height="25" valign="middle" class="v3ibbtn" dir="rtl"><div align="center">
																تعریف یا اصلاح جواب های&nbsp;نظرسنی وبلاگ</div>
													  </td>
													</tr>
													<tr>
														<td height="30" align="center">&nbsp;
															<asp:Label id="MessageResponse" runat="server" BackColor="Gold" Font-Size="Small" Visible="False"
																ForeColor="Red" Font-Name="Tahoma" Width="93px" Font-Names="Tahoma"></asp:Label></td>
													</tr>
													<tr align="center">
														<td height="30"><table id="tit" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<!--DWLayoutTable-->
																<tr>
																	<td height="27" vAlign="top">
																		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
																			<tr bgcolor="#cccccc">
																				<td width="35" height="22" bgcolor="#ccff00"></td>
																				<td width="520" vAlign="top" bgcolor="#ccff00">
																					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="362" height="22"><asp:textbox onkeypress="FKeyPress()" id="response" onkeydown="FBlogKeyDown()" runat="server"
																									ToolTip=".در این قسمت جواب نظرسنجی وبلاگ خود را تایپ نمایید" CssClass="v3txtboxReg155form" MaxLength="400" style="TEXT-ALIGN:right"
																									Width="100%"></asp:textbox></td>
																							<td width="158" vAlign="top">
																								<P align="center" class="style26"><FONT color="black">: جواب</FONT></P>
																							</td>
																						</tr>
																					</table>
																				</td>
																			</tr>
																			<tr>
																				<td height="5"></td>
																				<td></td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td height="28" vAlign="top" bgcolor="#cccccc"><table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<!--DWLayoutTable-->
																			<tr>
																				<td width="554" height="22" vAlign="top">
																					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<!--DWLayoutTable-->
																						<tr>
																							<td width="554" height="22" align="center" vAlign="middle" bgcolor="#cccccc">
																								<asp:Button class="v3ibbtn" id="define_response" runat="server" ToolTip=".با فشردن این دکمه متن جدید جواب نظرسنجی به سیستم اضافه خواهد شد"
																									Height="24px" Text="تعریف جواب جدید" Width="100%" onclick="define_response_Click"></asp:Button>
																							</td>
																						</tr>
																					</table>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td width="554" height="30"><br>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr align="center">
														<td height="162" valign="middle"><asp:datagrid id="data" runat="server" BorderColor="Black" BorderWidth="1px" Font-Names="tahoma"
																Width="100%" AutoGenerateColumns="False" AllowCustomPaging="True" GridLines="Horizontal" OnDeleteCommand="data_DeleteCommand">
																<FooterStyle Font-Names="tahoma"></FooterStyle>
																<SelectedItemStyle Font-Names="tahoma"></SelectedItemStyle>
																<EditItemStyle Font-Names="tahoma"></EditItemStyle>
																<AlternatingItemStyle Font-Names="tahoma"></AlternatingItemStyle>
																<ItemStyle Font-Names="tahoma" BorderStyle="Solid"></ItemStyle>
																<HeaderStyle Font-Names="tahoma" Font-Bold="True" HorizontalAlign="Center" ForeColor="Black"
																	VerticalAlign="Middle"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
																	<asp:BoundColumn DataField="ResponseText">
																		<HeaderStyle Width="500px"></HeaderStyle>
																		<ItemStyle Font-Size="X-Small" HorizontalAlign="Right" ForeColor="#660066" VerticalAlign="Middle"
																			BackColor="White"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:ButtonColumn Text="حذف" ButtonType="PushButton" CommandName="Delete"></asp:ButtonColumn>
																</Columns>
																<PagerStyle VerticalAlign="Middle" NextPageText="صفحه بعدی" Font-Size="Small" Font-Names="tahoma"
																	PrevPageText="         " HorizontalAlign="Left" BackColor="Tan"></PagerStyle>
															</asp:datagrid>
															<asp:Label id="GridMessage" runat="server" Width="531px" Font-Name="Tahoma" ForeColor="Red"
																Visible="False" Font-Size="Medium" BackColor="Gold" Font-Names="Tahoma">.هیچ رکورد قبلی برای اصلاح یافت نشد</asp:Label></td>
													</tr>
													<tr>
														<td height="97"><asp:label id="LabelFirstLoad" runat="server" ForeColor="White" Font-Size="0pt">true</asp:label>
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelNextID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>
															&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelPrevoiusID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>
															&nbsp;&nbsp;
															<asp:label id="LabelTemp" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>
															&nbsp;&nbsp;&nbsp;
															<asp:label id="LabelJ" runat="server" ForeColor="White" Font-Size="0pt">0</asp:label>
															<asp:label id="LabelQuestionID" runat="server" ForeColor="White" Font-Size="0pt">0</asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
		</form>
        </center>
	</body>
</HTML>
