//(c) Alireza Poshtkohi 2008, alireza.poshtkohi@gmail.com
function dynaframe()
{
	if (parent.document.getElementById('frameLeft').contentWindow.document.body.scrollHeight>500)
		parent.document.getElementById('frameLeft').height = parent.document.getElementById('frameLeft').contentWindow.document.body.scrollHeight +16;
	else
		parent.document.getElementById('frameLeft').height =500;

}
if (window.addEventListener)
window.addEventListener("load", dynaframe, false)
else if (window.attachEvent)
window.attachEvent("onload", dynaframe)

//BeautyAJAX
//AJAX kernel
var xmlHttp;
//Opened Comment Box Object Loader
var ocbol;
//AJAX image
var loaderUI="<div align='center'><img src='/services/ChatBox/images/loading.gif'></div>";

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


function SmileyGenerator()
{
    serverPage='/services/blogbuilderv1/SmileyGenerator.aspx';
	document.getElementById('smiliesAjax').innerHTML=loaderUI;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				document.getElementById('smiliesAjax').innerHTML='';
				document.getElementById('smiliesAjax').innerHTML = xmlHttp.responseText;
				return;
			}
		}
		xmlHttp.open("GET",serverPage,true);
		//xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send(null);
		//xmlHttp.close();
	}
}