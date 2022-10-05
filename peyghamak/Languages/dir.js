/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

// JavaScript Document
function dir()
{
	mtag=window.event.srcElement;
	if ((((mtag.tagName=="INPUT" && mtag.type=="text") || mtag.tagName=="TEXTAREA") && (mahal=="ALL" || mtag.lang=="fa")))
	{
		if (mfa=="hast") 
			mlang.outerHTML="";
		/*mtag.insertAdjacentHTML("beforebegin", "<span title='Left Shift+Alt=>EN or FA\nShift+Space=>Halfspace\nCtrl+K=>See KeyPosition' id=mlang style='background-color:#808080; font-size:11; color:#FFFFFF; position:absolute; margin-left:17; padding-right:1px; padding-left:2px; padding-bottom:2px; font-family:tahoma; cursor:default' onclick='toit()'>"+blang+"</span>")*/
		mtag.insertAdjacentHTML("afterEnd", "Hellow World!");
		curin=mtag;
		mlang.style.marginTop=mtag.offsetHeight;
		mtag.title='!Press Ctrl+K to see Help';
//		mtag.style.direction="ltr";
		mtag.style.textAlign="left";
		mfa="hast";
	}
	else
	{
		if (mfa=="hast" && window.event.srcElement!=mlang)
		{
			mlang.outerHTML="";
			mfa="";
		}
		if (help)
		{
			hel.outerHTML="";
			help=false;
		}
	}
}

document.onfocusin=dir;