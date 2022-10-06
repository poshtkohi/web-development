/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//Functions.js
//Version: 2.1
//This script is created by Alireza Poshtkohi. Do not remove, modify, or hide the author information. keep it intact.
//Mail: alireza.poshtkohi@gmail.com 

//-----------------------------global variables---------------------------------------------------------------------
var _isInUpdateMode = false;
var _updateID = -1;
var _editorIsDefined = false;
var _currentNewsGroupID = '-1';
var _tempNewsGroupID = '-1';
var _SelectForNewsGroupMode = '';

var langFarsi = 1;
var farsikey = [  
	0x0020, 0x0021, 0x061B, 0x066B, 0x00A4, 0x066A, 0x060C, 0x06AF,      
	0x0029, 0x0028, 0x002A, 0x002B, 0x0648, 0x002D, 0x002E, 0x002F,      
	0x06F0, 0x06F1, 0x06F2, 0x06F3, 0x06F4, 0x06F5, 0x06F6, 0x06F7,      
	0x06F8, 0x06F9, 0x003A, 0x06A9, 0x003E, 0x003D, 0x003C, 0x061F,      
	0x066C, 0x0624, 0x200C, 0x0698, 0x064A, 0x064D, 0x0625, 0x0623,      
	0x0622, 0x0651, 0x0629, 0x00BB, 0x00AB, 0x0621, 0x004E, 0x005D,      
	0x005B, 0x0652, 0x064B, 0x0626, 0x064F, 0x064E, 0x0056, 0x064C,            
	0x0058, 0x0650, 0x0643, 0x062C, 0x0698, 0x0686, 0x00D7, 0x0640,            
	0x067E, 0x0634, 0x0630, 0x0632, 0x06CC, 0x062B, 0x0628, 0x0644,            
	0x0627, 0x0647, 0x062A, 0x0646, 0x0645, 0x0626, 0x062F, 0x062E,            
	0x062D, 0x0636, 0x0642, 0x0633, 0x0641, 0x0639, 0x0631, 0x0635,            
	0x0637, 0x063A, 0x0638, 0x007D, 0x007C, 0x007B, 0x007E            
];  
//------------------------------------------------------------------------------------------------------------------
function DoPost(_mode)
{	
	var serverPage='';
	switch(_mode)
	{
		case 'users':
			serverPage='users.aspx';
			var username = document.getElementById('username').value;
			var password = document.getElementById('password').value;
			var ConfirmPassword = document.getElementById('ConfirmPassword').value;
			var AccountType = document.getElementById('AccountType').value;
			if(_isInUpdateMode)
				document.getElementById('username').disabled = true;
			if(!_isInUpdateMode)
			{
				if(username == '')
				{
					alert('.نام کاربری خالی است');
					return ;
				}
				var re = /^[\-0-9a-zA-Z]{1,}$/;
				if(!re.test(username))
				{
				   alert(".نام کاربری نامعتبر است");
				   return ;
				}
				if(password == '')
				{
					alert('.کلمه عبور خالی است');
					return ;
				}
				if(password != ConfirmPassword)
				{
					alert('.کلمه عبور با تکرار کلمه عبور برابر نیست');
					return ;
				}
			}
			else
			{
				if(password != '')
				{
					if(password != ConfirmPassword)
					{
						alert('.کلمه عبور با تکرار کلمه عبور برابر نیست');
						return ;
					}
				}
			}
			if(AccountType == 'none')
			{
				alert('.سطح دسترسی را انتخاب کنید');
				return ;
			}
			break;
		case 'NewsGroups':
			serverPage='NewsGroups.aspx';
			var title = document.getElementById('title').value;
			if(title == '')
			{
				alert('.عنوان خالی است');
				return ;
			}
			break;
		case 'NewsAdmin':
			serverPage='NewsAdmin.aspx';
			var PostTitle = document.getElementById('title').value;
			var PostContent = tinyMCE.get('content').getContent();
			var NewsSubject = document.getElementById('NewsSubject').value;
			if(PostTitle == '')
			{
				alert('.عنوان خبر خالی است');
				return ;
			}
			if(PostContent == '')
			{
				alert('.متن خبر خالی است');
				return ;
			}
			break;
		case 'NewsAdminForNewsGroupsMode':
			serverPage='NewsAdmin.aspx';
			var PostTitle = document.getElementById('title').value;
			var PostContent = tinyMCE.get('content').getContent();
			var NewsSubject = document.getElementById('NewsSubject').value;
			if(PostTitle == '')
			{
				alert('.عنوان خبر خالی است');
				return ;
			}
			if(PostContent == '')
			{
				alert('.متن خبر خالی است');
				return ;
			}
			break;
		default:
			return;
	}
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';
	document.getElementById('post').disabled=true;

	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = '/Login.aspx?mode=logouted';
					return;
				}
				/*if(IsInEditMode())
				{
					ResetUpdateMode();
				}*/
				document.getElementById('post').disabled=false;
				document.getElementById('message').innerHTML='';
				document.getElementById('message').style.display = 'none';
				if(xmlHttp.responseText == 'UserExisted')
				{
					alert('.نام کاربری وجود دارد. نام دیگری انتخاب کنید');
					return;
				}
				alert(xmlHttp.responseText);
				switch(_mode)
				{
					case 'users':
						ShowItems('1', 'ShowUsers');
						break;
					case 'NewsGroups':
						ShowItems('1', 'ShowNewsGroups');
						break;
					case 'NewsAdmin':
						if(_tempNewsGroupID != '-1')
						{
							SelectForNewsGroup(_SelectForNewsGroupMode, _tempNewsGroupID, _updateID);
							break;
						}
						ShowItems('1', 'ShowNewsAdmin');
						break;
					case 'NewsAdminForNewsGroupsMode':
						if(_tempNewsGroupID != '-1')
						{
							SelectForNewsGroup(_SelectForNewsGroupMode, _tempNewsGroupID, _updateID);
							break;
						}
						ShowItems(_currentNewsGroupID, 'ShowNewsGroupsShow');
						break;
					default:
						break;
				}
				if(_isInUpdateMode || _editorIsDefined)
					Cancel();
				else
					if(!_editorIsDefined)
						New(_mode);

				return;
			}
		}
		var __mode = 'mode=post';
		if(_isInUpdateMode)
		{
			switch(_mode)
			{
				case 'NewsAdminForNewsGroupsMode':
					__mode = 'mode=UpdateForNewsGroupsMode&id=' + _updateID;
					break;
				default:
					__mode = 'mode=update&id=' + _updateID;
					break;
			}
		}
		var parameters = '';
		switch(_mode)
		{
			case 'users':
				parameters = __mode + '&username='+encodeURIComponent(username)+'&password='+encodeURIComponent(password) +'&AccountType='+encodeURIComponent(AccountType);
				break;
			case 'NewsGroups':
				parameters = __mode + '&title='+encodeURIComponent(title);
				break;
			case 'NewsAdmin':
				parameters = __mode + '&PostTitle='+encodeURIComponent(PostTitle)+'&PostContent='+encodeURIComponent(PostContent) +'&NewsSubject='+encodeURIComponent(NewsSubject);
				break;
			case 'NewsAdminForNewsGroupsMode':
				parameters = __mode + '&PostTitle='+encodeURIComponent(PostTitle)+'&PostContent='+encodeURIComponent(PostContent) +'&NewsSubject='+encodeURIComponent(NewsSubject);
				break;
			default:
				break;
		}
		xmlHttp.open("POST",serverPage,true);
 		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
      	xmlHttp.setRequestHeader("Content-length", parameters.length);
	    xmlHttp.send(parameters);

	}
}
//------------------------------------------------------------------------------------------------------------------
function ShowItems(_page, _mode)
{
	var serverPage='';
	switch(_mode)
	{
		case 'ShowUsers':
			serverPage='users.aspx';
			break;
		case 'ShowNewsGroups':
			serverPage='NewsGroups.aspx';
			break;
		case 'ShowNewsAdmin':
			serverPage='NewsAdmin.aspx';
			break;
		case 'SearchByDate':
			serverPage='NewsAdmin.aspx';
			var SearchDate = document.getElementById('SearchDate').value;
			if(SearchDate == '')
			{
				alert('.یک تاریخ انتخاب کنید');
				return ;
			}
			break;
		case 'SearchByText':
			serverPage='NewsAdmin.aspx';
			var SearchText = document.getElementById('SearchText').value;
			var StartDate = document.getElementById('StartDate').value;
			var EndDate = document.getElementById('EndDate').value;
			if(StartDate != '')
			{
				if(EndDate == '')
				{
					alert('.برای فیلد تا تاریخ یک تاریخ انتخاب کنید');
					return ;
				}
			}
			else
				if(SearchText == '')
				{
					alert('.عبارت جستجو خالی است');
					return ;
				}
			
			break;
		case 'ShowNewsAdminForNewsGroupsMode':
			serverPage='NewsAdmin.aspx';
			break;
		case 'ShowNewsGroupsShow':
			serverPage='NewsGroupsShow.aspx';
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
					window.location = '/Login.aspx?mode=logouted';
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
						case 'ShowUsers':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کاربری وجود ندارد.</div>";
							return;
						case 'ShowNewsGroups':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>گروه خبری وجود ندارد.</div>";
							return;
						case 'ShowNewsAdmin':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>خبری وجود ندارد.</div>";
							return;
						case 'SearchByDate':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>خبری بر اساس تاریخ جستجو وجود ندارد.</div>";
							return;
						case 'SearchByText':
							if(SearchText == '')
								document.getElementById('resultText').innerHTML = "<br><div class='message'>خبری بر اساس عبارت جستجو و بازه زمانی وجود ندارد.</div>";
							else
								document.getElementById('resultText').innerHTML = "<br><div class='message'>خبری بر اساس بازه زمانی جستجو وجود ندارد.</div>";
							return;
						case 'ShowNewsAdminForNewsGroupsMode':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>خبری برای این بخش خبری وجود ندارد.</div>";
							return;
						case 'ShowNewsGroupsShow':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>گروه خبری وجود ندارد.</div>";
							return;
						default:
							return;
					}
					return;
				}
				document.getElementById('resultText').innerHTML=_xmlHttp.responseText;
				switch(_mode)
				{
					case 'ShowNewsAdmin':
						GenerateContextMenu(FindContextMenuIDsSection(_xmlHttp.responseText));
						break;
					case 'ShowNewsAdminForNewsGroupsMode':
						GenerateContextMenu(FindContextMenuIDsSection(_xmlHttp.responseText));
						break;
					case 'SearchByDate':
						GenerateContextMenu(FindContextMenuIDsSection(_xmlHttp.responseText));
						break;
					case 'SearchByText':
						GenerateContextMenu(FindContextMenuIDsSection(_xmlHttp.responseText));
						break;
					default:
						break;
				}
			}
		}
		_xmlHttp.open("POST",serverPage,true);
		_xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		switch(_mode)
		{
			case 'SearchByDate':
				var parameters = 'mode=' + _mode + '&page=' + _page + '&SearchDate=' + encodeURIComponent(SearchDate);
				_xmlHttp.setRequestHeader("Content-length", parameters.length);
				_xmlHttp.send(parameters);
				ShowSearchLayer(false);
				break;
			case 'SearchByText':
				var parameters = 'mode=' + _mode + '&page=' + _page + '&SearchText=' + encodeURIComponent(SearchText) + '&StartDate=' + encodeURIComponent(StartDate) + '&EndDate=' + encodeURIComponent(EndDate);
				_xmlHttp.setRequestHeader("Content-length", parameters.length);
				_xmlHttp.send(parameters);
				ShowSearchLayer(false);
				break;
			default:
				_xmlHttp.send('mode=' + _mode + '&page=' + _page);
				break;
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function ItemDelete(_mode,id)
{
	if (confirm('آیا شما مطمئن هستید؟'))
	{
		var serverPage='';
		switch(_mode)
		{
			case 'users':
				serverPage='users.aspx';
				break;
			case 'NewsGroups':
				serverPage='NewsGroups.aspx';
				break;
			case 'NewsAdmin':
				serverPage='NewsAdmin.aspx';
			case 'NewsAdminForNewsGroupsMode':
				serverPage='NewsAdmin.aspx';
				break;
			default:
				break;
		}
     	document.getElementById('loaderImg').innerHTML = loaderUI;
		document.getElementById('loaderImg').style.display = 'block';
		
		var xmlHttp = null;
		if((xmlHttp = createAjaxObject()) != null){
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = '/Login.aspx?mode=logouted';
						return;
					}
					document.getElementById('loaderImg').innerHTML = loaderUI;
					document.getElementById('loaderImg').style.display = 'none';
					if(xmlHttp.responseText == 'Success')
					{
						alert('.عملیات حذف با موفقیت انجام شد');
						var _m = '';
						switch(_mode)
						{
							case 'users':
								_m='ShowUsers';
								break;
							case 'NewsGroups':
								_m='ShowNewsGroups';
								break;
							case 'NewsAdmin':
								_m='ShowNewsAdmin';
								break;
							case 'NewsAdminForNewsGroupsMode':
								_m='ShowNewsAdminForNewsGroupsMode';
								ShowItems(_currentNewsGroupID, _m);
								return;
							default:
								return;
						}
						ShowItems('1', _m);
						return;
					}
					else
					{
						alert('.خطایی در عملیات حذف انتخاب شده رخ داد');
						return;
					}
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			switch(_mode)
			{
				case 'NewsAdminForNewsGroupsMode':
					xmlHttp.send('mode=DeleteForNewsGroupsMode&DeleteID='+id);
					return;
				default:
					xmlHttp.send('mode=delete&DeleteID='+id);
					return;
			}
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function ItemLoad(_mode,id)
{
	var serverPage='';
	switch(_mode)
	{
		case 'users':
			serverPage='users.aspx';
			document.getElementById('username').disabled = true;
			break;
		case 'NewsGroups':
			serverPage='NewsGroups.aspx';
			break;
		case 'NewsAdmin':
			serverPage='NewsAdmin.aspx';
			break;
		case 'NewsAdminForNewsGroupsMode':
			serverPage='NewsAdmin.aspx';
			break;
		case 'SelectForNewsGroupNewsAdminMode':
			serverPage='NewsAdmin.aspx';
			var IDs = id.split(',');
			_tempNewsGroupID = IDs[0];
			id = IDs[1];
			_SelectForNewsGroupMode = _mode;
			break;
		case 'SelectForNewsGroupNewsAdminInNewsGropMode':
			serverPage='NewsAdmin.aspx';
			var IDs = id.split(',');
			_tempNewsGroupID = IDs[0];
			id = IDs[1];
			_SelectForNewsGroupMode = _mode;
			break;//_tempNewsGroupID =>> NewsGroupsID,[PostID]
		default:
			break;
	}	
		
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';

	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = '/Login.aspx?mode=logouted';
					return;
				}
				document.getElementById('message').style.display = 'block';
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById('message').style.display = 'block';
					document.getElementById('message').innerHTML = '';
					switch(_mode)
					{
						case 'users':
							alert('این کاربر قبلا حذف شده است.');
							break;
						case 'NewsGroups':
							alert('این بخش خبری قبلا حذف شده است.');
							break;
						case 'NewsAdmin':
							alert('این خبر قبلا حذف شده است.');
							break;
						case 'NewsAdminForNewsGroupsMode':
							alert('این خبر قبلا از این بخش خبری حذف شده است.');
							break;
						case 'SelectForNewsGroupNewsAdminMode':
							alert('این خبر قبلا حذف شده است.');
							break;
						case 'SelectForNewsGroupNewsAdminInNewsGropMode':
							alert('این خبر قبلا از این بخش خبری حذف شده است.');
							break;
						default:
							break;
					}	
					return;
				}

				_isInUpdateMode = true;
				_updateID = id;
				document.getElementById('up').style.display = "block";
				document.getElementById('cancel').style.display = "block";
				document.getElementById('new').style.display = "none";
				document.getElementById('message').innerHTML='هم اکنون می توانید به ویرایش آیتم انتخاب شده بپردازید.';
				document.getElementById('message').style.display = 'block';
				
				var str = xmlHttp.responseText;
				var ret = str.split(',');
				switch(_mode)
				{
					case 'users':
						document.getElementById('username').innerText = Base64.decode(ret[0]);
						document.getElementById('AccountType').selectedIndex = parseInt(Base64.decode(ret[1]));
						break;
					case 'NewsGroups':
						document.getElementById('title').innerText = Base64.decode(ret[0]);
						break;
					case 'NewsAdmin':
						document.getElementById('title').innerText = Base64.decode(ret[0]);
						if(_editorIsDefined)
							tinyMCE.get('content').setContent(Base64.decode(ret[1]));
						document.getElementById('NewsSubject').selectedIndex = parseInt(Base64.decode(ret[2]))-1;
						break;
					case 'NewsAdminForNewsGroupsMode':
						document.getElementById('title').innerText = Base64.decode(ret[0]);
						if(_editorIsDefined)
							tinyMCE.get('content').setContent(Base64.decode(ret[1]));
						document.getElementById('NewsSubject').selectedIndex = parseInt(Base64.decode(ret[2]))-1;
					case 'SelectForNewsGroupNewsAdminMode':
						document.getElementById('title').innerText = Base64.decode(ret[0]);
						if(_editorIsDefined)
							tinyMCE.get('content').setContent(Base64.decode(ret[1]));
						document.getElementById('NewsSubject').selectedIndex = parseInt(Base64.decode(ret[2]))-1;
						break;
					case 'SelectForNewsGroupNewsAdminInNewsGropMode':
						document.getElementById('title').innerText = Base64.decode(ret[0]);
						if(_editorIsDefined)
							tinyMCE.get('content').setContent(Base64.decode(ret[1]));
						document.getElementById('NewsSubject').selectedIndex = parseInt(Base64.decode(ret[2]))-1;
					default:
						break;
				}	
				return;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
					switch(_mode)
			{
				case 'NewsAdminForNewsGroupsMode':
					xmlHttp.send('mode=LoadForNewsGroupsMode&id='+id);
					return;
				case 'SelectForNewsGroupNewsAdminInNewsGropMode':
					xmlHttp.send('mode=LoadForNewsGroupsMode&id='+id);
					return;
				default:
					xmlHttp.send('mode=load&id='+id);
					return;
			}
	}
}
//------------------------------------------------------------------------------------------------------------------
function Print(mode,PostID,action)
{
	var serverPage = '/NewsAdmin.aspx?action=' + action + '&mode=' + mode + '&id=' + PostID;
	var left = (screen.width/2)-(900/2);
	var top = (screen.height/2)-(760/2);
	var _window = window.open(serverPage, null, 'width=900, height=760, scrollbars=yes, resizable=yes, left=' + left + ', top=' + top);	
	//	_window.moveTo(top, lef);
	return;
}
//------------------------------------------------------------------------------------------------------------------
function Cancel()
{
	document.getElementById('message').style.display = 'none';
	document.getElementById('message').innerHTML = '';
	document.getElementById('up').style.display = "none";
	document.getElementById('cancel').style.display = "none";
	if(_currentNewsGroupID == '-1')
		document.getElementById('new').style.display = "block";
	_tempNewsGroupID = '-1';
	_SelectForNewsGroupMode = '';
	return ;
}
//------------------------------------------------------------------------------------------------------------------
function New(_mode)
{
	_isInUpdateMode = false;
	_updateID = -1;
	_currentNewsGroupID = '-1';
	_tempNewsGroupID = '-1';
	_SelectForNewsGroupMode = '';
	if(!document.getElementById )
		return;  
	var f = document.getElementById('form');
	for( var i = 0; i < f.elements.length; i++ )  
	{    
		f.elements[i].disabled = false;
		if(f.elements[i].type == 'select-one')
			f.elements[i].selectedIndex = 0;
		else
			f.elements[i].value = '';
	} 
	if(_editorIsDefined)
		tinyMCE.get('content').setContent('');
	document.getElementById('up').style.display = "block";
	document.getElementById('cancel').style.display = "block";
	document.getElementById('new').style.display = "none";
	
	return ;
}
//------------------------------------------------------------------------------------------------------------------
function SelectForNewsGroup(mode, NewsGroupID, PostID)
{
	/*if (confirm('آیا شما علاوه بر ذخیره این خبر مایل به انتخاب آن برای بخش خبری مورد نظرتان هستید؟'))
	{*/
		var serverPage='NewsAdmin.aspx';
//		alert("post: "+PostID + ";group:"+NewsGroupID);return ;

     	document.getElementById('loaderImg').innerHTML = loaderUI;
		document.getElementById('loaderImg').style.display = 'block';
		
		var xmlHttp = null;
		if((xmlHttp = createAjaxObject()) != null){
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = '/Login.aspx?mode=logouted';
						return;
					}
					document.getElementById('loaderImg').innerHTML = loaderUI;
					document.getElementById('loaderImg').style.display = 'none';
					if(xmlHttp.responseText == 'Success')
					{
						alert('.خبر مربوطه با موفقیت برای گروه خبری انتخاب شده، ذخیره و انتخاب شد');
						switch(mode)
						{
							case 'SelectForNewsGroupNewsAdminMode':
								ShowItems('1', 'ShowNewsAdmin');
								break;
							case 'SelectForNewsGroupNewsAdminInNewsGropMode':
								break;
							default:
								break;
						}	
						return;
					}
					if(xmlHttp.responseText == 'Existed')
					{
						alert('.این خبر قبلا برای این بخش خبری انتخاب شده است. خبر دیگری را انتخاب کنید');
						return;
					}
					else
					{
//						alert(xmlHttp.responseText); return;
						alert('.خطایی در عملیات حذف انتخاب شده رخ داد');
						return;
					}
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			xmlHttp.send('mode='+mode+'&NewsGroupID='+NewsGroupID+'&PostID='+PostID);
		}
	/*}*/
}
//------------------------------------------------------------------------------------------------------------------
function tinyMCEInit()
{	       
	_editorIsDefined = true;
	
	tinyMCE_GZ.init({
    theme : "advanced",
	skin : "o2k7",
	plugins : "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,inlinepopups",
	languages : 'en',
	disk_cache : true,
	debug : false
});

	tinyMCE.init({
	// O2k7 skin
    mode : "textareas",
    theme : "advanced",
	skin : "o2k7",
	plugins : "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,inlinepopups",

	// Theme options
	theme_advanced_buttons1 : "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect,language",
	theme_advanced_buttons2 : "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
	theme_advanced_buttons3 : "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
	theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
	theme_advanced_toolbar_location : "top",
	theme_advanced_toolbar_align : "left",
	theme_advanced_statusbar_location : "bottom",
	theme_advanced_resizing : false,

	// Example content CSS (should be your site CSS)
	content_css : "examples/css/content.css",

	// Drop lists for link/image/media/template dialogs
	template_external_list_url : "lists/template_list.js",
	external_link_list_url : "lists/link_list.js",
	external_image_list_url : "lists/image_list.js",
	media_external_list_url : "lists/media_list.js",
    setup : function(ed) {	
	
	
	/*ed.onKeyPress.add(function(ed, e){		   
				   var key = ed.getWin().event.keyCode;
				
				  if (key == 13) { ed.getWin().event.keyCode = 13; return true; }
				
				   if (langFarsi==1) { // If Farsi
					 if (key == 0x0020 && ed.getWin().event.shiftKey) // Shift-space -> ZWNJ
					   ed.getWin().event.keyCode = 0x200C;
					 else
					   ed.getWin().event.keyCode = farsikey[key - 0x0020];
					 if (farsikey[key - 0x0020] == 92) {
						ed.getWin().event.keyCode = 0x0698;
					 }
					 if (farsikey[key - 0x0020] == 8205) {
						ed.getWin().event.keyCode = 0x067E;
					 }
				   }
				   return true;

	        });
	
			ed.onKeyDown.add(function(ed, e){		   
				 var key = ed.getWin().event.keyCode;
				 if (key == 145){
					if (langFarsi==0) {
					  langFarsi = 1;
					  return true;
					}
					else {
					  langFarsi = 0;
					  return true;
					}
				
				}

	        });*/
			
		/*ed.onKeyPress.add(function(ed, e){		   
			   var key = ed.getWin().event.keyCode;
			   if (key < 0x0020 || key >= 0x00FF)
				  return;
			   if (langFarsi) {
				  if (key == 0x0020 && ed.getWin().event.shiftKey)
					 ed.getWin().event.keyCode = 0x200C;
				  else
					 ed.getWin().event.keyCode = farsikey[key - 0x0020];
			   }
	        });
		
		ed.onKeyDown.add(function(ed, e){		   
				 var key = ed.getWin().event.keyCode;
				 if (key == 145){
					if (!langFarsi) {
					  langFarsi = true;
					  return true;
					}
					else {
					  langFarsi = false;
					  return true;
					}
				
				}

	        });*/
		
		
		    /*ed.addButton('language', {
				title : 'انتخاب زبان',
				image : 'jscripts/tiny_mce/plugins/example/img/example.gif',
				onclick : function() {
				   changeLang();
				}
        	});			*/
    }
});

}
function changeLang() {
    if (langFarsi==0) {
    langFarsi = 1;
    return true;
  }
  else {
    langFarsi = 0;
    return true;
  }
}

