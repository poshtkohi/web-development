/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//(c) Alireza Poshtkohi, 2008, alireza.poshtkohi@gmail.com
//-----------------------------------------------------------------------------------------------------
function baShowUp(iPage, page, loaderDiv, showBox, resultDiv)
	{
	serverPage='my.aspx';
	if(iPage == 'friends')	
		document.getElementById(resultDiv).innerHTML = '';
	if(document.getElementById(resultDiv).innerHTML == 'پیغامکی وجود ندارد.')
		document.getElementById(resultDiv).innerHTML = '';
	document.getElementById(loaderDiv).innerHTML = loaderUI;
	if(iPage=='my')
		document.getElementById("charRem").innerHTML = "450 کاراکتر باقی مانده";
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
//-----------------------------------------------------------------------------------------------------
function ShowAllComments(_page, _mode)
{
	serverPage='my.aspx';
	document.getElementById('loaderImg' + _mode).innerHTML = loaderUI;
	document.getElementById('loaderImg' + _mode).style.display = 'block';
	
	var _xmlHttp = null;
	if((_xmlHttp = createAjaxObject()) != null){
		_xmlHttp.onreadystatechange=function()
		{
			if(_xmlHttp.readyState==4)
			{
				if(_xmlHttp.responseText == 'Logouted')
				{
					window.location = 'http://www.peyghamak.com/signin.aspx';
					return;
				}
				if(_xmlHttp.responseText == 'DoRefresh')
				{
					ShowAllComments('1', _mode);
					return;
				}
				document.getElementById('loaderImg' + _mode).innerHTML = "";
				document.getElementById('loaderImg' + _mode).style.display = 'none';
				if(_xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById('resultText' + _mode).innerHTML = "<div style='color:#FF0000'>نظری وجود ندارد.</div>";
					return;
				}
				document.getElementById('resultText' + _mode).innerHTML=_xmlHttp.responseText;
			}
		}
		_xmlHttp.open("POST",serverPage,true);
		_xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		_xmlHttp.send('ShowMode=' + _mode + '&page=' + _page);
	}
}
//-----------------------------------------------------------------------------------------------------