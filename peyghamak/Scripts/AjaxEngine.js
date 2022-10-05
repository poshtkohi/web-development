/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//(c) Alireza Poshtkohi, 2008, alireza.poshtkohi@gmail.com
//-------------------------------------------------------------------
function DoStar(PostID)
{
	if (confirm('آیا شما مطمئن هستید؟'))
	{
		document.getElementById('resultText').innerHTML='';
		serverPage="my.aspx";
		document.getElementById('loaderImg').innerHTML=loaderUI;
		if(createAjax()){
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = 'http://www.peyghamak.com/signin.aspx';
						return;
					}
					document.getElementById('loaderImg').innerHTML="";
					if(xmlHttp.responseText == 'Success')
					{
						document.getElementById('resultText').innerHTML='پیغامک انتخاب شده به عنوان پیغامک برگزیده انتخاب شد.';
						document.getElementById('star-img'+PostID).src="http://www.peyghamak.com/images/start_selected.gif";
						document.getElementById('star-img'+PostID).title="این یک پیغامک برگزیده است.";
						document.getElementById('star-img'+PostID).onclick="";
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
	}
}
//-------------------------------------------------------------------