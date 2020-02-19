using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace GIBS.FlexMLS_Agent.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "GIBS_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region override methods


        public override IDataReader FlexMLS_Agent_GetUsersByRoleName(int portalID, string role)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FlexMLS_Agent_GetUsersByRoleName"), portalID, role);
        }

        public override IDataReader FlexMLS_Agent_GetBuyers(int portalID, string role, int agentUserID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FlexMLS_Agent_GetBuyers"), portalID, role, agentUserID);
        }

        public override void FlexMLS_Agent_Insert_Or_Update(int moduleID, int buyerUserID, int agentUserID, int teamID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FlexMLS_Agent_Insert_Or_Update"), moduleID, buyerUserID, agentUserID, teamID);
        }

        public override IDataReader FlexMLS_Agent_Select(int buyerUserID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FlexMLS_Agent_Select"), buyerUserID);
        }



        public override IDataReader GetFlexMLS_Agents(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetFlexMLS_Agents"), moduleId);
        }

        public override IDataReader GetFlexMLS_Agent(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetFlexMLS_Agent"), moduleId, itemId);
        }

        public override void AddFlexMLS_Agent(int moduleId, string content, int userId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("AddFlexMLS_Agent"), moduleId, content, userId);
        }

        public override void UpdateFlexMLS_Agent(int moduleId, int itemId, string content, int userId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("UpdateFlexMLS_Agent"), moduleId, itemId, content, userId);
        }

        public override void DeleteFlexMLS_Agent(int moduleId, int itemId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("DeleteFlexMLS_Agent"), moduleId, itemId);
        }

        #endregion
    }
}
