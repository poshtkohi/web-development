﻿/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function charCounter(textBox, showDiv, disBtn){
	if(450-document.getElementById(textBox).value.length>=0){
		document.getElementById(showDiv).innerHTML=parseInt(450-document.getElementById(textBox).value.length)+' کاراکتر باقی مانده ';
		document.getElementById(disBtn).disabled=false;
	}else{
		document.getElementById(showDiv).innerHTML="بیشتر از 450 کاراکتر مجاز نیستید!";
		document.getElementById(disBtn).disabled=true;
	}
}