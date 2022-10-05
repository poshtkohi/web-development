<%@ Page Language="c#" Inherits="services.blogbuilderv1.LinkBox"  validateRequest="false" CodeFile="LinkBox.aspx.cs" %>
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
					.style23 { FONT-SIZE: 13px; COLOR: #ffffff; FONT-FAMILY: Tahoma }
                    </style>
             <script language="Javascript1.2">
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
			//-------------------------------------------
			function ContentCheck(form)
			{
			   if(form.content.value != "")
			   {
			      if(form.content.value.length > 204800)
				  {
				     alert(".حجم قالب نمی تواند از 200 کیلو بایت بیشتر باشد");
					 return false;
				  }
				  else return true;
			   }
			   else return true;
			}
			//-------------------------------------------
			function ValidateContent(field)
			{
			   if(!BlankField(field, ".فیلد محتوی قالب خالی است"))
			   {
				     return false;
			   } 
			   else
			   {
			       return true;
			   }
			}
			//-------------------------------------------
			function ValidateForm(form)
			{
				if(!ValidateContent(form.content))
					return false;
				else return true;
			}
			//---------------------------------------------------------------------------------------------------------------------------
					</script>
					<SCRIPT language="javascript" src="/services/linkbox/farsi.js" type="text/javascript"></SCRIPT>
					<SCRIPT language="javascript" src="/services/linkbox/farsiEditor.js"></SCRIPT>
                    <script language="javascript" type="text/javascript" src="js/common.js"></script>
	</HEAD>
<body>
<center>
			<form onclick="return ContentCheck(this);" method="post" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<!--DWLayoutTable-->
							<tr>
								<td height="5" colspan="2" align="center"><span class="style23">
								  <asp:label id="ContentError" runat="server" Font-Name="Tahoma" Width="311px" ForeColor="Red"
											Visible="False" Font-Size="X-Small" BackColor="Gold"></asp:label>
							  </span></td>
							</tr>
							<tr>
								<td height="27" colspan="2" class="v3ibbtn">
									<p align="center" class="main">کد لینک باکس وبلاگ</p>							  </td>
							</tr>
							<tr>
								<td colspan="2" align="center" valign="middle">
									<textarea readonly="readonly" style="MARGIN-TOP: 5px; FONT-SIZE: 8pt; WIDTH: 400px; COLOR: #333333; FONT-FAMILY: Tahoma; HEIGHT: 60px; TEXT-ALIGN: left"
										name="code">&lt;center&gt;&lt;iframe name="dk_linkbox" src="http://<%=((IranBlog.Classes.Security.SigninSessionInfo)this.Session["SigninSessionInfo"]).Subdomain%>.iranblog.com/services/linkbox/Show.aspx" width="500" height="300" scrolling="yes" marginwidth="1" marginheight="1" border="1" frameborder="0" style="border: 1px solid #316D94; "&gt;&lt;/iframe&gt;&lt;/center&gt; </textarea></td>
							</tr>
							<tr>
								<td height="27" colspan="2" class="v3ibbtn">
									<p align="center" class="main"><span lang="fa" xml:lang="fa">افزودن لینک جدید</span>										</p>							  </td>
							</tr>
							<TR>
								<TD colspan="2" align="center" vAlign="top">
									<TABLE style="FONT-SIZE: 9pt; COLOR: #333333; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse"
										borderColor="#111111" height="84" cellSpacing="0" cellPadding="0" width="350" border="0">
										<!--DWLayoutTable-->
										<TBODY>
											<TR>
												<TD align="right" height="14"><SPAN lang="fa">آدرس لینک</SPAN></TD>
												<TD align="left" height="14">
													<asp:TextBox ID="url" runat="server" class="WebBOX" dir="ltr" value="Http://" size="54" maxlength="200" /></TD>
											</TR>
											<TR>
												<TD align="right" height="90"><SPAN lang="fa">عنوان لینک</SPAN></TD>
												<TD align="left" height="77"><asp:TextBox ID="title" TextMode="MultiLine" runat="server" cols="32" rows="8" lang="fa" maxlength="400" />												</TD>
											</TR>
											<TR align="center" valign="middle">
												<TD height="20" colSpan="2">
													<asp:Button id="save" runat="server" Text="  ارسال " class="v3ibbtn" onclick="save_Click"></asp:Button>
													<INPUT type="reset" value=" باز نویسی" name="SendBTN" class="v3ibbtn">												</TD>
											</TR>
										</TBODY>
									</TABLE>								</TD>
							</TR>
							<tr>
							  <td height="180" colspan="2" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
							    <!--DWLayoutTable-->
							    <tr>
							      <td height="25" valign="middle" class="v3ibbtn" dir="rtl"><div align="center" class="style1">اصلاح لینک های قبلی</div></td>
						        </tr>
							    <tr align="center">
							      <td height="171" valign="middle" ><asp:datagrid id="data" runat="server" BorderColor="Black" BorderWidth="1px"
										Font-Names="tahoma" Width="100%" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" GridLines="Horizontal" OnDeleteCommand="data_DeleteCommand" OnPageIndexChanged="data_PageIndexChanged"
										>
<SelectedItemStyle Font-Names="tahoma"></SelectedItemStyle>

<EditItemStyle Font-Names="tahoma"></EditItemStyle>

<AlternatingItemStyle Font-Names="tahoma"></AlternatingItemStyle>

<ItemStyle Font-Names="tahoma" BorderStyle="Solid"></ItemStyle>

<HeaderStyle Font-Names="tahoma" Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Middle"></HeaderStyle>

<FooterStyle Font-Names="tahoma"></FooterStyle>

<Columns>
<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
<asp:BoundColumn DataField="title">
<HeaderStyle Width="500px"></HeaderStyle>

<ItemStyle Font-Size="X-Small" HorizontalAlign="Right" ForeColor="#660066" VerticalAlign="Middle" BackColor="White"></ItemStyle>
</asp:BoundColumn>
<asp:ButtonColumn Text="حذف" ButtonType="PushButton" CommandName="Delete"></asp:ButtonColumn>
</Columns>

<PagerStyle VerticalAlign="Middle" NextPageText="صفحه بعدی" Font-Size="Small" Font-Names="tahoma" PrevPageText="         " HorizontalAlign="Left" BackColor="Tan"></PagerStyle>
                                  </asp:datagrid>
<asp:Label id=GridMessage runat="server" Width="531px" Font-Name="Tahoma" ForeColor="Red" Visible="False" Font-Size="Medium" BackColor="Gold" Font-Names="Tahoma">.هیچ رکورد قبلی برای اصلاح یافت نشد</asp:Label></td>
						        </tr>
							    <tr>
							      <td height="0" valign="top"><asp:label id="LabelFirstLoad" runat="server" ForeColor="White" Font-Size="0pt">true</asp:label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:label id="LabelNextID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>
&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:label id="LabelPrevoiusID" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>
&nbsp;&nbsp;
                                    <asp:label id="LabelTemp" runat="server" ForeColor="White" Font-Size="0pt">Label</asp:label>
&nbsp;&nbsp;&nbsp;
                                  <asp:label id="LabelJ" runat="server" ForeColor="White" Font-Size="0pt">0</asp:label></td>
						        </tr>
						                                    </table></td>
							</tr>
							<tr>
							  <td width="821" height="24" valign="middle" class="v3ibbtn">
							  <p align="center" class="main main">ویرایش قالب لینک باکس وبلاگ</p></td>
							<td width="140" align="center" valign="middle"><asp:button class="v3ibbtn" runat="server" Width="50px" ToolTip="ذخیره قالب لینک باکس وبلاگ شما"
										Text="ذخیره" onclick="submit_Click"></asp:button></td>
							</tr>
							<tr>
								<td height="39" colspan="2" vAlign="top"><asp:textbox class="v3txtboxReg155form" id="content" style="BORDER-TOP-WIDTH: 1px; SCROLLBAR-FACE-COLOR: #999900; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: #000000; SCROLLBAR-HIGHLIGHT-COLOR: #f5700a; OVERFLOW: auto; SCROLLBAR-SHADOW-COLOR: #cc0000; COLOR: #ffffff; DIRECTION: ltr; BORDER-TOP-COLOR: #000000; SCROLLBAR-ARROW-COLOR: #999966; SCROLLBAR-BASE-COLOR: #b44f01; BACKGROUND-COLOR: #99cc00; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: #000000; scrollbar-3d-light-color: #999966; scrollbar-dark-shadow-color: #999966"
										runat="server" Width="550" Height="300" Wrap="false" TextMode="MultiLine" MaxLength="512000"></asp:textbox></td>
							</tr>
			  </table>
</form>
</center>
	</body>
</HTML>
