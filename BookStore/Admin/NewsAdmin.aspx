<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsAdmin.aspx.cs" Inherits="bookstore.admin.NewsAdmin" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="../styles/cp.css" />
        <script type="text/javascript" src="../js/jscripts/tiny_mce/tiny_mce_gzip.js"></script>
		<script type="text/javascript" src="../js/AjaxCore.js"></script>
		<script type="text/javascript" src="../js/Functions.js"></script>
		<script type="text/javascript" src="../js/farsi.js"></script>
        <script type="text/javascript" src="../js/common.js"></script>
        <script type="text/javascript">
			tinyMCEInit();
        </script>
  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white">
<center>
			<form id="form" name="form" onSubmit="return false;" method="post" runat="server">
				<div id="message" class="message" style="display:none"></div>
			    <div class="box" style="WIDTH:550px; display:none" id="up">
				<div class="content" align="text-align:right">
			  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">ارسال خبر جدید و یا ویرایش خبر انتخاب شده</div>
                        </div>
						<tr>
						  <td width="46%" height="21" align="right" valign="middle" colspan="3" style="width: 100%"><input name="NewsTitle" type="text" id="NewsTitle" class="Textbox" style="width:460px;direction:rtl;text-align:right" maxlength="400" lang="fa"/>
				          <span class="title"> :عنوان خبر</span></td>
						</tr>		
                            <tr>
                              <td colspan="3" align="center"><textarea name="NewsContent" id="NewsContent" class="Textbox" style="WIDTH: 520px; HEIGHT: 500px" rows="15" cols="67"></textarea></td>
                            </tr>	
						<tr>
							<td colspan="3" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="344"><img src="../images/save.gif" onClick="DoPost('NewsAdmin');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td width="99" height="20" align="center" dir="rtl"><asp:DropDownList ID="IsTopNews" runat="server" CssClass="Textbox">
                            <asp:ListItem value="0">خبر از نوع عادی</asp:ListItem>
                            <asp:ListItem value="1">خبر از نوع ویژه</asp:ListItem>
                          </asp:DropDownList></td>
                          <td width="99" align="center" dir="rtl"><asp:DropDownList ID="NewsLanguage" runat="server" CssClass="Textbox">
                            <asp:ListItem Value="1">زبان این خبر فارسی باشد</asp:ListItem>
                            <asp:ListItem Value="0">زبان این خبر انگلیسی باشد</asp:ListItem>
                          </asp:DropDownList></td>
		        </tr>
					</table>
				</div>
			</div>
            </form>
            <DIV class=box style="WIDTH: 550px">
            <tr>
				<td width="100%" height="20" colspan="2" align="left">
                	<input class="oBtn" title="ارسال خبر جدید" style="WIDTH: 209px;" type="submit" value="ارسال خبر جدید" onClick="New('NewsAdmin');" id="new" name="new" />
                	<input id="cancel" class="oBtn" style="width: 100%px; height: 23px; display:none" type="button" value="انصراف و بازگشت" onClick="Cancel();" /></td>
			</tr>
            <DIV class=header>
            <DIV class=title>خبر های قبلی</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg" align="center"><img src="/images/loading.gif"/></div>
                    <div id="resultText"></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script type="text/javascript">
	ShowItems('1', 'ShowNewsAdmin');
</script>