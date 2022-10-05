/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function baShowUp(iPage, page, loaderDiv, showBox, resultDiv)
	{
	serverPage='my.aspx';
	if(iPage == 'friends')	
		document.getElementById(resultDiv).innerHTML = '';
	if(document.getElementById(resultDiv).innerHTML == 'پیغامکی وجود ندارد.')
		document.getElementById(resultDiv).innerHTML = '';
	document.getElementById(loaderDiv).innerHTML = loaderUI;
	if(iPage=='my')
		document.getElementById("charRem").innerHTML = "280 کاراکتر باقی مانده";
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = 'http://www.peyghamak.com/signin.aspx';
					return;
				}
				document.getElementById(loaderDiv).innerHTML = "";
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById(resultDiv).innerHTML = 'پیغامکی وجود ندارد.';
					document.getElementById(showBox).innerHTML = '';
					return;
				}
				document.getElementById(loaderDiv).innerHTML="";
				document.getElementById(showBox).innerHTML=xmlHttp.responseText;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('ShowMode=' + iPage + '&page=' + page);
	}
	toggleTabs(iPage);
}