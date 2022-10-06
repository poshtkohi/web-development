//(c) Alireza Poshtkohi 2008, alireza.poshtkohi@gmail.com
function dynaframe()
{
	if (parent.document.getElementById('frameLeft').contentWindow.document.body.scrollHeight>500)
		parent.document.getElementById('frameLeft').height = parent.document.getElementById('frameLeft').contentWindow.document.body.scrollHeight +16;
	else
		parent.document.getElementById('frameLeft').height =500;

}
if (window.addEventListener)
window.addEventListener("load", dynaframe, false)
else if (window.attachEvent)
window.attachEvent("onload", dynaframe)