
function displayFunction1() {
    var x = document.getElementById("info-row-current");
    if (x.style.display === "none") {
        x.style.display = "table-row";
    } else {
        x.style.display = "none";
    }
}



function displayFunction2() {
    var x = document.getElementById("info-row-progress");
    if (x.style.display === "none") {
        x.style.display = "table-row";
    } else {
        x.style.display = "none";
    }
}



function displayFunction3() {
    var x = document.getElementById("info-row-End");
    if (x.style.display === "none") {
        x.style.display = "table-row";
    } else {
        x.style.display = "none";
    }
}


//Start pop

function check_empty() {
    if ( document.getElementById('email').value == "") {
        alert("Fill All Fields !");
    } else {
        document.getElementById('form').submit();
        alert("Form Submitted Successfully...");
    }
}
//Function To Display Popup
function div_show(id) {
    var inputf = document.getElementById("id");
    inputf.setAttribute('value', id);
    document.getElementById('abc').style.display = "block";
}
//Function to Hide Popup
function div_hide() {
    document.getElementById('abc').style.display = "none";
}
//End pop