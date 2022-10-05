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
	document.getElementById('loaderImg').innerHTML = loaderUI;
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
				document.getElementById('loaderImg').innerHTML = "";
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