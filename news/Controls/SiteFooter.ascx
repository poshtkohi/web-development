<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SiteFooter.ascx.cs" Inherits="news.Controls.SiteFooter" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script type="text/javascript" src="/js/wz_tooltip.js"></script>
<script type="text/javascript" src="/js/tip_balloon.js"></script>
<table width="100%" border="0" cellpadding="0" cellspacing="0" background="/images/obs.png">
	<!--DWLayoutTable-->
	<tr>
		<td width="497" height="61">&nbsp;</td>
		<NewsGroupSection runat="server" id="NewsGroupSection">
			<td width="55" valign="middle" align="center"><div id="menu"><a href="/NewsGroups.aspx" onmouseover="TagToTip('T2tDirectNewsGroups', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
						onmouseout="UnTip()"><img src="/images/docs_320.png" alt="Define News Groups" width="25" height="30"></a></div>
			</td>
			<td width="1" valign="middle" background="/images/obs_p.png"></td>
		</NewsGroupSection>
		<UsersSection runat="server" id="UsersSection">
			<td width="55" valign="middle" align="center"><div id="menu"><a href="/Users.aspx" onmouseover="TagToTip('T2tDirectUsers', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
						onmouseout="UnTip()"><img src="/images/group_32.png" alt="Users" width="30" height="30"></a></div>
			</td>
			<td width="1" valign="middle" background="/images/obs_p.png"></td>
		</UsersSection>
		<td width="55" valign="middle" align="center"><div id="menu"><a href="/Logout.aspx" onmouseover="TagToTip('T2tDirectLogout', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
					onmouseout="UnTip()"><img src="/images/exit2.png" width="28" height="28"></a></div>
		</td>
		<td width="1" valign="middle" background="/images/obs_p.png"></td>
		<td width="57" valign="middle" align="center">
        <DirectSearch runat="server" id="DirectSearch">
        	<div id="menu"><a href="/NewsAdmin.aspx#search" onclick="ShowSearchLayer(true);" onmouseover="TagToTip('T2tDirectُSearch', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
					onmouseout="UnTip()"><img src="/images/srch_32.png" alt="Search"></a></div>
        </DirectSearch>
        <IndirectSearch runat="server" id="IndirectSearch">
        	<div id="menu"><a href="/NewsAdmin.aspx?mode=search" onmouseover="TagToTip('T2tDirectُSearch', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
					onmouseout="UnTip()"><img src="/images/srch_32.png" alt="Search"></a></div>
        </IndirectSearch>
		</td>
		<td width="1" valign="middle" background="/images/obs_p.png"></td>
		<td width="57" valign="middle" align="center"><div id="menu"><a href="/NewsGroupsShow.aspx" onmouseover="TagToTip('T2tDirectNewsGroupsShow', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
					onmouseout="UnTip()"><img src="/images/docs_32.png" alt="News Group" width="28" height="30"></a></div>
		</td>
		<td width="1" valign="middle" background="/images/obs_p.png"></td>
		<td width="72" align="center" valign="middle">
         <DirectNewsAdmin runat="server" id="DirectNewsAdmin">
        	<div id="menu"><a href="/NewsAdmin.aspx#NewsAdmin" onclick="ShowItems('1', 'ShowNewsAdmin');" onmouseover="TagToTip('T2tDirectNewsAdmin', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
                        onmouseout="UnTip()"><img src="/images/HOME_3.PNG" alt="Home" width="32" height="31"></a></div>
        </DirectNewsAdmin>
        <IndirectNewsAdmin runat="server" id="IndirectNewsAdmin">
            <div id="menu"><a href="/NewsAdmin.aspx" onmouseover="TagToTip('T2tDirectNewsAdmin', BALLOON, true, ABOVE, true, OFFSETX, -17, FADEIN, 600, FADEOUT, 600, PADDING, 8)"
                        onmouseout="UnTip()"><img src="/images/HOME_3.PNG" alt="Home" width="32" height="31"></a></div>
        </IndirectNewsAdmin>
		</td>
	</tr>
</table>
<div id="T2tDirectNewsAdmin" style="display:none">
	<div class="tooltipDiv">
		در این قسمت می توانید به مدیریت اخبار همانند تعریف خبر جدید بپردازید.</div>
</div>
<div id="T2tDirectNewsGroupsShow" style="display:none">
	<div class="tooltipDiv">
		در این بخش می توانید به مشاهده گروه های خبری موجود بپردازید.</div>
</div>
<div id="T2tDirectُSearch" style="display:none">
	<div class="tooltipDiv">
		در این بخش می توانید به جستجوی خبر مورد نظرتان بپردازید.</div>
</div>
<div id="T2tDirectLogout" style="display:none">
	<div class="tooltipDiv">
		می توانید از این گزینه برای خروج از حساب کاربریتان استفاده کنید.</div>
</div>
<div id="T2tDirectUsers" style="display:none">
	<div class="tooltipDiv">
		از این گزینه میتوانید برای مدیریت کاربران سیستم و سطح دسترسی آنها استفاده کنید.</div>
</div>
<div id="T2tDirectNewsGroups" style="display:none">
	<div class="tooltipDiv">
		از این بخش می توانید برای مدیریت بخش های خبری سیستم استفاده کنید.</div>
</div>
