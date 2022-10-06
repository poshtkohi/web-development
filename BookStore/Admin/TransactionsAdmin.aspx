<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionsAdmin.aspx.cs" Inherits="bookstore.admin.TransactionsAdmin" validateRequest="false"%>
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
            <DIV class=title>کل اعتبار خریداری شده توسط کاربران</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg_ShowTotalAmount" align="center"><img src="../images/loading.gif"/></div>
                    <div id="resultText_ShowTotalAmount"></div>
              </DIV>
            </DIV>
            </DIV>
            
            <DIV class=box style="WIDTH: 550px">
            <DIV class=header>
            <DIV class=title>تراکنش ها</DIV></DIV>
            <DIV class=content style="TEXT-ALIGN: center">
              <DIV style="PADDING-BOTTOM: 5px; WIDTH: 99%" align=left>
                    <div id="loaderImg_ShowTransactions" align="center"><img src="../images/loading.gif"/></div>
                    <div id="resultText_ShowTransactions"></div>
              </DIV>
            </DIV>
            </DIV>
</center>
</BODY>
</HTML>
<script type="text/javascript">
	ShowItems('1', 'ShowTotalAmount');
    ShowItems('1', 'ShowTransactions');
</script>