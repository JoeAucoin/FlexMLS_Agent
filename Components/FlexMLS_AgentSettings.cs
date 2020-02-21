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
    public class FlexMLS_AgentSettings : ModuleSettingsBase
    {


        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>
        /// 

        //GoogleMapAPIKey  txtGoogleMapAPIKey
        public string GoogleMapAPIKey
        {
            get
            {
                if (Settings.Contains("GoogleMapAPIKey"))
                    return Settings["GoogleMapAPIKey"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "GoogleMapAPIKey", value.ToString());
            }
        }


        public string FlexMLSModule
        {
            get
            {
                if (Settings.Contains("FlexMLSModule"))
                    return Settings["FlexMLSModule"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "FlexMLSModule", value.ToString());
            }
        }


        public string AgentRole
        {
            get
            {
                if (Settings.Contains("AgentRole"))
                    return Settings["AgentRole"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "AgentRole", value.ToString());
            }
        }


        public string BrokerRole
        {
            get
            {
                if (Settings.Contains("BrokerRole"))
                    return Settings["BrokerRole"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "BrokerRole", value.ToString());
            }
        }


        public string BuyerRole
        {
            get
            {
                if (Settings.Contains("BuyerRole"))
                    return Settings["BuyerRole"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "BuyerRole", value.ToString());
            }
        }

        #endregion
    }
}
