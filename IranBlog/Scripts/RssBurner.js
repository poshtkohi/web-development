/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//(c) Alireza Poshtkohi, 2007,  alireza.poshtkohi@gmail.com
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
function RssBurner(username)
	{
	serverPage='/services/RssBurner.aspx?username='+username;
	if(createAjax()){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				document.getElementById('PeyghamakRssContainer').innerHTML= xmlHttp.responseText;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		xmlHttp.send('username='+username);
	}
}