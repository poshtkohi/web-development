<%@ Page Language="C#" AutoEventWireup="true" CodeFile="signout.aspx.cs" Inherits="blogbuilderv1_signout" %>
<%@import namespace="AlirezaPoshtkoohiLibrary"%>
<%
	if (AlirezaPoshtkoohiLibrary.blogcontent.DenialOfServiceProc(this))
		return;
%>