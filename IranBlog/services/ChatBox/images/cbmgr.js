var xmlHttp;
//Opened Comment Box Object Loader
var ocbol;
//AJAX image
var loaderUI="<img src='images/loading.gif' />";

//-------------------------------------------------------------------
function frmfocus(x,y){
(x.value==y)?x.value="":void (0);
}
//-------------------------------------------------------------------
function frmblur(x,y){
(x.value=="")?x.value=y:void (0);
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
//-------------------------------------------------------------------
function DoPost(BlogID)
{
	var name = document.getElementById('name').value;
	var email = document.getElementById('email').value;
	var PostContent = document.getElementById('PostContent').value;
	if(name == '' || name == 'نام')
	{
		alert('.نام خالی است');
		return ;
	}
	var re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
	if(!re.test(email))
	{
	   alert(".حروف ایمیل نا معتبر است");
	   return ;
	}
	if(PostContent == '' || PostContent == 'پیام')
	{
		alert('.پیام خالی است');
		return ;
	}
    serverPage="Show.aspx";
	document.getElementById('post').disabled=true;
	document.getElementById('loaderImg').innerHTML=loaderUI;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				document.getElementById('post').disabled=false;
				document.getElementById('loaderImg').innerHTML='';
				alert(xmlHttp.responseText);
				ShowMessages('1', BlogID);
				return;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('PostContent='+PostContent+'&BlogID='+BlogID+'&name='+name+'&email='+email);
		//xmlHttp.close();
	}
}
//-------------------------------------------------------------------
function ShowMessages(page,BlogID)
{
	serverPage='Show.aspx';
	document.getElementById('loaderImg').innerHTML = loaderUI;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'DoRefresh')
				{
					ShowMessages('1', BlogID);
					return;
				}
				document.getElementById('loaderImg').innerHTML = "";
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById('resultText').innerHTML = '<div id="error" style="color:#FF0000;font-family:tahoma;font-size:x-small;direction:rtl" align=center>پیامی وجود ندارد.</div>';
					return;
				}
				document.getElementById('loaderImg').innerHTML="";
				document.getElementById('resultText').innerHTML=xmlHttp.responseText;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('mode=show&page=' + page+'&BlogID=' + BlogID);
	}
}
//-------------------------------------------------------------------
function MessageDelete(id, BlogID)
{
	if (confirm('آیا شما مطمئن هستید که می خواهید این پیام را حذف کنید؟'))
	{
		serverPage="Show.aspx";
     	document.getElementById('loaderImg').innerHTML = loaderUI;
		if(document.getElementById('post') != null)
			document.getElementById('post').disabled = true;
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
					document.getElementById('loaderImg').innerHTML = "";
					if(xmlHttp.responseText == 'Success')
					{
						if(document.getElementById('post') != null)
							document.getElementById('post').disabled = false;
						alert('.پیام انتخاب شده با موفقیت از سیستم حذف شد');
						
						ShowMessages('1', BlogID);
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
			xmlHttp.send('mode=delete&DeleteID='+id+'&BlogID='+BlogID);
		}
	}
}
//-------------------------------------------------------------------
function pop(_8,w,h,s){
	nw=window.open("Smilies.aspx?sec="+_8,"cb"+_8.substring(0,3),"width="+w+", height="+h+", toolbar=no, scrollbars="+s+", status=no, resizable=yes");
	try{
	x=screen.width;
	y=screen.height;
	nw.moveTo((x/2)-(w/2)-100,(y/2)-(y/4));
	nw.focus();
	}
	catch(e){
	}
}
//-------------------------------------------------------------------