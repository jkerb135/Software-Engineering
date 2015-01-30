var ipawsApp = angular.module('iPaws', []);
var contact = $.connection.userActivityHub;


function showNotification(message, type) {
    console.log(message, type);
    toastr.options.closeButton = true;
    toastr[type](message);
}

function sendRequest() {
    toastr.options.closeButton = true;
    toastr.success("Your request has been sent");
    contact.server.getCategoryNotifications(urlParams["userName"]);
    contact.server.sendMessage(urlParams["userName"], localStorage.getItem("currUser") + " has requested to use a category of yours.","info");
}

function evaluateRequests(eval, username, type) {
    alert('this is the type: ' + type)
    contact.server.sendMessage(username, localStorage.getItem("currUser") + eval, type);
    contact.server.getCategoryNotifications(localStorage.getItem("currUser"));
}

ipawsApp.controller('masterController', function ($scope) {
    $scope.categories = [];
    $scope.tasks = [];


    $.connection.hub.start().done(function () {
        contact.server.getCategoryNotifications(localStorage.getItem("currUser"));
        contact.server.getTaskRequests(localStorage.getItem("currUser"));
    });

    contact.client.recieve = function (message,type) {
        toastr.options.closeButton = true;
        toastr[type](message);
    };

    contact.client.yourTaskRequests = function (tasks) {
        $scope.tasks = [];
        var messageCount = document.getElementById("messageCount2");

        if (tasks.length === 0) {
            document.getElementById("tasks").style.color = "#428bca";
            document.getElementById("tsk_down").style.color = "#428bca";
            document.getElementById("messageCount2").style.color = "#428bca";

            messageCount.innerHTML = 0;

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

            messageCount.style.color = "yellow";
            tsk.style.color = "yellow";
            crt.style.color = "yellow";

            $(tsk).fadeIn(100).fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
            $(crt).fadeIn(100).fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);

            messageCount.innerHTML = i + 1;

            $scope.tasks.push({
                "url": '/Admin/Requests.aspx',
                "user": tasks[i].UserName,
                "date": tasks[i].DateCompleted.substr(0, tasks[i].DateCompleted.lastIndexOf("T")),
                "message": "Has requested for you to create a task named " + tasks[i].TaskName + "."
            });
        }
        $scope.$apply();
    };

    contact.client.yourCategoryRequests = function (categories) {
        $scope.categories = [];
        var messageCount = document.getElementById("messageCount");
        console.log(categories);
        if (categories.length === 0) {
            document.getElementById("envelope").style.color = "#428bca";
            document.getElementById("msg_down").style.color = "#428bca";
            document.getElementById("messageCount").style.color = "#428bca";

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

            messageCount.style.color = "yellow";
            tsk.style.color = "yellow";
            crt.style.color = "yellow";

            $(tsk).fadeIn(100).fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
            $(crt).fadeIn(100).fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);

            messageCount.innerHTML = i + 1;

            $scope.categories.push({
                "url": '/Admin/Requests.aspx',
                "user": categories[i].Requester,
                "date": categories[i].Date.substr(0, categories[i].Date.lastIndexOf("T")),
                "message": "Has requested permission to use " + categories[i].CategoryName + "and all of the tasks under that category"
            });
        }
        $scope.$apply();
    };

});
