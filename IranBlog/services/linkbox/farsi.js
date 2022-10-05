/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

ToClipboard = function()
{
	var doc = document.getElementById('code');
	var code = doc.innerText;
		
	if(window.clipboardData)
	{
		window.clipboardData.setData('text', code);
		doc.focus();
		doc.select();
		
		alert('کد لینک باکس با موفقیت کپی شد');
	}
}

PrintSource = function()
{
	var doc = document.getElementById('code');
	var code = doc.innerText;
	var td		= doc.parentNode;
	var iframe	= document.createElement('IFRAME');
	var doc		= null;
	var wnd		= 

	// this hides the iframe
	iframe.style.cssText = 'position:absolute; width:0px; height:0px; left:-5px; top:-5px;';
	
	td.appendChild(iframe);
	
	doc = iframe.contentWindow.document;
	code = code.replace(/</g, '&lt;');
	
	doc.open();
	doc.write('<pre>' + code + '</pre>');
	doc.close();
	
	iframe.contentWindow.focus();
	iframe.contentWindow.print();
	
	td.removeChild(iframe);
}
ViewSource = function()
{
	var doc = document.getElementById('code');
	var code = doc.innerText;

		var wnd = window.open('', '_blank', 'width=750, height=400, location=0, resizable=1, menubar=0, scrollbars=1');
	
	code = code.replace(/</g, '&lt;');
	
	wnd.document.write('<pre>' + code + '</pre>');
	wnd.document.close();
}
About = function()
{
	var wnd	= window.open('', '_blank', 'dialog,width=320,height=150,scrollbars=0');
	var doc	= wnd.document;
	
	var styles = document.getElementsByTagName('style');
	var links = document.getElementsByTagName('link');
	
	doc.write(dp.sh.Strings.AboutDialog.replace('{V}', '9'));
	
	for(var i = 0; i < styles.length; i++)
		doc.write('<style>' + styles[i].innerHTML + '</style>');

	for(var i = 0; i < links.length; i++)
		if(links[i].rel.toLowerCase() == 'stylesheet')
			doc.write('<link type="text/css" rel="stylesheet" href="' + links[i].href + '"></link>');
	
	doc.close();
	wnd.focus();
}

