<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LikesControl.ascx.cs" Inherits="ExamASP.NET.Controls.LikesControl.LikesControl" %>

<div class="like-control col-md-1">
    <p>Likes</p>
    <div class="col-md-2">
        <asp:Button CssClass="vote-btn btn btn-default" ID="UpvoteBtn" Text="Up" OnClick="ProcessVote" CommandName="Up" runat="server" />
        <asp:Label ID="VoteDisplayLb" CssClass="vote-counter" Text="0" runat="server" />
        <asp:Button CssClass="vote-btn btn btn-default" ID="DownvoteBtn" Text="Down" OnClick="ProcessVote" CommandName="Down" runat="server" />
    </div>
</div>
