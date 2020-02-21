using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using GIBS.FlexMLS_Agent.Components;
using DotNetNuke.Common;
using System.Data;

namespace GIBS.Modules.FlexMLS_Agent
{
    public partial class ViewFlexMLS_Agent : PortalModuleBase, IActionable
    {
        static string _Role = "";
        static string _AgentRole = "";
        static string _BrokerRole = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    LoadSettings();

                    LoadAgents();
                    LoadBuyersGrid();
                    lblDebug.Text = this.UserId.ToString() + " UserID";
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        //public void GetRoles()
        //{

        //    try
        //    {

        //        ArrayList myRoles = new ArrayList();

        //        DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();

        //        myRoles = rc.GetPortalRoles(this.PortalId);

        //        ddlRoles.DataSource = myRoles;
        //        ddlRoles.DataBind();

        //        // ADD FIRST (NULL) ITEM
        //        ListItem item = new ListItem();
        //        item.Text = "-- Select Role --";
        //        item.Value = "";
        //        ddlRoles.Items.Insert(0, item);
        //        // REMOVE DEFAULT ROLES
        //        ddlRoles.Items.Remove("Administrators");
        //        ddlRoles.Items.Remove("Registered Users");
        //        ddlRoles.Items.Remove("Subscribers");
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}

        public void LoadSettings()
        {

            try
            {

                if (Settings.Contains("BuyerRole"))
                {
                    _Role = Settings["BuyerRole"].ToString();
                }
                if (Settings.Contains("AgentRole"))
                {
                    _AgentRole = Settings["AgentRole"].ToString();
                }
                if (Settings.Contains("BrokerRole"))
                {
                    _BrokerRole = Settings["BrokerRole"].ToString();
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void LoadAgents()
        {

            try
            {

                List<FlexMLS_AgentInfo> items;
                FlexMLS_AgentController controller = new FlexMLS_AgentController();

                items = controller.FlexMLS_Agent_GetUsersByRoleName(this.PortalId, _AgentRole.ToString());

                ddlAgents.DataSource = items;
                ddlAgents.DataBind();

                // ADD FIRST (NULL) ITEM
                ListItem item = new ListItem();
                item.Text = "-> View All";
                item.Value = "0";
                ddlAgents.Items.Insert(0, item);

                if (ddlAgents.Items.FindByValue(this.UserId.ToString()) != null)
                {
                    // ... code here
                    ddlAgents.SelectedValue = this.UserId.ToString();
                    ddlAgents.Visible = false;
                    lblAgents.Visible = false;
                }

                if (UserInfo.IsInRole(_BrokerRole.ToString()))
                {
                    ddlAgents.Visible = true;
                    lblAgents.Visible = true;
                }
                

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void LoadBuyersGrid()
        {

            try
            {

                List<FlexMLS_AgentInfo> items;
                FlexMLS_AgentController controller = new FlexMLS_AgentController();

                items = controller.FlexMLS_Agent_GetBuyers(this.PortalId, _Role.ToString(), Int32.Parse(ddlAgents.SelectedValue.ToString()));

                GridView1.DataSource = items;
                GridView1.DataBind();

                lblTotalRecordCount.Text = items.Count.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                LoadBuyersGrid();
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }


        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            try
            {
                if (e.CommandName == "Delete")
                {
                    // get the ID of the clicked row

                    int ID = Convert.ToInt32(e.CommandArgument);
                    // Delete the record 

                    //FBFoodInventoryController controller = new FBFoodInventoryController();
                    //controller.FBInvoice_Delete(ID);
                    ////  DeleteRecordByID(ID);
                    //// Implement this on your own :) 

                    //txtInvoiceID.Value = "0";

                    //FillInvoiceGrid();

                }


                if (e.CommandName == "Edit")
                {
                    int BuyerID = Convert.ToInt32(e.CommandArgument);


                  //  int clientID = (int)GridVieSwearch.DataKeys[e.NewEditIndex].Value;
                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditBuyer", "mid=" + ModuleId.ToString() + "&BuyerID=" + BuyerID));



                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }



        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // No requirement to implement code here
            e.Cancel = true;
        }



        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                e.Cancel = true;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                     true, false);

                return actions;
            }
        }

        #endregion

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBuyersGrid();
        }

        protected void ddlAgents_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                LoadBuyersGrid();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
            
            
        }
        /// <summary>
        /// Handles the items being bound to the datalist control. In this method we merge the data with the
        /// template defined for this control to produce the result to display to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        //protected void lstContent_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        //{
        //    Label content = (Label)e.Item.FindControl("lblContent");
        //    string contentValue = string.Empty;

        //    FlexMLS_AgentSettings settingsData = new FlexMLS_AgentSettings(this.TabModuleId);

        //    if (settingsData.Template != null)
        //    {
        //        //apply the content to the template
        //        ArrayList propInfos = CBO.GetPropertyInfo(typeof(FlexMLS_AgentInfo));
        //        contentValue = settingsData.Template;

        //        if (contentValue.Length != 0)
        //        {
        //            foreach (PropertyInfo propInfo in propInfos)
        //            {
        //                object propertyValue = DataBinder.Eval(e.Item.DataItem, propInfo.Name);
        //                if (propertyValue != null)
        //                {
        //                    contentValue = contentValue.Replace("[" + propInfo.Name.ToUpper() + "]",
        //                            Server.HtmlDecode(propertyValue.ToString()));
        //                }
        //            }
        //        }
        //        else
        //            //blank template so just set the content to the value
        //            contentValue = Server.HtmlDecode(DataBinder.Eval(e.Item.DataItem, "Content").ToString());
        //    }
        //    else
        //    {
        //        //no template so just set the content to the value
        //        contentValue = Server.HtmlDecode(DataBinder.Eval(e.Item.DataItem, "Content").ToString());
        //    }

        //    content.Text = contentValue;
        //}

    }
}