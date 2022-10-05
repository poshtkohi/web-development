<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LatestUpdates.aspx.cs" Inherits="services.LatestUpdates" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>لیست وبلاگ های به روز شده ایران بلاگ</title>
<meta name="Description" content="IranBlog User Update List" />
<meta name="Keywords"  content="Update,weblog,updatelist,iranblog user update,به روز,آپدیت" />
<meta name="Expires" content="2" />
<meta name="Distrubution" content="Global" />
<meta name="Robots" content="All" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<style type="text/css">
A{text-decoration: none}
body  {background-color:#008600;font-family:tahoma;font-size : 11px;}
#item {width : 650px;background-color:#FFFFFF;border:1px solid 000000;direction:rtl}
#item .Title {background-image:url(/images/bg.gif);padding : 4px;color:#F4FAFD;border-bottom:1px solid 000000}
#item .item {padding : 6px;text-align:right;}
#item .si-1 {color:#FF0000}
.si-2 {color:#669933}
small {margin-right:6px;color:#FF6633}
.subitem1 {background-color:#ffffff;padding:5px}
.subitem2 {background-color:#F3F3FB;padding:5px;border-top:1px solid #DFDFF3;border-bottom:1px solid #DFDFF3}
</style>
<script src="../Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
</head>
<body>
<div align="center">
<script type="text/javascript">
AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0','width','468','height','60','align','middle','src','/swf/banner468_60','quality','high','pluginspage','http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash','movie','/swf/banner468_60' ); //end AC code
</script><noscript><object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="468" height="60" align="middle">
  <param name="movie" value="/swf/banner468_60.swf" />
  <param name="quality" value="high" />
  <embed src="/swf/banner468_60.swf" quality="high" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="468" height="60"></embed>
</object></noscript>
</div>
<br>
<div  align="center">
<div id="item">
<div class="Title">
لیست وبلاگ های به روز شده ایران بلاگ</div>
<div class="item">

<% services.LatestUpdates.ShowLatestUpdates(this); %>

</div>
</div>
</div>
</body>
</html>
