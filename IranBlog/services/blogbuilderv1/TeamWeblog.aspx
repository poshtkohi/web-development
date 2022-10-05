<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamWeblog.aspx.cs" Inherits="services.blogbuilderv1.TeamWeblog" %>
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
		<script language="javascript" type="text/javascript" src="js/common.js"></script>
	</HEAD>
	<body onload="langFarsi=true;" background="/images/bgmainiranblog.png" style="BACKGROUND-REPEAT:repeat-x"
		bgcolor="#e4e6e3">
		<form method="post" runat="server">
<table id="TableTools" style="FONT-FAMILY: Tahoma" cellSpacing="0" cellPadding="0"
										width="100%" bgColor="#ffffff" border="0">
										<!--DWLayoutTable-->
										<tr>
										  <td width="553" height="31" vAlign="top">
											  <table cellSpacing="0" cellPadding="0" width="100%" background="/images/stretch.jpg" border="0">
												  <!--DWLayoutTable-->
												  <tr>
												    <td width="553"
															height="31" align="center" valign="middle" class="v3ibbtn" style="text-align: center" dir="rtl">
											        <SPAN 
      id=lblTitle>تعریف نویسنده جدید</SPAN></td>
												  </tr>
									        </table></td>
										</tr>
										<tr>
										  <td height="30" align="center" valign="top">&nbsp;
										  <asp:label id="message" runat="server" Font-Names="Tahoma" Width="93px" Font-Name="Tahoma"
													ForeColor="Red" Visible="False" Font-Size="Small" BackColor="Gold"></asp:label></td>
										</tr>
										<tr>
											<td height="385" align="center" vAlign="middle">
												<table cellSpacing="0" cellPadding="0" border="0">
													<!--DWLayoutTable-->
													<tr>
														<td height="85" vAlign="top" style="width: 547px"><DIV class=box>
