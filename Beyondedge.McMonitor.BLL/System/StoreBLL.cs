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
    public class StoreBLL
    {
        private ConfigSetting m_ConfigSetting;
        private FileUtility m_FileUtility;
        public StoreBLL()
        {
            this.m_ConfigSetting = new ConfigSetting();
            this.m_FileUtility = new FileUtility();
        }

        public StoreXML GetData()
        {
            StoreXML m_DeviceXML;
            try
            {
                string m_XmlFile = this.m_FileUtility.ValidateReadAllText(this.m_ConfigSetting.GetStoreAgentStorePath());
                m_DeviceXML = XmlUtility.DeSerialize<StoreXML>(m_XmlFile);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
                m_DeviceXML = null;
            }
            return m_DeviceXML;
        }

        public void UpdateData(StoreXML StoreXML)
        {
            try
            {
                var m_DeviceXML = XmlUtility.Serialization<StoreXML>(StoreXML);
                File.WriteAllText(this.m_ConfigSetting.GetStoreAgentStorePath(), m_DeviceXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
            }
        }
    }
}
