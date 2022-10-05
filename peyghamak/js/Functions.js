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
//var _editorIsDefined = false;
var _editorIsDefined = true;
var _currentPostID = '-1';
var _currentPersonID = '-1';
var _tabImagesRoot = 'http://www.peyghamak.com/theme/images/green/';
var _logoutedURL = 'http://www.peyghamak.com/signin.aspx';
var rightAlign=true;

var langFarsi = true;
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
		case 'my':
			serverPage='my.aspx';
			var PostContent = document.getElementById('PostContent').value;
			var _LanguageType = LanguageType();
			var _PostAlign = PostAlign();
			if(PostContent == '')
			{
				alert('.متن ارسالی خالی است');
				return ;
			}
			break;
		case 'comments':
			serverPage='comments.aspx';
			var PostContent = document.getElementById('message').value;
			var _LanguageType = LanguageType();
			var _PostAlign = PostAlign();
			if(PostContent == '')
			{
				alert('.متن ارسالی خالی است');
				return ;
			}
			break;
		case 'message':
			serverPage='message.aspx';
			var PostContent = document.getElementById('message').value;
			var _LanguageType = LanguageType();
			var _PostAlign = PostAlign();
			if(PostContent == '')
			{
				alert('.متن ارسالی خالی است');
				return ;
			}
			break;
		default:
			return;
	}
	document.getElementById('loaderImg').innerHTML = loaderUI;
	document.getElementById('loaderImg').style.display = 'block';
	document.getElementById('dopost').disabled=true;

	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = _logoutedURL;
					return;
				}
				document.getElementById('dopost').disabled=false;
				ShowMessageBox('',false);
				if(xmlHttp.responseText == 'Success')
				{
					document.getElementById('loaderImg').innerHTML = '';
					document.getElementById('loaderImg').style.display = 'none';
					switch(_mode)
					{
						case 'my':
							document.getElementById("PostNumLabel").innerHTML = parseInt(document.getElementById("PostNumLabel").innerHTML)+1;
							document.getElementById("counter").innerHTML = '450';
							document.getElementById('dopost').disabled=false;
							document.getElementById("PostContent").value = '';
							ShowMessageBox('متن شما با موفقیت ارسال شد.',true);
							ShowItems('1', 'ShowMyPosts');
							return;
						case 'comments':
							document.getElementById("NumCommentsLable").innerHTML = parseInt(document.getElementById("NumCommentsLable").innerHTML)+1;
							document.getElementById("commentCounter").innerHTML = '450';
							document.getElementById('dopost').disabled=false;
							document.getElementById("message").value = '';
							ShowMessageBox('نظر شما با موفقیت ارسال شد.',true);
							ShowItems('1', 'ShowPostComments');
							return;
						case 'message':
							document.getElementById("commentCounter").innerHTML = '450';
							document.getElementById('dopost').disabled=false;
							document.getElementById("message").value = '';
							ShowMessageBox('پیغام شما با موفقیت ارسال شد.',true);
							return;
						default:
							return;
					}
				}
				else
				{
					//alert(xmlHttp.responseText);
					return;
				}
			}
		}
		var __mode = 'mode=post';
