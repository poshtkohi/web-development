<%@ Page Language="C#" CodeFile="ShoppingCart.aspx.cs" Inherits="bookstore.cp.ShoppingCart"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>سبد خرید</title>
		<script type="text/javascript" src="../js/AjaxCore.js"></script>
		<script type="text/javascript" src="../js/Functions.js"></script>
        <link href="../style/calass.css" rel="stylesheet" type="text/css" />
        <link href="../styles/cp.css" rel="stylesheet" type="text/css" />
        <link href="../styles/addons.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="skl">
	<div id="top"><img src="../images/top.jpg" /></div>
    <div id="topmenu"><asp:PlaceHolder ID="LoginControl" runat="server"></asp:PlaceHolder></div>
    <div id="topspace"></div>
    <div id="center-s1">
          	<div id="PurchaseSection" style="display:none">
                <div align="center"><input class="oBtn" title="انجام عملیات خرید" style="WIDTH: 209px;" type="submit" value="انجام عملیات خرید" onClick="DoPost('DoPurchase');" name="PurchaseButton" id="PurchaseButton"/></div>
                <div id="loaderImg_DoPurchase" align="center" style="display:none"><img src="../images/loading.gif"/></div>
                <div id="resultText_DoPurchase" style="display:none" class="message"></div>
			</div>
        <p>&nbsp;</p>
     <span class="gen-title">سبد خرید</span><br />
       <div id="news">
            <div id="loaderImg_ShoppingCart" align="center" style="display:none"><img src="../images/loading.gif"/></div>
            <div id="loaderImg_ShowShoppingCart" align="center"><img src="../images/loading.gif"/></div>
            <div id="resultText_ShowShoppingCart"></div>
       </div>
          
       <p></p><br><br><br><br><br><br>
       <hr/>
      
       <!----> 
       <div id="width-spacer">&nbsp;</div>  
       </div>
      
  <div id="right">
        <asp:PlaceHolder ID="UserMenuControl" runat="server"></asp:PlaceHolder>  
  </div>
     
</div>
<div id="btm"></div> 
</body>
</html>
<script type="text/javascript">
	ShowItems('1', 'ShowShoppingCart');
</script>