<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax.account.aspx.cs" Inherits="services.blogbuilderv1.ajax.account" %>

<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa" />
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="/services/styles/cp.css" />
		<script type="text/javascript" src="/services/js/AjaxCore.js"></script>
		<script type="text/javascript" src="/services/js/Functions.js"></script>
		<script type="text/javascript" src="/services/js/farsi.js"></script>
        <script language="javascript" type="text/javascript" src="js/common.js"></script>
  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white" onUnload="_editorIsDefined=false;">
<center>
				<div id="message" class="message" style="display:none"></div>
			    <div class="box" style="WIDTH:550px; display:block" id="up">
				<div class="content" align="text-align:right">
             <form id="ChangePassword" name="ChangePassword" onSubmit="return false;" method="post">
			  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">
						تغییر کلمه عبور<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <input name="LastPassword" type="password" id="LastPassword" class="Textbox" style="width:200px;" maxlength="12"/>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :کلمه عبور قدیمی</span><span class="s">*</span></td>
						</tr>	
						
						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <input name="NewPassword" type="password" id="NewPassword" class="Textbox" style="width:200px;" maxlength="12"/>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :کلمه عبور جدید</span><span class="s">*</span></td>
						</tr>
						
						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <input name="ConfirmNewPassword" type="password" id="ConfirmNewPassword" class="Textbox" style="width:200px;" maxlength="12"/>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :تکرار کلمه عبور جدید</span><span class="s">*</span></td>
						</tr>

		
						<tr>
							<td colspan="3" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="112"><img src="/services/images/save.gif" onclick="DoPost('ajax.account.save.password');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td height="20" align="center" dir="rtl" colspan="2">&nbsp;</td>
				</tr>
			   </table>
