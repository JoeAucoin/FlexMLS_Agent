using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;

namespace GIBS.FlexMLS_Agent.Components
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class FlexMLS_AgentSettings
    {
        ModuleController controller;
        int tabModuleId;

        public FlexMLS_AgentSettings(int tabModuleId)
        {
            controller = new ModuleController();
            this.tabModuleId = tabModuleId;
        }

        protected T ReadSetting<T>(string settingName, T defaultValue)
        {
            Hashtable settings = controller.GetTabModuleSettings(this.tabModuleId);

            T ret = default(T);

            if (settings.ContainsKey(settingName))
            {
                System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                try
                {
                    ret = (T)tc.ConvertFrom(settings[settingName]);
                }
                catch
                {
                    ret = defaultValue;
                }
            }
            else
                ret = defaultValue;

            return ret;
        }

        protected void WriteSetting(string settingName, string value)
        {
            controller.UpdateTabModuleSetting(this.tabModuleId, settingName, value);
        }

        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>
        /// 

        //GoogleMapAPIKey  txtGoogleMapAPIKey
        public string GoogleMapAPIKey
        {
            get { return ReadSetting<string>("googleMapAPIKey", null); }
            set { WriteSetting("googleMapAPIKey", value); }
        }

        public string FlexMLSModule
        {
            get { return ReadSetting<string>("FlexMLSModule", null); }
            set { WriteSetting("FlexMLSModule", value); }
        }

        public string AgentRole
        {
            get { return ReadSetting<string>("agentRole", null); }
            set { WriteSetting("agentRole", value); }
        }

        public string BrokerRole
        {
            get { return ReadSetting<string>("brokerRole", null); }
            set { WriteSetting("brokerRole", value); }
        }

        public string BuyerRole
        {
            get { return ReadSetting<string>("buyerRole", null); }
            set { WriteSetting("buyerRole", value); }
        }

        #endregion
    }
}
