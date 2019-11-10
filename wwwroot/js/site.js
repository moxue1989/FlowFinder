// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    console.log("Starting Flow Finder!");
    getLocation();    
});

function showResults(results) {
    for (i = 0; i < results.length; i++) {
        var result = results[i];
        console.log(buildPanel(result));
    }
}

function buildPanel(result) {
    return result.destination.name;
}

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}

function showPosition(position) {
    $("#origin").val(position.coords.latitude + "," + position.coords.longitude);
}
