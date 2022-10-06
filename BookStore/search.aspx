<%@ Page Language="C#" CodeFile="search.aspx.cs" Inherits="bookstore.search"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>جستجوی کتاب</title>
		<script type="text/javascript" src="js/AjaxCore.js"></script>
		<script type="text/javascript" src="js/Functions.js"></script>
        <link href="style/calass.css" rel="stylesheet" type="text/css" />
        <link href="styles/cp.css" rel="stylesheet" type="text/css" />
        <link href="styles/addons.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="MainDiv">
    <div id="skl">
        <div id="top"><img src="images/top.jpg" /></div>
        <div id="topmenu">
        <div id="lang"><a href="#">En</a> - <a href="#">Fa</a></div>
        &nbsp;
        <asp:PlaceHolder ID="LoginPanelControl" runat="server"></asp:PlaceHolder> &nbsp;
        
      </div>
        <div id="topspace"></div>
                <div id="loaderImg_ShowMainPageSitePageDetailes" align="center" style="display:none"><img src="images/loading.gif"/></div>
        <div id="center-s1">
     <br />
     <span class="search">
	<form id="form" name="form" onSubmit="return false;" method="post" runat="server">
     <table width="100%" border="0" cellspacing="2" cellpadding="2">
      <tr>
        <td>&nbsp;</td>
        <td width="80" align="right">عنوان</td>
        <td width="10">&nbsp;</td>
        <td width="150" align="left"><input type="text" name="title" id="title" value="<%if(this.Request.Form["searchbox"] != null && this.Request.Form["searchbox"] != "") this.Response.Write(this.Request.Form["searchbox"]);%>" style="text-align:left;direction:ltr"/></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
       <td>&nbsp;</td>
       <td align="right">نویسنده</td>
       <td>&nbsp;</td>
       <td align="left"><input type="text" name="writer" id="writer" style="text-align:left;direction:ltr"/></td>
       <td>&nbsp;</td>
       </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="right">ناشر</td>
        <td>&nbsp;</td>
        <td align="left"><input type="text" name="publisher" id="publisher" style="text-align:left;direction:ltr"/></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="right">سال چاپ</td>
        <td>&nbsp;</td>
        <td align="left"><input type="text" name="date" id="date" style="text-align:left;direction:ltr"/></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="right">
          <input type="submit" name="search" id="search" value="بگرد" onclick="ShowItems('1', 'ShowByBookSearch');"/>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
     </table>
     </form>
	 </span>	
     <br />
     <hr />
     <p class="gen-info">
     <!-- سه مورد یافت شد -->
     </p>
     <br />
		    <div id="loaderImg_AddToShoppingCart" align="center" style="display:none"><img src="images/loading.gif"/></div>
            <div id="loaderImg" align="center" style="display:none"><img src="images/loading.gif"/></div>
            <div id="resultText"></div>      
     <br />
          <br />     <br />     <br />     <br />     <br />     <br />     <br />     <br />             <br />     <br />     <br />
		
     <div id="pager"> <!-- a href="#">1</a> <a href="#">2</a> 3 <a href="#">4</a> <a href="#">5</a --></div>
      
     <div id="width-spacer">&nbsp;</div>
    </div>
        <div id="right">
         <div id="booklist">
         <asp:PlaceHolder ID="MainMenuControl" runat="server"></asp:PlaceHolder>
         <div id="loaderImg_ShowMainPageSitePages" align="center"><img src="images/loading.gif"/></div>
         <div id="resultText_ShowMainPageSitePages"></div>     
         
        <div id="booklist">
        &nbsp;مشتریان ما&nbsp;
        </div>        
            <div id="loaderImg_ShowMainPageCustomers" align="center"><img src="images/loading.gif"/></div>
            <div id="resultText_ShowMainPageCustomers"></div>
        
        <div id="booklist">
        &nbsp;آمار&nbsp;
        </div>  
            <div id="loaderImg_ShowSiteStat" align="center"><img src="images/loading.gif"/></div>
            <div id="resultText_ShowSiteStat"></div><br/>
      
      <div id="booklist"> &nbsp;کتاب های تصادفی&nbsp; </div>
      <div id="loaderImg_RandomImages" align="center"><img src="images/loading.gif"/></div>
      <div id="resultText_RandomImages" align="center" style="display: none"><br/><a href="javascript:gotoshow()"><img name="slide" id="slide" onmouseover="imageFadeOut('slide');" onmouseout="imageFadeIn('slide');"></a>
      </div> 
      </div>   
    </div>
    <div id="btm">&nbsp;</div> 
</div>
<div id="BoolDetailsLayer" style="position:absolute; visibility:hidden"><img src="images/loading.gif" /></div>
</body>
</html>
<script type="text/javascript">
	ShowItems('1', 'ShowMainPageCustomers');
	ShowItems('1', 'ShowMainPageSitePages');
	ShowItems('1', 'ShowSiteStat');
	<%
		if(this.Request.Form["searchbox"] != null && this.Request.Form["searchbox"] != "")
			this.Response.Write("ShowItems('1', 'ShowByBookSearch');");
	%>
	ItemLoad('ShowRandomBooks', '-1');
</script>