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
    public class DeviceBLL
    {
        private ConfigSetting m_ConfigSetting;
        private FileUtility m_FileUtility;
	//Testing Git Process
        public DeviceBLL()
        {
            this.m_ConfigSetting = new ConfigSetting();
            this.m_FileUtility = new FileUtility();
        }

        public DeviceXML GetData()
        {
            DeviceXML m_DeviceXML;
            try
            {
                string m_XmlFile = this.m_FileUtility.ValidateReadAllText(this.m_ConfigSetting.GetStoreAgentDevicePath());
                m_DeviceXML = XmlUtility.DeSerialize<DeviceXML>(m_XmlFile);
                foreach (var m_Device in m_DeviceXML.Devices)
                {
                    m_Device.Password = AESUtility.Decrypt(m_Device.Password);
                    m_Device.SecondPassword = AESUtility.Decrypt(m_Device.SecondPassword);
                }
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex);
                m_DeviceXML = null;
            }
            return m_DeviceXML;
        }

        public void UpdateData(DeviceXML DeviceXML)
        {
            try
            {
                foreach (var m_Device in DeviceXML.Devices)
                {
                    m_Device.Password = AESUtility.Encrypt(m_Device.Password);
                    m_Device.SecondPassword = AESUtility.Encrypt(m_Device.SecondPassword);
                }

                var m_DeviceXML = XmlUtility.Serialization<DeviceXML>(DeviceXML);
                File.WriteAllText(this.m_ConfigSetting.GetStoreAgentDevicePath(), m_DeviceXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex);
            }
        }
    }
}
