using com.LandonKey.SocksWebProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Alduin.Server.Services
{
    public class DownloadFileServices
    {
        public void DownLoadFileByWebRequest(string urlAddress, string filePath)
        {
            try
            {
                System.Net.HttpWebRequest request = null;
                System.Net.HttpWebResponse response = null;
                com.LandonKey.SocksWebProxy.Proxy.ProxyConfig socket5 = new com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, 9150, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five);
                request = (HttpWebRequest)WebRequest.Create(urlAddress);
                request.Proxy = new SocksWebProxy(socket5);
                request.Timeout = 30000;
                request.Method = "GET";
                request.KeepAlive = true;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();

                if (File.Exists(filePath))
                    File.Delete(filePath);

                byte[] inBuf = new byte[100000];
                int bytesToRead = System.Convert.ToInt32(inBuf.Length);
                int bytesRead = 0;

                while (bytesToRead > 0)
                {
                    int n = s.Read(inBuf, bytesRead, bytesToRead);
                    if (n == 0)
                        break;
                    bytesRead += n;
                    bytesToRead -= n;
                }
                FileStream fstr = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                fstr.Write(inBuf, 0, bytesRead);
                s.Close();
                fstr.Close();
            }
            catch {};
        }
    }
}