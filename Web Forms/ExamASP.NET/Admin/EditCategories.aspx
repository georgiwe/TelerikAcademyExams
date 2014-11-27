<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCategories.aspx.cs" Inherits="ExamASP.NET.Admin.EditCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ListView ID="EditCategoryLV" DataKeyNames="Id" ItemType="ExamASP.NET.Models.Category" runat="server"
        UpdateMethod="EditCategoryLV_UpdateItem"
        SelectMethod="EditCategoryLV_GetData"
        DeleteMethod="EditCategoryLV_DeleteItem"
        InsertMethod="EditCategoryLV_InsertItem"
        InsertItemPosition="LastItem">

        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th>
                            <asp:LinkButton CssClass="btn btn-default" Text="Category Name" CausesValidation="false" CommandName="Sort" CommandArgument="Name" runat="server" />
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </tbody>
            </table>

            <br />

            <asp:DataPager PageSize="5" runat="server">
                <Fields>
                    <asp:NextPreviousPagerField ButtonCssClass="btn btn-default" />
                    <asp:NumericPagerField CurrentPageLabelCssClass="active" NextPreviousButtonCssClass="btn btn-default" />
                </Fields>
            </asp:DataPager>

        </LayoutTemplate>

        <ItemTemplate>
            <tr>
                <td><%#: Item.Name %></td>
                <td>
                    <asp:LinkButton CssClass="btn btn-info" Text="Edit" CausesValidation="false" CommandName="Edit" runat="server" />
                    <asp:LinkButton CssClass="btn btn-danger" Text="Delete" CausesValidation="false" CommandName="Delete" runat="server" />
                </td>
            </tr>
        </ItemTemplate>

        <EditItemTemplate>
            <tr>
                <td>
                    <asp:TextBox CssClass="form-control" ID="EditCategoryNameTB" Text='<%#: BindItem.Name %>' runat="server" />
                    <asp:RequiredFieldValidator ErrorMessage="Name is required!" ControlToValidate="EditCategoryNameTB" Display="Dynamic" ForeColor="Red" Font-Size="Small" runat="server" />
                </td>
                <td>
                    <asp:LinkButton CssClass="btn btn-info" Text="Save" CausesValidation="false" CommandName="Update" runat="server" />
                    <asp:LinkButton CssClass="btn btn-danger" Text="Cancel" CommandName="Cancel" CausesValidation="false" runat="server" />
                </td>
            </tr>
        </EditItemTemplate>

        <InsertItemTemplate>
            <tr>
                <td>
                    <asp:TextBox CssClass="form-control" ID="EditCategoryNameTB" Text='<%#: BindItem.Name %>' runat="server" />
                    <asp:RequiredFieldValidator ErrorMessage="Name is required!" ControlToValidate="EditCategoryNameTB" Display="Dynamic" ForeColor="Red" Font-Size="Small" runat="server" />
                </td>
                <td>
                    <asp:LinkButton CssClass="btn btn-success" Text="Add" CommandName="Insert" runat="server" />
                    <asp:LinkButton CssClass="btn btn-danger" Text="Cancel" CommandName="Cancel" CausesValidation="false" runat="server" />
                </td>
            </tr>
        </InsertItemTemplate>

        <EmptyDataTemplate>
            <div class="alert alert-info">
                <span>No Categories.</span>
            </div>
        </EmptyDataTemplate>

    </asp:ListView>
</asp:Content>
