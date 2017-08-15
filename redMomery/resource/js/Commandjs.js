
function postJSON(url, data, callback) {
    var request;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        request = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        request = new ActiveXObject("Microsoft.XMLHTTP");
    }
    request.open("post", url, true);
    request.onreadystatechange = function () {     // Simple event handler
        if (request.readyState === 4 && callback) // When response is complete
            callback(request);                    // call the callback.
    };
    request.setRequestHeader("Content-Type", "application/json");
    request.send(data);
}

function getJson(url, data, callback)
{
    var request;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        request = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        request = new ActiveXObject("Microsoft.XMLHTTP");
    }
    request.open("GET", url, true);
    request.onreadystatechange = function () {     // Simple event handler
        if (request.readyState === 4 && callback) // When response is complete
            callback(request);                    // call the callback.
    };
    request.setRequestHeader("Content-Type", "application/json");
    request.send(data);
}

function GetXYByAddressName(adress,callback)
{
    url = "http://192.168.0.111:20000/redmomeryserver.asmx/GetXYByAddess";
    data = "{'address':'" + adress + "'}";
    postJSON(url, data, callback);
}