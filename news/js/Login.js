/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//Login.js
//Version: 2.1
//This script is created by Alireza Poshtkohi. Do not remove, modify, or hide the author information. keep it intact.
//Mail: alireza.poshtkohi@gmail.com 

//------------------------------------------------------------------------------------------------------------------
function LoginFormValidation()
{	
	var username = document.getElementById('username').value;
	var password = document.getElementById('password').value;

	if(username == '')
	{
		alert('.نام کاربری خالی است');
		document.getElementById('username').focus();
		document.getElementById('username').select();
		return false;
	}
	var re = /^[\-0-9a-zA-Z]{1,}$/;
	if(!re.test(username))
	{
	   	alert(".نام کاربری نامعتبر است");
	   	document.getElementById('username').focus();
		document.getElementById('username').select();
	   	return false;
	}
	if(password == '')
	{
		alert('.کلمه عبور خالی است');
		document.getElementById('password').focus();
		document.getElementById('password').select();
		return false;
	}
	return true;
}
//------------------------------------------------------------------------------------------------------------------