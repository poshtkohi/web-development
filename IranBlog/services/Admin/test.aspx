<%@ Page language="c#" Inherits="services.Migrated_test" CodeFile="test.aspx.cs" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="AlirezaPoshtkoohiLibrary" %>
<HTML>
	<HEAD>
		<title>test</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:button id="Button1" style="Z-INDEX: 100; LEFT: 352px; POSITION: absolute; TOP: 280px" runat="server"
				Text="delete" onclick="Button1_Click"></asp:button>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 256px; POSITION: absolute; TOP: 32px" runat="server"
				TextMode="MultiLine" Height="208px" Width="296px"></asp:TextBox>
			<asp:Button id="Button2" style="Z-INDEX: 102; LEFT: 440px; POSITION: absolute; TOP: 280px" runat="server"
				Text="create" onclick="Button2_Click"></asp:Button>
			<asp:Button id="Button3" style="Z-INDEX: 104; LEFT: 392px; POSITION: absolute; TOP: 392px" runat="server"
				Text="Button" onclick="Button3_Click"></asp:Button>
			<asp:Button id="backup" style="Z-INDEX: 105; LEFT: 136px; POSITION: absolute; TOP: 392px" runat="server"
				Text="backup" onclick="backup_Click"></asp:Button>
			<asp:Button id="restore" style="Z-INDEX: 106; LEFT: 136px; POSITION: absolute; TOP: 456px" runat="server"
				Text="restore" onclick="Button4_Click"></asp:Button>
			<asp:Button id="execute_genaral" style="Z-INDEX: 108; LEFT: 584px; POSITION: absolute; TOP: 216px"
				runat="server" Text="execute query in general db" onclick="execute_Click"></asp:Button><asp:Button id="Button4" style="Z-INDEX: 108; LEFT: 587px; POSITION: absolute; TOP: 331px"
				runat="server" Text="execute query in newsletter db" onclick="execute_newsletter_Click"></asp:Button><asp:Button id="execute_comments" style="Z-INDEX: 108; LEFT: 586px; POSITION: absolute; TOP: 377px"
				runat="server" Text="execute query in comments db" onclick="execute_comments_Click"></asp:Button>
            <p>
                &nbsp;</p>
            <p>
			<asp:Button id="alter" style="Z-INDEX: 103; LEFT: 112px; POSITION: absolute; TOP: 280px" runat="server"
				Text="Alter Weblogs Table" onclick="Count_Click"></asp:Button>
			<asp:Button id="execute_posts0" style="Z-INDEX: 109; LEFT: 584px; POSITION: absolute; TOP: 273px"
				runat="server" Text="execute query in posts db" onclick="execute_posts_Click"></asp:Button>
			</p>
			<asp:Button id="execute_chatbox" style="Z-INDEX: 109; LEFT: 586px; POSITION: absolute; TOP: 423px"
				runat="server" Text="execute query in chatbox db" onclick="execute_chatbox_Click"></asp:Button>
			<asp:Button id="execute_postimport" style="Z-INDEX: 109; LEFT: 582px; POSITION: absolute; TOP: 511px"
				runat="server" Text="execute query in pages db" 
                onclick="execute_pages_Click"></asp:Button>
            <p>
			<asp:Button id="execute_postimport0" style="Z-INDEX: 109; LEFT: 583px; POSITION: absolute; TOP: 464px"
				runat="server" Text="execute query in postimport db" 
                onclick="execute_postimport_Click"></asp:Button>
            </p>
        </form>
	</body>
</HTML>

