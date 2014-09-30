<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="SE.Tasks" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Tasks</h1>
        </div>
    </div>
    <asp:MultiView ID="TasksMultiView" ActiveViewIndex="0" runat="server">

        <asp:View ID="CreateTask" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <h2>Create Task</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <div class="error-messages form-group">
                        <asp:ValidationSummary ID="CreateTaskValidationSummary" 
                            ValidationGroup="CreateTask" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="AssignTaskToCategoryLabel" runat="server" 
                            Text="Assign Task to Category"></asp:Label>
                        <asp:RequiredFieldValidator ID="AssignTaskToCategoryRequired" runat="server" ControlToValidate="AssignTaskToCategory"
                            ValidationGroup="CreateTask" ErrorMessage="Assign Task to Category is required.">*</asp:RequiredFieldValidator>
                        <asp:DropDownList ID="AssignTaskToCategory" runat="server" CssClass="form-control"
                            DataValueField="CategoryID" DataTextField="CategoryName" AutoPostBack="false">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="AssignUserToTaskLabel" runat="server" Text="Assign User to Task"></asp:Label>
                        <asp:DropDownList ID="AssignUserToTask" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="TaskNameLabel" runat="server" Text="Task Name"></asp:Label>
                        <asp:RequiredFieldValidator ID="TaskNameRequired" runat="server" ControlToValidate="TaskName"
                            ValidationGroup="CreateTask" ErrorMessage="Task Name is required.">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="TaskName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:Button ID="CreateTaskButton" CssClass="btn btn-default right" CausesValidation="true" 
                        ValidationGroup="CreateTask" runat="server" Text="Submit" onclick="CreateTaskButton_Click" />
                </div>
            </div>
            <asp:SqlDataSource ID="CategoryListSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" 
                
                SelectCommand="SELECT * FROM [Categories] WHERE ([CreatedBy] = @CreatedBy)" 
                ProviderName="System.Data.SqlClient">
                <SelectParameters>
                    <asp:SessionParameter Name="CreatedBy" SessionField="UserName" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>

        <asp:View ID="EditTask" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <h2>Edit Task</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <asp:UpdatePanel ID="EditTaskContainer" runat="server">
                        <ContentTemplate>
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
                                <div class="form-group">
                                    <asp:Label ID="EditTaskNameLabel" runat="server" Text="Task Name"></asp:Label>
                                    <asp:TextBox ID="EditTaskName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Button ID="EditTaskButton" runat="server" CssClass="btn btn-default right" 
                                    Text="Submit" onclick="EditTaskButton_Click" />
                                <asp:Button ID="ViewMainStepButton" runat="server" 
                                    CssClass="btn btn-info clear block" Text="Main Steps" 
                                    onclick="ViewMainStepButton_Click" />
                            </asp:Panel>
                            <asp:Panel ID="ManageMainStepPanel" runat="server" Visible="false">
                                <div class="form-group">
                                    <asp:Button ID="BackToTask" runat="server" CssClass="btn btn-primary" 
                                        Text="Back To Task" onclick="BackToTask_Click" />
                                    <asp:Button ID="AddNewMainStepButton" runat="server"
                                        CssClass="btn btn-default" Text="New Main Step" 
                                        onclick="AddNewMainStep_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="MainStepListLabel" runat="server" Text="Main Steps"></asp:Label>
                                    <asp:ListBox ID="MainStepList" runat="server" CssClass="form-control"
                                        DataValueField="MainStepID" DataTextField="MainStepName" >
                                    </asp:ListBox>
                                </div>
                                <div class="row form-group">
                                    <div class="col-xs-12 center">
                                        <asp:Button ID="MainStepMoveDown" CssClass="btn btn-default" runat="server" Text="down" 
                                            onclick="MainStepMoveDown_Click" />
                                        <asp:Button ID="MainStepMoveUp" CssClass="btn btn-default" runat="server" Text="up" 
                                            onclick="MainStepMoveUp_Click" />
                                        <asp:Button ID="MainStepEdit" CssClass="btn btn-primary" runat="server" Text="edit" 
                                            onclick="MainStepEdit_Click" />
                                        <asp:Button ID="MainStepDelete" CssClass="btn btn-danger" runat="server" Text="X" 
                                            onclick="MainStepDelete_Click" />
                                        <asp:Button ID="ViewDetailedSteps" CssClass="btn btn-secondary" runat="server" Text="detailed steps" 
                                            onclick="ViewDetailedSteps_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="NewMainStepPanel" runat="server" Visible="false">
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
                                <div class="right form-group">
                                    <asp:Button ID="MainStepCancel" runat="server" Text="Cancel" 
                                        CssClass="btn btn-default" onclick="MainStepCancel_Click" />
                                    <asp:Button ID="MainStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateMainStep" 
                                        CssClass="btn btn-default" Text="Submit" onclick="MainStepButton_Click" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="EditMainStepPanel" CssClass="clear" runat="server" Visible="false">
                                <div class="form-group">
                                    <asp:Label ID="EditMainStepNameLabel" runat="server" 
                                        Text="Main Step Name"></asp:Label>
                                    <asp:TextBox ID="EditMainStepName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EditMainStepTextLabel" runat="server" Text="Main Step Text"></asp:Label>
                                    <asp:TextBox ID="EditMainStepText" TextMode="multiline" Rows="5" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EditMainStepAudioLabel" runat="server" Text="Audio File"></asp:Label>
                                    <asp:FileUpload ID="EditMainStepAudio" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EditMainStepVideoLabel" runat="server" Text="Video File"></asp:Label>
                                    <asp:FileUpload ID="EditMainStepVideo" runat="server" />
                                </div>
                                <div class="right form-group">
                                    <asp:Button ID="EditMainStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateMainStep" 
                                        onclick="EditMainStepButton_Click" CssClass="btn btn-default" Text="Submit" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="ManageDetailedStepPanel" CssClass="clear" runat="server" Visible="false">
                                <div class="form-group">
                                    <asp:Button ID="BackToMainStep" runat="server" CssClass="btn btn-primary" 
                                        Text="Back To Main Step" onclick="BackToMainStep_Click" />
                                    <asp:Button ID="AddNewDetailedStepButton" runat="server"
                                        CssClass="btn btn-default" Text="New Detailed Step" 
                                        onclick="AddNewDetailedStep_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="DetailedStepListLabel" runat="server" Text="Detailed Steps"></asp:Label>
                                    <asp:ListBox ID="DetailedStepList" runat="server" CssClass="form-control"
                                        DataValueField="DetailedStepID" DataTextField="DetailedStepName" >
                                    </asp:ListBox>
                                </div>
                                <div class="row form-group">
                                    <div class="col-xs-12 center">
                                        <asp:Button ID="DetailedStepMoveDown" CssClass="btn btn-default" runat="server" Text="down" 
                                            onclick="DetailedStepMoveDown_Click" />
                                        <asp:Button ID="DetailedStepMoveUp" CssClass="btn btn-default" runat="server" Text="up" 
                                            onclick="DetailedStepMoveUp_Click" />
                                        <asp:Button ID="DetailedStepEdit" CssClass="btn btn-primary" runat="server" Text="edit" 
                                            onclick="DetailedStepEdit_Click" />
                                        <asp:Button ID="DetailedStepDelete" CssClass="btn btn-danger" runat="server" Text="X" 
                                            onclick="DetailedStepDelete_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="NewDetailedStepPanel" runat="server" Visible="false">
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
                                    <asp:Button ID="DetailedStepCancel" runat="server" Text="Cancel" 
                                        CssClass="btn btn-default" onclick="DetailedStepCancel_Click" />
                                    <asp:Button ID="DetailedStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateDetailedStep" 
                                        onclick="DetailedStepButton_Click" CssClass="btn btn-default" Text="Submit" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="EditDetailedStepPanel" runat="server" Visible="false">
                                <div class="form-group">
                                    <asp:Label ID="EditDetailedStepNameLabel" runat="server" Text="Detailed Step Name"></asp:Label>
                                    <asp:TextBox ID="EditDetailedStepName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EditDetailedStepTextLabel" runat="server" Text="Detailed Step Text"></asp:Label>
                                    <asp:TextBox ID="EditDetailedStepText" TextMode="multiline" Rows="5" 
                                        runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EditDetailedStepImageLabel" runat="server" Text="Image File"></asp:Label>
                                    <asp:FileUpload ID="EditDetailedStepImage" runat="server" />
                                </div>
                                <div class="right form-group">
                                    <asp:Button ID="EditDetailedStepButton" runat="server" CssClass="btn btn-default" 
                                        Text="Submit" onclick="EditDetailedStepButton_Click" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:SqlDataSource ID="MainStepListSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" 
                SelectCommand="SELECT * FROM [MainSteps] WHERE ([TaskID] = @TaskID) ORDER BY ListOrder" 
                ProviderName="System.Data.SqlClient">
                <SelectParameters>
                    <asp:QueryStringParameter Name="TaskID" QueryStringField="taskid" 
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="DetailedStepListSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" 
                SelectCommand="SELECT * FROM [DetailedSteps] WHERE ([MainStepID] = @MainStepID) ORDER BY ListOrder" 
                ProviderName="System.Data.SqlClient">
                <SelectParameters>
                    <asp:ControlParameter ControlID="MainStepList" Name="MainStepID" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>

        <asp:View ID="ManageTasks" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <h2>Manage Tasks</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:UpdatePanel ID="ManageTaskGrid" runat="server">
                        <ContentTemplate>
                            <div class="success-messages form-group">
                                <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
                            </div>     
                            <div class="table-responsive">
                                <asp:DataGrid ID="TaskList" CssClass="table table-bordered table-striped" runat="server" 
                                    AllowPaging="true" OnPageIndexChanged="TaskList_Change">
                                </asp:DataGrid>
                            </div>         
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:View>

    </asp:MultiView>
</asp:Content>
