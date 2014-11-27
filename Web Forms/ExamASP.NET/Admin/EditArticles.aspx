<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditArticles.aspx.cs" Inherits="ExamASP.NET.Admin.EditArticles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:ListView ID="EditArticlesLV" ItemType="ExamASP.NET.Models.Article" runat="server"
        DataKeyNames="Id"
        SelectMethod="EditArticlesLV_GetData"
        UpdateMethod="EditArticlesLV_UpdateItem"
        InsertMethod="EditArticlesLV_InsertItem"
        DeleteMethod="EditArticlesLV_DeleteItem">


        <LayoutTemplate>
            <div class="row">
                <asp:Button CssClass="btn btn-default pull-left sort-btn" CommandName="Sort" CommandArgument="Title" Text="Sort by Title" runat="server" />
                <asp:Button CssClass="btn btn-default pull-left sort-btn" CommandName="Sort" CommandArgument="DateCreated" Text="Sort by Date" runat="server" />
                <asp:Button CssClass="btn btn-default pull-left sort-btn" Text="Sort by Category" runat="server" />
                <asp:Button CssClass="btn btn-default pull-left sort-btn" Text="Sort by Likes" runat="server" />
            </div>

            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />

            <br />

            <asp:DataPager PageSize="5" runat="server">
                <Fields>
                    <asp:NextPreviousPagerField ButtonCssClass="btn btn-default" />
                    <asp:NumericPagerField />
                </Fields>
            </asp:DataPager>

        </LayoutTemplate>


        <ItemTemplate>
            <div class="row">
                <h3>
                    <asp:Label Text='<%#: Item.Title %>' runat="server" />
                    <asp:Button CssClass="btn btn-info" CommandName="Edit" Text="Edit" runat="server" />
                    <asp:Button CssClass="btn btn-danger" CommandName="Delete" Text="Delete" runat="server" />
                </h3>

                <p>Category: <%#: Item.Category.Name %></p>
                <p>
                    <%#: 
                        string.Format("{0}",
                            Item.Content.Length > 300 ?
                                Item.Content.Substring(0, 300) + "..." :
                                Item.Content
                    )%>
                </p>

                <p>Likes count: <%#: Item.Likes.Count %></p>

                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>
            </div>
        </ItemTemplate>

        <EditItemTemplate>
            <div class="row">
                <h3>
                    <asp:TextBox ID="UpdateArticleTitleTB" Text='<%#: BindItem.Title %>' runat="server" />
                    <asp:RequiredFieldValidator ErrorMessage="Title is required" ControlToValidate="UpdateArticleTitleTB" runat="server" ForeColor="Red" Display="Dynamic" Font-Size="Larger" />
                    <asp:Button CssClass="btn btn-info" CommandName="Update" Text="Save" runat="server" />
                    <asp:Button CssClass="btn btn-warning" CausesValidation="false" CommandName="Cancel" Text="Cancel" runat="server" />
                </h3>

                <p>
                    <asp:Label Text="Category:" runat="server" />
                    <br />
                    <asp:DropDownList ID="CategoriesDD" DataValueField="Id" DataTextField="Name" ItemType="ExamASP.NET.Models.Category" SelectedValue="<%#: BindItem.CategoryId %>" SelectMethod="CategoriesDD_GetData" runat="server">
                    </asp:DropDownList>
                </p>

                <p>
                    <asp:Label Text="Content:" runat="server" />
                    <br />
                    <asp:TextBox CssClass="form-control article-content-edit" ID="UpdateArticleContentTB" Text="<%#: BindItem.Content %>" TextMode="MultiLine" runat="server" />
                    <asp:RequiredFieldValidator ErrorMessage="Content is required" ControlToValidate="UpdateArticleContentTB" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="Small" />
                </p>

                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>

            </div>
        </EditItemTemplate>

        <EmptyDataTemplate>
            <div class="alert alert-info">
                No Articles.
            </div>
        </EmptyDataTemplate>

    </asp:ListView>

    <br />


    <asp:MultiView ID="CreateArticleMultiView" ActiveViewIndex="0" runat="server">
        <asp:View ID="CreateBtnView" runat="server">
            <asp:Button CssClass="btn btn-primary pull-right" Text="Create new Article" ID="ChangeViewButt" OnClick="ChangeViewButt_Click" runat="server" />
            <br />
            <br />
            <br />
        </asp:View>

        <asp:View ID="CreateArticleView" runat="server">
            <br />

            <asp:FormView ID="CreateArticleFV" ItemType="ExamASP.NET.Models.Article" DefaultMode="Insert" InsertMethod="CreateArticleFV_InsertItem" runat="server">
                <InsertItemTemplate>
                    <div class="row">
                        <h3>
                            <asp:Label Text="Title:" runat="server" />
                            <asp:TextBox ID="CreateArticleTitleTB" Text='<%#: BindItem.Title %>' runat="server" />
                            <asp:RequiredFieldValidator ErrorMessage="Title is required" ControlToValidate="CreateArticleTitleTB" runat="server" ForeColor="Red" Display="Dynamic" Font-Size="Larger" />
                        </h3>

                        <p>
                            <asp:Label Text="Category:" runat="server" />
                            <asp:DropDownList ID="CreateCategoriesDD" DataValueField="Id" DataTextField="Name" ItemType="ExamASP.NET.Models.Category" SelectedValue="<%#: BindItem.CategoryId %>" SelectMethod="CategoriesDD_GetData" runat="server">
                            </asp:DropDownList>
                        </p>

                        <p>
                            <asp:Label Text="Content:" runat="server" />
                            <br />
                            <asp:TextBox CssClass="form-control article-content-edit" ID="UpdateArticleContentTB" Text="<%#: BindItem.Content %>" TextMode="MultiLine" runat="server" />
                            <asp:RequiredFieldValidator ErrorMessage="Content is required" ControlToValidate="UpdateArticleContentTB" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="Small" />
                        </p>
                    </div>

                    <asp:Button CssClass="btn btn-success" Text="Create" CommandName="Insert" runat="server" />
                    <asp:Button CssClass="btn btn-danger" Text="Cancel" CausesValidation="false" ID="CancelCreateArticleButt" OnClick="CancelCreateArticleButt_Click" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
        </asp:View>
    </asp:MultiView>


</asp:Content>
