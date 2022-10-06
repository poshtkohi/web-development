<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cp.aspx.cs" Inherits="bookstore.admin.cp" %>
<title>::. سیستم جامع مدیریت وب سایت دریا لایب .::</title>
<meta http-equiv="Content-Language" PostContent="fa">
<meta http-equiv="Content-Type" PostContent="text/html; charset=utf-8">
<meta PostContent="alireza.poshtkohi@gmail.com" name="ASP.NET CMS Programmer and Developer Email">
<meta PostContent="Alireza Poshtkohi" name="ASP.NET CMS Programmer and Developer">
<LINK href="v3fa.css" type="text/css" rel="stylesheet">
	<style>BODY { SCROLLBAR-FACE-COLOR: #9bb700; SCROLLBAR-HIGHLIGHT-COLOR: #66cc99; SCROLLBAR-SHADOW-COLOR: #ffdfbf; SCROLLBAR-3DLIGHT-COLOR: #9bb700; SCROLLBAR-ARROW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #49692c; SCROLLBAR-DARKSHADOW-COLOR: #9bb700 }
	</style>
	<body>
		<form id="a" runat="server">
			<table style="BORDER-RIGHT: #e8e8e8 1px dotted; BORDER-TOP: #e8e8e8 1px dotted; BORDER-LEFT: #e8e8e8 1px dotted; BORDER-BOTTOM: #e8e8e8 1px dotted"
				height="582" cellSpacing="0" cellPadding="0" width="846" align="center" border="0">
				<tr>
					<td vAlign="top" colSpan="2" height="55">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="mainb" vAlign="top" colSpan="5" height="28">
									<div align="right"><img src="images/admin_02.jpg" width="128" height="54"><img src="images/admin_03.gif" width="82" height="54"></div>
							  </td>
							</tr>
						</table>
					</td>
					<td width="6"></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2" height="77">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="v3butlink_navhome" dir="rtl" vAlign="middle" align="right" width="848" height="38"><IMG height="9" src="images/loc_icon.gif" width="9">
									<A style="COLOR: #000066" href="cp.aspx" target="frameLeft">
								صفحه اصلی مرکز مدیریت وب سایت دریا لایب</A></td>
							</tr>
							<tr>
								<td vAlign="top" height="39">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="848" height="39">
                                                <asp:LinkButton id="logout" runat="server" onclick="logout_Click" 
                                                    >خروج از سیستم</asp:LinkButton></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td></td>
				</tr>
				<tr>
					<td vAlign="top" width="587" height="17" rowspan="2"><iframe id="frameLeft" border=0 name="frameLeft" marginwidth=0 marginheight=0 src="UsersAdmin.aspx" frameborder=0 scrolling="no" height=500px width="100%" align=""></iframe></td>
					<td vAlign="top" width="251" rowSpan="2">
						<table id="tasks" cellSpacing="0" borderColorDark="#f0f0f0" cellPadding="0" width="100%"
							borderColorLight="#ffffff" border="1">
							<tr>
							  <td dir="rtl" vAlign="top" align="right" width="250" height="425">
									<h2 class="v3h4" style="FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: #000066"><IMG height="16" src="images/folder.gif" width="18" align="absMiddle">
										منوی مدیریت:</h2>
											<div class="mainb v3color_blue" style="PADDING-RIGHT: 20px; PADDING-LEFT: 0px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"><IMG height="8" src="images/79-(4)fa.gif" width="10">مدیریت 
												بخش اخبار
											</div>
											<div class="v3butsubmenumain">
											  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
											  <a href="NewsAdmin.aspx" target="frameLeft">مدیریت اخبار</a></DIV>
											</div>
										<div class="mainb v3color_blue" style="PADDING-RIGHT: 20px; PADDING-LEFT: 0px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"><IMG height="8" src="images/79-(4)fa.gif" width="10">مدیریت
											<span lang="fa">آرشیو کتاب ها</span></div>										<div class="v3butsubmenumain">
										  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
										  <a href="BooksAdmin.aspx" target="frameLeft">مدیریت کتاب ها</a></DIV>
										</div>
                                    <div class="mainb v3color_blue" style="PADDING-RIGHT: 20px; PADDING-LEFT: 0px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"><IMG height="8" src="images/79-(4)fa.gif" width="10">مدیریت بخش مشتریان</div>
									<div class="v3butsubmenumain">
									  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
									  <a href="CustomersAdmin.aspx" target="frameLeft">مدیریت مشتریان</a></DIV>
									</div>
                                    <div class="mainb v3color_blue" style="PADDING-RIGHT: 20px; PADDING-LEFT: 0px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"><IMG height="8" src="images/79-(4)fa.gif" width="10">مدیریت بخش صفحات سایت</div>
									<div class="v3butsubmenumain">
									  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
									  <a href="PagesAdmin.aspx" target="frameLeft">مدیریت صفحات</a></DIV>
									</div>
									<div class="mainb v3color_blue" style="PADDING-RIGHT: 20px; PADDING-LEFT: 0px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"><IMG height="8" src="images/79-(4)fa.gif" width="10">مدیریت 
										بخش کاربران</div>
									<div class="v3butsubmenumain">
									  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
									  <a href="PurchasesAdmin.aspx" target="frameLeft">مدیریت خرید کاربران</a></DIV>
									</div>
                               	<div class="v3butsubmenumain">
									  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
									  <a href="UsersAdmin.aspx" target="frameLeft">مدیریت کاربران ثبت نامی</a></DIV>
									  </div>
								<div class="v3butsubmenumain">
									  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
									  <a href="TransactionsAdmin.aspx" target="frameLeft">تراکنش های کامل نشده</a></DIV>
									  </div>
                                <div class="v3butsubmenumain">
									  <DIV class="v3butsubmenu_off" onMouseOver="this.className='v3butsubmenu_on'; " onMouseOut="this.className='v3butsubmenu_off';">
									  <a href="PassAdmin.aspx" target="frameLeft">تغییر کلمه عبور کنترل پنل</a></DIV>
									  </div>
							  </td>
							</tr>
						</table>
					</td>
					<td></td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2" height="68">
						<TABLE class="main" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top" width="790" bgColor="#f0f0f0" height="52">
									<div dir="rtl" style="PADDING-RIGHT: 0px; PADDING-LEFT: 1px; PADDING-BOTTOM: 2px; PADDING-TOP: 10px"
										align="center">کلیه حقوق این وب سایت متعلق به وب<span lang="en-us"></span>
										سایت دریا لایب می باشد<span lang="en-us">.</span>
										<p dir="ltr"><span lang="en-us">All rights reserved to ِBookStore <font face="Times New Roman">
													© 2009.</font></span></p>
									</div>
								</td>
							</tr>
							<tr>
								<td vAlign="top" height="4"><IMG height="4" src="images/bg-top.gif" width="100%"></td>
							</tr>
							<tr>
								<td height="6"></td>
							</tr>
						</TABLE>
					</td>
					<td></td>
				</tr>
			</table>
		</form>
	</body>
