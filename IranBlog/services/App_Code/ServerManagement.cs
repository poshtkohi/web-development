/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.IO;
using System.Text;
using System.Management;
using System.ComponentModel;
using System.DirectoryServices;
using AlirezaPoshtkoohiLibrary;
using System.Web.UI;

namespace ServerManagement
{
	//======================================================================================================================
	public class IIS : IDisposable
	{
		private Component component = new Component();
		private bool disposed = false;
		//--------------------------------------------------------
		public IIS(string password)
		{
			if(password != constants.password)
			{
				this.Dispose();
				throw new Exception("You arenot authorized for using this Class Library " +
                                       "All rights reserved to Mr.Alireza Poshtkoohi");
			}
		}
		//--------------------------------------------------------
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		//--------------------------------------------------------
		private void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					component.Dispose();
				}           
			}
			disposed = true;         
		}
		//--------------------------------------------------------
		~IIS()      
		{
			Dispose(false);
		}
		//--------------------------------------------------------
		public void CloseHandleActiveDirectory(DirectoryEntry e)
		{
			if(e != null)
			{
				e.Close();
				e.Dispose();
			}
			return ;
		}
		//--------------------------------------------------------
		public int IISShow(Page page) // -1 returend meaning not found website in active directory
		{
			
			DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
			foreach(DirectoryEntry e in root.Children)
			{
				if(e.SchemaClassName == "IIsWebServer")
				{
					page.Response.Write(e.Properties["ServerComment"].Value.ToString() + " : " +"\n");
				}
			}
			return 0;
		}
		//--------------------------------------------------------
		public int CreateWebSite(string WebSiteName, string PathToRoot, string IP, string port, string ZoneName, int template, bool CreateDir)
		{
			/*try
			{*/
				bool find = false;
				DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Properties["ServerComment"].Value.ToString() == ZoneName)
						{
							PropertyValueCollection pvc = e.Properties["ServerBindings"];
							pvc.Add(String.Format("{0}:{1}:{2}.{3}", IP, port, WebSiteName, ZoneName));
							pvc.Add(String.Format("{0}:{1}:{2}.{3}", IP, port, "www." + WebSiteName, ZoneName)); // For Example: www.test.iranblog.com
							e.CommitChanges();
							find = true;
							break;
						}
					}
				}
				if(!find)
				{
					root.Close();
					root.Dispose();
					return -3;
				}
				if(CreateDir)
				{
					Directory.CreateDirectory(PathToRoot + "\\" + WebSiteName);
					File.Copy(constants.TemplatesPath + "Template"+ template.ToString() + ".html",
						constants.RootDircetoryWeblogs + "\\" + WebSiteName + "\\Default.html", true);
				}
				root.Close();
				root.Dispose();
				return 0;
			/*}
			catch
			{
				return -3;
			}*/
		}
        //--------------------------------------------------------
        public int DomainBind(string domain, string IP, string port, string MainBlogZoneName)
        {
            /*try
            {*/
            bool find = false;
            DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
            foreach (DirectoryEntry e in root.Children)
            {
                if (e.SchemaClassName == "IIsWebServer")
                {
                    if (e.Properties["ServerComment"].Value.ToString() == MainBlogZoneName)
                    {
                        PropertyValueCollection pvc = e.Properties["ServerBindings"];
                        pvc.Add(String.Format("{0}:{1}:{2}", IP, port, domain));
                        pvc.Add(String.Format("{0}:{1}:{2}", IP, port, "www." + domain)); // For Example: www.test.iranblog.com
                        e.CommitChanges();
                        find = true;
                        break;
                    }
                }
            }
            if (!find)
            {
                root.Close();
                root.Dispose();
                return -3;
            }
            root.Close();
            root.Dispose();
            return 0;
            /*}
            catch
            {
                return -3;
            }*/
        }
		//--------------------------------------------------------
		/*public int CreateWebSite(string WebSiteName, string PathToRoot, string IP, string port, string ZoneName, int template, bool CreateDir)
		{
			try
			{
				if(CreateDir)
				{
					Directory.CreateDirectory(PathToRoot + "\\" + WebSiteName);
					Directory.CreateDirectory(PathToRoot + "\\" + WebSiteName + "\\bin");
				}
				DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
				// Find unused ID value for new web site
				int siteID = 1;
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Properties["ServerComment"].Value.ToString() == WebSiteName)
						{
							return -1;
						}
						else
						{
							int ID = Convert.ToInt32(e.Name);
							if(ID >= siteID)
							{
								siteID = ID + 1;
							}
						}
					}
				}
				ManagementScope scope = new System.Management.ManagementScope("\\\\localhost\\root\\MicrosoftIISv2");
				ManagementObject oW3SVC = new ManagementObject (scope, new ManagementPath (@"IIsWebService='W3SVC'"), null);
				ManagementBaseObject inputParameters = oW3SVC.GetMethodParameters ("CreateNewSite");
				inputParameters["ServerComment"] = WebSiteName;
				ManagementBaseObject[] serverBindings = new ManagementBaseObject[1];
				serverBindings[0] = CreateServerBinding(WebSiteName + "." + ZoneName, IP, port);
				inputParameters["ServerBindings"] = serverBindings;
				inputParameters["PathOfRootVirtualDir"] = PathToRoot + "\\" + WebSiteName;
				inputParameters["ServerId"] = siteID;
				ManagementBaseObject outParameter = null;
				outParameter = oW3SVC.InvokeMethod ("CreateNewSite", inputParameters, null);
				outParameter.Dispose();
				oW3SVC.Dispose();
				inputParameters.Dispose();
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Name.ToString() == siteID.ToString())
						{
							e.Invoke("Put", "DefaultDoc", "Default.aspx, index.html");
							//e.Invoke("Put", "SecureBindings", IP + ":443:" + WebSiteName + "." + ZoneName;
							//e.Invoke("Put", "ServerSize", 1);
							e.Invoke("Put", "ServerAutoStart", 1);
							e.Invoke("SetInfo");
							e.Invoke("Start");
							e.Properties["AppIsolated"][0] = 2;
							//AccessExecute = 0x00000004
							//AccessRead = 0x00000001
							//AccessScript = 0x00000200
							//AccessSource = 0x00000010
							//AccessWrite = 0x00000002
							e.Properties["AccessFlags"][0] = 0x00000001 | 0x00000200;
							e.Properties["FrontPageWeb"][0] = 1;
							e.Properties["AppRoot"][0] = "LM/W3SVC/" + siteID + "/Root";
							e.Properties["AppFriendlyName"][0] = "Root";
							//AuthAnonymous = 0x00000001
							//AuthNTLM = 0x00000004
							e.Properties["AuthFlags"][0] = 0x00000001 | 0x00000004;
							//e.Properties["DirBrowseFlags"][0] = 0x00000002|0x00000004|0x00000008|
							//	                         0x00000010|0x00000020|0x40000000;
							e.CommitChanges();
							e.Close();
							e.Dispose();
							if(CreateDir)
							{
								File.Copy(constants.TemplatesPath + "Template"+ template.ToString() + ".html",
									constants.RootDircetoryWeblogs + "\\" + WebSiteName + "\\Default.html", true);
								File.Copy(constants.TemplatesPath + "Default.aspx", 
									constants.RootDircetoryWeblogs + "\\" + WebSiteName + "\\Default.aspx", true);
								File.Copy(constants.UsersFilesPath + "\\Web.config" , 
									constants.RootDircetoryWeblogs + "\\" + WebSiteName + "\\Web.config", true);
								File.Copy(constants.UsersFilesPath + "\\BlogContent.dll" , 
									constants.RootDircetoryWeblogs + "\\" + WebSiteName + "\\bin\\BlogContent.dll", true);
							}
							root.Close();
							root.Dispose();
							return 0;
						}
					}
				}
				root.Close();
				root.Dispose();
				return -1;
			}
			catch
			{
				return -3;
			}
		}*/
		//--------------------------------------------------------
		public int ChangeTemplate(string subdomain, int template)
		{
			try
			{
				File.Copy(constants.TemplatesPath + "Template" + template.ToString() + ".html",
					constants.RootDircetoryWeblogs + "\\" + subdomain + "\\Default.html", true);
				/*File.Copy(constants.TemplatesPath + "Default.aspx",
					constants.RootDircetoryWeblogs + "\\" + subdomain + "\\Default.aspx", true);
				File.Copy(constants.UsersFilesPath + "\\BlogContent.dll" , 
					constants.RootDircetoryWeblogs + "\\" + subdomain + "\\bin\\BlogContent.dll", true);*/
				return 0;
			}
			catch
			{
				/*this.IISStartStop("Start", e);
				e.Close();
				e.Dispose();*/
				return -3;
			}
		}
		//--------------------------------------------------------
		public int EditTemplate(string subdomain, string buffer)
		{
			/*DirectoryEntry e = this.SearchActiveDirectory(subdomain);
			if(e  == null)
			{
				return -3;
			}
			if(this.IISStartStop("Stop", e) < 0)
			{
				return -3;
			}*/
			try
			{
				StreamWriter sr = File.CreateText(constants.RootDircetoryWeblogs + "\\" + subdomain + "\\Default.html");
				sr.Write(buffer);
				sr.Close();
				/*this.IISStartStop("Start", e);
				e.Close();
				e.Dispose();*/
				return 0;
			}
			catch
			{
				/*this.IISStartStop("Start", e);
				e.Close();
				e.Dispose();*/
				return -3;
			}
		}
		//--------------------------------------------------------
		/*private ManagementObject CreateServerBinding(string HostName, string IP, string Port)
		{
			ManagementScope scope = new System.Management.ManagementScope("\\\\localhost\\root\\MicrosoftIISv2");
			ManagementClass classBinding = new ManagementClass (scope, new ManagementPath ("ServerBinding"), null);
			ManagementObject serverBinding = classBinding.CreateInstance();
			serverBinding.Properties["Hostname"].Value = HostName;
			serverBinding.Properties["IP"].Value = IP;
			serverBinding.Properties["Port"].Value = Port;
			serverBinding.Put();
			classBinding.Dispose();
			return serverBinding;
		}*/
		//--------------------------------------------------------
		public int DeleteWebSite(string WebSiteName, string ZoneName)
		{   // -1 retuened meaning not found web site: -3 happened exception: 0 ok for delete operation
			try
			{
				DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
				int find = -1;
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Properties["ServerComment"].Value.ToString() == ZoneName)
						{
							PropertyValueCollection pvc = e.Properties["ServerBindings"];
							foreach(string ServerComment in pvc)
							{
								if(ServerComment.IndexOf(WebSiteName+"."+ZoneName) > 0)
								{
									pvc.Remove(ServerComment);
									e.CommitChanges();
									//Directory.Delete(constants.RootDircetoryWeblogs + "\\" + WebSiteName, true);
									find = 0;
									break;
								}
							}
							break;
						}
					}
				}
				root.Close();
				root.Dispose();
				return find;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
		/*public int DeleteWebSite(string WebSiteName)
		{   // -1 retuened meaning not found web site: -3 happened exception: 0 ok for delete operation
			DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
			int find = -1;
			try
			{
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Properties["ServerComment"].Value.ToString() == WebSiteName)
						{
							int ID = Convert.ToInt32(e.Name);
							root.Invoke("Delete", "IIsWebServer", ID);
							Directory.Delete(constants.RootDircetoryWeblogs + "\\" + WebSiteName, true);
							find = 0;
							e.Close();
							break;
						}
					}
				}
				root.Close();
				root.Dispose();
				return find;
			} 
			catch
			{
				root.Close();
				root.Dispose();
				return -3;
			}
		}*/
		//--------------------------------------------------------
		public int IIsStop(string subdomain) //-1 returend meaning not found website in active directory
		{
			try
			{
				DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Properties["ServerComment"].Value.ToString() == subdomain)
						{
							e.Invoke("Stop");
							e.CommitChanges();
							e.Close();
							root.Close();
							e.Dispose();
							root.Dispose();
							return 0;
						}
					}
				}
				return -1;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
		public int IIsStart(string subdomain) // -1 returend meaning not found website in active directory
		{
			try
			{
				DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Properties["ServerComment"].Value.ToString() == subdomain)
						{
							e.Invoke("Start");
							e.CommitChanges();
							e.Close();
							root.Close();
							e.Dispose();
							root.Dispose();
							return 0;
						}
					}
				}
				return -1;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
		public int IISStartStop(string method, DirectoryEntry e)
		{
			try
			{
				e.Invoke(method);
				e.CommitChanges();
				return 0;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
		public DirectoryEntry SearchActiveDirectory(string subdomain)
		{
			DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
			DirectoryEntry e = null;
			foreach(DirectoryEntry ee in root.Children)
			{
				if(ee.SchemaClassName == "IIsWebServer")
				{
					if(ee.Properties["ServerComment"].Value.ToString() == subdomain)
					{
						e = ee;
						break;
					}
				}
			}
			return e;
		}
		//--------------------------------------------------------
	}
	//=========================================================================================================================
	public class DNS : IDisposable
	{
		private Component component = new Component();
		private bool disposed = false;
		//--------------------------------------------------------
		public DNS(string password)
		{
			if(password != constants.password)
			{
				this.Dispose();
				throw new Exception("You arenot authorized for using this Class Library " +
					"All rights reserved to Mr.Alireza Poshtkoohi");
			}
		}
		//--------------------------------------------------------
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		//--------------------------------------------------------
		private void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					component.Dispose();
				}           
			}
			disposed = true;         
		}
		//--------------------------------------------------------
		~DNS()      
		{
			Dispose(false);
		}
		//--------------------------------------------------------
		public int CreateRecord(string DnsServerAddress, string AvalilableZoneName, string RecordName, string HostIP)
		{
			/*try
			{*/
				ManagementScope scope = new System.Management.ManagementScope("\\\\" + DnsServerAddress + "\\root\\microsoftdns");
				ManagementClass zone = new ManagementClass(scope, new ManagementPath("MicrosoftDNS_ResourceRecord"), null);
				ManagementBaseObject inputs = zone.GetMethodParameters("CreateInstanceFromTextRepresentation");
				ManagementBaseObject outputs;
				inputs["DnsServerName"] = DnsServerAddress;
				inputs["ContainerName"] = AvalilableZoneName; // for example iranblog.com
				//owner class  ttl  A  IP_v4_address
				/*if(AvalilableZoneName == "")
				{
					inputs["TextRepresentation"] = ".test.com IN A " + HostIP;
				}
				else 
				{
					inputs["TextRepresentation"] = RecordName + "." + AvalilableZoneName + ". IN A " + HostIP;
				}*/
                if (RecordName == "")
                    inputs["TextRepresentation"] = AvalilableZoneName + ". IN A " + HostIP;
                else
                    inputs["TextRepresentation"] = RecordName + "." + AvalilableZoneName + ". IN A " + HostIP;
				outputs = zone.InvokeMethod("CreateInstanceFromTextRepresentation", inputs, null);
				outputs.Dispose();
				inputs.Dispose();
				zone.Dispose();
				return 0;
			/*}
			catch
			{
				return -3;
			}*/
		}
		//--------------------------------------------------------
		public int DeleteRecord(string DnsServerAddress, string AvalilableZoneName, string RecordName)
		{            /* -1 returned meaning not found none related recvord name in the zone */
			try
			{
				string subdomain = RecordName + "." + AvalilableZoneName;
				ManagementScope scope = new ManagementScope("\\\\" + DnsServerAddress + "\\root\\microsoftdns");
				string query = "Select * From MicrosoftDNS_ResourceRecord WHERE OwnerName='" + 
					RecordName + "." + AvalilableZoneName + "'";
				ObjectQuery oq = new ObjectQuery(query);
				ManagementObjectSearcher mos = new ManagementObjectSearcher(scope, oq);
				ManagementObjectCollection moc = mos.Get();
				string ownername = "";//, temp = "";
				//int n = 0;
				foreach (ManagementObject mo in moc)
				{
					ownername = (string) mo.Properties["OwnerName"].Value;
					if(subdomain == ownername)
					{
						mo.Delete();
						moc.Dispose();
						return 0;
					}
					/*n = ownername.IndexOf('.');
					temp = ownername.Substring(0, n);
					this.page.Response.Write(temp);
					if(temp == RecordName)
					{
						mo.Delete();
						moc.Dispose();
						return 0;
					}*/
				}
				return -1;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
		public int CreateZone(string DnsServerAddress, string ZoneName, uint ZoneType)
		{
			/*  ZoneType 
				Data type: uint32

				Indicates the type of the Zone. Valid values are: 


				DS integrated 
				Primary 
				Secondary 
			*/
			/*try
			{*/
				//ZoneType : Primary or Secondary 
				ManagementScope scope = new System.Management.ManagementScope("\\\\" + DnsServerAddress + "\\root\\microsoftdns");
				ManagementClass zone = new ManagementClass(scope, new ManagementPath("MicrosoftDNS_Zone"), null);
				ManagementBaseObject inputs = zone.GetMethodParameters("CreateZone");
				ManagementBaseObject outputs = null;
				inputs["ZoneName"] = ZoneName;
				inputs["ZoneType"] = ZoneType;
				//inputs["DataFileName"] = name + ".dns";
				//string []IPs = new string[1];
				//IPs[0] = "127.0.0.1";
				//inputs["IpAddr"] = IPs;
				inputs["AdminEmailName"] = "alireza.poshtkohi@gmail.com";
				outputs = zone.InvokeMethod("CreateZone", inputs, null);
				outputs.Dispose();
				inputs.Dispose();
				zone.Dispose();
				return 0;
			/*}
			catch
			{
				return -3;
			}*/
		}
		//--------------------------------------------------------
		public int DeleteZone(string DnsServerAddress, string AvalilableZoneName)
		{            /* -1 returned meaning not found none related recvord name in the zone */
			try
			{
				ManagementScope scope = new ManagementScope("\\\\" + DnsServerAddress + "\\root\\microsoftdns");
				string query = "Select * From MicrosoftDNS_Zone WHERE Name='" + AvalilableZoneName + "'";
				ObjectQuery oq = new ObjectQuery(query);
				ManagementObjectSearcher mos = new ManagementObjectSearcher(scope, oq);
				ManagementObjectCollection moc = mos.Get();
				foreach (ManagementObject mo in moc)
				{
					if((string) mo.Properties["Name"].Value == AvalilableZoneName)
					{
						mo.Delete();
						moc.Dispose();
						mos.Dispose();
						return 0;
					}
				}
				return -1;
			}
			catch
			{
				return -3;
			}
		}
		//--------------------------------------------------------
	}
	//================================================================================================================================
}