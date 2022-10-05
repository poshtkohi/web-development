<%@ Application Language="C#" %>
<%@ Import Namespace="Peyghamak" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        if (this.Session["SigninSessionInfo"] == null)
        {
            this.Session["SigninSessionInfo"] = new SigninSessionInfo();
        }
        if (this.Session["IsLogined"] == null)
        {
            this.Session["IsLogined"] = false;
        }
        if (this.Session["SecurityCode"] == null)
        {
            this.Session["SecurityCode"] = "";
        }
        /*if (this.Session["GuestBlogID"] == null)
        {
            this.Session["GuestBlogID"] = -1;
        }
        if (this.Session["GuestBlogUsername"] == null)
        {
            this.Session["GuestBlogUsername"] = "";
        }*/
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>