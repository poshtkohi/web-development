<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendNewsletter.aspx.cs" Inherits="services.blogbuilderv1.SendNewsletter" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<link rel="stylesheet" type="text/css" href="../images/style.css" />
					<style> 
					.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none } 
					.style12 { FONT-SIZE: x-small; FONT-FAMILY: Tahoma } 
					.style13 { FONT-SIZE: 7pt; COLOR: #ff0000 } 
					</style>
					<script type="text/javascript" src="htmlarea.js"></script>
					<script type="text/javascript" src="htmlarea-lang-en.js"></script>
					<script type="text/javascript" src="dialog.js"></script>
					<script language="javascript" src="/js/farsi.js" type="text/javascript"></script>
                    <script language="javascript" type="text/javascript" src="js/common.js"></script>
					<script language="javascript" type="text/javascript">
			/* All rights reserved to Mr. Alireza Poshtkohi (C) 2002-2007. alireza.poshtkohi@gmail.com */
			//---------------------------------------------------------------------------------------------------------------------------
			var editor = null;
     		var editor2Defined = false;
            function initEditor()
			{
                  editor = new HTMLArea("content");
                  editor.generate();
		    }
			//---------------------------------------------------------------------------------------------------------------------------
			function ContentCheck(form)
			{
			   if(form.content.value != "")
			   {
			      if(form.content.value.length > 40960)
				  {
				     alert(".حجم متن نمی تواند از 40 کیلو بایت بیشتر باشد");
					 return false;
				  }
				  else return true;
			   }
			}
			//---------------------------------------------------------------------------------------------------------------------------
			</script>
</HEAD>
	<body onload="initEditor()">
	<form id="basicposting" name="basicposting" method="post" runat="server" onClick="return ContentCheck(this);">
<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                <td align="center" valign="middle">							    <strong>
									  <asp:Label ID="message" runat="server" BackColor="Gold" Font-Size="X-Small" Visible="false"
											ForeColor="Red" Width="367px" Font-Name="Tahoma" Font-Names="Tahoma"></asp:Label>
								  </strong>							  </td>
						<tr>
						  <td width="359" height="3"></td>
						  <td width="143" rowspan="2" align="center" valign="top" bgcolor="#ffffff"><span class="style12">
                          <asp:TextBox CssClass="v3txtboxReg155form" ID="subject" MaxLength="200" runat="server" 
											onKeyDown="FBlogKeyDown()" onKeyPress="FKeyPress()" style="TEXT-ALIGN: right" Width="150px"
											Font-Names="Tahoma" /></span></td>
						  <td width="52" rowspan="2" align="center" valign="top" bgcolor="#ffffff">:
					      عنوان </td>
						  <td width="1"></td>
						</tr>
						<tr>
						  <td rowspan="2" valign="top" bgcolor="#ffffff"><DIV id="Layer1" style="SCROLLBAR-FACE-COLOR: #271502; FONT-SIZE: medium; BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-COLOR: #000000; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; OVERFLOW: auto; WIDTH: 359px; SCROLLBAR-SHADOW-COLOR: #ffffff; DIRECTION: rtl; SCROLLBAR-3DLIGHT-COLOR: #271502; BORDER-TOP-COLOR: #000000; SCROLLBAR-ARROW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #999999; FONT-FAMILY: Tahoma; SCROLLBAR-DARKSHADOW-COLOR: #271502; HEIGHT: 30px; BACKGROUND-COLOR: #cccccc; BORDER-RIGHT-COLOR: #000000"
										align="justify">
								  <DIV align="center">
									  <TABLE id="Main" height="30" cellSpacing="0" cellPadding="0" width="326" bgColor="#999999"
												border="0">
										  <TR>
											  <TD width="326" height="30" align="right" valign="top">
												  <%
																	                               for(int i = 1 ; i <= 40; i ++)
																	                                   this.Response.Write("<img src=\"http://www.iranblog.com/smiley/" + i + ".gif\" width=\"18\" height=\"18\">");
																								 %>										      </TD>
										  </TR>
								      </TABLE>
								  </DIV>
					      </DIV></td>
							<td height="22"></td>
				  </tr>
						<tr>
						  <td height="12" colspan="2" align="center" valign="middle" dir="rtl">متن
						    ارسالی خبرنامه شما :</td>
					  <td></td>
				  </tr>
						<tr>
						  <td height="555" colspan="3" valign="top"><asp:TextBox ID="content" runat="server" TextMode="MultiLine" MaxLength="80192" Font-Names="Tahoma"
										CssClass="v3txtboxReg155form" style="COLOR:#ffffff; border-width:1px; border-color:#CCCCCC;width:99%;height:555px" /></td>
						  <td></td>
						</tr>
						<tr>
						  <td height="22">&nbsp;</td>
						  <td colspan="2">&nbsp;</td>
						  <td style="height: 22px"></td>
				  </tr>
						<tr>
						  <td height="22" colspan="3" valign="top" style="height: 22px"><asp:button id="submit" runat="server" class="v3ibbtn" Text="ارسال خبر نامه جدید" ToolTip="ارسال خبر نامه جدید" Width="564px" OnClick="submit_Click"></asp:button></td>
						  <td style="height: 22px"></td>
				  </tr>
						<tr>
						  <td height="38">&nbsp;</td>
						  <td colspan="2">&nbsp;</td>
						  <td style="height: 22px"></td>
				  </tr>
						
		      </table>
	</form>
	</body>
</HTML>