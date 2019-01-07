<%@ Page Title="Parking Spaces" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParkingSpaces.aspx.cs" Inherits="SmartParkingSystem.ParkingSpaces" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style>
                .slots {
                    padding-top: 30px;
                    padding-bottom: 30px;
                    margin-right: 10px;
                    margin-bottom : 10px;
                    color: inherit;
                    border-radius: 25px;
                    /*text-align:start;*/
                    /*column-count:5;*/
                    /*text-decoration-skip:none;*/
                   
                }

                 .parkslots {
                    
                    background: linear-gradient(#d5dfe2, #337ab7)
                }

                    .parkslots:hover {
                        background: linear-gradient(#337ab7, #d5dfe2);
                    }

                .totalslots {
                    background: linear-gradient(#d5dfe2, #ff9900)
                }

                    .totalslots:hover {
                        background: linear-gradient(#ff9900, #d5dfe2);
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

                .bookedslots{
                    background: linear-gradient(#d5dfe2, #ff6666)
                }

                    .bookedslots:hover {
                        background: linear-gradient(#ff6666, #d5dfe2);
                    }
            </style>
            <div>
                <h1>Parking Spaces</h1>
            </div>

            <div class="row">
                    <asp:PlaceHolder ID="PhForParkingSpaces" runat="server"></asp:PlaceHolder>
             </div>
            <asp:Panel runat="server" ID="PnlForParkingSlots" Visible="false">
                <hr />
                <div class="row">
                    <h4>
                        <asp:HiddenField ID="ParkingSpaceIdLbl" runat="server" />
                        <asp:Label ID="ParkingSpaceNameLbl" runat="server" Text="Parking Space: {0}"></asp:Label></h4>
                </div>
                <div class="row">
                    <a class="col-md-2 slots totalslots" runat="server" onserverclick="TotalParkingSlots_ServerClick">
                        <asp:Label ID="TotalParkingSlotsLbl" runat="server" Text="Total Parking Slots: {0}"></asp:Label>
                    </a>

                    <a href="ParkingSlots.aspx" class="col-md-2 slots occupiedslots" runat="server" onserverclick="OccupiedParkingSlots_ServerClick">
                        <asp:Label ID="OccupiedParkingSlotsLbl" runat="server" Text="Occupied Slots: {0}"></asp:Label>
                    </a>
                    <a class="col-md-2 slots availableslots" runat="server" onserverclick="AvailableParkingSlots_ServerClick">
                        <asp:Label ID="AvailableParkingSlotsLbl" runat="server" Text="Available Slots: {0}"></asp:Label>
                    </a>
                    <a class="col-md-2 slots bookedslots" runat="server" onserverclick="BookedParkingSlots_ServerClick">
                        <asp:Label ID="BookedParkingSlotsLbl" runat="server" Text="Booked Slots: {0}"></asp:Label>
                    </a>
                </div>
                <div class="row">
                    <asp:GridView ID="ParkingDetailsGrid" runat="server" AutoGenerateColumns="false" Width="80%" HeaderStyle-BackColor="#5e5e5e" HeaderStyle-ForeColor="#f8f8f8" AlternatingRowStyle-BackColor="WhiteSmoke"
                        AllowPaging="true" PageSize="10" OnPageIndexChanging="ParkingDetailsGrid_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="Slot Number" DataField="SlotNumber" />
                            <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                            <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                            <asp:BoundField HeaderText="Email" DataField="Email" />
                            <asp:BoundField HeaderText="Phone" DataField="Phone" />
                            <asp:BoundField HeaderText="Vehicle Number" DataField="Vehicle_No" />
                            <asp:BoundField HeaderText="Level" DataField="Level" />
                            <asp:BoundField HeaderText="Slot Status" DataField="SlotStatus" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
