//BeautyAJAX
//(c) Alireza Poshtkohi 2008, alireza.poshtkohi@gmail.com
//AJAX kernel
var xmlHttp;
//Opened Comment Box Object Loader
var ocbol;
//AJAX image
var loaderUI="<img src='/services/ChatBox/images/loading.gif'>";
/*var helps=new Array("posting","LastPostEdit","PostArchive", "EditTemplate", "TeamWeblog", "LinkBox", "NewsLetter", "stat", "domain", "rss");

function iniHelp()
{
	for(i  = 0 ; i<helps.length ; i++)
		helps[i] = 'none';
}*/
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


function HelpFunction(filename)
{
/*	if(helps[filename] != 'none' || helps[filename] != 'undefined')
	{
	    document.getElementById('resultText').innerHTML = helps[filename];
		return;
	}*/
    serverPage='/services/blogbuilderv1/ControlPanelHelpFiles/' + filename + '.htm';
	document.getElementById('loaderImg').innerHTML=loaderUI;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				document.getElementById('loaderImg').innerHTML='';
				//helps[filename] = xmlHttp.responseText;
				document.getElementById('resultText').innerHTML = xmlHttp.responseText;
				return;
			}
		}
			//alert('1');
		xmlHttp.open("GET",serverPage,true);
		//xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send(null);
		//xmlHttp.close();
	}
}