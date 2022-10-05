/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//(c) Alireza Poshtkohi, 2008, alireza.poshtkohi@gmail.com

function PostDelete(id,mode)
{
	if (confirm('آیا شما مطمئن هستید که می خواهید این پیغامک را حذف کنید؟'))
	{
		serverPage="my.aspx";
		document.getElementById('resultText').innerHTML = '';	
     	document.getElementById('loaderImg').innerHTML = loaderUI;
		if(mode != 'starredFromGuestPage')
			document.getElementById('doPost').disabled = true;
		if(createAjax())
		{
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						window.location = 'http://www.peyghamak.com/signin.aspx';
						return;
					}
					document.getElementById('loaderImg').innerHTML = "";
					if(xmlHttp.responseText == 'Success')
					{
						if(mode=='starredFromGuestPage')
						{
     						document.getElementById('resultText').innerHTML = 'پیغامک انتخاب شده با موفقیت از لیست مورد علاقه اتان حذف شد.'
							baShowUp('guest','1', 'loaderImg','pContainer', 'resultText');
							return;
						}
						document.getElementById('doPost').disabled = false;
						document.getElementById('resultText').innerHTML = 'پیغامک انتخاب شده با موفقیت از سیستم حذف شد.';
						if(mode == 'my')
					    {
							if( parseInt(document.getElementById("PostNumLabel").innerHTML) > 0)
								document.getElementById("PostNumLabel").innerHTML = parseInt(document.getElementById("PostNumLabel").innerHTML)-1;
						}
						if(mode == 'private')
					    {
							if( parseInt(document.getElementById("PrivateMessagesNumLabel").innerHTML) > 0)
								document.getElementById("PrivateMessagesNumLabel").innerHTML = parseInt(document.getElementById("PrivateMessagesNumLabel").innerHTML)-1;
						}
						baShowUp(mode,'1', 'loaderImg', 'pContainer', 'resultText');
						if(mode == 'starred')
							ShowAllComments('1', 'ShowAllStarredComments');
						return;
					}
					else
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			if(mode == 'starredFromGuestPage')
			{
				xmlHttp.send('DeleteID=' + id + '&DeleteMode=starred');
			}
			else
				xmlHttp.send('DeleteID=' + id + '&DeleteMode=' + mode);
		}
	}
}