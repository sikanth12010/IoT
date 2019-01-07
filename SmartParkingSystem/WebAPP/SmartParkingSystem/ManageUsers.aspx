<%@ Page Title="Manage Owners" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="SmartParkingSystem.ManageUsers" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Manage Owners</h1>
    </div>
    <div class="row">
        <asp:GridView ID="ManageOwnersGrid" runat="server" AutoGenerateColumns="false" Width="80%" HeaderStyle-BackColor="#5e5e5e" HeaderStyle-ForeColor="#f8f8f8" AlternatingRowStyle-BackColor="WhiteSmoke"
            AllowPaging="true" PageSize="10">
            <Columns>
                <asp:BoundField HeaderText="User Name" DataField="UserName" />
                <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Phone" DataField="Phone" />
                <asp:BoundField HeaderText="Owned Space" DataField="OwnedParkingSpace" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

