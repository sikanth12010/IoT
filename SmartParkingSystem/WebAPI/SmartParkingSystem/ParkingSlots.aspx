<%@ Page Title="Parking Slots" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParkingSlots.aspx.cs" Inherits="SmartParkingSystem.ParkingSlots" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .slots{
            padding-top: 30px;
            padding-bottom: 30px;
            margin-bottom: 30px;
            margin-right: 10px;
            color: inherit;
            border-radius: 25px;
        }
        .totalslots {            
            background: linear-gradient(#d5dfe2, #337ab7)
        }
       .totalslots:hover {
            background: linear-gradient(#337ab7, #d5dfe2);
        }
       .occupiedslots {            
            background: linear-gradient(#d5dfe2, #c60000)
        }
       .occupiedslots:hover {
            background: linear-gradient(#c60000, #d5dfe2);
        }       
       .availableslots {            
            background: linear-gradient(#d5dfe2, #297f14)
        }
       .availableslots:hover {
            background: linear-gradient(#297f14, #d5dfe2);
        }
    </style>
    <div>
        <h1>Parking Slots</h1>
    </div>
    
    <div>
        <h4><asp:Label ID="ParkingSpaceLbl" runat="server" Text="Parking Space: {0}"></asp:Label></h4>
    </div>
    <div class="row">
        <a class="col-md-2 slots totalslots" runat="server" onserverclick="TotalParkingSlots_ServerClick">
            <asp:Label ID="TotalParkingSlotsLbl" runat="server" Text="Total Parking Slots: {0}"></asp:Label>
        </a>

        <a href="ParkingSlots.aspx" class="col-md-2 slots occupiedslots" runat="server" onserverclick="OccupiedParkingSlots_ServerClick">
            <asp:Label ID="OccupiedParkingSlotsLbl" runat="server" Text="Occupied Parking Slots: {0}"></asp:Label>            
        </a>
        <a class="col-md-2 slots availableslots" runat="server" onserverclick="AvailableParkingSlots_ServerClick">
            <asp:Label ID="AvailableParkingSlotsLbl" runat="server" Text="Available Parking Slots: {0}"></asp:Label> 
        </a>
    </div>
    <div class="row">
        <asp:GridView ID="ParkingDetailsGrid" runat="server" AutoGenerateColumns="false" Width="80%" HeaderStyle-BackColor="Wheat" AlternatingRowStyle-BackColor="WhiteSmoke"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ParkingDetailsGrid_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Parking Space Id" DataField="ParkingSpaceId" />
                <asp:BoundField HeaderText="Slot Number" DataField="SlotNo" />
                <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
                <asp:BoundField HeaderText="Vehicle Number" DataField="VehicleNo" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Phone" DataField="Phone" />
                <asp:BoundField HeaderText="Slot Status" DataField="SlotStatus" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
