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
var _IsAjaxLinks = true;
var _updateID = -1;
var _editorIsDefined = false;
//var _currentNewsGroupID = '-1';
var _selectedTemplateID = '-1';
var _tinyMCECompare = '<p style="font-family: tahoma;font-size: 10pt" dir="rtl">&nbsp;</p>';

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
function scroll(){
//    window.scrollTo(1000,1000);alert("hi");
//	window.frames[0].scrollTo(0,0);
	document.getElementById('message').focus();
  }
function startCallback()
{
	var _filename = document.getElementById('file').value.toLowerCase();
	if(_filename == '')
	{
		alert('.یک عکس انتحاب کنید');
		return false;
	}
	if(_filename.indexOf('.jpg') <= 0 && _filename.indexOf('.png') <= 0 && _filename.indexOf('.gif') <= 0)
	{
		alert('.فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد');
		return false;
	}
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';
	dynaframe();
//	document.getElementById('post').disabled=true;
	return true;
}
function completeCallback(response)
{
	document.getElementById('message').innerHTML = '';
	document.getElementById('message').style.display = 'none';
//	document.getElementById('post').disabled=true;
	document.getElementById('file').value = '';
	dynaframe();
	alert('.عکس شما با موفقیت در سایت ایران بلاگ به روز رسانی شد');
	return;
}
//------------------------------------------------------------------------------------------------------------------
function DoPost(_mode)
{
	var serverPage='';
	switch(_mode)
	{
		case 'PostAdmin':
			serverPage='PostAdmin.aspx';
			var PostTitle = document.getElementById('PostTitle').value;
			var PostContent = tinyMCE.get('PostContent').getContent();
			var ContinuedPostContent = tinyMCE.get('ContinuedPostContent').getContent();
			var CategoryID = document.getElementById('CategoryID').value;
			var comment = document.getElementById('comment').value;
			if(ContinuedPostContent == _tinyMCECompare)
				ContinuedPostContent = '';
			if(PostContent == '' || PostContent == _tinyMCECompare)
			{
				alert('.متن پست امروز شما خالی است');
				return ;
			}
			if(PostContent != '')
			{
				if(PostContent.length > 204800)
				{
					alert(".حجم پست وبلاگ شما نمی تواند از 256 کیلو بایت بیشتر باشد");
					return ;
				}
				if(ContinuedPostContent != '')
				{
					if(ContinuedPostContent.length > 204800)
					{
					 alert(".حجم ادامه مطلب نمی تواند از 256 کیلو بایت بیشتر باشد");
					 return ;
					}
				}
			}
			break;
		case 'ajax.links':
			serverPage='ajax.links.aspx';
			var LinkTitle = document.getElementById('LinkTitle').value;
			var LinkAddress = document.getElementById('LinkAddress').value;
			if(LinkTitle == '')
			{
				alert('.عنوان پیوند خالی است');
				return ;
			}
			if(LinkAddress == '')
			{
				alert('.آدرس پیوند خالی است');
				return ;
			}
			var re = /^(http):\/\/\S+\.\S/;
			if(!re.test(LinkAddress))
			{
			   alert(".آدرس پیوند نامعتبر است");
			   return ;
			}
			break;
		case 'ajax.linkss':
			serverPage='ajax.links.aspx';
			var LinkTitle = document.getElementById('LinkTitle').value;
			var LinkAddress = document.getElementById('LinkAddress').value;
			if(LinkTitle == '')
			{
				alert('.عنوان پیوند خالی است');
				return ;
			}
			if(LinkAddress == '')
			{
				alert('.آدرس پیوند خالی است');
				return ;
			}
			var re = /^(http):\/\/\S+\.\S/;
			if(!re.test(LinkAddress))
			{
			   alert(".آدرس پیوند نامعتبر است");
			   return ;
			}
			break;
		case 'ajax.PostArchive':
			serverPage='ajax.PostArchive.aspx';
			var PostArchiveTitle = document.getElementById('PostArchiveTitle').value;
			if(PostArchiveTitle == '')
			{
				alert('.موضوع خالی است');
				return ;
			}
			break;
		case 'ajax.account.save.password':
			serverPage='ajax.account.aspx';
			var LastPassword = document.getElementById('LastPassword').value;
			var NewPassword = document.getElementById('NewPassword').value;
			var ConfirmNewPassword = document.getElementById('ConfirmNewPassword').value;
			if(LastPassword == '')
			{
				alert('.کلمه عبور قدیمی خالی است');
				return ;
			}
			if(NewPassword == '')
			{
				alert('.کلمه عبور جدید خالی است');
				return ;
			}
			if(NewPassword != ConfirmNewPassword)
			{
				alert('.کلمه عبور جدید با تکرار کلمه عبور جدید برابر نیست');
				return ;
			}
			break;
		case 'ajax.account.save.settings':
			serverPage='ajax.account.aspx';
			var BlogEmail = document.getElementById('BlogEmail').value;
			var BlogFirstName = document.getElementById('BlogFirstName').value;
			var BlogLastName = document.getElementById('BlogLastName').value;
			var BlogTitle = document.getElementById('BlogTitle').value;
			var BlogAbout = document.getElementById('BlogAbout').value;
			var BlogEmailEnable = document.getElementById('BlogEmailEnable').value;
			var BlogCategory = document.getElementById('BlogCategory').value;
			var BlogMaxPostShow = document.getElementById('BlogMaxPostShow').value;
			var BlogArciveDisplayMode = document.getElementById('BlogArciveDisplayMode').value;
			if(BlogTitle == '')
			{
				alert('.عنوان وبلاگ خالی است');
				return ;
			}
			if(BlogEmail == '')
			{
				alert('.آدرس ایمیل خالی است');
				return ;
			}
			if(BlogEmail == '')
			{
				alert('.آدرس ایمیل خالی است');
				return ;
			}
			var re = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
			if(!re.test(BlogEmail))
			{
			   alert(".آدرس ایمیل نامعتبر است");
			   return ;
			}
//			_isInUpdateMode = true;
			break;
		case 'ajax.TemplateAdmin':
			serverPage='ajax.TemplateAdmin.aspx';
			break;
		case 'ajax.TemplateAdmin.save':
			serverPage='ajax.TemplateAdmin.aspx';
			var TemplateContent = document.getElementById('TemplateContent').value;
            if (TemplateContent == '')
		    {
				alert(".محتویات قالب وبلاگ شما نمی تواند خالی باشد");
				return;
			}
            if (TemplateContent.length > 256*1024)
            {
                alert(".حجم قالب وبلاگ شما نمی تواند از 256 کیلو بایت بیشتر باشد");
				return;
			}//alert(TemplateContent);return;
			break;
		default:
			return;
	}
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';
	switch(_mode)
	{
		case 'ajax.TemplateAdmin':
			break;
		default:
			//document.getElementById('post').disabled=true;
			dynaframe();
			break;
	}
	
	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = 'Logouted.aspx';
					return;
				}
				//document.getElementById('post').disabled=false;
				document.getElementById('message').innerHTML='';
				document.getElementById('message').style.display = 'none';
				alert(xmlHttp.responseText);
				switch(_mode)
				{
					case 'PostAdmin':
						ShowItems('1', 'ShowPostAdmin');
						break;
					case 'ajax.links':
						ShowItems('1', 'ShowAjaxLinks');
						Cancel();
						return;
					case 'ajax.linkss':
						ShowItems('1', 'ShowAjaxLinks');
						Cancel();
						return;
					case 'ajax.PostArchive':
						ShowItems('1', 'ShowAjaxPostArchive');
						Cancel();
						return;
					case 'ajax.TemplateAdmin':
						return;
					case 'ajax.TemplateAdmin.save':
						Cancel();
						return;
					default:
						return;
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
				/*case 'NewsAdminForNewsGroupsMode':
					__mode = 'mode=UpdateForNewsGroupsMode&id=' + _updateID;
					break;*/
				default:
					__mode = 'mode=update&id=' + _updateID;
					break;
			}
		}
		var parameters = '';
		switch(_mode)
		{
			case 'PostAdmin':
				parameters = __mode + '&PostTitle='+encodeURIComponent(PostTitle)+'&PostContent='+encodeURIComponent(PostContent) +'&ContinuedPostContent='+encodeURIComponent(ContinuedPostContent) +'&CategoryID='+encodeURIComponent(CategoryID) +'&comment='+encodeURIComponent(comment);
				break;
			case 'ajax.links':
				parameters = __mode + '&LinkTitle='+encodeURIComponent(LinkTitle)+'&LinkAddress='+encodeURIComponent(LinkAddress) +'&_mode='+encodeURIComponent(_mode);
				break;
			case 'ajax.linkss':
				parameters = __mode + '&LinkTitle='+encodeURIComponent(LinkTitle)+'&LinkAddress='+encodeURIComponent(LinkAddress) +'&_mode='+encodeURIComponent(_mode);
				break;
			case 'ajax.PostArchive':
				parameters = __mode + '&PostArchiveTitle='+encodeURIComponent(PostArchiveTitle);
				break;
			case 'ajax.account.save.password':
				parameters = __mode + '&LastPassword='+encodeURIComponent(LastPassword)+'&NewPassword='+encodeURIComponent(NewPassword) +'&_mode='+encodeURIComponent(_mode);
				document.getElementById('LastPassword').value = '';document.getElementById('NewPassword').value = '';document.getElementById('ConfirmNewPassword').value = '';
				break;
			case 'ajax.account.save.settings':
				parameters = __mode + '&BlogEmail='+encodeURIComponent(BlogEmail)+'&BlogFirstName='+encodeURIComponent(BlogFirstName)+'&BlogLastName='+encodeURIComponent(BlogLastName)+'&BlogTitle='+encodeURIComponent(BlogTitle)+'&BlogAbout='+encodeURIComponent(BlogAbout)+'&BlogEmailEnable='+encodeURIComponent(BlogEmailEnable)+'&BlogCategory='+encodeURIComponent(BlogCategory)+'&BlogMaxPostShow='+encodeURIComponent(BlogMaxPostShow)+'&BlogArciveDisplayMode='+encodeURIComponent(BlogArciveDisplayMode) +'&_mode='+encodeURIComponent(_mode);
				document.getElementById('LastPassword').value = '';document.getElementById('NewPassword').value = '';document.getElementById('ConfirmNewPassword').value = '';
				break;
			case 'ajax.TemplateAdmin':
				parameters = 'mode=TemplateSelect&SelectedTemplateID='+encodeURIComponent(_selectedTemplateID);
				break;
			case 'ajax.TemplateAdmin.save':
				parameters = 'mode=TemplateSave&TemplateContent='+encodeURIComponent(TemplateContent);
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
		case 'ShowPostAdmin':
			serverPage='PostAdmin.aspx';
			break;
		case 'ShowAjaxLinks':
			serverPage='ajax.links.aspx';
			break;
		case 'ShowAjaxPostArchive':
			serverPage='ajax.PostArchive.aspx';
			break;
		case 'ShowAjaxTemplates':
			serverPage='ajax.TemplateAdmin.aspx';
			break;
		default:
			return;
	}
	document.getElementById('loaderImg').innerHTML = loaderUI;
	document.getElementById('loaderImg').style.display = 'block';
	
	document.getElementById('resultText').innerHTML = '';
	
	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = 'Logouted.aspx';
					return;
				}
				if(xmlHttp.responseText == 'DoRefresh')
				{
					ShowItems('1', _mode);
					return;
				}
				document.getElementById('loaderImg').innerHTML = "";
				document.getElementById('loaderImg').style.display = 'none';
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					switch(_mode)
					{
						case 'ShowPostAdmin':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>پستی وجود ندارد.</div>";
							return;
						case 'ShowAjaxLinks':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>پیوندی وجود ندارد.</div>";
							return;
						case 'ShowAjaxPostArchive':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>آرشیو موضوعی وجود ندارد.</div>";
							return;
						case 'ShowAjaxTemplates':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>قالبی وجود ندارد.</div>";
							return;
						default:
							return;
					}
					return;
				}
				document.getElementById('resultText').innerHTML=xmlHttp.responseText;
				dynaframe();
				/*switch(_mode)
				{
					case 'ShowPostAdmin':
						GenerateContextMenu(FindContextMenuIDsSection(xmlHttp.responseText));
						break;
					default:
						break;
				}*/
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		switch(_mode)
		{
			case "ShowAjaxLinks":
				if(_IsAjaxLinks)
					xmlHttp.send('mode=' + _mode + '&page=' + _page + '&_mode=ajax.links');
				else
					xmlHttp.send('mode=' + _mode + '&page=' + _page + '&_mode=ajax.linkss');
				break;
			default:
				xmlHttp.send('mode=' + _mode + '&page=' + _page);
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
			case 'PostAdmin':
				serverPage='PostAdmin.aspx';
				break;
			case 'ajax.links':
				serverPage='ajax.links.aspx';
				break;
			case 'ajax.linkss':
				serverPage='ajax.links.aspx';
				break;
			case 'ajax.PostArchive':
				serverPage='ajax.PostArchive.aspx';
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
						window.location = 'Logouted.aspx';
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
							case 'PostAdmin':
								_m='ShowPostAdmin';
								break;
							case 'ajax.links':
								_m='ShowAjaxLinks';
								break;
							case 'ajax.linkss':
								_m='ShowAjaxLinks';
								break;
							case 'ajax.PostArchive':
								_m='ShowAjaxPostArchive';
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
				case 'ajax.links':
					xmlHttp.send('mode=delete&DeleteID=' + id + '&_mode=ajax.links');
					return;
				case 'ajax.linkss':
					xmlHttp.send('mode=delete&DeleteID=' + id + '&_mode=ajax.linkss');
					return;
				default:
					xmlHttp.send('mode=delete&DeleteID=' + id);
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
		case 'PostAdmin':
			serverPage='PostAdmin.aspx';
			break;
		case 'ajax.links':
			serverPage='ajax.links.aspx';
			break;
		case 'ajax.linkss':
			serverPage='ajax.links.aspx';
			break;
		case 'ajax.PostArchive':
			serverPage='ajax.PostArchive.aspx';
			break;
		case 'ajax.TemplateAdmin':
			if(document.getElementById('TemplateContent').value != '')
			{
				document.getElementById('up').style.display = "block";
				document.getElementById('cancel').style.display = "block";
				document.getElementById('new').style.display = "none";
				document.getElementById('message').innerHTML='هم اکنون می توانید به ویرایش آیتم انتخاب شده بپردازید.';
				document.getElementById('message').style.display = 'block';
				dynaframe();
				return;
			}
			serverPage='ajax.TemplateAdmin.aspx';
			break;
		default:
			return;
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
					window.location = 'Logouted.aspx';
					return;
				}
				document.getElementById('message').style.display = 'block';
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById('message').style.display = 'block';
					document.getElementById('message').innerHTML = '';
					switch(_mode)
					{
						case 'PostAdmin':
							alert('این پست قبلا حذف شده است.');
							break;
						case 'ajax.links':
							alert('این پیوند قبلا حذف شده است.');
							break;
						case 'ajax.linkss':
							alert('این پیوند قبلا حذف شده است.');
							break;
						case 'ajax.PostArchive':
							alert('این موضوع قبلا حذف شده است.');
							break;
						case 'ajax.TemplateAdmin':
							alert('قالبی یافت نشد، یک قالب برای وبلاگ خود انتخاب کنید.');
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
				dynaframe();
				
				var str = xmlHttp.responseText;
				str += ',';
				var ret = str.split(',');
				switch(_mode)
				{
					case 'PostAdmin':
						document.getElementById('CategoryID').disabled = true;
						/*  
							PostTitle
							PostContent
							ContinuedPostContent
							CategoryID
							comment
						*/
						document.getElementById('PostTitle').value = Base64.decode(ret[0]);
						//alert(Base64.decode(ret[1]));return
						if(_editorIsDefined)
						{
							tinyMCE.get('PostContent').setContent(Base64.decode(ret[1]));
							var _ContinuedPostContent = Base64.decode(ret[2]);
							if(_ContinuedPostContent == '')
							{
								AddContinuedSection(false);
								tinyMCE.get('ContinuedPostContent').setContent(_tinyMCECompare);
							}
						    else
							{
								AddContinuedSection(true);
								tinyMCE.get('ContinuedPostContent').setContent(_ContinuedPostContent);
							}
						}
						document.getElementById('CategoryID').selectedIndex = GetListBoxIndexByValue('CategoryID', Base64.decode(ret[3]));
						document.getElementById('comment').selectedIndex = GetListBoxIndexByValue('comment', Base64.decode(ret[4]));
						break;
					case 'ajax.links':
						/*  
							LinkTitle
							LinkAddress
						*/
						document.getElementById('LinkTitle').value = Base64.decode(ret[0]);
						document.getElementById('LinkAddress').value = Base64.decode(ret[1]);
						break;
					case 'ajax.linkss':
						/*  
							LinkTitle
							LinkAddress
						*/
						document.getElementById('LinkTitle').value = Base64.decode(ret[0]);
						document.getElementById('LinkAddress').value = Base64.decode(ret[1]);
						break;
				    case 'ajax.PostArchive':
						/*  
							PostArchiveTitle
						*/
						document.getElementById('PostArchiveTitle').value = Base64.decode(ret[0]);
						break;
					case 'ajax.TemplateAdmin':
						/*  
							TemplateContent
						*/
						document.getElementById('TemplateContent').value = Base64.decode(ret[0]);
						break;
					default:
						return;
				}	
				scroll();
				return;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		switch(_mode)
		{
			/*case 'NewsAdminForNewsGroupsMode':
				xmlHttp.send('mode=LoadForNewsGroupsMode&id='+id);
				return;*/
			case 'ajax.links':
					xmlHttp.send('mode=load&id=' + id + '&_mode=ajax.links');
				return;
			case 'ajax.linkss':
					xmlHttp.send('mode=load&id=' + id + '&_mode=ajax.linkss');
				return;
			default:
				xmlHttp.send('mode=load&id=' + id);
				return;
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function Cancel()
{
	document.getElementById('message').style.display = 'none';
	document.getElementById('message').innerHTML = '';
	document.getElementById('up').style.display = "none";
	document.getElementById('cancel').style.display = "none";
	document.getElementById('new').style.display = "block";
	dynaframe();
/*	if(_currentNewsGroupID == '-1')
		document.getElementById('new').style.display = "block";*/
	return ;
}
//------------------------------------------------------------------------------------------------------------------
function New(_mode)
{
	_isInUpdateMode = false;
	_updateID = -1;
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

	document.getElementById('up').style.display = "block";
	document.getElementById('cancel').style.display = "block";
	document.getElementById('new').style.display = "none";
	switch(_mode)
	{
		case 'PostAdmin':
			if(_editorIsDefined)
			{
				tinyMCE.get('PostContent').setContent(_tinyMCECompare);
				tinyMCE.get('ContinuedPostContent').setContent(_tinyMCECompare);
			}
			AddContinuedSection(false);
			break;
		default:
			if(_editorIsDefined)
				tinyMCE.get('content').setContent(_tinyMCECompare);
			break;
	}
	dynaframe();
	return ;
}
//------------------------------------------------------------------------------------------------------------------
function tinyMCEInit()
{	       
	_editorIsDefined = true;
	tinyMCE_GZ.init({
    theme : "advanced",
	skin : "o2k7",
	plugins : "safari,pagebreak,layer,advhr,advimage,advlink,emotions,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,inlinepopups",
	languages : 'en',
	disk_cache : true,
	debug : false
	});
	tinyMCE.init({
	// O2k7 skin
    mode : "textareas",
    theme : "advanced",
	skin : "o2k7",
	plugins : "safari,pagebreak,layer,advhr,advimage,advlink,emotions,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,inlinepopups",

	// Theme options
	theme_advanced_buttons1 : "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,formatselect,fontselect,fontsizeselect,language",
	theme_advanced_buttons2 : "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,code,|,preview,|,forecolor,backcolor",
	theme_advanced_buttons3 : "hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
	theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,pagebreak",
	theme_advanced_toolbar_location : "top",
	theme_advanced_toolbar_align : "left",
	theme_advanced_statusbar_location : "bottom",
	theme_advanced_resizing : false,
	
    setup : function(ed) {	
	
	
	ed.onKeyPress.add(function(ed, e){		   
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

	        });
			
		
		
		    ed.addButton('language', {
				title : 'انتخاب زبان',
				image : '/services/jscripts/tiny_mce/plugins/example/img/example.gif',
				onclick : function() {
				   changeLang();
				}
        	});			
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
//------------------------------------------------------------------------------------------------------------------
function AddContinuedSection(_isAdd)
{
	if(_isAdd)
	{
		document.getElementById('ContinuedPostSection').style.display = "block";
		document.getElementById('removetb').style.display = "block";
		document.getElementById('addtb').style.display = "none";
	}
	else
	{
		document.getElementById('ContinuedPostSection').style.display = "none";
		document.getElementById('removetb').style.display = "none";
		document.getElementById('addtb').style.display = "block";
	}
	if(_editorIsDefined)
		tinyMCE.get('ContinuedPostContent').setContent(_tinyMCECompare);
	dynaframe();
}
//------------------------------------------------------------------------------------------------------------------
function GetListBoxIndexByValue(_linkBoxName, _value)
{
	var _index = -1;
	var obj = document.getElementById(_linkBoxName);
	for(var i = 0 ; obj.length ; i++)
	{
		if(obj[i].value == _value)
		{
			_index = i;
			break;
		}
	}
	return _index;
}
//------------------------------------------------------------------------------------------------------------------
AIM = {

    frame : function(c) {
        var n = 'f' + Math.floor(Math.random() * 99999);
        var d = document.createElement('DIV');
        d.innerHTML = '<iframe style="display:none" src="about:blank" id="'+n+'" name="'+n+'" onload="AIM.loaded(\''+n+'\')"></iframe>';
        document.body.appendChild(d);

        var i = document.getElementById(n);
        if (c && typeof(c.onComplete) == 'function') {
            i.onComplete = c.onComplete;
        }

        return n;
    },

    form : function(f, name) {
        f.setAttribute('target', name);
    },

    submit : function(f, c) {
        AIM.form(f, AIM.frame(c));
        if (c && typeof(c.onStart) == 'function') {
            return c.onStart();
        } else {
            return true;
        }
    },

    loaded : function(id) {
        var i = document.getElementById(id);
        if (i.contentDocument) {
            var d = i.contentDocument;
        } else if (i.contentWindow) {
            var d = i.contentWindow.document;
        } else {
            var d = window.frames[id].document;
        }
        if (d.location.href == "about:blank") {
            return;
        }

        if (typeof(i.onComplete) == 'function') {
            i.onComplete(d.body.innerHTML);
        }
    }
}
//------------------------------------------------------------------------------------------------------------------
//these functions are for the ajax.TemplateAdmin.aspx page
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