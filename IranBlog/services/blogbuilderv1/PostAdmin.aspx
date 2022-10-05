<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostAdmin.aspx.cs" Inherits="services.blogbuilderv1.PostAdmin" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="/services/styles/cp.css" />
        <script type="text/javascript" src="/services/jscripts/tiny_mce/tiny_mce_gzip.js"></script>
		<script type="text/javascript" src="/services/js/AjaxCore.js"></script>
		<script type="text/javascript" src="/services/js/Functions.js"></script>
		<script type="text/javascript" src="/services/js/farsi.js"></script>
        <script language="javascript" type="text/javascript" src="js/common.js"></script>
        <script type="text/javascript">
			tinyMCEInit();
        </script>
  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white" onUnload="_editorIsDefined=false;">
<center>
			<form id="form" name="form" onSubmit="return false;" method="post" runat="server">
				<div id="message" class="message" style="display:none"></div>
			    <div class="box" style="WIDTH:550px; display:none" id="up">
				<div class="content" align="text-align:right">
			  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">ارسال مطلب جدید و یا ویرایش مطلب انتخاب شده</div>
                        </div>
                       <PostSection>
						<tr>
						  <td width="46%" height="21" align="center" valign="middle"><asp:DropDownList ID="CategoryID" runat="server" CssClass="Textbox" style="width:150px"></asp:DropDownList>
					      <span class="title">:آرشیو موضوعی </span></td>
					      <td width="54%" align="center" valign="middle"><input name="PostTitle" type="text" id="PostTitle" class="Textbox" style="width:200px;direction:rtl;text-align:right" maxlength="200" lang="fa"/>
				          <span class="title"> :عنوان مطلب</span></td>
						</tr>		
                            <tr>
                              <td colspan="2" align="center"><textarea name="PostContent" id="PostContent" class="Textbox" style="WIDTH: 520px; HEIGHT: 500px" rows="15" cols="67"></textarea></td>
                            </tr>
                            <tr>
                            <td height="21" colspan="2">
                         <tr>
                                <td align="left" ><a href="javascript:AddContinuedSection(false);" id="removetb" style="HEIGHT: 20px;display:none">[حذف ادامه مطلب]</a><a href="javascript:AddContinuedSection(true);" id="addtb" style="HEIGHT: 20px; display:block">[درج ادامه مطلب]</a></td>
                         </tr>
                               </td>
                            </tr>	
                </PostSection>
                        <ContinuedPostSection>
                            <tr>
                            <td colspan="2" align="left" valign="middle">
                  <table id="ContinuedPostSection" style="display:none">
                                    <tr>
                                        <td align="right" class="title" >:ادامه مطلب</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" valign="middle"><textarea name="ContinuedPostContent" id="ContinuedPostContent" class="Textbox" style="WIDTH: 520px; HEIGHT: 500px" rows="15" cols="67"></textarea></td>
                                	</tr>
                                </table>     </td>
                          </tr>						
                        </ContinuedPostSection>
		
						<tr>
							<td colspan="2" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="50px"><img src="/services/images/save.gif" onClick="DoPost('PostAdmin');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td height="20" align="center" dir="rtl"><asp:DropDownList ID="comment" runat="server" CssClass="Textbox">
                                  <asp:ListItem value="enabled">نظرخواهی برای این پست فعال باشد</asp:ListItem>
                                  <asp:ListItem value="disabled">امکان درج نظر جدید وجود نداشته باشد</asp:ListItem>
                                    <asp:ListItem Value="PreverifyActivate">نظرات پس از تایید نمایش داده شود</asp:ListItem>
                          </asp:DropDownList></td>
				</tr>
					</table>
				</div>
			</div>
            </form>
            <DIV class=box style="WIDTH: 550px">
            <tr>
				<td width="100%" height="20" colspan="2" align="left">
                	<input class="oBtn" title="ارسال مطلب جدید" style="WIDTH: 209px;" type="submit" value="ارسال مطلب جدید" onClick="New('PostAdmin');" id="new" name="new" />
                	<input id="cancel" class="oBtn" style="width: 100%px; height: 23px; display:none" type="button" value="انصراف و بازگشت" onClick="Cancel();" /></td>
			</tr>
            <DIV class=header>
            <DIV class=title>پست های قبلی</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg" align="center"><img src="/services/images/loading.gif"/></div>
                    <div id="resultText"></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script type="text/javascript">
	ShowItems('1', 'ShowPostAdmin');
</script>