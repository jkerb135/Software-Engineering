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
                        <asp:Button ID="Update" runat="server"
                            CssClass="btn btn-primary" Text="Update Category"
                            OnClick="UpdateCategory_Click" />
                        <asp:Button ID="Delete" runat="server"
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
                        <asp:Button ID="DeleteTask" runat="server"
                            CssClass="btn btn-danger" Text="Delete Task"
                            OnClick="DeleteTaskButton_Click" OnClientClick="return confirm('Are you sure you want to delete this task?');" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="MainStepManagement" CssClass="form-group" runat="server" Visible="false">
                    <div class="form-inline">
                        <asp:Button ID="Button2" runat="server"
                            CssClass="btn btn-success" Text="Add New Main Step"
                            OnClick="AddNewMainStep_Click" />
                        <asp:Button ID="Button3" runat="server"
                            CssClass="btn btn-primary" Text="Update Main Step"
                            OnClick="UpdateTask_Click" />
                        <asp:Button ID="Button4" runat="server"
                            CssClass="btn btn-danger" Text="Delete Main Step"
                            OnClick="DeleteCategoryButton_Click" OnClientClick="return confirm('Are you sure you want to delete this category?');" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="DetailedStepManagement" CssClass="form-group" runat="server" Visible="false">
                    <div class="form-inline">
                        <asp:Button ID="Button5" runat="server"
                            CssClass="btn btn-success" Text="Add New Detailed Step"
                            OnClick="AddNewMainStep_Click" />
                        <asp:Button ID="Button6" runat="server"
                            CssClass="btn btn-primary" Text="Update Detailed Step"
                            OnClick="UpdateTask_Click" />
                        <asp:Button ID="Button7" runat="server"
                            CssClass="btn btn-danger" Text="Delete Detailed Step"
                            OnClick="DeleteCategoryButton_Click" OnClientClick="return confirm('Are you sure you want to delete this category?');" />
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
                            runat="server" Text="Cancel" OnClick="EditCategoryCancel_Click" />
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
                                    <asp:Label ID="EditAssignTaskToCategoryLabel" runat="server"
                                        Text="Assign Task to Category"></asp:Label>
                                    <asp:DropDownList ID="EditAssignTaskToCategory" runat="server" CssClass="form-control"
                                        DataValueField="CategoryID" DataTextField="CategoryName" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EditAssignUserToTaskLabel" runat="server" Text="Assign User to Task"></asp:Label>
                                    <asp:DropDownList ID="EditAssignUserToTask" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <asp:Button ID="EditTaskButton" runat="server" CssClass="btn btn-default right"
                                    Text="Submit" OnClick="EditTaskButton_Click" />
                                <asp:Button ID="Button1" CssClass="btn btn-default"
                                    runat="server" Text="Cancel" OnClick="EditCategoryCancel_Click" />
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
                        <asp:Button ID="MainStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateMainStep"
                                CssClass="btn btn-success" Text="Submit" OnClick="EditMainStepButton_Click" />
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
                    <div class="form-group">
                        <asp:Label ID="MainStepListLabel" runat="server" Text="Reorder Steps"></asp:Label>
                        <asp:ListBox ID="MainStepList" runat="server" CssClass="form-control"
                            DataValueField="MainStepID" DataTextField="MainStepName"></asp:ListBox>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-12 center">
                            <asp:Button ID="MainStepMoveDown" CssClass="btn btn-default" runat="server" Text="down"
                                OnClick="MainStepMoveDown_Click" />
                            <asp:Button ID="MainStepMoveUp" CssClass="btn btn-default" runat="server" Text="up"
                                OnClick="MainStepMoveUp_Click" />
                            <asp:Button ID="MainStepDelete" CssClass="btn btn-danger" runat="server" Text="X"
                                OnClick="MainStepDelete_Click" />
                        </div>
                        <div class="right form-group">
                            <asp:Button ID="MainStepCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-default" OnClick="EditCategoryCancel_Click" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="ListBoxPanel" runat="server">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="catFilter" runat="server" placeholder="Filter Categories" AutoPostBack="true" OnTextChanged="catFilter_TextChanged" ToolTip="Enter Category Search Here"></asp:TextBox>
                                <asp:ListBox CssClass="form-control" ID="catList" runat="server" Height="500px" OnSelectedIndexChanged="QueryTasks" AutoPostBack="True" ToolTip="Click To Navigate"></asp:ListBox>
                            </div>
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="taskFilter" runat="server" placeholder="Filter Tasks"></asp:TextBox>
                                <asp:ListBox CssClass="col-lg-12 form-control" ID="taskList" runat="server" Height="500px" OnSelectedIndexChanged="QueryMainStep" AutoPostBack="True"></asp:ListBox>
                            </div>
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="mainFilter" runat="server" placeholder="Filter Main Steps"></asp:TextBox>
                                <asp:ListBox CssClass="col-lg-12 form-control" ID="mainStep" runat="server" Height="500px" AutoPostBack="True" OnSelectedIndexChanged="QueryDetailedStep"></asp:ListBox>
                            </div>
                            <div class="col-lg-3" style="padding: 0px">
                                <asp:TextBox CssClass="form-control" ID="detailFilter" runat="server" placeholder="Filter Detailed Steps"></asp:TextBox>
                                <asp:ListBox CssClass="col-lg-12 form-control" ID="detailedStep" runat="server" Height="500px" AutoPostBack="True" OnSelectedIndexChanged="detailButtons"></asp:ListBox>
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
        ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:SessionParameter Name="CreatedBy" SessionField="UserName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="MainStepListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        SelectCommand="SELECT * FROM [MainSteps] WHERE ([TaskID] = @TaskID) ORDER BY ListOrder"
        ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:QueryStringParameter Name="TaskID" QueryStringField="taskid"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
