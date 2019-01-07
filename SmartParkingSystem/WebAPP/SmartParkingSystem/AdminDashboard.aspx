<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="SmartParkingSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
    </style>
    <div class="row">
        <h1>Smart Parking System</h1>
    </div>
    
    <div class="row">
        <div id="DivSuperAdmin" runat="server" visible="false">
            <a href="ManageUsers.aspx" class="col-md-2 slots parkslots" style="border-radius: 30px;">
                <br />
                <h4>Manage Owners
                </h4>
            </a>
        </div>
        <a href="ParkingSpaces.aspx" class="col-md-2 slots parkslots" style="border-radius: 30px;">
            <br />
            <h4>Parking Spaces
            </h4>
        </a>        
        <a href="Reports.aspx" class="col-md-2 slots parkslots" style="border-radius: 30px;">
            <br />
            <h4>View Reports
            </h4>
        </a>
    </div>
</asp:Content>
