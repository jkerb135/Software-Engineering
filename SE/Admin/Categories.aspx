<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="SE.Categories" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Categories</h1>
        </div>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="CategoryContainer" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="MainStepButton" />
                <asp:PostBackTrigger ControlID="EditDetailedStepButton" />
            </Triggers>
            <ContentTemplate>
                <div class="error-messages form-group">
                    <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
                </div>
                <div class="success-messages form-group">
                    <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
                </div>
                <asp:Panel ID="AddNewCategoryPanel" CssClass="form-group" runat="server" Visible="false">
                    <div class="form-inline">
                        <asp:Button ID="AddNewCategoryButton" runat="server"
                            CssClass="btn btn-success" Text="Add New Category"
                            OnClick="AddNewCategory_Click" />
                        <asp:Button ID="Update" runat="server" Visible="false"
                            CssClass="btn btn-primary" Text="Update Category"
                            OnClick="UpdateCategory_Click" />
                        <asp:Button ID="Delete" runat="server" Visible="false"
                            CssClass="btn btn-danger" Text="Delete Category"
                            OnClick="DeleteCategoryButton_Click" OnClientClick="return confirm('Are you sure you want to delete this category?');" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="TaskManagmentPanel" CssClass="form-group" runat="server" Visible="false">
                    <div class="form-inline">
                        <asp:Button ID="AddNewTask" runat="server"
                            CssClass="btn btn-success" Text="Add New Task"
                            OnClick="AddNewTask_Click" />
                        <asp:Button ID="UpdateTask" runat="server"
                            CssClass="btn btn-primary" Text="Update Task"
                            OnClick="UpdateTask_Click" />
                        <asp:Button ID="IsActiveTask" runat="server" OnClick="IsActiveTaskButton_Click"  />
                    </div>
                </asp:Panel>
                <asp:Panel ID="MainStepManagement" CssClass="form-group" runat="server" Visible="false">
                    <div class="form-inline">
                        <asp:Button ID="AddNewMainStep" runat="server"
                            CssClass="btn btn-success" Text="Add New Main Step"
                            OnClick="AddNewMainStep_Click" />
                        <asp:Button ID="MainStepMoveDown" CssClass="btn btn-default" runat="server" Text="down"
                            OnClick="MainStepMoveDown_Click" />
                        <asp:Button ID="MainStepMoveUp" CssClass="btn btn-default" runat="server" Text="up"
                            OnClick="MainStepMoveUp_Click" />
                        <asp:Button ID="UpdateMainStep" runat="server"
                            CssClass="btn btn-primary" Text="Update Main Step"
                            OnClick="UpdateMainStep_Click" />
                        <asp:Button ID="DeleteMainStep" runat="server"
                            CssClass="btn btn-danger" Text="Delete Main Step"
                            OnClick="DeleteMainStep_Click" OnClientClick="return confirm('Are you sure you want to delete this main step?');" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="DetailedStepManagement" CssClass="form-group" runat="server" Visible="false">
                    <div class="form-inline">
                        <asp:Button ID="AddNewDetailedStep" runat="server"
                            CssClass="btn btn-success" Text="Add New Detailed Step"
                            OnClick="AddNewDetailedStep_Click" />
                        <asp:Button ID="DetailedStepMoveDown" CssClass="btn btn-default" runat="server" Text="down"
                            OnClick="DetailedStepMoveDown_Click" />
                        <asp:Button ID="DetailedStepMoveUp" CssClass="btn btn-default" runat="server" Text="up"
                            OnClick="DetailedStepMoveUp_Click" />
                        <asp:Button ID="UpdateDetailedStep" runat="server"
                            CssClass="btn btn-primary" Text="Update Detailed Step"
                            OnClick="UpdateDetailedStep_Click" />
                        <asp:Button ID="DeleteDetailedStep" runat="server"
                            CssClass="btn btn-danger" Text="Delete Detailed Step"
                            OnClick="DeleteDetailedStep_Click" OnClientClick="return confirm('Are you sure you want to delete this detailed step?');" />
                    </div>
                </asp:Panel>
                <div class="form-group">
                    <asp:DropDownList ID="CategoryList" runat="server" CssClass="form-control"
                        DataValueField="CategoryID" DataTextField="CategoryName" AutoPostBack="true"
                        OnSelectedIndexChanged="CategoryList_SelectedIndexChanged" Visible="false">
                    </asp:DropDownList>
                </div>
                <asp:Panel ID="EditCategoryPanel" runat="server">
                    <div class="row form-group">
                        <div class="form-group col-lg-10">
                            <asp:Label ID="EditCategoryNameLabel" runat="server"
                                Text="Category Name"></asp:Label>
                            <asp:TextBox ID="EditCategoryName" CssClass="form-control"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-6">
                            <asp:Label ID="AllUsersLabel" runat="server"
                                Text="All Users"></asp:Label>
                            <asp:ListBox ID="AllUsers" CssClass="form-control"
                                runat="server"></asp:ListBox>
                        </div>
                        <div class="col-xs-6">
                            <asp:Label ID="UsersInCategoryLabel" runat="server"
                                Text="Users In Category"></asp:Label>
                            <asp:ListBox ID="UsersInCategory" CssClass="form-control col-xs-6"
                                runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 center">
                            <asp:Button ID="MoveLeft" CssClass="btn btn-default" runat="server" Text="<"
                                OnClick="MoveLeft_Click" />
                            <asp:Button ID="MoveRight" CssClass="btn btn-default" runat="server" Text=">"
                                OnClick="MoveRight_Click" />
                        </div>
                    </div>
                    <div class="inline-form right">
                        <asp:Button ID="EditCategoryCancel" CssClass="btn btn-default"
                            runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                        <asp:Button ID="EditCategoryButton" CssClass="btn btn-default"
                            runat="server" OnClick="EditCategoryButton_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="TaskPanel" runat="server" Visible="False">
                    <div class="row">
                        <div class="col-md-6 col-xs-12">
                            <div class="success-messages form-group">
                                <asp:Label ID="EditSuccessMessage" runat="server"></asp:Label>
                            </div>
                            <div class="error-messages form-group">
                                <asp:Label ID="EditErrorMessage" runat="server"></asp:Label>
                                <asp:ValidationSummary ID="CreateMainStepValidationSummary"
                                    ValidationGroup="CreateMainStep" runat="server" />
                                <asp:ValidationSummary ID="CreateDetailedStepValidationSummary"
                                    ValidationGroup="CreateDetailedStep" runat="server" />
                            </div>
                            <asp:Panel ID="EditTaskPanel" CssClass="form-group" runat="server">
                                <p class="form-group">Note: All fields are optional</p>
                                <div class="form-group">
                                    <asp:Label ID="EditTaskNameLabel" runat="server" Text="Task Name"></asp:Label>
                                    <asp:TextBox ID="EditTaskName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EditAssignUserToTaskLabel" runat="server" Text="Assign User to Task"></asp:Label>
                                    <asp:DropDownList ID="EditAssignUserToTask" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <asp:Button ID="EditTaskButton" runat="server" CssClass="btn btn-default right"
                                    Text="Submit" OnClick="EditTaskButton_Click" />
                                <asp:Button ID="EditTaskCancel" CssClass="btn btn-default"
                                    runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="ManageMainStepPanel" runat="server" Visible="false">
                    <div class="form-group">
                        <asp:Label ID="MainStepNameLabel" runat="server"
                            Text="Main Step Name"></asp:Label>
                        <asp:RequiredFieldValidator ID="MainStepNameRequired" runat="server" ControlToValidate="MainStepName"
                            ValidationGroup="CreateMainStep" ErrorMessage="Main Step Name is required.">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="MainStepName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="MainStepTextLabel" runat="server" Text="Main Step Text"></asp:Label>
                        <asp:TextBox ID="MainStepText" TextMode="multiline" Rows="5" runat="server" CssClass="form-control"></asp:TextBox>        
                    </div>
                    <div class="form-group">
                        <asp:Label ID="MainStepAudioLabel" runat="server" Text="Audio File"></asp:Label>
                        <asp:FileUpload ID="MainStepAudio" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="MainStepVideoLabel" runat="server" Text="Video File"></asp:Label>
                        <asp:FileUpload ID="MainStepVideo" runat="server" />
                    </div>
                    <div class="row form-group">
                        <div class="right form-group">
                            <asp:Button ID="MainStepCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-default" OnClick="ButtonCancel_Click" />
                        <asp:Button ID="MainStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateMainStep"
                                CssClass="btn btn-default" Text="Submit" OnClick="EditMainStepButton_Click" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="ManageDetailedStepPanel" runat="server" Visible="false">
                    <div class="form-group">
                        <asp:Label ID="DetailedStepNameLabel" runat="server" Text="Detailed Step Name"></asp:Label>
                        <asp:RequiredFieldValidator ID="DetailedStepNameRequired" runat="server" 
                            ControlToValidate="DetailedStepName" ValidationGroup="CreateDetailedStep" 
                            ErrorMessage="Detailed Step Name is required.">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="DetailedStepName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="DetailedStepTextLabel" runat="server" Text="Detailed Step Text"></asp:Label>
                        <asp:TextBox ID="DetailedStepText" TextMode="multiline" Rows="5" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="DetailedStepImageLabel" runat="server" Text="Image File"></asp:Label>
                        <asp:FileUpload ID="DetailedStepImage" runat="server" />
                    </div>
                    <div class="right form-group">
                        <asp:Button ID="EditDetailedStepCancel" runat="server" Text="Cancel" 
                            CssClass="btn btn-default" onclick="ButtonCancel_Click" />
                        <asp:Button ID="EditDetailedStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateDetailedStep" 
                            onclick="EditDetailedStepButton_Click" CssClass="btn btn-default" Text="Submit" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="ListBoxPanel" runat="server">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="catFilter" runat="server" placeholder="Filter Categories" AutoPostBack="true" OnTextChanged="catFilter_TextChanged" ToolTip="Enter Category Search Here"></asp:TextBox>
                                <asp:ListBox CssClass="form-control" ID="catList" runat="server" Height="500px" OnSelectedIndexChanged="QueryTasks" AutoPostBack="True" ToolTip="Click To Navigate" DataTextField="CategoryName" DataValueField="CategoryID"></asp:ListBox>
                            </div>
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="taskFilter" runat="server" placeholder="Filter Tasks"></asp:TextBox>
                                <asp:ListBox CssClass="col-lg-12 form-control" ID="taskList" runat="server" Height="500px" OnSelectedIndexChanged="QueryMainStep" AutoPostBack="True" DataTextField="TaskName" DataValueField="TaskID"></asp:ListBox>
                            </div>
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="mainFilter" runat="server" placeholder="Filter Main Steps"></asp:TextBox>
                                <asp:ListBox CssClass="col-lg-12 form-control" ID="mainStep" runat="server" Height="500px" AutoPostBack="True" OnSelectedIndexChanged="QueryDetailedStep" DataTextField="MainStepName" DataValueField="MainStepID"></asp:ListBox>
                            </div>
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="detailFilter" runat="server" placeholder="Filter Detailed Steps"></asp:TextBox>
                                <asp:ListBox CssClass="col-lg-12 form-control" ID="detailedStep" runat="server" Height="500px" AutoPostBack="True" OnSelectedIndexChanged="detailButtons" DataTextField="DetailedStepName" DataValueField="DetailedStepID"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:SqlDataSource ID="CategoryListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        SelectCommand="SELECT * FROM [Categories] WHERE ([CreatedBy] = @CreatedBy)" 
        ProviderName="System.Data.SqlClient" OnSelected="CategoryListSource_Selected" FilterExpression="[CategoryName] LIKE '%{0}%'">
        <FilterParameters>
            <asp:ControlParameter Name="CategoryName" ControlID="catFilter" PropertyName="Text" />
        </FilterParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CreatedBy" SessionField="UserName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="TaskListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:Parameter Name="CategoryID" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="MainStepListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:Parameter Name="TaskID" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DetailedStepListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:Parameter Name="MainStepID" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
