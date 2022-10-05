/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function toggleTabs(clickedBt){
	switch(clickedBt){
		case 'friends':
			document.getElementById('friends').src="http://www.peyghamak.com/images/Buttons/tab_f_e.png";
			document.getElementById('my').src="http://www.peyghamak.com/images/Buttons/tab_m_d.png";
			document.getElementById('private').src="http://www.peyghamak.com/images/Buttons/tab_p_d.png";
			document.getElementById('starred').src="http://www.peyghamak.com/images/Buttons/tab_s_d.png";
		break;
		case 'my':
			document.getElementById('friends').src="http://www.peyghamak.com/images/Buttons/tab_f_d.png";
			document.getElementById('my').src="http://www.peyghamak.com/images/Buttons/tab_m_e.png";
			document.getElementById('private').src="http://www.peyghamak.com/images/Buttons/tab_p_d.png";
			document.getElementById('starred').src="http://www.peyghamak.com/images/Buttons/tab_s_d.png";
		break;
		case 'private':
			document.getElementById('friends').src="http://www.peyghamak.com/images/Buttons/tab_f_d.png";
			document.getElementById('my').src="http://www.peyghamak.com/images/Buttons/tab_m_d.png";	
			document.getElementById('private').src="http://www.peyghamak.com/images/Buttons/tab_p_e.png";
			document.getElementById('starred').src="http://www.peyghamak.com/images/Buttons/tab_s_d.png";
		break;
		case 'starred':
			document.getElementById('friends').src="http://www.peyghamak.com/images/Buttons/tab_f_d.png";
			document.getElementById('my').src="http://www.peyghamak.com/images/Buttons/tab_m_d.png";
			document.getElementById('private').src="http://www.peyghamak.com/images/Buttons/tab_p_d.png";
			document.getElementById('starred').src="http://www.peyghamak.com/images/Buttons/tab_s_e.png";
		break;
	}	
	document.getElementById('gAnchor').focus();
}