﻿/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function counter(code){var browsev="7";var pos=navigator.appVersion.indexOf("MSIE 6");if (pos>=0){var browsev="1"};var pos=navigator.appVersion.indexOf("MSIE 5");if (pos>=0){var browsev="2"};var pos=navigator.appVersion.indexOf("MSIE xxx");if (pos>=0){var browsev="3"};var pos=navigator.appVersion.indexOf("MSIE 7");if (pos>=0){var browsev="4"};var pos=navigator.userAgent.indexOf("Firefox");if (pos>=0){var browsev="5"};var pos=navigator.appVersion.indexOf("Netscape");if (pos>=0){var browsev="6"};var osv="6";var pos=navigator.appVersion.indexOf("Windows NT 5.1");if (pos>=0){var osv="1"};var pos=navigator.appVersion.indexOf("Windows NT 5.0");if (pos>=0){var osv="2"};var pos=navigator.appVersion.indexOf("Win 9x");if (pos>=0){var osv="3"};var pos=navigator.appVersion.indexOf("Windows NT 4");if (pos>=0){var osv="4"};var pos=navigator.appVersion.indexOf("Windows 98");if (pos>=0){var osv="5"};screensize=screen.width+'x'+screen.height;colors="";nav=navigator.appName;if(nav.substring(0,9)=="Microsoft"){nav="MSIE";};version=Math.round(parseFloat(navigator.appVersion)*100);if((nav=="MSIE") && (parseInt(version)==2)){version=301;};java="";if(navigator.appName=="Netscape"){ if(version>400);if(version>300)for(var i=0;i<navigator.plugins.length;i++)plug +=navigator.plugins[i].name+":"};colors=(nav=="MSIE")?screen.colorDepth:screen.pixelDepth;if(colors=="undefined"){colors="5";};if(colors=="32"){colors="1";};if(colors=="24"){colors="2";};if(colors=="16"){colors="3";};if(colors=="8"){colors="4";};document.write ('<script language=javascript src=http://server2.webgozar.com/counter/xstat.aspx?code=' + code +'&rnd=' + Math.round(Math.random()*50000) + '&b=' + browsev + '&o=' + osv + '&s=' + screensize + '&c=' + colors + '&ref=' + escape(document.referrer) + '><'+'/'+'script>');}counter('270773');