</form>
               <form action="ajax.account.aspx" method="post" id="SaveAvatorForm" name="SaveAvatorForm" onsubmit="return AIM.submit(this, {'onStart' : startCallback, 'onComplete' : completeCallback})"  runat="server">

                  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">
						تنظیمات اصلی
          وبلاگ<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:textbox ID="BlogTitle" class="Textbox" MaxLength="100" style="width:200px;text-align:right" lang="fa" runat="server" onkeypress="if(window.event.keyCode==13)return false;"></asp:textbox>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :عنوان وبلاگ</span><span class="s">*</span></td>
						</tr>	
						
						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:textbox name="BlogAbout" id="BlogAbout" class="Textbox" style="width:200px;text-align:right" MaxLength="200" runat="server" lang="fa" onkeypress="if(window.event.keyCode==13)return false;"></asp:textbox>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :درباره شما</span></td>
						</tr>
						
						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:dropdownlist CssClass="Textbox" id="BlogCategory" runat="server" style="width:200px;">
                                                                            <asp:ListItem Value="learning" Selected="True">آموزشي</asp:ListItem>
                                                                            <asp:ListItem Value="news">اخبار</asp:ListItem>
                                                                            <asp:ListItem Value="literature">ادبيات</asp:ListItem>
                                                                            <asp:ListItem Value="internet">اينترنت</asp:ListItem>
                                                                            <asp:ListItem Value="medical">پزشكي</asp:ListItem>
                                                                            <asp:ListItem Value="trade">تجارت</asp:ListItem>
                                                                            <asp:ListItem Value="memory">خاطره</asp:ListItem>
                                                                            <asp:ListItem Value="newspaper">روزنامه نگاري</asp:ListItem>
                                                                            <asp:ListItem Value="life">زندگي</asp:ListItem>
                                                                            <asp:ListItem Value="cinema">سينما</asp:ListItem>
                                                                            <asp:ListItem Value="personal">شخصي</asp:ListItem>
                                                                            <asp:ListItem Value="nature">طبيعت</asp:ListItem>
                                                                            <asp:ListItem Value="satire">طنز</asp:ListItem>
                                                                            <asp:ListItem Value="emotional">عاطفی</asp:ListItem>
                                                                            <asp:ListItem Value="general">عمومي</asp:ListItem>
                                                                            <asp:ListItem Value="philosophy">فلسفه</asp:ListItem>
                                                                            <asp:ListItem Value="computer">کامپیوتر</asp:ListItem>
                                                                            <asp:ListItem Value="joke">لطیفه</asp:ListItem>
                                                                            <asp:ListItem Value="religion">مذهب</asp:ListItem>
                                                                            <asp:ListItem Value="music">موسيقي</asp:ListItem>
                                                                            <asp:ListItem Value="authoring">نويسندگي</asp:ListItem>
                                                                            <asp:ListItem Value="art">هنر</asp:ListItem>
                                                                            <asp:ListItem Value="sport">ورزش</asp:ListItem>
                                                                      </asp:dropdownlist>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :مقوله وبلاگ</span><span class="s">*</span></td>
						</tr>
                        
                        						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:textbox name="BlogMaxPostShow" id="BlogMaxPostShow" class="Textbox" 
                                  style="width:200px;" maxlength="2" runat="server" 
                                  onkeypress="if(window.event.keyCode==13)return false;">10</asp:textbox>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :تعداد نمایش پستها در صفحات وبلاگ</span><span class="s">*</span></td>
						</tr>
                        
                        						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:dropdownlist CssClass="Textbox" id="BlogArciveDisplayMode" runat="server" 
                                  style="width:200px;">
                                                                              <asp:ListItem Value="true">آرشیو مطالب بصورت ماهانه</asp:ListItem>
                                                                              <asp:ListItem Value="false">آرشیو مطالب بصورت هفتگی</asp:ListItem>
                                                                            </asp:dropdownlist>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :نحوه آرشیو مطالب وبلاگ</span><span class="s">*</span></td>
						</tr>


						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:textbox name="FirstName" id="BlogFirstName" class="Textbox" 
                                  style="width:200px;text-align:right" MaxLength="30" runat="server" lang="fa" 
                                  onkeypress="if(window.event.keyCode==13)return false;"></asp:textbox>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :نام</span></td>
						</tr>
                        
                        						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:textbox name="LastName" id="BlogLastName" class="Textbox" style="width:200px;text-align:right" 
                                  MaxLength="30" runat="server" lang="fa" 
                                  onkeypress="if(window.event.keyCode==13)return false;"></asp:textbox>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :نام خانوادگی</span></td>
						</tr>
                        
                        						<tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
						  <asp:textbox name="email" id="BlogEmail" class="Textbox" style="width:200px;" 
                                  MaxLength="50" runat="server" 
                                  onkeypress="if(window.event.keyCode==13)return false;"></asp:textbox>
				          </td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :ایمیل</span><span class="s">*</span></td>
						</tr>
                        
                        <tr>
						  <td width="112">&nbsp;</td>
					      <td align="center" valign="middle" style="width: 324px">
                              <asp:dropdownlist CssClass="Textbox" id="BlogEmailEnable" runat="server" 
                                  style="width:200px;">
					        <asp:ListItem Value="enable">بلی</asp:ListItem>
					        <asp:ListItem Value="disable">خیر</asp:ListItem>
				          </asp:dropdownlist></td>
					      <td width="431" align="left" valign="middle" style="width: 215px">
				          <span class="title"> :نمایش ایمیلتان در وبلاگتان</span><span class="s">*</span></td>
						</tr>
		
						<tr>
							<td colspan="3" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="112"><img src="/services/images/save.gif" onclick="DoPost('ajax.account.save.settings');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td height="20" align="center" dir="rtl" colspan="2">&nbsp;</td>
				</tr>
			   </table>
               
                               <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">
						عکس (آواتور) وبلاگ
						<tr>
						  <td width="135"><asp:Image ID="ProfileImage" runat="server" Height="120px" Width="120px" ToolTip="پیش نمایش عکس وبلاگ" /></td>
					      <td width="269" align="right" valign="middle">
                            <input id="file" type="file" runat="server" onkeypress="if(window.event.keyCode==13)return false;" style="width:300px"/>
				        </td>
					      <td width="136" align="center" valign="middle">  <span class="title"> :فایل تصویر</span><span class="s">*</span></td>
						</tr>	
		
						<tr>
							<td colspan="3" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="135"><input type="image" src="/services/images/save.gif"/>
                          </td>
                          <td height="20" colspan="2" align="right" class="s" dir="rtl">
              <span lang="fa">حداقل ابعاد تصویر </span>48x48<span lang="fa"> 
                و حداکثر حجم عکس 256 کیلو بایت 
                می باشد.</span>
              <span dir="ltr"><br>(.jpg, .gif, .png)</span></td>
				</tr>
					</table>
                    
                    
               </form>
               
               
				</div>
			</div>
</center>
</BODY>
</HTML>