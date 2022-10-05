<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ip.aspx.cs" Inherits="services.ip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="save" style="Z-INDEX: 101; LEFT: 320px; POSITION: absolute; TOP: 277px" Width="50px"/>
        <asp:TextBox ID="_saPassword" runat="server" style="Z-INDEX: 101; LEFT: 319px; POSITION: absolute; TOP: 214px"></asp:TextBox>
        <asp:Label ID="LabelSaPassword" runat="server" Text="New IP :" style="Z-INDEX: 101; LEFT: 212px; POSITION: absolute; TOP: 216px"></asp:Label>
        <asp:Label ID="message" runat="server" style="Z-INDEX: 101; LEFT: 258px; POSITION: absolute; TOP: 143px" Visible="False" BackColor="Yellow" ForeColor="Red"></asp:Label>
        <asp:TextBox ID="verify" runat="server" Style="z-index: 106; left: 321px; position: absolute;
            top: 242px" TextMode="Password"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Style="z-index: 107; left: 210px; position: absolute;
            top: 246px">Verify Code</asp:Label>
    </div>
    </form>
</body>
</html>