function FKeyDown (txtFrm){
 var key = window.event.keyCode;
 if (key == 145){
    if (langFarsi == 0) {
      langFarsi = 1;
      return true;
    }
    else {
      langFarsi = 0;
      return true;
    }

}

}
function FKeyPress(txtFrm) {
   var key = window.event.keyCode;

  if (key == 13) { window.event.keyCode = 13; return true; }

   if (langFarsi == 1) { // If Farsi
     if (key == 0x0020 && window.event.shiftKey) // Shift-space -> ZWNJ
       window.event.keyCode = 0x200C;
     else
       window.event.keyCode = farsikey[key - 0x0020];
     if (farsikey[key - 0x0020] == 92) {
        window.event.keyCode = 0x0698;
     }
     if (farsikey[key - 0x0020] == 8205) {
        window.event.keyCode = 0x067E;
     }
   }
   return true;
}

//------------------------------------------------------------------------------------------------------------------
function GenerateContextMenu(IDs)
{
	for(var i = 0 ; i < IDs.length ; i++)
        SimpleContextMenu.attach('container' + IDs[i], 'CM' +  IDs[i]);
}
//------------------------------------------------------------------------------------------------------------------
function FindContextMenuIDsSection(buffer)
{
	//var buffer = "<!--xxx'28','27','26','25','24','23','22'xxx-->";
	var _p1 = buffer.indexOf('xxxx', 0) + 'xxxx'.length;
	var _p2 = buffer.indexOf('xxxx', _p1);
	if(_p1 >= 0 && _p2 > _p1)
		return buffer.substr(_p1, _p2 - _p1).split(',');
	else
		return null;
}
//------------------------------------------------------------------------------------------------------------------
function ShowSearchLayer(isShow)
{
	if(isShow)
	{
		Cancel();
		document.body.className='bodyInvis';
		document.getElementById('MainDiv').style.visibility='hidden';
		document.getElementById('SearchLayer').style.visibility='visible';
	}
	else
	{
		document.body.className='body';
		document.getElementById('MainDiv').style.visibility='visible';
		document.getElementById('SearchLayer').style.visibility='hidden';
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