
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

//从string中提取XML对象，格式为：string 标签里面为对应的json数据文本格式
function xmlfromtext(xmlString) {
    var xmlDoc = null;
    //判断浏览器的类型
    //支持IE浏览器
    if (!window.DOMParser && window.ActiveXObject) { //window.DOMParser 判断是否是非ie浏览器
        var xmlDomVersions = ['MSXML.2.DOMDocument.6.0', 'MSXML.2.DOMDocument.3.0', 'Microsoft.XMLDOM'];
        for (var i = 0; i < xmlDomVersions.length; i++) {
            try {
                xmlDoc = new ActiveXObject(xmlDomVersions[i]);
                xmlDoc.async = false;
                xmlDoc.loadXML(xmlString); //loadXML方法载入xml字符串
                break;
            } catch (e) {
            }
        }
    }
        //支持Mozilla浏览器
    else if (window.DOMParser && document.implementation && document.implementation.createDocument) {
        try {
            /* DOMParser 对象解析 XML 文本并返回一个 XML Document 对象。
            * 要使用 DOMParser，使用不带参数的构造函数来实例化它，然后调用其 parseFromString() 方法
            * parseFromString(text, contentType) 参数text:要解析的 XML 标记 参数contentType文本的内容类型
            * 可能是 "text/xml" 、"application/xml" 或 "application/xhtml+xml" 中的一个。注意，不支持 "text/html"。
            */
            domParser = new DOMParser();
            xmlDoc = domParser.parseFromString(xmlString, 'text/xml');
        } catch (e) {
        }
    }
    else {
        return null;
    }
    return xmlDoc;

}


//这里是从XML中提取出对应的JSon数据文本格式，然后在将转化为对应的Json对象数组
//注意这个处理性能有限，只能用来处理简单的文本数据比如传输过来的XML只有一个子节点,这个程序的最初的目的是为了针对老兵的信息传输进行转换
function JsonFromXml(xml)
{
    var temp = xml;
    var json = {};
    temp = temp.childNodes[0].children;
    for (var i = 0; i < temp.length; i++) {
        json[i] = temp[i].innerHTML;
    }
    var lblist = [];
    for (var i in json) {
        var temp = JSON.parse(json[i]);
        lblist.push(temp);
    }
    temp = lblist;
    return temp;
}