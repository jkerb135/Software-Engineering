    $(document).on('click', '.addSupervisor', function () {
        $('#usermanager').toggle("slide", { direction: "right" }, 1000);
            $('#addSupervisor').toggle("slide", { direction: "right" }, 1000);
    });
    $(document).on('click', '.addUser', function () {
        $('#usermanager').toggle("slide", { direction: "right" }, 1000);
        $('#addUser').toggle("slide", { direction: "right" }, 1000);
    });
    $(document).on('click', '.addUser', function () {
        console.log('add user');
    });
    $(document).on('click', '.back', function () {
        console.log('back');
    });
