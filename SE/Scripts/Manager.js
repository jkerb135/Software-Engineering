var openDiv;
$(document).on('click', '.addSupervisor', function() {
    $('#SiteBody_managerState').val('Supervisor');
    $('#usersContainer').attr("style", "display:none");
    $('#usermanager').hide('slide', { direction: 'left' }, 1000);
    $('#usermanager').promise().done(function() {
        $('#addSupervisor').show('slide', { direction: 'left' }, 1000);
        openDiv = $('#addSupervisor');
        $('.panel-body').slideToggle();
        $('#mgmtHead').toggleClass("fa-arrow-up fa-arrow-down");
    });
});
$(document).on('click', '.addUser', function() {
    $('#SiteBody_managerState').val('User');
    $('#usersContainer').attr("style", "display:block");
    $('#usermanager').hide('slide', { direction: 'left' }, 1000);
    $('#usermanager').promise().done(function() {
        $('#addSupervisor').show('slide', { direction: 'left' }, 1000);
        openDiv = $('#addSupervisor');
        $('.panel-body').slideToggle();
        $('#mgmtHead').toggleClass("fa-arrow-up fa-arrow-down");
    });
});
$(document).on('click', '#goback', function() {
    $(openDiv).hide('slide', { direction: 'left' }, 1000);
    $(openDiv).promise().done(function() {
        $('#usermanager').show('slide', { direction: 'left' }, 1000);
        openDiv = $('#usermanager');
        $('.panel-body').slideToggle();
        $('#mgmtHead').toggleClass("fa-arrow-up fa-arrow-down");
    });
});
$(document).on('click', '#userManagement', function() {
    $('.panel-body').slideToggle();
    $('#mgmtHead').toggleClass("fa-arrow-up fa-arrow-down");
});