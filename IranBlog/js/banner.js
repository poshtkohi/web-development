/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

/* Advanced Iran Blog Advertisement by Alireza Poshtkohi. Alireza at www.iranblog.com */

var AD_IQWW_timerCloseAd=0,AD_IQWW_timerFade=0,AD_IQWW_timerRedo=0,AD_IQWW_opacity=0,AD_IQWW_mode=0;
var AD_IQWW_windowWidth=0,AD_IQWW_windowHeight=0;
var AD_IQWW_timeoutCloseAd=180000,AD_IQWW_timeoutStart=7000,AD_IQWW_timeoutRedo=0;
var AD_IQWW_layers=new Array('AD_IQWW_HEAD','AD_IQWW_HEAD');

/*************** start phase ****************/
function AD_IQWW_init() {

AD_IQWW_mode=2;

if (AD_IQWW_timerRedo) clearTimeout(AD_IQWW_timerRedo);

	AD_IQWW_reloadPage();
if (AD_IQWW_clientWindowSize) {
	if(AD_IQWW_timeoutStart)
	AD_IQWW_timerStart=setTimeout("AD_IQWW_start()",AD_IQWW_timeoutStart);
	}
else {
	if(AD_IQWW_timeoutRedo)
	AD_IQWW_timerRedo=setTimeout("AD_IQWW_init()",AD_IQWW_timeoutRedo);
	}

return false;
}
/***************** frameset *****************/

function AD_IQWW_clientWindowSize() {
	var ad=1;
		
	if (self.innerWidth) {
		AD_IQWW_windowWidth=self.innerWidth;
		AD_IQWW_windowHeight=self.innerHeight;
	} else if (document.documentElement && document.documentElement.clientWidth) {
		AD_IQWW_windowWidth=document.documentElement.clientWidth;
		AD_IQWW_windowHeight=document.documentElement.clientHeight;
	} else if (document.body) {
		AD_IQWW_windowWidth=document.body.clientWidth;
		AD_IQWW_windowHeight=document.body.clientHeight;
	}
	if (document.layers||navigator.userAgent.toLowerCase().indexOf("gecko")>=0) AD_IQWW_windowWidth-=16;
	

	if (AD_IQWW_windowWidth<300) ad=0;
	if (AD_IQWW_windowHeight<80) ad=0;
	return ad;
}

/***************** layers functions *****************/

function AD_IQWW_findObj(n, d) { 
      var obj=null;
	if (document.getElementById) {
	 obj=document.getElementById(n);
	 } else if (document.layers && document.layers[n]) {
	 obj=document.layers[n];
	 } else if (document.all) {
	 obj=document.all[n];
	 } 
      return(obj);
}

function AD_IQWW_showHideLayers() { 
	var i,p,v,w,obj,args=AD_IQWW_showHideLayers.arguments;

	for (i=0; i<(args.length-1); i+=2) if ((obj=AD_IQWW_findObj(args[i]))!=null) { 
		v=args[i+1];
		if (obj.style) { 
		obj=obj.style;
		v=(v=='show')?'visible':(v='hide')?'hidden':v; 
		}
		obj.visibility=v;
	 }
}

function AD_IQWW_fade(fadeStep) {
	if (AD_IQWW_timerFade) clearTimeout(AD_IQWW_timerFade);
	var i=0;
	for(i=0;i<2;i++) {
		obj=AD_IQWW_findObj(AD_IQWW_layers[i]);
		AD_IQWW_opacity=AD_IQWW_opacity + fadeStep;
		if(obj.style){
			if (obj.filters) {
				if (obj.style.filter) {
					obj.filters.alpha.opacity=AD_IQWW_opacity;
				} else {
					obj.style.filter="alpha(opacity=0)";
					obj.filters.alpha.opacity=AD_IQWW_opacity;
				}
			} else {
				obj.style.MozOpacity=0;
				obj.style.MozOpacity=AD_IQWW_opacity/100;
			}
		}
		if(AD_IQWW_opacity<100 && AD_IQWW_opacity>0) {
			AD_IQWW_timerFade=setTimeout('AD_IQWW_fade('+fadeStep+')',100);
		}
	}
}

function AD_IQWW_setPos(layer,left,top) {
	var obj=AD_IQWW_findObj(layer);
	
	if (obj.style) {
		if (document.all) {
			if (left!='') obj.style.pixelLeft=left;
			if (top!='') obj.style.pixelTop=top;
		} else {
			if (left!='') obj.style.left=left;
			if (top!='') obj.style.top=top;
		}
	} else {
		if (left!='') obj.left=left;
		if (top!='') obj.top=top;
	}
}


/***************** events *****************/

function AD_IQWW_close() {
	if (AD_IQWW_timerFade) clearTimeout(AD_IQWW_timerFade);
	if (AD_IQWW_timerCloseAd) clearTimeout(AD_IQWW_timerCloseAd);
	AD_IQWW_opacity=100; 
	AD_IQWW_fade(1);
	AD_IQWW_mode=4;
	AD_IQWW_reloadPage();

}

function AD_IQWW_start() {
	if (AD_IQWW_timerStart) clearTimeout(AD_IQWW_timerStart);
	if (AD_IQWW_timerFade) clearTimeout(AD_IQWW_timerFade);
	if (AD_IQWW_timerCloseAd) clearTimeout(AD_IQWW_timerCloseAd);
	AD_IQWW_fade(1);
      AD_IQWW_mode=3;
	AD_IQWW_reloadPage();

	if(AD_IQWW_timeoutCloseAd)
	AD_IQWW_timerCloseAd=setTimeout("AD_IQWW_close()",AD_IQWW_timeoutCloseAd);
}

