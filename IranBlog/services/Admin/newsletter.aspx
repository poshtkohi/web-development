<%@ Page language="c#" Inherits="services.newsletter" validateRequest="false" CodeFile="newsletter.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>newsletter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:button id="send" style="Z-INDEX: 101; LEFT: 272px; POSITION: absolute; TOP: 408px" runat="server"
				Text="send" onclick="send_Click"></asp:button><asp:textbox id="verify" style="Z-INDEX: 108; LEFT: 272px; POSITION: absolute; TOP: 72px" runat="server"
				TextMode="Password"></asp:textbox><asp:label id="Label3" style="Z-INDEX: 107; LEFT: 184px; POSITION: absolute; TOP: 72px" runat="server">Verify Code</asp:label><asp:textbox id="message" style="Z-INDEX: 102; LEFT: 264px; POSITION: absolute; TOP: 120px" runat="server"
				TextMode="MultiLine" Height="248px" Width="384px"></asp:textbox></form>
	</body>
</HTML>
