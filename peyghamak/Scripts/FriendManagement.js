/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function FriendManagement(field,action,FriendBlogID,mode)
{
	if (confirm('آیا شما مطمئن هستید؟'))
	{
		field.disabled=true;
		field.style.display='none';
		var f = document.createElement('form');
		f.style.display = 'none';
		field.parentNode.appendChild(f);
		f.method = 'POST';
		f.action = 'friends.aspx';
		var _action = document.createElement('input'); 
		_action.setAttribute('type', 'hidden');
		_action.setAttribute('name', 'action');
		_action.setAttribute('value', action);
		f.appendChild(_action);
		
		var _FriendBlogID = document.createElement('input'); 
		_FriendBlogID.setAttribute('type', 'hidden');
		_FriendBlogID.setAttribute('name', 'FriendBlogID');
		_FriendBlogID.setAttribute('value', FriendBlogID);
		f.appendChild(_FriendBlogID);
		
		var _mode = document.createElement('input'); 
		_mode.setAttribute('type', 'hidden');
		_mode.setAttribute('name', 'mode');
		_mode.setAttribute('value', mode);
		f.appendChild(_mode);
		
		f.submit(); 
	}
	return false;
}