function AD_IQWW_reloadPage() {
	AD_IQWW_clientWindowSize();
	onresize=AD_IQWW_reloadPage;	 

	if (AD_IQWW_clientWindowSize) {
		AD_IQWW_setPos('AD_IQWW_CLOSE',AD_IQWW_windowWidth-140,'');
		AD_IQWW_setPos('AD_IQWW_HEAD',AD_IQWW_windowWidth-640,'');
	}

	if(AD_IQWW_mode==4)
	AD_IQWW_showHideLayers('AD_IQWW_HEAD','hide','AD_IQWW_CLOSE','show');

	if(AD_IQWW_mode==3)
	AD_IQWW_showHideLayers('AD_IQWW_CLOSE','hide','AD_IQWW_HEAD','show');
}

/***************** init layers *****************/

function AD_IQWW_drawLayers() {

if (AD_IQWW_mode) return;
else AD_IQWW_mode=1;


document.writeln('<style type="text/css">\n.adstyle1 {font-family:verdana,helvetica,arial;font-size:10px;color:white;font-weight:bold;text-decoration:none;}\n.adstyle2 {font-family:verdana,helvetica,arial;font-size:12px;color:white;font-weight:bold;text-decoration:none;}\n</style>\n');

document.writeln('<div id="AD_IQWW_CLOSE" dir="ltr" style="position:absolute; width:140px; left:0px; top:0px; visibility:hidden; z-index:2000; overflow: hidden;">'+
'<table border="0" cellpadding="0" cellspacing="0">'+
'<tr><td ><img src="Advertisements/rcorner.gif" width="20" height="20" border="0"></td>'+
'<td width="101" height="20" align="center" bgcolor="white" style="border-bottom: 1 solid #808080;" ><a href="javascript:void(0)" onclick="AD_IQWW_start();return false;" style="font-family:arial;font-size:9pt;text-decoration:none;color:#000000;">Advertisement</a></td>'+
'<td width="20" bgcolor="white" style="border-bottom: 1 solid #808080"><a href="javascript:void(0)" onclick="AD_IQWW_start();return false;"><img src="Advertisements/replayicon.gif" width="20" height="19" border="0"></td>'+
'</tr></table>'+
'</div>');

document.writeln('<div id="AD_IQWW_HEAD" dir="ltr" style="position:absolute; width:640px; top:0px; left:0px; visibility:hidden; z-index:2001;">'+
'<table border="0" cellpadding="0" cellspacing="0"><tr><td>'+
'<table border="0" cellpadding="0" cellspacing="0">'+
'<tr><td><img src="Advertisements/lcorner.gif" width="20" height="20" border="0"></td></tr>'+
'<tr height="61"><td height="61" style="border-left: 1px solid #808080; border-bottom: 1 solid #808080" bgcolor="#f4f4f4">&nbsp</td></tr>'+
'</table></td><td>'+
'<table border="0" cellpadding="0" cellspacing="0">'+
'<tr height="81"><td align="top" height="81" width="480" bgcolor="#f4f4f4" rowspan="2" style="border-top: 1px solid #808080; border-bottom: 1 solid #808080" valign="middle" align="center">'+
'<div align="center" valign="center">');

document.writeln('<table border="0" bordercolor="" cellpadding="0" cellspacing="0"><tr><td><object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0" width="468" height="60"><param name=movie value="Advertisements/new2.swf"><param name=quality value=high><embed src="Advertisements/new2.swf" quality=high pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="468" height="60"></embed></object><br><div align="center"><a style="text-decoration:underline;font-family:Tahoma;font-size:9pt;" href="http://admanager.persianblog.com/processASPClick.asp?bannerID=307" target="_blank">Soft City</a></div></td></tr></table>'); 

document.writeln('</div></td></tr></table></td><td>'+
'<table border="0" cellpadding="0" cellspacing="0">'+
'<tr><td bgcolor="#f4f4f4"><img src="Advertisements/rcorner.gif" width="20" height="20" border="0"></td></tr>'+
'<tr height="61"><td height="61" style=" border-bottom: 1 solid #808080" bgcolor="#f4f4f4">&nbsp</td></tr>'+
'</table></td><td>'+
'<table border="0" cellpadding="0" cellspacing="0" width="121">'+
'<tr><td align="center" width="101" height="20" bgcolor="white" style="border-bottom: 1 solid #808080;font-size:9pt" ><a href="http://www.persianblog.com/ads.asp" target="_blank" style="font-family:arial;font-size:9pt;text-decoration:none;color:#000000;">Advertise Here</a></td>'+
'<td width="20" bgcolor="white" style="border-bottom: 1 solid #808080"><a href="javascript:void(0)" onclick="AD_IQWW_close();return false;" ><img src="Advertisements/closeicon.gif" width="20" height="19" border="0"></a></td>'+
'</tr><tr height="60">'+
'<td height="60" colspan="2" style="border-right: 1px solid #808080; border-bottom: 1 solid #808080"><a href="http://www.persianblog.com/ads.asp" target="_blank" style="font-family:arial;font-size:9pt;text-decoration:none;color:#000000;"><img onload="IranBlogAdvertisment();return false;" src="Advertisements/pb-ads.gif" width="120" height="60" border="0"></a></td>'+
'</tr></table></td></tr></table>'+
'</div>');

}
/****** IranBlogAdvertisment *************/
function IranBlogAdvertisment() {

 if ( (AD_IQWW_mode==3) || (AD_IQWW_mode==4) ) {
 AD_IQWW_reloadPage();
 } 
 else if (AD_IQWW_mode==0){
 AD_IQWW_drawLayers();
 window.onload=IranBlogAdvertisment;
 window.onresize=IranBlogAdvertisment;
 window.onfocus=IranBlogAdvertisment;
 } else {
 AD_IQWW_init();
 }

}

