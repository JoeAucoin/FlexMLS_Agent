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
using System.Text.RegularExpressions;
using DotNetNuke.Common;

using GIBS.Modules.FlexMLS_Favorites.Components;
using GIBS.Modules.FlexMLS.Components;
using System.Text;
using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
using System.Web;

namespace GIBS.Modules.FlexMLS_Agent
{
    public partial class EditFlexMLS_Agent : PortalModuleBase
    {

        int BuyerID = Null.NullInteger;
        static string _Role = "";
        static string _AgentRole = "";
        int _CurrentPage = 1;
        static string _FlexMLSPage = "";



        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            string myAIPkey = "";
            if (Settings.Contains("GoogleMapAPIKey"))
            {
                myAIPkey = Settings["GoogleMapAPIKey"].ToString();
            }     
          //  lblDebug.Text = myAIPkey.ToString();
            GMap1.Key = myAIPkey.ToString();         //Key for browser apps (with referers)  
            GControl control = new GControl(GControl.preBuilt.LargeMapControl);

            GMap1.Add(control);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //FlexMLSSettings settingsData = new FlexMLSSettings(this.TabModuleId);
                //string myAIPkey = settingsData.EmailBCC.ToString();

                if (Request.QueryString["BuyerID"] != null)
                {
                    BuyerID = Int32.Parse(Request.QueryString["BuyerID"]);
                }

             //   lblDebug.Text = BuyerID.ToString();