<DIV class=content>
<TABLE width="100%" border=0 cellPadding=1 cellSpacing=2 dir=rtl style="BORDER-COLLAPSE: collapse">
  <TBODY>
  <TR bgcolor="#CCFF00">
    <TD width="15%" height=20 class=bTD>نام کاربری:</TD>
    <TD width="85%" height=20 class=bTD style="text-align: right">
        <asp:TextBox ID="username" runat="server" CausesValidation="True" style="text-align:left; width:150px" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="UsernameRequiredFieldValidator" runat="server" ControlToValidate="username"
            ErrorMessage="ورود نام کاربری الزامی است"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="UsernameRegularExpressionValidator" runat="server"
            ControlToValidate="username" ErrorMessage="حروف نام کاربری نامعتبر است" ValidationExpression="^[\-0-9a-zA-Z]{1,}$"></asp:RegularExpressionValidator></TD></TR>
  <TR>
    <TD class=bTD width="15%" height=20>نام نویسنده:</TD>
    <TD class=bTD width="85%" height=20 style="text-align: right">
        <asp:TextBox ID="name" runat="server" style="text-align:right; width:300px" onkeypress="FKeyPress()" onkeydown="FBlogKeyDown()" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ControlToValidate="name"
            ErrorMessage="ورود نام نویسنده الزامی است"></asp:RequiredFieldValidator>
    </TD></TR>
  <TR bgcolor="#CCFF00">
    <TD width="15%" height=20 class=bTD>ایمیل نویسنده:</TD>
    <TD width="85%" height=20 class=bTD style="text-align: right">
        <asp:TextBox ID="email" runat="server" style="text-align:left; width:170px" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="email"
            ErrorMessage=" ورود ایمیل نویسنده الزامی است"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email"
            ErrorMessage="حروف ایمیل نامعتبر است" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></TD>
  </TR>
  <TR>
    <TD class=bTD width="15%" height=20>کلمه عبور جدید:</TD>
    <TD class=bTD width="85%" height=20 style="text-align: right">
        <asp:TextBox ID="password" runat="server" style="text-align:left; width:150px" TextMode="Password" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ControlToValidate="password"
            ErrorMessage="ورود کلمه عبور الزامی است"></asp:RequiredFieldValidator></TD></TR>
  <TR bgcolor="#CCFF00">
    <TD width="15%" class=bTD style="height: 20px">تکرار کلمه عبور:</TD>
    <TD width="85%" class=bTD style="height: 20px; text-align: right">
        <asp:TextBox ID="ConfirmPassword" runat="server" style="text-align:left; width:150px" TextMode="Password" MaxLength="50"></asp:TextBox>&nbsp;
        <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="password"
            ControlToValidate="ConfirmPassword" ErrorMessage="کلمه عبور با تکرار کلمه عبور برابر نیست"></asp:CompareValidator></TD></TR>
  <TR>
    <TD width="15%" height=20 vAlign=top bgcolor="#CCFF00" class=bTD>حق دسترسی:</TD>
    <TD width="85%" height=20 vAlign=top bgcolor="#CCFF00" class=bTD style="text-align: right" dir=rtl>
      <TABLE id=chkPermission style="FONT-SIZE: 8pt; FONT-FAMILY: Tahoma" 
      border=0>
        <TBODY>
        <TR>
          <TD bgcolor="#FFFFFF" style="text-align: right; width: 454px;"><LABEL for=chkPermission_0>
              <asp:CheckBox ID="PostAccess" runat="server" Checked="true" />
              دسترسی و ویرایش 
            مطالب نوشته شده توسط خود نویسنده</LABEL></TD></TR>
        <TR>
          <TD style="text-align: right; width: 454px;">
              <asp:CheckBox ID="SubjectedArchiveAccess" runat="server" /><LABEL for=chkPermission_1>ویرایش و تعریف 
            موضوعات جدید برای مطالب</LABEL></TD></TR>
        <TR>
          <TD bgcolor="#FFFFFF" style="text-align: right; width: 454px;">
              <asp:CheckBox ID="WeblogLinksAccess" runat="server" /><LABEL for=chkPermission_2>دسترسی به بخش 
            پیوندهای وبلاگ</LABEL></TD></TR>
        <TR>
          <TD style="text-align: right; width: 454px;">
              <asp:CheckBox ID="DailyLinksAccess" runat="server" /><LABEL for=chkPermission_3>دسترسی به بخش 
            پیوندهای روزانه</LABEL></TD></TR>
        <TR>
          <TD bgcolor="#FFFFFF" style="text-align: right; width: 454px;">
              <asp:CheckBox ID="TemplateAccess" runat="server" /><LABEL for=chkPermission_4>امکان انتخاب قالب 
            جدید یا ویرایش قالب وبلاگ</LABEL></TD></TR>
        <TR>
          <TD style="text-align: right; width: 454px;">
              <asp:CheckBox ID="NewsletterAccess" runat="server" /><LABEL for=chkPermission_3>دسترسی به بخش 
            	خبر نامه وبلاگ</LABEL></TD></TR>
        <TR>
          <TD bgcolor="#FFFFFF" style="text-align: right; width: 454px;">
              <asp:CheckBox ID="OthersPostAccess" runat="server" /><LABEL for=chkPermission_6>امکان دسترسی و 
            ویرایش مطالب دیگر نویسندگان و نظرات بلاگ</LABEL></TD></TR>
			<TR>
          <TD style="text-align: right; width: 454px;">
              <asp:CheckBox ID="LinkBoxAccess" runat="server" /><LABEL for=chkPermission_3>دسترسی به بخش 
            	مدیریت لینک باکس</LABEL></TD></TR>
		<TR>
          <TD bgcolor="#FFFFFF" style="text-align: right; width: 454px;">
              <asp:CheckBox ID="PollAccess" runat="server" /><LABEL for=chkPermission_3>دسترسی به بخش 
            	نظر سنجی وبلاگ</LABEL></TD></TR>
        <TR>
          <TD style="width: 454px">
              <asp:CheckBox ID="FullAccess" runat="server" /><LABEL for=chkPermission_7>مدیریت وبلاگ (دسترسی 
            به همه بخشها و تعریف کاربران جدید و ویرایش 
        آنها)</LABEL></TD></TR></TBODY></TABLE></TD></TR>
  <TR>
    <TD class=bTD vAlign=bottom align=middle width="100%" colSpan=2 style="height: 35px">
        <input id="cancel" type="reset" value="انصراف" />
        &nbsp; &nbsp;
        <asp:Button ID="Save" runat="server" OnClick="Save_Click" Text="درج مشخصات" /></TD></TR>
  <TR>
   </TR></TBODY></TABLE>
