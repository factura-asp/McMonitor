using Beyondedge.McMonitor.DAL.System;
using Beyondedge.McMonitor.Security;
using Beyondedge.McMonitor.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyondedge.McMonitor.BLL.System
{
    public class SystemLogBLL
    {
        private ConfigSetting m_ConfigSetting;
        private FileUtility m_FileUtility;
        public SystemLogBLL()
        {
            this.m_ConfigSetting = new ConfigSetting();
            this.m_FileUtility = new FileUtility();
        }

        public SystemLogXML GetData()
        {
            SystemLogXML m_SystemLogXML;
            try
            {
                string m_XmlFile = this.m_FileUtility.ValidateReadAllText(this.m_ConfigSetting.GetStoreAgentSystemLogPath());
                m_SystemLogXML = XmlUtility.DeSerialize<SystemLogXML>(m_XmlFile);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
                m_SystemLogXML = null;
            }
            return m_SystemLogXML;
        }

        public void UpdateData(SystemLogXML SystemLogXML)
        {
            try
            {
                var m_SystemLogXML = XmlUtility.Serialization<SystemLogXML>(SystemLogXML);
                File.WriteAllText(this.m_ConfigSetting.GetStoreAgentSystemLogPath(), m_SystemLogXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
            }
        }

        public void WriteSystemLog(string user, string storeCode, string description)
        {
            try
            {
                SystemLogXML m_SystemLogXML = GetData();

                LogHistory m_LogHistory = new LogHistory();
                m_LogHistory.User = user;
                m_LogHistory.StoreCode = storeCode;
                m_LogHistory.Description = description;
                m_LogHistory.CreatedDate = DateTime.Now;
                m_SystemLogXML.LogHistorys.Add(m_LogHistory);
                UpdateData(m_SystemLogXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex);
            }
        }

        public void CaptureUpDownTimeOfDevice(Device m_Device, string storeCode)
        {
            try
            {
                string user = "System";
                bool IsAvailableDevice = RemoteConnection.PingIP(m_Device.Ip);
                string description = string.Empty;
                if (IsAvailableDevice)
                {
                    description = string.Format("Device: {0} - Function Capture Up/DownTimeOfDevice: {1}", m_Device.Name, "Up");
                }
                else
                {
                    description = string.Format("Device: {0} - Function Capture Up/DownTimeOfDevice: {1}", m_Device.Name, "Down");
                }

                WriteSystemLog(user, storeCode, description);

            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex);
            }
        }
    }
}
