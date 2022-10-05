<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Smilies.aspx.cs" Inherits="services.ChatBox.Smilies" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
<html><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8"><meta name="robots" content="noindex, nofollow"><title>IranBlog.com ChatBox Smilies</title><link rel="stylesheet" type="text/css" href="images/style.css"><script language="javascript">
onkey = false;
function smilie(txt) {
  if (window.opener) {
    cd = window.opener.document;
    ln = cd.forms[0].PostContent;

    if (ln.value == "پیام") ln.value = "";
    ln.focus();

    if (cd.selection) {
      sel = cd.selection.createRange();
      c = "\001";
      if (sel.text != null) sel.text = c;
      end = start = ln.value.indexOf(c);
      if (end == -1) end = start = ln.value.length;
      sel.moveStart('character',-1);
      sel.text = "";
    }
    else if(ln.selectionStart != "null") {
      start = ln.selectionStart;
      end = ln.selectionEnd;
    }
    
    spce = (ln.value.charAt(end) == " ")?true:false;
    txt = ((start == 0 || ln.value.charAt(start-1) == " ")?"":" ")+txt;
    txt = txt+((spce)?"":" ");

    ln.value = ln.value.substring(0, start)+txt+ln.value.substring(end);

    caret = start + txt.length + ((spce)?1:0);
    
    if (cd.selection) {
      sel.moveEnd ('character', -ln.value.length);
      sel.moveEnd ('character', caret);
      sel.moveStart ('character', caret);
      sel.select();
    }
    else if(ln.selectionStart != "null") {
      ln.selectionStart = caret;
      ln.selectionEnd = caret;
    }
    
    if (onkey) this.focus();
    else this.close();

  }
}

function getkey(e) {
  if (!e) var e = window.event;
  if (e.shiftKey) onkey = true;
  else onkey = false;
}


  window.opener.document.body.onunload = function () {window.close();}
  window.opener.onunload = function () {window.close();}
</script></head>
<body leftmargin="2" topmargin="2" rightmargin="2" bottommargin="2" class="mnbdy">
    <form id="form1" runat="server">
<script type="text/javascript">document.onkeydown = getkey;document.onkeyup = getkey;</script>
<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" align="center"><tr><td height="100%" valign="top">
<table border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
  <td align="center" class="stxt2"><strong>شکلک ها</strong></td>
</tr>
</table>

<br>
<table border="0" cellpadding="4" cellspacing="0" width="100%" id="smtbl">
<tr style="height:32px;"><td class="stxt" align="center"><img src="images/smilies/1/smile.gif" alt=":)" style="cursor:pointer;" onClick="smilie(':)');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/biggrin.gif" alt=":biggrin:" style="cursor:pointer;" onClick="smilie(':biggrin:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/toocool.gif" alt=":cool:" style="cursor:pointer;" onClick="smilie(':cool:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/grin.gif" alt=":D" style="cursor:pointer;" onClick="smilie(':D');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/glad.gif" alt=":glad:" style="cursor:pointer;" onClick="smilie(':glad:');"></a></td>
</tr><tr style="height:32px;"><td class="stxt" align="center"><img src="images/smilies/1/lol.gif" alt=":lol:" style="cursor:pointer;" onClick="smilie(':lol:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/tongue.gif" alt=":P" style="cursor:pointer;" onClick="smilie(':P');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/wink.gif" alt=";)" style="cursor:pointer;" onClick="smilie(';)');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/confused.gif" alt=":confused:" style="cursor:pointer;" onClick="smilie(':confused:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/cyclops.gif" alt=":cyclops:" style="cursor:pointer;" onClick="smilie(':cyclops:');"></a></td>
</tr><tr style="height:32px;"><td class="stxt" align="center"><img src="images/smilies/1/nuts.gif" alt=":nuts:" style="cursor:pointer;" onClick="smilie(':nuts:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/surprised.gif" alt=":o" style="cursor:pointer;" onClick="smilie(':o');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/quizzical.gif" alt=":quizzical:" style="cursor:pointer;" onClick="smilie(':quizzical:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/rollseyes.gif" alt=":roll:" style="cursor:pointer;" onClick="smilie(':roll:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/tired.gif" alt=":tired:" style="cursor:pointer;" onClick="smilie(':tired:');"></a></td>
</tr><tr style="height:32px;"><td class="stxt" align="center"><img src="images/smilies/1/zonked.gif" alt=":zonked:" style="cursor:pointer;" onClick="smilie(':zonked:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/unsure.gif" alt=":/" style="cursor:pointer;" onClick="smilie(':/');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/sad.gif" alt=":(" style="cursor:pointer;" onClick="smilie(':(');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/aggrieved.gif" alt=":aggrieved:" style="cursor:pointer;" onClick="smilie(':aggrieved:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/aghast.gif" alt=":aghast:" style="cursor:pointer;" onClick="smilie(':aghast:');"></a></td>
</tr><tr style="height:32px;"><td class="stxt" align="center"><img src="images/smilies/1/cry.gif" alt=":cry:" style="cursor:pointer;" onClick="smilie(':cry:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/furious.gif" alt=":furious:" style="cursor:pointer;" onClick="smilie(':furious:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/nervous.gif" alt=":nervous:" style="cursor:pointer;" onClick="smilie(':nervous:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/angry.gif" alt=":x" style="cursor:pointer;" onClick="smilie(':x');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/frown.gif" alt=":|" style="cursor:pointer;" onClick="smilie(':|');"></a></td>
</tr><tr style="height:32px;"><td class="stxt" align="center"><img src="images/smilies/1/heart.gif" alt=":heart:" style="cursor:pointer;" onClick="smilie(':heart:');"></a></td>
<td class="stxt" align="center"><img src="images/smilies/1/thebox.gif" alt=":thebox:" style="cursor:pointer;" onClick="smilie(':thebox:');"></a></td>
</table>

<br>
</td></tr>
<tr><td>

<table border="0" cellpadding="2" cellspacing="0" width="100%">
<tr><td class="mnbdy" align="center">
                            <asp:PlaceHolder ID="CopyrightFooterControl" runat="server"></asp:PlaceHolder>
                            </td>
</tr>
</table>

</td></tr>
</table>
</form>
</body></html>