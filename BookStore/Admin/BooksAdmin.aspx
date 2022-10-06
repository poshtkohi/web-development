<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BooksAdmin.aspx.cs" Inherits="bookstore.admin.BooksAdmin" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="../styles/cp.css" />
		<script type="text/javascript" src="../js/AjaxCore.js"></script>
		<script type="text/javascript" src="../js/Functions.js"></script>
		<script type="text/javascript" src="../js/farsi.js"></script>
        <script type="text/javascript" src="../js/common.js"></script>
  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white">
<center>
			<form id="form" name="form" onSubmit="return false;" method="post" runat="server">
				<div id="message" class="message" style="display:none"></div>
			    <div class="box" style="WIDTH:550px; display:none" id="up">
				<div class="content" align="text-align:right">
			  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">
						تعریف کتاب جدید و یا ویرایش&nbsp;کتاب انتخاب شده</div></div>
						<tr>
						  <td width="112"><textarea name="EnglishCategory" id="EnglishCategory" class="Textbox" style="WIDTH: 300px; HEIGHT: 50px" rows="15" cols="67"></textarea></td>
					      <td width="100" align="center" valign="middle"> : English Category</td>
						</tr>	
                        
                        <tr>
						  <td width="112"><textarea name="PersianCategory" id="PersianCategory" class="Textbox" style="WIDTH: 300px; HEIGHT: 50px" rows="15" cols="67" lang="fa"></textarea></td>
					      <td width="100" align="center" valign="middle"> :Persian Category</td>
						</tr>	
                        
						<tr>
						  <td width="112"><input name="Title" type="text" id="Title" class="Textbox" style="width:300px;" maxlength="400" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : Title</td>
						</tr>	

						<tr>
						  <td width="112"><input name="Writer" type="text" id="Writer" class="Textbox" style="width:300px;" maxlength="100" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : Writer</td>
						</tr>	

						<tr>
						  <td width="112"><input name="Translator" type="text" id="Translator" class="Textbox" style="width:300px;" maxlength="100" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : Translator</td>
						</tr>	

						<tr>
						  <td width="112"><input name="Publisher" type="text" id="Publisher" class="Textbox" style="width:300px;" maxlength="400" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : Publisher</td>
						</tr>	

						<tr>
						  <td width="112"><input name="PublishDate" type="text" id="PublishDate" class="Textbox" style="width:300px;" maxlength="4" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : Publish Date</td>
						</tr>	

						<tr>
						  <td width="112"><input name="Pages" type="text" id="Pages" class="Textbox" style="width:300px;" maxlength="100" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : Pages</td>
						</tr>	

						<tr>
						  <td width="112"><input name="ISBN" type="text" id="ISBN" class="Textbox" style="width:300px;" maxlength="50" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : ISBN</td>
						</tr>	

						<tr>
						  <td width="112"><asp:DropDownList ID="FileType" 
                                  runat="server" CssClass="Textbox" style="width:300px">
						    <asp:ListItem Value="0">PDF</asp:ListItem>
						    <asp:ListItem Value="1">Word</asp:ListItem>
					      </asp:DropDownList></td>
					      <td width="100" align="center" valign="middle"> : File Type</td>
						</tr>	

						<tr>
						  <td width="112"><input name="FileSize" type="text" id="FileSize" class="Textbox" style="width:300px;" maxlength="100" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : File Size</td>
						</tr>	

						<tr>
						  <td width="112"><input name="Price" type="text" id="Price" class="Textbox" style="width:300px;" maxlength="400" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : Price</td>
						</tr>	

						<tr>
						  <td width="112"><textarea name="Abstract" id="Abstract" class="Textbox" style="WIDTH: 300px; HEIGHT: 50px" rows="15" cols="67"></textarea></td>
					      <td width="100" align="center" valign="middle"> : Abstract</td>
						</tr>	

						<tr>
						  <td width="112"><input name="filename" type="text" id="filename" class="Textbox" style="width:300px;" maxlength="400" dir="ltr"/></td>
					      <td width="100" align="center" valign="middle"> : File Name</td>
						</tr>	

						<tr>
						  <td width="112"><asp:DropDownList ID="Language" 
                                  runat="server" CssClass="Textbox" style="width:300px">
						    <asp:ListItem Value="0">English</asp:ListItem>
						    <asp:ListItem Value="1">Persian</asp:ListItem>
					      </asp:DropDownList></td>
					      <td width="100" align="center" valign="middle">&nbsp;<span class="title">: <span lang="en-us">Language</span></span></td>
						</tr>	



		
						<tr>
							<td colspan="2" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="112"><img src="../images/save.gif" onclick="DoPost('BooksAdmin');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td height="20" align="center" dir="rtl">&nbsp;</td>
				</tr>
					</table>
				</div>
			</div>
            </form>
            <DIV class=box style="WIDTH: 550px">
            <tr>
				<td width="100%" height="20" colspan="2" align="left">
                	<input class="oBtn" title="تعریف کتاب جدید" style="WIDTH: 209px;" type="submit" value="تعریف کتاب جدید" onClick="New('BooksAdmin');" id="new" name="new" />
                	<input id="cancel" class="oBtn" style="width: 100%px; height: 23px; display:none" type="button" value="انصراف و بازگشت" onClick="Cancel();" /></td>
			</tr>
            <DIV class=header>
            <DIV class=title>کتاب های قبلی</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg" align="center"><img src="../images/loading.gif"/></div>
                    <div id="resultText"></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script type="text/javascript">
	ShowItems('1', 'ShowBooksAdmin');
</script>