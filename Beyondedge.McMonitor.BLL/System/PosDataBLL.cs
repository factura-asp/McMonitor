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
    public class PosDataBLL
    {
        private ConfigSetting m_ConfigSetting;
        private FileUtility m_FileUtility;
        public PosDataBLL()
        {
            this.m_ConfigSetting = new ConfigSetting();
            this.m_FileUtility = new FileUtility();
        }

        private PosDataXML GetData()
        {
            PosDataXML m_PosDataXML;
            try
            {
                string m_XmlFile = this.m_FileUtility.ValidateReadAllText(this.m_ConfigSetting.GetStoreAgentPosDataPath());
                m_PosDataXML = XmlUtility.DeSerialize<PosDataXML>(m_XmlFile);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
                m_PosDataXML = null;
            }
            return m_PosDataXML;
        }

        public void UpdateData(PosDataXML PosDataXML)
        {
            try
            {
                var m_PosDataXML = XmlUtility.Serialization<PosDataXML>(PosDataXML);
                File.WriteAllText(this.m_ConfigSetting.GetStoreAgentPosDataPath(), m_PosDataXML);
            }
            catch (Exception ex)
            {
                LogsUtility.WriteLogError(ex.ToString());
            }
        }
    }
}
