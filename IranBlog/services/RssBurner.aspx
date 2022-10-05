<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RssBurner.aspx.cs" Inherits="RssBurner" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>