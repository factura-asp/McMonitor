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
    public class CongratulationBLL
    {
        private ConfigSetting m_ConfigSetting;
        private FileUtility m_FileUtility;
        public CongratulationBLL()
        {
            this.m_ConfigSetting = new ConfigSetting();
            this.m_FileUtility = new FileUtility();
        }

        public CongratulationXML GetData()
        {
            CongratulationXML m_CongratulationXML;
            try
            {
                string m_XmlFile = this.m_FileUtility.ValidateReadAllText(this.m_ConfigSetting.GetStoreAgentCongratulationPath());
                m_CongratulationXML = XmlUtility.DeSerialize<CongratulationXML>(m_XmlFile);
                
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex);
                m_CongratulationXML = null;
            }
            return m_CongratulationXML;
        }

        public void UpdateData(CongratulationXML CongratulationXML)
        {
            try
            {
                var m_CongratulationXML = XmlUtility.Serialization<CongratulationXML>(CongratulationXML);
                File.WriteAllText(this.m_ConfigSetting.GetStoreAgentCongratulationPath(), m_CongratulationXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex);
            }
        }
    }
}
