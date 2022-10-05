<%@ Page language="c#" Inherits="services.FindPassword" CodeFile="FindPassword.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FindPassword</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="subdomain" style="Z-INDEX: 101; LEFT: 384px; POSITION: absolute; TOP: 184px"
				runat="server"></asp:TextBox>
			<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 312px; POSITION: absolute; TOP: 184px" runat="server">subdomain</asp:Label>
			<asp:TextBox id="password" style="Z-INDEX: 103; LEFT: 384px; POSITION: absolute; TOP: 248px"
				runat="server"></asp:TextBox>
			<asp:Label id="Label2" style="Z-INDEX: 104; LEFT: 320px; POSITION: absolute; TOP: 256px" runat="server">Password</asp:Label>
			<asp:Button id="find" style="Z-INDEX: 105; LEFT: 392px; POSITION: absolute; TOP: 344px" runat="server"
				Text="Find" onclick="find_Click"></asp:Button>
			<asp:TextBox id="verify" style="Z-INDEX: 106; LEFT: 384px; POSITION: absolute; TOP: 288px" runat="server"
				TextMode="Password"></asp:TextBox>
			<asp:Label id="Label3" style="Z-INDEX: 107; LEFT: 288px; POSITION: absolute; TOP: 288px" runat="server">Verify Code</asp:Label>
            <asp:TextBox ID="username" runat="server" Style="z-index: 103; left: 380px; position: absolute;
                top: 215px"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Style="z-index: 104; left: 316px; position: absolute;
                top: 223px">Username</asp:Label>
		</form>
	</body>
</HTML>
