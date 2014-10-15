$(function () {
    console.log('Working');
    $(document).on("click", "#catButton", function () {
        console.log('clicked');
        $("#catData").show("slow");
        $("#userData").hide();
        $("#taskData").hide();
    });
    $(document).on("click", "#taskButton", function () {
        console.log('clicked');
        $("#catData").hide();
        $("#userData").hide();
        $("#taskData").show("slow");
    });
    $(document).on("click", "#assignedUsers", function () {
        console.log('clicked');
        $("#catData").hide();
        $("#taskData").hide();
        $("#userData").show("slow");
    });
    


});