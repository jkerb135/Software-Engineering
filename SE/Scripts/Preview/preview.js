var api = "http://localhost:6288/api/";
var url = "http://ipawsteamb.csweb.kutztown.edu";
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
$(function () {
    sessionStorage.setItem("stepnum", 0);
    $(document).bind('pageinit');
    $("#next").hide();
    $("#finish").hide();
    $("#pump").hide();
    $("#done").hide();
    $("#detailSteps").hide();
    $("#start").show();

    var taskId = getParameterByName('taskId');
    console.log(taskId);
    getMainSteps(taskId);
});
function TaskObject(id, name, text, audio, video) {
    this.stepId = id;
    this.text = name;
    this.stepText = text;
    this.audio = audio;
    this.video = video;
}

function getMainSteps(taskId) {
    var mtotal = 0;
    $("#bot").empty();
    var mrequest = api + "MainStep/GetMainStepByTaskId/" + taskId;
    $.ajax({
        type: "GET",
        url: mrequest,
        datatype: 'json',
        success: function(data, status, xhr) {
            console.log(data);
            $.each(data, function(key, main) {
                mtotal += 1;
                var taskData = JSON.stringify(new TaskObject(main.mainStepId, main.mainStepName, main.mainStepText, main.audioPath, main.videoPath));
                sessionStorage.setItem(mtotal, taskData);
                sessionStorage.setItem('totalSteps', mtotal);
                $("#bot").append('<li id="step ' + mtotal + '">' + main.mainStepName + '</li>');
                $('#bot').listview().listview('refresh');
                sessionStorage.setItem('maintotal', mtotal);
            }); // end of each
        }, //end of success
        error: function(xhr) {
            console.log(xhr.responseText);
        }
    }); //end of ajax
}

function getDetailedSteps(mainId) {
        var dtotal = 0;
        $("#bot").empty();
        var drequest = api + "DetailedStep/GetDetailedStepById/" + mainId;
        $.ajax({
            type: "GET",
            url:  drequest,
            datatype: 'json',
            success: function (data, status, xhr) {
                console.log(data);
                $.each(data, function (key, detail) {
                    dtotal += 1;
                    console.log(detail.imagePath);
                    $("#detailstep").append('<p id="step' + detail.detailedStepId + '">Step: ' + dtotal + ': ' + detail.detailedStepText + '</p>');
                    if (detail.imagePath != null) {
                        var imgUrl = url + detail.imagePath.substr(detail.imagePath.indexOf('~') + 1);
                        $("#image").append('<p><img src="' + imgUrl + '" height="200" alt="' + detail.imageName + '" /></p>');
                    }


                });// end of each
            },//end of success
            error: function (xhr) {
                console.log(xhr.responseText);
            }
        });//end of ajax


}function next() {

    var stepup = sessionStorage.getItem("stepnum");
    stepup = parseInt(stepup);
    $('#step' + stepup).prepend('completed - ');
    stepup += 1;
    console.log("stepup" + stepup);
    sessionStorage.setItem("stepnum", stepup);
    var end = sessionStorage.getItem("maintotal");
    if (end == stepup) {
        setpage();
        $("#next").hide();
        $("#done").hide();
        $("#finish").show();
    }
    else {
        setpage();
    }

}function setpage() {
    var step = sessionStorage.getItem("stepnum");
    stepup = parseInt(step);
    console.log(step);

    console.log(JSON.parse(sessionStorage.getItem(step)));
    var test = JSON.parse(sessionStorage.getItem(step));
    $("#steptitle").text(test.text); 
    $("#av").empty();
    if (test.video != null) {
        var videoUrl = url + test.video.substr(test.video.indexOf('~') + 1);
        $("#av").append('<video width="400" controls><source src="' + videoUrl + '" type="video/mp4"><img src="images/video.png" border="0" height="50px" /></video>');
    }
    if (test.audio != null) {
        var audioUrl = url + test.audio.substr(test.audio.indexOf('~') + 1);
        $("#av").append('<audio width="400" controls><source src="' + audioUrl + '" type="audio/mp3"><img src="images/audio.png" border="0" height="50px" /></audio>');
    }
    $("#detailstep").empty();
    $("#image").empty();
    var id = (test.stepId);
    console.log("id" + id);
    getDetailedSteps(id);
}
$(document).on('click', '#start', function () {
    $("#next").hide();
    $("#pump").hide();
    $("#done").show();
    $("#start").hide();
    $("#detailSteps").show();
    next();
});$(document).on('click', '#done', function () {
    $("#next").show();
    $("#pump").show();
    $("#done").hide();
    $("#start").hide();
});$(document).on('click', '#next', function () {
    $("#next").hide();
    $("#pump").hide();
    $("#done").show();
    $("#start").hide();
    next();
});$(document).on('click', '#finish', function () {
    cleartask();
    window.close();
});function cleartask() {
    sessionStorage.clear();
    sessionStorage.setItem("stepnum", 0);
}