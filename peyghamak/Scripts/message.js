/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//(c) Alireza Poshtkohi, 2008, alireza.poshtkohi@gmail.com
function SendPrivateMessage(loaderDiv, resultDiv, postBtn, LanguageType, PostAlign, _PersonID)
{
	document.getElementById(resultDiv).innerHTML='';
	if(document.getElementById("iPost").value == '')
	{
		document.getElementById(resultDiv).innerHTML = 'متن ارسالی خالی است.';
		return ;
	}
    serverPage="message.aspx";
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
					document.getElementById(resultDiv).innerHTML = 'پیغام ارسالی خالی است.';
				if(xmlHttp.responseText == 'Success')
					document.getElementById(resultDiv).innerHTML='پیغام شما با موفقیت ارسال شد.';
				else
					document.getElementById(resultDiv).innerHTML=xmlHttp.responseText;
				document.getElementById(postBtn).disabled=false;
				document.getElementById("iPost").value = '';
				document.getElementById("charRem").innerHTML = '450 کاراکتر باقی مانده ';
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('content=' + encodeURIComponent(document.getElementById("iPost").value) + '&LanguageType=' + LanguageType + '&PostAlign=' + PostAlign + '&PersonID=' + _PersonID);
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