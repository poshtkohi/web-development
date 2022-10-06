<%@ Page language="c#" Codebehind="NewsGroupsShow.aspx.cs" AutoEventWireup="false" Inherits="news.NewsGroupsShow" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="Controls/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LoginStatusControl" Src="Controls/LoginStatusControl.ascx" %>
<html>
<head>
        <title>بخش های خبری</title>
        <meta http-equiv="Content-Language" content="fa">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link href="styles/main.css" rel="stylesheet" rev="stylesheet"/>
        <link href="styles/cp.css" rel="stylesheet" rev="stylesheet"/>
        <script language="javascript" src="/js/AjaxCore.js"></script>
        <script language="javascript" src="/js/Functions.js"></script>
        <script language="javascript" src="/js/farsi.js"></script>
        <script src="/Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
</head>
<body>
<table width="900" border="0" cellpadding="0" cellspacing="0" align="center">
  <!--DWLayoutTable-->
  <tr>
    <td width="23" rowspan="5" valign="top" background="images/bg_lft.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
    <td height="167" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF">
						<!--DWLayoutTable-->
		  <tr>
							<td><div>
							  <!--DWLayoutEmptyCell-->
                              <script type="text/javascript">
AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','525','height','170','src','swf/logo','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','swf/logo' ); //end AC code
                            </script>
							  <noscript>
							    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="525" height="170">
                                <param name="movie" value="swf/logo.swf">
                                <param name="quality" value="high">
                                <embed src="swf/logo.swf" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="525" height="170"></embed>
                              </object>
                              </noscript>
		      </div></td>
						  <td width="73" valign="top" bgcolor="#DBEDFF"><!--DWLayoutEmptyCell-->&nbsp; </td>
							<td><div class="header_right"><!--DWLayoutEmptyCell--></div>
							</td>
						</tr>
					</table>    </td>
    <td width="25" rowspan="5" valign="top" background="images/bg.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
  </tr>
  <tr>
    <td height="32" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
      <!--DWLayoutTable-->
      <tr>
        <td> <div align="center" class="under"><img src="images/titr2.gif" align="middle">&nbsp;</div></td>
        </tr>
             
    </table></td>
  </tr>
  <tr>
    <td height="5" valign="top" bgcolor="#999999"></td>
  </tr>
  <tr>
    <td height="387" valign="top"><table width="100%" height="388" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF">
      <!--DWLayoutTable-->
      <div id="message" class="message" style="display:none"></div>
      <tr>
        <td width="100%" align="right" valign="middle" class="box"><uc1:LoginStatusControl id="LoginStatusControl" runat="server"></uc1:LoginStatusControl></td>
        </tr>
      <tr>
        <td valign="top">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#DBEDFF" id="down">
          <!--DWLayoutTable-->
          <tr>
            <td width="35" height="276">&nbsp;</td>
            <td width="761" valign="top">
            
            <div id="loaderImg" align="center"></div>
            <div id="resultText"></div>           
            <script language="javascript">
				ShowItems('1', 'ShowNewsGroupsShow');
			</script> </td>
            <td>&nbsp;</td>
          </tr>
        </table></td>
      </tr>

    </table></td>
  </tr>
    <tr>
    <td height="61" valign="top"><uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter></td>
  </tr>
     <tr>
    <td height="25">&nbsp;</td>
    <td valign="top" background="images/b-b.gif"><!--DWLayoutEmptyCell-->&nbsp;</td>
  <td>&nbsp;</td>
  </tr>
</table>
		</table>
</body>
</html>