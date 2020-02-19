<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditFlexMLS_Agent.ascx.cs" Inherits="GIBS.Modules.FlexMLS_Agent.EditFlexMLS_Agent" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FlexMLS_Agent/Style.css" />

<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>


<!-- fotorama.css & fotorama.js. -->
<link  href="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.css" rel="stylesheet" /> <!-- 3 KB -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.js" type="text/javascript"></script> <!-- 16 KB -->

<!-- 2. Add images to <div class="fotorama"></div>. -->


<div>
    <asp:Label ID="lblDebug" runat="server"></asp:Label>
</div>




<div class="dnnForm" id="form-buyer">
    <div style="float:right;">
        <asp:linkbutton id="cmdUpdate" runat="server" CssClass="btn btn-primary" text="Update" OnClick="cmdUpdate_Click" /><br /><br />
        <asp:linkbutton id="cmdCancel" runat="server" CssClass="btn btn-default" text="Cancel" causesvalidation="False" OnClick="cmdCancel_Click" />
        

    </div>
    <fieldset>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="txtBuyer" ResourceKey="txtBuyer" />
            <asp:TextBox runat="server" ID="txtBuyer" CssClass="dnnFormRequired" TextMode="MultiLine" />
            
        </div>

        <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="ddlAgent" ResourceKey="lblAgent" />
            <asp:DropDownList runat="server" ID="ddlAgent" DataValueField="UserID" DataTextField="Name">
            </asp:DropDownList>
        </div>



    </fieldset>


</div>



 <asp:datalist id="lstSearchResults" datakeyfield="ListingNumber" runat="server" cellpadding="4" Width="100%" OnItemDataBound="lstSearchResults_ItemDataBound" >
  <itemtemplate>

  <div class="section group" style="background-color:antiquewhite; margin-bottom:10px;">
    <div class="col span_9_of_12">

        <div class="section group">
            <div class="col span_2_of_12">
                <asp:Label ID="lblMLNumber" runat="server" CssClass="listingstatus"/>
            </div>
            <div class="col span_4_of_12">
                <asp:HyperLink ID="hyperlinkListingAddress" Text="Listing Address" NavigateUrl="" CssClass="ListingAddress" runat="server" />
            </div>
            <div class="col span_3_of_12">
                <asp:Label ID="lblYearBuilt" runat="server" CssClass="ListingDetails" />
            </div>
            <div class="col span_3_of_12">
	            <asp:Label ID="lblListingPrice" runat="server" CssClass="ListingPrice" />
            </div>
        </div>

        <div class="section group">
            
            <div class="col span_3_of_12">
	            <asp:Label ID="lblListingStatus" runat="server" Text="" CssClass="listingstatus"  />
            </div>
            <div class="col span_3_of_12">
                <asp:Label ID="lblBedsBaths" runat="server" Text="" />
            </div>
            <div class="col span_3_of_12">
                <asp:Label ID="lblLotSquareFootage" runat="server" CssClass="ListingDetails" />
            </div>
            <div class="col span_3_of_12">
                <asp:Label ID="lblLivingSpace" runat="server" CssClass="ListingDetails" />
            </div>
            
        </div>

        <div class="section group">
            <div class="col span_12_of_12">
                <asp:Label ID="lblMarketingRemarks" runat="server" />
            </div>
        </div>


        <div class="section group">
            
            <div class="col span_2_of_12">
	            <asp:Label ID="lblListingAgent" runat="server" Text="Agent" CssClass="lablestyle" />
            </div>
            <div class="col span_4_of_12">
                <asp:Label ID="txtListingAgent" runat="server" Text='<%# Eval("ListingAgentName") + ", " +  Eval("ListingOfficeName") %>' CssClass="datastyle" />
            </div>
            <div class="col span_2_of_12">
                <asp:Label ID="lblListingAgentPhone" runat="server" CssClass="lablestyle" />
            </div>
            <div class="col span_4_of_12">
                <asp:Label ID="txtListingAgentPhone" runat="server" CssClass="datastyle" />
            </div>
            
        </div>
		
        <div class="section group">
            
            <div class="col span_2_of_12">
	            <asp:Label ID="lblListingOffice" runat="server" Text="Office" CssClass="lablestyle" meta:resourcekey="UserNameLabel" />
            </div>
            <div class="col span_4_of_12">
                <asp:Label ID="txtListingOffice" runat="server" Text='' CssClass="datastyle" />
            </div>
            <div class="col span_2_of_12">
                <asp:Label ID="lblListingOfficePhone" runat="server" CssClass="lablestyle" />
            </div>
            <div class="col span_4_of_12">
                <asp:Label ID="txtListingOfficePhone" runat="server" CssClass="datastyle" />
            </div>
            
        </div>	



        
        <div>
        <asp:HyperLink ID="hyperlinkListingDetail" Text="Listing Details" NavigateUrl="" runat="server" CssClass="btn btn-sm btn-default" />
        &nbsp; 
         
          <asp:LinkButton ID="linkButtonEmailAFriend" runat="server" CommandArgument='<%# Eval("ListingNumber") %>' 
         onclick="linkButtonEmailAFriend_Click" CssClass="ActionLinks" Text="E-Mail a Friend" Visible="false" /> 


          <asp:LinkButton ID="linkButtonRemoveListing" runat="server" CommandArgument='<%# Eval("ItemID") %>' 
         onclick="linkButtonRemoveListing_Click" CssClass="btn btn-sm btn-default" Text="Remove Listing" /></div>
           
    </div>
    <div class="col span_3_of_12">           
           <div class="fotorama" data-width="100%"
     data-ratio="800/600" data-minwidth="240" data-maxwidth="240">
           <asp:Image ID="imgListingImage" runat="server"  AlternateText='MLS <%# Eval("ListingNumber") %>' CssClass="resize" /></div>
           	
    </div>
	
</div>

  </itemtemplate>
</asp:datalist>
<dnn:PagingControl id="PagingControl1" runat="server" Visible="False" BackColor="#FFFFFF" BorderColor="#000000" ></dnn:PagingControl>

 <cc1:GMap ID="GMap1" runat="server" Width="100%" Height="500px"  />
