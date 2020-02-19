using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace GIBS.FlexMLS_Agent.Components
{
    public class FlexMLS_AgentController : ISearchable, IPortable
    {

        #region public method



        public List<FlexMLS_AgentInfo> FlexMLS_Agent_GetUsersByRoleName(int portalID, string role)
        {
            return CBO.FillCollection<FlexMLS_AgentInfo>(DataProvider.Instance().FlexMLS_Agent_GetUsersByRoleName(portalID, role));
        }

        public List<FlexMLS_AgentInfo> FlexMLS_Agent_GetBuyers(int portalID, string role, int agentUserID)
        {
            return CBO.FillCollection<FlexMLS_AgentInfo>(DataProvider.Instance().FlexMLS_Agent_GetBuyers(portalID, role, agentUserID));
        }

        public void FlexMLS_Agent_Insert_Or_Update(FlexMLS_AgentInfo info)
        {
            //check we have some content to store
            if (info.AgentUserID != Null.NullInteger)
            {
                DataProvider.Instance().FlexMLS_Agent_Insert_Or_Update(info.ModuleId, info.BuyerUserID, info.AgentUserID, info.TeamID);
            }
        }

        public FlexMLS_AgentInfo FlexMLS_Agent_Select(int buyerUserID)
        {
            return (FlexMLS_AgentInfo)CBO.FillObject(DataProvider.Instance().FlexMLS_Agent_Select(buyerUserID), typeof(FlexMLS_AgentInfo));
        }



        /// <summary>
        /// Gets all the FlexMLS_AgentInfo objects for items matching the this moduleId
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<FlexMLS_AgentInfo> GetFlexMLS_Agents(int moduleId)
        {
            return CBO.FillCollection<FlexMLS_AgentInfo>(DataProvider.Instance().GetFlexMLS_Agents(moduleId));
        }

        /// <summary>
        /// Get an info object from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        /// 

        public FlexMLS_AgentInfo GetFlexMLS_Agent(int moduleId, int itemId)
        {
            return (FlexMLS_AgentInfo)CBO.FillObject(DataProvider.Instance().GetFlexMLS_Agent(moduleId, itemId), typeof(FlexMLS_AgentInfo));
        }


        /// <summary>
        /// Adds a new FlexMLS_AgentInfo object into the database
        /// </summary>
        /// <param name="info"></param>
        public void AddFlexMLS_Agent(FlexMLS_AgentInfo info)
        {
            //check we have some content to store
            if (info.Name != string.Empty)
            {
                DataProvider.Instance().AddFlexMLS_Agent(info.ModuleId, info.Name, info.CreatedByUser);
            }
        }

        /// <summary>
        /// update a info object already stored in the database
        /// </summary>
        /// <param name="info"></param>
        public void UpdateFlexMLS_Agent(FlexMLS_AgentInfo info)
        {
            //check we have some content to update
            if (info.Name != string.Empty)
            {
                DataProvider.Instance().UpdateFlexMLS_Agent(info.ModuleId, info.ItemId, info.Name, info.CreatedByUser);
            }
        }


        /// <summary>
        /// Delete a given item from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public void DeleteFlexMLS_Agent(int moduleId, int itemId)
        {
            DataProvider.Instance().DeleteFlexMLS_Agent(moduleId, itemId);
        }


        #endregion

        #region ISearchable Members

        /// <summary>
        /// Implements the search interface required to allow DNN to index/search the content of your
        /// module
        /// </summary>
        /// <param name="modInfo"></param>
        /// <returns></returns>
        public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(ModuleInfo modInfo)
        {
            SearchItemInfoCollection searchItems = new SearchItemInfoCollection();

            List<FlexMLS_AgentInfo> infos = GetFlexMLS_Agents(modInfo.ModuleID);

            foreach (FlexMLS_AgentInfo info in infos)
            {
                SearchItemInfo searchInfo = new SearchItemInfo(modInfo.ModuleTitle, info.Name, info.CreatedByUser, info.CreatedDate,
                                                    modInfo.ModuleID, info.ItemId.ToString(), info.Name, "Item=" + info.ItemId.ToString());
                searchItems.Add(searchInfo);
            }

            return searchItems;
        }

        #endregion

        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public string ExportModule(int moduleID)
        {
            StringBuilder sb = new StringBuilder();

            List<FlexMLS_AgentInfo> infos = GetFlexMLS_Agents(moduleID);

            if (infos.Count > 0)
            {
                sb.Append("<FlexMLS_Agents>");
                foreach (FlexMLS_AgentInfo info in infos)
                {
                    sb.Append("<FlexMLS_Agent>");
                    sb.Append("<content>");
                    sb.Append(XmlUtils.XMLEncode(info.Name));
                    sb.Append("</content>");
                    sb.Append("</FlexMLS_Agent>");
                }
                sb.Append("</FlexMLS_Agents>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            XmlNode infos = DotNetNuke.Common.Globals.GetContent(Content, "FlexMLS_Agents");

            foreach (XmlNode info in infos.SelectNodes("FlexMLS_Agent"))
            {
                FlexMLS_AgentInfo FlexMLS_AgentInfo = new FlexMLS_AgentInfo();
                FlexMLS_AgentInfo.ModuleId = ModuleID;
                FlexMLS_AgentInfo.Name = info.SelectSingleNode("name").InnerText;
                FlexMLS_AgentInfo.CreatedByUser = UserID;

                AddFlexMLS_Agent(FlexMLS_AgentInfo);
            }
        }

        #endregion
    }
}
