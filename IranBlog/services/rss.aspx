<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rss.aspx.cs" Inherits="services.rss" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>
