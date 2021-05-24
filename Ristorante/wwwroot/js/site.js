﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var now = new Date();
var hour = now.getHours();
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

function SetInput() {
    var scelta = document.getElementById("data").value;
    console.log(scelta)
    if (hour > 10 && scelta == today) {
        $("#orario option[value='pranzo']").hide();
        if ($("#orario").val() === 'pranzo') {
            $("#orario").val('');
        }
    }
    else {
        $("#orario option[value='pranzo']").show();
    }
}

document.getElementById("data").addEventListener("change", SetInput);