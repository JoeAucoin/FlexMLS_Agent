using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using GIBS.FlexMLS_Agent.Components;
using System.Collections;
using System.Web.UI.WebControls;

namespace GIBS.Modules.FlexMLS_Agent
{
    public partial class Settings : ModuleSettingsBase
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {

                    BindModules();
                    GetRoles();

                    FlexMLS_AgentSettings settingsData = new FlexMLS_AgentSettings(this.TabModuleId);

                    if (settingsData.FlexMLSModule != null)
                    {
                        ddlFlexMLSModule.SelectedValue = settingsData.FlexMLSModule.ToString();
                        
                    }
                    
                    if (settingsData.AgentRole != null)
                    {
                        ddlAgentRole.SelectedValue = settingsData.AgentRole.ToString();

                    }
                    
                    if (settingsData.BrokerRole != null)
                    {
                        ddlBrokerRole.SelectedValue = settingsData.BrokerRole.ToString();

                    }
                    
                    if (settingsData.BuyerRole != null)
                    {
                        ddlBuyerRole.SelectedValue = settingsData.BuyerRole.ToString();

                    }

                    if (settingsData.GoogleMapAPIKey != null)
                    {
                        txtGoogleMapAPIKey.Text = settingsData.GoogleMapAPIKey.ToString();
                    }


                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        private void BindModules()
        {

            DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
            ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, "GIBS - FlexMLS");

            foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
            {
                if (!mi.IsDeleted)
                {
                    ListItem objListItem = new ListItem();

                    objListItem.Value = mi.TabID.ToString();    // mi.ModuleID.ToString();
                    objListItem.Text = mi.ModuleTitle.ToString();

                    ddlFlexMLSModule.Items.Add(objListItem);

                }
            }


            ddlFlexMLSModule.Items.Insert(0, new ListItem(Localization.GetString("SelectModule", this.LocalResourceFile), "-1"));
        }

        public void GetRoles()
        {
          //  ArrayList myRoles = new ArrayList();

            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();

            var myRoles = rc.GetRoles(this.PortalId);
          //  myRoles
            ddlAgentRole.DataSource = myRoles;
            ddlAgentRole.DataTextField = "RoleName";
            ddlAgentRole.DataValueField = "RoleName";
            ddlAgentRole.DataBind();

            // ADD FIRST (NULL) ITEM
            ListItem item = new ListItem();
            item.Text = "-- Select Role for Agents --";
            item.Value = "";
            ddlAgentRole.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlAgentRole.Items.Remove("Administrators");
            ddlAgentRole.Items.Remove("Registered Users");
            ddlAgentRole.Items.Remove("Subscribers");


            // BROKERS
            ddlBrokerRole.DataSource = myRoles;
            ddlBrokerRole.DataTextField = "RoleName";
            ddlBrokerRole.DataValueField = "RoleName";
            ddlBrokerRole.DataBind();

            // ADD FIRST (NULL) ITEM
            ListItem itembroker = new ListItem();
            itembroker.Text = "-- Select Role for Brokers --";
            itembroker.Value = "";
            ddlBrokerRole.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlBrokerRole.Items.Remove("Administrators");
            ddlBrokerRole.Items.Remove("Registered Users");
            ddlBrokerRole.Items.Remove("Subscribers");

            // BUYERS

            ddlBuyerRole.DataSource = myRoles;
            ddlBuyerRole.DataTextField = "RoleName";
            ddlBuyerRole.DataValueField = "RoleName";
            ddlBuyerRole.DataBind();

            // ADD FIRST (NULL) ITEM
            ListItem itembuyer = new ListItem();
            itembuyer.Text = "-- Select Role for Buyers --";
            itembuyer.Value = "";
            ddlBuyerRole.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlBuyerRole.Items.Remove("Administrators");
         //   ddlBuyerRole.Items.Remove("Registered Users");
            ddlBuyerRole.Items.Remove("Subscribers");
            


        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                FlexMLS_AgentSettings settingsData = new FlexMLS_AgentSettings(this.TabModuleId);
                settingsData.FlexMLSModule = ddlFlexMLSModule.SelectedValue.ToString();
                settingsData.AgentRole = ddlAgentRole.SelectedValue.ToString();
                settingsData.BrokerRole = ddlBrokerRole.SelectedValue.ToString();
                settingsData.BuyerRole = ddlBuyerRole.SelectedValue.ToString();
                settingsData.GoogleMapAPIKey = txtGoogleMapAPIKey.Text.ToString();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}