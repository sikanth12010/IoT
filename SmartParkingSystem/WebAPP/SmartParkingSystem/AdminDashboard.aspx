<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="SmartParkingSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Smart Parking System</h1>
    </div>
    
    <div class="row">
        <div id="DivSuperAdmin" runat="server" visible="false">
            <a href="#" class="col-md-2 jumbotron" style="border-radius: 25px;">
                <br />
                <h4>Manage Owners
                </h4>
            </a>
        </div>
        <a href="ParkingSpaces.aspx" class="col-md-2 jumbotron" style="border-radius: 25px;">
            <h4>Manage Parking Spaces
            </h4>
        </a>        
        <a href="#" class="col-md-2 jumbotron" style="border-radius: 25px;">
            <br />
            <h4>View Reports
            </h4>
        </a>
    </div>
</asp:Content>
