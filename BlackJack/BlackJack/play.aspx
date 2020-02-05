<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="play.aspx.cs" Inherits="BlackJack.play" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
            <div class="col-12">
                <h4>Dealer: </h4>
                <div id="dealerPlace" class="imgsize" runat="server">
                  
                </div>
            </div>
            <div class="col-12">
                <h4>Player: </h4>
                 <div id="playerPlace" class="imgsize" runat="server">
                 
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Label ID="lblBet" runat="server" Text="Enter the amout: " ></asp:Label>
                <br />
                <asp:TextBox ID="txtAmountToBet" runat="server" ValidationGroup="Validation"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator" runat="server" ErrorMessage="The bet must be between 1 - 100" ControlToValidate="txtAmountToBet" MinimumValue="1" MaximumValue="100" ValidationGroup="Validation"></asp:RangeValidator>
                <br />
                <asp:Button ID="btnStart" runat="server" Text="Start" Width="80" BackColor="Green" OnClick="btnStart_Click"  />
                <br />
                <br />
                <asp:Label ID="lbltext" runat="server" Text="Player Money: "></asp:Label>
                <asp:Label ID="lblPlayerMoney" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnHit" runat="server" Width="80" Text="Hit" OnClick="btnHit_Click" BackColor="Salmon" />
                <asp:Button ID="btnStand" runat="server" Width="80" Text="Stand" OnClick="btnStand_Click" BackColor="Yellow" />
                <br />
                <br />
                <asp:Label ID="lblInfo" runat="server" ></asp:Label>
                <br />
                <asp:Button ID="btnRestart" runat="server" Width="130" Text="Restart" OnClick="btnRestart_Click" BackColor="Red" />
           </div>
      </div>
</asp:Content>
