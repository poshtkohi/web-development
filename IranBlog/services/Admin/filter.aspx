<%@ Page Language="C#" AutoEventWireup="true" CodeFile="filter.aspx.cs" Inherits="Admin_filter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form2" runat="server">
			<asp:button id="_filter" style="Z-INDEX: 100; LEFT: 320px; POSITION: absolute; TOP: 277px" runat="server"
				Width="51" Text="Filter" Height="24" OnClick="_filter_Click"></asp:button>
			<asp:button id="_unfilter" style="Z-INDEX: 107; LEFT: 320px; POSITION: absolute; TOP: 312px"
				runat="server" Width="64px" Text="Unfilter" Height="24" OnClick="_unfilter_Click"></asp:button><asp:textbox id="_subdomain" style="Z-INDEX: 102; LEFT: 319px; POSITION: absolute; TOP: 214px"
				runat="server"></asp:textbox><asp:label id="LabelSubdomain" style="Z-INDEX: 103; LEFT: 212px; POSITION: absolute; TOP: 216px"
				runat="server" Text="New Password :">Subdomain</asp:label><asp:label id="message" style="Z-INDEX: 104; LEFT: 258px; POSITION: absolute; TOP: 143px" runat="server"
				ForeColor="Red" BackColor="Yellow" Visible="False"></asp:label><asp:textbox id="verify" style="Z-INDEX: 105; LEFT: 321px; POSITION: absolute; TOP: 242px" runat="server"
				TextMode="Password"></asp:textbox><asp:label id="Label3" style="Z-INDEX: 106; LEFT: 225px; POSITION: absolute; TOP: 242px" runat="server">Verify Code</asp:label></form>
	</body>
</HTML>
