//(c) Alireza Poshtkohi, 2002-2007, alireza.poshtkohi@gmail.com

var LanguageButtonHover = false

var LanguageButtonShouldHide = false

var LanguageButtonCaller

var LanguageButtonTimer



var MaxCommentLength = 2048



function selectPermission()

{

	if (document.activeElement.isTextEdit == true && document.activeElement != document.body)

	{

		return true

	}

	else

	{

		return false

	}

}



function insertSmiley(smiley)

{

	var textArea = document.getElementById("CommentContent")

	var selectedTextLength = 0

	var canCreateTextRange = textArea.createTextRange

	

	if (canCreateTextRange)

	{

		textArea.focus(textArea.caretPos)

		textArea.caretPos = document.selection.createRange().duplicate()

		selectedTextLength = textArea.caretPos.text.length

	}

	

	if (textArea.value.length + smiley.length + 2 - selectedTextLength > MaxCommentLength)

	{

		alert(".A6'Ì ©'AÌ (1'Ì /1, 4©D© " + smiley + " H,H/ F/'1/")

		return

	}

	

	smiley = "[" + smiley + "]"



	if (canCreateTextRange)

	{

		textArea.caretPos.text = smiley

	}

	else

	{

		textArea.value += smiley

	}

}



function pasteComment()

{

	var textArea = document.getElementById("CommentContent")

	var selectedTextLength = 0



	if (textArea.createTextRange)

	{

		textArea.caretPos = document.selection.createRange().duplicate()

		selectedTextLength = textArea.caretPos.text.length

	}

	if (textArea.value.length + window.clipboardData.getData("text").length - selectedTextLength > MaxCommentLength)

	{

		alert(".E*F EH1/ F81 -,ÌE (H/G H B'(D /1, FEÌ ('4/")

		return false

	}

	return true

}



function isNumeric()

{

	// Only permit numeric characters

	if (window.event.keyCode != 13 && (window.event.keyCode < 50 || window.event.keyCode > 57))

	{

		return false

	}

	return true

}



function showLanguageButton(object)

{

	window.clearTimeout(LanguageButtonTimer)

	LanguageButtonCaller = object

	var LanguageButton = document.getElementById("divLanguageButton")

	var left = object.offsetWidth

	var top = 0

	while (object.offsetParent)

	{

		left += object.offsetLeft

		top += object.offsetTop

		object = object.offsetParent

	}

	LanguageButton.style.left = left + 3

	LanguageButton.style.top = top + 1

	LanguageButton.style.visibility = "visible"

	LanguageButtonShouldHide = false

}



function hideLanguageButton()

{

	if (LanguageButtonHover)

	{

		LanguageButtonShouldHide = true

	}

	else

	{

		LanguageButtonShouldHide = false

		LanguageButtonHover = false

		LanguageButtonTimer = window.setTimeout('document.getElementById("divLanguageButton").style.visibility = "hidden"', 100)

	}

}



function hoverLanguageButton(state)

{

	if (state == "in")

	{

		LanguageButtonHover = true

	}

	else

	{

		LanguageButtonHover = false

		if (LanguageButtonShouldHide)

		{

			hideLanguageButton()

		}

	}

}



function toggleLanguage()

{

	var LanguageButton = document.getElementById("divLanguageButton")

	langFarsi = !langFarsi;

	if (langFarsi)

	{

		LanguageButton.innerText = "FA"

	}

	else

	{

		LanguageButton.innerText = "EN"

	}

	LanguageButtonCaller.focus()

}



function hoverSmiley(smiley, state)

{

	if (state == 'in')

	{

		smiley.style.filter = "progid:DXImageTransform.Microsoft.BasicImage(grayscale=0)"

	}

	else

	{

		smiley.style.filter = "progid:DXImageTransform.Microsoft.BasicImage(grayscale=1)"

	}

}


//AJAX kernel
var xmlHttp;
//Opened Comment Box Object Loader
var ocbol;
//AJAX image
var loaderUI="<img src='commenting/loading.gif'/>";

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
  
function PostComment(postBtn,name,email,url,CommentContent,resultDiv,loaderDiv,BlogID,PostID,IsPrivateComment)
{
	var _IsPrivateComment = 'false';
	if(document.getElementById(IsPrivateComment).checked)
		_IsPrivateComment = 'true';
	document.getElementById(resultDiv).innerHTML='';
	if(document.getElementById(email).value != '')
	{
		var re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
		if(!re.test(document.getElementById(email).value))
		{
			document.getElementById(resultDiv).innerHTML="حروف ایمیل نامعتبر است.";
			return ;
		}
	}
	if(document.getElementById(url).value != '')
	{
		var re = /^(http):\/\/\S+\.\S/;
		if(!re.test(document.getElementById(url).value))
		{
			document.getElementById(resultDiv).innerHTML="حروف وب سایت نامعتبر است.";
			return ;
		}
	}
	if(document.getElementById(CommentContent).value == '')
	{
		document.getElementById(resultDiv).innerHTML = 'متن ارسالی خالی است.';
		return ;
	}
    serverPage="commenting.aspx";
	document.getElementById(postBtn).disabled=true;
	document.getElementById(loaderDiv).innerHTML=loaderUI;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				document.getElementById(loaderDiv).innerHTML="";
				if(xmlHttp.responseText == 'EmptyContent')
				{
					document.getElementById(resultDiv).innerHTML = 'متن ارسالی خالی است.';
					return;
				}
				if(xmlHttp.responseText == 'Success')
				{
					document.getElementById(resultDiv).innerHTML='نظر شما با موفقیت ارسال شد.';
					baShowUp(BlogID,PostID,'loaderImg', 'pContainer', 'resultText');
				}
				else
					document.getElementById(resultDiv).innerHTML=xmlHttp.responseText;
				document.getElementById(postBtn).disabled=false;
				document.getElementById(CommentContent).value = '';
				ClearFields(name,url,email,CommentContent,IsPrivateComment);
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('CommentContent=' + document.getElementById(CommentContent).value + '&name=' + document.getElementById(name).value + '&email=' + document.getElementById(email).value + '&url=' + document.getElementById(url).value + '&BlogID=' + BlogID + '&PostID=' + PostID + '&IsPrivateComment=' + _IsPrivateComment);
	}
}

