/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function showComments(divId){
	var ocbolTemp=divId;
	//if another comment box is open, close it
	if(ocbol!=undefined && ocbol!=ocbolTemp){
		if(document.getElementById(ocbol).rows.length>=2){
			document.getElementById(ocbol).deleteRow(1);
		}
	}
	//if comment box shown
	if(document.getElementById(ocbolTemp).rows.length <= 1){
		//insert row
		var x=document.getElementById(ocbolTemp).insertRow(1)
		//insert cell
		var y=x.insertCell(0);
		var w=x.insertCell(1);
		//fill it
		w.innerHTML="<div id='comment_box'><textarea class='commentBox' id='comment'></textarea></div>";
		w.align="left";
		var z=x.insertCell(2);
		//merge it as one cell
		//y.colSpan="3";
	}else{
		//toggle show/hide

		document.getElementById(ocbolTemp).deleteRow(1)
	}
	//assing current object ID to OCBOL
	ocbol=ocbolTemp;
}
