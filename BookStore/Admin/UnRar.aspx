<%@ Page language="c#" Inherits="bookstore.UnRar" CodeFile="UnRar.aspx.cs" %>


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
            <p>&nbsp;
                From
                  <asp:TextBox ID="from" runat="server" 
                    style="Z-INDEX: 108; LEFT: 56px; POSITION: absolute; TOP: 14px; right: 840px;"></asp:TextBox>
            </p>
          <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>
                TO
                  <asp:TextBox ID="to" runat="server" 
                    
                    style="Z-INDEX: 108; LEFT: 44px; POSITION: absolute; TOP: 70px; right: 852px;"></asp:TextBox>
            </p>
            <p>&nbsp;
		      </p>
            <p>
			<asp:Button id="unrar" style="Z-INDEX: 108; LEFT: 15px; POSITION: absolute; TOP: 109px"
				runat="server" Text="unrar" onclick="unrar_Click"></asp:Button>
            </p>
            <p>&nbsp;
                </p>
            <p>&nbsp;
			    </p>
			<p>
			<asp:Button id="path" style="Z-INDEX: 108; LEFT: 17px; POSITION: absolute; TOP: 148px"
				runat="server" Text="path" onclick="path_Click"></asp:Button>
            </p>
        </form>
	</body>
</HTML>

