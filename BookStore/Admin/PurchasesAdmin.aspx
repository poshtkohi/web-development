<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchasesAdmin.aspx.cs" Inherits="bookstore.admin.PurchasesAdmin" validateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="stylesheet" type="text/css" href="../styles/cp.css" />
		<script type="text/javascript" src="../js/AjaxCore.js"></script>
		<script type="text/javascript" src="../js/Functions.js"></script>
        <script type="text/javascript" src="../js/common.js"></script>
  </HEAD>

<BODY style="BACKGROUND-IMAGE: none; MARGIN: 0px; BACKGROUND-COLOR: white">
<center>
            <DIV class=box style="WIDTH: 550px">
            <DIV class=header>
            <DIV class=title>خرید های تایید نشده</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg_ShowUnverifiedPurchases" align="center"><img src="../images/loading.gif"/></div>
                    <div id="resultText_ShowUnverifiedPurchases"></div>
              </DIV>
            </DIV>
            </DIV>
            
            <DIV class=box style="WIDTH: 550px">
            <DIV class=header>
            <DIV class=title>خرید های تایید شده</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg_ShowVerifiedPurchases" align="center"><img src="../images/loading.gif"/></div>
                    <div id="resultText_ShowVerifiedPurchases"></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script type="text/javascript">
	ShowItems('1', 'ShowUnverifiedPurchases');
	ShowItems('1', 'ShowVerifiedPurchases');
</script>