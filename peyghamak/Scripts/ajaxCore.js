/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

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
function createAjaxObject()
{
  try
    {
    // Firefox, Opera 8.0+, Safari
    var _xmlHttp=new XMLHttpRequest();
	return _xmlHttp;
    }
  catch (e)
    {
    // Internet Explorer
    try
      {
      _xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
	  return _xmlHttp;
      }
    catch (e)
      {
      try
        {
        _xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
		return _xmlHttp;
        }
      catch (e)
        {
        alert("!نمیکند پشتیبانی را  AJAX مرورگر شما ");
        return null;
        }
      }
    }
  }

