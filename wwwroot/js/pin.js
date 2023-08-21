$(document).ready(function () {
    $(".pinButton").click(function (e) {
        e.preventDefault();

        var itemId = $(this).data("id");

        $.ajax({
            type: "POST",
            url: "/Chatrooms/Pin/" + itemId,
            success: function (result) {
                // Handle the success response here
                console.log("Pin successful!");
            },
            error: function (error) {
                // Handle the error response here
                console.log("Pin failed:", error);
            }
        });
    });
});