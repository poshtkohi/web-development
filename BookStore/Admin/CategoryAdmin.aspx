<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryAdmin.aspx.cs" Inherits="bookstore.admin.CategoryAdmin" validateRequest="false"%>
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
			<form id="form" name="form" onSubmit="return false;" method="post">
				<div id="message" class="message" style="display:none"></div>
			    <div class="box" style="WIDTH:550px; display:none" id="up">
				<div class="content" align="text-align:right">
			  <table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse;" cellSpacing="1" width="100%" border="0">
                        <div class="header">
                            <div class="title">
						تعریف مقوله جدید کتاب و یا ویرایش مقوله&nbsp; کتاب انتخاب شده
						<tr>
						  <td width="112">&nbsp;</td>
					      <td width="431" align="center" valign="middle"><input name="EnglishCategory" type="text" id="EnglishCategory" class="Textbox" style="width:300px;" maxlength="400" dir="ltr"/>
				          <span class="title"> : مقوله به انگلیسی</span></td>
						</tr>	
						
												<tr>
						  <td width="112">&nbsp;</td>
					      <td width="431" align="center" valign="middle"><input name="PersianCategory" type="text" id="PersianCategory" class="Textbox" style="width:300px;" maxlength="400" dir="rtl" lang="fa"/>
				          <span class="title"> : مقوله به فارسی</span></td>
						</tr>	

		
						<tr>
							<td colspan="2" style="height: 5px"><hr color="#b0b7f2" size="1" /></td>
				</tr>
						<tr>
						  <td height="20" align="left" width="112"><img src="../images/save.gif" onclick="DoPost('CategoryAdmin');" id="post" name="post" title="ذخیره" style="cursor:pointer;"/></td>
                          <td height="20" align="center" dir="rtl">&nbsp;</td>
				</tr>
					</table>
				</div>
			</div>
            </form>
            <DIV class=box style="WIDTH: 550px">
            <tr>
				<td width="100%" height="20" colspan="2" align="left">
                	<input class="oBtn" title="تعریف مقوله جدید کتاب" style="WIDTH: 209px;" type="submit" value="تعریف مقوله جدید کتاب" onClick="New('CategoryAdmin');" id="new" name="new" />
                	<input id="cancel" class="oBtn" style="width: 100%px; height: 23px; display:none" type="button" value="انصراف و بازگشت" onClick="Cancel();" /></td>
			</tr>
            <DIV class=header>
            <DIV class=title>مقوله های کتاب قبلی</DIV></DIV>
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
	ShowItems('1', 'ShowCategoryAdmin');
</script>