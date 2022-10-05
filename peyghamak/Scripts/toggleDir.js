/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function toggleImg(textBox,toggleImage,imgType){
	var img_1;
	var img_2;
	switch(imgType){
		case 'GN':
		img_1=langDirImg[0];
		img_2=langDirImg[1];
		break;
		case 'GR':
		img_1=langDirImg_gray[0];
		img_2=langDirImg_gray[1];
		break;
	}
	if(rightAlign){
		document.getElementById(textBox).style.textAlign="left";
		document.getElementById(textBox).style.direction="ltr";
		document.getElementById(toggleImage).src=img_1;
		document.getElementById("iDir").value="ltr";
		document.getElementById("iAlign").value="left";
	}else{
		document.getElementById(textBox).style.textAlign="right";
		document.getElementById(textBox).style.direction="rtl";		
		document.getElementById(toggleImage).src=img_2;
		document.getElementById("iDir").value="rtl";
		document.getElementById("iAlign").value="right";
	}
	rightAlign=!rightAlign;
}