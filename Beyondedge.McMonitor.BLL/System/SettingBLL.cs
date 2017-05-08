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
    public class SettingBLL
    {
        private ConfigSetting m_ConfigSetting;
        private FileUtility m_FileUtility;
        public SettingBLL()
        {
            this.m_ConfigSetting = new ConfigSetting();
            this.m_FileUtility = new FileUtility();
        }

        public SettingXML GetData()
        {
            SettingXML m_SettingXML;
            try
            {
                string m_XmlFile = this.m_FileUtility.ValidateReadAllText(this.m_ConfigSetting.GetStoreAgentSettingPath());
                m_SettingXML = XmlUtility.DeSerialize<SettingXML>(m_XmlFile);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
                m_SettingXML = null;
            }
            return m_SettingXML;
        }

        public void UpdateData(SettingXML SettingXML)
        {
            try
            {
                var m_SettingXML = XmlUtility.Serialization<SettingXML>(SettingXML);
                File.WriteAllText(this.m_ConfigSetting.GetStoreAgentSettingPath(), m_SettingXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
            }
        }
    }
}
