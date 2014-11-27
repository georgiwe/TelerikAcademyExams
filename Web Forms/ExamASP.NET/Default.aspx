<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExamASP.NET._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>News</h1>

    <h2>Most popular articles</h2>

    <asp:ListView ID="TopArticlesLV" SelectMethod="TopArticlesLV_GetData" ItemType="ExamASP.NET.Models.Article" runat="server">

        <ItemTemplate>
            <div class="row">
                <h3>
                    <asp:HyperLink NavigateUrl='<%#: string.Format("~/ArticleDetails.aspx?id={0}", Item.Id)  %>' runat="server"
                        Text='<%#: Item.Title %>' />

                    <small><%#: Item.Category.Name %></small> </h3>
                <p>
                    <%#:
                        string.Format("{0}",
                            Item.Content.Length > 300 ?
                                Item.Content.Substring(0, 300) + "..." :
                                Item.Content
                    )%>
                </p>
                <p><%#: string.Format("Likes: {0}", Item.Likes.Sum(l => l.Value)) %></p>

                <div>
                    <i><%#: string.Format("by {0}", Item.Author.UserName) %></i>
                    <i><%#: string.Format("created on: {0}", Item.DateCreated) %></i>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>


    <h2>All Categories</h2>


    <asp:ListView ID="CategoriesLV" SelectMethod="CategoriesLV_GetData" ItemType="ExamASP.NET.Models.Category" GroupItemCount="2" runat="server">

        <GroupTemplate>
            <div class="row">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </div>
        </GroupTemplate>

        <LayoutTemplate>
            <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
        </LayoutTemplate>


        <ItemTemplate>
            <div class="col-md-6">
                <h3><%#: Item.Name %></h3>

                <asp:ListView DataSource='<%# Item.Articles.OrderByDescending(a => a.DateCreated).Take(3) %>' ItemType="ExamASP.NET.Models.Article" runat="server">
                    <LayoutTemplate>
                        <ul>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </ul>
                    </LayoutTemplate>

                    <EmptyDataTemplate>
                        <span>No articles.</span>
                    </EmptyDataTemplate>

                    <ItemTemplate>
                        <li>
                            <%--<asp:HyperLink NavigateUrl='<%#: string.Format("~/ArticleDetails.aspx?id={0}", Item.Id)  %>' Text='<%#: string.Format("<strong>{0}</strong> <i>by {1}</i>", Item.Title, Item.Author.UserName) %>' runat="server" />--%>
                            <a href='<%#: string.Format("~/ArticleDetails.aspx?id={0}", Item.Id) %>' runat="server"><strong><%#: Item.Title %> </strong><i><%#: Item.Author.UserName %> </i></a>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </ItemTemplate>

    </asp:ListView>

</asp:Content>
