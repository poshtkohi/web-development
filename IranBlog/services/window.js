	  agreeWindow = null;

				//-------------------------------------------
			    function AgreementWindow()
				{
				   leftpos = 0;
				   toppos = 0;
				   if (screen) {
				     leftpos = screen.width / 2 -220;					 
					 toppos = screen.height / 2 - 100;
					 }
				    agreeWindow = window.open("/services/agree.html", "AgreeWin", "statusbar=yes,width=440,height=200,left="+leftpos+",top="+toppos);
				}
				//-------------------------------------------	
				
				function closeWindow()
				{
					if (agreeWindow && !agreeWindow.closed) 
					{
						agreeWindow.close();
					}
					
					
				}