</DIV>
														</DIV></td>
													</tr>
												</table>
										  </td>
										</tr>
										<tr>
										  <td vAlign="top">
											  <table cellSpacing="0" cellPadding="0" width="100%" border="0">
												  <!--DWLayoutTable-->
												  <tr>
													  <td width="553"
															height="25" vAlign="middle" class="v3ibbtn" dir="rtl">
														  <div align="center"><span class="style30">اصلاح اطلاعات نویسندگان وبلاگ</span></div>
												    </td>
												  </tr>
												  <tr align="center">
													  <td vAlign="middle" height="192"><asp:datagrid id="data" runat="server" Font-Names="tahoma" Width="100%" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False" BorderWidth="1px" BorderColor="Black" OnEditCommand="data_EditCommand" OnDeleteCommand="data_DeleteCommand">
															  <FooterStyle Font-Names="tahoma"></FooterStyle>
															  <SelectedItemStyle Font-Names="tahoma"></SelectedItemStyle>
															  <EditItemStyle Font-Names="tahoma"></EditItemStyle>
															  <AlternatingItemStyle Font-Names="tahoma"></AlternatingItemStyle>
															  <ItemStyle Font-Names="tahoma" BorderStyle="Solid"></ItemStyle>
															  <HeaderStyle Font-Names="tahoma" Font-Bold="True" HorizontalAlign="Center" ForeColor="Black"
																	VerticalAlign="Middle"></HeaderStyle>
															  <Columns>
																  <asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
																  <asp:BoundColumn DataField="username">
																	  <HeaderStyle Width="500px"></HeaderStyle>
																	  <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" ForeColor="#660066" VerticalAlign="Middle"
																			BackColor="White"></ItemStyle>
																  </asp:BoundColumn>
                                                                  <asp:BoundColumn DataField="PostNum" Visible="False"></asp:BoundColumn>
																  <asp:EditCommandColumn ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="ویرایش">
																	  <HeaderStyle Font-Names="tahoma"></HeaderStyle>
																	  <ItemStyle Font-Names="tahoma"></ItemStyle>
																	  <FooterStyle Font-Names="tahoma"></FooterStyle>
																  </asp:EditCommandColumn>
																  <asp:ButtonColumn Text="حذف" ButtonType="PushButton" CommandName="Delete"></asp:ButtonColumn>
															  </Columns>
															  <PagerStyle VerticalAlign="Middle" NextPageText="صفحه بعدی" Font-Size="Small" Font-Names="tahoma"
																	PrevPageText="         " HorizontalAlign="Left" BackColor="Tan"></PagerStyle>
														  </asp:datagrid><asp:label id="GridMessage" runat="server" Font-Names="Tahoma" Width="531px" Font-Name="Tahoma"
																ForeColor="Red" Visible="False" Font-Size="Medium" BackColor="Gold">.هیچ رکورد قبلی برای اصلاح یافت نشد</asp:label>
														  <asp:label id="LabelFirstLoad" runat="server" ForeColor="White" Font-Size="0pt">true</asp:label>
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
									                                                                </table>
		</form>
	</body>
</HTML>
