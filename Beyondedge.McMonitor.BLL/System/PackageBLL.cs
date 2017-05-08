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
    public class PackageBLL
    {
        private ConfigSetting m_ConfigSetting;
        private FileUtility m_FileUtility;
        public PackageBLL()
        {
            this.m_ConfigSetting = new ConfigSetting();
            this.m_FileUtility = new FileUtility();
        }

        public PackageXML GetData()
        {
            PackageXML m_PackageXML;
            try
            {
                string m_XmlFile = this.m_FileUtility.ValidateReadAllText(this.m_ConfigSetting.GetStoreAgentPackagePath());
                m_PackageXML = XmlUtility.DeSerialize<PackageXML>(m_XmlFile);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
                m_PackageXML = null;
            }
            return m_PackageXML;
        }

        public void UpdateData(PackageXML PackageXML)
        {
            try
            {
                var m_PackageXML = XmlUtility.Serialization<PackageXML>(PackageXML);
                File.WriteAllText(this.m_ConfigSetting.GetStoreAgentPackagePath(), m_PackageXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
            }
        }
    }
}
