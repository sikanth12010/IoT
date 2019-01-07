<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SmartParkingSystem.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
                .rep {
                    text-align:center;
                    align-items:center;
                    background-color:transparent;
                }
    </style>
    <h2>Smart Parking System</h2>
    <h3>An Initiative by the IoT Community at Mastek Ltd.</h3>
    <p>The proposed Smart Parking system consists of an on-site deployment of an IoT module that is used to monitor and signalize the state of availability of each single parking space. A mobile application is also provided that allows an end user to check the availability of parking space and book a parking slot accordingly.</p>
    <asp:Image CssClass="rep" runat ="server" ImageUrl="~/Images/About.png" BackColor="Transparent" ImageAlign ="Baseline"/>
</asp:Content>
