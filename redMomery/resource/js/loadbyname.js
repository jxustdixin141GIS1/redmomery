/// <reference path="D:\题库系统\redMomery\redmomery.server\resource/Commandjs.js" />
function LoadLBbyname(LBname) {

    var name = LBname;
    var url = configUrl + "/redmomeryserver.asmx/getLBINfo"
    var data1 = { LBname: name };
    $.ajax({
        type: "post",
        dataType: "json",
        url: url,
        data: { LBname: name },


        success: function (data) {
            var data1 = data.response;
            var g = JSON.parse(data1)
            var data = g.d;//取出对象，json
            temp = data1;
            var divcontex = $("#LB_story")[0];
            divcontex.innerHTML = data1[0].toString();
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        complete: function (data) {
            //注意这里是需要判别下是不是json格式的数据，若是不是的话，就只能硬钢Xml了
            var data1 = data.responseText;
            var xmls = xmlfromtext(data1);
            var temp = xmls;
            var json = {};
            //这些代码可以处理大批量的数据格式
            temp = temp.childNodes[0].children
            for (var i = 0; i < temp.length; i++) {
                json[i] = temp[i].innerHTML;
            }
            var lblist = [];
            for (var i in json) {
                var temp = JSON.parse(json[i]);
                lblist.push(temp);
            }
            temp = lblist;
             lb = temp[0];
            show(lb);
            //了数据转换完成
        }
    });
}

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

function show(lb) {
    var divs = $("#LB_story")[0];
    divs.innerHTML = "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "姓&nbsp&nbsp名：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBname + "</span>" + "</p>" +
                      "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "性&nbsp&nbsp别：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBsex + "</span>" + "</p>" +
                      "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "出生日期：" + "</span>" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBbirthday + "</span>" + "</p>" +
                      "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "部队职务：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBjob + "</span>" + "</p>" +
                      "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "现居住地：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBdomicile + "</span>" + "</p>" +
                      "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "部队番号：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.designation + "</span>" + "</p>" +
                      "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "生活现状：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBlife + "</span>" + "</p>"
                       + "<p >" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBexperience + "</span>" + "</p>" +
                        "<p >" + "<img src='" + lb.LBPhoto.toString().replace('~/', "") + "' width=30%   height=30% />" + "</p>";
}