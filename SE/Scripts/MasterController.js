var ipawsApp = angular.module('iPaws', []);
var contact = $.connection.userActivityHub;


function showNotification(message, type) {
    toastr.options.closeButton = true;
    toastr[type](message);
}

function sendRequest() {
    toastr.options.closeButton = true;
    toastr.success("Your request has been sent");
    contact.server.getCategoryNotifications(urlParams["userName"]);
    contact.server.sendMessage(urlParams["userName"], localStorage.getItem("currUser") + " has requested to use a category of yours.","info");
}

function sendRequest2(userName) {
    toastr.options.closeButton = true;
    toastr.success("Your request has been sent");
    contact.server.getCategoryNotifications(userName);
    contact.server.sendMessage(userName, localStorage.getItem("currUser") + " has requested to use a category of yours.", "info");
}

function evaluateRequests(eval, username, type) {
    contact.server.sendMessage(username, localStorage.getItem("currUser") + eval, type);
    contact.server.getCategoryNotifications(localStorage.getItem("currUser"));
}

function submitUserRequest(message) {
    toastr.options.closeButton = true;
    toastr["success"](message);
    contact.server.getTaskRequests(localStorage.getItem("currUser"));
    $('#taskRequestModal').modal('hide');
}


ipawsApp.controller('masterController', function($scope) {
    $scope.categories = [];
    $scope.tasks = [];


    $.connection.hub.start().done(function() {
        contact.server.getCategoryNotifications(localStorage.getItem("currUser"));
        contact.server.getTaskRequests(localStorage.getItem("currUser"));
    });

    contact.client.taskRequest = function (message) {
        console.log(message);
        toastr.options.closeButton = true;
        toastr['info'](message);
        contact.server.getTaskRequests(localStorage.getItem("currUser"));
    };

    contact.client.recieve = function (message, type) {
        toastr.options.closeButton = true;
        toastr[type](message);
    };

    contact.client.refresh = function (data, message) {
        console.log(data);
        toastr.options.closeButton = true;
        toastr["success"](message);
    };

    contact.client.yourTaskRequests = function (tasks) {
        $scope.tasks = [];
        var messageCount = document.getElementsByClassName("messageCount2");

        if (tasks.length === 0) {
            document.getElementById("tasks").style.color = "#428bca";
            document.getElementById("tsk_down").style.color = "#428bca";

            for (var h = 0; length = messageCount.length, h < length; h++) {
                messageCount[h].innerHTML = 0;
                messageCount[h].style.color = "#428bca";
            }

            $scope.tasks.push({
                "url": "#",
                "user": "Tasks",
                "date": "",
                "message": "No New Task Requests"
            });
        }
        for (var i = 0; len = tasks.length, i < len; i++) {
            var tsk = document.getElementById("tasks");
            var crt = document.getElementById("tsk_down");


            tsk.style.color = "yellow";
            crt.style.color = "yellow";

            for (var j = 0; count = messageCount.length, j < count; j++) {
                messageCount[j].style.color = "yellow";
                messageCount[j].innerHTML = i + 1;
            }


            $scope.tasks.push({
                "url": '/Admin/UserRequests.aspx',
                "user": tasks[i].UserName,
                "date": tasks[i].DateCompleted.substr(0, tasks[i].DateCompleted.lastIndexOf("T")),
                "message": "Has requested for you to create a task named " + tasks[i].TaskName + "."
            });
        }
        $scope.$apply();
    };

    contact.client.yourCategoryRequests = function (categories) {
        $scope.categories = [];
        var messageCount = document.getElementsByClassName("messageCount");
        console.log(categories);
        if (categories.length === 0) {
            document.getElementById("envelope").style.color = "#428bca";
            document.getElementById("msg_down").style.color = "#428bca";

            for (var h = 0; length = messageCount.length, h < length; h++) {
                messageCount[h].innerHTML = 0;
                messageCount[h].style.color = "#428bca";
            }

            messageCount.innerHTML = 0;

            $scope.categories.push({
                "url": '#',
                "user": "Categories",
                "date": "",
                "message": "No New Requests"
            });
        }
        for (var i = 0; len = categories.length, i < len; i++) {
            var tsk = document.getElementById("envelope");
            var crt = document.getElementById("msg_down");
            tsk.style.color = "yellow";
            crt.style.color = "yellow";

            for (var j = 0; count = messageCount.length, j < count; j++) {
                messageCount[j].style.color = "yellow";
                messageCount[j].innerHTML = i + 1;
            }


            $scope.categories.push({
                "url": '/Admin/SupervisorRequests.aspx',
                "user": categories[i].Requester,
                "date": categories[i].Date.substr(0, categories[i].Date.lastIndexOf("T")),
                "message": "Has requested permission to use " + categories[i].CategoryName + "and all of the tasks under that category"
            });
        }
        $scope.$apply();
    };

});
