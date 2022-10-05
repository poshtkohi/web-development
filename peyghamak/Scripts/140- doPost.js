/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function baPost(loaderDiv, resultDiv, postBtn, LanguageType, PostAlign)
{
	document.getElementById(resultDiv).innerHTML='';
	if(document.getElementById("iPost").value == '')
	{
		document.getElementById(resultDiv).innerHTML = 'متن ارسالی خالی است.';
		return ;
	}
    serverPage="my.aspx";
	document.getElementById(postBtn).disabled=true;
	document.getElementById(loaderDiv).innerHTML=loaderUI;
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
				document.getElementById(loaderDiv).innerHTML="";
				if(xmlHttp.responseText == 'EmptyContent')
					document.getElementById(resultDiv).innerHTML = 'متن ارسالی خالی است.';
				if(xmlHttp.responseText == 'Success')
				{
					document.getElementById(resultDiv).innerHTML='متن شما با موفقیت ارسال شد.';
					baShowUp('my','1', 'loaderImg', 'pContainer', 'resultText');
				}
				else
					document.getElementById(resultDiv).innerHTML=xmlHttp.responseText;
				document.getElementById(postBtn).disabled=false;
				document.getElementById("iPost").value = 'اینجا بنویسید';
				document.getElementById("PostNumLabel").innerHTML = parseInt(document.getElementById("PostNumLabel").innerHTML)+1;
				document.getElementById("charRem").innerHTML = '140 کاراکتر باقی مانده ';
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('content=' + document.getElementById("iPost").value + '&LanguageType=' + LanguageType + '&PostAlign=' + PostAlign);
		//xmlHttp.close();
	}
}

function LanguageType()
{
	if(blang=="فارسي") 
		return '0';
	if(blang="English")
		return '1';
	else
		return '0';
}
function PostAlign()
{
	if(rightAlign) 
		return '1';
	else
		return '0';
}