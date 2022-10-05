<%@ Page language="c#" Inherits="services.FindIP" CodeFile="FindIP.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FindIP</title>
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
			<asp:Label id="Label2" style="Z-INDEX: 104; LEFT: 296px; POSITION: absolute; TOP: 334px" runat="server">RegisterDate</asp:Label>
			<asp:Button id="find" style="Z-INDEX: 105; LEFT: 385px; POSITION: absolute; TOP: 480px" runat="server"
				Text="Find" onclick="find_Click"></asp:Button>
			<asp:TextBox id="verify" style="Z-INDEX: 106; LEFT: 388px; POSITION: absolute; TOP: 380px" runat="server"
				TextMode="Password"></asp:TextBox>
            <asp:TextBox ID="LastLoginIP" runat="server" Style="z-index: 103; left: 380px; position: absolute;
                top: 215px"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Style="z-index: 104; left: 299px; position: absolute;
                top: 216px; height: 16px;">LastLoginIP</asp:Label>
			<p>
			<asp:TextBox id="LastLoginDate" style="Z-INDEX: 103; LEFT: 387px; POSITION: absolute; TOP: 254px; right: 536px;"
				runat="server"></asp:TextBox>
			</p>
			<asp:Label id="Label3" style="Z-INDEX: 107; LEFT: 294px; POSITION: absolute; TOP: 384px; right: 682px;" runat="server">Verify Code</asp:Label>
			<asp:TextBox id="RegisterDate" style="Z-INDEX: 103; LEFT: 383px; POSITION: absolute; TOP: 334px"
				runat="server"></asp:TextBox>
			<asp:TextBox id="RegisterIP" style="Z-INDEX: 103; LEFT: 384px; POSITION: absolute; TOP: 293px"
				runat="server"></asp:TextBox>
			<asp:Label id="Label6" style="Z-INDEX: 104; LEFT: 310px; POSITION: absolute; TOP: 298px" runat="server">RegisterIP</asp:Label>
			<p>&nbsp;</p>
			<asp:Label id="Label5" style="Z-INDEX: 104; LEFT: 294px; POSITION: absolute; TOP: 256px; height: 19px;" runat="server">LastLoginDate</asp:Label>
		</form>
	</body>
</HTML>
