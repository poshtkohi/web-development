/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//(c) Alireza Poshtkohi, 2008, alireza.poshtkohi@gmail.com

function CommentDelete(id,_PostID)
{
	if (confirm('آیا شما مطمئن هستید که می خواهید این نظر را حذف کنید؟'))
	{
		serverPage="comments.aspx";
		document.getElementById('resultText').innerHTML = '';	
     	document.getElementById('loaderImg').innerHTML = loaderUI;
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
						document.getElementById('doPost').disabled = false;
						document.getElementById('resultText').innerHTML = 'نظر انتخاب شده با موفقیت از سیستم حذف شد.';
						//if( parseInt(document.getElementById("CommentNumLabel").innerHTML) > 0)
						//	document.getElementById("CommentNumLabel").innerHTML = parseInt(document.getElementById("CommentNumLabel").innerHTML)-1;
						baShowUp('comments&PostID=' + _PostID,'1', 'loaderImg', 'pContainer', 'resultText');
						return;
					}
					else
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			xmlHttp.send('DeleteID=' + id  + "&PostID=" + _PostID);
		}
	}
	return false;
}