/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

if(typeof HTMLElement!="undefined" && !
HTMLElement.prototype.insertAdjacentElement){
	HTMLElement.prototype.insertAdjacentElement = function
(where,parsedNode)
	{
		switch (where){
		case 'beforeBegin':
			this.parentNode.insertBefore(parsedNode,this)
			break;
		case 'afterBegin':
			this.insertBefore(parsedNode,this.firstChild);
			break;
		case 'beforeEnd':
			this.appendChild(parsedNode);
			break;
		case 'afterEnd':
			if (this.nextSibling) 
this.parentNode.insertBefore(parsedNode,this.nextSibling);
			else this.parentNode.appendChild(parsedNode);
			break;
		}
	}

	HTMLElement.prototype.insertAdjacentHTML = function
(where,htmlStr)
	{
		var r = this.ownerDocument.createRange();
		r.setStartBefore(this);
		var parsedHTML = r.createContextualFragment(htmlStr);
		this.insertAdjacentElement(where,parsedHTML)
	}


	HTMLElement.prototype.insertAdjacentText = function
(where,txtStr)
	{
		var parsedText = document.createTextNode(txtStr)
		this.insertAdjacentElement(where,parsedText)
	}
}




var mahal="", blang="فارسي", mfa="", curin, mova, defe="", help
//mahal="ALL"
function mfin(){mtag=window.event.srcElement;if ((((mtag.tagName=="INPUT" && mtag.type=="text") || mtag.tagName=="TEXTAREA") && (mahal=="ALL" || mtag.lang=="fa"))){
if (mfa=="hast") mlang.outerHTML="";mtag.insertAdjacentHTML("beforeBegin", "<span title='Left Shift+Alt=>EN or FA\nShift+Space=>Halfspace\nCtrl+K=>See KeyPosition' id=mlang style='background-color:#808080; font-size:11; color:#FFFFFF; position:absolute; margin-left:17; padding-right:1px; padding-left:2px; padding-bottom:2px; font-family:tahoma; cursor:default' onclick='toit()'>"+blang+"</span>");curin=mtag;mlang.style.marginTop=mtag.offsetHeight;mtag.title='!Press Ctrl+K to see Help';mtag.style.direction="rtl";mfa="hast";}else{if (mfa=="hast" && window.event.srcElement!=mlang){mlang.outerHTML="";mfa="";}if (help){hel.outerHTML="";help=false;}}}function toit(){if(blang=="فارسي") blang="English"; else blang="فارسي";mlang.innerHTML=blang;curin.focus();}function mkdown(){
mtag=window.event.srcElement;mkey=window.event.keyCode;allcode="|72|70|192|74|69|219|221|80|79|78|66|86|67|220|83|65|87|81|88|90|85|89|84|82|186|222|71|76|75|188|73|68|77|190|191|48|57|";allharf="ابپتثجچحخدذرزژسشصضطظعغفقكگلمنوهيئ./09";allscode="|72|188|219|221|70|90|191|186|77|190|65|83|68|81|87|69|71|84|78|66|89|86|82|74|80|79|73|85|48|57|";allsharf="آؤ»«ّة؟:ءأًٌٍَُِْ،أإ؛ؤ‍ـ©®™€()";if ((((mtag.tagName=="INPUT" && mtag.type=="text") || mtag.tagName=="TEXTAREA") && (mahal=="ALL" || mtag.lang=="fa"))){mtag.caretPos = document.selection.createRange();if (mkey==16 && window.event.altLeft) toit();
mk=allcode.indexOf("|"+mkey+"|");mpos=0;ml=0;if (mk>-1 && blang=="فارسي"){if (!window.event.ctrlKey){if (!window.event.shiftKey && !window.event.ctrlKey){if (mk>0){mall=allcode.substr(0,mk);while (mpos>-1){mpos=mall.indexOf("|",mpos+1);ml++;}} if(mtag.caretPos.text!=0) document.selection.clear(); mtag.caretPos.text=allharf.substr(ml,1);return(false);}else{mk=allscode.indexOf("|"+mkey+"|");if (mk>-1){if (mk>0){mall=allscode.substr(0,mk);while (mpos>-1){mpos=mall.indexOf("|",mpos+1);ml++;}}mtag.caretPos.text = allsharf.substr(ml,1);if (mkey==80) mtag.caretPos.text = "";}return(false);}}else{if (mkey==75 && window.event.ctrlKey && blang=="فارسي" && !help){
mtag.insertAdjacentHTML("afterend", "<Span id=hel style='z-index:100; Filter:Alpha(opacity=90); border:5 ridge lightgreen; background-color:lightgreen; position:absolute; padding:10; margin-right:100; top:150'><table dir=ltr align=center style='font-family:tahoma; padding:10; text-align:center; font-size:12; color:001100; border:1 green ridge'><tr><td NOWRAP style='border:1 green ridge; padding:2'>Shift+H = آ<td NOWRAP style='border:1 green ridge; padding:2'>Shift+< = ؤ<td NOWRAP style='border:1 green ridge; padding:2'>Shift+} = »<td NOWRAP style='border:1 green ridge; padding:2'>Shift+S = ضمه<td NOWRAP style='border:1 green ridge; padding:2'>Shift+F = تشديد</tr><tr><td NOWRAP style='border:1 green ridge; padding:2'>Shift+? = ؟<td NOWRAP style='border:1 green ridge; padding:2'>Shift+N = أ<td NOWRAP style='border:1 green ridge; padding:2'>Shift+U = يورو<td NOWRAP style='border:1 green ridge; padding:2'>Shift+E = تنوين‏جر<td NOWRAP style='border:1 green ridge; padding:2'>Shift+P = كپي‏رايت</tr><tr><td NOWRAP style='border:1 green ridge; padding:2'>Shift+Y = ؛<td NOWRAP style='border:1 green ridge; padding:2'>Shift+T = ،<td NOWRAP style='border:1 green ridge; padding:2'>Shift+G = ساكن<td NOWRAP style='border:1 green ridge; padding:2'>Shift+R = حرف‏كوچك<td NOWRAP style='border:1 green ridge; padding:2'>Shift+W = تنوين‏رفع</tr><tr><td NOWRAP style='border:1 green ridge; padding:2'>Shift+Z = ة<td NOWRAP style='border:1 green ridge; padding:2'>Shift+{ = «<td NOWRAP style='border:1 green ridge; padding:2'>Shift+D = كسره<td NOWRAP style='border:1 green ridge; padding:2'>Shift+I = نشان‏تجاري<td NOWRAP style='border:1 green ridge; padding:2'>Shift+Q = تنوين‏نصب</tr><tr><td NOWRAP style='border:1 green ridge; padding:2'>Shift+B = إ<td NOWRAP style='border:1 green ridge; padding:2'>Shift+M = ء<td NOWRAP style='border:1 green ridge; padding:2'>Shift+A = فتحه<td NOWRAP style='border:1 green ridge; padding:2'>Shift+O = ثبت‏شده<td NOWRAP style='border:1 green ridge; padding:2'>Shift+Space = نيم‏فاصله</tr></table></span>");
hel.style.left=window.screen.availWidth/2-hel.offsetWidth/2;hel.style.top=50+parent.document.body.scrollTop;help=true;return(false);}}}if (mkey==32 && window.event.shiftKey && blang=="FA"){mtag.caretPos.text = "‏";return(false);}}}


var browserName=navigator.appName; 
if (browserName=="Microsoft Internet Explorer")
{
  
document.onfocusin=mfin;
document.onkeydown=mkdown;
}
