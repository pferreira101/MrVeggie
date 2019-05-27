// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$("#myLink").click(function (e) {

    e.preventDefault();
    $.ajax({

        url: $(this).attr("href"), // comma here instead of semicolon   
        success: function () {
            alert("Value Added");  // or any other indication if you want to show
        }

    });

});