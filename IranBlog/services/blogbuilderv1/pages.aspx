<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pages.aspx.cs" Inherits="services.blogbuilderv1.pages" ValidateRequest="false"%>
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
			<link rel="stylesheet" type="text/css" href="styles/cp.css" />
			<script type="text/javascript" src="/services/js/AjaxCore.js"></script>
			<script language="javascript" type="text/javascript" src="js/common.js"></script>
            <script language="javascript" type="text/javascript" src="js/pages.js"></script>
            <script type="text/javascript" src="htmlarea.js"></script>
            <script type="text/javascript" src="htmlarea-lang-en.js"></script>
            <script type="text/javascript" src="dialog.js"></script>
            <script language="javascript" src="/js/farsi.js" type="text/javascript"></script>
			<STYLE>
			.Textbox {
            BORDER-RIGHT: #c0c0c0 1px solid; BORDER-TOP: #c0c0c0 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #c0c0c0 1px solid; WIDTH: 270px; COLOR: #000000; BORDER-BOTTOM: #c0c0c0 1px solid; FONT-FAMILY: Tahoma; HEIGHT: 20px; BACKGROUND-COLOR: #ffffff
            }
            </STYLE>
  </HEAD>

<BODY dir=rtl style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white">
<center>
<form onsubmit="return false;">
<div id="_IsInEditMode" style="display:none">false</div>
<div id="PageID" style="display:none">-1</div>
<div id="message" class="message" style="display:none"></div>
			    <div class="box" style="WIDTH:550px">
				<div class="content" align="text-align:right">
					<!-- START TABLE -->
					<table style="FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; BORDER-COLLAPSE: collapse; display:none" cellSpacing="1" width="100%" border="0" id="NewOrEditPageSection">
                    <div class="header">
                        <div class="title">ایجاد صفحه جدید یا ویرایش صفحه انتخاب شده</div>
                    </div>
						<tr>
							<td style="HEIGHT: 21px" width="100%" height="21" >عنوان مطلب:
								<input name="title" type="text" id="title" class="TextBox" style="width:450px;" maxlength="200" />
							</td>
						</tr>
                        <ThemeSection>
                        <tr>
                            <td style="HEIGHT: 21px" width="100%" height="21">
                                <table width="100%">
                                <tr>
                                <td >کد قالب صفحه:</td>
                                </tr>
                                </table>
							</td>
                            </tr>						
                            <tr>
                                <td width="100%" align="left"><textarea name="ThemeContent" id="ThemeContent" class="TextBox" style="WIDTH: 520px; HEIGHT: 241px" rows="15" cols="67"></textarea></td>
                            </tr>
                        </ThemeSection>
                        <PostSection>
                            <tr>
                            <td style="HEIGHT: 21px" width="100%" height="21">
                                <table width="100%">
                                <tr>
                                <td >مطلب صفحه:</td>
                                </tr>
                                </table>
                               </td>
                            </tr>						
                            <tr>
                                <td width="100%" align="left" dir="ltr"><textarea name="PostContent" id="PostContent" class="TextBox" style="WIDTH: 520px; HEIGHT: 241px" rows="15" cols="67"></textarea></td>
                            </tr>
                        </PostSection>
		
						<tr>
							<td width="100%" style="height: 31px"><hr color="#b0b7f2" size="1" /></td>
						</tr>
						<tr>
							<td align="left" width="100%" height="20">
                                <input id="cancel" class="oBtn" style="width: 100%px; height: 23px" type="button"
                                    value="انصراف و بازگشت" onclick="Cancel();dynaframe();" />&nbsp; &nbsp;<input type="submit" name="DoPostBtn" value="ثبت اطلاعات صفحه و بازسازی وبلاگ" onClick="DoPost();dynaframe();" id="DoPostBtn" class="oBtn" style="height:23px;width:201px;" />&nbsp;							</td>
						</tr>
					</table>
				  <!--END TABLE -->

				</div>
			</div>

<DIV class=box style="WIDTH: 550px">
<DIV class=header>
<DIV class=title>صفحات وبلاگ</DIV></DIV>
<DIV class=content style="TEXT-ALIGN: center"><BR>
  <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>&nbsp;
    <input class=oBtn title="امکان ویرایش کامل کدها" style="WIDTH: 209px" type=button value="ایجاد صفحه جدید با ویرایش کامل کدها " onclick="DefineNewPage();dynaframe();" id="DefineNewPageBtn" name="DefineNewPageBtn"/>
  </DIV>
<div id="result"></div>
</DIV>
</DIV>
</form>
<script language="javascript">
	ShowPages('1');
</script>
</center>
</BODY>
</HTML>