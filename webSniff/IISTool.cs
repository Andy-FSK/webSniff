using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.Administration;

namespace webSniff
{
    public  class IISTool
    {
        private readonly ServerManager serverManager;
        public IISTool()
        {
            serverManager = new Microsoft.Web.Administration.ServerManager();
        }
        /// <summary>
        /// 给IIS添加禁止IP限制
        /// 仅针对iis 7及以上版本
        /// 首先需要引入Microsoft.Web.Administration.dll
        /// 该文件位置在windows2008的\Windows\System32\inetsrv目录下
        /// 注意：生成的EXE文件必须以管理员身份运行
        /// </summary>
        /// <param name="ip"></param>
        /// 过 http://go.microsoft.com/fwlink/?LinkId=213942 
        public static void banIP(string ip)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                Configuration config = serverManager.GetApplicationHostConfiguration();
                ConfigurationSection ipSecuritySection = config.GetSection("system.webServer/security/ipSecurity");
                ConfigurationElementCollection ipSecurityCollection = ipSecuritySection.GetCollection();
                ConfigurationElement addElement = ipSecurityCollection.CreateElement("add");
                addElement["ipAddress"] = ip;
                ipSecurityCollection.Add(addElement);
                serverManager.CommitChanges();
            }
        }
        public static void allowIP(string ip)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                Configuration config = serverManager.GetApplicationHostConfiguration();
                ConfigurationSection ipSecuritySection = config.GetSection("system.webServer/security/ipSecurity");
                ConfigurationElementCollection ipSecurityCollection = ipSecuritySection.GetCollection();
                ConfigurationElement removeElement = ipSecurityCollection.CreateElement("remove");
                removeElement["ipAddress"] = ip;
                //ipSecurityCollection.Add(removeElement);
                ipSecurityCollection.Remove(removeElement);
                serverManager.CommitChanges();
            }
        }
        public  IEnumerable<string> GetSiteNames()
        {
            foreach (var item in GetWorkerProcesses())
            {
                yield return item.AppPoolName;
            }
        }
        public IEnumerable<WorkerProcess> GetWorkerProcesses()
        {
            return serverManager.WorkerProcesses;
        }
    }
}
