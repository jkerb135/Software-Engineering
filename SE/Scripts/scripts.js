﻿/*
Author			: Josh Kerbaugh\Daniel Talley
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: Login.aspx.cs
Purpose			: This is the main script file for generation of a report preview for the manager.
*/
function showPreview(files, type) {
    for (var i = 0, f; f = files[i]; i++) {
        var reader = new FileReader();
        reader.onload = function (evt) {
            var mimeType = evt.target.result.split(",")[0].split(":")[1].split(";")[0];

            if (type == "audio" && mimeType.match(/audio/)) {
                var audio = "<audio class='audio-preview' controls>" +
                    "<source src='" + evt.target.result + "'/></audio>";
                $('.main-step-audio-preview').append(audio);
            }
            if (type == "video" && mimeType.match(/video/)) {
                var video = "<video class='video-preview' controls>" +
                    "<source src='" + evt.target.result + "'/></video>";
                $('.main-step-video-preview').append(video);
            }
            if (type = "image" && mimeType.match(/image/)) {
                var image = "<img class='image-preview' src='" + evt.target.result + "'/>";
                $('.detailed-step-image-preview').append(image);
            }
        }
        reader.readAsDataURL(f);
    }
}

$("body").on('change', '.main-step-audio', function (evt) {
    $('.main-step-audio-preview').empty();
    showPreview(evt.target.files, "audio");
});

$("body").on('change', '.main-step-video', function (evt) {
    $('.main-step-video-preview').empty();
    showPreview(evt.target.files, "video");
});

$("body").on('change', '.detailed-step-image', function (evt) {
    $('.detailed-step-image-preview').empty();
    showPreview(evt.target.files, "image");
});

$(document).ready(function () {
    $('body').on('click', '.print-report', function () {
        pathArray = window.location.href.split('/');
        protocol = pathArray[0];
        host = pathArray[2];
        baseUrl = protocol + '//' + host;
        var report = popupwindow(baseUrl + "/Admin/PreviewReport.aspx", "Preview Report", "800", "500");
        report.print();
    });
});

function popupwindow(url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}