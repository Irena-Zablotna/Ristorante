// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



var now = new Date();
var hour = now.getHours();
var scelta = document.getElementById("data").value;
var dd = now.getDate();
var mm = now.getMonth() + 1; //January is 0
var yyyy = now.getFullYear();

if (dd < 10) {
    dd = '0' + dd
}
if (mm < 10) {
    mm = '0' + mm
}

var today = yyyy + '-' + mm + '-' + dd;
document.getElementById("data").setAttribute("min", today);

function SetInput(){
    if (hour > 10 && scelta == today) {
        document.getElementById("pranzo").textContent = " ";
    }
}

document.getElementById("name").addEventListener("click", SetInput);