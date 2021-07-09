// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



window.addEventListener("scroll", () => {

    if (window.pageYOffset > 150) {
        $(".top-button").fadeIn(1000);
 } else {
        $(".top-button").fadeOut(1000);
   }
})
    
//$(".navbar a").on("click", function () {
//    $(".navbar").find(".active").removeClass("active");
//    $(this).parent().addClass("active");
//});