<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseDetails.aspx.cs" Inherits="bookstore.admin.PurchaseDetails" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="../styles/cp.css" />
		<script type="text/javascript" src="../js/AjaxCore.js"></script>
		<script type="text/javascript" src="../js/Functions.js"></script>
  <title>جزئیات خرید</title></HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white" onUnload="_editorIsDefined=false;">
<center>
            <DIV class=box style="WIDTH: 550px">
            <DIV class=header>
            <DIV class=title>جزئیات خرید</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg_ShowPurchaseDetails" align="center"><img src="../images/loading.gif"/></div>
                    <div id="resultText_ShowPurchaseDetails"></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script type="text/javascript">
	ShowItems('<%=this.Request.QueryString["PurchaseID"]%>', 'ShowPurchaseDetails');
</script>