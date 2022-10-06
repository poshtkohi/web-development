<%@ Page language="c#" Codebehind="NewsAdmin.aspx.cs" AutoEventWireup="false" Inherits="news.NewsAdmin" validateRequest="false"%>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="Controls/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LoginStatusControl" Src="Controls/LoginStatusControl.ascx" %>
<%
string _mode = "ShowNewsAdmin";
string _operationMode = "NewsAdmin";
Int64 _NewsGroupID = (Int64)1;
if(this.Request.QueryString["mode"] != null && this.Request.QueryString["mode"] == "NewsGroup")
{
	_mode = "ShowNewsAdminForNewsGroupsMode";
	_operationMode = "NewsAdminForNewsGroupsMode";
	bool _isForward = false;
	try { _NewsGroupID = Convert.ToInt64(this.Request.QueryString["NewsGroupID"]); }
	catch { _isForward = true; }
	
	if(_isForward)
	{
		this.Response.Redirect("/NewsGroups.aspx", true);
		return ;
	}
	else
		this.LabelNewGroupTitle.Text = news.NewsAdmin.GetNewsGrroupTitleByNewsGroupID(_NewsGroupID);
}
else
	this.LabelNewGroupTitle.Visible = false;
//mode=NewsGroup&NewsGroupID=204
//this.Response.Write(_mode);
%>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>مدیریت اخبار</title>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<link href="styles/main.css" rel="stylesheet" rev="stylesheet" />
		<link href="styles/cp.css" rel="stylesheet" rev="stylesheet" />
		<link href="styles/ContextMenu.css" rel="stylesheet" rev="stylesheet" />
        <link href="/ShamsiDatePicker/CSS/calendar.css" rel="stylesheet" rev="stylesheet">
        <script type="text/javascript" src="/jscripts/tiny_mce/tiny_mce_gzip.js"></script>
        <script type="text/javascript" src="/js/ContextMenu.js"></script>
		<script type="text/javascript" src="/js/AjaxCore.js"></script>
		<script type="text/javascript" src="/js/Functions.js"></script>
		<script type="text/javascript" src="/js/farsi.js"></script>
        <script type="text/javascript" src="/ShamsiDatePicker/JScripts/base.js"></script>
		<script type="text/javascript" src="/ShamsiDatePicker/JScripts/calendar.js"></script>
       	<script src="/Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
        <script type="text/javascript">
			tinyMCEInit();
			SimpleContextMenu.setup({'preventDefault':true, 'preventForms':false});
			function BeginRefresh(){
				ShowItems('<%=_NewsGroupID%>', '<%=_mode%>');
				setTimeout("BeginRefresh()",600000);
			}
        </script>
        <%
			if(_mode == "ShowNewsAdminForNewsGroupsMode")
				this.Response.Write(String.Format("<script type='text/javascript'>_currentNewsGroupID='{0}'</script>", _NewsGroupID));
		%>
</head>
	<body onUnload="_editorIsDefined=false;"<%
if(this.Request.QueryString["mode"] != null && this.Request.QueryString["mode"] == "search")
	this.Response.Write(" class='bodyInvis'");
%> onLoad="BeginRefresh();">
    <div id="MainDiv">
            <!--script type="text/javascript" src="/js/wz_dragdrop.js"></script --> 
		<table width="900" border="0" cellpadding="0" cellspacing="0" align="center">
			<!--DWLayoutTable-->
			<tr>
				<td width="23" rowspan="5" valign="top" background="images/bg_lft.gif"><!--DWLayoutEmptyCell-->&nbsp; 
					</td>
				<td height="167" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF">
						<!--DWLayoutTable-->
		  <tr>
							<td><div>
							  <!--DWLayoutEmptyCell-->
                              <script type="text/javascript">
AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','525','height','170','src','swf/logo','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','swf/logo' ); //end AC code
                            </script>
							  <noscript>
							    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="525" height="170">
                                <param name="movie" value="swf/logo.swf">
                                <param name="quality" value="high">
                                <embed src="swf/logo.swf" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="525" height="170"></embed>
                              </object>
                              </noscript>
		      </div></td>
						  <td width="73" valign="top" bgcolor="#DBEDFF"><!--DWLayoutEmptyCell-->&nbsp; </td>
							<td><div class="header_right"><!--DWLayoutEmptyCell--></div>
							</td>
						</tr>
					</table>
			  </td>
				<td width="25" rowspan="5" valign="top" background="images/bg.gif"><!--DWLayoutEmptyCell-->&nbsp; 
					</td>
			</tr>
			<tr>
				<td height="32" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
						<!--DWLayoutTable-->
						<tr>
							<td>
								<div align="center" class="under"><img src="images/titr1.gif" align="middle">&nbsp;</div>
						  </td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="5" valign="top" bgcolor="#999999"></td>
			</tr>
			<tr>
				<td height="387" valign="top"><table width="100%" height="590" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF">
						<!--DWLayoutTable-->
						<div id="message" class="message" style="display:none"></div>
                        <tr>
                        <td width="67%" align="right" valign="middle" style="font-size:12px" dir="rtl">
                          <asp:Label ID="LabelNewGroupTitle" runat="server" />                        </td>
                        <td width="33%" align="right" valign="middle" class="box"><uc1:LoginStatusControl id="LoginStatusControl" runat="server"></uc1:LoginStatusControl></td>
                  </tr>
						<tr style="display:none" id="up">
							<form id="form" name="form" onSubmit="return false;" method="post">
								<td height="204" colspan="2" valign="top">
									<table width="100%" border="0" cellpadding="0" cellspacing="0">
										<!--DWLayoutTable-->
										<tr>
											<td height="27"></td>
											<td></td>
											<td width="13">&nbsp;</td>
									  </tr>
										<tr>
											<td height="33" valign="top" background="images/pbx-15.gif" width="13"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top" background="images/pbx-18.gif" width="824"><!--DWLayoutEmptyCell-->&nbsp; </td>
										  <td valign="top" background="images/pbx-14.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
										</tr>
										<tr>
											<td height="117" valign="top" background="images/pbx-left.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top" bgcolor="#FFFFFF">
												<table border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
													<!--DWLayoutTable-->
													<tr>
														<td width="825" height="26" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
												          <!--DWLayoutTable-->
												          <tr>
												            <td height="25" colspan="4" align="right" valign="top" class="box">نگارش خبر جدید و یا ویرایش خبر انتخاب شده <img src="images/pbx-30.gif"/></td>
							                              </tr>
                                                          												          <tr>
												            <td width="196" height="26" align="center" valign="middle"><select name="NewsSubject" class="txtBox" id="NewsSubject" style="width:160px">
                      <option value="1" selected>عمومی</option>
                      <option value="2">سیاسی</option>
                      <option value="3">اقتصادی</option>
                      <option value="4">اجتماعی</option>
                      <option value="5">فرهنگی-هنری</option>
                      <option value="6">حوادث</option>
                      <option value="7">مذهبی</option>
                      <option value="8">ورزشی</option>
                      <option value="9">ویژه</option>
                                                                                                                                                                                    </select></td>
								                            <td width="82" valign="middle"><span class="box"> :موضوع</span></td>
								                            <td width="462" align="center" valign="middle"><input name="title" type="text" maxlength="1024" border="1" class="txtBox" id="title" style="width:450px; direction:rtl" lang="fa"></td>
								                            <td width="85" valign="middle"><span class="box"> :عنوان خبر</span></td>
										                  </tr>
											            </table></td>
													</tr>
													<tr>
													  <td height="91" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
													    <!--DWLayoutTable-->
													    <tr>
													      <td width="825" height="243" align="center" valign="middle"><span class="header_left">
													        <textarea id="content" name="content" rows="15" cols="80" style="width: 820px;height: 600px"></textarea>
													      </span></td>
												        </tr>
													    <tr>
													      <td height="41" align="left" valign="middle"><div style="cursor:pointer;"><img src="images/b_2.gif" onClick="DoPost('<%=_operationMode%>');" id="post" name="post" title="ذخیره"/></div></td>
													    </tr>
													    </table>													  </td>
												  </tr>
												</table>											</td>
											<td valign="top" background="images/pbx-right.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
										</tr>
										<tr>
											<td height="27" valign="top" background="images/pbx-17.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
											<td valign="top" background="images/pbx-19.gif"></td>
											<td valign="top" background="images/pbx-16.gif"><!--DWLayoutEmptyCell-->&nbsp; </td>
										</tr>
										<tr>
											<td height="39"></td>
											<td></td>
											<td></td>
										</tr>
									</table>							  </td>
							</form>
						</tr>
						<tr>
							<td colspan="2" valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF" id="down">
									<!--DWLayoutTable-->
									<tr>
									  <td height="19" colspan="2">
                                      <input class="oBtn" title="نگارش خبر جدید" style="WIDTH: 209px;<%if(_mode == "ShowNewsAdminForNewsGroupsMode") this.Response.Write("display:none"); %>" type="submit" value="نگارش خبر جدید" onClick="New('users');" id="new" name="new" /> <input id="cancel" class="oBtn" style="width: 100%px; height: 23px; display:none" type="button"
												value="انصراف و بازگشت" onClick="Cancel();" /></td>
										<td width="35">&nbsp;</td>
									</tr>
									<tr>
										<td width="35" height="276">&nbsp;</td>
										<td width="761" valign="top">
											<div id="loaderImg" align="center"></div>
											<div id="resultText"></div>
										  <script language="javascript">
											ShowItems('<%=_NewsGroupID%>', '<%=_mode%>');
											</script>									  </td>
										<td>&nbsp;</td>
									</tr>
								</table>							</td>
						</tr>
					</table>
			  </td>
			</tr>
			<tr>
				<td height="61" valign="top"><uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
				</td>
			</tr>
			<tr>
				<td height="25">&nbsp;</td>
			  <td valign="top" background="images/b-b.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
			  <td>&nbsp;</td>
			</tr>
		</table>
        </div>
        <div id="SearchLayer" style="position:absolute; visibility:hidden">
        	<table border="0" cellpadding="0" cellspacing="0" id="TableSearch">
                        <!--DWLayoutTable-->
                        <tr>
                            <td width="13" height="25" valign="top" background="images/pbx-15.gif"><!--DWLayoutEmptyCell-->&nbsp;														</td>
                            <td width="273" align="right" valign="middle" background="images/pbx-18.gif"><div class="box">جستجوی خبر <img src="images/pbx-30.gif"></div></td>
                          <td width="13" valign="top" background="images/pbx-14.gif"><!--DWLayoutEmptyCell-->&nbsp;														</td>
                        </tr>
                        <tr>
                          <td height="160" valign="top" background="images/pbx-left.gif"><!--DWLayoutEmptyCell-->&nbsp;														</td>
                            <td align="center" valign="top" bgcolor="#ffffff"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                              <!--DWLayoutTable-->
                              <tr>
                                <td width="261" height="81" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                                  <!--DWLayoutTable-->
                                  <tr>
                                    <td width="169" height="5" valign="top"></td>
                                    <td width="92" valign="top"></td>
                                  </tr>
                                  <tr>
                                    <td height="29" valign="middle" bgcolor="#DBEDFF"><input name="SearchDate" id="SearchDate" type="text" maxlength="50" border="1" class="txtBox" onClick="displayDatePicker('SearchDate');" readonly></td>
                                    <td align="center" valign="middle" bgcolor="#DBEDFF"><div class="box">:انتخاب تاریخ</div></td>
                                  </tr>
                                  <tr>
                                    <td height="39" valign="middle" bgcolor="#DBEDFF"><div style="cursor:pointer;"><img src="images/searchBtn.gif" onClick="ShowItems('1', 'SearchByDate');" id="SearchByDate" name="SearchByDate" title="جستجو بر اساس تاریخ"/></div></td>
                                    <td valign="top" bgcolor="#DBEDFF"><!--DWLayoutEmptyCell-->&nbsp;</td>
                                  </tr>
                                </table>                                </td>
                              </tr>
                              <tr>
                                <td height="79" valign="top"><table border="0" cellpadding="0" cellspacing="0">
                                  <!--DWLayoutTable-->
                                  <tr>
                                    <td width="169" height="5" valign="top"></td>
                                    <td width="92" valign="top"></td>
                                  </tr>
                                  <tr>
                                    <td height="29" valign="middle" bgcolor="#B3D1ED"><input name="SearchText" id="SearchText" type="text" maxlength="200" border="1" class="txtBox" style="text-align:right;direction:rtl;" lang="fa"></td>
                                    <td align="center" valign="middle" bgcolor="#B3D1ED"><div class="box">:عبارت جستجو</div></td>
                                  </tr>
                                                                    <tr>
                                    <td height="29" valign="middle" bgcolor="#B3D1ED"><input name="ُStartDate" id="StartDate" type="text" maxlength="200" border="1" class="txtBox" onClick="displayDatePicker('StartDate');" readonly></td>
                                    <td align="center" valign="middle" bgcolor="#B3D1ED"><div class="box">:از تاریخ</div></td>
                                  </tr>
                                                                    <tr>
                                    <td height="29" valign="middle" bgcolor="#B3D1ED"><input name="EndDate" id="EndDate" type="text" maxlength="200" border="1" class="txtBox" onClick="displayDatePicker('EndDate');" readonly></td>
                                    <td align="center" valign="middle" bgcolor="#B3D1ED"><div class="box">:تا تاریخ</div></td>
                                  </tr>
                                  <tr>
                                    <td height="39" valign="middle" bgcolor="#B3D1ED"><div style="cursor:pointer;"><img src="images/searchBtn.gif" onClick="ShowItems('1', 'SearchByText');" id="SearchByText" name="SearchByText" title="جستجو بر اساس عبارت"/></div></td>
                                    <td valign="top" bgcolor="#B3D1ED"><!--DWLayoutEmptyCell-->&nbsp;</td>
                                  </tr>
                                </table></td>
                              </tr>
                            </table>
                            </td>
                      <td valign="top" background="images/pbx-right.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
                        </tr>
                        
                        
                        <tr>
                            <td height="27" valign="top" background="images/pbx-17.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
                            <td rowspan="2" valign="middle" align="left" background="images/pbx-19.gif"><input id="cancel" class="oBtn" style="HEIGHT: 23px" type="button" value="انصراف و بازگشت" onClick="ShowSearchLayer(false);"></td>
                            <td valign="top" background="images/pbx-16.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
                        </tr>
                        <tr>
                            <td height="1"></td>
                            <td></td>
                        </tr>
                    </table>
    </div>
<script type="text/javascript">
//SET_DHTML(CURSOR_MOVE, RESIZABLE, NO_ALT, SCROLL, "reltab");
centerView('SearchLayer');
<%
if(this.Request.QueryString["mode"] != null && this.Request.QueryString["mode"] == "search")
	this.Response.Write("\r\nShowSearchLayer(true);");
%>
</script>
</body>
</html>