using System;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace GIBS.FlexMLS_Agent.Components
{
    public class FlexMLS_AgentInfo
    {
        //private vars exposed thro the
        //properties
        private int moduleId;
        private int itemId;
        private int userID;
        private int buyerUserID;
        private int agentUserID;
        private int brokerUserID;
        private int teamID;	

        private string name;
        private int createdByUser;
        private DateTime createdDate;
     //   private string createdByUserName = null;
        private string buyer;
        private string agent;
        private string email;
        private string telephone;
        private string lastActivity;

        /// <summary>
        /// empty cstor
        /// </summary>
        public FlexMLS_AgentInfo()
        {
        }


        #region properties

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }

        }

        public int BuyerUserID
        {
            get { return buyerUserID; }
            set { buyerUserID = value; }

        }

        public int AgentUserID
        {
            get { return agentUserID; }
            set { agentUserID = value; }


        }

        public int BrokerUserID
        {
            get { return brokerUserID; }
            set { brokerUserID = value; }


        }

        public int TeamID
        {
            get { return teamID; }
            set { teamID = value; }


        }			

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }


        public string Buyer
        {
            get { return buyer; }
            set { buyer = value; }

        }

        public string Agent
        {
            get { return agent; }
            set { agent = value; }


        }

        public string Email
        {
            get { return email; }
            set { email = value; }


        }

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        //lastActivity

        public string LastActivity
        {
            get { return lastActivity; }
            set { lastActivity = value; }
        }


        //public string CreatedByUserName
        //{
        //    get
        //    {
        //        if (createdByUserName == null)
        //        {
        //            int portalId = PortalController.GetCurrentPortalSettings().PortalId;
        //            UserInfo user = UserController.GetUser(portalId, createdByUser, false);
        //            createdByUserName = user.DisplayName;
        //        }

        //        return createdByUserName;
        //    }
        //}

        #endregion
    }
}
