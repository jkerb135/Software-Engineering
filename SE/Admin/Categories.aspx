﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="SE.Categories" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
    <script>
        function previewTask() {
            console.log('clicked');
            var taskId = $(".taskValue").val();
            //var url = "http://localhost:6288/PreviewTask.aspx?taskId=" + taskId;
            var url = "http://acad.kutztown.edu/~jkerb135/FrontEnd/?taskId=" + taskId + "#dotask";
            window.open(url);
        }

        $(document).on("change", "input", function () {
            if (this.type === "file") {
                if (this.files[0].size > 4096) {
                    toastr['error']("File size is too large");
                    this.value = "";
                }
            } else {
                return;
            }
        });
    </script>
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel ID="CategoryContainer" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="MainStepButton" />
            <asp:PostBackTrigger ControlID="MainStepButtonNew" />
            <asp:PostBackTrigger ControlID="EditDetailedStepButton" />
            <asp:PostBackTrigger ControlID="EditDetailedStepButtonNew" />
            <asp:AsyncPostBackTrigger ControlID="HelpBtn" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <input type="hidden" class="taskValue" id="taskIDValue" value="" runat="server" />
                <div class="col-xs-6">
                    <h1 class="page-header">
                        <asp:Label ID="header" runat="server" Text="Management Panel"></asp:Label></h1>
                </div>
                <div class="col-xs-6" style="margin-top: 20px;">
                    <h1>
                        <asp:LinkButton class="fa fa-question-circle pull-right" runat="server" ID="HelpBtn" OnClick="HelpBtn_OnClick" Style="text-decoration: none;"></asp:LinkButton></h1>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">

                    <div class="error-messages form-group">
                        <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
                    </div>
                    <div class="success-messages form-group">
                        <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:DropDownList ID="CategoryList" runat="server" CssClass="form-control"
                            DataValueField="CategoryID" DataTextField="CategoryName" AutoPostBack="true"
                            OnSelectedIndexChanged="CategoryList_SelectedIndexChanged" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <asp:Panel ID="EditCategoryPanel" runat="server">
                        <div class="row form-group">
                            <div class="form-group col-xs-10">
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
                            <asp:Button ID="EditCategoryCancel" CssClass="btn btn-danger"
                                runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                            <asp:Button ID="EditCategoryButton" CssClass="btn btn-success"
                                runat="server" OnClick="EditCategoryButton_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TaskPanel" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-xs-12">
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
                                    <div class="row">
                                        <div class="form-group col-xs-12">
                                            <asp:Label ID="EditTaskNameLabel" runat="server" Text="Task Name"></asp:Label>
                                            <asp:TextBox ID="EditTaskName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-6">
                                            <asp:Label ID="AllUsersTaskLabel" runat="server"
                                                Text="All Users"></asp:Label>
                                            <asp:ListBox ID="AllUsersTask" CssClass="form-control"
                                                runat="server"></asp:ListBox>
                                        </div>
                                        <div class="col-xs-6">
                                            <asp:Label ID="UsersAssignedToTaskLabel" runat="server"
                                                Text="Users Assigned to Task"></asp:Label>
                                            <asp:ListBox ID="UsersAssignedToTask" CssClass="form-control col-xs-6"
                                                runat="server"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 center">
                                            <asp:Button ID="MoveLeftTaskAssignment" CssClass="btn btn-default" runat="server" Text="<"
                                                OnClick="MoveLeft_Click" />
                                            <asp:Button ID="MoveRightTaskAssignment" CssClass="btn btn-default" runat="server" Text=">"
                                                OnClick="MoveRight_Click" />
                                        </div>
                                    </div>
                                    <div class="right">
                                        <asp:Button ID="EditTaskCancel" CssClass="btn btn-danger"
                                            runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                                        <asp:Button ID="EditTaskButton" runat="server" CssClass="btn btn-success"
                                            Text="Submit" OnClick="EditTaskButton_Click" />
                                    </div>
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
                            <asp:Label ID="MainStepAudioCurrentLabel" CssClass="uploaded-file main-step-audio-preview" runat="server"></asp:Label>
                            <asp:FileUpload ID="MainStepAudio" CssClass="main-step-audio" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="MainStepVideoLabel" runat="server" Text="Video File"></asp:Label>
                            <asp:Label ID="MainStepVideoCurrentLabel" CssClass="uploaded-file main-step-video-preview" runat="server"></asp:Label>
                            <asp:FileUpload ID="MainStepVideo" CssClass="main-step-video" runat="server" />
                        </div>
                        <div class="row form-group">
                            <div class="right form-group">
                                <asp:Button ID="MainStepCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                                <asp:Button ID="MainStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateMainStep"
                                    CssClass="btn btn-success" Text="Submit" OnClick="EditMainStepButton_Click" />
                                <asp:Button ID="MainStepButtonNew" runat="server" CausesValidation="true" ValidationGroup="CreateMainStep"
                                    CssClass="btn btn-primary" Text="+ Main Step" OnClick="EditMainStepButton_Click" />
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
                            <asp:Label ID="DetailedStepImageCurrentLabel" CssClass="uploaded-file detailed-step-image-preview" runat="server"></asp:Label>
                            <asp:FileUpload ID="DetailedStepImage" CssClass="detailed-step-image" runat="server" />
                        </div>
                        <div class="right form-group">
                            <asp:Button ID="EditDetailedStepCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-danger" OnClick="ButtonCancel_Click" />
                            <asp:Button ID="EditDetailedStepButton" runat="server" CausesValidation="true" ValidationGroup="CreateDetailedStep"
                                OnClick="EditDetailedStepButton_Click" CssClass="btn btn-success" Text="Submit" />
                            <asp:Button ID="EditDetailedStepButtonNew" runat="server" CausesValidation="true" ValidationGroup="CreateDetailedStep"
                                CssClass="btn btn-primary" Text="+ Detailed Step" OnClick="EditDetailedStepButton_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ListBoxPanel" runat="server">
                        <div class="col-xs-12">
                            <div class="row center">
                                <div class="col-xs-3" style="padding: 5px">
                                    <div class="row" style="padding: 0px 0px 10px 15px">
                                        <div class="col-xs-7 reset">
                                            <asp:TextBox CssClass="form-control" ID="catFilter" runat="server" placeholder="Filter Categories" AutoPostBack="true" ToolTip="Enter Category Search Here"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button CssClass="form-control btn btn-primary" ID="catDateSort" runat="server" Text="" OnClick="catDateSort_Click" />
                                        </div>
                                    </div>
                                    <div class="row" style="padding: 0px 0px 0px 15px;">
                                        <div class="col-xs-3 reset">
                                            <asp:Button ID="AddNewCategoryButton" runat="server" CssClass="btn-sm btn-success form-control" Text="Add" OnClick="AddNewCategory_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="UpdateCategory" runat="server" CssClass="btn btn-primary form-control" Text="Update" OnClick="UpdateCategory_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="DeleteCategory" runat="server" CssClass="btn btn-danger form-control" Text="Deactivate" OnClick="DeleteCategoryButton_Click" />
                                        </div>
                                    </div>
                                    <asp:ListBox CssClass="form-control" Style="overflow-x: scroll" ID="catList" runat="server" Height="350px" OnSelectedIndexChanged="QueryTasks" AutoPostBack="True" ToolTip="Click To Navigate" AppendDataBoundItems="true" DataTextField="CategoryName" DataValueField="CategoryID"></asp:ListBox>
                                                                    <div class="row" style="padding: 0px 0px 0px 15px;">
                                        <div class="col-xs-11 reset">
                                            <asp:Button ID="PublishCategory" runat="server" CssClass="btn btn-success form-control" Text="Publish" OnClick="PublishCategory_OnClick"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-3" style="padding: 5px">
                                    <div class="row" style="padding: 0px 0px 10px 15px">
                                        <div class="col-xs-12 reset">
                                        </div>
                                        <div class="col-xs-7 reset">
                                            <asp:TextBox CssClass="form-control" ID="taskFilter" runat="server" placeholder="Filter Tasks" AutoPostBack="true" OnTextChanged="taskFilter_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button CssClass="form-control btn btn-primary" ID="taskDateSort" runat="server" Text="" OnClick="taskDateSort_Click" />
                                        </div>

                                    </div>

                                    <div class="row" style="padding: 0px 0px 0px 15px;">
                                        <div class="col-xs-3 reset">
                                            <asp:Button ID="AddNewTask" runat="server" CssClass="btn btn-success form-control" Text="Add " OnClick="AddNewTask_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="UpdateTask" runat="server" CssClass="btn btn-primary form-control" Text="Update" OnClick="UpdateTask_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="IsActiveTask" runat="server" CssClass="btn btn-danger form-control" OnClick="IsActiveTaskButton_Click" Text="Deactivate" />
                                        </div>
                                    </div>
                                    <asp:ListBox CssClass="col-xs-12 form-control" Style="overflow-x: scroll" ID="taskList" runat="server" Height="350px" OnSelectedIndexChanged="QueryMainStep" AutoPostBack="True" DataTextField="TaskName" DataValueField="TaskID" AppendDataBoundItems="true"></asp:ListBox>
                                    <div class="row" style="padding: 0px 0px 0px 15px;">
                                        <div class="col-xs-11 reset">
                                            <asp:Button ID="PreviewTask" runat="server" CssClass="btn btn-success form-control" Text="Preview Task" OnClientClick="previewTask();return false;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-3" style="padding: 5px">
                                    <div class="row" style="padding: 0px 0px 10px 15px;">
                                        <div class="col-xs-11 reset">
                                            <asp:TextBox CssClass="form-control" ID="mainFilter" runat="server" placeholder="Filter Main Steps" AutoPostBack="true" OnTextChanged="mainFilter_TextChanged"></asp:TextBox>
                                        </div>
                                        <!--<div class="col-xs-4 reset">
                                            <asp:Button CssClass="form-control btn btn-primary" ID="mainStepSort" runat="server" Text="" OnClick="mainStep_Sort" />
                                        </div>-->
                                    </div>
                                    <div class="row" style="padding: 0px 0px 0px 15px;">
                                        <div class="col-xs-3 reset">
                                            <asp:Button ID="AddNewMainStep" runat="server" CssClass="btn btn-success form-control" Text="Add" OnClick="AddNewMainStep_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="UpdateMainStep" runat="server" CssClass="btn btn-primary form-control" Text="Update" OnClick="UpdateMainStep_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="DeleteMainStep" runat="server" CssClass="btn btn-danger form-control" Text="Delete" OnClick="DeleteMainStep_Click" OnClientClick="return confirm('Are you sure you want to delete this main step?');" />
                                        </div>
                                    </div>

                                    <asp:ListBox CssClass="col-xs-12 form-control" ID="mainStep" Style="overflow-x: scroll" runat="server" Height="350px" AutoPostBack="True" OnSelectedIndexChanged="QueryDetailedStep" DataTextField="MainStepName" DataValueField="MainStepID" AppendDataBoundItems="true"></asp:ListBox>
                                    <div class="form-inline">
                                        <asp:Button ID="MainStepMoveDown" CssClass="btn btn-default form-control" runat="server" Text="Move Step Down" OnClick="MainStepMoveDown_Click" Visible="True" />
                                        <asp:Button ID="MainStepMoveUp" CssClass="btn btn-default form-control" runat="server" Text="Move Step Up" OnClick="MainStepMoveUp_Click" Visible="True" />
                                    </div>
                                </div>
                                <div class="col-xs-3" style="padding: 5px">
                                    <div class="row" style="padding: 0px 0px 10px 15px;">
                                        <div class="col-xs-11 reset">
                                            <asp:TextBox CssClass="form-control" ID="detailFilter" runat="server" placeholder="Filter Detailed Steps" AutoPostBack="true" OnTextChanged="detailFilter_TextChanged"></asp:TextBox>
                                        </div>
                                        <!--<div class="col-xs-4 reset">
                                            <asp:Button CssClass="form-control btn btn-primary" ID="detailedSort" runat="server" Text="" OnClick="detailedDateSort_Click" />
                                        </div>-->
                                    </div>
                                    <div class="row" style="padding: 0px 0px 0px 15px;">
                                        <div class="col-xs-3 reset">
                                            <asp:Button ID="AddNewDetailedStep" runat="server" CssClass="btn btn-success form-control" Text="Add" OnClick="AddNewDetailedStep_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="UpdateDetailedStep" runat="server" CssClass="btn btn-primary form-control" Text="Update" OnClick="UpdateDetailedStep_Click" />
                                        </div>
                                        <div class="col-xs-4 reset">
                                            <asp:Button ID="DeleteDetailedStep" runat="server" CssClass="btn btn-danger form-control" Text="Delete" OnClick="DeleteDetailedStep_Click" OnClientClick="return confirm('Are you sure you want to delete this detailed step?');" />
                                        </div>
                                    </div>

                                    <asp:ListBox CssClass="col-xs-12 form-control" ID="detailedStep" Style="overflow-x: scroll" runat="server" Height="350px" AutoPostBack="True" OnSelectedIndexChanged="DetailButtons" DataTextField="DetailedStepName" DataValueField="DetailedStepID" AppendDataBoundItems="true"></asp:ListBox>
                                    <div class="form-inline">
                                        <asp:Button ID="DetailedStepMoveDown" CssClass="btn btn-default" runat="server" Text="Move Step Down"
                                            OnClick="DetailedStepMoveDown_Click" />
                                        <asp:Button ID="DetailedStepMoveUp" CssClass="btn btn-default" runat="server" Text="Move Step Up"
                                            OnClick="DetailedStepMoveUp_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                    <h4 class="modal-title" style="color: white;">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body" style="overflow-y: scroll; max-height: 350px">
                                    <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Ok</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

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
        ProviderName="System.Data.SqlClient" FilterExpression="[TaskName] LIKE '%{0}%'">
        <FilterParameters>
            <asp:ControlParameter Name="TaskName" ControlID="taskFilter" PropertyName="Text" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter Name="CategoryID" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="MainStepListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        ProviderName="System.Data.SqlClient" FilterExpression="[MainStepName] LIKE '%{0}%'">
        <FilterParameters>
            <asp:ControlParameter Name="MainStepName" ControlID="mainFilter" PropertyName="Text" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter Name="TaskID" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DetailedStepListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        ProviderName="System.Data.SqlClient" FilterExpression="[DetailedStepName] LIKE '%{0}%'">
        <FilterParameters>
            <asp:ControlParameter Name="DetailedStepName" ControlID="detailFilter" PropertyName="Text" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter Name="MainStepID" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
