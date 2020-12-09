<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compose.aspx.cs" Inherits="SquawkerWebApplication.Compose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                PageMethods.PostBackNewSquawk(document.getElementById("NewSquawkText").value, '', '', OnSuccess);
            }
        }

        function showPosition(position) {
            PageMethods.PostBackNewSquawk(document.getElementById("NewSquawkText").value, position.coords.latitude, position.coords.longitude, OnSuccess);
        }

        function OnSuccess(response, userContext, methodName) {
        }
    </script>

    <div>
        <center>
            <span>        
                <asp:TextBox runat="server" Width="80%"  MaxLength="280" Height="120px" ID="NewSquawkText" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>  
                <asp:Button runat="server" ID="PostNewSquawk" Text="Squawk!" ClientIDMode="Static" OnClick="PostNewSquawk_Click" />

            </span>  
              <p>
                  <br />
              </p>
            <span>               
                <asp:LinqDataSource
                    runat="server"
                    ID="SquawksDataSource" 
                    ContextTypeName="DbDataContext" 
                    TableName ="Squawks"
                    OnContextCreating="SquawksDataSource_ContextCreating"
                    OnSelecting="SquawksDataSource_Selecting" >
                </asp:LinqDataSource>

                <asp:ListView
                    runat="server"
                    ID="PostsListView"
                    DataSourceID="SquawksDataSource"
                    ItemPlaceholderID="itemPlaceholder">

                    <LayoutTemplate>
                        <span>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </span>
                        <br />
                    </LayoutTemplate>

                    <ItemTemplate>
                        <asp:Table runat="server" Width="80%" BorderWidth="0" BorderStyle="None">                            
                            <asp:TableRow>
                                <asp:TableCell>
                                     <asp:Image runat="server" 
                                            ID="AvatarImage" ImageUrl="~/Content/generic-avatar.jpg" 
                                            Width="100px" Height="100px" />
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <div><asp:Label runat="server" ID="FirstName" Text='<%# $"{Eval("FirstName")} {Eval("Surname")}" %>'></asp:Label></div>
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:Label runat="server" ID="LongAgo" Text='<%# DateTime.UtcNow.Subtract((DateTime)Eval("CreationDate")) %>'></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                 <asp:TableCell ColumnSpan="3">
                                     <asp:Label runat="server" ID="Content" Text='<%# Eval("Content") %>'></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ItemTemplate>

                    <EmptyDataTemplate>
                        <b>Looks like you havne't squawked at anything yet...</b>
                    </EmptyDataTemplate>

                </asp:ListView>

            </span>
         </center>
    </div>
</asp:Content>
