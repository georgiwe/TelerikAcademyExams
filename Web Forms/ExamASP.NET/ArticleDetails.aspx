<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticleDetails.aspx.cs" Inherits="ExamASP.NET.ArticleDetails" %>


<%@ Register Src="~/Controls/LikesControl/LikesControl.ascx"
    TagName="LikesControl" TagPrefix="userControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LoginView runat="server">
        <LoggedInTemplate>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <userControls:LikesControl runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>


            <%--<likecontrol></likecontrol>--%>
        </LoggedInTemplate>
    </asp:LoginView>


    <asp:FormView ID="ArticleDetailsFV" SelectMethod="ArticleDetailsFV_GetItem" ItemType="ExamASP.NET.Models.Article" runat="server">
        <ItemTemplate>
            <h2><%#: Item.Title %> <small><%#: string.Format("Category: {0}", Item.Category.Name) %></small></h2>

            <p><%#: Item.Content %></p>

            <p>
                <asp:Label Text='<%#: string.Format("Author: {0}", Item.Author.UserName) %>' runat="server" />
                <asp:Label CssClass="pull-right" Text='<%#: Item.DateCreated %>' runat="server" />
            </p>
        </ItemTemplate>

        <EmptyDataTemplate>
            <div class="alert alert-error">
                The article does not exist.
            </div>
        </EmptyDataTemplate>
    </asp:FormView>
</asp:Content>
