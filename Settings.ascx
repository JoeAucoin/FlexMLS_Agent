<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.FlexMLS_Agent.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/sectionheadcontrol.ascx" %>

<div class="dnnForm" id="form-settings">
   
    <fieldset>
        <div class="dnnFormItem">
			<dnn:label id="lblFlexMLSModule" runat="server" suffix=":" controlname="ddlFlexMLSModule"></dnn:label>
			<asp:dropdownlist id="ddlFlexMLSModule" Runat="server" datavaluefield="TabID" datatextfield="TabName"></asp:dropdownlist>
        </div>

        <div class="dnnFormItem">
            <dnn:label id="lblAgentRole" runat="server" controlname="ddlAgentRole" suffix=":"></dnn:label>
            <asp:DropDownList ID="ddlAgentRole" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
            </asp:DropDownList>		
        </div>
		
		
        <div class="dnnFormItem">
            <dnn:label id="lblBrokerRole" runat="server" controlname="ddlBrokerRole" suffix=":"></dnn:label>
            <asp:DropDownList ID="ddlBrokerRole" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
            </asp:DropDownList>		
        </div>	

        <div class="dnnFormItem">
            <dnn:label id="lblBuyerRole" runat="server" controlname="ddlBuyerRole" suffix=":"></dnn:label>
            <asp:DropDownList ID="ddlBuyerRole" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
            </asp:DropDownList>		
        </div>	

    <dnn:sectionhead id="sectGoogleMapSettings" cssclass="Head" runat="server" text="GoogleMap Settings" section="GoogleMapSettingSection"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="GoogleMapSettingSection" runat="server">
        <div class="dnnFormItem">
    <dnn:label id="lblGoogleMapAPIKey" runat="server" controlname="txtGoogleMapAPIKey" suffix=":"></dnn:label>
            <asp:textbox ID="txtGoogleMapAPIKey" runat="server"  />		
        </div>
        
    
    </div>
        
    </fieldset>

</div>