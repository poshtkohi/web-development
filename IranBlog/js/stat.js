/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

function NG()
{
	var b=1;
	var o=0;
	var p=new Array("Shockwave Flash","Shockwave for Director","RealPlayer","QuickTime","VivoActive","LiveAudio","VRML","Dynamic HTML Binding","Windows Media Services");
	var np=navigator.plugins;
	for(var x=0;x<p.length;x++)
	{
		for(var i=0;i<np.length;i++)
			if(np[i].name.indexOf(p[x])>=0)o|=b;
		b*=2;
	}
	return o;
}
function IG()
{
	if(!document.body)document.write('<body>');
	var db=document.body;
	var o=0;
	var b=1;
	var p=new Array("D27CDB6E-AE6D-11CF-96B8-444553540000","2A202491-F00D-11CF-87CC-0020AFEECF20","23064720-C4F8-11D1-994D-00C04F98BBC9","","","","90A7533D-88FE-11D0-9DBE-0000C0411FC3","9381D8F2-0288-11D0-9501-00AA00B911A5","22D6F312-B0F6-11D0-94AB-0080C74C7E95");
	db.addBehavior("#default#clientcaps");
	for(var i=0;i<p.length;i++)
	{
		if(p[i])
			if(db.isComponentInstalled("{"+p[i]+"}","componentid"))o|=b;
		b*=2;
	}
	return o;
}
function iranblogstat(id,options)
{
	try{if(document.lastChild.innerHTML.indexOf("iranblogstat("+id,document.lastChild.innerHTML.indexOf("iranblogstat("+id)+1)>-1) return;}
	catch(e){}
	if (document.location.protocol=="file:") return;
	var _pDh = _pDomain(1);
	var n=navigator;
	var ver=n.appVersion;
	var d=document;
	var p2;
	var sc_title = "";
	var _w = "16", _h = "16";
	var verIE=parseInt(ver.substring(ver.indexOf("MSIE")+5,ver.indexOf("MSIE")+6));
	if(verIE>0)
		ver=verIE;
	else 
		ver=parseInt(ver);
		
	var u="http://www.persianstat.com/service/stats.aspx?id="+id;
	var r;
	if(options&2)r=top.document.referrer;
	else r=d.referrer;
		
	p2=r.indexOf(document.domain);
	if ((p2>=0) && (p2<=8)) { r=""; }
	if (r.indexOf("[")==0 && r.lastIndexOf("]")==(r.length-1)) { r=""; }
		
	if(r!="")u+="&r="+escape(r);
		
	if((n.appName=="Netscape"&&ver>=3))
		u+="&p="+NG();
				
	if(verIE>=5&&n.appVersion.indexOf('Win')>=0&&n.userAgent.indexOf('Opera')<0)
		u+="&p="+IG();
				
	if(ver>=4)
	{
		var s=screen;
		var w=s.width;
		var h=s.height;
		var c=s.colorDepth;
		if(w)u+="&w="+w;
		if(h)u+="&h="+h;
		if(c)u+="&c="+c;
	}
	u+="&v=1&y=1";
	if(_pgQ("__pUU") == _pDh) 
	{
		u+="&u=0";
	}
	else
	{
		_psQ("__pUU",_pDh,10,"/","","");
		u+="&u=1";
	}
	u+="&t="+document.location.pathname+document.location.search+"&i="+options;
	if(options == 4){_w="0";_h="0";}
	else if(options == 8){_w="80";_h="13";}
	sc_title = ""+document.title;
	sc_title = sc_title.substring(0, 100); 
	u+="&l="+escape(sc_title);

d.write('<iframe src="'+u+'" height="0" width="0" scrolling="no" marginwidth="0" allowtransparency="true" frameborder="no" marginheight="0" name="PersianStat"></iframe>');
}
function _psQ( name, value, expires, path, domain, secure ) 
{
var today = new Date();
today.setTime( today.getTime() );
expires = expires * 1000 * 60;
var expires_date = new Date( today.getTime() + (expires) );
document.cookie = name + "=" +escape( value ) +
( ( expires ) ? ";expires=" + expires_date.toGMTString() : "" ) +
( ( path ) ? ";path=" + path : "" ) + 
( ( domain ) ? ";domain=" + domain : "" ) +
( ( secure ) ? ";secure" : "" );
}
function _pHash(dm) {
if (!dm || dm=="") return 1;
var h=0,g=0;
for (var i=dm.length-1;i>=0;i--) {
var c=parseInt(dm.charCodeAt(i));
h=((h << 6) & 0xfffffff) + c + (c << 14);
if ((g=h & 0xfe00000)!=0) h=(h ^ (g >> 21));
}
return h;
}
function _pDomain(i) 
{
var dmn=document.domain;
if (dmn.substring(0,4)=="www.") {
dmn=dmn.substring(4,dmn.length);
}
if(i==1){return _pHash(dmn+document.location.pathname);}
else return dmn;
}
function _pgQ( name ) {
var start = document.cookie.indexOf( name + "=" );
var len = start + name.length + 1;
if ( ( !start ) &&
( name != document.cookie.substring( 0, name.length ) ) )
{
return null;
}
if ( start == -1 ) return null;
var end = document.cookie.indexOf( ";", len );
if ( end == -1 ) end = document.cookie.length;
return unescape( document.cookie.substring( len, end ) );
return null;
}
function _uvoid() { return; }


function stat()
{
        iranblogstat(10015254, 0);
         return ;
}
function statabout()
{
        iranblogstat(10015268, 0);
         return ;
}