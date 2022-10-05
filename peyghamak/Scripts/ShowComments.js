/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//(c) Alireza Poshtkohi, 2008, alireza.poshtkohi@gmail.com
function baShowUp(iPage, page, loaderDiv, showBox, resultDiv)
	{
	serverPage='comments.aspx';
	document.getElementById(loaderDiv).innerHTML = loaderUI;
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
				if(xmlHttp.responseText == 'DoRefresh')
				{
					baShowUp(iPage, '1', loaderDiv, showBox, resultDiv);
					return;
				}
				document.getElementById(loaderDiv).innerHTML = "";
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById(resultDiv).innerHTML = 'نظری وجود ندارد.';
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
}