<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="bookstore._Default"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title>صفحه اصلی</title>
		<script type="text/javascript" src="js/AjaxCore.js"></script>
		<script type="text/javascript" src="js/Functions.js"></script>
        <link href="style/calass.css" rel="stylesheet" type="text/css" />
        <link href="styles/cp.css" rel="stylesheet" type="text/css" />
        <link href="styles/addons.css" rel="stylesheet" type="text/css" />
        <meta PostContent="alireza.poshtkohi@gmail.com" name="ASP.NET Programmer and Developer Email">
		<meta PostContent="Alireza Poshtkohi" name="ASP.NET Programmer and Developer">

</head>
<body>
<div id="MainDiv">
<div id="skl">
  <div id="top"><img src="images/top.jpg" /></div>
  <div id="topmenu">
    <div id="lang"><a href="#">En</a> - <a href="#">Fa</a></div>
    <div id="search">
      <form action="/search.aspx" method="post" id="searchform" name="searchform">
        <div style="width:20px; float:left;" onClick="javascript:document.getElementById('searchform').submit()">&nbsp;</div>
        <input name="searchbox" id="searchbox" type="text" value="جستجو" onClick="SearchBoxCleaner();" style="text-align:left"/>
      </form>
    </div>
    &nbsp;
    <asp:PlaceHolder ID="LoginPanelControl" runat="server"></asp:PlaceHolder>
    &nbsp; </div>
  <div id="topspace"></div>
  <div id="loaderImg_ShowMainPageSitePageDetailes" align="center" style="display:none"><img src="images/loading.gif"/></div>
  <div id="center-s1">
    <div id="loaderImg_ShowTopNews" align="center"><img src="images/loading.gif"/></div>
    <div id="resultText_ShowTopNews"></div>
    <hr/>

    <span class="gen-title">اخبار</span> 
    <div id="news">
      <div id="loaderImg" align="center"><img src="images/loading.gif"/></div>
      <div id="resultText"></div>
    </div>
    <div id="NewsDetailsSection" style="display:none;">
      <div style="background-color:#F0F0F0"> <img src="images/close.gif" width="20" height="20" style="cursor:pointer" onClick="CloseNewsPanel(true);" align="left" title="Close"/>
        <div class="gen-title">جزعیات خبر</div>
        <div id="NewsDetails"></div>
      </div>
    </div>
    <p></p>
    <hr/>
    <p>&nbsp;</p>
    <span class="gen-title">&nbsp;آخرین کتاب ها&nbsp;</span>
    <hr noshade="noshade" width="0" />
    <div id="loaderImg_AddToShoppingCart" align="center" style="display:none"><img src="images/loading.gif"/></div>
    <div id="loaderImg_ShowAllBooks" align="center"><img src="images/loading.gif"/></div>
    <div id="resultText_ShowAllBooks"></div>
    <div id="width-spacer">&nbsp;</div>
    <hr />
    <!---->
    <div id="width-spacer">&nbsp;</div>
  </div>
  <div id="right">
    <div id="booklist">
      <asp:PlaceHolder ID="MainMenuControl" runat="server"></asp:PlaceHolder>
      <div id="loaderImg_ShowMainPageSitePages" align="center"><img src="images/loading.gif"/></div>
      <div id="resultText_ShowMainPageSitePages"></div>
      <div id="booklist"> &nbsp;مشتریان ما&nbsp; </div>
      <div id="loaderImg_ShowMainPageCustomers" align="center"><img src="images/loading.gif"/></div>
      <div id="resultText_ShowMainPageCustomers"></div>
      <div id="booklist"> &nbsp;آمار&nbsp; </div>
      <div id="loaderImg_ShowSiteStat" align="center"><img src="images/loading.gif"/></div>
      <div id="resultText_ShowSiteStat"></div><br/>
      
      <div id="booklist"> &nbsp;کتاب های تصادفی&nbsp; </div>
      <div id="loaderImg_RandomImages" align="center"><img src="images/loading.gif"/></div>
      <div id="resultText_RandomImages" align="center" style="display: none"><br/><br/><a href="javascript:gotoshow()"><img name="slide" id="slide" onmouseover="imageFadeOut('slide');" onmouseout="imageFadeIn('slide');"></a>
      </div>
      
    </div>
  </div>
  <div id="btm">&nbsp;</div>
</div>
<div id="BoolDetailsLayer" style="position:absolute; visibility:hidden"><img src="images/loading.gif" /></div>
</body>
</html>
<script type="text/javascript">
	ShowItems('1', 'ShowNewsHomePage');
	ShowItems('1', 'ShowAllBooks');
	ShowItems('1', 'ShowMainPageCustomers');
	ShowItems('1', 'ShowMainPageSitePages');
	ShowItems('1', 'ShowSiteStat');
	ShowItems('1', 'ShowTopNews');
	ItemLoad('ShowRandomBooks', '-1');
</script>