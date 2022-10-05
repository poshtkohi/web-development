<%@ Page Language="VB" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<script runat="server">
sub page_load()
Dim s As String = ""
	s &= "Request.UserAgent=" & Request.UserAgent  & "<br/>"
    With Request.Browser
        s &= "Browser Capabilities" & "<br/>"
        s &= "IsMobileDevice = " & .IsMobileDevice & "<br/>"
        s &= "Type = " & .Type & "<br/>"
        s &= "Name = " & .Browser & "<br/>"
        s &= "Version = " & .Version & "<br/>"
        s &= "Major Version = " & .MajorVersion & "<br/>"
        s &= "Minor Version = " & .MinorVersion & "<br/>"
        s &= "Platform = " & .Platform & "<br/>"
        s &= "Is Beta = " & .Beta & "<br/>"
        s &= "Is Crawler = " & .Crawler & "<br/>"
        s &= "Is AOL = " & .AOL & "<br/>"
        s &= "Is Win16 = " & .Win16 & "<br/>"
        s &= "Is Win32 = " & .Win32 & "<br/>"
        s &= "Supports Frames = " & .Frames & "<br/>"
        s &= "Supports Tables = " & .Tables & "<br/>"
        s &= "Supports Cookies = " & .Cookies & "<br/>"
        s &= "Supports VBScript = " & .VBScript & "<br/>"
        s &= "Supports JavaScript = " & _
            .EcmaScriptVersion.ToString() & "<br/>"
        s &= "Supports Java Applets = " & .JavaApplets & "<br/>"
        s &= "Supports ActiveX Controls = " & .ActiveXControls & _
            "<br/>"
    End With
    response.write(s)
	end sub
</script>
</body>
</html>