/*		if(_isInUpdateMode)
		{
			switch(_mode)
			{
				default:
					__mode = 'mode=update&id=' + _updateID;
					break;
			}
		}*/
		var parameters = '';
		switch(_mode)
		{
			case 'my':
				parameters = __mode + '&PostContent='+encodeURIComponent(PostContent) +'&LanguageType='+encodeURIComponent(_LanguageType) +'&PostAlign='+encodeURIComponent(_PostAlign);
				break;
			case 'comments':
				parameters = __mode + '&PostContent='+encodeURIComponent(PostContent) +'&LanguageType='+encodeURIComponent(_LanguageType) +'&PostAlign='+encodeURIComponent(_PostAlign) + '&PostID=' + _currentPostID;
				break;
			case 'message':
				parameters = __mode + '&PostContent='+encodeURIComponent(PostContent) +'&LanguageType='+encodeURIComponent(_LanguageType) +'&PostAlign='+encodeURIComponent(_PostAlign) + '&PersonID=' + _currentPersonID;
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
	var serverPage = '';
	var _temp = '';
	switch(_mode)
	{
		case 'ShowLatestPosts':
			serverPage='home.aspx';
			break;
		case 'ShowMyPosts':
			serverPage='my.aspx';
			toggleTabs(_mode);
			break;
		case 'ShowMyFriendsPosts':
			serverPage='my.aspx';
			toggleTabs(_mode);
			break;
		case "ShowPrivateMessages":
			serverPage='my.aspx';
			toggleTabs(_mode);
			break;
		case 'ShowStarredPosts':
			serverPage='my.aspx';
			toggleTabs(_mode);
			break;
		case 'ShowAllComments':
			serverPage='my.aspx';
			_temp = _mode;
			break;
		case 'ShowAllStarredComments':
			serverPage='my.aspx';
			_temp = _mode;
			break;
		case "ShowPostComments":
			serverPage='comments.aspx';
			break;
		case 'ShowGuestPosts':
			serverPage='guest.aspx';
			break;
		default:
			return;
	}
	document.getElementById('loaderImg'+_temp).innerHTML = loaderUI;
	document.getElementById('loaderImg'+_temp).style.display = 'block';
	
	//document.getElementById('resultText'+_temp).innerHTML = '';
	
	var _xmlHttp = null;
	if((_xmlHttp = createAjaxObject()) != null){
		_xmlHttp.onreadystatechange=function()
		{
			if(_xmlHttp.readyState==4)
			{
				if(_xmlHttp.responseText == 'Logouted')
				{
					window.location = _logoutedURL;
					return;
				}
				if(_xmlHttp.responseText == 'DoRefresh')
				{
					ShowItems('1', _mode);
					return;
				}
				document.getElementById('loaderImg'+_temp).innerHTML = "";
				document.getElementById('loaderImg'+_temp).style.display = 'none';
				if(_xmlHttp.responseText == 'NoFoundPost')
				{
					switch(_mode)
					{
						case 'ShowHome':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>پستی وجود ندارد.</div>";
							return;
						case 'ShowMyPosts':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>پستی وجود ندارد.</div>";
							return;
						case 'ShowMyFriendsPosts':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>پستی وجود ندارد.</div>";
							return;
						case 'ShowGuestPosts':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>پستی وجود ندارد.</div>";
							return;
						case 'ShowPrivateMessages':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>پیغام شخصی وجود ندارد.</div>";
							return;
						case 'ShowPostComments':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>نظری وجود ندارد.</div>";
							return;
						case 'ShowStarredPosts':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>پست برگزیده ایی وجود ندارد.</div>";
							return;
						case 'ShowAllComments':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>نظری وجود ندارد.</div>";
							return;
						case 'ShowAllStarredComments':
							document.getElementById('resultText'+_temp).innerHTML = "<div class='error' align=center>نظری وجود ندارد.</div>";
							return;
						default:
							return;
					}
					return;
				}
				document.getElementById('resultText'+_temp).innerHTML=_xmlHttp.responseText;
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
			case 'ShowPostComments':
				_xmlHttp.send('mode=' + _mode + '&page=' + _page + '&PostID=' + _currentPostID);
				break;
			default:
				_xmlHttp.send('mode=' + _mode + '&page=' + _page);
				break;
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function ItemDelete(id,_mode)
{
if (confirm('آیا شما مطمئن هستید؟'))
	{		var serverPage='';
		switch(_mode)
		{
			case 'MyPostDelete':
				serverPage='my.aspx';
				break;
			case 'PrivateMesseageDelete':
				serverPage='my.aspx';
				break;
			case 'StarredPostDeleteFromMyPage':
				serverPage='my.aspx';
				break;
			case 'StarredPostDeleteFromGuestPage':
				serverPage='my.aspx';
				break;
			case 'CommentDelete':
				serverPage='comments.aspx';
				break;
			default:
				return;
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
						window.location = _logoutedURL;
						return;
					}
					document.getElementById('loaderImg').innerHTML = loaderUI;
					document.getElementById('loaderImg').style.display = 'none';
					if(xmlHttp.responseText == 'Success')
					{
						ShowMessageBox('عملیات حذف با موفقیت انجام شد.',true);
						var _m = '';
						switch(_mode)
						{
							case 'MyPostDelete':
								if( parseInt(document.getElementById("PostNumLabel").innerHTML) > 0)
									document.getElementById("PostNumLabel").innerHTML = parseInt(document.getElementById("PostNumLabel").innerHTML)-1;
								_m='ShowMyPosts';
								break;
							case 'PrivateMesseageDelete':
								if( parseInt(document.getElementById("PrivateMessagesNumLabel").innerHTML) > 0)
									document.getElementById("PrivateMessagesNumLabel").innerHTML = parseInt(document.getElementById("PrivateMessagesNumLabel").innerHTML)-1;
								_m='ShowPrivateMessages';
								break;
							case 'StarredPostDeleteFromMyPage':
								_m='ShowStarredPosts';
								break;
							case 'StarredPostDeleteFromGuestPage':
								return;
							case 'CommentDelete':
								if( parseInt(document.getElementById("NumCommentsLable").innerHTML) > 0)
									document.getElementById("NumCommentsLable").innerHTML = parseInt(document.getElementById("NumCommentsLable").innerHTML)-1;
								_m='ShowPostComments';
								break;
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
				/*case 'NewsAdminForNewsGroupsMode':
					xmlHttp.send('mode=DeleteForNewsGroupsMode&DeleteID='+id);
					return;*/
				case 'CommentDelete':
					xmlHttp.send('mode='+_mode+'&DeleteID='+id+'&PostID='+_currentPostID);
					return;
				default:
					xmlHttp.send('mode='+_mode+'&DeleteID='+id);
					return;
			}
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function toggleTabs(mode){
	switch(mode){
		case 'ShowMyFriendsPosts':
			document.getElementById('friends').style.background = "url("+_tabImagesRoot+'friends_tab_selected.png'+")";
			document.getElementById('my').style.background = "url("+_tabImagesRoot+'my_tab.png'+")";
			document.getElementById('private').style.background = "url("+_tabImagesRoot+'pm_tab.png'+")";
			document.getElementById('starred').style.background = "url("+_tabImagesRoot+'favorites_tab.png'+")";
			break;
		case 'ShowMyPosts':
			document.getElementById('friends').style.background = "url("+_tabImagesRoot+'friends_tab.png'+")";
			document.getElementById('my').style.background = "url("+_tabImagesRoot+'my_tab_selected.png'+")";
			document.getElementById('private').style.background = "url("+_tabImagesRoot+'pm_tab.png'+")";
			document.getElementById('starred').style.background = "url("+_tabImagesRoot+'favorites_tab.png'+")";
			break;
		case 'ShowPrivateMessages':
			document.getElementById('friends').style.background = "url("+_tabImagesRoot+'friends_tab.png'+")";
			document.getElementById('my').style.background = "url("+_tabImagesRoot+'my_tab.png'+")";
			document.getElementById('private').style.background = "url("+_tabImagesRoot+'pm_tab_selected.png'+")";
			document.getElementById('starred').style.background = "url("+_tabImagesRoot+'favorites_tab.png'+")";
			break;
		case 'ShowStarredPosts':
			document.getElementById('friends').style.background = "url("+_tabImagesRoot+'friends_tab.png'+")";
			document.getElementById('my').style.background = "url("+_tabImagesRoot+'my_tab.png'+")";
			document.getElementById('private').style.background = "url("+_tabImagesRoot+'pm_tab.png'+")";
			document.getElementById('starred').style.background = "url("+_tabImagesRoot+'favorites_tab_selected.png'+")";
			break;
	}	
//	document.getElementById('gAnchor').focus();
}
//------------------------------------------------------------------------------------------------------------------
function LanguageType()
{
	if(langFarsi) 
		return '0';
	else
		return '1';
}
//------------------------------------------------------------------------------------------------------------------
function PostAlign()
{
	if(rightAlign) 
		return '1';
	else
		return '0';
}
//------------------------------------------------------------------------------------------------------------------
function FKeyDown() {
	return ;
	
	if (window.event.shiftKey && window.event.altKey) {
		langFarsi=!langFarsi;
		return false;
	}
	return true;
}
//------------------------------------------------------------------------------------------------------------------
function FKeyPress() {
	return ;
	
   var key = window.event.keyCode;
   if (key < 0x0020 || key >= 0x00FF)
      return;
   if (langFarsi) {
      var el = event.srcElement;
      var objRegExp = new RegExp("[A-Za-z\x27\x2C\x3B\x5B\x5C\x5D\x7C]");
      var validate_key = objRegExp.test(String.fromCharCode(key));
      if ((validate_key || (key==92)) && (key != 0x200C) && (el.value.lastIndexOf(String.fromCharCode(1740)) == el.value.length - 1) && el.value.length > 0) {
         el.value = el.value.slice(0, -1);
         el.value += String.fromCharCode(1610);
      }
      if (key == 0x0020 && window.event.shiftKey)
         window.event.keyCode = 0x200C;
      else
         window.event.keyCode = farsikey[key - 0x0020];
   }
   return true;
}
//------------------------------------------------------------------------------------------------------------------
function changeLanguage(postFieldName,isFarsi) {
	langFarsi=isFarsi;
	if(langFarsi)
	{
		rightAlign=true;
		document.getElementById(postFieldName).style.textAlign="right";
		document.getElementById(postFieldName).style.direction="rtl";
	}
	else
	{
	    rightAlign=false;
		document.getElementById(postFieldName).style.textAlign="left";
		document.getElementById(postFieldName).style.direction="ltr";
	}
} 
//------------------------------------------------------------------------------------------------------------------
function FBlogKeyDown(postFieldName,isFarsi) {
	/*if (window.event.shiftKey && window.event.altKey) {      
		changeLanguage(isFarsi);
		return false;
	}
	return true;*/
//	alert(isFarsi);
	changeLanguage(postFieldName,isFarsi);
}
//------------------------------------------------------------------------------------------------------------------
function charCounter(postFieldName,counterFieldName){
	if(450-document.getElementById(postFieldName).value.length>=0){
		document.getElementById(counterFieldName).innerHTML=parseInt(450-document.getElementById(postFieldName).value.length);
		document.getElementById('dopost').disabled=false;
	}else{
		ShowMessageBox("بیشتر از 450 کاراکتر مجاز نیستید!",true);
		document.getElementById('dopost').disabled=true;
	}
}
//------------------------------------------------------------------------------------------------------------------

var ie4 = ((navigator.appVersion.indexOf("MSIE")>0) && (parseInt(navigator.appVersion) >= 4)); 
var count = 0, count2 = 0, add1 = 3, add2 = 10, timerID,timerManagerID, _isInitilizedShowMessageBoxFader=false,_isInitilizedTimerManager=false;
function ShowMessageBoxFader() 
{ 
	if (ie4) 
	{ 
		count += add1; 
		count2 += add2; 
		delay = 30; 
		if(count2 > 100)
			count2 = 100; 
		if(count > 100) 
		{ 
			count = 100; 
			add1 = -10; add2 = -3; 
			delay = 350; 
		}
		if(count < 0) 
			count = 0; 
		if(count2 < 0) 
		{ 
			count2 = 0; 
			add1 = 3; 
			add2 = 10; 
			delay = 200; 
		} 
		document.getElementById('messagee').style.filter = "Alpha(Opacity="+count2+",FinishOpacity="+count+",style=2)"; 
		_isInitilizedShowMessageBoxFader = true;
		timerID = setTimeout("ShowMessageBoxFader()", delay); 
	} 
}
function ResetFaderTimer()
{
	count = 0, count2 = 0, add1 = 3, add2 = 10;
}
function TimerManager()
{
	if(!_isInitilizedTimerManager)
	{
		_isInitilizedTimerManager = true;
		setTimeout("TimerManager()", 5000); 
		return;
	}
	else
	{
		//alert("hello");
		if(_isInitilizedShowMessageBoxFader)
		{
/*			if(timerID != null)
			{*/
				clearInterval(timerID);
				clearInterval(timerManagerID);
//				timerID = null;
				_isInitilizedShowMessageBoxFader = false;
				_isInitilizedTimerManager = false;
				ResetFaderTimer();
				document.getElementById('messagee').innerHTML='';
				document.getElementById('messagee').style.display = 'none';
				return;
			//}
		}
	}
}
function ShowMessageBox(message,isShow)
{
	if(isShow)
	{
		if(_isInitilizedShowMessageBoxFader)		
		{
			clearInterval(timerID);
			_isInitilizedShowMessageBoxFader = false;
			if(_isInitilizedTimerManager)
			{
				clearInterval(timerManagerID);
				_isInitilizedTimerManager = false;
			}
			ResetFaderTimer();
		}

		document.getElementById('messagee').innerHTML='>> ' + message;
		document.getElementById('messagee').style.display = 'block';
		if(!_isInitilizedShowMessageBoxFader)		
			ShowMessageBoxFader();
		if(!_isInitilizedTimerManager)
			TimerManager();
	}
	else
	{
		document.getElementById('messagee').innerHTML='';
		document.getElementById('messagee').style.display = 'none';
		return ;
	}
}
//------------------------------------------------------------------------------------------------------------------
function FriendManagement(field,action,FriendBlogID,mode)
{
	if (confirm('آیا شما مطمئن هستید؟'))
	{
		field.disabled=true;
		field.style.display='none';
		var f = document.createElement('form');
		f.style.display = 'none';
		field.parentNode.appendChild(f);
		f.method = 'POST';
		f.action = 'friends.aspx';
		var _action = document.createElement('input'); 
		_action.setAttribute('type', 'hidden');
		_action.setAttribute('name', 'action');
		_action.setAttribute('value', action);
		f.appendChild(_action);
		
		var _FriendBlogID = document.createElement('input'); 
		_FriendBlogID.setAttribute('type', 'hidden');
		_FriendBlogID.setAttribute('name', 'FriendBlogID');
		_FriendBlogID.setAttribute('value', FriendBlogID);
		f.appendChild(_FriendBlogID);
		
		var _mode = document.createElement('input'); 
		_mode.setAttribute('type', 'hidden');
		_mode.setAttribute('name', 'mode');
		_mode.setAttribute('value', mode);
		f.appendChild(_mode);
		
		f.submit(); 
	}
	return false;
}
//------------------------------------------------------------------------------------------------------------------
function UserBlock()
{
	return true;
	/*if (confirm('آیا شما مطمئن هستید؟'))
		return true;
	else
		return false;*/
}
//------------------------------------------------------------------------------------------------------------------
function DoStar(PostID)
{
	/*if (confirm('آیا شما مطمئن هستید؟'))
	{*/
		serverPage="my.aspx";
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
						window.location = _logoutedURL;
						return;
					}
					document.getElementById('loaderImg').innerHTML="";
					if(xmlHttp.responseText == 'Success')
					{
						document.getElementById('loaderImg').innerHTML = '';
					    document.getElementById('loaderImg').style.display = 'none';
						document.getElementById('star-img'+PostID).src="http://www.peyghamak.com/images/start_selected.gif";
						document.getElementById('star-img'+PostID).title="این یک پست برگزیده است";
						document.getElementById('star-img'+PostID).onclick="";
						//ShowMessageBox('پست انتخاب شده به عنوان پست برگزیده انتخاب شد.',true);
					}
					else
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			xmlHttp.send('mode=staring&PostID='+PostID);
			//xmlHttp.close();
		}
	//}
}
//------------------------------------------------------------------------------------------------------------------
//these functions are for the view.aspx page
function imageFadeOut(imgName) 
{
  if ( document.all) 
  {
  	var _img = document.getElementById(imgName);
    _img.opacity = 100;
    setOpacity(_img.name, -10, 100);
  }
}

function imageFadeIn(imgName) 
{
  if ( document.all) 
  {
   	var _img = document.getElementById(imgName);
    _img.opacity = 0;
    setOpacity(_img.name, 10, 100);
  }
}

function setOpacity (imgName, step, delay) 
{
  var img = document.images[imgName];
  if(img.opacity < 40 && step <= 0) return;
  img.opacity += step;
  if (document.all) img.style.filter = 'alpha(opacity = ' + img.opacity + ')'; 
  if (step > 0 && img.opacity < 100 || step < 0 && img.opacity > 0)
    setTimeout('setOpacity("' + img.name + '",' + step + ', ' + delay + ')', delay);
} 

//------------------------------------------------------------------------------------------------------------------
function ListUsersBySearchQueryValidation()
{
  var query = document.getElementById('query').value;
  if(query == '')
  {
  	alert('.عبارت جستجو خالی است');
  	return false;
  }
  if(query.length <= 2)
  {
   	alert('.تعداد حروف عبارت جستجو باید از 1 بیشتر باشد');
  	return false;
  }
  return true;
}
//------------------------------------------------------------------------------------------------------------------