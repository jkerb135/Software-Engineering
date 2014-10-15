$(function () {
    $(document).on("click", "#catButton", function () {
        $("#catData").show("slow");
        $("#userData").hide();
        $("#taskData").hide();
    });
    $(document).on("click", "#taskButton", function () {
        $("#catData").hide();
        $("#userData").hide();
        $("#taskData").show("slow");
    });
    $(document).on("click", "#assignedUsers", function () {
        $("#catData").hide();
        $("#taskData").hide();
        $("#userData").show("slow");
    });
});