/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

// JavaScript Document
function baShowUp(iPage, page, loaderDiv, showBox, resultDiv)
//function baShowUp(ShowMode, page, showBox, loaderDiv, resultDiv)
{
	serverPage=iPage+".aspx";
	document.getElementById(loaderDiv).innerHTML=loaderUI;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				document.getElementById(loaderDiv).innerHTML = "";
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById(resultDiv).innerHTML = 'پیغامکی وجود ندارد.';
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
}