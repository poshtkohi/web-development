/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//MainPagesFunctions.js
//Version: 2.1
//This script is created by Alireza Poshtkohi. Do not remove, modify, or hide the author information. keep it intact.
//Mail: alireza.poshtkohi@gmail.com 

//-----------------------------global variables---------------------------------------------------------------------

//------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------
function DoPost(_mode)
{
	var serverPage='';
	switch(_mode)
	{
		case 'signin':
			serverPage='Default.aspx';
			var username = document.getElementById('username').value;
			var password = document.getElementById('password').value;
			var weblog = document.getElementById('weblog').value;
			var cookieEnabled = document.getElementById('cookieEnabled').checked;
			if(username == '')
			{
				alert('.نام کاربری خالی است');
				document.getElementById('username').focus();
				return false;
			}
			if(password == '')
			{
				alert('.کلمه عبور خالی است');
				document.getElementById('password').focus();
				return false;
			}
			break;
		default:
			return;
	}
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';
//	document.getElementById('post').disabled=true;

	
	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logined')
				{
					window.location = 'http://www.iranblog.com/services/blogbuilderv1/';
					return false;
				}
				else
				{
//					document.getElementById('post').disabled=false;
					document.getElementById('message').innerHTML='';
					document.getElementById('message').style.display = 'none';
					alert(xmlHttp.responseText);
	
					return false;
				}
			}
		}
		var parameters = '';
		switch(_mode)
		{
			case 'signin':
				parameters = 'mode=signin&username='+encodeURIComponent(username)+'&password='+encodeURIComponent(password) +'&weblog='+encodeURIComponent(weblog)+'&cookieEnabled='+encodeURIComponent(cookieEnabled);
				break;
			default:
				break;
		}
		xmlHttp.open("POST",serverPage,true);
 		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
      	xmlHttp.setRequestHeader("Content-length", parameters.length);
	    xmlHttp.send(parameters);
		return false;

	}
}
//------------------------------------------------------------------------------------------------------------------
function ShowItems(_page, _mode)
{
	var serverPage='';
	switch(_mode)
	{
		case 'ShowUpdates':
			serverPage='/services/';
			break;
		default:
			break;
	}
//	document.getElementById('loaderImg').innerHTML = loaderUI;
	document.getElementById('loaderImg').style.display = 'block';
	
	document.getElementById('resultText').innerHTML = '';
	
	var _xmlHttp = null;
	if((_xmlHttp = createAjaxObject()) != null){
		_xmlHttp.onreadystatechange=function()
		{
			if(_xmlHttp.readyState==4)
			{
				if(_xmlHttp.responseText == 'Logouted')
				{
					window.location = 'Logouted.aspx';
					return;
				}
				if(_xmlHttp.responseText == 'DoRefresh')
				{
					ShowItems('1', _mode);
					return;
				}
//				document.getElementById('loaderImg').innerHTML = "";
				document.getElementById('loaderImg').style.display = 'none';
				if(_xmlHttp.responseText == 'NoFoundPost')
				{
					switch(_mode)
					{
						case 'ShowUpdates':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>پستی وجود ندارد.</div>";
							return;
						default:
							return;
					}
					return;
				}
				document.getElementById('resultText').innerHTML=_xmlHttp.responseText;
				/*switch(_mode)
				{
					case 'ShowPostAdmin':
						GenerateContextMenu(FindContextMenuIDsSection(_xmlHttp.responseText));
						break;
					default:
						break;
				}*/
			}
		}
		_xmlHttp.open("POST",serverPage,true);
		_xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		switch(_mode)
		{
			default:
				_xmlHttp.send('mode=' + _mode + '&page=' + _page);
				break;
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function MakeTeamWeblogSection(_isAdd)
{
	if(_isAdd)
	{
		document.getElementById('TeamWeblogSection').style.display = "block";
		document.getElementById('removetb').style.display = "block";
		document.getElementById('addtb').style.display = "none";
		document.getElementById('weblog').focus();
	}
	else
	{
		document.getElementById('TeamWeblogSection').style.display = "none";
		document.getElementById('removetb').style.display = "none";
		document.getElementById('addtb').style.display = "block";
		document.getElementById('username').focus();
		document.getElementById('weblog').value = '';
	}
}
//------------------------------------------------------------------------------------------------------------------
function DoLoginByEnterCapture()
{
	if(window.event.keyCode == 13)
	{
		DoPost('signin');
		return;
	}
}
//------------------------------------------------------------------------------------------------------------------
function LoginFormValidation()
{	
	var username = document.getElementById('username').value;
	var password = document.getElementById('password').value;

	if(username == '')
	{
		alert('.نام کاربری خالی است');
		document.getElementById('username').focus();
		document.getElementById('username').select();
		return false;
	}
	var re = /^[\-0-9a-zA-Z]{1,}$/;
	if(!re.test(username))
	{
	   	alert(".نام کاربری نامعتبر است");
	   	document.getElementById('username').focus();
		document.getElementById('username').select();
	   	return false;
	}
	if(password == '')
	{
		alert('.کلمه عبور خالی است');
		document.getElementById('password').focus();
		document.getElementById('password').select();
		return false;
	}
	return true;
}
//------------------------------------------------------------------------------------------------------------------