                if (!IsPostBack)
                {
                    //load the data into the control the first time
                    //we hit this page
                    LoadSettings();
                    LoadAgents();

                   
                    //check we have an item to lookup
                    if (!Null.IsNull(BuyerID))
                    {
                        LoadBuyer(BuyerID);
                        GetBuyerRecord(BuyerID);
                        LoadFavListings();
                    }


                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void GetBuyerRecord(int _buyerID)
        {

            try
            {
                
                FlexMLS_AgentController controller = new FlexMLS_AgentController();
                FlexMLS_AgentInfo item = controller.FlexMLS_Agent_Select(_buyerID);

                if (item != null)
                {

                    ddlAgent.SelectedValue = item.AgentUserID.ToString();

                }
                else
                {
                    lblDebug.Text += "Error on loading buyer info.";
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void LoadBuyer(int BuyerID)
        {
            try
            {

                DotNetNuke.Entities.Users.UserInfo BuyerUser = DotNetNuke.Entities.Users.UserController.GetUserById(this.PortalId, BuyerID);

          //      SetPageName(BuyerUser.Profile.GetPropertyValue("Company") + " - " + BuyerUser.FirstName + " " + BuyerUser.LastName);

                //      this.ModuleConfiguration.ModuleControl.ControlTitle = DonationUser.Profile.GetPropertyValue("Company") + " - " + DonationUser.DisplayName;
                txtBuyer.Text = BuyerUser.FirstName.ToString() + " " + BuyerUser.LastName.ToString() +
                    Environment.NewLine + BuyerUser.Profile.GetPropertyValue("Address") +
                    Environment.NewLine + BuyerUser.Profile.GetPropertyValue("City") +
                    ", " + BuyerUser.Profile.GetPropertyValue("Region") +
                    " " + BuyerUser.Profile.GetPropertyValue("PostalCode");
                
                
                
                //txtFirstName.Text = BuyerUser.FirstName;
                //txtLastName.Text = BuyerUser.LastName;
                //txtMiddleName.Text = BuyerUser.Profile.GetPropertyValue("MiddleName");
                //txtCompany.Text = BuyerUser.Profile.GetPropertyValue("Company");

                //txtAdditionalName.Text = BuyerUser.Profile.GetPropertyValue("AdditionalName");
                //txtAdditionalFirstName.Text = BuyerUser.Profile.GetPropertyValue("AdditionalFirstName");
                //txtAdditionalMI.Text = BuyerUser.Profile.GetPropertyValue("AdditionalMI");

                //ListItem LIdonortype = ddlDonorType.Items.FindByValue(BuyerUser.Profile.GetPropertyValue("DonorType"));
                //if (LIdonortype != null)
                //{
                //    // value found
                //    ddlDonorType.SelectedValue = BuyerUser.Profile.GetPropertyValue("DonorType");
                //}
                //else
                //{
                //    //Value not found
                //}





                //txtEmail.Text = BuyerUser.Email;

                //ListItem litown = ddlPrefix.Items.FindByValue(BuyerUser.Profile.GetPropertyValue("Prefix"));
                //if (litown != null)
                //{
                //    // value found
                //    ddlPrefix.SelectedValue = BuyerUser.Profile.GetPropertyValue("Prefix");
                //}
                //else
                //{
                //    //Value not found
                //    //   ddlTown.SelectedValue = item.ClientTown;
                //}


                //txtStreet.Text = BuyerUser.Profile.Street;
                //txtStreet2.Text = BuyerUser.Profile.GetPropertyValue("Street2");
                //txtCity.Text = BuyerUser.Profile.City;
                //txtWorkPhone.Text = BuyerUser.Profile.GetPropertyValue("WorkPhone");
                //txtTelephone.Text = BuyerUser.Profile.Telephone;
                //txtCellPhone.Text = BuyerUser.Profile.Cell;
                //txtFax.Text = BuyerUser.Profile.Fax;
                //txtZip.Text = BuyerUser.Profile.PostalCode;

                //   ddlState.SelectedValue = DonationUser.Profile.Region;
                //     lblDebug.Text = DonationUser.Profile.Region;

                //ListItem liregion = ddlState.Items.FindByText(BuyerUser.Profile.Region);      //.FindByValue(DonationUser.Profile.Region);
                //if (liregion != null)
                //{
                //    // value found
                //    ddlState.SelectedValue = ddlState.Items.FindByText(BuyerUser.Profile.Region).Value;    //DonationUser.Profile.Region;
                //}
                //else
                //{
                //    //Value not found
                //    //   ddlTown.SelectedValue = item.ClientTown;
                //    lblStatus.Text += "<br />STATE NOT FOUND";
                //}



                //txtComments.Text = BuyerUser.Profile.GetPropertyValue("Comments");



                //string _PrimaryAddress = BuyerUser.Profile.GetPropertyValue("Prefix") + " "
                //    + BuyerUser.FirstName + " "
                //    + BuyerUser.Profile.GetPropertyValue("MiddleName") + " "
                //    + BuyerUser.LastName + " "
                //    + BuyerUser.Profile.GetPropertyValue("Suffix") + Environment.NewLine
                //    + BuyerUser.Profile.GetPropertyValue("AdditionalName") + Environment.NewLine
                //    + BuyerUser.Profile.GetPropertyValue("Company") + Environment.NewLine
                //    + BuyerUser.Profile.Street + Environment.NewLine
                //    + BuyerUser.Profile.GetPropertyValue("Street2") + Environment.NewLine
                //    + BuyerUser.Profile.City + ", "
                //    + BuyerUser.Profile.Region + " "
                //    + BuyerUser.Profile.PostalCode;
                ////   REMOVE EMPTY LINES
                //_PrimaryAddress = Regex.Replace(_PrimaryAddress.ToString(), @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

 //               txtPrimaryAddress.Text = _PrimaryAddress.ToString().TrimStart().TrimEnd().Replace("  ", " ");




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void LoadFavListings()
        {
            try
            {
                List<FlexMLS_FavoritesInfo> items;
                FlexMLS_FavoritesController controller = new FlexMLS_FavoritesController();


                items = controller.FlexMLS_Favorites_Get_List(0, BuyerID, "Listing");

                if (items.Count > 0)
                {
                    string MlNumbers = "";
                    for (int i = 0; i < items.Count; i++)
                    {
                        MlNumbers += (string)items[i].Favorite.ToString() + ",";
                        //  list.Add(new ListItem((string)items[i].Village.ToString(), (string)items[i].Village.ToString()));
                    }


                    SearchMLS(MlNumbers.ToString());
                }
                else
                {
                    GMap1.Visible = false;
                    lblDebug.Text = Localization.GetString("DefaultContent", LocalResourceFile);
                    lstSearchResults.Visible = false;
                }


                //           hyperlinkFavListings.Visible = true;
                string vPage = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "FavListing", "mid=" + ModuleId.ToString());
                //          hyperlinkFavListings.NavigateUrl = vPage.ToString();


                int intModTabID = -1;
                ModuleInfo objModuleInfo = new ModuleInfo();
                ModuleController objModuleContr = new ModuleController();
                objModuleInfo = objModuleContr.GetModuleByDefinition(PortalId, "GIBS - FlexMLS");
                intModTabID = objModuleInfo.TabID;

                string strRedir = Globals.NavigateURL(intModTabID);


                //HERE-FIX              HyperLinkNewSearch.NavigateUrl = strRedir.ToString();


           //     FlexMLS_FavoritesSettings settingsData = new FlexMLS_FavoritesSettings(this.TabModuleId);
           //     if (settingsData.FlexMLSModule != null)
           //     {
           //         // NO LONGER USING MODULEID - Convert.ToInt32(settingsData.FlexMLSModule)
                    
           //         //   string vLink = Globals.NavigateURL(Int32.Parse(settingsData.FlexMLSModule.ToString()),false);
      

           //     }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void SearchMLS(string MlNumbers)
        {

            try
            {

                int PageSize = 20;
                //Display 20 items per page
                //Get the currentpage index from the url parameter
                if (Request.QueryString["currentpage"] != null)
                {
                    _CurrentPage = Convert.ToInt32(Request.QueryString["currentpage"].ToString());
                }
                else
                {
                    _CurrentPage = 1;
                }

                List<FlexMLSInfo> items;
                FlexMLSController controller = new FlexMLSController();

                items = controller.FlexMLS_Search_MLS_Numbers(MlNumbers.ToString());

                //    lblDebug.Text = MlNumbers.ToString();

                PagedDataSource objPagedDataSource = new PagedDataSource();
                objPagedDataSource.DataSource = items;

                if (items.Count < 1)
                {
                    GMap1.Visible = false;
                    lblDebug.Text = Localization.GetString("DefaultContent", LocalResourceFile);
                }

                if (objPagedDataSource.PageCount > 0)
                {
                    objPagedDataSource.PageSize = PageSize;
                    objPagedDataSource.CurrentPageIndex = _CurrentPage - 1;
                    objPagedDataSource.AllowPaging = true;
                }


                lstSearchResults.DataSource = objPagedDataSource;
                lstSearchResults.DataBind();

                lblDebug.Text = "Total Listings Found: " + items.Count.ToString();

                if (PageSize == 0 || items.Count <= PageSize)
                {
                    PagingControl1.Visible = false;
                }
                else
                {
                    PagingControl1.Visible = true;
                    PagingControl1.TotalRecords = items.Count;
                    PagingControl1.PageSize = PageSize;
                    PagingControl1.CurrentPage = _CurrentPage;
                    PagingControl1.TabID = TabId;
                    PagingControl1.QuerystringParams = "pg=List&" + GenerateQueryStringParameters(this.Request, "Town", "Village", "Beds", "Baths", "WaterFront", "WaterView", "Type", "Low", "High", "LOID");

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void BuildGoogleMap(double Latitude, double Longitude, string BubbleText)
        {

            try
            {

                GMap1.setCenter(new GLatLng(Latitude, Longitude), 14);
                //GMap1.mapType = GMapType.GTypes.Hybrid;
                //GMap1.Add(GMapType.GTypes.Normal);      //.addMapType(GMapType.GTypes.Physical);
                //GMap1.Add(GMapType.GTypes.Physical);

                GControl control = new GControl(GControl.preBuilt.SmallMapControl);
                GControl control2 = new GControl(GControl.preBuilt.MenuMapTypeControl, new GControlPosition(GControlPosition.position.Top_Left));

                GMap1.Add(control);
                GMap1.Add(control2);

                GLatLng latlng = new GLatLng(Latitude, Longitude);

                string vBubbleText = BubbleText.ToString();    // lblListingAddress.Text.ToString() + "<br />" + lblSummary.Text.ToString();
                //XPinLetter xPinLetter = new XPinLetter(PinShapes.pin_star, "A", Color.Red, Color.White, Color.Black);
                XPinLetter xPinLetter = new XPinLetter(PinShapes.pin_star, "+", System.Drawing.Color.Red, System.Drawing.Color.White, System.Drawing.Color.Gold);


                GMarker marker = new GMarker(latlng, new GMarkerOptions(new GIcon(xPinLetter.ToString(), xPinLetter.Shadow())));
                //  GInfoWindowOptions windowOptions = new GInfoWindowOptions();
                GInfoWindow commonInfoWindow = new GInfoWindow(marker, vBubbleText.ToString(), false);
                GMap1.Add(commonInfoWindow);



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void lstSearchResults_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {

            try
            {

                GMap1.mapType = GMapType.GTypes.Hybrid;
                GMap1.Add(GMapType.GTypes.Normal);      //.addMapType(GMapType.GTypes.Physical);
                GMap1.Add(GMapType.GTypes.Physical);
                string _ListingNumber = "";
                string _PropertyType = "";


                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _ListingNumber = DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString();
                    _PropertyType = DataBinder.Eval(e.Item.DataItem, "PropertyType").ToString();

                    //////****** REMOVE LISTING BUTTON
                    //LinkButton removeListing = (LinkButton)e.Item.FindControl("linkButtonRemoveListing");
                    //removeListing.CommandArgument = _ListingNumber.ToString();

                    // DEAL WITH REMOVED LISTINGS!
                    if (DataBinder.Eval(e.Item.DataItem, "StatusCode").ToString() == "")
                    {

                        HyperLink Address = (HyperLink)e.Item.FindControl("hyperlinkListingAddress");
                        Address.Text = "MLS# " + DataBinder.Eval(e.Item.DataItem, "ItemID").ToString() + " had been removed. Call agent for details!";

                        Image ListingImage = (Image)e.Item.FindControl("imgListingImage");

                        string checkImage = "http://mls.gibs.com/images/" + DataBinder.Eval(e.Item.DataItem, "ItemID").ToString() + ".jpg";

                        if (UrlExists(checkImage.ToString()) == true)
                        {
                            // ListingImage.ImageUrl = checkImage.ToString();
                            ListingImage.ImageUrl = "http://mls.gibs.com/images/" + DataBinder.Eval(e.Item.DataItem, "ItemID").ToString() + ".jpg";

                        }
                        else if (UrlExists("http://mls.gibs.com/images/" + DataBinder.Eval(e.Item.DataItem, "ItemID").ToString() + "_1.jpg") == true)
                        {
                            //
                            ListingImage.ImageUrl = "http://mls.gibs.com/images/" + DataBinder.Eval(e.Item.DataItem, "ItemID").ToString() + "_1.jpg";

                        }
                        else
                        {

                            ListingImage.ImageUrl = "http://mls.gibs.com/images/NoImage.jpg";

                        }

                        ListingImage.ToolTip = "MLS Listing " + _ListingNumber.ToString();
                        ListingImage.AlternateText = "MLS Listing " + _ListingNumber.ToString();



                    }

                    else
                    {
                        // Retrieve the Hyperlink control in the current DataListItem.
                        HyperLink eLink = (HyperLink)e.Item.FindControl("hyperlinkListingDetail");
                        string _pageName = DataBinder.Eval(e.Item.DataItem, "Address").ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + "_" + DataBinder.Eval(e.Item.DataItem, "Village").ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + ".aspx";

                        string vLink = Globals.NavigateURL(Int32.Parse(_FlexMLSPage.ToString()));
                        var result = vLink.Substring(vLink.LastIndexOf('/') + 1);
                        // DISABLE ADDING OF NEW RECORD IF COMING FROM THIS MODULE BY QUERYSTRING ADDITION OF . . . /t/f
                        vLink = vLink.ToString().Replace(result.ToString(), "tabid/" + _FlexMLSPage.ToString() + "/pg/v/t/f/MLS/" + _ListingNumber.ToString() + "/" + _pageName.ToString());

                        eLink.NavigateUrl = vLink.ToString();

                        Label MLS = (Label)e.Item.FindControl("lblListingNumber");
                        //           MLS.Text = "MLS " + _ListingNumber.ToString();

                        // lblLotSquareFootage
                        Label LotSquareFootage = (Label)e.Item.FindControl("lblLotSquareFootage");
                        double sqft = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Acres"));
                        if (sqft > 0)
                        {
                            LotSquareFootage.Text = Math.Round(sqft, 2).ToString() + " Acres";
                        }

                        if (_PropertyType.ToString().ToUpper() == "COND" || _PropertyType.ToString().ToUpper() == "COMM")
                        {
                            LotSquareFootage.Text = DataBinder.Eval(e.Item.DataItem, "Complex").ToString();    //CONDO COMPLEX NAME
                        }

                        Label ListingStatus = (Label)e.Item.FindControl("lblListingStatus");
                        string _listingstatus = DataBinder.Eval(e.Item.DataItem, "StatusCode").ToString();
                        ListingStatus.Text = _listingstatus.ToString();

                        Label MLNumber = (Label)e.Item.FindControl("lblMLNumber");
                        MLNumber.Text = "MLS # " + _ListingNumber.ToString();

                        //lblBedsBaths
                        string _S_baths = "";
                        string _S_beds = "";
                        string _S_halfbaths = "";
                        if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString()) > 1)
                        {
                            _S_beds = "s";
                        }

                        // ****** CHECK TO SEE IF THE LISTING HAS BEEN REMOVED *************************
                        if (DataBinder.Eval(e.Item.DataItem, "PropertyType") != null)
                        {
                            //if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TotalBaths").ToString()) > 1)
                            //{
                            //    _S_baths = "s";
                            //}
                        }


                        Label BedsBaths = (Label)e.Item.FindControl("lblBedsBaths");




                        if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString()) > 0)
                        {
                            BedsBaths.Text = DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString() + " Bedroom" + _S_beds.ToString();
                        }
                        if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString()) > 0)
                        {
                            BedsBaths.Text = BedsBaths.Text.ToString() + " - " + DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString() + " Full Bath" + _S_baths.ToString();
                        }

                        if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "HalfBaths").ToString()) > 0)
                        {
                            BedsBaths.Text = BedsBaths.Text.ToString() + ", " + DataBinder.Eval(e.Item.DataItem, "HalfBaths").ToString() + " Half Bath" + _S_halfbaths.ToString();
                        }
                        if (_PropertyType.ToString() == "COMM" || _PropertyType.ToString() == "MULT")
                        {
                            BedsBaths.Text = DataBinder.Eval(e.Item.DataItem, "Style").ToString();
                        }


                        // lblLivingSpace  
                        double tempDouble = 00.000;
                        Label SquareFootage = (Label)e.Item.FindControl("lblLivingSpace");
                        if (double.TryParse(DataBinder.Eval(e.Item.DataItem, "LivingSpace").ToString(), out tempDouble))
                        {

                            double livingspace = double.Parse(DataBinder.Eval(e.Item.DataItem, "LivingSpace").ToString());
                            SquareFootage.Text = livingspace.ToString("##,###") + " Sqft.";
                        }


                        // lblAddress
                        string _UnitNumber = "";
                        if (DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString().Length >= 1 && DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString() != "0")
                        {
                            _UnitNumber = " #" + DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString();
                        }
                        HyperLink Address = (HyperLink)e.Item.FindControl("hyperlinkListingAddress");

                        Address.Text = DataBinder.Eval(e.Item.DataItem, "Address").ToString() + _UnitNumber.ToString() + "<br />" + DataBinder.Eval(e.Item.DataItem, "Village").ToString();
                        string BubbleAddress = Address.Text.ToString().Replace("<br />", ", ").ToString();
                        Address.NavigateUrl = vLink.ToString();

                        string vListingPrice = "";
                        Label ListingPrice = (Label)e.Item.FindControl("lblListingPrice");
                        if (double.TryParse(DataBinder.Eval(e.Item.DataItem, "ListingPrice").ToString(), out tempDouble))
                        {
                            // lblListingPrice

                            vListingPrice = DataBinder.Eval(e.Item.DataItem, "ListingPrice").ToString();
                            ListingPrice.Text = String.Format("{0:C0}", double.Parse(vListingPrice.ToString()));
                        }


                        //lblYearBuilt
                        Label YearBuilt = (Label)e.Item.FindControl("lblYearBuilt");
                        YearBuilt.Text = DataBinder.Eval(e.Item.DataItem, "PropertySubType1").ToString() + "<br />Built In " + DataBinder.Eval(e.Item.DataItem, "YearBuilt").ToString();

                        // CHECK FOR LAND LISTING
                        if (_PropertyType.ToString() == "LOTL" || _PropertyType.ToString() == "MULT")
                        {
                            YearBuilt.Text = DataBinder.Eval(e.Item.DataItem, "PropertySubType1").ToString();
                        }


                        // IMAGE
                        // IMAGE
                        Image ListingImage = (Image)e.Item.FindControl("imgListingImage");

                        string checkImage = "http://mls.gibs.com/images/" + _ListingNumber.ToString() + ".jpg";

                        if (UrlExists(checkImage.ToString()) == true)
                        {
                            // ListingImage.ImageUrl = checkImage.ToString();
                            ListingImage.ImageUrl = "http://mls.gibs.com/images/" + _ListingNumber.ToString() + ".jpg";

                        }
                        else if (UrlExists("http://mls.gibs.com/images/" + _ListingNumber.ToString() + "_1.jpg") == true)
                        {
                            //
                            ListingImage.ImageUrl = "http://mls.gibs.com/images/" + _ListingNumber.ToString() + "_1.jpg";

                        }
                        else
                        {

                            ListingImage.ImageUrl = "http://mls.gibs.com/images/NoImage.jpg";

                            // ImageNeeded(_ListingNumber.ToString());
                        }

                        ListingImage.ToolTip = "MLS Listing " + _ListingNumber.ToString();
                        ListingImage.AlternateText = "MLS Listing " + _ListingNumber.ToString();

                        // lblMarketingRemarks
                        Label MarketingRemarks = (Label)e.Item.FindControl("lblMarketingRemarks");
                        MarketingRemarks.Text = DataBinder.Eval(e.Item.DataItem, "PublicRemarks").ToString();

                        // CHECK IF AUTHENTICATED
                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {

                        }
                        // vLink = vLink.ToString().Replace(result.ToString(), "tabid/" + _FlexMLSPage.ToString() + "/pg/v/t/f/MLS/" + _ListingNumber.ToString() + "/" + _pageName.ToString());

                        ////HyperLinkInquiry - CONTACT FORM
                        //HyperLink InquiryHyperLink = (HyperLink)e.Item.FindControl("HyperLinkInquiry");
                        //// string InquiryLink = Globals.NavigateURL("View", "pg", "Contact", "MLS", _ListingNumber.ToString());
                        //string InquiryLink = vLink.ToString().Replace("v/t/f", "Contact").ToString();
                        //InquiryHyperLink.NavigateUrl = InquiryLink.ToString();

                        ////HyperLinkShowing - SCHEDULE A SHOWING
                        //HyperLink ShowingHyperLink = (HyperLink)e.Item.FindControl("HyperLinkShowing");
                        //string ShowingLink = vLink.ToString().Replace("v/t/f", "Contact/Schedule/Showing").ToString();
                        ////Globals.NavigateURL("View", "pg", "Contact", "MLS", _ListingNumber.ToString(), "Schedule", "Showing");
                        ////ShowingLink = ShowingLink.ToString().Replace("ctl/View/", "");
                        //ShowingHyperLink.NavigateUrl = ShowingLink.ToString();


                        // ADD TO MAP
                        double _lat = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Latitude").ToString());
                        double _log = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Longitude").ToString());
                        //string _bubbleText = "<img src='" + checkImage.ToString() + "' id='" + _ListingNumber.ToString() + "' width='90' height='60' align='right' alt='" + BubbleAddress.ToString() + "'>"
                        //    + Address.Text.ToString() + "<br />" + ListingPrice.Text.ToString()
                        //    + "<br /><a href='" + vLink.ToString() + "'>MLS " + _ListingNumber.ToString() + "</a>";
                        string _bubbleText = "<div style='width:270px;height:120px;'><img src='" + "/DesktopModules/GIBS/FlexMLS/ImageHandler.ashx?MlsNumber="
                            + _ListingNumber.ToString() + "&MaxSize=120' id='" + _ListingNumber.ToString()
                            + "' align='right' alt='" + BubbleAddress.ToString() + "' style='border: 1px solid #000000;'>"
                            + Address.Text.ToString() + "<br />" + ListingPrice.Text.ToString()
                            + "<br /><a href='" + vLink.ToString() + "'>MLS " + _ListingNumber.ToString() + "<br />View Listing</a></div>";

                        if (_lat > 0)
                        {
                            BuildGoogleMap(_lat, _log, _bubbleText.ToString());
                        }

                    }


                }
                    




                

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void ImageNeeded(int listingNumber)
        {
            try
            {

                FlexMLSController controller = new FlexMLSController();
                FlexMLSInfo item = new FlexMLSInfo();

                item.ListingNumber = listingNumber.ToString();

                controller.FlexMLS_ImagesNeeded_Insert(item);

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public string GetStatusDesc(string Status)
        {

            try
            {
                string myRetValue = "";
                switch (Status)
                {
                    case "A":
                        myRetValue = "Active";
                        break;
                    case "C":
                        myRetValue = "Pending with Contingencies";
                        break;
                    case "R":
                        myRetValue = "Listing Removed. Call agent for details!";
                        break;

                    default:
                        myRetValue = "";
                        break;
                }
                return myRetValue.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return "Error";
            }

        }


        private static bool UrlExists(string url)
        {
            try
            {
                new System.Net.WebClient().DownloadData(url);
                return true;
            }
            catch (System.Net.WebException e)
            {
                if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }

        protected static string GenerateQueryStringParameters(HttpRequest request, params string[] queryStringKeys)
        {
            StringBuilder queryString = new StringBuilder(64);
            foreach (string key in queryStringKeys)
            {
                if (request.QueryString[key] != null)
                {
                    if (queryString.Length > 0)
                    {
                        queryString.Append("&");
                    }

                    queryString.Append(key).Append("=").Append(request.QueryString[key]);
                }
            }

            return queryString.ToString();
        }

        private string GetAdditionalQueryStringParams()
        {
            throw new NotImplementedException();
        }


        protected void linkButtonFavoritesRemoveListing_Click(object sender, EventArgs e)
        {
            try
            {
                int MLSnumber = 0;
                LinkButton myButton = sender as LinkButton;

                if (myButton != null)
                {
                    MLSnumber = Convert.ToInt32(myButton.CommandArgument);
                }

                FlexMLSController controller = new FlexMLSController();
                FlexMLSInfo item = new FlexMLSInfo();

                item.Favorite = MLSnumber.ToString();
                item.FavoriteType = "Listing";
                item.ModuleId = this.ModuleId;
                item.UserID = this.UserId;

                controller.FlexMLS_Favorites_Add(item);

                myButton.Text = "SAVED! - " + item.Favorite.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        protected void linkButtonEmailAFriend_Click(object sender, EventArgs e)
        {
            try
            {
                int MLSnumber = 0;
                LinkButton myButton = sender as LinkButton;

                if (myButton != null)
                {
                    MLSnumber = Convert.ToInt32(myButton.CommandArgument);
                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void linkButtonRemoveListing_Click(object sender, EventArgs e)
        {
            try
            {
                int MLSnumber = 0;
                LinkButton myButton = sender as LinkButton;

                if (myButton != null)
                {
                    MLSnumber = Convert.ToInt32(myButton.CommandArgument.ToString());
                }
                
                FlexMLS_FavoritesController controller = new FlexMLS_FavoritesController();
                string _FlexMLSModule = "0";
                if (Settings.Contains("FlexMLSModule"))
                {
                    _FlexMLSModule = Settings["FlexMLSModule"].ToString();
                }

                int FlexMLSModuleID = Int32.Parse(_FlexMLSModule.ToString());

                controller.FlexMLS_Favorites_Delete_By_MlsNumber(FlexMLSModuleID, BuyerID, MLSnumber.ToString());

                LoadFavListings();

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

                ddlAgent.DataSource = items;
                ddlAgent.DataBind();

                // ADD FIRST (NULL) ITEM
                ListItem item = new ListItem();
                item.Text = "-- Select Agent --";
                item.Value = "0";
                ddlAgent.Items.Insert(0, item);




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

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
                if (Settings.Contains("FlexMLSModule"))
                {
                    _FlexMLSPage = Settings["FlexMLSModule"].ToString();
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                FlexMLS_AgentController controller = new FlexMLS_AgentController();
                FlexMLS_AgentInfo item = new FlexMLS_AgentInfo();

                item.BuyerUserID = BuyerID;
                item.ModuleId = this.ModuleId;
                item.AgentUserID = Int32.Parse(ddlAgent.SelectedValue.ToString());
                item.TeamID = 0;
                controller.FlexMLS_Agent_Insert_Or_Update(item);

                ////determine if we are adding or updating
                //if (Null.IsNull(item.ItemId))
                //    controller.AddFlexMLS_Agent(item);
                //else
                //    controller.UpdateFlexMLS_Agent(item);

                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(), true);
                
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


    }
}