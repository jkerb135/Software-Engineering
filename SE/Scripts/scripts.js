function showPreview(files) {
    for (var i = 0, f; f = files[i]; i++) {
        var reader = new FileReader();
        reader.onload = function (evt) {
            var mimeType = evt.target.result.split(",")[0].split(":")[1].split(";")[0];

            if (mimeType.match(/audio/)) {
                var audioExt = [".mp3", ".wav", ".m4a", ".mwa"];
                var audio = "<audio class='audio-preview' controls>" +
                    "<source src='" + evt.target.result + "'/></audio>";
                $('.main-step-audio-preview').append(audio);
            }
            if (mimeType.match(/video/)) {
                var videoExt = [".mp3", ".wav", ".m4a", ".mwa"];
                var video = "<video class='video-preview' controls>" +
                    "<source src='" + evt.target.result + "'/></video>";
                $('.main-step-video-preview').append(video);
            }
            if (mimeType.match(/image/)) {
                var image = "<img class='image-preview' src='" + evt.target.result + "'/>";
                $('.detailed-step-image-preview').append(image);
            }
        }
        reader.readAsDataURL(f);
    }
}

$("body").on('change', '.main-step-audio', function (evt) {
    $('.main-step-audio-preview').empty();
    showPreview(evt.target.files);
});

$("body").on('change', '.main-step-video', function (evt) {
    $('.main-step-video-preview').empty();
    showPreview(evt.target.files);
});

$("body").on('change', '.detailed-step-image', function (evt) {
    $('.detailed-step-image-preview').empty();
    showPreview(evt.target.files);
});