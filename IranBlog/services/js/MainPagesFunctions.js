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
var _isPassForgetLayerFirstLoad = true;
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
		case 'PassForget':
			serverPage='Default.aspx';
			var username_fp = document.getElementById('username_fp').value;
			var email_fp = document.getElementById('email_fp').value;
			var re = /^[\-0-9a-zA-Z]{1,}$/
			if(username_fp == '')
			{
				alert('.نام کاربری خالی است');
				document.getElementById('username_fp').focus();
				return false;
			}
			if(email_fp == '')
			{
				alert('.ایمیل خالی است');
				document.getElementById('email_fp').focus();
				return false;
			}
			if(!re.test(username_fp))
			{
				document.getElementById('username_fp').focus();
				alert(".حروف ایمیل نا معتبر است");
				return;
			}
			re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
			if(!re.test(email_fp))
			{
				alert(".حروف ایمیل نا معتبر است");
				document.getElementById('email_fp').focus();
				return;
			}
			break;
		default:
			return;
	}
//	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('loaderImg_post').style.display = 'block';
	document.getElementById('post').disabled=true;

	
	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logined')
				{
					window.location = 'services/blogbuilderv1/';
					return false;
				}
				else
				{
					document.getElementById('loaderImg_post').style.display = 'none';
					switch(_mode)
					{
						case 'PassForget':
							if(xmlHttp.responseText == 'Success')
							{
								alert('.کلمه عبورتان به آدرس ایمیلتان ارسال شد');
								document.getElementById('username_fp').value='';
								document.getElementById('email_fp').value='';
								ShowPassForgetLayer(false);
							}
							else
							{
								document.getElementById('post').disabled=false;
								alert(xmlHttp.responseText);
							}
							break;
						default:
							document.getElementById('post').disabled=false;
							alert(xmlHttp.responseText);
							break;
					}
	
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
			case 'PassForget':
				parameters = 'mode=PassForget&username_fp='+encodeURIComponent(username_fp)+'&email_fp='+encodeURIComponent(email_fp);
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
function ShowPassForgetLayer(isShow)
{
	if(_isPassForgetLayerFirstLoad)
	{
		var _xmlHttp = null;
		if((_xmlHttp = createAjaxObject()) != null){
			_xmlHttp.onreadystatechange=function()
			{
				if(_xmlHttp.readyState==4)
				{
					document.getElementById('ForgetPassLayer').innerHTML=_xmlHttp.responseText;
					_isPassForgetLayerFirstLoad = false;
					centerView('ForgetPassLayer');
					ShowPassForgetLayer(true);
				}
			}
			_xmlHttp.open("GET",'/services/blogbuilderv1/AjaxTemplates/ForgetPassTemplate.html',true);
			_xmlHttp.send(null);
		}
	}
	else
	{
		document.getElementById('post').disabled=false;
		if(isShow)
		{
			document.body.className='bodyInvis';
			document.getElementById('MainDiv').style.visibility='hidden';
			document.getElementById('ForgetPassLayer').style.visibility='visible';
		}
		else
		{
			document.body.className='body';
			document.getElementById('MainDiv').style.visibility='visible';
			document.getElementById('ForgetPassLayer').style.visibility='hidden';
		}
	}
}
function centerView(layer/*no display:none*/, doNotAddOffsets){
	if(typeof layer=="string"){layer=document.getElementById(layer);};
	if(layer){
	var parent=layer.parentNode;/*unless body tag, must have position to relative or absolute*/
	parent.style.overflow="auto";
	layer.style.position="absolute";/*much better if top and left are specified in style, with 'px'*/
	layer.style.top=layer.style.top||layer.offsetTop+'px';
	layer.style.left=layer.style.left||layer.offsetLeft+'px';
	var clientH=0, clientW=0, offsetT=0, offsetL=0, top=0, left=0;
		if(parent && parent.nodeType==1/*a tag*/){
			if(parent.nodeName=="BODY"){
				if(typeof window.innerHeight!="undefined"){clientH=window.innerHeight; clientW=window.innerWidth;}
				else if(document.documentElement && document.documentElement.clientHeight){clientH=document.documentElement.clientHeight; clientW=document.documentElement.clientWidth;}
				else if(document.body.clientHeight){clientH=document.body.clientHeight; clientW=document.body.clientWidth;}
				else{clientH=parent.clientHeight; clientW=parent.clientWidth;};
				//
				if(typeof pageYOffset!="undefined"){offsetT=pageYOffset; offsetL=pageXOffset;}
				else if(document.documentElement && document.documentElement.scrollTop){offsetT=document.documentElement.scrollTop; offsetL=document.documentElement.scrollLeft;}
				else if(document.body && typeof document.body.scrollTop!="undefined"){offsetT=document.body.scrollTop; offsetL=document.body.scrollLeft;}
				else{offsetT=0; offsetL=0;};
			top=Math.abs(parent.offsetTop + ((clientH/2) - (layer.offsetHeight/2)));
			left=Math.abs(parent.offsetLeft + ((clientW/2) - (layer.offsetWidth/2)));
			}
			else{
			clientH=parent.offsetHeight; clientW=parent.offsetWidth;
			offsetT=parent.scrollTop; offsetL=parent.scrollLeft;
			top=Math.abs(((clientH/2) - (layer.offsetHeight/2))); left=Math.abs(((clientW/2) - (layer.offsetWidth/2)));
			};
		if(!doNotAddOffsets){top+=offsetT; left+=offsetL;};
		layer.style.top=top+'px';//comment out to avoid positioning and allow returning only
		layer.style.left=left+'px';//comment out to avoid positioning and allow returning only
		return [top, left, top+'px', left+'px'];
		};
	};
}
//------------------------------------------------------------------------------------------------------------------