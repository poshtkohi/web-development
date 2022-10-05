//-----------------------------------------------------------------------------
var xmlHttp;
//Opened Comment Box Object Loader
var ocbol;
//AJAX image
var loaderUI="<img src='/services/ChatBox/images/loading.gif' />";
var editor = null;
//-----------------------------------------------------------------------------
function IsInEditMode()
{
	if(document.getElementById('_IsInEditMode').innerText=='true')
		return true;
	else
		return false;
}
//-----------------------------------------------------------------------------
function ResetUpdateMode()
{
	document.getElementById('_IsInEditMode').innerText = 'false';
	document.getElementById('PageID').innerText = '-1';
}
//-----------------------------------------------------------------------------
function SetUpdateMode(PageID)
{
	document.getElementById('_IsInEditMode').innerText = 'true';
	document.getElementById('PageID').innerText = PageID;
}
//-----------------------------------------------------------------------------
function Cancel()
{
//	ResetUpdateMode();
	document.getElementById('message').style.display = 'none';
	document.getElementById('message').innerHTML = '';
	document.getElementById('NewOrEditPageSection').style.display = "none";
	dynaframe();
	return ;
}
//-----------------------------------------------------------------------------
function DefineNewPage()
{
	document.getElementById('title').innerText = "";
	document.getElementById('ThemeContent').innerText = "";
	if(editor != null )
	{
		editor.setHTML("");
	}
	document.getElementById('NewOrEditPageSection').style.display = "block";
	if(editor == null)
	{
		editor = new HTMLArea("PostContent");
		editor.generate();
	}
	dynaframe();
	return ;
}
//-------------------------------------------------------------------
function createAjax()
{
  try
    {
    // Firefox, Opera 8.0+, Safari
    xmlHttp=new XMLHttpRequest();
	return true;
    }
  catch (e)
    {
    // Internet Explorer
    try
      {
      xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
	  return true;
      }
    catch (e)
      {
      try
        {
        xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
		return true;
        }
	    catch (e)
        {
			alert("!نمیکند پشتیبانی را  AJAX مرورگر شما ");
			return false;
        }
      }
    }
}
//-----------------------------------------------------------------------------
function DoPost()
{	
	if(document.getElementById('title').value == '')
	{
		alert('.عنوان مطلب خالی است');
		return ;
	}
	if(document.getElementById('ThemeContent').value == '')
	{
		alert('.کد قالب صفحه خالی است');
		return ;
	}
	if(editor.getHTML() == '')
	{
		alert('.مطلب صفحه خالی است');
		return ;
	}

	Cancel();
	serverPage='pages.aspx';
		
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';
	document.getElementById('DefineNewPageBtn').disabled=true;

	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = '/services/blogbuilderv1/Logouted.aspx';
					return;
				}
				if(IsInEditMode())
				{
					ResetUpdateMode();
				}
				document.getElementById('DefineNewPageBtn').disabled=false;
				document.getElementById('message').innerHTML='';
				document.getElementById('message').style.display = 'none';
				alert(xmlHttp.responseText);
				ShowPages('1');
				return;
			}
		}
		var _mode = 'mode=post';
		if(IsInEditMode())
		{
			_mode = 'mode=update&id=' + document.getElementById('PageID').innerText;
		}
		var parameters = _mode + '&PostContent='+encodeURIComponent(editor.getHTML())+'&ThemeContent='+encodeURIComponent(document.getElementById('ThemeContent').value) +'&title='+encodeURIComponent(document.getElementById('title').value);
		xmlHttp.open("POST",serverPage,true);
 		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
      	xmlHttp.setRequestHeader("Content-length", parameters.length);
	    xmlHttp.send(parameters);

	}
}
//-----------------------------------------------------------------------------
function ShowPages(page)
{
	serverPage='pages.aspx';
		
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';

	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'DoRefresh')
				{
					ShowPages('1');
					return;
				}
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById('message').innerHTML = 'هیچ صفحه ایی یافت نشد.';
    				document.getElementById('result').innerHTML = '';
					return;
				}
				document.getElementById('message').innerHTML='';
				document.getElementById('message').style.display = 'none';
				document.getElementById('result').innerHTML=xmlHttp.responseText;
				dynaframe();
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('mode=show&page='+page);
	}
}
//-----------------------------------------------------------------------------
function PageDelete(id)
{
	if (confirm('آیا شما مطمئن هستید که می خواهید این صفحه را حذف کنید؟'))
	{
		serverPage="pages.aspx";
		
     	document.getElementById('message').innerHTML = loaderUI;
		document.getElementById('message').style.display = 'block';
		
		if(createAjax())
		{
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = '/services/blogbuilderv1/Logouted.aspx';
						return;
					}
					document.getElementById('message').innerHTML = loaderUI;
					document.getElementById('message').style.display = 'block';
					if(xmlHttp.responseText == 'Success')
					{
						alert('.صفحه انتخاب شده با موفقیت از سیستم حذف شد');
						
						ShowPages('1');
						return;
					}
					else
					{
						alert('.خطایی در حذف پیام انتخاب شده رخ داد');
					}
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			xmlHttp.send('mode=delete&DeleteID='+id);
		}
	}
}
//-----------------------------------------------------------------------------
function PageLoad(id)
{
	serverPage='pages.aspx';
		
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';

	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = '/services/blogbuilderv1/Logouted.aspx';
					return;
				}
				document.getElementById('message').style.display = 'block';
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById('message').style.display = 'block';
					document.getElementById('message').innerHTML = '';
					alert('صفحه مربوطه قبلا حذف شده است.');
					return;
				}
				SetUpdateMode(id);
				document.getElementById('message').innerHTML='هم اکنون می توانید به ویرایش صفحه انتخاب شده بپردازید.';
				document.getElementById('message').style.display = 'block';
				var str = xmlHttp.responseText;//'title,PostTheme,PostContent';
				var ret = str.split(',');
				document.getElementById('title').innerText = Base64.decode(ret[0]);
				document.getElementById('ThemeContent').innerText = Base64.decode(ret[1]);
				document.getElementById('NewOrEditPageSection').style.display = "block";
				dynaframe();
				if(editor == null)
				{
					editor = new HTMLArea("PostContent");
					editor.generate();
				}
				editor.setHTML(Base64.decode(ret[2]));
//				dynaframe();
				return;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('mode=load&id='+id);
	}
}
//-----------------------------------------------------------------------------