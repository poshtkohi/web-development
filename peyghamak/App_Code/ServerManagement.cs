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
using System.Web.UI;

namespace Peyghamak.ServerManagement
{
	//======================================================================================================================
	public class IIS : IDisposable
	{
		private Component component = new Component();
		private bool disposed = false;
        private string username = null;
        private string password = null;
        private string serverName = "localhost";
		//--------------------------------------------------------
		public IIS()
		{
		}
        //--------------------------------------------------------
        public IIS(string serverName, string username, string password)
        {
            this.serverName = serverName;
            this.username = username;
            this.password = password;
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
                    this.username = null;
                    this.password = null;
                    this.serverName = null;
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
		public int BindSubdomain(string subdomain, string IP, string port, string ZoneName)
		{
			/*try
			{*/
				bool find = false;
				DirectoryEntry root = new DirectoryEntry(String.Format("IIS://{0}/W3SVC", this.serverName));
                if (this.username != null && this.username != "")
                {
                    root.Username = this.username;
                    root.Password = this.password;
                }
				foreach(DirectoryEntry e in root.Children)
				{
					if(e.SchemaClassName == "IIsWebServer")
					{
						if(e.Properties["ServerComment"].Value.ToString() == ZoneName)
						{
							PropertyValueCollection pvc = e.Properties["ServerBindings"];
                            pvc.Add(String.Format("{0}:{1}:{2}.{3}", IP, port, subdomain, ZoneName));
                            pvc.Add(String.Format("{0}:{1}:{2}.{3}", IP, port, "www." + subdomain, ZoneName)); // For Example: www.test.iranblog.com
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
            DirectoryEntry root = new DirectoryEntry(String.Format("IIS://{0}/W3SVC", this.serverName));
            if (this.username != null && this.username != "")
            {
                root.Username = this.username;
                root.Password = this.password;
            }
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
		public int DeleteBoundSubdomain(string subdomain, string ZoneName)
		{   // -1 retuened meaning not found web site: -3 happened exception: 0 ok for delete operation
			try
			{
                DirectoryEntry root = new DirectoryEntry(String.Format("IIS://{0}/W3SVC", this.serverName));
                if (this.username != null && this.username != "")
                {
                    root.Username = this.username;
                    root.Password = this.password;
                }
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
                                if (ServerComment.IndexOf(subdomain + "." + ZoneName) > 0)
								{
									pvc.Remove(ServerComment);
									e.CommitChanges();									
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
		public int IIsStop(string subdomain) //-1 returend meaning not found website in active directory
		{
			try
			{
                DirectoryEntry root = new DirectoryEntry(String.Format("IIS://{0}/W3SVC", this.serverName));
                if (this.username != null && this.username != "")
                {
                    root.Username = this.username;
                    root.Password = this.password;
                }
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
                DirectoryEntry root = new DirectoryEntry(String.Format("IIS://{0}/W3SVC", this.serverName));
                if (this.username != null && this.username != "")
                {
                    root.Username = this.username;
                    root.Password = this.password;
                }
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
            DirectoryEntry root = new DirectoryEntry(String.Format("IIS://{0}/W3SVC", this.serverName));
            if (this.username != null && this.username != "")
            {
                root.Username = this.username;
                root.Password = this.password;
            }
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
		private string username = null;
        private string password = null;
		//--------------------------------------------------------
		public DNS()
		{
		}
        //--------------------------------------------------------
        public DNS(string username, string password)
        {            
            this.username = username;
            this.password = password;
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
                    this.username = null;
                    this.password = null;
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
                if (this.username != null && this.username != "")
                {
                    scope.Options.Username = this.username;
                    scope.Options.Password = this.password;
                }
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
                if (this.username != null && this.username != "")
                {
                    scope.Options.Username = this.username;
                    scope.Options.Password = this.password;
                }
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
                if (this.username != null && this.username != "")
                {
                    scope.Options.Username = this.username;
                    scope.Options.Password = this.password;
                }
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
                if (this.username != null && this.username != "")
                {
                    scope.Options.Username = this.username;
                    scope.Options.Password = this.password;
                }
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