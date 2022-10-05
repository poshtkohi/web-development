<%@ Page Language="c#" Inherits="services.blogbuilderv1.delete" CodeFile="delete.aspx.cs" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
	    <link rel="stylesheet" type="text/css" href="../images/style.css" />
		<meta http-equiv="Content-Language" content="fa">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
					<style>
.posted { MARGIN-BOTTOM: 2px; FONT: x-small Tahoma; TEXT-ALIGN: right; TEXT-DECORATION: none }
</style>
					<script language="javascript" type="text/javascript">
				/* All rights reserved to Mr. Alireza Poshtkoohi (C) 2005-2007. alireza.poshtkohi@gmail.com */
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
				function ValidateََAcknowledgePhrase(field)
				{
				   if(!BlankField(field, ".فیلد کلمه تایید خالی است"))
				          return false;
				   if(field.value.length != 6)
				   {
				       alert(".تعداد حروف فیلد کلمه تایید کم است");
					   return false;
				   }		  
				   else return true; 
				}
				//-------------------------------------------
				function ValidateForm(form)
				{
				    if(!ValidateََAcknowledgePhrase(form.acknowledge))
					      return false;
					else return true;
				}
				//---------------------------------------------------------------------------------------------------------------------------
					</script>
				<script language="javascript" type="text/javascript" src="js/common.js"></script>
	</HEAD>
	<body><center>
		<form id="d" name="d" onsubmit="return ValidateForm(this);" method="post" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="146" height="198">&nbsp;</td>
								<td width="284" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="111" height="39">&nbsp;</td>
											<td width="153">&nbsp;</td>
											<td width="116">&nbsp;</td>
										</tr>
										<tr>
											<td height="109">&nbsp;</td>
											<td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td width="159" height="23" valign="top" align="right"><asp:label id="AcknowledgeError" Width="93px" runat="server" BackColor="Gold" Font-Size="Small"
																Visible="False" ForeColor="Red"></asp:label></td>
													</tr>
													<!--DWLayoutTable-->
													<tr>
														<td height="40" valign="top" width="159">
															<asp:Image CssClass="txtBoxStyleLogin" Height="40px" id="RandomImage" ImageUrl="/services/request.aspx?i=randimg"
																runat="server" Width="148px"></asp:Image></td>
													</tr>
													<tr>
														<td height="23" valign="top" width="159">
															<asp:TextBox id="acknowledge" runat="server" MaxLength="6" CssClass="v3txtboxReg155form" ToolTip=".کد درون عکس را در این قسمت وارد کنید"
																Width="150px"></asp:TextBox></td>
													</tr>
													<tr>
														<td height="23" width="159">
															<asp:Button ID="submit" runat="server" Text="حذف عضویت" class="v3ibbtn" Width="150px" Font-Names="Tahoma" onclick="submit_Click"></asp:Button></td>
													</tr>
												</table>											</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td height="50">&nbsp;</td>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
									</table>								</td>
								<td width="124">&nbsp;</td>
							</tr>
						</table>
	</form>
    </center>
	</body>
</HTML>
