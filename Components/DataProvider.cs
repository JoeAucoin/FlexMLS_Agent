using System;
using System.Data;
using DotNetNuke;
using DotNetNuke.Framework;

namespace GIBS.FlexMLS_Agent.Components
{
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static DataProvider instance = null;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "GIBS.FlexMLS_Agent.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion


        #region Abstract methods

        /* implement the methods that the dataprovider should */

        public abstract IDataReader FlexMLS_Agent_GetUsersByRoleName(int portalID, string role);
        public abstract IDataReader FlexMLS_Agent_GetBuyers(int portalID, string role, int agentUserID);
        public abstract void FlexMLS_Agent_Insert_Or_Update(int moduleID, int buyerUserID, int agentUserID, int teamID);
        public abstract IDataReader FlexMLS_Agent_Select(int buyerUserID);


        public abstract IDataReader GetFlexMLS_Agents(int moduleId);
        public abstract IDataReader GetFlexMLS_Agent(int moduleId, int itemId);
        public abstract void AddFlexMLS_Agent(int moduleId, string content, int userId);
        public abstract void UpdateFlexMLS_Agent(int moduleId, int itemId, string content, int userId);
        public abstract void DeleteFlexMLS_Agent(int moduleId, int itemId);

        #endregion

    }



}
