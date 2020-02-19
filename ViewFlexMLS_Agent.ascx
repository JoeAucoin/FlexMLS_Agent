<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFlexMLS_Agent.ascx.cs" Inherits="GIBS.Modules.FlexMLS_Agent.ViewFlexMLS_Agent" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:dnncssinclude id="DnnCssInclude1" runat="server" filepath="~/DesktopModules/GIBS/FlexMLS_Agent/Style.css" />
<asp:Label ID="lblDebug" runat="server" Text="Label"></asp:Label>

<div class="section group">
    <div class="col span_3_of_12 caption1">
        <div>
            <asp:Label ID="lblAgents" runat="server" Text="Agent"></asp:Label>
            <asp:DropDownList ID="ddlAgents" runat="server" AutoPostBack="True" DataValueField="UserID" Width="140px"
                DataTextField="Name" OnSelectedIndexChanged="ddlAgents_SelectedIndexChanged">
            </asp:DropDownList>
        </div>



    </div>
    <div class="col span_9_of_12 pacontent1">
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="BuyerUserID" OnRowCommand="GridView1_RowCommand"
            OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging"
            AutoGenerateColumns="False" GridLines="Horizontal" AllowPaging="True" PageSize="10"
            CssClass="dnnGrid">
            <AlternatingRowStyle CssClass="dnnGridAltItem" />
            <FooterStyle CssClass="dnnGridFooter" />
            <HeaderStyle CssClass="dnnGridHeader" />
            <PagerStyle CssClass="dnnGridPager" />
            <RowStyle CssClass="dnnGridItem" />
            <SelectedRowStyle CssClass="dnnFormError" />
            <PagerStyle CssClass="GridPager" />
            <PagerSettings Mode="NumericFirstLast" />
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False" CommandArgument='<%# Eval("UserID") %>'
                            CommandName="Edit" runat="server">
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/icon_moduledefinitions_32px.gif" AlternateText="Edit Record" /></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this buyer?');"
                            CommandArgument='<%# Eval("UserID") %>' CommandName="Delete" runat="server">
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" /></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Buyer" DataField="Buyer"></asp:BoundField>
                <asp:TemplateField HeaderText="Email" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href='mailto:<%# Eval("Email") %>'><asp:Image ID="imgEmail" runat="server" ImageUrl="~/images/icon_bulkmail_32px.gif" AlternateText="Email Buyer" /></asp:LinkButton></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Telephone" DataField="Telephone"></asp:BoundField>
                <asp:BoundField HeaderText="aUID" DataField="AgentUserID" Visible="false"></asp:BoundField>
                <asp:BoundField HeaderText="Agent" DataField="Agent"></asp:BoundField>
                
                 <asp:TemplateField HeaderText="LastActivity">
                    <ItemTemplate>
                        <asp:Label ID="lblLastActivity" runat="server" Text='<%# Eval("LastActivity","{0:d}") %>' ToolTip='<%# Eval("LastActivity","{0:f}") %>' />
                    </ItemTemplate>

               </asp:TemplateField> 

                
            </Columns>
        </asp:GridView>
        <div style="font-weight: bold">
            
                <asp:Label ID="lblTotalRecordCount" runat="server" Text=""></asp:Label>
                Records<br />
                You are viewing page
                <%=GridView1.PageIndex + 1%>
                of
                <%=GridView1.PageCount%>
            
        </div>
    </div>
</div>