function ClearFields(name,url,email,CommentContent,IsPrivateComment)
{
	document.getElementById(name).value='';
	document.getElementById(url).value='';
	document.getElementById(email).value='';
	document.getElementById(CommentContent).value='';
	document.getElementById(IsPrivateComment).checked=false;
}

function baShowUp(_BlogID, _PostID, loaderDiv, showBox, resultDiv)
	{
	serverPage='commenting.aspx';
	document.getElementById(loaderDiv).innerHTML = loaderUI;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'DoRefresh')
				{
					baShowUp(iPage, '1', loaderDiv, showBox, resultDiv);
					return;
				}
				document.getElementById(loaderDiv).innerHTML = "";
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					document.getElementById(resultDiv).innerHTML = 'نظری وجود ندارد.';
					document.getElementById(showBox).innerHTML = '';
					return;
				}
				document.getElementById(loaderDiv).innerHTML="";
				document.getElementById(showBox).innerHTML=xmlHttp.responseText;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('ShowMode=comments' + '&BlogID=' + _BlogID + '&PostID=' + _PostID);
	}
}

function CommentVerify(id,_PostID,_BlogID)
{
	if (confirm('آیا شما مطمئن هستید که می خواهید این نظر را تایید کنید؟'))
	{
		serverPage="commenting.aspx";
		document.getElementById('resultText').innerHTML = '';	
     	document.getElementById('loaderImg').innerHTML = loaderUI;
		document.getElementById('postBtn').disabled = true;
		document.getElementById('verify-' + id).style.display='none';
		if(createAjax())
		{
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = serverPage + "?BlogID=" + _BlogID + "&PostID=" + _PostID;
						return;
					}
					document.getElementById('loaderImg').innerHTML = "";
					if(xmlHttp.responseText == 'Success')
					{
						document.getElementById('postBtn').disabled = false;
						document.getElementById('resultText').innerHTML = 'این نظر با موفقیت تایید شد.';
						return;
					}
					else
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			xmlHttp.send('action=verify&CommentID=' + id  + '&BlogID=' + _BlogID + '&PostID=' + _PostID);
		}
	}
	return false;
}

function CommentDelete(id,_PostID,_BlogID)
{
	if (confirm('آیا شما مطمئن هستید که می خواهید این نظر را حذف کنید؟'))
	{
		serverPage="commenting.aspx";
		document.getElementById('resultText').innerHTML = '';	
     	document.getElementById('loaderImg').innerHTML = loaderUI;
		document.getElementById('postBtn').disabled = true;
		document.getElementById('delete-' + id).style.display='none';
		if(createAjax())
		{
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = serverPage + "?BlogID=" + _BlogID + "&PostID=" + _PostID;
						return;
					}
					document.getElementById('loaderImg').innerHTML = "";
					if(xmlHttp.responseText == 'Success')
					{
						document.getElementById('tb-' + id).style.display='none';
						document.getElementById('postBtn').disabled = false;
						document.getElementById('resultText').innerHTML = 'نظر انتخاب شده با موفقیت از سیستم حذف شد.';
						return;
					}
					else
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			xmlHttp.send('action=delete&CommentID=' + id  + '&BlogID=' + _BlogID + '&PostID=' + _PostID);
		}
	}
	return false;
}

function PreverifyActivation(_BlogID,_PostID,action)
{
	if (confirm('آیا شما مطمئن هستید؟'))
	{
		serverPage="commenting.aspx";
		document.getElementById('resultText').innerHTML = '';	
     	document.getElementById('loaderImg').innerHTML = loaderUI;
		if(createAjax())
		{
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = serverPage + "?BlogID=" + _BlogID + "&PostID=" + _PostID;
						return;
					}
					document.getElementById('loaderImg').innerHTML = "";
					if(xmlHttp.responseText == 'Success')
					{
						if(action=='activate')
						{
							document.getElementById('PreverifyActivate').style.display='none';
							document.getElementById('PreverifyDeactivate').style.display='block';
						}
						else
						{
							document.getElementById('PreverifyActivate').style.display='block';
							document.getElementById('PreverifyDeactivate').style.display='none';
						}
						document.getElementById('resultText').innerHTML = 'عملیات انتخاب شده با موفقیت انجام شد.';
						return;
					}
					else
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			xmlHttp.send('action=' + action + '&BlogID=' + _BlogID + '&PostID=' + _PostID);
		}
	}
	return false;
}