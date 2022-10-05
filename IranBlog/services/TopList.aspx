<title>Top List</title><%@import namespace="AlirezaPoshtkoohiLibrary"%>
<script language="c#" runat="server">
private void Page_Load(object sender, System.EventArgs e)
{
	db.TopVisits(this);
}
</script>